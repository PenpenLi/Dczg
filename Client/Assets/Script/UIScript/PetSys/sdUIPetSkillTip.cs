using System;
using UnityEngine;
using System.Collections;

public class sdUIPetSkillTip : MonoBehaviour 
{
	static private GameObject		m_preWnd			= null;
	
	public GameObject icon = null;
	public GameObject iconBg = null;
	public GameObject icon1 = null;
	public GameObject iconBg1 = null;
	public GameObject label_name = null;
	public GameObject ispassive = null;
	public GameObject label_des = null;
	
	public GameObject value_att = null;
	public GameObject value_cd = null;
	
	public int m_iSkillID = 0;
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update()
	{
	}
	
	void OnClick()
    {
		if ( gameObject.name=="btnClose")
		{
			if( sdUIPetControl.m_UIPetSkillTip != null )
			{
				//sdUIPetControl.Instance.ClosePetPnl(sdUIPetControl.m_UIPetSkillTip);
				WndAni.HideWndAni(sdUIPetControl.m_UIPetSkillTip,false,"bg_grey");
			}
			
			if( m_preWnd )
				m_preWnd.SetActive(true);
		}
	}
	
	public void ActivePetSkillTip(GameObject PreWnd, int iID)
	{
		m_preWnd = PreWnd;
		
		if (iID<=0) 
		{
			m_iSkillID = 0;
			
			if (label_name)
				label_name.GetComponent<UILabel>().text = "技能ID错误";
			
			if (ispassive)
				ispassive.GetComponent<UILabel>().text = "技能ID错误";
			
			if (label_des)
				label_des.GetComponent<UILabel>().text = "技能ID错误";
			
			if (value_att)
				value_att.GetComponent<UILabel>().text = "技能ID错误";
			
			if (value_cd)
				value_cd.GetComponent<UILabel>().text = "技能ID错误";

			return;
		}
		
		m_iSkillID = iID;
		ReflashPetSkillTipUI();
	}
	
	public void ReflashPetSkillTipUI()
	{
		Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[m_iSkillID] as Hashtable;
		if (kSkillInfo!=null)
		{
			if (icon)
			{
				icon.GetComponent<UISprite>().spriteName = kSkillInfo["icon"].ToString();
				icon.SetActive(true);
			}
			
			if (icon1)
			{
				icon1.GetComponent<UISprite>().spriteName = kSkillInfo["icon"].ToString();
				icon1.SetActive(true);
			}
			
			if (label_name)
				label_name.GetComponent<UILabel>().text = kSkillInfo["strName"].ToString();
		
			if (icon)
				icon.SetActive(true);
			if (iconBg)
				iconBg.SetActive(true);
			
			if (icon1)
				icon1.SetActive(false);
			if (iconBg1)
				iconBg1.SetActive(false);
			
			if (ispassive)
			{
				int iType = int.Parse(kSkillInfo["byJob"].ToString());
				if (iType==0)
				{
					ispassive.GetComponent<UILabel>().text = "普通攻击";
				}
				else if (iType==1)
				{
					ispassive.GetComponent<UILabel>().text = "必杀技";
					
					if (icon)
						icon.SetActive(false);
					if (iconBg)
						iconBg.SetActive(false);
					
					if (icon1)
						icon1.SetActive(true);
					if (iconBg1)
						iconBg1.SetActive(true);
				}
				else if (iType==2)
				{
					ispassive.GetComponent<UILabel>().text = "主动技能";
				}
				else if (iType==3)
				{
					ispassive.GetComponent<UILabel>().text = "被动技能";
				}
			}
			
			if (label_des)
				label_des.GetComponent<UILabel>().text = kSkillInfo["Description"].ToString();
			
			if (value_att)
			{
				int attAttr = int.Parse(kSkillInfo["byDamegePro"].ToString());
				string strAtt = "未知属性";
				if(attAttr == 0)
					strAtt = "物理伤害";
				else if(attAttr == 1)
					strAtt = "冰霜伤害";
				else if(attAttr == 2)
					strAtt = "火焰伤害";
				else if(attAttr == 3)
					strAtt = "毒药伤害";
				else if(attAttr == 4)
					strAtt = "雷电伤害";

				value_att.GetComponent<UILabel>().text = strAtt;
			}
			
			if (value_cd)
			{
				float fSec = float.Parse(kSkillInfo["dwCooldown"].ToString());
				fSec = fSec/1000.0f;
				value_cd.GetComponent<UILabel>().text = fSec.ToString()+"秒";
			}
		}
		else
		{
			if (icon)
				icon.SetActive(false);
			
			if (label_name)
				label_name.GetComponent<UILabel>().text = "无此技能";
		
			if (ispassive)
				ispassive.GetComponent<UILabel>().text = "无此技能";
			
			if (label_des)
				label_des.GetComponent<UILabel>().text = "无此技能";
			
			if (value_att)
				value_att.GetComponent<UILabel>().text = "无此技能";
			
			if (value_cd)
				value_cd.GetComponent<UILabel>().text = "无此技能";
		}
	}
}

