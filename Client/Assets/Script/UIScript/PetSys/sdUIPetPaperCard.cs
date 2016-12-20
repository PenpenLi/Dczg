using UnityEngine;
using System.Collections;
using System;

public class sdUIPetPaperCard : MonoBehaviour 
{
	public int m_iPetID = -1;
	public int index = -1;
	public int m_iCurNum = 0;
	public int m_iMaxNum = 0;

	public GameObject bg = null;
	public GameObject bgColor = null;
	public GameObject color1 = null;
	public GameObject color2 = null;
	public GameObject type = null;
	public GameObject txtName = null;//宠物名字..
	public GameObject icon = null;
	public GameObject star0 = null;//星星标志0..
	public GameObject star1	= null;//星星标志1..
	public GameObject star2	= null;//星星标志2..
	public GameObject star3	= null;//星星标志3..
	public GameObject star4	= null;//星星标志4..
	public GameObject txtpaper = null;
	public GameObject btnPetPaper = null;
	
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
			if( m_iPetID>0 )
				sdUIPetControl.Instance.ActivePetSmallTip(null, m_iPetID, 0, 1);
		}
	}
	
	public void ReflashPetIconUI(int iID)
	{
		if (iID==-1)
		{
			m_iPetID = -1;
			m_iCurNum = 0;
			m_iMaxNum = 0;
			gameObject.SetActive(false);
			return;
		}
		else if (iID==0)
		{
			m_iPetID = 0;
			m_iCurNum = 0;
			m_iMaxNum = 0;
			gameObject.SetActive(true);

			if (bg)
				bg.SetActive(true);

			if (bgColor)
				bgColor.SetActive(false);

			return;
		}

		m_iPetID = iID;
		gameObject.SetActive(true);

		if (bg)
			bg.SetActive(false);

		if (bgColor)
			bgColor.SetActive(true);

		Hashtable Info = sdConfDataMgr.Instance().GetPetTemplate(m_iPetID.ToString());
		if (Info == null)
			return;

		int iAbility = int.Parse(Info["Ability"].ToString());
		if (txtName)
		{
			sdNewPetMgr.SetLabelColorByAbility(iAbility, txtName);
			txtName.GetComponent<UILabel>().text = Info["Name"].ToString();
		}

		if (type)
		{
			int iType = int.Parse(Info["BaseJob"].ToString());
			if (iType==1)
			{
				type.GetComponent<UISprite>().spriteName = "IPzs";
				type.SetActive(true);
			}
			else if (iType==2)
			{
				type.GetComponent<UISprite>().spriteName = "IPfs";
				type.SetActive(true);
			}
			else if (iType==3)
			{
				type.GetComponent<UISprite>().spriteName = "IPyx";
				type.SetActive(true);
			}
			else if (iType==4)
			{
				type.GetComponent<UISprite>().spriteName = "IPms";
				type.SetActive(true);
			}
			else
			{
				type.SetActive(false);
			}
		}

		if (icon)
			icon.GetComponent<UISprite>().spriteName = Info["Icon"].ToString();

		SetPetStar(iAbility);
		if (iAbility==1)
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf1";

			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2w";
		}
		else if (iAbility==2)
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf2";
			
			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2g";
		}
		else if (iAbility==3)
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf3";
			
			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2b";
		}
		else if (iAbility==4)
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf4";
			
			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2p";
		}
		else if (iAbility==5)
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf5";
			
			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2y";
		}
		else
		{
			if (color1)
				color1.GetComponent<UISprite>().spriteName = "spf1";
			
			if (color2)
				color2.GetComponent<UISprite>().spriteName = "IconL2w";
		}

		if (txtpaper)
			txtpaper.GetComponent<UILabel>().text = m_iCurNum.ToString()+"/"+m_iMaxNum.ToString();

		if (btnPetPaper)
		{
			if (m_iCurNum>=m_iMaxNum)
				btnPetPaper.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName = "btn_2";
			else
				btnPetPaper.transform.FindChild("Background").gameObject.GetComponent<UISprite>().spriteName = "btn_2d";
		}
	}
	
	public void SetPetStar(int iStar)
	{
		//星级..
		if (star0==null || star1==null || star2==null || star3==null || star4==null)
			return;
		
		float fWidth = (float)star0.GetComponent<UISprite>().width*1.0f;
		
		if (iStar==1)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(false);
			star4.SetActive(false); 
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==2)
		{
			star0.SetActive(false);
			star1.SetActive(false);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==3)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(false);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==4)
		{
			star0.SetActive(false);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*0.5f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.5f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.5f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.5f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else if (iStar==5)
		{
			star0.SetActive(true);
			star1.SetActive(true);
			star2.SetActive(true);
			star3.SetActive(true);
			star4.SetActive(true);
			
			star1.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*1.0f, star1.GetComponent<UISprite>().transform.localPosition.y, 
					star1.GetComponent<UISprite>().transform.localPosition.z);
			star2.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*2.0f, star2.GetComponent<UISprite>().transform.localPosition.y, 
					star2.GetComponent<UISprite>().transform.localPosition.z);
			star3.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*3.0f, star3.GetComponent<UISprite>().transform.localPosition.y, 
					star3.GetComponent<UISprite>().transform.localPosition.z);
			star4.GetComponent<UISprite>().transform.localPosition = new Vector3
				(star0.GetComponent<UISprite>().transform.localPosition.x + fWidth*4.0f, star4.GetComponent<UISprite>().transform.localPosition.y, 
					star4.GetComponent<UISprite>().transform.localPosition.z);
		}
		else
		{
			star0.SetActive(true);
			star1.SetActive(false);
			star2.SetActive(false);
			star3.SetActive(false);
			star4.SetActive(false);
		}
	}
}