using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

/// <summary>
/// Android platformunda bildirimleri y�netmek i�in kullan�lan s�n�f.
/// </summary>
public class AndroidNotifications : MonoBehaviour
{
#if UNITY_ANDROID
    /// <summary>
    /// Kullan�c�dan bildirim izni talep eder.
    /// </summary>
    public static void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    /// <summary>
    /// Android bildirim kanal� olu�turur ve kaydeder.
    /// </summary>
    public static void RegisterNotificationChannel()
    {
        var channel = new AndroidNotificationChannel
        {
            Id = "default_channel",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Full Lives"
        };

        AndroidNotificationCenter.RegisterNotificationChannel(channel);
    }

    /// <summary>
    /// Bildirim g�nderir.
    /// </summary>
    public static void SendNotification(string title, string text, int fireTimeinMinutes)
    {
        if (CheckSendNotifications())
        {
            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
                FireTime = System.DateTime.Now.AddMinutes(fireTimeinMinutes),
                SmallIcon = "icon_0",
                LargeIcon = "icon_1"
            };

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
    }

    /// <summary>
    /// Bildirim g�nderme �zelli�ini kontrol eder.
    /// </summary>
    public static bool CheckSendNotifications()
    {
        return PlayerPrefs.GetInt("SendNotification", 1) == 1;
    }

    /// <summary>
    /// Bildirimleri etkinle�tirir.
    /// </summary>
    public static void EnableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 1);
    }

    /// <summary>
    /// Bildirimleri devre d��� b�rak�r ve mevcut t�m bildirimleri iptal eder.
    /// </summary>
    public static void DisableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 0);
        AndroidNotificationCenter.CancelAllNotifications();
    }

#endif
}
