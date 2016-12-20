using UnityEngine;
using System.Collections;
using System;

public class sdUIPetLevelUpIcon : MonoBehaviour 
{
	public UInt64 m_uuDBID = UInt64.MaxValue;

	public GameObject		m_add		= null;
	public GameObject		m_bg		= null;
	public GameObject		m_icon		= null;
	public GameObject		m_star0		= null;
	public GameObject		m_star1		= null;
	public GameObject		m_star2		= null;
	public GameObject		m_star3		= null;
	public GameObject		m_star4		= null;
	
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
		if (gameObject)
		{
			GameObject wnd = sdGameLevel.instance.NGUIRoot;
			if (wnd)
			{
				sdUIPetLevelPnl petPnl = wnd.GetComponentInChildren<sdUIPetLevelPnl>();
				if (petPnl)
				{
					SClientPetInfo Info = null;
					if (petPnl.m_uuDBID==UInt64.MaxValue)
						return;
					Info = sdNewPetMgr.Instance.GetPetInfo(petPnl.m_uuDBID);
					if (Info == null)
						return;
					
					int iLevel = Info.m_iLevel;
					int iMyLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
					iMyLevel = iMyLevel + 20;
					if (iLevel>=sdNewPetMgr.MAX_PET_LEVEL)
					{
						sdUICharacter.Instance.ShowOkMsg("该战魂已经满级", null);
						return;
					}
					else if (iLevel<sdNewPetMgr.MAX_PET_LEVEL && iLevel>=iMyLevel)
					{
						sdUICharacter.Instance.ShowOkMsg("战魂等级不可高于主角等级20级以上", null);
						return;
					}

					sdUIPetControl.Instance.ActivePetLevelSelectPnl(null, petPnl.m_uuDBID);
					petPnl.SetPetModelVisible(false);
				}
			}
		}
	}
	
	public void SetDBIDAndReflashUI(UInt64 uuDBID)
	{
		m_uuDBID = uuDBID;

		if (m_add)
			m_add.SetActive(true);

		if (m_bg)
		{
			m_bg.SetActive(true);
			m_bg.GetComponent<UISprite>().spriteName = "IconL2w";
		}

		if (m_uuDBID==UInt64.MaxValue)
		{
			if (m_icon)
				m_icon.SetActive(false);
			
			if (m_star0)
				m_star0.SetActive(false);
			
			if (m_star1)
				m_star1.SetActive(false);
			
			if (m_star2)
				m_star2.SetActive(false);
			
			if (m_star3)
				m_star3.SetActive(false);
			
			if (m_star4)
				m_star4.SetActive(false);
		}
		else
		{
			SClientPetInfo Info = sdNewPetMgr.Instance.GetPetInfo(m_uuDBID);
			if (Info == null)
				return;

			if (m_add)
				m_add.SetActive(false);
			
			if (m_icon)
			{
				m_icon.GetComponent<UISprite>().spriteName = Info.m_strIcon;
				m_icon.SetActive(true);
			}
			
			SetPetStar(Info.m_iAbility);
		}
	}
	
	public void SetPetStar(int iStar)
	{
		if (m_star0==null || m_star1==null || m_star2==null || m_star3==null || m_star4==null)
			return;
		
		float fWidth = (float)m_star0.GetComponent<UISprite>().width*0.75f;
		
		if (iStar==1)
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2w";

			m_star0.SetActive(false);
			m_star1.SetActive(false);
			m_star2.SetActive(true);
			m_star3.SetActive(false);
			m_star4.SetActive(false);
			
			m_star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_star2.GetComponent<UISprite>().transform.localPosition.y, 
					m_star2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2g";

			m_star0.SetActive(false);
			m_star1.SetActive(false);
			m_star2.SetActive(true);
			m_star3.SetActive(true);
			m_star4.SetActive(false);
			
			m_star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_star2.GetComponent<UISprite>().transform.localPosition.y, 
					m_star2.GetComponent<UISprite>().transform.localPosition.z);
			m_star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_star3.GetComponent<UISprite>().transform.localPosition.y, 
					m_star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2b";

			m_star0.SetActive(false);
			m_star1.SetActive(true);
			m_star2.SetActive(true);
			m_star3.SetActive(true);
			m_star4.SetActive(false);
			
			m_star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_star1.GetComponent<UISprite>().transform.localPosition.y, 
					m_star1.GetComponent<UISprite>().transform.localPosition.z);
			m_star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_star2.GetComponent<UISprite>().transform.localPosition.y, 
					m_star2.GetComponent<UISprite>().transform.localPosition.z);
			m_star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_star3.GetComponent<UISprite>().transform.localPosition.y, 
					m_star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2p";

			m_star0.SetActive(false);
			m_star1.SetActive(true);
			m_star2.SetActive(true);
			m_star3.SetActive(true);
			m_star4.SetActive(true);
			
			m_star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, m_star1.GetComponent<UISprite>().transform.localPosition.y, 
					m_star1.GetComponent<UISprite>().transform.localPosition.z);
			m_star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, m_star2.GetComponent<UISprite>().transform.localPosition.y, 
					m_star2.GetComponent<UISprite>().transform.localPosition.z);
			m_star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, m_star3.GetComponent<UISprite>().transform.localPosition.y, 
					m_star3.GetComponent<UISprite>().transform.localPosition.z);
			m_star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, m_star4.GetComponent<UISprite>().transform.localPosition.y, 
					m_star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2y";

			m_star0.SetActive(true);
			m_star1.SetActive(true);
			m_star2.SetActive(true);
			m_star3.SetActive(true);
			m_star4.SetActive(true);
			
			m_star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, m_star1.GetComponent<UISprite>().transform.localPosition.y, 
					m_star1.GetComponent<UISprite>().transform.localPosition.z);
			m_star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, m_star2.GetComponent<UISprite>().transform.localPosition.y, 
					m_star2.GetComponent<UISprite>().transform.localPosition.z);
			m_star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, m_star3.GetComponent<UISprite>().transform.localPosition.y, 
					m_star3.GetComponent<UISprite>().transform.localPosition.z);
			m_star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(m_star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, m_star4.GetComponent<UISprite>().transform.localPosition.y, 
					m_star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			m_bg.GetComponent<UISprite>().spriteName = "IconL2w";

			m_star0.SetActive(true);
			m_star1.SetActive(false);
			m_star2.SetActive(false);
			m_star3.SetActive(false);
			m_star4.SetActive(false);
		}
	}
}