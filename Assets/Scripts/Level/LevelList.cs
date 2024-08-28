using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using static Match3.LevelData;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor.SceneManagement;
#endif

namespace Match3
{
    [Serializable]
    public struct LevelGoals
    {
        public GemGoal[] Goals;
    }

    [CreateAssetMenu]
    public class LevelList : ScriptableObject
    {
#if UNITY_EDITOR
        // Editörde, sahne varlýklarý için bir dizi içerir
        public SceneAsset[] Scenes;
#endif

        public LevelGoals[] Goals;
        public int[] TargetScore;
        public int[] MaxMove;

        [HideInInspector] public int[] SceneList;

        public int SceneCount
        {
            get
            {
#if UNITY_EDITOR
                return Scenes.Length;
#else
                return SceneList.Length;
#endif
            }
        }

        /// <summary>
        /// Aktif sahnenin indeksini döndürür.
        /// </summary>
        /// <returns>Aktif sahnenin indeksi</returns>
        public static int GetSceneIndex()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            string sceneNumber = SceneManager.GetActiveScene().name.Substring(5, sceneName.Length - 5);
            return int.Parse(sceneNumber) - 1;
        }

        /// <summary>
        /// Belirtilen seviyeyi yükler.
        /// </summary>
        /// <param name="levelNumber">Yüklenecek seviye numarasý</param>
        public void LoadLevel(int levelNumber)
        {
#if UNITY_EDITOR
            // Editörde, sahneyi doðrudan yükler
            EditorSceneManager.LoadSceneInPlayMode(AssetDatabase.GetAssetPath(Scenes[levelNumber]),
                new LoadSceneParameters(LoadSceneMode.Single));
#else
            // Build sýrasýnda, normal sahne yöneticisi aracýlýðýyla yükler
            SceneManager.LoadScene(SceneList[levelNumber], LoadSceneMode.Single);
#endif
        }
    }

#if UNITY_EDITOR
    class BuildLevelList : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        /// <summary>
        /// Build öncesi kontrolleri ve ayarlarý yapar.
        /// </summary>
        /// <param name="report">Build raporu</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            try
            {
                var levelListAssets = AssetDatabase.FindAssets("t:LevelList");

                if (levelListAssets.Length == 0)
                {
                    throw new BuildFailedException("Bir level list bulunamadý, build iþlemi iptal edildi");
                }

                var levelList =
                    AssetDatabase.LoadAssetAtPath<LevelList>(AssetDatabase.GUIDToAssetPath(levelListAssets[0]));

                if (levelList.Scenes.Length == 0)
                {
                    throw new BuildFailedException("Level list sahne dizisi boþ, build iþlemi iptal edildi");
                }

                var buildLevels = EditorBuildSettings.scenes;
                var levels = new int[levelList.Scenes.Length];

                bool buildListChange = false;

                for (int i = 0; i < levelList.Scenes.Length; ++i)
                {
                    var sceneAsset = levelList.Scenes[i];
                    var scenePath = AssetDatabase.GetAssetPath(sceneAsset);

                    if (sceneAsset == null)
                    {
                        throw new BuildFailedException("Level list içinde null sahne bulunuyor, düzeltmeden build yapmayýn");
                    }

                    var idx = Array.FindIndex(buildLevels, scene => scene.path == scenePath);

                    if (idx == -1)
                    {
                        idx = buildLevels.Length - 1;
                        ArrayUtility.Add(ref buildLevels, new EditorBuildSettingsScene(scenePath, true));
                        buildListChange = true;
                    }
                    else if (!buildLevels[idx].enabled)
                    {
                        buildLevels[idx].enabled = true;
                        buildListChange = true;
                    }

                    levels[i] = idx;
                }

                bool levelListChanged = false;
                for (int i = 0; i < levels.Length; ++i)
                {
                    if (i >= levelList.SceneList.Length || levels[i] != levelList.SceneList[i])
                    {
                        levelListChanged = true;
                        break;
                    }
                }

                if (levelListChanged)
                {
                    levelList.SceneList = levels;
                    EditorUtility.SetDirty(levelList);
                    AssetDatabase.SaveAssetIfDirty(levelList);
                }

                if (levelListChanged || buildListChange)
                {
                    EditorBuildSettings.scenes = buildLevels;
                    EditorUtility.DisplayDialog("Build Durduruldu",
                        "Build sýrasýnda sahne listesi, LevelList varlýklarýndaki liste ile eþleþecek þekilde deðiþtirildi.\n" +
                        "Sahne listesi düzeltildi, lütfen build iþlemini yeniden baþlatýn.", "Tamam");

                    throw new BuildFailedException("Level List yeniden inþa edildi, build iþlemini yeniden baþlatýn");
                }
            }
            catch (Exception e)
            {
                throw new BuildFailedException($"Build öncesi iþlem sýrasýnda bir istisna oluþtu {e.Message}");
            }
        }
    }
#endif
}
