using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

/// <summary>
/// Android platformunda bildirimleri yönetmek için kullanýlan sýnýf.
/// </summary>
public class AndroidNotifications : MonoBehaviour
{
#if UNITY_ANDROID
    /// <summary>
    /// Kullanýcýdan bildirim izni talep eder.
    /// </summary>
    public static void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

    /// <summary>
    /// Android bildirim kanalý oluþturur ve kaydeder.
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
    /// Bildirim gönderir.
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
    /// Bildirim gönderme özelliðini kontrol eder.
    /// </summary>
    public static bool CheckSendNotifications()
    {
        return PlayerPrefs.GetInt("SendNotification", 1) == 1;
    }

    /// <summary>
    /// Bildirimleri etkinleþtirir.
    /// </summary>
    public static void EnableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 1);
    }

    /// <summary>
    /// Bildirimleri devre dýþý býrakýr ve mevcut tüm bildirimleri iptal eder.
    /// </summary>
    public static void DisableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 0);
        AndroidNotificationCenter.CancelAllNotifications();
    }

#endif
}
