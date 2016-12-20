using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 鐢ㄤ簬鐧婚檰鐣岄潰瑙掕壊灞曠ず
/// </summary>
public class sdLoginChar : sdGameActor_Impl
{
	// 鍔ㄧ敾aa
	protected string	kIdleAnim			= null;
    protected string    kShowAnim           = "begin_show";	
	protected string	kShowEffect			= null;	
	protected string	kShowEffect2		= null;	
	protected float		kShowEffectTime		= 0;	
	protected bool 		bShowEffectLoaded	= false;
	protected bool 		bShowEffectLoaded2	= false;

	AudioClip			__audio				=	null;
	
	// 鍒濆?鍖朼a
	public new bool init(sdGameActorCreateInfo kInfo)
	{
		//AvatarCount	= 1;
		base.init(kInfo);
		
		switch (kInfo.mJob)
		{
			case 1:
			case 2:
			case 3:
			{
				LoadWarriorData();
				break;
			}
			
			case 4:
			case 5:
			case 6:
			{
				LoadMageData();
				break;
			}
			
			case 7:
			case 8:
			case 9:
			{
				LoadRougeData();
				break;
			}			
			case 10:
			case 11:
			case 12:
			{
				LoadClericData();
				break;
			}
			
			default:
			{
				LoadWarriorData();
				break;
			}
		}

		return true;
	}
	public  void PlayShow()
    {
        BundleGlobal.Instance.LoadNull(OnPlayShow);
    }
    void OnPlayShow(ResLoadParams param,Object obj)
	{
	// 鍔ㄧ敾鎾?斁..
        if (mAnimController != null
            && kShowAnim != null && mAnimController[kShowAnim] != null
            && kIdleAnim != null && mAnimController[kIdleAnim] != null
            && (kShowEffect == null || bShowEffectLoaded)
            && (kShowEffect2 == null || bShowEffectLoaded2)
            && RenderNode != null)
        {
            mAnimController.cullingType = AnimationCullingType.AlwaysAnimate;
            mAnimController[kShowAnim].wrapMode = WrapMode.Once;
            mAnimController.Play(kShowAnim);
            if (kShowEffect != null) attachEffect(kShowEffect, "gamelevel", transform.localPosition, transform.localRotation, 1.0f, kShowEffectTime);
            if (kShowEffect2 != null) attachEffect(kShowEffect2, "dummy_right_weapon_at", Vector3.zero, Quaternion.identity, 1.0f, kShowEffectTime);

            if (__audio != null && GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().PlayOneShot(__audio);
            }

            PlayIdle();

            SetVisiable_WhenLoading(true);
        }
        else
        {
            BundleGlobal.Instance.LoadNull(OnPlayShow);
        }
	}
	public  void	PlayIdle()
	{
        BundleGlobal.Instance.LoadNull(OnPlayIdle);
    }
    public void OnPlayIdle(ResLoadParams param,Object obj)
    {
		if( mAnimController!=null 
			&& kIdleAnim!=null && mAnimController[kIdleAnim]!=null )
		{
			mAnimController.cullingType			= AnimationCullingType.AlwaysAnimate;
			mAnimController[kIdleAnim].wrapMode = WrapMode.Loop;
			mAnimController.CrossFadeQueued(kIdleAnim,0.3f);
		}
	}
	// 鏇存柊aa
	virtual public void tickFrame()
	{			

	}
	
	public bool IsShowAnimPlaying()
	{
		if( mAnimController==null || kShowAnim==null || AnimController[kShowAnim]==null ) return false;
		return mAnimController.IsPlaying(kShowAnim);
	}
	
