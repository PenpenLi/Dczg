using UnityEngine;
using System.Collections;

public class glucallback : MonoBehaviour {

    public delegate void GLCallback(string msg);

	public GLCallback _onInitUpgradeAsync;
    public GLCallback _onDownloadStart;
    public GLCallback _onDownloadChange;
    public GLCallback _onDownloadFinish;
    public GLCallback _onDownloadCancel;
    public GLCallback _onMD5CheckStart;
    public GLCallback _onMD5CheckFinish;
    public GLCallback _onError;

    public string message = "Callback message";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	void onInitUpgradeResult(string msg)
    {
        message = "onInitUpgradeResult: " + msg;
        if (_onInitUpgradeResult != null)
        {
            _onInitUpgradeResult(msg);
        }
    }
	
	void onCheckNetworkResult(string msg)
    {
        message = "onCheckNetworkResult: " + msg;
        if (_onCheckNetworkResult != null)
        {
            _onCheckNetworkResult(msg);
        }
    }
	*/

    void onDownloadStart(string msg)
    {
        message = "onDownloadStart: " + msg;
        if (_onDownloadStart != null)
        {
            _onDownloadStart(msg);
        }
        Debug.Log(message);
    }

    void onDownloadChange(string msg)
    {
        message = "onDownloadChange: " + msg;
        Debug.Log(message);
        if (_onDownloadChange != null)
        {
            _onDownloadChange(msg);
        }
        
    }

    void onDownloadFinish(string msg)
    {
        message = "onDownloadFinish: " + msg;
        if (_onDownloadFinish != null)
        {
            _onDownloadFinish(msg);
        }
        Debug.Log(message);
    }

    void onDownloadCancel(string msg)
    {
        message = "onDownloadCancel: " + msg;
        if (_onDownloadCancel != null)
        {
            _onDownloadCancel(msg);
        }
        Debug.Log(message);
    }

    void onMD5CheckStart(string msg)
    {
        message = "onMD5CheckStart: " + msg;
        if (_onMD5CheckStart != null)
        {
            _onMD5CheckStart(msg);
        }
        Debug.Log(message);
    }

    void onMD5CheckFinish(string msg)
    {
        message = "onMD5CheckFinish: " + msg;
        if (_onMD5CheckFinish != null)
        {
            _onMD5CheckFinish(msg);
        }
        Debug.Log(message);
    }

    void onError(string msg)
    {
        message = "onError: " + msg;
        if (_onError != null)
        {
            _onError(msg);
        }
        Debug.Log(message);
    }
	
	void onInitUpgradeAsync(string msg)
	{
	    message = "onInitUpgradeAsync: " + msg;
        if (_onInitUpgradeAsync != null)
        {
            _onInitUpgradeAsync(msg);
        }
        Debug.Log(message);
	}
}
