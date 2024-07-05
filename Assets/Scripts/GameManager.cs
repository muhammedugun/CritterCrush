using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.VFX;

namespace Match3
{
    /// <summary>
    /// The GameManager is the interface between all the system in the game. It is either instantiated by the Loading scene
    /// which is the first at the start of the game, or Loaded from the Resource Folder dynamically at first access in editor
    /// so we can press play from any point of the game without having to add it to every scene and test if it already exist
    /// </summary>
    [DefaultExecutionOrder(-9999)]
    public class GameManager : MonoBehaviour
    {
        //This is set to true when the manager is deleted. This is useful as the manager can be deleted before other
        //objects 
        private static bool s_IsShuttingDown = false;
        
        public static GameManager Instance
        {
            get
            {
                
                // In Editor, the instance can be crated on the fly so we can play any scene without setup to do.
                // In a build, the first scene will Init all that so we are sure there will already be an instance.
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

        //we use two sources so we can crossfade
        private AudioSource MusicSourceActive;
        private AudioSource MusicSourceBackground;
        private Queue<AudioSource> m_SFXSourceQueue = new();

        private GameObject m_BoosterModePrefab;
    
        private VisualEffect m_WinEffect;
        private VisualEffect m_LoseEffect;
        
        private SoundData m_SoundData = new();

        private void Awake()
        {
            if (s_Instance == this)
            {
                return;
            }

            if (s_Instance == null)
            {
                s_Instance = this;
                DontDestroyOnLoad(gameObject);
                
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

                m_WinEffect = Instantiate(Settings.VisualSettings.WinEffect, transform);
                m_LoseEffect = Instantiate(Settings.VisualSettings.LoseEffect, transform);

                LoadSoundData();
            }
        }

        private void OnDestroy()
        {
            if (s_Instance == this) s_IsShuttingDown = true;
        }

        void GetReferences()
        {
            Board = FindFirstObjectByType<Board>();
        }

        /// <summary>
        /// Called by the LevelData when it awake, notify the GameManager we started a new level.
        /// </summary>
        public void StartLevel()
        {
            GetReferences();
          

            m_WinEffect.gameObject.SetActive(false);
            m_LoseEffect.gameObject.SetActive(false);
            
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

            PoolSystem.AddNewInstance(Settings.VisualSettings.CoinVFX, 12);

            //we delay the board init to leave enough time for all the tile to init
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
            //setup the camera so it look at the center of the play area, and change its ortho setting so it perfectly frame
            var bounds = Board.Bounds;
            Vector3 center = Board.Grid.CellToLocalInterpolated(bounds.center) + new Vector3(0.5f, 0.5f, 0.0f);
            center = Board.transform.TransformPoint(center);
            
            //we offset of 1 up as the top bar is thicker, so this center it better between the top & bottom bar
            Camera.main.transform.position = center + Vector3.back * 10.0f + Vector3.up * 0.75f;

            float halfSize = 0.0f;
            
            if (Screen.height > Screen.width)
            {
                float screenRatio = Screen.height / (float)Screen.width;
                halfSize = ((bounds.size.x + 1) * 0.5f + LevelData.Instance.BorderMargin) * screenRatio;
            }
            else
            {
                //On Wide screen, we fit vertically
                halfSize = (bounds.size.y + 3) * 0.5f + LevelData.Instance.BorderMargin;
            }

            halfSize += LevelData.Instance.BorderMargin;
        
            Camera.main.orthographicSize = halfSize;
        }

        /// <summary>
        /// Called by the Main Menu when it open, notify the GameManager we are back in the menu so need to hide the Game UI.
        /// </summary>
        public void MainMenuOpened()
        {
            PoolSystem.Clean();
            m_WinEffect.gameObject.SetActive(false);
            m_LoseEffect.gameObject.SetActive(false);
            
            SwitchMusic(Instance.Settings.SoundSettings.MenuSound);
        }

        private void Update()
        {

            if (MusicSourceActive.volume < 1.0f)
            {
                MusicSourceActive.volume = Mathf.MoveTowards(MusicSourceActive.volume, 1.0f, Time.deltaTime * 0.5f);
                MusicSourceBackground.volume = Mathf.MoveTowards(MusicSourceBackground.volume, 0.0f, Time.deltaTime * 0.5f);
            }
        }

        public void ChangeStars(int amount)
        {
            Stars += amount;
            if (Stars < 0)
                Stars = 0;
        
       
        }

        public void WinStar()
        {
            string input = SceneManager.GetActiveScene().name;
            string levelNumber = input.Substring("Level".Length);
            int levelIndex = int.Parse(levelNumber) - 1;
            int earnedStarCount=0;

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

        public void AddLive(int amount)
        {
            Lives += amount;
        }

        public void LoseLife()
        {
            Lives -= 1;
        }

        public void UpdateVolumes()
        {
            Settings.SoundSettings.Mixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.MainVolume)) * 30.0f);
            Settings.SoundSettings.Mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.SFXVolume)) * 30.0f);
            Settings.SoundSettings.Mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(0.0001f, m_SoundData.MusicVolume)) * 30.0f);
        }

        public void SaveSoundData()
        {
            System.IO.File.WriteAllText(Application.persistentDataPath + "/sounds.json", JsonUtility.ToJson(m_SoundData));
        }

        void LoadSoundData()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/sounds.json"))
            {
                JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(Application.persistentDataPath+"/sounds.json"), m_SoundData);
            }
            
            UpdateVolumes();
        }

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

        public void ActivateBoosterItem(BoosterItem item)
        {
            LevelData.Instance.DarkenBackground(item != null);
            m_BoosterModePrefab?.SetActive(item != null);
            Board.ActivateBoosterItem(item);
        }

        public void DeactiveBoosterItem()
        {
            Board.DeactiveBoosterItem();
        }

        public void UseBoosterItem(BoosterItem item, Vector3Int cell)
        {
            var existingItem = BoosterItems.Find(entry => entry.Item == item);
            if(existingItem == null) return;
        
            existingItem.Item.Use(cell);
            existingItem.Amount -= 1;
            
            m_BoosterModePrefab?.SetActive(false);

        }

        public AudioSource PlaySFX(AudioClip clip)
        {
            var source = m_SFXSourceQueue.Dequeue();
            m_SFXSourceQueue.Enqueue(source);

            source.clip = clip;
            source.Play();

            return source;
        }

        void SwitchMusic(AudioClip music)
        {
            MusicSourceBackground.clip = music;
            MusicSourceBackground.Play();
            (MusicSourceActive, MusicSourceBackground) = (MusicSourceBackground, MusicSourceActive);
        }
    }
}