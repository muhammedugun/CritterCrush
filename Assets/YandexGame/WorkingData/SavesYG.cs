
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        
        public int[] levelScores = new int[21];  // level number gönder
        public int[] levelStars = new int[21]; 
        public int[] boosterCounts = new int[5]; 
        public int[] boosterPrices = new int[5];
        public int starCount, currentAvatarIndex;
        public int lifeCount = 5;
        public string playerName = "Player";
        public string NextLifeRechargeTime;
        public bool[] openLevels = new bool[21]; // level number gönder
        public bool musicOn = true;
        public bool soundOn = true;
        public bool isPlayingFirstTime = true;
        public bool isLifeLoading;
        
        
        public SavesYG()
        {
            openLevels[1] = true;
        }
    }
}
