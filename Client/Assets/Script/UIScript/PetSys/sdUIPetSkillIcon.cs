using UnityEngine;
using System.Collections;
using System;

public class sdUIPetSkillIcon : MonoBehaviour 
{
	public int m_iSkillID = 0;
	
	public GameObject bg		= null;
	public GameObject bglay		= null;
	public GameObject icon		= null;
	
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
	
	public void ReflashPetSkillIconUI(int iSkillID)
	{
		if (iSkillID<=0) 
		{
			m_iSkillID = 0;
			
			if (bg)
				bg.SetActive(false);
			
			if (bglay)
				bglay.SetActive(false);
			
			if (icon)
				icon.SetActive(false);
			
			return;
		}
		
		m_iSkillID = iSkillID;
		Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[m_iSkillID] as Hashtable;
		if (kSkillInfo!=null)
		{
			if (icon)
			{
				icon.GetComponent<UISprite>().spriteName = kSkillInfo["icon"].ToString();
				icon.SetActive(true);
			}
			
			if (bg)
				bg.SetActive(true);
			
			if (bglay)
				bglay.SetActive(true);
		}
		else
		{
			if (icon)
				icon.SetActive(false);
			
			if (bg)
				bg.SetActive(false);
			
			if (bglay)
				bglay.SetActive(false);
		}
	}
}