	// 浠庤?澶囧垪琛ㄦ洿鏂拌?澶嘺a
	public void updateAvatar(uint [] auiEquipID, uint uiCount)
	{
		if (itemInfo == null || uiCount == 0)
		{
			return;
		}
		
		
		List<sdGameItem> akItemRemain = new List<sdGameItem>();	//< 闇瑕佷繚鐣欑殑瑁呭?aa
		List<sdGameItem> akItemAdd = new List<sdGameItem>();	//< 闇瑕佹坊鍔犵殑瑁呭?aa
		
		// 浠庢柊瑁呭?鍒楄〃鎵惧嚭闇瑕佷繚鐣欑殑瑁呭?鍜岄渶瑕佹柊娣诲姞鐨勮?澶嘺a
		for (int i = 0; i < uiCount; ++i)
		{
			int iEquipID = (int)auiEquipID[i];
			if (itemInfo.ContainsKey(iEquipID))
			{
				akItemRemain.Add(itemInfo[iEquipID] as sdGameItem);
			}
			else
			{
				Hashtable hashItem = sdConfDataMgr.Instance().GetItemById(iEquipID.ToString());
				if (hashItem == null)
					continue;
				
				sdGameItem item = new sdGameItem();
				akItemAdd.Add(item);
				
				item.templateID = iEquipID;
				item.mdlPath = hashItem["Filename"].ToString();
				item .mdlPartName = hashItem["FilePart"].ToString();
				item.anchorNodeName = sdGameActor.WeaponDummy(hashItem["Character"].ToString());
				item.itemClass = int.Parse(hashItem["Class"].ToString());
				item.subClass = int.Parse(hashItem["SubClass"].ToString());
			}
		}
	
		// 浠庡師瑁呭?鍒楄〃鎵惧嚭闇瑕佽?鍗歌浇鐨勮?澶嗠骞跺尯鍒嗘槸鍚︽槸榛樿?瑁呭?鎴栨槸鍚﹀皢瑕佹崲涓婃柊瑁呭?aa
		foreach(DictionaryEntry entry in itemInfo)
		{
			sdGameItem item = entry.Value as sdGameItem;
			
			bool bRemain = false;
			foreach (sdGameItem itemEntry in akItemRemain)
			{
				if (itemEntry.templateID == item.templateID)
				{
					bRemain = true;
					break;
				}
			}
			
			if (bRemain)
				continue;	//< 璇ヨ?澶囪?淇濈暀aa
			
			bool bHasNewEquip = false; 
			foreach (sdGameItem itemEntry in akItemAdd)
			{
				if (itemEntry.itemClass == item.itemClass && itemEntry.subClass == item.subClass)
				{
					bHasNewEquip = true;
					break;
				}
			}
			
			if (bHasNewEquip)
				continue;	//< 璇ヨ?澶囦綅鏈夋柊瑁呭?琚?崲涓奱a
				
			sdGameItem dummyItem = getStartItem(item.equipPos);
			if (dummyItem != null)
			{
				if (dummyItem.templateID == item.templateID)
					akItemRemain.Add(item);		//< 璇ヨ?澶囨槸榛樿?瑁呭?,涓嶉渶瑕佽?鏇挎崲aa
				else
					akItemAdd.Add(dummyItem);	//< 璇ヨ?澶囪?鍗歌浇,闇瑕佹崲涓婇粯璁よ?澶嘺a
			}
		}
		
		// 娓呯┖瑁呭?琛╝a
		itemInfo.Clear();
		
		// 淇濈暀鐨勮?澶囧姞鍏ヨ?澶囪〃aa
		foreach (sdGameItem itemEntry in akItemRemain)
		{
			itemInfo[itemEntry.templateID] = itemEntry;
		}

		// 鏂版坊鍔犵殑瑁呭?(绉婚櫎鐨勮?澶囨崲涓婇粯璁よ?澶嗚鍔犲叆瑁呭?琛ㄥ苟鎹㈣?aa
		foreach (sdGameItem itemEntry in akItemAdd)
		{
			itemInfo[itemEntry.templateID] = itemEntry;
			if(itemEntry.mdlPartName.Length > 0)
			{
				changeAvatar(itemEntry.mdlPath, itemEntry.mdlPartName, itemEntry.anchorNodeName);
			}
		}
	}
	
