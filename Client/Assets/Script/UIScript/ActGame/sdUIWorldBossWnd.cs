using UnityEngine;
using System.Collections;
using System;

public class sdUIWorldBossWnd : MonoBehaviour
{
	public GameObject m_preWnd = null;

	public GameObject hpbarPanel = null;
	public GameObject lbName = null;
	public GameObject lbHp = null;
	public GameObject hpBar = null;

	public GameObject propPanel = null;
	public GameObject lbV1 = null;
	public GameObject lbV2 = null;
	public GameObject lbV3 = null;

	public GameObject panRank = null;
	public GameObject lbMyRank = null;
	public GameObject lbMyName = null;
	public GameObject lbMyDamage = null;

	public GameObject panDesc = null;
	
	public GameObject lbJjKs = null;
	public GameObject lbJjKsTime = null;

	public GameObject lbRs = null;
	public GameObject lbRsV = null;
	public GameObject lbTimeV = null;

	public GameObject lbYjs = null;
	public GameObject lbNextTime = null;

	public GameObject btnBegin = null;
	
	public bool m_bJumped	= false;
	public bool m_bWorldBossAck = false;

	public Hashtable lbItemList = new Hashtable();
	public GameObject lbItem = null;

	static GameObject m_BossModel;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}

	float fTime = 0.0f;
	void Update () 
	{
		//发送更新数据请求..
		if (gameObject.activeSelf)
		{
			fTime += Time.deltaTime;
			if (fTime>=5.0f)
			{
				sdActGameMsg.Send_CS_WB_INFO_REFRESH_REQ();
				fTime = 0.0f;
			}
		}

		//进入世界Boss场景..
		if(!m_bJumped && m_bWorldBossAck)
		{
			m_bJumped = true;
			m_bWorldBossAck = false;

			int iLevelID = sdActGameMgr.Instance.GetWorldBossLevelID();
			if (iLevelID>0)
			{
				string bundlePath = ""; 
				string levelName = "";
				int index;
				for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
				{
					if( sdLevelInfo.levelInfos[i].levelID == iLevelID )
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
		}
	}
	
	void OnClick()
    {

	}
	
	public void ActiveWorldBossWnd(GameObject PreWnd)
	{
		m_preWnd = PreWnd;
		ShowRightUIPanel(true);
		OnActivePnlSetRadioButton(true);
		m_bJumped = false;
		m_bWorldBossAck = false;
		fTime = 0.0f;

		RefreshWorldBossUI();
		sdActGameMsg.Send_CS_WB_INFO_REFRESH_REQ();
	}

	public void OnActivePnlSetRadioButton(bool bDesc)
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (btn.gameObject.name=="TabRank" && bDesc==false)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
			else if (btn.gameObject.name=="TabDesc" && bDesc==true)
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
	}
	
	public void ShowRightUIPanel(bool bDesc)
	{
		if (bDesc==true)
		{
			if (panRank) panRank.SetActive(false);
			if (panDesc) panDesc.SetActive(true);
		}
		else
		{
			if (panRank) panRank.SetActive(true);
			if (panDesc) panDesc.SetActive(false);
		}
	}

	public void RefreshWorldBossUI()
	{
		string strBossName = sdActGameMgr.Instance.GetWorldBossPramStr("BossName");
		if (lbName)
			lbName.GetComponent<UILabel>().text = strBossName;

		if (lbHp)
			lbHp.GetComponent<UILabel>().text = "x10";

		int iStatus = sdActGameMgr.Instance.m_WorldBossInfo.m_Status;
		if (iStatus<=1)
		{
			if (propPanel) propPanel.SetActive(false);

			if (lbV1)
				lbV1.GetComponent<UILabel>().text = "%0";
			if (lbV2)
				lbV2.GetComponent<UILabel>().text = "%0";
			if (lbV3)
				lbV3.GetComponent<UILabel>().text = "%0";

			if (sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank==0)
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = "";
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = "";
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = "";
			}
			else
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank.ToString();
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Nm);
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Dmg.ToString();
			}

			if (btnBegin)
			{
				btnBegin.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName = "btn_dis";
				btnBegin.transform.FindChild("word").gameObject.GetComponent<UISprite>().spriteName = "jjks";
			}
			//计算开始时间..
			DateTime timeStar = sdConfDataMgr.Instance().ConvertServerTimeToClientTime((double)sdActGameMgr.Instance.m_WorldBossInfo.m_StartTime);
			DateTime timeNow = DateTime.Now;
			long diff = timeStar.Ticks - timeNow.Ticks;
			if (diff<0)
				diff = 0;
			int iAllSeconds = (int)(diff/10000000L);
			int iHours = iAllSeconds/3600;
			int iMinutes = 0;
			int iSeconds = iAllSeconds%3600;
			if (iSeconds>0)
			{
				iMinutes = iSeconds/60;
				iSeconds = iSeconds%60;
			}
			string str1 = "";
			string str2 = "";
			string str3 = "";
			if (iHours<10)
				str1 = string.Format("0{0}:",iHours);
			else
				str1 = string.Format("{0}:",iHours);

			if (iMinutes<10)
				str2 = string.Format("0{0}:",iMinutes);
			else
				str2 = string.Format("{0}:",iMinutes);

			if (iSeconds<10)
				str3 = string.Format("0{0}",iSeconds);
			else
				str3 = string.Format("{0}",iSeconds);
			//显示开始时间..
			if (lbJjKs)
			{
				lbJjKs.SetActive(true);
				lbJjKs.transform.FindChild("lbJjKsTime").gameObject.GetComponent<UILabel>().text = str1+str2+str3;
			}

			if (lbRs) lbRs.SetActive(false);
			if (lbYjs) lbYjs.SetActive(false);
		}
		else if (iStatus==2)
		{
			if (propPanel) propPanel.SetActive(true);

			int iBuff = sdActGameMgr.Instance.m_iWBMyBuff*10;
			string format = System.String.Format("%{0}",iBuff);
			if (lbV1)
				lbV1.GetComponent<UILabel>().text = format;
			if (lbV2)
				lbV2.GetComponent<UILabel>().text = format;
			if (lbV3)
				lbV3.GetComponent<UILabel>().text = format;

			if (sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank==0)
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = "";
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = "";
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = "";
			}
			else
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank.ToString();
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Nm);
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Dmg.ToString();
			}

			if (btnBegin)
			{
				btnBegin.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName = "btn_nml";
				btnBegin.transform.FindChild("word").gameObject.GetComponent<UISprite>().spriteName = "qwtz";
			}
			
			if (lbJjKs) lbJjKs.SetActive(false);
			if (lbRs)
			{
				lbRs.SetActive(true);

				if (sdActGameMgr.Instance.m_WorldBossInfo.m_ReliveTime>0)
				{
					lbRs.transform.FindChild("lbTime1").gameObject.GetComponent<UILabel>().text = "你已经死亡";
					int iSeconds = (int)(sdActGameMgr.Instance.m_WorldBossInfo.m_ReliveTime/1000);
					lbRs.transform.FindChild("lbTimeV").gameObject.GetComponent<UILabel>().text = iSeconds.ToString()+"秒";
					lbRs.transform.FindChild("lbTime2").gameObject.GetComponent<UILabel>().text = "后可免费复活";
				}
				else
				{
					lbRs.transform.FindChild("lbTime1").gameObject.GetComponent<UILabel>().text = "本活动将于";
					//计算结束时间..
					DateTime timeStar = sdConfDataMgr.Instance().ConvertServerTimeToClientTime((double)sdActGameMgr.Instance.m_WorldBossInfo.m_EndTime);
					DateTime timeNow = DateTime.Now;
					long diff = timeStar.Ticks - timeNow.Ticks;
					if (diff<0)
						diff = 0;
					int iAllSeconds = (int)(diff/10000000L);
					int iHours = iAllSeconds/3600;
					int iMinutes = 0;
					int iSeconds = iAllSeconds%3600;
					if (iSeconds>0)
					{
						iMinutes = iSeconds/60;
						iSeconds = iSeconds%60;
					}
					string str1 = "";
					string str2 = "";
					string str3 = "";
					if (iHours<10)
						str1 = string.Format("0{0}:",iHours);
					else
						str1 = string.Format("{0}:",iHours);
					
					if (iMinutes<10)
						str2 = string.Format("0{0}:",iMinutes);
					else
						str2 = string.Format("{0}:",iMinutes);
					
					if (iSeconds<10)
						str3 = string.Format("0{0}",iSeconds);
					else
						str3 = string.Format("{0}",iSeconds);
					//显示结束时间..
					lbRs.transform.FindChild("lbTimeV").gameObject.GetComponent<UILabel>().text = str1+str2+str3;
					lbRs.transform.FindChild("lbTime2").gameObject.GetComponent<UILabel>().text = "后结束!";
				}

				lbRs.transform.FindChild("lbRsV").gameObject.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_TotalNum.ToString();
			}
			if (lbYjs) lbYjs.SetActive(false);
		}
		else if (iStatus>=3)
		{
			if (propPanel) propPanel.SetActive(false);
			
			if (lbV1)
				lbV1.GetComponent<UILabel>().text = "%0";
			if (lbV2)
				lbV2.GetComponent<UILabel>().text = "%0";
			if (lbV3)
				lbV3.GetComponent<UILabel>().text = "%0";
			
			if (sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank==0)
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = "";
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = "";
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = "";
			}
			else
			{
				if (lbMyRank)
					lbMyRank.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Rank.ToString();
				if (lbMyName)
					lbMyName.GetComponent<UILabel>().text = System.Text.Encoding.UTF8.GetString(sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Nm);
				if (lbMyDamage)
					lbMyDamage.GetComponent<UILabel>().text = sdActGameMgr.Instance.m_WorldBossInfo.m_AtkInfo.m_Dmg.ToString();
			}

			if (btnBegin)
			{
				btnBegin.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName = "btn_dis";
				btnBegin.transform.FindChild("word").gameObject.GetComponent<UISprite>().spriteName = "jjks";
			}
			
			if (lbJjKs) lbJjKs.SetActive(false);
			if (lbRs) lbRs.SetActive(false);
			if (lbYjs) lbYjs.SetActive(true);
			//计算下一场的时间..
			string strTimeStar = sdConfDataMgr.Instance().ConvertServerTimeToStrClientTime((double)sdActGameMgr.Instance.m_WorldBossInfo.m_NextTime);
			//显示下一场的时间..
			lbYjs.transform.FindChild("lbNextTime").gameObject.GetComponent<UILabel>().text = strTimeStar;
		}
	}

	public void RefreshLBItemListPage()
	{
		HeaderProto.SWorldBossInfo worldBossInfo = sdActGameMgr.Instance.m_WorldBossInfo;
		
		int num = worldBossInfo.m_Atkcount;	
		int count = lbItemList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(lbItem) as GameObject;
				tempItem.transform.parent = lbItem.transform.parent;
				tempItem.transform.localPosition = lbItem.transform.localPosition;
				tempItem.transform.localScale = lbItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (46*count);
				tempItem.transform.localPosition = pos;
				lbItemList.Add(lbItemList.Count, tempItem.GetComponent<sdUIWorldBossRecordIcon>());
				++count;
			}
		}	

		IDictionaryEnumerator iter = lbItemList.GetEnumerator();
		for (int i=0;i<worldBossInfo.m_Atkcount;i++)
		{
			if (iter.MoveNext())
			{
				int iRank = worldBossInfo.m_Atklist[i].m_Rank;
				string strName = System.Text.Encoding.UTF8.GetString(worldBossInfo.m_Atklist[i].m_Nm);
				int iDamage = worldBossInfo.m_Atklist[i].m_Dmg;
				sdUIWorldBossRecordIcon icon = iter.Value as sdUIWorldBossRecordIcon;
				if (worldBossInfo.m_Atklist[i].m_Id>0)
					icon.SetIdAndReflashUI(iRank, strName, iDamage);
				else
					icon.SetIdAndReflashUI(0,"",0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIWorldBossRecordIcon icon = iter.Value as sdUIWorldBossRecordIcon;
			icon.SetIdAndReflashUI(0,"",0);
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
		m_BossModel.SetActive(true);
	}

	void OnDrag(Vector2 delta)
	{
		if( m_BossModel == null ) return;
			m_BossModel.transform.Rotate(0,-delta.x/2.0f,0);
	}

	public void setbossmodelVisible(bool bSet)
	{
		if (m_BossModel!=null)
			m_BossModel.SetActive(bSet);
	}
}