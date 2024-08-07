using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// TODO: Sahne her açýldýðýnda start çalýþmamasý için instance oluþturulacak
public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    private static readonly int _lifeLoadingTime = 25;

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

    private void Start()
    {
#if UNITY_ANDROID
        AndroidNotifications.RequestAuthorization();
        AndroidNotifications.RegisterNotificationChannel();
#endif
    }


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
    /// Caný 1 azaltýr
    /// </summary>
    public static void DecreaseHealthOne()
    {
        AddLifeCount(-1);
    }

    public static void AddLifeCount(int count)
    {
        int currentLifeCount = GetLifeCount();
        currentLifeCount += count;
        currentLifeCount = Mathf.Clamp(currentLifeCount, 0, 5);
        PlayerPrefs.SetInt("LifeCount", currentLifeCount);
        PlayerPrefs.Save();

        if(GetLifeCount() == 5)
            SetIsLoading(false);

        EventBus.Publish(EventType.LifeCountChanged);
    }



    public static int GetLifeCount()
    {
        return PlayerPrefs.GetInt("LifeCount", 0);
    }

    public static bool GetIsLoading()
    {
        if (PlayerPrefs.GetInt("IsLifeLoading", 0) == 1) return true;
        else return false;

    }

    public static void SetIsLoading(bool boolean)
    {
        if (boolean)
        {
            PlayerPrefs.SetInt("IsLifeLoading", 1);
        }
        else
        {
            PlayerPrefs.SetInt("IsLifeLoading", 0);
        }

        PlayerPrefs.Save();

    }


    public static void SetNextLoadLifeTime(double minutes)
    {
        SetIsLoading(true);
        DateTime now = DateTime.UtcNow; // UTC zamanýný kullan
        DateTime futureTime = now.AddMinutes(minutes);

        long rechargeTimeStamp = new DateTimeOffset(futureTime).ToUnixTimeSeconds();
        PlayerPrefs.SetString("NextLifeRechargeTime", rechargeTimeStamp.ToString());
        PlayerPrefs.Save();
#if UNITY_ANDROID
        AndroidNotificationCenter.CancelAllNotifications();
        AndroidNotifications.SendNotification("A Life is Filled", "A your life is back now!", (int)minutes);
#endif
    }

    /// <summary>
    /// Kalan süreyi dakika cinsinden getir
    /// </summary>
    public static int GetRemainingMinutes()
    {
        string futureTimestampString = PlayerPrefs.GetString("NextLifeRechargeTime");
        if (long.TryParse(futureTimestampString, out long futureTimestamp))
        {
            DateTime futureTime = DateTimeOffset.FromUnixTimeSeconds(futureTimestamp).UtcDateTime;
            TimeSpan timeLeft = futureTime - DateTime.UtcNow; // UTC zamanýný kullan

            return (int)timeLeft.TotalMinutes; // Toplam dakika cinsinden kalan süreyi verir
        }
        return 0;
    }

    /// <summary>
    /// Kalan sürenin saniye cinsinden kýsmýný getir
    /// </summary>
    public static int GetRemainingSeconds()
    {
        string futureTimestampString = PlayerPrefs.GetString("NextLifeRechargeTime");
        if (long.TryParse(futureTimestampString, out long futureTimestamp))
        {
            DateTime futureTime = DateTimeOffset.FromUnixTimeSeconds(futureTimestamp).UtcDateTime;
            TimeSpan timeLeft = futureTime - DateTime.UtcNow; // UTC zamanýný kullan

            return timeLeft.Seconds; // Kalan sürenin sadece saniye kýsmýný verir
        }
        return 0;
    }

    public static void RewardLives(int ID)
    {
        // Reward Life reklamý izlendiyse
        if (ID == 1)
        {
            AddLifeCount(3);
        }

    }

}