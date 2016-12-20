using UnityEngine;
using System.Collections;

public class glupgrade
{
#if UNITY_EDITOR

#else

#if UNITY_ANDROID
    static AndroidJavaClass cls_CompassActivity = new AndroidJavaClass("com.shandagames.gameplus.upgrade.sdgUpgradeU3dWrap");
#endif

#endif
    static glucallback gameplus_callback = null;
    const string obj_name = "gameplus_obj";
	private static bool bUpdating = false;
	
	static void CreateObj(){

        if (gameplus_callback != null)
        {
            return;
        }

		float scale = 0.0f;
		
	    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);

        obj.name = obj_name;

        obj.GetComponent<Renderer>().enabled = false;

        obj.AddComponent<glucallback>();

        obj.transform.localScale = new Vector3(scale, scale, scale);

        gameplus_callback = obj.GetComponent<glucallback>();
	}
	
	public static void gluInit()
	{
		CreateObj();
	}

	/*
	* 鍚屾?鏂瑰紡
	*/
	public static int InitUpgrade()
    {
#if UNITY_EDITOR
        return 0;
#else

#if UNITY_ANDROID
        return cls_CompassActivity.CallStatic<int>("InitUpgrade");
#endif
#if UNITY_IPHONE
        return 0;
#endif
#endif
    }
	
	/*
	* 鍚屾?鏂瑰紡鎵╁睍
	*/
	public static int InitUpgradeEx(int nTimeout)
    {
#if UNITY_EDITOR
        return 0;
#else

#if UNITY_ANDROID
        return cls_CompassActivity.CallStatic<int>("InitUpgradeEx",nTimeout);
#endif
#if UNITY_IPHONE
        return 0;
#endif
#endif
    }
	
	
	/*
	* 寮傛?鍥炶皟鏂瑰紡
	*/
	public static void InitUpgradeAsync()
    {
#if UNITY_EDITOR

#else

#if UNITY_ANDROID
        cls_CompassActivity.CallStatic("InitUpgradeAsync", obj_name);
#endif
#endif
    }
	
	/*
    public static int InitUpgrade()
    {
		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) 
		{

			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {

                return cls_CompassActivity.CallStatic<int>("InitUpgrade", obj_Activity);
			}
		}
	}
	*/

    public static glucallback getCallback()
    {
        return gameplus_callback;
    }

    public static void startUpdate()
    {
		if(!bUpdating)
		{
#if UNITY_EDITOR

#else

#if UNITY_ANDROID
			cls_CompassActivity.CallStatic("startUpdate", obj_name);
#endif
#endif
			bUpdating = true;
		}
	}

    public static void stopUpdate()
    {
#if UNITY_EDITOR

#else

#if UNITY_ANDROID
        cls_CompassActivity.CallStatic("stopUpdate");
#endif
#endif
		bUpdating = false;
    }
	
	public static int checkNetworkStatus()
    {
#if UNITY_EDITOR
        return 0;
#else

#if UNITY_ANDROID
        return cls_CompassActivity.CallStatic<int>("checkNetworkStatus");
#endif
#if UNITY_IPHONE
        return 0;
#endif
#endif
    }

	/*
    public static int checkNetworkStatus()
    {
        using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {

            using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {

                return cls_CompassActivity.CallStatic<int>("checkNetworkStatus", obj_Activity);
            }
        }
    }
	*/
	
}
