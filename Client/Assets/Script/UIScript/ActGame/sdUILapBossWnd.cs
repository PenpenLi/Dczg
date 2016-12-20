using UnityEngine;
using System.Collections;
using System;

public class sdUILapBossWnd : MonoBehaviour
{
	static private GameObject		m_preWnd			= null;
	public GameObject panLB = null;
	public GameObject panRK = null;
	public GameObject panJL = null;
	public GameObject panSM = null;
	public GameObject btnUnlock = null;
	public GameObject btnEnter = null;
	public GameObject lbXzcs = null;

	public GameObject propPanel = null;
	public GameObject lbName = null;
	public GameObject lbHp = null;
	public GameObject hpBar = null;
	public GameObject Background = null;
	public GameObject lbV0 = null;
	public GameObject lbV1 = null;
	public GameObject lbV2 = null;
	public GameObject lbV3 = null;
	public GameObject texBoss = null;

	public GameObject effectPanel = null;

	private int m_iShowPanel = 1;
	public bool m_bJumped	= false;
	public bool m_bTuituAck	= false;
	public bool m_bLapBossAck = false;
	public uint m_LevelID = 0;
	public UInt64 m_SelectUUID = UInt64.MaxValue;

	public Hashtable lbItemList = new Hashtable();
	public GameObject lbItem = null;

	public Hashtable rkItemList = new Hashtable();
	public GameObject rkItem = null;

	public Hashtable jlItemList = new Hashtable();
	public GameObject jlItem = null;

