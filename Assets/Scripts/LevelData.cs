using System;
using UnityEngine;

namespace Match3
{
    /// <summary>
    /// Contains all the data for the Level in which this is : Goals and max number of Moves. This will also  notify the
    /// GameManager that we loaded a level
    /// </summary>
    [DefaultExecutionOrder(12000)]
    public class LevelData : MonoBehaviour
    {
        public static LevelData Instance { get; private set; }
        private LevelList _levelList;

        [Serializable]
        public class GemGoal
        {
            public Gem Gem;
            public int Count;
        }

        public string LevelName = "Level";
        [HideInInspector] public int MaxMove;
        [HideInInspector] public int TargetScore;
        [HideInInspector] public int CurrentScore;
        public int LowMoveTrigger = 10;
        public GemGoal[] Goals;
        
        [Header("Visuals")]
        public float BorderMargin = 0.3f;
        public SpriteRenderer Background;
        
        [Header("Audio")] 
        public AudioClip Music;

        public delegate void GoalChangeDelegate(int gemType,int newAmount);
        public delegate void MoveNotificationDelegate(int moveRemaining);
        public delegate void ScoreChangeDelegate(int score);


        public Action OnAllGoalFinished;
        public Action OnNoMoveLeft;
    
        public GoalChangeDelegate OnGoalChanged;
        public MoveNotificationDelegate OnMoveHappened;
        public ScoreChangeDelegate OnScoreChanged;

        public int RemainingMove { get; private set; }
        public int GoalLeft { get; private set; }


        private int m_StartingWidth;
        private int m_StartingHeight;
        


        private void Awake()
        {
            Instance = this;
            var levelDataProvider = FindObjectOfType<LevelDataProvider>();

            _levelList = levelDataProvider.levelList;

            MaxMove = _levelList.MaxMove[LevelList.GetSceneIndex()];
            TargetScore = _levelList.TargetScore[LevelList.GetSceneIndex()];

            RemainingMove = MaxMove;
            GoalLeft = Goals.Length;
            GameManager.Instance.StartLevel();
        }

        void Start()
        {
            m_StartingWidth = Screen.width;
            m_StartingHeight = Screen.height;

            if (Background != null)
                Background.gameObject.SetActive(false);
        }

        void Update()
        {
            //to detect device orientation change or resolution change, we check if the screen change since since init
            //and recompute camera zoom
            if (Screen.width != m_StartingWidth || Screen.height != m_StartingHeight)
            {
                GameManager.Instance.ComputeCamera();
            }
        }
        
        public bool Matched(Gem gem)
        {
            CurrentScore += gem.GemScore;
            EventBus<int>.Publish(EventType.ScoreChanged, CurrentScore);

            foreach (var goal in Goals)
            {
                if (goal.Gem.GemType == gem.GemType)
                {
                    if (goal.Count == 0)
                        return false;
                
                    
                    
                    goal.Count -= 1;
                    EventBus<int>.Publish(EventType.GoalCountChanged, goal.Count);
                    OnGoalChanged?.Invoke(gem.GemType, goal.Count);

                    if (goal.Count == 0)
                    {
                        GoalLeft -= 1;
                        if (GoalLeft == 0)
                        {
                            // Elinde kalan hamle say�s�na g�re ekstra puan kazand�r�r
                            CurrentScore += RemainingMove * 30;
                            EventBus<int>.Publish(EventType.ScoreChanged, CurrentScore);

                            GameManager.Instance.WinStar();
                            GameManager.Instance.Board.ToggleInput(false);
                            OnAllGoalFinished?.Invoke();
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public void DarkenBackground(bool darken)
        {
            if (Background == null)
                return;

            Background.gameObject.SetActive(darken);
        }

        public void Moved()
        {
            var prev = RemainingMove;
        
        
            RemainingMove = Mathf.Max(0, RemainingMove - 1);
            EventBus<int>.Publish(EventType.Moved, RemainingMove);
            OnMoveHappened?.Invoke(RemainingMove);


            if (RemainingMove <= 0)
            {
                OnNoMoveLeft();
                EventBus.Publish(EventType.MoveCountOver);
            }
        }
    }
}