using System;
using Match3;
using YG;
using UnityEngine;

/// <summary>
/// Oyundaki ya�am say�s�n� y�neten s�n�f.
/// </summary>
public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    private static readonly int _lifeLoadingTime = 25;

    /// <summary>
    /// Singleton desenini kullanarak tek bir LifeManager �rne�ini y�netir.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += AddLivesForReward;
    }
    
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= AddLivesForReward;
    }

    /// <summary>
    /// Hayat y�kleme durumunu kontrol eder ve gerekli i�lemleri yapar.
    /// </summary>
    public static void LifeLoadControl()
    {
        if (GetLifeCount() < 5)
        {
            if (GetIsLoading())
            {
                int remainingMinutes = GetRemainingMinutes();
                int remainingSeconds = GetRemainingSeconds();

                if (remainingMinutes <= 0 && remainingSeconds <= 0)
                {
                    int addedLifeCount = Math.Abs(((remainingMinutes / _lifeLoadingTime) - 1));
                    AddLifeCount(addedLifeCount);

                    if (GetLifeCount() < 5)
                    {
                        int newNextLoadLifeTime = _lifeLoadingTime - (Math.Abs(remainingMinutes) % 25);
                        SetNextLoadLifeTime(newNextLoadLifeTime);
                    }
                    else
                    {
                        SetIsLoading(false);
                    }
                }
            }
            else
            {
                SetNextLoadLifeTime(_lifeLoadingTime);
            }
        }
    }

    /// <summary>
    /// Hayat say�s�n� 1 azalt�r.
    /// </summary>
    public static void DecreaseHealthOne()
    {
        AddLifeCount(-1);
    }

    private void AddLivesForReward(int adID)
    {
        if(adID == (int)AdManager.RewardID.Live)
            AddLifeCount(+3);
    }

    /// <summary>
    /// Hayat say�s�n� belirtilen miktarda art�r�r.
    /// </summary>
    public static void AddLifeCount(int count)
    {
        int currentLifeCount = GetLifeCount();
        currentLifeCount += count;
        currentLifeCount = Mathf.Clamp(currentLifeCount, 0, 5);
        
        YandexGame.savesData.lifeCount = currentLifeCount;
        YandexGame.SaveProgress();

        if (GetLifeCount() == 5)
            SetIsLoading(false);

        EventBus.Publish(EventType.LifeCountChanged);
    }

    /// <summary>
    /// �u anki hayat say�s�n� getirir.
    /// </summary>
    public static int GetLifeCount()
    {
        return YandexGame.savesData.lifeCount;
    }

    /// <summary>
    /// Hayat�n y�klenip y�klenmedi�ini kontrol eder.
    /// </summary>
    public static bool GetIsLoading()
    {
        if (YandexGame.savesData.isLifeLoading) return true;
        else return false;
    }

    /// <summary>
    /// Hayat�n y�klenme durumunu ayarlar.
    /// </summary>
    public static void SetIsLoading(bool boolean)
    {
        if (boolean)
            YandexGame.savesData.isLifeLoading = true;
        else
            YandexGame.savesData.isLifeLoading = false;
        
        YandexGame.SaveProgress();
    }

    /// <summary>
    /// Sonraki y�kleme zaman�n� ayarlar ve bildirim g�nderir.
    /// </summary>
    public static void SetNextLoadLifeTime(double minutes)
    {
        SetIsLoading(true);
        DateTime now = DateTime.UtcNow; // UTC zaman�n� kullan
        DateTime futureTime = now.AddMinutes(minutes);

        long rechargeTimeStamp = new DateTimeOffset(futureTime).ToUnixTimeSeconds();
        YandexGame.savesData.NextLifeRechargeTime = rechargeTimeStamp.ToString();
        YandexGame.SaveProgress();
    }

    /// <summary>
    /// Kalan s�reyi dakika cinsinden getirir.
    /// </summary>
    public static int GetRemainingMinutes()
    {
        string futureTimestampString = YandexGame.savesData.NextLifeRechargeTime;
           
        if (long.TryParse(futureTimestampString, out long futureTimestamp))
        {
            DateTime futureTime = DateTimeOffset.FromUnixTimeSeconds(futureTimestamp).UtcDateTime;
            TimeSpan timeLeft = futureTime - DateTime.UtcNow; // UTC zaman�n� kullan

            return (int)timeLeft.TotalMinutes; // Toplam dakika cinsinden kalan s�reyi verir
        }
        return 0;
    }

    /// <summary>
    /// Kalan s�renin saniye cinsinden k�sm�n� getirir.
    /// </summary>
    public static int GetRemainingSeconds()
    {
        string futureTimestampString = YandexGame.savesData.NextLifeRechargeTime;
          
        if (long.TryParse(futureTimestampString, out long futureTimestamp))
        {
            DateTime futureTime = DateTimeOffset.FromUnixTimeSeconds(futureTimestamp).UtcDateTime;
            TimeSpan timeLeft = futureTime - DateTime.UtcNow; // UTC zaman�n� kullan

            return timeLeft.Seconds; // Kalan s�renin sadece saniye k�sm�n� verir
        }
        return 0;
    }

    /// <summary>
    /// Belirtilen ID'ye g�re �d�l hayat� verir.
    /// </summary>
    public static void RewardLives(int ID)
    {
        // Reward Life reklam� izlendiyse
        if (ID == 1)
        {
            AddLifeCount(3);
        }
    }
}
