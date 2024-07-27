using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#endif

public class AndroidNotifications : MonoBehaviour
{
#if UNITY_ANDROID

    

    public static void RequestAuthorization()
    {
        if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
        {
            Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
        }
    }

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

    public static void SendNotification(string title, string text, int fireTimeinMinutes)
    {
        if(CheckSendNotifications())
        {
            var notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = text;
            notification.FireTime = System.DateTime.Now.AddMinutes(fireTimeinMinutes);
            notification.SmallIcon = "icon_0";
            notification.LargeIcon = "icon_1";

            AndroidNotificationCenter.SendNotification(notification, "default_channel");
        }
        
    }

    public static bool CheckSendNotifications()
    {
        if(PlayerPrefs.GetInt("SendNotification", 1) == 1)
            return true;
        else
            return false;
    }

    public static void EnableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 1);
    }

    public static void DisableNotifications()
    {
        PlayerPrefs.SetInt("SendNotification", 0);
        AndroidNotificationCenter.CancelAllNotifications();
    }

#endif

}
