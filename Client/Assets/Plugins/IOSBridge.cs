using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Procurios.Public;

//#if UNITY_IPHONE

public class IOSBridge : ISDKInterface
{
	readonly string messageReceiverName;
	
	[DllImport("__Internal")]
	static extern void GP_U3D_Init(string gameId, string unityObj);
    [DllImport("__Internal")]
	static extern void GP_U3D_Login(string unityObj);
   	[DllImport("__Internal")]
	static extern void GP_U3D_Logout(string unityObj);
   	[DllImport("__Internal")]
	static extern void GP_U3D_Pay(string orderId, string areaId, string productId, string extend, string unityObj);
   	[DllImport("__Internal")]
	static extern void GP_U3D_GetAreaConfiguration(string unityObj);
   	[DllImport("__Internal")]
	static extern void GP_U3D_GetProductConfiguration(string unityObj);
   	[DllImport("__Internal")]
	static extern void GP_U3D_SetGameArea(string areaId);
   	[DllImport("__Internal")]
	static extern void GP_U3D_GetTicket(string gameId, string areaId, string unityObj);
   	[DllImport("__Internal")]
    static extern void GP_U3D_DoExtend(string unityObj, int command, string param);
	
	public IOSBridge (string messageReceiverName)
	{
		this.messageReceiverName = messageReceiverName;
	}

	#region ISDKInterface implementation
	void ISDKInterface.Initialize (string gameId)
	{
		GP_U3D_Init(gameId, messageReceiverName);
	}

	void ISDKInterface.Login ()
	{
		GP_U3D_Login(messageReceiverName);
	}

	void ISDKInterface.LoginArea (string areaId)
	{
		GP_U3D_SetGameArea(areaId);
	}

	void ISDKInterface.Logout ()
	{
		GP_U3D_Logout(messageReceiverName);
	}

	void ISDKInterface.Pay (string orderId, string areaId, string productId, string extend)
	{
		GP_U3D_Pay(orderId, areaId, productId, extend, messageReceiverName);
	}

	void ISDKInterface.GetTicket (string appId, string areaId)
	{
		GP_U3D_GetTicket(appId, areaId, messageReceiverName);
	}

	void ISDKInterface.Pause ()
	{
	}

	void ISDKInterface.Resume ()
	{
	}

	void ISDKInterface.ShowFloatIcon (bool show, int position)
	{
	}

	void ISDKInterface.GetAreaConfig ()
	{
		GP_U3D_GetAreaConfiguration(messageReceiverName);
	}

	void ISDKInterface.GetProductConfig ()
	{
		GP_U3D_GetProductConfiguration(messageReceiverName);
	}

	void ISDKInterface.DoExtend (int command, System.Collections.Specialized.NameValueCollection param)
	{
		GP_U3D_DoExtend(messageReceiverName, command, JSON.NameValueCollection2Json(param));
	}

	void ISDKInterface.Destroy ()
	{
	}
	#endregion
}

//#endif
