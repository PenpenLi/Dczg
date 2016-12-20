using UnityEngine;
using System.Collections;
using System;

public class sdUIPetBattleIcon : MonoBehaviour 
{
	public UInt64 m_uuDBID = UInt64.MaxValue;
	
	public GameObject	m_spIcon		= null;//宠物icon..
	public GameObject	m_spStar0		= null;//星星标志0..
	public GameObject	m_spStar1		= null;//星星标志1..
	public GameObject	m_spStar2		= null;//星星标志2..
	public GameObject	m_spStar3		= null;//星星标志3..
	public GameObject	m_spStar4		= null;//星星标志4..
	public GameObject	m_spType		= null;//宠物职业..
	public GameObject	m_txtAdd		= null;//强化等级..
	public GameObject	m_txtLevel		= null;//宠物等级..
	public GameObject	m_txtName		= null;//宠物名字..
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}

	
	public void ReflashPetBattleIconUI()
	{
		if (m_uuDBID==UInt64.MaxValue)
		{
			if (m_spIcon)
				m_spIcon.SetActive(false);
			
			if (m_spStar0)
				m_spStar0.SetActive(false);
			if (m_spStar1)
				m_spStar1.SetActive(false);
			if (m_spStar2)
				m_spStar2.SetActive(false);
			if (m_spStar3)
				m_spStar3.SetActive(false);
			if (m_spStar4)
				m_spStar4.SetActive(false);
			
			if (m_spType)
				m_spType.SetActive(false);
			
			if (m_txtAdd)
				m_txtAdd.SetActive(false);
			
			if (m_txtLevel)
				m_txtLevel.SetActive(false);
			
			if (m_txtName)
				m_txtName.SetActive(false);
		}
		else
		{
			SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;
			
			if (m_spIcon)
			{
				m_spIcon.gameObject.GetComponent<UISprite>().spriteName = Info.m_strIcon;
				m_spIcon.SetActive(true);
			}
			
			SetPetStar(Info.m_iAbility);
			
			if (m_spType)
			{
				if (Info.m_iBaseJob==1)
				{
					m_spType.GetComponent<UISprite>().spriteName = "zs";
					m_spType.SetActive(true);
				}
				else if (Info.m_iBaseJob==2)
				{
					m_spType.GetComponent<UISprite>().spriteName = "fs";
					m_spType.SetActive(true);
				}
				else if (Info.m_iBaseJob==3)
				{
					m_spType.GetComponent<UISprite>().spriteName = "yx";
					m_spType.SetActive(true);
				}
				else if (Info.m_iBaseJob==4)
				{
					m_spType.GetComponent<UISprite>().spriteName = "ms";
					m_spType.SetActive(true);
				}
				else
				{
					m_spType.SetActive(false);
				}
			}
			
			if (m_txtAdd)
			{
				if (Info.m_iUp>0)
				{
					m_txtAdd.GetComponent<UILabel>().text = "+" + Info.m_iUp.ToString();
					m_txtAdd.SetActive(true);
				}
				else
				{
					m_txtAdd.SetActive(false);
				}
			}
			
			if (m_txtLevel)
			{
				m_txtLevel.GetComponent<UILabel>().text = "Lv." + Info.m_iLevel.ToString();
				m_txtLevel.SetActive(true);
			}
			
			if (m_txtName)
			{
				sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_txtName);
				m_txtName.GetComponent<UILabel>().text = Info.m_strName;
				m_txtName.SetActive(true);
			}
		}
	}
	
	public void SetPetStar(int iStar)
	{
		if (m_spStar0==null || m_spStar1==null || m_spStar2==null || m_spStar3==null || m_spStar4==null)
			return;
		
		float fWidth = (float)m_spStar0.GetComponent<UISprite>().width*0.75f;
		
		if (iStar==1)
		{
			m_spStar0.SetActive(false);
			m_spStar1.SetActive(false);
			m_spStar2.SetActive(true);
			m_spStar3.SetActive(false);
			m_spStar4.SetActive(false);
			
			m_spStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_spStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			m_spStar0.SetActive(false);
			m_spStar1.SetActive(false);
			m_spStar2.SetActive(true);
			m_spStar3.SetActive(true);
			m_spStar4.SetActive(false);
			
			m_spStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_spStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_spStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			m_spStar0.SetActive(false);
			m_spStar1.SetActive(true);
			m_spStar2.SetActive(true);
			m_spStar3.SetActive(true);
			m_spStar4.SetActive(false);
			
			m_spStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_spStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_spStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_spStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			m_spStar0.SetActive(false);
			m_spStar1.SetActive(true);
			m_spStar2.SetActive(true);
			m_spStar3.SetActive(true);
			m_spStar4.SetActive(true);
			
			m_spStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, m_spStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_spStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_spStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar3.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, m_spStar4.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			m_spStar0.SetActive(true);
			m_spStar1.SetActive(true);
			m_spStar2.SetActive(true);
			m_spStar3.SetActive(true);
			m_spStar4.SetActive(true);
			
			m_spStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_spStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_spStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_spStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar3.GetComponent<UISprite>().transform.localPosition.z);
			m_spStar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_spStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, m_spStar4.GetComponent<UISprite>().transform.localPosition.y, 
					m_spStar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			m_spStar0.SetActive(true);
			m_spStar1.SetActive(false);
			m_spStar2.SetActive(false);
			m_spStar3.SetActive(false);
			m_spStar4.SetActive(false);
		}
	}
}