using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class petSkillUnit
{
	public int m_iSkillID = 0;	//skill   id..
	public int m_iType = 0;		//0为skill,1为buff..
}

public class sdUIPetPropPnl : MonoBehaviour 
{
	//根据模板ID显示宠物属性..
	public int m_iPetTemplateID = 0;
	public int m_iPetUp = 0;
	public int m_iPetLevel = 1;
	public bool m_bNeedShowGather = false;
	//根据宠物实例ID显示宠物属性..
	public UInt64 m_uuDBID = UInt64.MaxValue;

	public GameObject m_title = null;
	//左侧UI..
	public GameObject m_picLock = null;
	public GameObject m_picType = null;
	public GameObject m_picCz = null;
	public GameObject m_lbName = null;
	public GameObject m_lbStrong = null;
	public GameObject m_lbValue = null;
	public GameObject m_btnLock = null;
	public GameObject m_btnPetShow = null;
	//经验条..
	public GameObject m_lbLevel = null;
	public GameObject m_lbExp = null;
	public GameObject m_expBar = null;
	//右侧框架UI..
	public GameObject m_panRight = null;
	//基础属性..
	public GameObject m_panDesc = null;
	public GameObject m_lbv1 = null;
	public GameObject m_lbv21 = null;
	public GameObject m_lbv22 = null;
	public GameObject m_lbv3 = null;
	public GameObject m_lbv4 = null;
	public GameObject m_lbv5 = null;
	public GameObject m_lbdesc = null;
	public GameObject m_pstar0 = null;
	public GameObject m_pstar1 = null;
	public GameObject m_pstar2 = null;
	public GameObject m_pstar3 = null;
	public GameObject m_pstar4 = null;
	//宠物技能..
	public GameObject m_panSkill = null;
	public GameObject m_lbskillv0 = null;
	public GameObject m_skill0 = null;
	public GameObject m_skill1 = null;
	public GameObject m_skill2 = null;
	public GameObject m_skill3 = null;
	public GameObject m_skill4 = null;
	//宠物组合..
	public GameObject m_panZuhe = null;
	Hashtable petzuheInfoList = new Hashtable();
	public GameObject copyListItem = null;
	//底部按钮面板..
	public GameObject 		m_panDown = null;
	public GameObject		m_btnLevelup		= null;
	public GameObject		m_btnStrong			= null;
	public GameObject		m_btnPetGather = null;

	//其他数据..
	public GameObject		m_preWnd			= null;
	
	static uint m_nPetTemplateID = 0;
	static GameObject m_PetModel;

	void Awake () 
	{

	}
	
	void Start () 
	{
	}
	
	void Update () 
	{	
	}

	void OnClick()
    {
	}

