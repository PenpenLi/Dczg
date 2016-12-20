using System;
using UnityEngine;
using System.Collections.Specialized;
using System.Collections;
using Procurios.Public;

public class GHCallback : MonoBehaviour
{
	public Action<int, string, NameValueCollection> InitializeCallback{ get; set; }

	public Action<int, string, NameValueCollection> LoginCallback{ get; set; }

	public Action<int, string, NameValueCollection> LogoutCallback{ get; set; }

	public Action<int, string, NameValueCollection> PayCallback{ get; set; }

	public Action<int, string, NameValueCollection> GetTicketCallback{ get; set; }

	public Action<int, string, NameValueCollection> GetAreaConfigCallback{ get; set; }

	public Action<int, string, NameValueCollection> GetProductConfigCallback{ get; set; }

	public Action<int, string, NameValueCollection> DoExtendCallback{ get; set; }
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void InvokeCallback (string json, Action<int, string, NameValueCollection> callback)
	{
		Debug.Log(json);
		
		if (callback == null) {
			return;
		}
		
		Hashtable result = (Hashtable)JSON.JsonDecode (json);
		int code = (int)(double)result ["code"];
		string message = (string)result ["message"];
		NameValueCollection resultData = new NameValueCollection ();
		if (code == 0) {
			Hashtable data = (Hashtable)result ["data"];
			if(data!=null)
			{
				foreach (string key in data.Keys) {
					resultData [key] = (string)data [key];
				}
			}
		}
		callback (code, message, resultData);
	}

	void GHInitializeCallback (string msg)
	{
		InvokeCallback (msg, InitializeCallback);
	}

	void GHLoginCallback (string msg)
	{
		InvokeCallback (msg, LoginCallback);
	}

	void GHLogoutCallback (string msg)
	{
		InvokeCallback (msg, LogoutCallback);
	}

	void GHPayCallback (string msg)
	{
		InvokeCallback (msg, PayCallback);
	}

	void GHGetTicketCallback (string msg)
	{
		InvokeCallback (msg, GetTicketCallback);
	}

	void GHGetAreaConfigCallback (string msg)
	{
		InvokeCallback (msg, GetAreaConfigCallback);
	}

	void GHGetProductConfigCallback (string msg)
	{
		InvokeCallback (msg, GetProductConfigCallback);
	}

	void GHDoExtendCallback (string msg)
	{
		InvokeCallback (msg, DoExtendCallback);
	}
}