	static GameObject m_BossModel;
	static GameObject m_BossEffect;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
		Init();
	}

	bool bLoadEffectEnd = false;
	bool bBossModelVisible = false;
	float fTime = 0.0f;
	void Update () 
	{
		if(!m_bJumped && m_bTuituAck && m_bLapBossAck)
		{
			m_bJumped = true;
			
			string bundlePath = ""; 
			string levelName = "";
			int index;
			for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
			{
				if( sdLevelInfo.levelInfos[i].levelID == m_LevelID )
				{
					string str = (string)sdLevelInfo.levelInfos[i].levelProp["Scene"];
					bundlePath = str + ".unity.unity3d";
					index = str.LastIndexOf("/");
					levelName = str.Substring(index+1);
					break;
				}
			}
			
			BundleGlobal.Instance.StartLoadBundleLevel(bundlePath,levelName);
		}

		if (bLoadEffectEnd)
		{
			fTime += Time.deltaTime;
			if (fTime>=4.0f)
			{
				if( m_BossEffect )
					Destroy(m_BossEffect);
				m_BossEffect = null;

				if (effectPanel)
					effectPanel.SetActive(false);

				if (m_BossModel && bBossModelVisible==true)
					m_BossModel.SetActive(true);

				bLoadEffectEnd = false;
				fTime = 0.0f;
			}
		}
	}
	
	public void Init()
	{

	}
	
	void OnClick()
    {

	}
	
	public void ActiveLapBossWnd(GameObject PreWnd, int iType)
	{
		m_preWnd = PreWnd;
		SetShowPanelType(iType);
		OnActivePnlSetRadioButton();
		m_bJumped = false;
		m_bTuituAck = false;
		m_bLapBossAck = false;
		m_LevelID = 0;

		m_SelectUUID = UInt64.MaxValue;
		RefreshLBItemListPage();
		RefreshRKItemListPage();
		RefreshRecordItemListPage();

		if (iType==1)
			sdActGameMsg.Send_CS_GET_ABYSS_OPEN_LIST_REQ();
		else if (iType==2)
			sdActGameMsg.Send_CS_GET_ABYSS_TRIGGER_LIST_REQ();
		else if (iType==3)
			sdActGameMsg.Send_CS_GET_ABYSS_OPEN_REC_REQ();

		UpdateLapBossUI();
		OnHideBossUI();

		if (effectPanel)
			effectPanel.SetActive(false);

		bLoadEffectEnd = false;
		fTime = 0.0f;
		//触发深渊时，需要播放特效..
		if (iType==2)
			PlayOpenAnim();
	}

	public void UpdateLapBossUI()
	{
		int iAP = int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["AP"].ToString());
		int iMaxAP = int.Parse(sdGameLevel.instance.mainChar.GetBaseProperty()["MaxAP"].ToString());
		if (lbXzcs)
		{
			string strTemp = string.Format("{0}/{1}", iAP, iMaxAP);
			lbXzcs.GetComponent<UILabel>().text = strTemp;
		}
	}
	
	public void OnActivePnlSetRadioButton()
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="TabPlayList"&&m_iShowPanel==1)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
			else if (btn.gameObject.name=="TabLockList"&&m_iShowPanel==2)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
			else if (btn.gameObject.name=="TabLogList"&&m_iShowPanel==3)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
			else if (btn.gameObject.name=="TabDesc"&&m_iShowPanel==4)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}
	
	public void SetShowPanelType(int iType)
	{
		m_iShowPanel = iType;
		ShowRightUIPanel();
	}
	
	public void ShowRightUIPanel()
	{
		if (m_iShowPanel==1)
		{
			if (panLB) panLB.SetActive(true);
			if (panRK) panRK.SetActive(false);
			if (panJL) panJL.SetActive(false);
			if (panSM) panSM.SetActive(false);

			if (btnEnter) btnEnter.SetActive(false);
			if (btnUnlock) btnUnlock.SetActive(false);
		}
		else if (m_iShowPanel==2)
		{
			if (panLB) panLB.SetActive(false);
			if (panRK) panRK.SetActive(true);
			if (panJL) panJL.SetActive(false);
			if (panSM) panSM.SetActive(false);

			if (btnEnter) btnEnter.SetActive(false);
			if (btnUnlock) btnUnlock.SetActive(false);
		}
		else if (m_iShowPanel==3)
		{
			if (panLB) panLB.SetActive(false);
			if (panRK) panRK.SetActive(false);
			if (panJL) panJL.SetActive(true);
			if (panSM) panSM.SetActive(false);

			if (btnEnter) btnEnter.SetActive(false);
			if (btnUnlock) btnUnlock.SetActive(false);
		}
		else if (m_iShowPanel==4)
		{
			if (panLB) panLB.SetActive(false);
			if (panRK) panRK.SetActive(false);
			if (panJL) panJL.SetActive(false);
			if (panSM) panSM.SetActive(true);

			if (btnEnter) btnEnter.SetActive(false);
			if (btnUnlock) btnUnlock.SetActive(false);
		}
	}

	public void SetAllLBItemUnSelected()
	{
		foreach(DictionaryEntry info in lbItemList)
		{
			sdUILapBossEnterIcon icon = info.Value as sdUILapBossEnterIcon;
			icon.SetSelect(false);
		}

		m_SelectUUID = UInt64.MaxValue;
	}

	public void SetAllRKItemUnSelected()
	{
		foreach(DictionaryEntry info in rkItemList)
		{
			sdUILapBossLockIcon icon = info.Value as sdUILapBossLockIcon;
			icon.SetSelect(false);
		}

		m_SelectUUID = UInt64.MaxValue;
	}

	public void RefreshLBItemListPage()
	{
		Hashtable list = null;
		list = sdActGameMgr.Instance.m_LapBossEnterInfo;
		
		int num = list.Count;	
		int count = lbItemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(lbItem) as GameObject;
				tempItem.GetComponent<sdUILapBossEnterIcon>().index = count;
				tempItem.transform.parent = lbItem.transform.parent;
				tempItem.transform.localPosition = lbItem.transform.localPosition;
				tempItem.transform.localScale = lbItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (152*count);
				tempItem.transform.localPosition = pos;
				tempItem.GetComponent<sdUILapBossEnterIcon>().bSelect = false;
				lbItemList.Add(lbItemList.Count, tempItem.GetComponent<sdUILapBossEnterIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = lbItemList.GetEnumerator();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			if (iter.MoveNext())
			{
				sdUILapBossEnterIcon icon = iter.Value as sdUILapBossEnterIcon;
				icon.SetIdAndReflashUI(UInt64.Parse(key1));
			}
		}
		
		while (iter.MoveNext())
		{
			sdUILapBossEnterIcon icon = iter.Value as sdUILapBossEnterIcon;
			icon.SetIdAndReflashUI(UInt64.MaxValue);
		}

		m_SelectUUID = UInt64.MaxValue;
	}

	public void RefreshRKItemListPage()
	{
		Hashtable list = null;
		list = sdActGameMgr.Instance.m_LapBossLockInfo;
		
		int num = list.Count;	
		int count = rkItemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(rkItem) as GameObject;
				tempItem.GetComponent<sdUILapBossLockIcon>().index = count;
				tempItem.transform.parent = rkItem.transform.parent;
				tempItem.transform.localPosition = rkItem.transform.localPosition;
				tempItem.transform.localScale = rkItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (152*count);
				tempItem.transform.localPosition = pos;
				tempItem.GetComponent<sdUILapBossLockIcon>().bSelect = false;
				rkItemList.Add(rkItemList.Count, tempItem.GetComponent<sdUILapBossLockIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = rkItemList.GetEnumerator();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			if (iter.MoveNext())
			{
				sdUILapBossLockIcon icon = iter.Value as sdUILapBossLockIcon;
				icon.SetIdAndReflashUI(UInt64.Parse(key1));
			}
		}
		
		while (iter.MoveNext())
		{
			sdUILapBossLockIcon icon = iter.Value as sdUILapBossLockIcon;
			icon.SetIdAndReflashUI(UInt64.MaxValue);
		}

		m_SelectUUID = UInt64.MaxValue;
	}

	public void RefreshRecordItemListPage()
	{
		Hashtable list = null;
		list = sdActGameMgr.Instance.m_LapBossRecordInfo;
		
		int num = list.Count;	
		int count = jlItemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(jlItem) as GameObject;
				tempItem.GetComponent<sdUILapBossRecordIcon>().index = count;
				tempItem.transform.parent = jlItem.transform.parent;
				tempItem.transform.localPosition = jlItem.transform.localPosition;
				tempItem.transform.localScale = jlItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (82*count);
				tempItem.transform.localPosition = pos;
				jlItemList.Add(jlItemList.Count, tempItem.GetComponent<sdUILapBossRecordIcon>());
				++count;
			}
		}	
		
		IDictionaryEnumerator iter = jlItemList.GetEnumerator();
		foreach(DictionaryEntry info in list)
		{
			string key1 = info.Key.ToString();
			if (iter.MoveNext())
			{
				sdUILapBossRecordIcon icon = iter.Value as sdUILapBossRecordIcon;
				icon.SetIdAndReflashUI(UInt64.Parse(key1));
			}
		}
		
		while (iter.MoveNext())
		{
			sdUILapBossRecordIcon icon = iter.Value as sdUILapBossRecordIcon;
			icon.SetIdAndReflashUI(UInt64.MaxValue);
		}
	}

	public void OnHideBossUI()
	{
		if (propPanel)
			propPanel.SetActive(false);

		if (texBoss)
			texBoss.SetActive(false);

		DestoryBossModel();
		bBossModelVisible = false;
	}

	public void OnSelectBossSetUI(SAbyssLockInfo slInfo)
	{
		Hashtable info = sdConfDataMgr.Instance().GetLapBossTemplate(slInfo.m_ActTmpId.ToString());

		if (propPanel)
		{
			propPanel.SetActive(true);

			if (info != null)
			{
				Hashtable kMonsterProperty = sdConfDataMgr.Instance().GetTable("MonsterProperty");
				Hashtable kTable = kMonsterProperty[int.Parse(info["MonsterTemplateID"].ToString())] as Hashtable;
				if (kTable!=null)
				{
					string strTemp = kTable["Name"].ToString();
					if (lbName)
						lbName.GetComponent<UILabel>().text = strTemp;

					int iMaxHp = (int)kTable["MaxHP"];
					int iHpNum = (int)kTable["HPBarNum"];
					int iPerHp = iMaxHp/iHpNum;
					int iNowHp = (int)slInfo.m_Blood;
					int iNowHpNum = 0;
					int iLineHp = 0;
					if (iNowHp==iMaxHp)
					{
						iNowHpNum = iHpNum;
						iLineHp = iPerHp;
					}
					else
					{
						if (iNowHp>iPerHp)
						{
							int iNum = iNowHp/iPerHp;
							iLineHp = iNowHp%iPerHp;
							if (iLineHp>0)
								iNowHpNum = iNum+1;
							else
								iNowHpNum = iNum;
						}
						else
						{
							iLineHp = iNowHp;
							iNowHpNum = 1;
						}
					}

					float fPer = (float)iLineHp/(float)iPerHp;
					if (hpBar)
						hpBar.GetComponent<UISlider>().value = fPer;
					if (lbHp)
						lbHp.GetComponent<UILabel>().text = "x"+iNowHpNum.ToString();

					if (iNowHpNum>1)
					{
						if (Background)
							Background.GetComponent<UISprite>().spriteName = "bl5";
					}
					else
					{
						if (Background)
							Background.GetComponent<UISprite>().spriteName = "bl4";
					}

					if (lbV0)
					{
						lbV0.GetComponent<UILabel>().text = info["AbyLevel"].ToString();
					}

					if (lbV1)
					{
						int iTemp = (int)kTable["AtkDmgMax"];
						lbV1.GetComponent<UILabel>().text = iTemp.ToString();
					}

					if (lbV2)
					{
						int iTemp = (int)kTable["Def"];
						lbV2.GetComponent<UILabel>().text = iTemp.ToString();
					}

					if (lbV3)
					{
						lbV3.GetComponent<UILabel>().text = iNowHp.ToString();
					}
				}

				LoadBossModel(info["BossTex"].ToString());
			}
		}

//		if (texBoss!=null && info!=null)
//		{
//			texBoss.SetActive(true);
//			ResLoadParams param = new ResLoadParams();
//			string strTex = info["BossTex"].ToString();
//			if( strTex != "" )
//				sdResourceMgr.Instance.LoadResource("UI/LapBossTex/$LBT_"+strTex+".png",LoadTexCallback,param,typeof(Texture));
//		}


	}

	public void LoadTexCallback(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null )
			return;

		if( texBoss )
		{
			texBoss.GetComponent<UITexture>().mainTexture = (Texture)obj;
			texBoss.GetComponent<UITexture>().color = new Color(1.0f,1.0f,1.0f,1.0f);
		}
	}

	public void SelectEnterIcon(bool bFirst, UInt64 uuID)
	{
		m_SelectUUID = UInt64.MaxValue;
		OnHideBossUI();
		if (btnEnter)
			btnEnter.SetActive(false);

		sdUILapBossEnterIcon icon = null;
		if (bFirst==true)
		{
			foreach(DictionaryEntry item in lbItemList)
			{
				icon = item.Value as sdUILapBossEnterIcon;
				break;
			}
		}
		else
		{
			if (uuID!=UInt64.MaxValue)
			{
				foreach(DictionaryEntry item in lbItemList)
				{
					sdUILapBossEnterIcon iconTemp = item.Value as sdUILapBossEnterIcon;
					if (iconTemp.GetId()==uuID)
					{
						icon = iconTemp;
						break;
					}
				}
			}
		}


		if (icon!=null)
		{
			if (icon.GetId()!=UInt64.MaxValue)
			{
				icon.SetSelect(true);
				SAbyssLockInfo info = sdActGameMgr.Instance.GetEnterInfo(icon.GetId());
				if (info!=null)
				{
					m_SelectUUID = icon.GetId();
					OnSelectBossSetUI(info);
					if (btnEnter)
						btnEnter.SetActive(true);
				}
			}
		}
	}

	public void SelectFirstLockIcon()
	{
		sdUILapBossLockIcon icon = null;
		foreach(DictionaryEntry item in rkItemList)
		{
			icon = item.Value as sdUILapBossLockIcon;
			break;
		}
		
		m_SelectUUID = UInt64.MaxValue;
		OnHideBossUI();
		if (btnUnlock)
			btnUnlock.SetActive(false);
		
		if (icon!=null)
		{
			if (icon.GetId()!=UInt64.MaxValue)
			{
				icon.SetSelect(true);
				SAbyssLockInfo info = sdActGameMgr.Instance.GetLockInfo(icon.GetId());
				if (info!=null)
				{
					m_SelectUUID = icon.GetId();
					OnSelectBossSetUI(info);
					if (btnUnlock)
						btnUnlock.SetActive(true);
				}
			}
		}
	}

	public void LoadBossModel(string strRes)
	{
		DestoryBossModel();
		
		// 载入模型..
		ResLoadParams param = new ResLoadParams();
		param.pos = new Vector3(-80.0f,-230.0f,-200.0f);
		param.rot.Set(0,180.0f,0,0);
		param.scale = new Vector3(160.0f,160.0f,160.0f);
		string strPath = "Prefab/LapBoss/"+strRes+".prefab";
		sdResourceMgr.Instance.LoadResource(strPath, BossLoadInstantiate, param);
	}

	public void DestoryBossModel()
	{
		if( m_BossModel )
			Destroy(m_BossModel);
		m_BossModel = null;
	}

	public void BossLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_BossModel = GameObject.Instantiate(obj) as GameObject;
		m_BossModel.name = "LapBossModel";
		m_BossModel.transform.parent = GameObject.Find("bgComm2").transform;
		m_BossModel.transform.localPosition = param.pos;

		if (bLoadEffectEnd==true)
			m_BossModel.SetActive(false);
		else
			m_BossModel.SetActive(true);

		bBossModelVisible = true;
	}

	void OnDrag(Vector2 delta)
	{
		if( m_BossModel == null ) return;
			m_BossModel.transform.Rotate(0,-delta.x/2.0f,0);
	}

	public void PlayOpenAnim()
	{
		if (m_BossModel)
			m_BossModel.SetActive(false);

		if (effectPanel)
			effectPanel.SetActive(true);

		LoadBossEffect();
	}

	public void LoadBossEffect()
	{
		if( m_BossEffect )
			Destroy(m_BossEffect);
		m_BossEffect = null;
		
		string prefabname = "Effect/MainChar/FX_UI/$Fx_Shenyuan/Fx_Shenyuan.prefab";
		ResLoadParams param = new ResLoadParams();
		param.info = "LabBossEffect";
		sdResourceMgr.Instance.LoadResource(prefabname,OnLoadPrefab,param);
	}
	
	public void OnLoadPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		if (obj == null) return;
		if (effectPanel == null) return;
		if (param.info == "LabBossEffect")
		{
			m_BossEffect = GameObject.Instantiate(obj) as GameObject;
			m_BossEffect.transform.parent = effectPanel.transform;
			m_BossEffect.transform.localPosition = new Vector3(0,0,0);
			m_BossEffect.transform.localScale = new Vector3(100,100,100);
			bLoadEffectEnd = true;
			fTime = 0.0f;
		}
	}

	public void setbossmodelVisible(bool bSet)
	{
		if (m_BossModel!=null)
			m_BossModel.SetActive(bSet);
	}
}