	public void PropUIShow(int iIndex)
	{
		//基础信息..
		if (iIndex==0)
		{
			if (m_panRight) m_panRight.SetActive(true);
			if (m_panDesc) m_panDesc.SetActive(true);
			if (m_panSkill) m_panSkill.SetActive(false);
			if (m_panZuhe) m_panZuhe.SetActive(false);
			if (m_panDown && m_uuDBID!=UInt64.MaxValue)
				m_panDown.SetActive(true);

			sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
			foreach(sdRadioButton btn in list)
			{
				if (btn.gameObject.name=="RbPropDesc")
				{
					btn.Active(true);
					sdUICharacter.Instance.ActiceRadioBtn(btn);	
				}
			}
			//升级，强化按钮..
			if (m_btnLevelup) m_btnLevelup.SetActive(true);
			if (m_btnStrong) m_btnStrong.SetActive(true);
		}
		//升级显示..
		else if (iIndex==1)
		{
			if (m_panRight) m_panRight.SetActive(false);
			if (m_panDesc) m_panDesc.SetActive(false);
			if (m_panSkill) m_panSkill.SetActive(false);
			if (m_panZuhe) m_panZuhe.SetActive(false);
			if (m_panDown && m_uuDBID!=UInt64.MaxValue)
				m_panDown.SetActive(true);
			//升级，强化按钮..
			if (m_btnLevelup) m_btnLevelup.SetActive(false);
			if (m_btnStrong) m_btnStrong.SetActive(false);
		}
		//宠物技能..
		else if (iIndex==2)
		{
			if (m_panRight) m_panRight.SetActive(true);
			if (m_panDesc) m_panDesc.SetActive(false);
			if (m_panSkill) m_panSkill.SetActive(true);
			if (m_panZuhe) m_panZuhe.SetActive(false);
			if (m_panDown && m_uuDBID!=UInt64.MaxValue)
				m_panDown.SetActive(true);
			
			sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
			foreach(sdRadioButton btn in list)
			{
				if (btn.gameObject.name=="RbPropJn")
				{
					btn.Active(true);
					sdUICharacter.Instance.ActiceRadioBtn(btn);	
				}
			}
			//升级，强化按钮..
			if (m_btnLevelup) m_btnLevelup.SetActive(true);
			if (m_btnStrong) m_btnStrong.SetActive(true);
		}
		//宠物组合..
		else if (iIndex==3)
		{
			if (m_panRight) m_panRight.SetActive(true);
			if (m_panDesc) m_panDesc.SetActive(false);
			if (m_panSkill) m_panSkill.SetActive(false);
			if (m_panZuhe) m_panZuhe.SetActive(true);
			if (m_panDown && m_uuDBID!=UInt64.MaxValue)
				m_panDown.SetActive(true);
			
			sdRadioButton[] list = gameObject.GetComponentsInChildren<sdRadioButton>();
			foreach(sdRadioButton btn in list)
			{
				if (btn.gameObject.name=="RbPropZh")
				{
					btn.Active(true);
					sdUICharacter.Instance.ActiceRadioBtn(btn);	
				}
			}
			//升级，强化按钮..
			if (m_btnLevelup) m_btnLevelup.SetActive(true);
			if (m_btnStrong) m_btnStrong.SetActive(true);
		}

		//合成按钮先屏蔽..
		if (m_btnPetGather) m_btnPetGather.SetActive(false);
		if (m_panDown)
		{
			m_panDown.transform.FindChild("lbCur").gameObject.SetActive(false);
			m_panDown.transform.FindChild("lbCurV").gameObject.SetActive(false);
		}
		//合成按钮是否显示的逻辑..
		if (m_iPetTemplateID>0)
		{
			//升级，强化按钮..
			if (m_btnLevelup) m_btnLevelup.SetActive(false);
			if (m_btnStrong) m_btnStrong.SetActive(false);

			ReflashGatherUI();
		}
	}

	public void ReflashGatherUI()
	{
		int iCurNum = sdNewPetMgr.Instance.getPetGatherCurNumByPetId(m_iPetTemplateID);
		int iMaxNum = sdNewPetMgr.Instance.getPetGatherMaxNumByPetId(m_iPetTemplateID);

		//需要显示合成信息..
		if (m_bNeedShowGather)
		{
			//配置了碎片..
			if (iMaxNum>0)
			{
				//设置数量..
				if (m_panDown)
				{
					m_panDown.transform.FindChild("lbCur").gameObject.SetActive(true);
					m_panDown.transform.FindChild("lbCurV").gameObject.SetActive(true);
					m_panDown.transform.FindChild("lbCurV").gameObject.GetComponent<UILabel>().text = iCurNum.ToString()+"/"+iMaxNum.ToString();
				}

				//可以合成..
				if (iCurNum>=iMaxNum)
				{
					if (m_btnPetGather)
						m_btnPetGather.SetActive(true);
				}
				else
				{
					if (m_btnPetGather)
						m_btnPetGather.SetActive(true);
				}
			}
			else
			{
				if (m_panDown)
				{
					m_panDown.transform.FindChild("lbCur").gameObject.SetActive(false);
					m_panDown.transform.FindChild("lbCurV").gameObject.SetActive(false);
				}
				
				if (m_btnPetGather)
					m_btnPetGather.SetActive(false);
			}
		}
		else
		{
			if (m_panDown)
			{
				m_panDown.transform.FindChild("lbCur").gameObject.SetActive(false);
				m_panDown.transform.FindChild("lbCurV").gameObject.SetActive(false);
			}

			if (m_btnPetGather)
				m_btnPetGather.SetActive(false);
		}
	}

