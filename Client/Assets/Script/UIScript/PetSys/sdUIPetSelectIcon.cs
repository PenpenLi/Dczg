using UnityEngine;
using System.Collections;
using System;

public class sdUIPetSelectIcon : MonoBehaviour 
{
	public UInt64 m_uuDBID = UInt64.MaxValue;
	public int index		= -1;
	public int m_iBz		= 0;		//1参战，2助战，3new，0普通..
	public bool m_bSelect	= false;	//是否选中..
	
	public GameObject m_sPetBz			= null;//参战标志..
	public GameObject m_sPetIcon		= null;//宠物icon..
	public GameObject m_sPetSelect		= null;//选中标志..
	public GameObject m_sPetStar0		= null;//星星标志0..
	public GameObject m_sPetStar1		= null;//星星标志1..
	public GameObject m_sPetStar2		= null;//星星标志2..
	public GameObject m_sPetStar3		= null;//星星标志3..
	public GameObject m_sPetStar4		= null;//星星标志4..
	public GameObject m_sPetType		= null;//宠物职业..
	public GameObject m_sPettxtAdd		= null;//强化等级..
	public GameObject m_sPettxtLevel	= null;//宠物等级..
	public GameObject m_sPettxtName		= null;//宠物名字..
	
	void Awake () 
	{
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public void ReflashPetSelectIconUI(UInt64 uuID)
	{
		if (uuID==UInt64.MaxValue) 
		{
			m_uuDBID = UInt64.MaxValue;
			gameObject.SetActive(false);
			m_iBz		= 0;
			SetPetSelect(false);
			return;
		}
		
		gameObject.SetActive(true);
		m_uuDBID = uuID;
		SetPetSelect(false);
		
		SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
		if (Info == null)
			return;
		
		if (m_sPetBz)
		{
			m_iBz = sdNewPetMgr.Instance.GetIsInBattleTeam(m_uuDBID);
			if (m_iBz==1)
			{
				m_sPetBz.GetComponent<UISprite>().spriteName = "cz";
				m_sPetBz.SetActive(true);
			}
			else if (m_iBz==2)
			{
				m_sPetBz.GetComponent<UISprite>().spriteName = "zz";
				m_sPetBz.SetActive(true);
			}
			else if (m_iBz==3)
			{
				m_sPetBz.GetComponent<UISprite>().spriteName = "new";
				m_sPetBz.SetActive(true);
			}
			else
			{
				m_sPetBz.SetActive(false);
			}
		}
		
		if (m_sPetIcon)
		{
			m_sPetIcon.GetComponent<UISprite>().spriteName = Info.m_strIcon;
			m_sPetIcon.SetActive(true);
		}
		
		SetPetStar(Info.m_iAbility);
		
		if (m_sPetType)
		{
			if (Info.m_iBaseJob==1)
			{
				m_sPetType.GetComponent<UISprite>().spriteName = "zs";
				m_sPetType.SetActive(true);
			}
			else if (Info.m_iBaseJob==2)
			{
				m_sPetType.GetComponent<UISprite>().spriteName = "fs";
				m_sPetType.SetActive(true);
			}
			else if (Info.m_iBaseJob==3)
			{
				m_sPetType.GetComponent<UISprite>().spriteName = "yx";
				m_sPetType.SetActive(true);
			}
			else if (Info.m_iBaseJob==4)
			{
				m_sPetType.GetComponent<UISprite>().spriteName = "ms";
				m_sPetType.SetActive(true);
			}
			else
			{
				m_sPetType.SetActive(false);
			}
		}
		
		if (m_sPettxtAdd)
		{
			if (Info.m_iUp>0)
			{
				m_sPettxtAdd.GetComponent<UILabel>().text = "+" + Info.m_iUp.ToString();
				m_sPettxtAdd.SetActive(true);
			}
			else
			{
				m_sPettxtAdd.SetActive(false);
			}
		}
		
		if (m_sPettxtLevel)
		{
			m_sPettxtLevel.GetComponent<UILabel>().text = "Lv." + Info.m_iLevel.ToString();
			m_sPettxtLevel.SetActive(true);
		}
		
		if (m_sPettxtName)
		{
			sdNewPetMgr.SetLabelColorByAbility(Info.m_iAbility, m_sPettxtName);
			m_sPettxtName.GetComponent<UILabel>().text = Info.m_strName;
			m_sPettxtName.SetActive(true);
		}
	}
	
	public void SetPetStar(int iStar)
	{
		if (m_sPetStar0==null || m_sPetStar1==null || m_sPetStar2==null || m_sPetStar3==null || m_sPetStar4==null)
			return;
		
		float fWidth = (float)m_sPetStar0.GetComponent<UISprite>().width*0.75f;
		
		if (iStar==1)
		{
			m_sPetStar0.SetActive(false);
			m_sPetStar1.SetActive(false);
			m_sPetStar2.SetActive(true);
			m_sPetStar3.SetActive(false);
			m_sPetStar4.SetActive(false);
			
			m_sPetStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_sPetStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			m_sPetStar0.SetActive(false);
			m_sPetStar1.SetActive(false);
			m_sPetStar2.SetActive(true);
			m_sPetStar3.SetActive(true);
			m_sPetStar4.SetActive(false);
			
			m_sPetStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_sPetStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_sPetStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			m_sPetStar0.SetActive(false);
			m_sPetStar1.SetActive(true);
			m_sPetStar2.SetActive(true);
			m_sPetStar3.SetActive(true);
			m_sPetStar4.SetActive(false);
			
			m_sPetStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_sPetStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_sPetStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_sPetStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			m_sPetStar0.SetActive(false);
			m_sPetStar1.SetActive(true);
			m_sPetStar2.SetActive(true);
			m_sPetStar3.SetActive(true);
			m_sPetStar4.SetActive(true);
			
			m_sPetStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, m_sPetStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_sPetStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_sPetStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar3.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, m_sPetStar4.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			m_sPetStar0.SetActive(true);
			m_sPetStar1.SetActive(true);
			m_sPetStar2.SetActive(true);
			m_sPetStar3.SetActive(true);
			m_sPetStar4.SetActive(true);
			
			m_sPetStar1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_sPetStar1.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar1.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_sPetStar2.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar2.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_sPetStar3.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar3.GetComponent<UISprite>().transform.localPosition.z);
			m_sPetStar4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_sPetStar0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, m_sPetStar4.GetComponent<UISprite>().transform.localPosition.y, 
					m_sPetStar4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			m_sPetStar0.SetActive(true);
			m_sPetStar1.SetActive(false);
			m_sPetStar2.SetActive(false);
			m_sPetStar3.SetActive(false);
			m_sPetStar4.SetActive(false);
		}
	}
	
	public void SetPetSelect(bool bSelect)
	{
		m_bSelect = bSelect;
		
		if (m_bSelect)
		{
			if (m_sPetSelect)
				m_sPetSelect.SetActive(true);
		}
		else 
		{
			if (m_sPetSelect)
				m_sPetSelect.SetActive(false);
		}
	}
}