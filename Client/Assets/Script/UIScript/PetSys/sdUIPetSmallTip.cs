using System;
using UnityEngine;
using System.Collections;

public class sdUIPetSmallTip : MonoBehaviour 
{
	static private GameObject		m_preWnd			= null;
	
	public GameObject m_bgtz = null;
	public GameObject m_bgDown = null;
	public GameObject m_panSkill = null;
	public GameObject m_star0 = null;
	public GameObject m_star1 = null;
	public GameObject m_star2 = null;
	public GameObject m_star3 = null;
	public GameObject m_star4 = null;
	
	public GameObject m_lbskillv0	= null;
	public GameObject m_skill0		= null;
	public GameObject m_skill1		= null;
	public GameObject m_skill2		= null;
	public GameObject m_skill3		= null;
	public GameObject m_skill4		= null;
	
	public GameObject m_tab_jn		= null;
	public GameObject m_tab_sx		= null;
	public GameObject m_panDown		= null;
	public GameObject m_tj_Type		= null;
	
	static int m_iPetTemplateID = 0;
	static int m_iPetUp = 0;
	static int m_iPetLevel = 1;
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
		if ( gameObject.name=="btnClose")
		{
            if (sdUIPetControl.m_UIPetSmallTip != null)
            {
               // sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetSmallTip);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetSmallTip,false,"bg_grey");
            }
			
			if( m_preWnd )
				m_preWnd.SetActive(true);
		}
		else if ( gameObject.name=="tab_jn" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetSmallTip petPnl = wnd.GetComponentInChildren<sdUIPetSmallTip>();
				if (petPnl)
					petPnl.ShowLeftPanBookUI(true);
			}
		}
		else if ( gameObject.name=="tab_sx" )
		{
			GameObject wnd = GameObject.Find("NGUIRoot");
			if (wnd)
			{
				sdUIPetSmallTip petPnl = wnd.GetComponentInChildren<sdUIPetSmallTip>();
				if (petPnl)
					petPnl.ShowLeftPanBookUI(false);
			}
		}
		else if (gameObject.name=="$PetSmallTip(Clone)")
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetSmallTip petPnl = wnd.GetComponentInChildren<sdUIPetSmallTip>();
				if (petPnl)
					sdConfDataMgr.Instance().OnModelClickRandomPlayAnm(petPnl.GetPetModel());
			}
		}
	}

	public GameObject GetPetModel()
	{
		return m_PetModel;
	}
	
	public void ActivePetSmallTip(GameObject PreWnd, int iID, int iUp, int iLevel)
	{
		m_preWnd = PreWnd;
		ShowPetModelAndUI(iID, iUp, iLevel);
		OnShowWndResetUI();
		DestroyPetModel();
	}
	
	public void OnShowWndResetUI()
	{
		sdRadioButton[] list = GetComponentsInChildren<sdRadioButton>();
		foreach(sdRadioButton btn in list)
		{
			if (m_tab_sx!=null && btn==m_tab_sx.GetComponent<sdRadioButton>())
			{
				btn.Active(true);
				sdUICharacter.Instance.ActiceRadioBtn(btn);	
			}
		}
		ShowLeftPanBookUI(false);
	}
	
	public void ShowLeftPanBookUI(bool bShowSkill)
	{
		if (m_PetModel)
			m_PetModel.SetActive(!bShowSkill);
		
		if (m_bgtz)
			m_bgtz.SetActive(!bShowSkill);

		if (m_bgDown)
			m_bgDown.SetActive(!bShowSkill);

		if (m_panDown)
			m_panDown.SetActive(!bShowSkill);
		
		if (m_panSkill)
			m_panSkill.SetActive(bShowSkill);
	}
	
	public void ShowPetModelAndUI(int iID, int iUp, int iLevel)
	{
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iID.ToString());
		if (info != null)
		{
			m_iPetTemplateID = iID;
			m_iPetUp = iUp;
			m_iPetLevel = iLevel;
			//更新其他属性显示..
			if(m_panDown)
			{
				UILabel[] lables = m_panDown.GetComponentsInChildren<UILabel>();
				for (int i =0; i < lables.Length; ++i)
				{
					UILabel lable = lables[i];
					if (lable && lable.name=="tj_lbt0")
					{
						sdNewPetMgr.SetLabelColorByAbility(int.Parse(info["Ability"].ToString()), lable.gameObject);
						if (iUp>0)
							lable.text = info["Name"].ToString() + " +" + iUp.ToString();
						else
							lable.text = info["Name"].ToString();
					}
					else if (lable && lable.name=="tj_lbt1")
					{
						lable.text = "Lv." + iLevel.ToString();
					}
					else if (lable && lable.name=="tj_lbv2")
					{
						float fTemp = sdNewPetMgr.Instance.GetPetAttCoe((uint)iID, iUp, iLevel);
						int iTemp = (int)fTemp + int.Parse(info["Property.AtkDmgMax"].ToString());
						lable.text = iTemp.ToString();
					}
					else if (lable && lable.name=="tj_lbv3")
					{
						float fTemp = sdNewPetMgr.Instance.GetPetDefCoe((uint)iID, iUp, iLevel);
						int iTemp = (int)fTemp + int.Parse(info["Property.Def"].ToString());
						lable.text = iTemp.ToString();
					}
					else if (lable && lable.name=="tj_lbv4")
					{
						float fTemp = sdNewPetMgr.Instance.GetPetHPCoe((uint)iID, iUp, iLevel);
						int iTemp = (int)fTemp + int.Parse(info["Property.MaxHP"].ToString());
						lable.text = iTemp.ToString();
					}
				}
				
				int iAbility = int.Parse(info["Ability"].ToString());
				if (iAbility==1)
				{
					m_star0.SetActive(true);
					m_star1.SetActive(false);
					m_star2.SetActive(false);
					m_star3.SetActive(false);
					m_star4.SetActive(false);
				}
				else if (iAbility==2)
				{
					m_star0.SetActive(true);
					m_star1.SetActive(true);
					m_star2.SetActive(false);
					m_star3.SetActive(false);
					m_star4.SetActive(false);
				}
				else if (iAbility==3)
				{
					m_star0.SetActive(true);
					m_star1.SetActive(true);
					m_star2.SetActive(true);
					m_star3.SetActive(false);
					m_star4.SetActive(false);
				}
				else if (iAbility==4)
				{
					m_star0.SetActive(true);
					m_star1.SetActive(true);
					m_star2.SetActive(true);
					m_star3.SetActive(true);
					m_star4.SetActive(false);
				}
				else if (iAbility==5)
				{
					m_star0.SetActive(true);
					m_star1.SetActive(true);
					m_star2.SetActive(true);
					m_star3.SetActive(true);
					m_star4.SetActive(true);
				}
			}
			
			//技能显示..
			ReflashPetSkillUI(iID);
			//职业图标..
			UILabel lbTemp = null;
			if (m_tj_Type)
			{
				lbTemp = m_tj_Type.GetComponent<UILabel>();
				if (lbTemp)
				{
					int iBaseJob = int.Parse(info["BaseJob"].ToString());
					if (lbTemp)
					{
						if (iBaseJob==1)
							lbTemp.text = "战士";
						else if (iBaseJob==2)
							lbTemp.text = "法师";
						else if (iBaseJob==3)
							lbTemp.text = "游侠";
						else if (iBaseJob==4)
							lbTemp.text = "牧师";
						else
							lbTemp.text = "其他";
					}
				}
			}
		}
	}

	public void DestroyPetModel()
	{
		if( m_PetModel )
			Destroy(m_PetModel);
		m_PetModel = null;
	}

	public void LoadPetModel()
	{
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(m_iPetTemplateID.ToString());
		if (info != null)
		{
			// 载入宠物形象..
			DestroyPetModel();
			
			ResLoadParams param = new ResLoadParams();
			param.pos = new Vector3(0.0f,-160.0f,-100.0f);
			param.rot.Set(0,180.0f,0,0);
			param.scale = new Vector3(180.0f,180.0f,180.0f);
			string strPath = (info["Res"].ToString()).Replace(".prefab","_UI.prefab");
			sdResourceMgr.Instance.LoadResource(strPath, PetLoadInstantiate, param);
		}
	}
	
	public void PetLoadInstantiate(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		m_PetModel = GameObject.Instantiate(obj) as GameObject;
		m_PetModel.name = "PetSmallTipModel";
		m_PetModel.transform.parent = gameObject.transform;
		m_PetModel.transform.localPosition = param.pos;
		//播放一下idle01的动画..
		sdConfDataMgr.Instance().OnModelPlayIdleAnm(m_PetModel);
	}
	
	void OnDrag(Vector2 delta)
	{
		if( gameObject.name == "$PetSmallTip(Clone)" )
		{
			if( m_PetModel == null ) return;
			m_PetModel.transform.Rotate(0,-delta.x/2.0f,0);
		}
	}
	
	public void ReflashPetSkillUI(int iID)
	{
		if (m_skill0)
			m_skill0.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill1)
			m_skill1.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill2)
			m_skill2.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		if (m_skill3)
			m_skill3.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(0);
		
		Hashtable info = sdConfDataMgr.Instance().GetPetTemplate(iID.ToString());
		if (info == null)
			return;
		
		int skillID = 0;
		if (m_lbskillv0)
		{
			skillID = int.Parse(info["DfSkill"].ToString());
			Hashtable cur = sdConfDataMgr.Instance().m_MonsterSkillInfo[skillID] as Hashtable;;
			if (cur!=null)
				m_lbskillv0.GetComponent<UILabel>().text = cur["Description"].ToString();
			else
				m_lbskillv0.GetComponent<UILabel>().text = "没有查到该技能";
		}
		
		Hashtable skillTable = new Hashtable();
		int iIndex = 0;
		if (int.Parse(info["Skill1"].ToString())>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = int.Parse(info["Skill1"].ToString());
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (int.Parse(info["Skill2"].ToString())>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = int.Parse(info["Skill2"].ToString());
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (int.Parse(info["Skill3"].ToString())>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = int.Parse(info["Skill3"].ToString());
			unit.m_iType = 0;
			skillTable.Add(iIndex, unit);
			iIndex++;
		}
		if (int.Parse(info["Skill4"].ToString())>0)
		{
			petSkillUnit unit = new petSkillUnit();
			unit.m_iSkillID = int.Parse(info["Skill4"].ToString());
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
			m_skill4.GetComponent<sdUIPetSkillIcon>().ReflashPetSkillIconUI(int.Parse(info["SpSkill"].ToString()));
		}
	}
}

