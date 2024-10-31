using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Match3
{
    /// <summary>
    /// GameManager, oyundaki t m sistemler aras nda aray z sa lar. Oyun ba larken Loading sahnesi taraf ndan ba lat l r
    /// veya ilk eri imde dinamik olarak kaynak klas r nden y klenir. Bu sayede oyunun herhangi bir noktas nda oynat labilir.
    /// </summary>
    [DefaultExecutionOrder(-9999)]
    public class GameManager : MonoBehaviour
    {
        // Y netici silindi inde bu true olarak ayarlan r. Di er nesnelerden  nce silinebildi i i in kullan  l d r.
        private static bool s_IsShuttingDown = false;
        
        private static GameManager s_Instance;

        public Board Board;
        public InputAction ClickAction;
        public InputAction ClickPosition;
        public GameSettings Settings;

        public int Stars { get; private set; }
        public int Lives { get; private set; } = 5;

        public SoundData Volumes => m_SoundData;

        public List<BoosterItemEntry> BoosterItems = new();

        public VFXPoolSystem PoolSystem { get; private set; } = new();

        //  ki ses kayna   kullan yoruz, b ylece ge i  yapabiliriz
        private AudioSource MusicSourceActive;
        private AudioSource MusicSourceBackground;
        private Queue<AudioSource> m_SFXSourceQueue = new();

        private GameObject m_BoosterModePrefab;

        public SoundData m_SoundData = new();

        public static GameManager Instance
        {
            get
            {
                // Edit rde,  rnek dinamik olarak olu turulabilir, b ylece herhangi bir sahneyi kurmadan oynatabiliriz.
                // Bir yap mda, ilk sahne her  eyi ba lat r, bu nedenle bir  rnek zaten mevcut olacakt r.
#if UNITY_EDITOR
                if (s_Instance == null && !s_IsShuttingDown)
                {
                    var newInstance = Instantiate(Resources.Load<GameManager>("GameManager"));
                    newInstance.Awake();
                }
#endif
                return s_Instance;
            }

            private set => s_Instance = value;
        }
        
        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                DontDestroyOnLoad(gameObject);
        
                InitializeGameManager();
            }
            else if (s_Instance != this)
            {
                Destroy(gameObject); // Sahne değişiminde ikinci bir GameManager oluşturulmasını önlemek için
                return;
            }
        }

        private void InitializeGameManager()
        {
            Application.targetFrameRate = 60;

            ClickAction.Enable();
            ClickPosition.Enable();

            MusicSourceActive = Instantiate(Settings.SoundSettings.MusicSourcePrefab, transform);
            MusicSourceBackground = Instantiate(Settings.SoundSettings.MusicSourcePrefab, transform);

            MusicSourceActive.volume = 1.0f;
            MusicSourceBackground.volume = 0.0f;

            for (int i = 0; i < 16; ++i)
            {
                var sourceInst = Instantiate(Settings.SoundSettings.SFXSourcePrefab, transform);
                m_SFXSourceQueue.Enqueue(sourceInst);
            }

            if (Settings.VisualSettings.BoosterModePrefab != null)
            {
                m_BoosterModePrefab = Instantiate(Settings.VisualSettings.BoosterModePrefab);
                m_BoosterModePrefab.SetActive(false);
            }

            LoadSoundData();
        }


        private void OnDestroy()
        {
            if (s_Instance == this && !s_IsShuttingDown)
            {
                s_IsShuttingDown = true;
            }
        }

        
        private void Update()
        {
            if (MusicSourceActive && MusicSourceActive.volume < 1.0f)
            {
                MusicSourceActive.volume = Mathf.MoveTowards(MusicSourceActive.volume, 1.0f, Time.deltaTime * 0.5f);
                MusicSourceBackground.volume = Mathf.MoveTowards(MusicSourceBackground.volume, 0.0f, Time.deltaTime * 0.5f);
            }
        }

        public static bool IsShuttingDown()
        {
            return s_IsShuttingDown;
        }

        [Serializable]
        public class SoundData
        {
            public float MainVolume = 1.0f;
            public float MusicVolume = 1.0f;
            public float SFXVolume = 1.0f;
        }

        [System.Serializable]
        public class BoosterItemEntry
        {
            public int Amount;
            public BoosterItem Item;
        }

        void GetReferences()
        {
            Board = FindFirstObjectByType<Board>();
        }

        /// <summary>
        /// LevelData taraf ndan  a r l r, yeni bir seviyeye ba land   n  GameManager'a bildirir.
        /// </summary>
        public void StartLevel()
        {
            GetReferences();

            LevelData.Instance.OnAllGoalFinished += () =>
            {
                Instance.Board.ToggleInput(false);
                Instance.Board.TriggerFinalStretch();
            };

            LevelData.Instance.OnNoMoveLeft += () =>
            {
                Instance.Board.ToggleInput(false);
                Instance.Board.TriggerFinalStretch();
            };

            if (LevelData.Instance.Music != null)
            {
                SwitchMusic(LevelData.Instance.Music);
            }

            // Tahtan n ba lat lmas n  geciktiriyoruz, t m karolar n ba lat lmas na yeterli zaman b rak yoruz
            StartCoroutine(DelayedInit());
        }

        IEnumerator DelayedInit()
        {
            yield return null;

            Board.Init();
            ComputeCamera();
        }

        public void ComputeCamera()
        {
            // Kameray  oyun alan n n ortas na bakacak  ekilde ayarlar ve tam olarak kareye almak i in ortografik ayar n  de i tirir
            var bounds = Board.Bounds;
            Vector3 center = Board.Grid.CellToLocalInterpolated(bounds.center) + new Vector3(0.5f, 0.5f, 0.0f);
            center = Board.transform.TransformPoint(center);

            //  st  ubu un kal nl    nedeniyle 1 yukar  kayd r yoruz, bu sayede merkez  st ve alt  ubu un aras nda daha iyi olur
            Camera.main.transform.position = center + Vector3.back * 10.0f + Vector3.up * 0.75f;

            float halfSize = 0.0f;

            if (Screen.height > Screen.width)
            {
                float screenRatio = Screen.height / (float)Screen.width;
                halfSize = ((bounds.size.x + 1) * 0.5f + LevelData.Instance.BorderMargin) * screenRatio;
            }
            else
            {
                // Geni  ekranlarda, dikey olarak s  d r yoruz
                halfSize = (bounds.size.y + 3) * 0.5f + LevelData.Instance.BorderMargin;
            }

            halfSize += LevelData.Instance.BorderMargin;

            Camera.main.orthographicSize = halfSize;
        }

        /// <summary>
        /// Ana Men  taraf ndan  a r l r, GameManager'a men de oldu umuzu bildirir, bu nedenle oyun UI's n  gizlememiz gerekir.
        /// </summary>
        public void MainMenuOpened()
        {
            PoolSystem.Clean();

            SwitchMusic(Instance.Settings.SoundSettings.MenuSound);
        }

        public void ChangeStars(int amount)
        {
            Stars += amount;
            if (Stars < 0)
                Stars = 0;
        }

        /// <summary>
        /// Oyuncu seviyeyi tamamlad   nda  a r l r ve y ld zlar  kazand r r.
        /// </summary>
        public void WinStar()
        {
            string input = SceneManager.GetActiveScene().name;
            string levelNumber = input.Substring("Level".Length);
            int levelIndex = int.Parse(levelNumber) - 1;
            int earnedStarCount = 0;

            ScoreManager.SetLevelScore(levelIndex + 1, LevelData.Instance.CurrentScore);

            if (LevelData.Instance.CurrentScore >= LevelData.Instance.TargetScore * 0.33)
            {
                earnedStarCount = 1;
            }
            if (LevelData.Instance.CurrentScore >= LevelData.Instance.TargetScore * 0.66)
            {
                earnedStarCount = 2;
            }
            if (LevelData.Instance.CurrentScore >= LevelData.Instance.TargetScore)
            {
                earnedStarCount = 3;
            }

            StarManager.AddStarCount(earnedStarCount);
            LevelCompletion.CompleteLevel(levelIndex, earnedStarCount);
        }

        /// <summary>
        /// Ya am say s n  art r r.
        /// </summary>
        public void AddLive(int amount)
        {
            Lives += amount;
        }

        /// <summary>
        /// Ya am say s n  azalt r.
        /// </summary>
        public void LoseLife()
        {
            Lives -= 1;
        }

        /// <summary>
        /// Ses ayarlar n  g nceller.
        /// </summary>
        public void UpdateVolumes()
        {
            Settings.SoundSettings.Mixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.MainVolume)) * 30.0f);
            Settings.SoundSettings.Mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.MusicVolume)) * 30.0f);
            Settings.SoundSettings.Mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.SFXVolume)) * 30.0f);
        }

        public void SaveSoundData()
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/sounds.json", JsonUtility.ToJson(m_SoundData));
        }

        void LoadSoundData()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/sounds.json"))
            {
                JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(Application.persistentDataPath + "/sounds.json"), m_SoundData);
            }

            UpdateVolumes();
        }

        /// <summary>
        /// Booster item' n  listeye ekler.
        /// </summary>
        public void AddBoosterItem(BoosterItem item)
        {
            var existingItem = BoosterItems.Find(entry => entry.Item == item);
            if (existingItem != null)
            {
                existingItem.Amount += 1;
            }
            else
            {
                BoosterItems.Add(new BoosterItemEntry()
                {
                    Amount = 1,
                    Item = item
                });
            }
        }

        /// <summary>
        /// Booster item' n  aktif eder.
        /// </summary>
        public void ActivateBoosterItem(BoosterItem item)
        {
            LevelData.Instance.DarkenBackground(item != null);
            m_BoosterModePrefab?.SetActive(item != null);
            Board.ActivateBoosterItem(item);
        }

        /// <summary>
        /// Booster item' n  devre d    b rak r.
        /// </summary>
        public void DeactiveBoosterItem()
        {
            Board.DeactiveBoosterItem();
        }

        /// <summary>
        /// Booster item' n  kullan r ve listeden   kar r.
        /// </summary>
        public void UseBoosterItem(BoosterItem item, Vector3Int cell)
        {
            var existingItem = BoosterItems.Find(entry => entry.Item == item);
            if (existingItem == null) return;

            existingItem.Item.Use(cell);
            existingItem.Amount -= 1;

            m_BoosterModePrefab?.SetActive(false);
        }

        /// <summary>
        /// Ses efektlerini oynat r.
        /// </summary>
        public AudioSource PlaySFX(AudioClip clip)
        {
            var source = m_SFXSourceQueue.Dequeue();
            m_SFXSourceQueue.Enqueue(source);

            source.clip = clip;
            source.Play();

            return source;
        }

        /// <summary>
        /// M zi i de i tirir.
        /// </summary>
        void SwitchMusic(AudioClip music)
        {
            MusicSourceBackground.clip = music;
            MusicSourceBackground.Play();
            (MusicSourceActive, MusicSourceBackground) = (MusicSourceBackground, MusicSourceActive);
        }
    }
}