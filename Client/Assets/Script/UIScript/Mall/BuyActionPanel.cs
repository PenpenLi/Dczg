using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BuyActionPanel : Singleton<BuyActionPanel>
{
	public GameObject 	m_goWndRoot	= null;
	bool _bWndOpen = false;

	UIButton _closeBtn = null;
	UIButton _cancelBtn = null;
	UIButton _confirmBtn = null;
	UIButton _tipBtn = null;

	GameObject _progressBar = null;

	UILabel _recoveryTime = null;
	UILabel _fullTime = null;
	UILabel _remainTimes = null;
	UILabel _recoveryPoint = null;
	UILabel _currEp = null;
	UILabel _nonCashLabel = null;

	int _fullTimeBegin = 0;
	int _recoveryTimeBegin = 0;

	int _costCash = 0;
 	int _recoveryEP = 0;
	int _maxRecoveryTimes = 0;

	void Start () 
	{

	}

	void Update () 
	{

		if(_fullTime == null)
			return;

		/*
		//服务器时间
		ulong serverTime = sdGlobalDatabase.Instance.ServerBeginTime + 
			sdGlobalDatabase.Instance.TimeFromServerBeginTime;
		
		// 实际上nRemainTime是上次免费购买时间
		int remainTime = 48 * 3600 - (int)(serverTime - nLastTime) / 1000;
		if (remainTime <= 0)
		{
			lblTime.text = "本次购买免费";
		}
		else
		{
			int hour = remainTime / 3600;
			remainTime -= hour * 3600;
			int minute = remainTime / 60;
			int second = remainTime % 60;
			
			string str1 = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
			string str2 = "后免费";
			string str = str1 + str2;
			lblTime.text = str;				
		}
		*/

		int remainTime = _fullTimeBegin - (int)(sdGlobalDatabase.Instance.TimeFromServerBeginTime/1000);
		if(remainTime<=0)
			remainTime = 0;

		int hour = remainTime / 3600;
		remainTime -= hour * 3600;
		int minute = remainTime / 60;
		int second = remainTime % 60;
		_fullTime.text = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
		

	}
	
	void OnDestory()
	{
		
	}
	
	public void Init()
	{
		
	}
	
	void OnCloseBtn(GameObject go)
	{
		ClosePanel();

	}

	public void OpenPanel()
	{
		_bWndOpen = true;
		if( m_goWndRoot != null )
		{
			m_goWndRoot.SetActive(true);
			//WndAni.ShowWndAni(m_goWndRoot,true,"bg_grey");

			sdMainChar mc = sdGameLevel.instance.mainChar;
			if (mc != null)
			{
				int myCurrentEP = int.Parse(mc.GetBaseProperty()["EP"].ToString ());
				int myMaxEP = int.Parse(mc.Property["MaxEP"].ToString());
				_progressBar.GetComponent<UISlider>().value = (float)myCurrentEP/(float)myMaxEP;

				_currEp.text = "体力：" + myCurrentEP.ToString() + "/" + myMaxEP.ToString();
				_recoveryPoint.text = "购买后恢复" + myMaxEP.ToString() + "点体力！";
				_recoveryEP = myMaxEP;

				_fullTimeBegin = (myMaxEP - myCurrentEP) * 5 * 60 + 
					(int)(sdGlobalDatabase.Instance.TimeFromServerBeginTime/1000);
			}

			
			_maxRecoveryTimes = 0;
			foreach(sdMallManager.ActionPointInfo item in sdMallManager.Instance.m_actionPointInfos)
			{
				if(sdMallManager.Instance.m_iCurrentVIPLevel >= item.NeedVIPLevel)
					_maxRecoveryTimes++;
				else
					break;
			}

			_remainTimes.text = "剩余可购买次数：  " + sdMallManager.Instance.m_iEnergyCount.ToString() + "/" 
				+ _maxRecoveryTimes.ToString();
			
			_costCash = sdMallManager.Instance.m_actionPointInfos[(int)sdMallManager.Instance.m_iEnergyCount].CostNonCash;
			_nonCashLabel.text = "x" + _costCash.ToString();

		}
		else
		{
			sdUILoading.ActiveSmallLoadingUI(true);
			ResLoadParams param = new ResLoadParams();
			param.info = "BuyActionPanel";
			sdResourceMgr.Instance.LoadResource("UI/Shop/$BuyActionPanel.prefab", 
		    	                                OnPanelLoaded, 
		        	                            param, 
		            	                        typeof(GameObject));
		}
	}
	
	public void OnPanelLoaded(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info != "BuyActionPanel")
			return;

		m_goWndRoot = GameObject.Instantiate(obj) as GameObject;
		m_goWndRoot.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_goWndRoot.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_goWndRoot.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		m_goWndRoot.SetActive(true);

		_closeBtn =  m_goWndRoot.transform.FindChild("Close").GetComponent<UIButton>();
		UIEventListener.Get(_closeBtn.gameObject).onClick = OnCloseBtn;

		_cancelBtn = m_goWndRoot.transform.FindChild("CancelBtn").GetComponent<UIButton>();
		UIEventListener.Get(_cancelBtn.gameObject).onClick = OnCloseBtn;

		_confirmBtn = m_goWndRoot.transform.FindChild("ConfirmBtn").GetComponent<UIButton>();
		UIEventListener.Get(_confirmBtn.gameObject).onClick = OnConfirmBtn;


		_progressBar = m_goWndRoot.transform.FindChild("Background/ProgressBar").gameObject;

		_recoveryTime = m_goWndRoot.transform.FindChild("Background/RecoveryTime").GetComponent<UILabel>();
		_fullTime = m_goWndRoot.transform.FindChild("Background/FullTime").GetComponent<UILabel>();
		_remainTimes = m_goWndRoot.transform.FindChild("Background/RemainTimes").GetComponent<UILabel>();
		_recoveryPoint = m_goWndRoot.transform.FindChild("Background/RecoveryPoint").GetComponent<UILabel>();
		_currEp = m_goWndRoot.transform.FindChild("Background/ProgressBar/Text").GetComponent<UILabel>();
		_nonCashLabel = m_goWndRoot.transform.FindChild("ConfirmBtn/NonCash/Label").GetComponent<UILabel>();

		sdUILoading.ActiveSmallLoadingUI(false);
		OpenPanel();
		
	}

	void OnConfirmBtn(GameObject go)
	{

		if(sdMallManager.Instance.m_iEnergyCount>=_maxRecoveryTimes)
		{
			sdUICharacter.Instance.ShowOkMsg("您的购买次数已满！", null);
			return;
		}

		uint nCash = (uint)sdGameLevel.instance.mainChar.Property["Cash"] + 
			(uint)sdGameLevel.instance.mainChar.Property["NonCash"];
		if( _costCash >= nCash)
		{
			sdUICharacter.Instance.ShowOkMsg("您的徽章不足！", null);
			return;
		}

		if(sdMallManager.Instance.m_buyActionTips)
		{
			ClosePanel();
			sdMallMsg.Send_CS_SHOP_BUY_ACTION_POINT_REQ ();
		}
		else
		{
			ClosePanel();
			BuyActionTipPanel.Instance.m_costCash = _costCash;
			BuyActionTipPanel.Instance.m_recoveryEP = _recoveryEP;
			BuyActionTipPanel.Instance.OpenPanel();
		}

	}

	public bool IsOpen() { return _bWndOpen; }

	public void ClosePanel()
	{
		if( m_goWndRoot == null )
			return;

		_bWndOpen = false;

		m_goWndRoot.SetActive(false);

		//m_goWndRoot.SetActive(false);
		//WndAni.HideWndAni(m_goWndRoot,true,"bg_grey");
	}
}

