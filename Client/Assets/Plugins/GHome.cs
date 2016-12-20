using System;
using UnityEngine;
using System.Collections;
using System.Collections.Specialized;

public class GHome
{
	public const int EXTEND_COMMAND_DUOWAN_OPEN_USER_CENTER = 1001;
	public const int EXTEND_COMMAND_DUOWAN_OPEN_BBS = 1002;
	public const int EXTEND_COMMAND_UC_SUBMIT_EXTEND_DATA = 1003;
	public const int EXTEND_COMMAND_91_PAUSE_PAGE = 1004;
	public const int EXTEND_COMMAND_91_EXIT_PAGE = 1005;
	
	public static class FloatIconPosition{
		public const int LeftTop = 0;
		public const int LeftMiddle = 1;
		public const int LeftBottom = 2;
		public const int RightTop = 3;
		public const int RightMiddle = 4;
		public const int RightBottom = 5;
		public const int MiddleTop = 6;
		public const int MiddleBottom = 7;
	}
	
	ISDKInterface _SDK = null;
	GHCallback _GHCallback = null;
	static GHome _Instance = null;
	
	public static GHome GetInstance ()
	{
		if (_Instance == null) {
			_Instance = new GHome ();
		}
		return _Instance;
	}
	
	GHome ()
	{
	}
	
	void CreateObj ()
	{
		if (_GHCallback != null) {
			return;
		}
		
		float scale = 0.0f;
		GameObject obj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		obj.name = Constants.MESSAGE_RECEIVER_NAME;
		obj.GetComponent<Renderer>().enabled = false;
		obj.AddComponent <GHCallback>();
		obj.transform.localScale = new Vector3 (scale, scale, scale);
		
		_GHCallback = obj.GetComponent<GHCallback> ();
	}
	
	public bool CheckInitialized (Action<int, string, NameValueCollection> callback)
	{
		if (_SDK == null) {
			Debug.Log (Constants.MESSAGE_SDK_NEED_INITIALIZE);
			if (callback != null) {
				callback (Constants.ERROR_CODE_SDK_NEED_INITIALIZE, Constants.MESSAGE_SDK_NEED_INITIALIZE, new NameValueCollection ());
			}
			return false;
		}
		return true;
	}
	
	public bool CheckUninitialized (Action<int, string, NameValueCollection> callback)
	{
		if (_SDK != null) {
			Debug.Log (Constants.MESSAGE_SDK_ALREADY_INITIALIIZED);
			if (callback != null) {
				callback (Constants.ERROR_CODE_SDK_ALREADY_INITIALIZED, Constants.MESSAGE_SDK_ALREADY_INITIALIIZED, new NameValueCollection ());
			}
			return false;
		}
		return true;
	}

	public void Initialize (string gameId, Action<int, string, NameValueCollection> callback)
	{
		if (_SDK != null) {
			return;
		}
		
		CreateObj ();
		
		
#if UNITY_IPHONE
			_SDK = new IOSBridge (Constants.MESSAGE_RECEIVER_NAME);
#endif

#if UNITY_ANDROID
			_SDK = new AndroidBridge (Constants.MESSAGE_RECEIVER_NAME);
#endif
		
		_GHCallback.InitializeCallback = callback;
		_SDK.Initialize (gameId);
	}
	
	public void Destroy ()
	{
		if (_SDK == null) {
			return;
		}
		
		_SDK.Destroy ();
		_SDK = null;
	}

	public void Login (Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.LoginCallback = callback;
		_SDK.Login ();
	}

	public void LoginArea (string areaId)
	{
		if (!CheckInitialized (null)) {
			return;
		}
		_SDK.LoginArea (areaId);
	}

	public void Logout (Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.LogoutCallback = callback;
		_SDK.Logout ();
	}

	public void Pay (string orderId, string areaId, string productId, string extend, Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.PayCallback = callback;
		_SDK.Pay (orderId, areaId, productId, extend);
	}

	public void GetTicket (string appId, string areaId, Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.GetTicketCallback = callback;
		_SDK.GetTicket (appId, areaId);
	}

	public void Pause ()
	{
		if (!CheckInitialized (null)) {
			return;
		}
		_SDK.Pause ();
	}

	public void Resume ()
	{
		if (!CheckInitialized (null)) {
			return;
		}
		_SDK.Resume ();
	}

	public void ShowFloatIcon (bool show, int position)
	{
		if (!CheckInitialized (null)) {
			return;
		}
		_SDK.ShowFloatIcon (show, position);
	}

	public void GetAreaConfig (Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.GetAreaConfigCallback = callback;
		_SDK.GetAreaConfig ();
	}

	public void GetProductConfig (Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.GetProductConfigCallback = callback;
		_SDK.GetProductConfig ();
	}
	
	public void DoExtend(int command, NameValueCollection param, Action<int, string, NameValueCollection> callback)
	{
		if (!CheckInitialized (callback)) {
			return;
		}
		_GHCallback.DoExtendCallback = callback;
		_SDK.DoExtend (command, param);
	}
}
