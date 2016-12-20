using UnityEngine;
using System;
using System.Collections;

public class ChoosePetButton : MonoBehaviour 
{
	public int			m_PetIdx;
	public UISprite		m_PetIcon;
	public UISprite		m_PetFrame;
	public GameObject	m_PetAdd;
	public GameObject	m_PetStar1;
	public GameObject	m_PetStar2;
	public GameObject	m_PetStar3;
	public GameObject	m_PetStar4;
	public GameObject	m_PetStar5;
	public GameObject	m_LockMask;
	public GameObject	m_LevelPrepareWnd;

	static GameObject	m_ChooseFriendPetWnd		= null;
	static bool			m_isLoadChooseFriendPetWnd	= false;

	static int			m_PetUnlockLevel			= 12011;
	static int			m_FriPetUnlockLevel			= 13011;
	
	
	void Start () 
	{
		UpdatePet();
	}
	
	public void SetInfo(SClientPetInfo sPet)
	{
		m_PetIcon.spriteName	= sPet.m_strIcon;
		m_PetFrame.spriteName	= sdConfDataMgr.Instance().GetPetQuilityBorder( sPet.m_iAbility ); 
		m_PetAdd.SetActive(false);
		if( sPet.m_iAbility >= 1 ) m_PetStar1.SetActive(true); else m_PetStar1.SetActive(false);
		if( sPet.m_iAbility >= 2 ) m_PetStar2.SetActive(true); else m_PetStar2.SetActive(false);
		if( sPet.m_iAbility >= 3 ) m_PetStar3.SetActive(true); else m_PetStar3.SetActive(false);
		if( sPet.m_iAbility >= 4 ) m_PetStar4.SetActive(true); else m_PetStar4.SetActive(false);
		if( sPet.m_iAbility >= 5 ) m_PetStar5.SetActive(true); else m_PetStar5.SetActive(false);
		if( sPet.m_iAbility==2 || sPet.m_iAbility==4 )
			m_PetStar1.transform.localPosition = new Vector3(10.0f,m_PetStar1.transform.localPosition.y,0);
		else
			m_PetStar1.transform.localPosition = new Vector3(0,m_PetStar1.transform.localPosition.y,0);
	}
	
	public void UpdatePet()
	{
		// 好友宠物.
		if( m_PetIdx == 100 )
		{
			m_PetIcon.spriteName = "";
			m_PetFrame.spriteName = "";
			m_PetStar1.SetActive(false);
			m_PetAdd.SetActive(true);
			m_LockMask.SetActive(false);

			// 判断好友宠物是否解锁.
			if( sdLevelInfo.GetLevelValid(m_FriPetUnlockLevel) == false )
			{
				m_PetAdd.SetActive(false);
				m_LockMask.SetActive(true); 
			}
			return;
		}

		// 判断宠物是否解锁.
		if( sdLevelInfo.GetLevelValid(m_PetUnlockLevel) == false )
		{
			m_PetIcon.spriteName	= "";
			m_PetFrame.spriteName	= "";
			m_PetAdd.SetActive(false);
			m_PetStar1.SetActive(false);
			m_LockMask.SetActive(true); 
			return;
		}
		
		//UInt64 uPetID = sdNewPetMgr.Instance.GetPetFromTeamByIndex(m_PetIdx);
		UInt64 uPetID = sdNewPetMgr.Instance.mPetAllTeam[m_PetIdx];
		if( uPetID != UInt64.MaxValue )
		{
			SClientPetInfo sPet = sdNewPetMgr.Instance.GetPetInfo(uPetID);

			m_PetIcon.spriteName	= sPet.m_strIcon;
			m_PetFrame.spriteName	= sdConfDataMgr.Instance().GetPetQuilityBorder( sPet.m_iAbility ); 
			m_PetAdd.SetActive(false);
			if( sPet.m_iAbility >= 1 ) m_PetStar1.SetActive(true); else m_PetStar1.SetActive(false);
			if( sPet.m_iAbility >= 2 ) m_PetStar2.SetActive(true); else m_PetStar2.SetActive(false);
			if( sPet.m_iAbility >= 3 ) m_PetStar3.SetActive(true); else m_PetStar3.SetActive(false);
			if( sPet.m_iAbility >= 4 ) m_PetStar4.SetActive(true); else m_PetStar4.SetActive(false);
			if( sPet.m_iAbility >= 5 ) m_PetStar5.SetActive(true); else m_PetStar5.SetActive(false);
			if( sPet.m_iAbility==2 || sPet.m_iAbility==4 )
				m_PetStar1.transform.localPosition = new Vector3(15.0f,m_PetStar1.transform.localPosition.y,0);
			else
				m_PetStar1.transform.localPosition = new Vector3(0,m_PetStar1.transform.localPosition.y,0);
		}
		else
		{
			m_PetIcon.spriteName	= "";
			m_PetFrame.spriteName	= "";
			m_PetAdd.SetActive(true);
			m_PetStar1.SetActive(false);
			m_LockMask.SetActive(false); 
		}
	}

	void Update () 
	{

	}
	
	void OnClick()
	{
		// 好友宠物.
		if( m_PetIdx == 100 )
		{
			if( sdLevelInfo.GetLevelValid(m_FriPetUnlockLevel) == false )
			{
				sdUICharacter.Instance.ShowMsgLine("好友系统还未解锁，不能选择好友战魂。",MSGCOLOR.Yellow);
				return;
			}
			ShowChooseFriendPet();
		}
		else
		{
			if( sdLevelInfo.GetLevelValid(m_PetUnlockLevel) == false )
			{
				sdUICharacter.Instance.ShowMsgLine("战魂系统还未解锁，不能选择战魂。",MSGCOLOR.Yellow);
				return;
			}
			sdUIPetControl.Instance.ActivePetByLevelPrepare(m_LevelPrepareWnd);
			sdUIPetControl.Instance.ActivePetWarPnl(m_LevelPrepareWnd,m_PetIdx/7);
		}	
	}
	
	void ShowChooseFriendPet()
	{
		if( m_ChooseFriendPetWnd == null ) 
		{
			if(m_isLoadChooseFriendPetWnd) return;
			ResLoadParams param = new ResLoadParams();
			sdResourceMgr.Instance.LoadResource("UI/LevelUI/$ChooseFriendPetWnd.prefab", LoadWnd, param);
			m_isLoadChooseFriendPetWnd = true;
		}
		else
		{
			WndAni.ShowWndAni(m_ChooseFriendPetWnd,false,"sp_grey");
			//m_ChooseFriendPetWnd.SetActive(true);
		}
	}
	
	void LoadWnd(ResLoadParams param,UnityEngine.Object obj)
	{
		if( obj == null ) return;
		
		m_ChooseFriendPetWnd = GameObject.Instantiate(obj) as GameObject;
		m_ChooseFriendPetWnd.transform.parent = sdGameLevel.instance.UICamera.transform;
		m_ChooseFriendPetWnd.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
		m_ChooseFriendPetWnd.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
		sdUICharacter.Instance.SetLevelChooseFriPetWnd(m_ChooseFriendPetWnd);
		ShowChooseFriendPet();
		
		m_isLoadChooseFriendPetWnd = false;
	}
}