	// 鏇存柊澶村彂鏍峰紡aa
	public void setHairStyle(int iHairStyle)
	{	
		if (iHairStyle < 0 || iHairStyle > 7)
			return;
		
		if (iHairStyle == mHairStyle)
			return;
		
		mHairStyle = (byte)iHairStyle;
		
		// 鏇存柊鍒濆?瑁呭?琛╝a
		int iHairTemplateID = mHairStyle + 100 + 10 * mGender + 1;
		Hashtable kHairTable = (Hashtable)sdConfDataMgr.Instance().GetHairTable();
		if (kHairTable != null)
		{
			foreach(DictionaryEntry kHairItem in kHairTable)
			{
				Hashtable kHairItemInfo = (Hashtable)kHairItem.Value;
				
				int iTemplateID = int.Parse(kHairItemInfo["Id"].ToString());
				if (iHairTemplateID == iTemplateID)
				{
					// 浠庡垵濮嬭?澶囪〃绉婚櫎鏃х殑鍙戝瀷aa
					int equipPos = int.Parse(kHairItemInfo["Character"].ToString());
					sdGameItem kOldHairItem = getStartItem(equipPos);
					if (kOldHairItem != null)
						mNakeItemTable.Remove(kOldHairItem.templateID);
					
					// 浠庡垵濮嬭?澶囪〃娣诲姞鏂扮殑鍙戝瀷aa
					sdGameItem kNewHairItem = new sdGameItem();	
					
					kNewHairItem.templateID = int.Parse(kHairItemInfo["Id"].ToString());
					kNewHairItem.mdlPath = kHairItemInfo["Filename"].ToString();
					kNewHairItem.mdlPartName = kHairItemInfo["FilePart"].ToString();
					kNewHairItem.anchorNodeName = kHairItemInfo["Dummy"].ToString();
					kNewHairItem.equipPos = equipPos;
								
					mNakeItemTable[kNewHairItem.templateID] = kNewHairItem;
					
					break;
				}
			}	
		}
	
		// 鏇存柊鍙戝瀷aa
		// 	1.浠庤?澶囪〃绉婚櫎鎵鏈夊彂鍨媋a
		//	2.浠庤８妯¤?澶囪〃鑾峰彇榛樿?鍙戝瀷aa
		if (renderNode != null)
		{
			int equipPos = 1;
			
			foreach (DictionaryEntry kItem in itemInfo)
			{
				sdGameItem kEquipItem = kItem.Value as sdGameItem;
				if (kEquipItem.equipPos == equipPos)
				{
					itemInfo.Remove(kItem);
					break;
				}
			}
			
			sdGameItem kHairItem = getStartItem(equipPos);
			if (kHairItem != null)
			{
				itemInfo[kHairItem.templateID] = kHairItem;
				if(kHairItem.mdlPartName.Length > 0)
				{
					
					changeAvatar(kHairItem.mdlPath, kHairItem.mdlPartName, kHairItem.anchorNodeName);	
					isChangeFace = true;
				}
			}
		}
	}
	
	// 鏇存柊澶村彂棰滆壊aa
	public void setHairColor(int iHairColor)
	{	
		if (iHairColor < 0 || iHairColor > 7)
			return;
		
		if (iHairColor == mSkinColor)
			return;
		
		mSkinColor = (byte)iHairColor;
		
		// 鏇存柊棰滆壊aa	
		GameObject kHairObject = meshTable["face"] as GameObject;
		if (kHairObject == null)
			return;
		
		SkinnedMeshRenderer kHairRenderer = kHairObject.GetComponent<SkinnedMeshRenderer>();
		if (kHairRenderer == null)
			return;
		
		foreach (Material kMaterial in kHairRenderer.materials)
		{
			if (kMaterial.name.Contains("hair"))
			{
				if (mSkinColor < 0)
					mSkinColor = 0;
					
				if (mSkinColor >= msColorTableSize)
					mSkinColor = (byte)(msColorTableSize - 1);
		
				kMaterial.color = msColorTable[mSkinColor];
					
				break;
			}
		}	
	}
	

