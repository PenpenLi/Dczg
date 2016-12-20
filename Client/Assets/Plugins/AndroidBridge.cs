using System;
using UnityEngine;
using Procurios.Public;

#if UNITY_ANDROID

public class AndroidBridge : ISDKInterface
{
	readonly string messageReceiverName;
	
	AndroidJavaClass sdkClass = new AndroidJavaClass ("com.snda.ghome.sdk.Unity3d");
	AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	
	public AndroidBridge (string messageReceiverName)
	{
		this.messageReceiverName = messageReceiverName;
	}

	#region ISDKInterface implementation
	void ISDKInterface.Initialize (string gameId)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("initialize", currentActivity, messageReceiverName, gameId);
		}
	}

	void ISDKInterface.Login ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("login", currentActivity, messageReceiverName);
		}
	}

	void ISDKInterface.LoginArea (string areaId)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("loginArea", currentActivity, areaId);
		}
	}

	void ISDKInterface.Logout ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("logout", currentActivity, messageReceiverName);
		}
	}

	void ISDKInterface.Pay (string orderId, string areaId, string productId, string extend)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("pay", currentActivity, messageReceiverName, orderId, areaId, productId, extend);
		}
	}

	void ISDKInterface.GetTicket (string appId, string areaId)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("getTicket", currentActivity, messageReceiverName, appId, areaId);
		}
	}

	void ISDKInterface.Pause ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("pause", currentActivity);
		}
	}

	void ISDKInterface.Resume ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("resume", currentActivity);
		}
	}

	void ISDKInterface.ShowFloatIcon (bool show, int position)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("showFloatIcon", currentActivity, show, position);
		}
	}

	void ISDKInterface.GetAreaConfig ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("getAreaConfig", currentActivity, messageReceiverName);
		}
	}

	void ISDKInterface.GetProductConfig ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("getProductConfig", currentActivity, messageReceiverName);
		}
	}

	void ISDKInterface.DoExtend (int command, System.Collections.Specialized.NameValueCollection param)
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("doExtend", currentActivity, messageReceiverName, command, JSON.NameValueCollection2Json(param));
		}
	}

	void ISDKInterface.Destroy ()
	{
		using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity")) {
			sdkClass.CallStatic ("destroy", currentActivity);
		}
	}
	#endregion
}

#endif
