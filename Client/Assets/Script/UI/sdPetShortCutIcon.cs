using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 宠物图标aa
/// </summary>
public class sdPetShortCutIcon : sdShortCutIcon
{	
	// 用于选中显示的边框aa
	protected UISprite mPetSelectSprite = null;
	protected UISprite mPetCD = null;
	protected UISprite mPetHP = null;
	
	protected bool mIsPetActive = false;		//< 宠物激活标记aa 
	protected bool mIsHPEnough = true;			//< 宠物血量是否满足要求aa
	protected float mLastRecoverHPTime = 0.0f;	//< 宠物上次自动回复血量事件aa

	// 初始化(继承自MonoBehaviour)aa
	protected virtual void Start()
	{
		if (mPetSelectSprite == null)
		{
			mPetSelectSprite = gameObject.transform.FindChild("active").GetComponent<UISprite>();
			mPetSelectSprite.spriteName = "";
			mPetCD = gameObject.transform.FindChild("cd").GetComponent<UISprite>();
			if( gameObject.transform.FindChild("Hp") != null )
				mPetHP = gameObject.transform.FindChild("Hp").GetComponent<UISprite>();
			gameObject.transform.localPosition = Vector3.zero;
		}
	}

	// 激活(继承自MonoBehaviour)aa
	protected virtual void OnEnable()
	{
		if (mPetSelectSprite == null)
		{
			mPetSelectSprite = gameObject.transform.FindChild("active").GetComponent<UISprite>();
			mPetSelectSprite.spriteName = "";
			mPetCD = gameObject.transform.FindChild("cd").GetComponent<UISprite>();
			if( gameObject.transform.FindChild("Hp") != null )
				mPetHP = gameObject.transform.FindChild("Hp").GetComponent<UISprite>();
			gameObject.transform.localPosition = Vector3.zero;
		}
	}
	
	// 设置激活aa
	//	1.设置激活效果aa
	//	2.设置激活标记aa
	//	2.记录标记时间aa
	public void ActivePet()
	{
		if (mIsPetActive) 
			return;

		mPetSelectSprite.spriteName = "petselect";
		mIsPetActive = true;
		mLastRecoverHPTime = Time.time;
	}
	
	// 设置取消激活aa
	public void DeactivePet()
	{
		if (!mIsPetActive) 
			return;

		mPetSelectSprite.spriteName = "";
		mIsPetActive = false;
		mLastRecoverHPTime = Time.time;	
	}
	