	// 杞藉叆鍔ㄤ綔璧勬簮...
	protected void LoadWarriorData()
	{
		kIdleAnim		= "warrior_idle_01";

		loadAnimation("Model/Mdl_mainChar_0/warrior_action/$warrior_idle_01.FBX");	
		loadAnimation("Model/Mdl_mainChar_0/warrior_action/$warrior_begin_show.FBX");	
		kShowEffect		= "Effect/MainChar/$Warrior/Fx_Warrior_Begin_Show_001.prefab";
		kShowEffect2	= null;
		kShowEffectTime	= 3.0f;
		
		// 棰勮浇鍏ョ壒鏁堣祫婧愶紝閬垮厤鍜屽姩浣滀笉鍚屾?..
		if( kShowEffect != null )
		{
			bShowEffectLoaded = false;
			ResLoadParams param	= new ResLoadParams();
            sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect, OnLoadEffect, param);
		}
		ResLoadParams AudioParam	= new ResLoadParams();
		sdResourceMgr.Instance.LoadResourceImmediately("Music/$creatcharacter/creatcharacter_warrior.wav",OnLoadAudio,AudioParam);
        
	}
	
	protected void LoadMageData()
	{
		kIdleAnim		= "$mage_idle_04";

		loadAnimation("Model/Mdl_mainChar_1/mage_action/$mage_idle_04.FBX");
		loadAnimation("Model/Mdl_mainChar_1/mage_action/$mage_begin_show.FBX");
		kShowEffect		= "Effect/MainChar/$Mage/Fx_Mage_Begin_Show_001.prefab";
		kShowEffect2	= "Effect/MainChar/$Mage/Fx_Mage_Begin_Show_002.prefab";
		kShowEffectTime	= 3.0f;
		
		// 棰勮浇鍏ョ壒鏁堣祫婧愶紝閬垮厤鍜屽姩浣滀笉鍚屾?..
		if( kShowEffect != null )
		{
			bShowEffectLoaded = false;
			ResLoadParams param	= new ResLoadParams();
            sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect, OnLoadEffect, param);
			if( kShowEffect2 != null )
			{
				bShowEffectLoaded2 = false;
                sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect2, OnLoadEffect2, param);
			}
		}
		ResLoadParams AudioParam	= new ResLoadParams();
        sdResourceMgr.Instance.LoadResourceImmediately("Music/$creatcharacter/creatcharacter_mage.wav", OnLoadAudio, AudioParam);
	}
	
	protected void LoadRougeData()
	{
		kIdleAnim		= "$ranger_idle_03";

		loadAnimation("Model/Mdl_mainChar_0/ranger_action/$ranger_idle_03.FBX");	
		loadAnimation("Model/Mdl_mainChar_0/ranger_action/$ranger_begin_show.FBX");
		kShowEffect		= "Effect/MainChar/$Ranger/ranger_begin_show.prefab";
		kShowEffect2	= null;
		kShowEffectTime	= 3.0f;
		
		// 棰勮浇鍏ョ壒鏁堣祫婧愶紝閬垮厤鍜屽姩浣滀笉鍚屾?..
		if( kShowEffect != null )
		{
			bShowEffectLoaded = false;
			ResLoadParams param	= new ResLoadParams();
            sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect, OnLoadEffect, param);
		}
		ResLoadParams AudioParam	= new ResLoadParams();
        sdResourceMgr.Instance.LoadResourceImmediately("Music/$creatcharacter/creatcharacter_ranger.wav", OnLoadAudio, AudioParam);
	}
	
	protected void LoadClericData()
	{
		kIdleAnim		= "$cleric_idle_04";
	
		loadAnimation("Model/Mdl_mainChar_1/cleric_action/$cleric_idle_04.FBX");
		loadAnimation("Model/Mdl_mainChar_1/cleric_action/$cleric_skill05.FBX");
		kShowEffect		= "Effect/MainChar/$Priest/Fx_Priest_Begin_Show_001.prefab";
		kShowEffect2	= "Effect/MainChar/$Priest/Fx_Priest_Begin_Show_002.prefab";
		kShowEffectTime	= 3.0f;
		
		// 棰勮浇鍏ョ壒鏁堣祫婧愶紝閬垮厤鍜屽姩浣滀笉鍚屾?..
		if( kShowEffect != null )
		{
			bShowEffectLoaded = false;
			ResLoadParams param	= new ResLoadParams();
            sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect, OnLoadEffect, param);
			if( kShowEffect2 != null )
			{
				bShowEffectLoaded2 = false;
                sdResourceMgr.Instance.LoadResourceImmediately(kShowEffect2, OnLoadEffect2, param);
			}
		}
		ResLoadParams AudioParam	= new ResLoadParams();
        sdResourceMgr.Instance.LoadResourceImmediately("Music/$creatcharacter/creatcharacter_cleric.wav", OnLoadAudio, AudioParam);
	}
	
	void OnLoadEffect(ResLoadParams param,Object obj)
	{
		bShowEffectLoaded = true;
	}
	void OnLoadEffect2(ResLoadParams param,Object obj)
	{
		bShowEffectLoaded2 = true;
	}

	public void OnLoadAudio(ResLoadParams param,Object obj)
	{
		__audio	=	obj as AudioClip;

	}
    protected override void OnAllLoadFinished()
    {
       PlayShow();

       
    }
}