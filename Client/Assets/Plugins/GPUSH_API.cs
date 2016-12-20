using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

public class GPUSH_API {
    [DllImport("__Internal")]
    static extern void GPUSH_U3D_Init(string appid, string appkey);
    [DllImport("__Internal")]
    static extern void GPUSH_U3D_SetUserInfo(string area,string username);
    [DllImport("__Internal")]
    static extern void GPUSH_U3D_Vibrate(long millisecond);
    [DllImport("__Internal")]
    static extern void GPUSH_U3D_NewNotification(int id,string title,string content,int day,int hour,int minute,int second);
    [DllImport("__Internal")]
    static extern void GPUSH_U3D_NewNotificationRepeat(int id, string title, string content, int day, int hour, int minute, int second);
    [DllImport("__Internal")]
	static extern void GPUSH_U3D_ClearNotification(int id);
    [DllImport("__Internal")]
	static extern void GPUSH_U3D_ClearAllNotification(int userdata);
    [DllImport("__Internal")]
	static extern void GPUSH_U3D_StartNotification(int userdata);

	// Use this for initialization
    public  static void Init(string appid, string appkey)
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_IPHONE
            GPUSH_U3D_Init(appid,appkey);
#endif
        }
    }
	
	// Update is called once per frame
    public static void SetUserInfo(int area, string username)
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActicity.Call("SetUserInfo", new object[] { area, username });
#endif
#if UNITY_IPHONE
            GPUSH_U3D_SetUserInfo(area.ToString(),username);
#endif
        }
    }
    public static void ClearNotification()
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_IPHONE
            GPUSH_U3D_ClearAllNotification(0);
#endif
        }
    }
    public static void NewNotification(int id, string title, string content, int day, int hour, int minute, int second)
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActicity.Call("NewNotification", new object[] { id, title, content, day, hour, minute, second });
#endif
#if UNITY_IPHONE
            IOS_NotificationMessage(content, day, hour, minute, second);

#endif
        }
    }
    public static void NewNotificationRepeat(int id, string title, string content, int day, int hour, int minute, int second)
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            //android 不能设置重复 只能设置一定次数..
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            for (int i = 0; i < 10; i++)
            {   
                currentActicity.Call("NewNotificationRepeat", new object[] { id, title, content, i*day, hour, minute, second });
            }
#endif
#if UNITY_IPHONE
            //IOS 可以直接设置重复..
            if (day == 7)
            {
                IOS_NotificationMessageRepeat(content, hour, minute, second, CalendarUnit.Week);
            }
            else if (day == 30)
            {
                IOS_NotificationMessageRepeat(content, hour, minute, second, CalendarUnit.Month);
            }
            else if (day == 365)
            {
                IOS_NotificationMessageRepeat(content, hour, minute, second, CalendarUnit.Year);
            }
            else
            {
                IOS_NotificationMessageRepeat(content, hour, minute, second, CalendarUnit.Day);
            }
#endif
        }
    }
    public static void StartNotification()
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActicity.Call("StartNotification", new object[] { 0 });
#endif
#if UNITY_IPHONE
            //IOS_NotificationMessage("(测试延时通知)(1 minute)", 0, 0, 1, 0);
            //IOS_NotificationMessage("(测试延时通知)(3 minute)", 0, 0, 3, 0);
            //IOS_NotificationMessage("(测试延时通知)(5 minute)", 0, 0, 5, 0);
            //GPUSH_U3D_NewNotification(3, title, content, 0, 0, 7, 0);
#endif
        }
    }
    public static void Restart()
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActicity.Call("Restart", new object[] { 0 });
#endif
#if UNITY_IPHONE
       
#endif 
        }
    }
    public static void Vibrate(long milliseconds)
    {
        if (Application.isEditor)
        {

        }
        else
        {
#if UNITY_ANDROID
            AndroidJavaClass dsActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActicity = dsActivity.GetStatic<AndroidJavaObject>("currentActivity");
            currentActicity.Call("Vibrate", new object[] { milliseconds });
#endif
#if UNITY_IPHONE
            GPUSH_U3D_Vibrate(100);
#endif
        }
    }
#if UNITY_IPHONE
    //本地推送
    static void IOS_NotificationMessage(string message, int day,int hour, int minute,int second)
    {

        System.DateTime time = System.DateTime.Now;
        time    =   time.AddDays(day).AddHours(hour).AddMinutes(minute).AddSeconds(second);
        IOS_NotificationMessage(message, time, false, CalendarUnit.Day);
    }

    static void IOS_NotificationMessageRepeat(string message, int hour, int minute, int second,CalendarUnit repeat)
    {

        System.DateTime now = System.DateTime.Now;
        System.DateTime time = new System.DateTime(now.Year, now.Month, now.Day, hour, minute, second);
        IOS_NotificationMessage(message, time, true, repeat);
    }
    //本地推送 你可以传入一个固定的推送时间
    static void IOS_NotificationMessage(string message, System.DateTime newDate, bool isRepeatDay,CalendarUnit repeat)
    {
        //推送时间需要大于当前时间
        //if (newDate > System.DateTime.Now)
        {
            LocalNotification localNotification = new LocalNotification();
            localNotification.fireDate = newDate;
            localNotification.alertBody = message;
            localNotification.applicationIconBadgeNumber = 1;
            localNotification.hasAction = true;
            if (isRepeatDay)
            {
                //是否每天定期循环
                localNotification.repeatCalendar = CalendarIdentifier.ChineseCalendar;
                localNotification.repeatInterval = repeat;
            }
            localNotification.soundName = LocalNotification.defaultSoundName;
           
            NotificationServices.ScheduleLocalNotification(localNotification);
            Debug.Log("Add IOS LocalNotification = " + message + " Time=" + newDate);
        }
    }
    //清空所有本地消息
    static void IOS_CleanNotification()
    {
        LocalNotification l = new LocalNotification();
        l.applicationIconBadgeNumber = -1;
        NotificationServices.PresentLocalNotificationNow(l);
        NotificationServices.CancelAllLocalNotifications();
        NotificationServices.ClearLocalNotifications();
    }

#endif
}