	public GameObject GetPetModel()
	{
		return m_PetModel;
	}
	
	public void ActivePetPropPnl(GameObject PreWnd, UInt64 uPetID)
	{
		m_preWnd = PreWnd;
		m_uuDBID = uPetID;

		m_iPetTemplateID = 0;
		m_iPetUp = 0;
		m_iPetLevel = 1;
		m_bNeedShowGather = false;

		DestroyPetModel();
		PropUIShow(0);
		RefreshPetZuhePage();
		ReflashPetPropUI();
	}

	public void ActivePetPropTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
		m_preWnd = PreWnd;
		m_uuDBID = UInt64.MaxValue;
		
		m_iPetTemplateID = iID;
		m_iPetUp = iUp;
		m_iPetLevel = iLevel;
		m_bNeedShowGather = false;

		DestroyPetModel();
		PropUIShow(0);
		RefreshPetZuhePage();
		ReflashPetPropUI();
	}

	public void ActivePetPropTujianTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
		m_preWnd = PreWnd;
		m_uuDBID = UInt64.MaxValue;
		
		m_iPetTemplateID = iID;
		m_iPetUp = iUp;
		m_iPetLevel = iLevel;
		m_bNeedShowGather = true;
		
		DestroyPetModel();
		PropUIShow(0);
		RefreshPetZuhePage();
		ReflashPetPropUI();
	}

	public void DestroyPetModel()
	{
		if( m_PetModel )
			Destroy(m_PetModel);
		m_PetModel = null;
	}

	public void LoadPetModel()
	{
		// 载入宠物形象..
		if (m_uuDBID!=UInt64.MaxValue)
		{
			SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;

			m_nPetTemplateID = Info.m_uiTemplateID;
			DestroyPetModel();
			
			// 载入模型..
			ResLoadParams param = new ResLoadParams();
			param.pos = new Vector3(0.0f,-163.0f,-200.0f);
			param.rot.Set(0,180.0f,0,0);
			param.scale = new Vector3(180.0f,180.0f,180.0f);
			string strPath = Info.m_strRes.Replace(".prefab","_UI.prefab");
			sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
		}
		else
		{
			if (m_iPetTemplateID>0)
			{
				Hashtable tempInfo = sdConfDataMgr.Instance().GetPetTemplate(m_iPetTemplateID.ToString());
				if (tempInfo != null)
				{
					string strRes = tempInfo["Res"].ToString();

					m_nPetTemplateID = (uint)m_iPetTemplateID;
					DestroyPetModel();
					
					// 载入模型..
					ResLoadParams param = new ResLoadParams();
					param.pos = new Vector3(0.0f,-163.0f,-200.0f);
					param.rot.Set(0,180.0f,0,0);
					param.scale = new Vector3(180.0f,180.0f,180.0f);
					string strPath = strRes.Replace(".prefab","_UI.prefab");
					sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
				}
			}
		}
	}

	public void PetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModel = GameObject.Instantiate(obj) as GameObject;
		m_PetModel.name = "PetPropModel";
		m_PetModel.transform.parent = UnityEngine.GameObject.Find("propModelLeft").transform;
		m_PetModel.transform.localPosition = param.pos;
		m_PetModel.SetActive(true);
		//播放一下idle01的动画..
		sdConfDataMgr.Instance().OnModelPlayIdleAnm(m_PetModel);
		//m_PetModel.transform.localRotation = param.rot;
		//m_PetModel.transform.localScale	= param.scale;
	}
	
	void OnDrag(Vector2 delta)
	{
		if( gameObject.name == "propModelLeft" )
		{
			if( m_PetModel == null ) return;
			m_PetModel.transform.Rotate(0,-delta.x/2.0f,0);
		}
	}
	
	public void SetPetStar(int iStar)
	{
		if (m_pstar0==null || m_pstar1==null || m_pstar2==null || m_pstar3==null || m_pstar4==null)
			return;
		
		if (iStar==1)
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(false);
			m_pstar2.SetActive(false);
			m_pstar3.SetActive(false);
			m_pstar4.SetActive(false);
		}
		else if (iStar==2)
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(true);
			m_pstar2.SetActive(false);
			m_pstar3.SetActive(false);
			m_pstar4.SetActive(false);
		}
		else if (iStar==3)
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(true);
			m_pstar2.SetActive(true);
			m_pstar3.SetActive(false);
			m_pstar4.SetActive(false);
		}
		else if (iStar==4)
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(true);
			m_pstar2.SetActive(true);
			m_pstar3.SetActive(true);
			m_pstar4.SetActive(false);
		}
		else if (iStar==5)
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(true);
			m_pstar2.SetActive(true);
			m_pstar3.SetActive(true);
			m_pstar4.SetActive(true);
		}
		else
		{
			m_pstar0.SetActive(true);
			m_pstar1.SetActive(false);
			m_pstar2.SetActive(false);
			m_pstar3.SetActive(false);
			m_pstar4.SetActive(false);
		}
	}
	
	public void ReflashPetPropUI()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}
		else
		{
			if (m_iPetTemplateID>0)
			{
				Info = sdNewPetMgr.Instance.GetPetInfoByTemplateID(m_iPetTemplateID, m_iPetUp, m_iPetLevel);
				if (Info == null)
					return;
			}
			else
			{
				return;
			}
		}

		//左侧UI显示..
		if (m_picType)
		{
			if (Info.m_iBaseJob==1)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPzs";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==2)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPfs";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==3)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPyx";
				m_picType.SetActive(true);
			}
			else if (Info.m_iBaseJob==4)
			{
				m_picType.GetComponent<UISprite>().spriteName = "IPms";
				m_picType.SetActive(true);
			}
			else
			{
				m_picType.SetActive(false);
			}
		}

		if(m_lbName)
		{
			sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_lbName);
			m_lbName.GetComponent<UILabel>().text = Info.m_strName;
		}
		
		if(m_lbStrong)
		{
			if (Info.m_iUp>0)
			{
				m_lbStrong.GetComponent<UILabel>().text = "+" + Info.m_iUp.ToString();
				m_lbStrong.SetActive(true);
			}
			else
			{
				m_lbStrong.SetActive(false);
			}
		}
		
		if(m_lbValue)
		{
			if (m_uuDBID!=UInt64.MaxValue)
			{
				int iTemp = sdConfDataMgr.Instance().GetPetScore(m_uuDBID);
				m_lbValue.GetComponent<UILabel>().text = iTemp.ToString();
			}
			else
			{
				if (m_iPetTemplateID>0)
				{
					int iTemp = sdConfDataMgr.Instance().GetPetScoreByTemplateID(m_iPetTemplateID, m_iPetUp, m_iPetLevel);
					m_lbValue.GetComponent<UILabel>().text = iTemp.ToString();
				}
			}
		}
		
		if (m_picCz)
		{
			int m_iBz = sdNewPetMgr.Instance.GetIsInBattleTeam(Info.m_uuDBID);
			if (m_iBz==1)
			{
				m_picCz.GetComponent<UISprite>().spriteName = "cz";
				m_picCz.SetActive(true);
			}
			else if (m_iBz==2)
			{
				m_picCz.GetComponent<UISprite>().spriteName = "zz";
				m_picCz.SetActive(true);
			}
			else
			{
				m_picCz.SetActive(false);
			}
		}

		if (Info.m_Lock==1)
		{
			if (m_picLock)
				m_picLock.SetActive(true);

			if (m_btnLock)
				m_btnLock.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "js";
		}
		else
		{
			if (m_picLock)
				m_picLock.SetActive(false);

			if (m_btnLock)
				m_btnLock.transform.FindChild("bg").GetComponent<UISprite>().spriteName = "jjs";
		}

		if (m_iPetTemplateID>0&&m_uuDBID==UInt64.MaxValue)
		{
			if (m_btnLock)
				m_btnLock.SetActive(false);

			if (m_btnPetShow)
				m_btnPetShow.SetActive(false);
		}
		else if (m_iPetTemplateID==0&&m_uuDBID!=UInt64.MaxValue)
		{
			if (m_btnLock)
				m_btnLock.SetActive(true);
			
			if (m_btnPetShow)
				m_btnPetShow.SetActive(true);
		}

		//左侧经验条..
		ReflashExpLineUI();

		//基础属性..
		if(m_lbv1)
		{
			if (Info.m_iBaseJob==1)
			{
				m_lbv1.GetComponent<UILabel>().text = "战士";
			}
			else if (Info.m_iBaseJob==2)
			{
				m_lbv1.GetComponent<UILabel>().text = "法师";
			}
			else if (Info.m_iBaseJob==3)
			{
				m_lbv1.GetComponent<UILabel>().text = "游侠";
			}
			else if (Info.m_iBaseJob==4)
			{
				m_lbv1.GetComponent<UILabel>().text = "牧师";
			}
			else
			{
				m_lbv1.GetComponent<UILabel>().text = "其他";
			}
		}
		
		if(m_lbv21)
			m_lbv21.GetComponent<UILabel>().text = Info.m_strSPD1;
		
		if(m_lbv22)
			m_lbv22.GetComponent<UILabel>().text = Info.m_strSPD2;

		Hashtable infoTmp = sdConfDataMgr.Instance().GetPetTemplate(Info.m_uiTemplateID.ToString());
		if (infoTmp != null)
		{
			if(m_lbv3)
			{
				m_lbv3.GetComponent<UILabel>().text = Info.m_CurProperty.m_iAtkDmgMax.ToString();

				float fTemp = sdNewPetMgr.Instance.GetPetAttCoe(Info.m_uiTemplateID, sdNewPetMgr.MAX_PET_STRONG_LEVEL, sdNewPetMgr.MAX_PET_LEVEL);
				int iTemp = (int)fTemp + int.Parse(infoTmp["Property.AtkDmgMax"].ToString());
				m_lbv3.transform.FindChild("lbv3_0").gameObject.GetComponent<UILabel>().text = "(Max:" + iTemp.ToString() + ")";
			}
			
			if(m_lbv4)
			{
				m_lbv4.GetComponent<UILabel>().text = Info.m_CurProperty.m_iDef.ToString();

				float fTemp = sdNewPetMgr.Instance.GetPetDefCoe(Info.m_uiTemplateID, sdNewPetMgr.MAX_PET_STRONG_LEVEL, sdNewPetMgr.MAX_PET_LEVEL);
				int iTemp = (int)fTemp + int.Parse(infoTmp["Property.Def"].ToString());
				m_lbv4.transform.FindChild("lbv4_0").gameObject.GetComponent<UILabel>().text = "(Max:" + iTemp.ToString() + ")";
			}
			
			if(m_lbv5)
			{
				m_lbv5.GetComponent<UILabel>().text = Info.m_CurProperty.m_iMaxHP.ToString();
				
				float fTemp = sdNewPetMgr.Instance.GetPetHPCoe(Info.m_uiTemplateID, sdNewPetMgr.MAX_PET_STRONG_LEVEL, sdNewPetMgr.MAX_PET_LEVEL);
				int iTemp = (int)fTemp + int.Parse(infoTmp["Property.MaxHP"].ToString());
				m_lbv5.transform.FindChild("lbv5_0").gameObject.GetComponent<UILabel>().text = "(Max:" + iTemp.ToString() + ")";
			}
		}
		
		if (m_lbdesc)
			m_lbdesc.GetComponent<UILabel>().text = Info.m_strDesc;
		
		SetPetStar(Info.m_iAbility);

		ReflashPetSkillUI();
	}
	
	public void ReflashExpLineUI()
	{
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}
		else
		{
			if (m_iPetTemplateID>0)
			{
				Info = sdNewPetMgr.Instance.GetPetInfoByTemplateID(m_iPetTemplateID, m_iPetUp, m_iPetLevel);
				if (Info == null)
					return;
			}
			else
			{
				return;
			}
		}

		if(m_lbLevel)
			m_lbLevel.GetComponent<UILabel>().text = "Lv." + Info.m_iLevel.ToString();
		
		UInt64 myExp = Info.m_uuExperience;
		int iAbility = Info.m_iAbility;
		int iLevel = Info.m_iLevel;
		UInt64 uuLevelUPExp = sdNewPetMgr.Instance.GetPetCurLevelNeedExp(Info.m_uiTemplateID, iAbility, iLevel);

		float fValue = 1.0f;
		if (uuLevelUPExp>0)
			fValue = (float)myExp/(float)uuLevelUPExp;
		if (fValue>1.0f)
			fValue = 1.0f;
		
		if (m_expBar!=null)
			m_expBar.GetComponent<UISlider>().value = fValue;
		
		if(m_lbExp)
		{
			fValue = fValue*100.0f;
			int iValue = (int)fValue;
			m_lbExp.GetComponent<UILabel>().text = iValue.ToString() + "%";
		}
	}

	public void SetPetModelVisible(bool bShow)
	{
		if (m_PetModel)
			m_PetModel.SetActive(bShow);
	}
	
	public void RefreshPetZuhePage()
	{
		int iTmpID = 0;
		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		}
		else
		{
			if (m_iPetTemplateID>0)
			{
				Info = sdNewPetMgr.Instance.GetPetInfoByTemplateID(m_iPetTemplateID, m_iPetUp, m_iPetLevel);
			}
		}
	
		if (Info != null)
			iTmpID = (int)Info.m_uiTemplateID;

		List<sdPetZuheData> listData = new List<sdPetZuheData>();
		
		Hashtable kTable = sdConfDataMgr.Instance().GetPetGroupsTable();
		if (kTable != null)
		{
			foreach (DictionaryEntry de in kTable)
			{
				string key1 = (string)de.Key;
				Hashtable valTable = (Hashtable)de.Value;
				int iPetID1 = int.Parse(valTable["Data1.PetID"].ToString());
				int iPetID2 = int.Parse(valTable["Data2.PetID"].ToString());
				int iPetID3 = int.Parse(valTable["Data3.PetID"].ToString());
				int iPetID4 = int.Parse(valTable["Data4.PetID"].ToString());
				
				bool bNeedShow = false;
				if (iPetID1>0&&iPetID1==iTmpID)
					bNeedShow = true;
				if (iPetID2>0&&iPetID2==iTmpID)
					bNeedShow = true;
				if (iPetID3>0&&iPetID3==iTmpID)
					bNeedShow = true;
				if (iPetID4>0&&iPetID4==iTmpID)
					bNeedShow = true;

				if (bNeedShow)
				{
					sdPetZuheData data = new sdPetZuheData();
					data.iZuheID = int.Parse(key1);

					data.iPetID1 = iPetID1;
					if (iPetID1>0)
						data.iPetEnableType1 = 2;
					else
						data.iPetEnableType1 = 0;

					data.iPetID2 = iPetID2;
					if (iPetID2>0)
						data.iPetEnableType2 = 2;
					else
						data.iPetEnableType2 = 0;

					data.iPetID3 = iPetID3;
					if (iPetID3>0)
						data.iPetEnableType3 = 2;
					else
						data.iPetEnableType3 = 0;

					data.iPetID4 = iPetID4;
					if (iPetID4>0)
						data.iPetEnableType4 = 2;
					else
						data.iPetEnableType4 = 0;

					listData.Add(data);
				}
			}
		}
		
		int num = listData.Count;
		int iZero = 0;
		if (num<2)
			iZero = 2-num;
		
		num = num + iZero;
		int count = petzuheInfoList.Count;
		if (num > count)
		{
			num = num - count;
			for (int i = 0; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyListItem) as GameObject;
				tempItem.GetComponent<sdUIPetZuheIcon>().index = count;
				tempItem.transform.parent = copyListItem.transform.parent;
				tempItem.transform.localPosition = copyListItem.transform.localPosition;
				tempItem.transform.localScale = copyListItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (295*count);
				tempItem.transform.localPosition = pos;
				petzuheInfoList.Add(petzuheInfoList.Count, tempItem.GetComponent<sdUIPetZuheIcon>());
				++count;
			}
		}
		
		IDictionaryEnumerator iter = petzuheInfoList.GetEnumerator();
		foreach (sdPetZuheData infoEntry in listData)
		{
			if (iter.MoveNext())
			{
				sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
				if (infoEntry.iPetEnableType1==1)
					icon.bpetGray0 = true;
				else
					icon.bpetGray0 = false;
				
				if (infoEntry.iPetEnableType2==1)
					icon.bpetGray1 = true;
				else
					icon.bpetGray1 = false;
				
				if (infoEntry.iPetEnableType3==1)
					icon.bpetGray2 = true;
				else
					icon.bpetGray2 = false;
				
				if (infoEntry.iPetEnableType4==1)
					icon.bpetGray3 = true;
				else
					icon.bpetGray3 = false;
				
				icon.SetIdAndReflashUI(infoEntry.iZuheID);
				icon.SetGray(false);
			}
		}
		
		//只显示底板的卡片..
		for (int i=0;i<iZero;i++)
		{
			if (iter.MoveNext())
			{
				sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
				icon.SetIdAndReflashUI(0);
			}
		}
		
		while (iter.MoveNext())
		{
			sdUIPetZuheIcon icon = iter.Value as sdUIPetZuheIcon;
			icon.SetIdAndReflashUI(-1);
		}

		if (copyListItem!=null)
		{
			copyListItem.transform.parent.GetComponent<UIDraggablePanel>().ResetPosition();
		}
	}
	
	public void ReflashPetSkillUI()
	{
		if (m_skill0)
			m_skill0.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill1)
			m_skill1.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill2)
			m_skill2.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill3)
			m_skill3.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);

		SClientPetInfo Info = null;
		if (m_uuDBID!=UInt64.MaxValue)
		{
			Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
		}
		else
		{
			if (m_iPetTemplateID>0)
			{
				Info = sdNewPetMgr.Instance.GetPetInfoByTemplateID(m_iPetTemplateID, m_iPetUp, m_iPetLevel);
				if (Info == null)
					return;
			}
			else
			{
				return;
			}
		}
		
		int skillID = 0;
		if (m_lbskillv0)
		{
			skillID = Info.m_iDfSkill;
			Hashtable cur = sdConfDataMgr.Instance().m_MonsterSkillInfo[skillID] as Hashtable;
			if (cur!=null)
				m_lbskillv0.GetComponent<UILabel>().text = cur["Description"].ToString();
			else
				m_lbskillv0.GetComponent<UILabel>().text = "没有查到该技能..";
		}
		
		Hashtable skillTable = new Hashtable();
		int iIndex = 0;
		if (Info.m_iSkill1>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = Info.m_iSkill1;
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (Info.m_iSkill2>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = Info.m_iSkill2;
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (Info.m_iSkill3>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = Info.m_iSkill3;
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (Info.m_iSkill4>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = Info.m_iSkill4;
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		
		int i = 0;
		foreach(DictionaryEntry skillUnit in skillTable)
		{
			petSkillUnit skill = (petSkillUnit)skillUnit.Value;
			if (i==0 && skill!=null && m_skill0!=null)
				m_skill0.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(skill.m_iSkillID);
			else if (i==1 && skill!=null && m_skill1!=null)
				m_skill1.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(skill.m_iSkillID);
			else if (i==2 && skill!=null && m_skill2!=null)
				m_skill2.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(skill.m_iSkillID);
			else if (i==3 && skill!=null && m_skill3!=null)
				m_skill3.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(skill.m_iSkillID);
			
			i++;
		}
		
		if (m_skill4)
		{
			m_skill4.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(Info.m_iSpSkill);
		}
	}
}