	// 更新(继承自MonoBehaviour)
	protected override void Update()
	{
		// 更新CDaa
		if (maxCd > 0)
		{
			cooldown -= Time.deltaTime;
			if( mPetSelectSprite.spriteName != "" )
				mPetCD.fillAmount = 0;	// 激活状态不显示CD.
			else
				mPetCD.fillAmount = cooldown/maxCd;
			
			if (cooldown <=0)
				maxCd = 0;	//< CD结束aa
		}

		// 更新宠物血量aa
		if (sdNewPetMgr.Instance.ActivePetIndex == (int)id)
		{
			GameObject kGameLevelObject = sdGameLevel.instance.gameObject;
			sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();	
			sdGameMonster kPet = kTuituLogic.ActivePet;
			if (kPet != null && kPet.Initialized)
			{
				int iCurHP = kPet.GetCurrentHP();
				int iMaxHP = kPet.GetMaxHP();								
				if (mPetHP != null)
					mPetHP.fillAmount = (float)iCurHP / (float)iMaxHP;	//< 更新血条aa
			}
		}
		else if (sdNewPetMgr.Instance.BattlePetNum == (int)id)
		{
			// 宠物不显示血量.
			GameObject kGameLevelObject = sdGameLevel.instance.gameObject;
			sdTuiTuLogic kTuituLogic = kGameLevelObject.GetComponent<sdTuiTuLogic>();	
			sdGameMonster kPet = kTuituLogic.FriendPet;
			if (kPet != null && kPet.Initialized)
			{
				if (Time.time - mLastRecoverHPTime > 20.0f)			//< 20s自动消失aa
				{
					sdNewPetMgr.Instance.DeactiveFriendPet();
				}
			}
		}
		else
		{
			float fDeltaTime = Time.time - mLastRecoverHPTime;
			if (fDeltaTime > 0.99f)
			{
				UInt64 ulDBID = sdNewPetMgr.Instance.GetPetFromTeamByIndex((int)id);
				if (ulDBID != UInt64.MaxValue)
				{
					Hashtable kPetProperty = sdNewPetMgr.Instance.GetPetPropertyFromDBID(ulDBID);
					if (kPetProperty != null) 
					{
						int iMaxHP = (int)kPetProperty["MaxHP"];
						int iDeltaHP = (int)(iMaxHP * 0.01f);
						int iCurHP = (int)kPetProperty["HP"];
						
						if (iCurHP + iDeltaHP > iMaxHP)
							iCurHP = iMaxHP;
						else
							iCurHP += iDeltaHP;
						
						kPetProperty["HP"] = iCurHP;						//< 更新血量aa
						
						if (mPetHP != null)
							mPetHP.fillAmount = (float)iCurHP / (float)iMaxHP;	//< 更新血条aa
						
						mLastRecoverHPTime = Time.time;
						
						if (iCurHP / (float)iMaxHP > 0.3f)					//< 血量超过30%才能召唤aa
							mIsHPEnough = true;	
						else
							mIsHPEnough = false;	
					}
				}
			}
		}	
	}
	
	public void OnClick()
	{
		if (type == ShortCutType.Type_Pet)
		{
			// 主角死亡则禁用宠物选择图标aa
			if (sdGameLevel.instance != null)
			{
				sdMainChar kMainChar = sdGameLevel.instance.mainChar;
				if (kMainChar != null)
				{
					if (kMainChar.GetCurrentHP() <= 0)
						return;
				}
			}

			// 宠物已激活则显示出错信息并返回aa
			if (mIsPetActive)
			{
				sdUICharacter.Instance.ShowMsgLine( sdConfDataMgr.Instance().GetShowStr("petActive") ,MSGCOLOR.Yellow);
				return;
			}

			// 宠物CD尚未结束aa
			if (maxCd > 0) 
			{
				if( sdNewPetMgr.Instance.BattlePetNum == (int)id )
					sdUICharacter.Instance.ShowMsgLine( sdConfDataMgr.Instance().GetShowStr("petFriend") ,MSGCOLOR.Yellow);		// 好友宠物只能使用一次.
				else
					sdUICharacter.Instance.ShowMsgLine( sdConfDataMgr.Instance().GetShowStr("petCooldown") ,MSGCOLOR.Yellow);	// 宠物还在CD中.
				return;	
			}
			
			// 宠物血量不足aa
			if (!mIsHPEnough)
			{
				sdUICharacter.Instance.ShowMsgLine( sdConfDataMgr.Instance().GetShowStr("petHPlow") ,MSGCOLOR.Yellow);	// 宠物血量低于指定数值.
				return;		
			}
				
			// 激活指定宠物aa
			if (sdNewPetMgr.Instance.BattlePetNum == (int)id)
			{
				if (sdNewPetMgr.Instance.ActiveFriendPet() == true)
				{
					// 好友宠物只能使用一次.
					SetCoolDown(99999999, true);

					GameObject kFightUiObject = GameObject.Find("FightUi");
					if (kFightUiObject != null) 
						kFightUiObject.GetComponent<sdFightUi>().RefreshPet();
				}
			}
			else
			{
				if (sdNewPetMgr.Instance.ActivePetByIndex((int)id, 90) == true)
				{
					SetCoolDown(90, true);
						
					GameObject kFightUiObject = GameObject.Find("FightUi");
					if (kFightUiObject != null) 
						kFightUiObject.GetComponent<sdFightUi>().RefreshPet();
				}
			}
		}
	}
}