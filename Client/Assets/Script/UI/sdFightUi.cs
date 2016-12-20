using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 推图界面aa
/// </summary>
public	class sdFightUi : MonoBehaviour
{
	public GameObject monsterHp = null;	// 大BOSS血条aa
	public GameObject seniorHp = null;	// 小BOSS血条aa
	public GameObject relive = null;	// 复活框aa

	private float showTime = -1;
	Hashtable buffList = new Hashtable();
	Hashtable debuffList = new Hashtable();
	
	public GameObject atPanel = null;
	public GameObject mtPanel = null;
	//public GameObject medbtnLeft = null;
	//public GameObject medbtnRight = null;
	
	public GameObject modeBtn1 = null;
	public GameObject modeBtn2 = null;

	// 角色宠物图标对象aa
	public GameObject pet1IconObject = null;
	public GameObject pet2IconObject = null;
	public GameObject pet3IconObject = null;
	protected Hashtable mPetIconObjectList = new Hashtable();

	// 好友宠物图标对象aa
	public GameObject friendPetIconObject = null;

	// 宠物技能图标对象aa
	public GameObject skillPetObject = null;
	protected GameObject mPetSkillIconBackgroundObject = null;

	public GameObject comboPanel = null;
	
	public GameObject changeEffect = null;
	public GameObject PlayerHeadIcon = null;


    //combo
    int m_nCombo = 0;
    GameObject[] sprite_obj = new GameObject[4];
    List<string> lst = new List<string>();
    int nWidth = 30;
    long lastComboTime = 0;
    float fSacleTime = 0.2f;
    float fTime = 0.0f;
    float fFadeTime = 0.0f;
    float fPeakScale = 2.0f;
    public Transform comboRoot = null;

    //pt
    GameObject pt_count = null;
    UILabel lb_ptinfo = null;

    public GameObject setPanel = null;

    public GameObject newSkill = null;
    public GameObject curSkill = null;

    float rSkill = 1.0f;
	
	void Awake()
	{	
		// 根据分辨率调整战斗界面尺寸...
		
		float rPet		= 1.0f;
		float rMedicine = 1.0f;
		float rAt		= 1.0f;
		float rPlayer	= 1.0f;
		float rBoss		= 1.0f;
		
		if( Screen.width==960 && Screen.height==640 )
		{
			// iphone 4,4s
			rSkill		= 1.4f;
			rPet		= 1.18f;
			rMedicine	= 1.1f;
			rAt			= 1.18f;
		}
		else if( Screen.width==1136 && Screen.height==640 )
		{
			// iphone 5,5s
			rSkill		= 1.2f;
		}
		else if( SDGlobal.screenAspectRatio == SCREEN_ASPECT_RATIO.r4_3 )
		{
			// 4:3 分辨率一般都是PAD
			rSkill		= 1.3f;
			rPet		= 1.2f;
			rMedicine	= 1.1f;
			rAt			= 1.2f;
		}
		else if( Screen.height <= 540 )
		{
			// Android 上的小分辨率手机一般都是比较旧的小屏幕手机...
			rSkill		= 1.2f;
		}

		Transform t = gameObject.transform.FindChild("Skill");
		if( t != null )
		{
			t.localScale = new Vector3(rSkill,rSkill,1.0f);
			t.GetComponent<UIAnchor>().relativeOffset *= rSkill;
		}
		t = gameObject.transform.FindChild("Pet");
		if( t != null )
		{
			t.localScale = new Vector3(rPet,rPet,1.0f);
			t.GetComponent<UIAnchor>().relativeOffset *= rPet;
		}
		t = gameObject.transform.FindChild("Player");
		if( t != null )
		{
			t.localScale = new Vector3(rPlayer,rPlayer,1.0f);
			t.GetComponent<UIAnchor>().relativeOffset *= rPlayer;
		}
		t = gameObject.transform.FindChild("Boss");
		if( t != null )
		{
			t.localScale = new Vector3(rBoss,rBoss,1.0f);
			t.GetComponent<UIAnchor>().relativeOffset *= rBoss;
		}
		
// 		if( medbtnLeft!=null && medbtnRight!=null )
// 		{
// 			medbtnLeft.transform.localScale = new Vector3(rMedicine,rMedicine,1.0f);
// 			medbtnLeft.GetComponent<UIAnchor>().relativeOffset *= rMedicine;
// 			medbtnRight.transform.localScale = new Vector3(rMedicine,rMedicine,1.0f);
// 			//medbtnRight.GetComponent<UIAnchor>().relativeOffset *= rMedicine;
// 		}
		if( modeBtn1!=null && modeBtn2!=null )
		{
			modeBtn1.transform.localScale = new Vector3(rAt,rAt,1.0f);
			modeBtn1.GetComponent<UIAnchor>().relativeOffset *= rAt;
			modeBtn2.transform.localScale = new Vector3(rAt,rAt,1.0f);
			modeBtn2.GetComponent<UIAnchor>().relativeOffset *= rAt;
		}

        comboRoot = gameObject.transform.FindChild("combo");
        for (int index = 0; index < sprite_obj.Length; ++index)
        {
            sprite_obj[index] = comboRoot.FindChild("Sprite_num" + index.ToString()).gameObject;
        }
        nWidth = sprite_obj[0].GetComponent<UISprite>().width;
        comboRoot.gameObject.SetActive(false);
		//sdGameLevel.instance.FightUI	=	this;

        pt_count = gameObject.transform.FindChild("pt_count").gameObject;
        lb_ptinfo = pt_count.transform.FindChild("Label_info").GetComponent<UILabel>();
        if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PET_TRAIN)
            SetPT(sdPTManager.Instance.m_nAttack);
        else
            pt_count.SetActive(false);
	}


    public void SetPT(int nAttack)
    {
        lb_ptinfo.text = sdConfDataMgr.Instance().GetShowStr("ptattack") + nAttack.ToString() + "/5";
    }

    public void AddCombo(int nCombo)
    {
        lst.Clear();
        SetComboAlpha(1.0f);
        m_nCombo += nCombo;
        int combo = m_nCombo;
        while (combo > 0)
        {
            int number = combo % 10;
            lst.Add(number.ToString());
            combo = combo / 10;
        }
        int nTotal = lst.Count;
        int index = 0;
        int startX = (nTotal - 1) * nWidth / 2;
        for (; index < nTotal; ++index)
        {
            sprite_obj[index].SetActive(true);
            sprite_obj[index].transform.localPosition = new Vector3(startX - nWidth * index, sprite_obj[index].transform.localPosition.y, 0);
            sprite_obj[index].GetComponent<UISprite>().spriteName = lst[index];
        }
        for (; index < sprite_obj.Length; ++index)
            sprite_obj[index].SetActive(false);
        lastComboTime = System.DateTime.Now.Ticks / 10000000L;
        comboRoot.localScale = Vector3.one;
        fTime = 0.0f;
        fFadeTime = 0.0f;
    }


    public void ResetCombo()
    {
        m_nCombo = 0;
    }
	
	void OnDestroy()
	{
		if (sdUICharacter.Instance != null)sdUICharacter.Instance.isReady = false;	
	}

	// 激活指定图标的宠物aa
	public void ActivePet(int iIndex)
	{
		GameObject kPetObj = mPetIconObjectList[iIndex] as GameObject;
		sdPetShortCutIcon kPetShortCutIcon = kPetObj.GetComponentInChildren<sdPetShortCutIcon>();
		if (kPetShortCutIcon != null)
		{
			kPetObj.SetActive(true);
			kPetShortCutIcon.OnClick();
		}
	}
	
	// 更新宠物相关UIaa
	public void RefreshPet()
	{
		skillPetObject.SetActive(false);
		friendPetIconObject.SetActive(false);

		int iActivePetIndex = sdNewPetMgr.Instance.ActivePetIndex;
		for (int i = 0; i < sdNewPetMgr.Instance.BattlePetNum; i++)
		{
			GameObject petObj = mPetIconObjectList[i] as GameObject;
			
			UInt64 ulDBID = sdNewPetMgr.Instance.GetPetFromTeamByIndex(i);
			if (ulDBID != UInt64.MaxValue)
			{
				Hashtable kPetProperty = sdNewPetMgr.Instance.GetPetPropertyFromDBID(ulDBID);
				if (kPetProperty == null) continue;

				// 宠物图标.
				if (sdConfDataMgr.Instance().PetAtlas != null)
					petObj.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
				else
					LoadPetIcon();
				petObj.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().spriteName = kPetProperty["Icon"] as string;

				// 宠物品质框.
				UISprite spFrame = petObj.transform.FindChild("Button").FindChild("Bg1").GetComponent<UISprite>();
				spFrame.spriteName = sdConfDataMgr.Instance().GetPetQuilityBorder( int.Parse(kPetProperty["Ability"].ToString()) );

				// 宠物激活状态.
				sdPetShortCutIcon petIcon = petObj.transform.FindChild("Button").GetComponent<sdPetShortCutIcon>();
				petIcon.id = (uint)i;
				if(i == iActivePetIndex)
				{
					petIcon.ActivePet();

					int iSpSkillId = (int)kPetProperty["SpSkill"];
					if (iSpSkillId != 0)
					{
						skillPetObject.SetActive(true);

						UISprite sp = mPetSkillIconBackgroundObject.GetComponent<UISprite>();
						if (sp != null)
						{
							Hashtable kSkillInfo = sdConfDataMgr.Instance().m_MonsterSkillInfo[iSpSkillId] as Hashtable;
							if (kSkillInfo != null)
							{
								sp.spriteName = kSkillInfo["icon"] as string;

								if (sdConfDataMgr.Instance().PetSkillAtlas != null)	
									sp.atlas = sdConfDataMgr.Instance().PetSkillAtlas;
								else
									LoadPetSkillIcon();
							}
						}
					}
				}
				else
				{
					petIcon.DeactivePet();
				}
			}
			else
			{
				if (petObj != null)
					petObj.SetActive(false);
			}
		}

		// 好友宠物.
		Hashtable kFriendPetProperty = sdNewPetMgr.Instance.FriendPetProperty;
		if (kFriendPetProperty != null)
		{
			friendPetIconObject.SetActive(true);

			if (sdConfDataMgr.Instance().PetAtlas != null)
				friendPetIconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
			else
				LoadPetIcon();
			friendPetIconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().spriteName = kFriendPetProperty["Icon"] as string;

			// 宠物品质框.
			UISprite spFrame = friendPetIconObject.transform.FindChild("Button").FindChild("Bg1").GetComponent<UISprite>();
			spFrame.spriteName = sdConfDataMgr.Instance().GetPetQuilityBorder( int.Parse(kFriendPetProperty["Ability"].ToString()) );
			
			// 宠物激活状态.
			sdPetShortCutIcon petIcon = friendPetIconObject.transform.FindChild("Button").GetComponent<sdPetShortCutIcon>();
			petIcon.id = (ulong)sdNewPetMgr.Instance.BattlePetNum;
			if (sdNewPetMgr.Instance.IsFriendPetActived)
				petIcon.ActivePet();
			else
				petIcon.DeactivePet();
		}
	}

	// 加载宠物图集aa
	protected bool mIsLoadPetAtlas = false;
	protected void LoadPetIcon()
	{
		if (mIsLoadPetAtlas)
			return;
		
		ResLoadParams kParam = new ResLoadParams();
		kParam.info = "pet";
		string kPath = "UI/Icon/$icon_pet_0/icon_pet_0.prefab";
		sdResourceMgr.Instance.LoadResource(kPath, OnLoadAtlas, kParam, typeof(UIAtlas));

		mIsLoadPetAtlas = true;
	}

	// 加载宠物技能图集aa
	protected bool mIsLoadPetSkillAtlas = false;
	protected void LoadPetSkillIcon()
	{
		if (mIsLoadPetSkillAtlas)
			return;

		ResLoadParams kParam = new ResLoadParams();
		kParam.info = "petskill";
		string kPath = "UI/Icon/$icon_petskill_0/icon_petskill_0.prefab";
		sdResourceMgr.Instance.LoadResource(kPath, OnLoadAtlas, kParam, typeof(UIAtlas));

		mIsLoadPetSkillAtlas = false;
	}

	// 加载图集回调aa
	protected void OnLoadAtlas(ResLoadParams kRes, UnityEngine.Object kObj)
	{
		if (kRes.info == "pet")
		{
			if (sdConfDataMgr.Instance().PetAtlas == null)
				sdConfDataMgr.Instance().PetAtlas = kObj as UIAtlas;

			pet1IconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
			pet2IconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
			pet3IconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
			friendPetIconObject.transform.FindChild("Button").FindChild("Bg").GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetAtlas;
		}
		else if (kRes.info == "petskill")
		{
			if (sdConfDataMgr.Instance().PetSkillAtlas == null)
				sdConfDataMgr.Instance().PetSkillAtlas = kObj as UIAtlas;

			if (mPetSkillIconBackgroundObject != null)
				mPetSkillIconBackgroundObject.GetComponent<UISprite>().atlas = sdConfDataMgr.Instance().PetSkillAtlas;
		}
	}
	
	public void HideRelive()
	{
		WndAni.HideWndAni(relive,false,"sp_grey");
		//if (relive != null) relive.SetActive(false);
	}
	
	public void ShowRelive()
	{
        if (relive.activeSelf) return;
		WndAni.ShowWndAni(relive,false,"sp_grey");
        int maxReliveNum = sdLevelInfo.ReliveLimit(sdUICharacter.Instance.iCurrentLevelID);
        int curReliveNum = maxReliveNum - sdUICharacter.Instance.ReliveNum();
        relive.transform.FindChild("lb_times").GetComponent<UILabel>().text = string.Format(sdConfDataMgr.Instance().GetShowStr("ReliveNum2"), curReliveNum, maxReliveNum);
		relive.transform.FindChild("lb_cost").GetComponent<UILabel>().text = sdUICharacter.Instance.RelivePrice().ToString();
        
        //if (relive != null) relive.SetActive(true);
	}

    bool canAuto = false; 
	void Start()
	{
        sdUICharacter.Instance.ClearReliveNum();
        if (sdLevelInfo.GetLevelValid(12011) || sdGameLevel.instance.testMode)
        {
            canAuto = true;
        }

        if (sdUICharacter.Instance.iCurrentLevelID != 1 || sdGameLevel.instance.testMode)
        {
            setPanel.SetActive(true);
        }
        else
        {
            setPanel.SetActive(false);
        }

		//sdUICharacter.Instance.ShowMask(true, GameObject.Find("suodi"));
		if (sdFriendMgr.Instance.fightFriend != null)
		{
			sdFriendMsg.notifySelectFriAss(sdFriendMgr.Instance.fightFriend.id);
		}
		
		sdBuffIcon[] iconList = GetComponentsInChildren<sdBuffIcon>();
		foreach(sdBuffIcon icon in iconList)
		{
			if (icon.isBuff)
			{
				buffList.Add(icon.index, icon);	
			}
			else
			{
				debuffList.Add(icon.index, icon);
			}
		}

		// 宠物图标对象加入列表aa
		if (pet1IconObject != null) mPetIconObjectList.Add(0, pet1IconObject);
		if (pet2IconObject != null) mPetIconObjectList.Add(1, pet2IconObject);
		if (pet3IconObject != null) mPetIconObjectList.Add(2, pet3IconObject);

		// 宠物技能图标aa
		if (skillPetObject != null)
		{
			Transform kBackgroundTransform = skillPetObject.transform.FindChild("Btn").FindChild("Background");
			if (kBackgroundTransform != null)
				mPetSkillIconBackgroundObject =	kBackgroundTransform.gameObject;
		}

		// 大Boss血条aa
		//monsterHp = GameObject.Find("Boss");		
		if (monsterHp != null) monsterHp.SetActive(false);

		// 小Boss血条aa
		//seniorHp = GameObject.Find("HpBar_Senior");	
		if (seniorHp != null) seniorHp.SetActive(false);

		// 死亡复活框aa
		//relive = GameObject.Find("Relive");
		if (relive != null) relive.SetActive(false);

		Hashtable table = sdGameSkillMgr.Instance.GetSkillList();
		if (table == null) return;
		foreach(DictionaryEntry item in table)
		{
			sdGameSkill skill = (sdGameSkill)item.Value;
			if (skill.isPassive) continue;
			string name = "Normal";
			if (skill.index == 0)
			{
				name = "Skill1";
			} 
			else if (skill.index == 1)
			{
				name = "Normal";	
			}
			else
			{
				name = "Skill"+ skill.index.ToString();
			}
			
			if (atPanel != null)
			{
				Transform skillIcon = atPanel.transform.FindChild(name);
				if (skillIcon != null)
				{
					string iconNam = skill.icon;
                    if (name == "Normal")
                    {
                        skillIcon.FindChild("suodi").FindChild("Background").GetComponent<UISprite>().spriteName = "suodi";
                        skillIcon.FindChild("suodi").GetComponent<sdShortCutIcon>().id = (UInt64)skill.templateID;
                    }
                    else
                    {
                        skillIcon.GetComponentInChildren<UISprite>().spriteName = iconNam;
                        skillIcon.GetComponentInChildren<sdShortCutIcon>().id = (UInt64)skill.templateID;
                        if (skill.templateID == sdGameSkillMgr.Instance.newSkill)
                        {
                            GameObject obj = GameObject.Instantiate(skillIcon.gameObject) as GameObject;
                            obj.transform.parent = newSkill.transform;
                            obj.transform.localPosition = Vector3.zero;
                            obj.transform.localScale = skillIcon.localScale;
                            skillIcon.gameObject.SetActive(false);
                            sdGameSkillMgr.Instance.newSkill = 0;
                            newSkill.SetActive(true);
                            curSkill = skillIcon.gameObject;
                            TweenPosition tp = curSkill.AddComponent<TweenPosition>();
                            Vector3 pos = skillIcon.transform.localPosition;
                            tp.to = pos;
                            tp.duration = 1f;
                            tp.from = newSkill.transform.InverseTransformPoint(Vector3.zero);
                            TweenScale ts = curSkill.AddComponent<TweenScale>();
                            Vector3 scale = curSkill.transform.localScale;
                            ts.to = scale;
                            ts.duration = 1f;
                            ts.from = new Vector3(2,2,2);
                       	}
                    }
				}
			}
			
			if (mtPanel != null)
			{
				Transform skillIcon = mtPanel.transform.FindChild(name);
				if (skillIcon != null)
				{
					string iconNam = skill.icon;
					skillIcon.FindChild("Btn").FindChild("Background").GetComponent<UISprite>().spriteName = iconNam;
					skillIcon.FindChild("Btn").GetComponent<sdShortCutIcon>().id = (UInt64)skill.templateID;
				}
			}
		}

		// 更新宠物图标aa
		RefreshPet();

		// 尝试激活默认宠物aa
		for (int iIndex = 0; iIndex < sdNewPetMgr.Instance.BattlePetNum; iIndex++)
		{
			UInt64 ulDBID = sdNewPetMgr.Instance.GetPetFromTeamByIndex(iIndex);
			if (ulDBID != UInt64.MaxValue)
			{
				ActivePet(iIndex);
				break;
			}
		}
		
		if (sdUICharacter.Instance != null)sdUICharacter.Instance.isReady = true;
		//HideMedicinePrice();
        if (sdGameLevel.instance.mainChar != null)
        {
            BuffChange(sdGameLevel.instance.mainChar.GetBuffs());
        }
        else
        {
            BuffChange(null);
        }
		
		comboPanel.SetActive(false);
		sdUICharacter.Instance.fightTime = 0f;
		sdUICharacter.Instance.IsFight = true;
		
		Hashtable showInfo = new Hashtable();
		showInfo["value"] = sdGameLevel.instance.mainChar["Level"].ToString();
		sdUICharacter.Instance.SetProperty("Level", showInfo);

        showInfo["value"] = sdGameLevel.instance.mainChar["MaxHP"].ToString();
        sdUICharacter.Instance.SetProperty("MaxHP", showInfo);
        sdUICharacter.Instance.SetProperty("HP", showInfo);

        showInfo["value"] = sdGameLevel.instance.mainChar["MaxSP"].ToString();
        sdUICharacter.Instance.SetProperty("MaxSP", showInfo);
        sdUICharacter.Instance.SetProperty("SP", showInfo);

		// 设置视角和操作按钮状态.
		if( sdConfDataMgr.Instance().GetSetting("CFG_Camera") == "1" )
		{
			GameObject o = GameObject.Find("btn_anglemode");
			if( o != null ) o.GetComponentInChildren<UISprite>().spriteName = "sj2";
		}
		if( sdConfDataMgr.Instance().GetSetting("CFG_Move") == "1" )
		{
			GameObject o = GameObject.Find("btn_controlmode");
			if( o != null ) o.GetComponentInChildren<UISprite>().spriteName = "cz2";
		}
	}

	public void ShowMonsterHp()
	{
		if (sdUICharacter.Instance.MonsterHPType == 1)
		{
			showTime = 0;

			if (monsterHp != null)
			{
				monsterHp.SetActive(true);
				GameObject.Find("label_bossname").transform.FindChild("Time").GetComponent<UILabel>().text = "X" + sdUICharacter.Instance.MonsterHpNum.ToString();
			}
		}
		else if (sdUICharacter.Instance.MonsterHPType == 0)
		{
			if (seniorHp != null)
			{
				seniorHp.SetActive(true);	
				GameObject.Find("label_seniorname").transform.FindChild("Time").GetComponent<UILabel>().text = "X"+sdUICharacter.Instance.MonsterHpNum.ToString();
			}
		}
	}
	
	public void HideMonsterHp()
	{
		if (sdUICharacter.Instance.MonsterHPType == 1)
		{
			if (monsterHp != null)
				monsterHp.SetActive(false);
		}
		else if (sdUICharacter.Instance.MonsterHPType == 0)
		{
			if (seniorHp != null)
				seniorHp.SetActive(false);	
		}
	}
	
	public void SetHpNum()
	{
		if (sdUICharacter.Instance.MonsterHPType == 1)
		{
			if (monsterHp != null)
				GameObject.Find("label_bossname").transform.FindChild("Time").GetComponent<UILabel>().text = "X"+sdUICharacter.Instance.CurrentMonsterHpNum.ToString();
		}
		else if (sdUICharacter.Instance.MonsterHPType == 0)
		{
			if (seniorHp != null)
				GameObject.Find("label_seniorname").transform.FindChild("Time").GetComponent<UILabel>().text = "X" + sdUICharacter.Instance.CurrentMonsterHpNum.ToString();
		}
	}
	
	public void SetBossName(string name)
	{
		Hashtable uiValueDesc = new Hashtable();
		uiValueDesc["value"] = name;
		uiValueDesc["des"] = "";
		sdUICharacter.Instance.SetProperty("MonsterName", uiValueDesc);
	}
	
// 	public void ShowMedicinePrice(int price)
// 	{
// 		GameObject medicine = medbtnLeft;
// 		if(medicine == null) return;
// 		UISprite sprite = medicine.transform.FindChild("pricebg").GetComponent<UISprite>();
// 		if (sprite != null) sprite.spriteName = "icon_xz";
// 		UILabel label = medicine.transform.FindChild("price").GetComponent<UILabel>();
// 		if (label != null) label.text = price.ToString();
// 		label = medicine.transform.FindChild("num").GetComponent<UILabel>();
// 		if (label != null) label.text = "";
// 		
// 		medicine = medbtnRight;
// 		if(medicine == null) return;
// 		sprite = medicine.transform.FindChild("pricebg").GetComponent<UISprite>();
// 		if (sprite != null) sprite.spriteName = "icon_xz";
// 		label = medicine.transform.FindChild("price").GetComponent<UILabel>();
// 		if (label != null) label.text = price.ToString();
// 		label = medicine.transform.FindChild("num").GetComponent<UILabel>();
// 		if (label != null) label.text = "";
// 		
// 	}
	
// 	public void HideMedicinePrice()
// 	{
// 		GameObject medicine = medbtnLeft;
// 		if(medicine == null) return;
// 		UISprite sprite = medicine.transform.FindChild("pricebg").GetComponent<UISprite>();
// 		if (sprite != null) sprite.spriteName = "";
// 		UILabel label = medicine.transform.FindChild("price").GetComponent<UILabel>();
// 		if (label != null) label.text = "";
// 		label = medicine.transform.FindChild("num").GetComponent<UILabel>();
// 		if (label != null) label.text = (sdUICharacter.Instance.FreeMedicineNum() - sdUICharacter.Instance.MedicineNum()).ToString();
// 		
// 		medicine = medbtnRight;
// 		if(medicine == null) return;
// 		sprite = medicine.transform.FindChild("pricebg").GetComponent<UISprite>();
// 		if (sprite != null) sprite.spriteName = "";
// 		label = medicine.transform.FindChild("price").GetComponent<UILabel>();
// 		if (label != null) label.text = "";
// 		label = medicine.transform.FindChild("num").GetComponent<UILabel>();
// 		if (label != null) label.text = (sdUICharacter.Instance.FreeMedicineNum() - sdUICharacter.Instance.MedicineNum()).ToString();
// 		
// 	}
	
	bool needUpdate = true;
	bool autoMode = false;
	bool fullMode = false;

    void SetComboAlpha(float fAlpha)
    {
        UISprite combo = comboRoot.gameObject.GetComponent<UISprite>();
        combo.color = new Color(combo.color.r, combo.color.g, combo.color.b, fAlpha);
        for (int index = 0; index < sprite_obj.Length; ++index)
        {
            UISprite sprite = sprite_obj[index].GetComponent<UISprite>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, fAlpha);
        }
    }

    void UpdateCombo()
    {
        if (sdUICharacter.Instance.bBossMovie)
            return;
        if (System.DateTime.Now.Ticks / 10000000L - lastComboTime > 5)
        {
            if (fFadeTime > 0.5f)
                sdUICharacter.Instance.ShowComboWnd(false, 0);
            else
            {
                float fAlpha = 1.0f - fFadeTime * 2.0f;
                SetComboAlpha(fAlpha);
            }
            fFadeTime += Time.deltaTime;
        }
        else
        {
            fTime += Time.deltaTime;
            if (fTime < fSacleTime * 0.5f)
            {
                float scale = 1.0f + fTime * (fPeakScale - 1.0f) / fSacleTime/2.0f;
                comboRoot.localScale = new Vector3(scale, scale, scale);
            }
            else if (fTime < fSacleTime)
            {
                float scale = fPeakScale - (float)((fTime - fSacleTime * 0.5f) * (fPeakScale - 1.0f) / fSacleTime / 2.0f);
            }
            else
                comboRoot.localScale = Vector3.one;
        }
    }

    //boss过场动画需要停止combo消失时间aaa
    public void AddComboTime(long time)
    {
        lastComboTime += time;
    }

	void Update() 
	{
		if (sdUICharacter.Instance.IsFight)
		{
			sdUICharacter.Instance.fightTime += Time.deltaTime;
		}

        if (newSkill != null && newSkill.active && newSkill.GetComponent<Animator>().enabled &&
            newSkill.transform.localScale.x == 2)
        {
            curSkill.transform.position = newSkill.transform.position;
            curSkill.GetComponent<TweenPosition>().from = curSkill.transform.localPosition;
            curSkill.SetActive(true);
            newSkill.SetActive(false);
        }

        UpdateCombo();		
		int id = sdLevelInfo.GetCurLevelId();
		
		// 大BOSS血条的动态出现效果aa
		if (showTime >=0)
		{
			showTime += Time.deltaTime;	
		}
		
		if (showTime > 0 && showTime < 0.2)
		{
			Vector3 vec = monsterHp.transform.localPosition;
			vec.y = (float)(715 - 145*showTime/0.3);
			monsterHp.transform.localPosition=vec;	
		}
		else if (showTime >=0.2 && showTime < 0.3)
		{
			Vector3 vec = monsterHp.transform.localPosition;
			vec.y = (float)(570 + (145*0.3*(showTime-0.2)/0.1));
			monsterHp.transform.localPosition = vec;	
		}
		else if (showTime >= 0.3 && showTime < 0.4)
		{
			Vector3 vec = monsterHp.transform.localPosition;
			vec.y = (float)(613.5 - 43.5*(showTime-0.3)/0.1);
			monsterHp.transform.localPosition = vec;	
		}
		else if (showTime >=0.4 && showTime < 0.45)
		{
			Vector3 vec = monsterHp.transform.localPosition;
			vec.y = (float)(570 + (145*0.05*(showTime-0.4)/0.05));
			monsterHp.transform.localPosition = vec;	
		}
		else if (showTime >= 0.45 && showTime < 0.5)
		{
			Vector3 vec = monsterHp.transform.localPosition;
			vec.y = (float)(577.25 - 7.25*(showTime-0.45)/0.05);
			monsterHp.transform.localPosition = vec;	
		}
		else
		{
			showTime = -1;
		}

		//
		sdGameLevel level = sdGameLevel.instance;
		if (level == null) 
			return;

		if (!needUpdate && level.AutoMode == autoMode && fullMode == level.FullAutoMode) return;
		autoMode = level.AutoMode;
		fullMode = level.FullAutoMode;
		needUpdate = false;
        if (level.AutoMode)
		{
			atPanel.SetActive(true);
			mtPanel.SetActive(false);
            if(canAuto)
            {
                if (level.FullAutoMode)
                {
                    modeBtn1.SetActive(true);
                    modeBtn2.SetActive(false);
                }
                else
                {
                    modeBtn1.SetActive(false);
                    modeBtn2.SetActive(true);
                }
            }
            else
            {
                modeBtn1.SetActive(false);
                modeBtn2.SetActive(false);
            }
		}
		else
		{
			modeBtn1.SetActive(false);
			modeBtn2.SetActive(false);
			mtPanel.SetActive(true);
			atPanel.SetActive(false);
// 			medbtnLeft.SetActive(false);
// 			medbtnRight.SetActive(true);
		}

		//深渊舔怪不需要显示血瓶..
		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
// 			medbtnLeft.SetActive(false);
// 			medbtnRight.SetActive(false);
		}
	}
	
	public void BuffChange(List<sdBuff> list)
	{
		int buffNum = 0;
		int debuffNum = 0;
		if (list != null)
		{
			foreach (sdBuff buff in list)
			{
				if (buff != null)
				{
					int id = buff.GetTemplateID();	
					Hashtable buffTable = sdConfDataMgr.Instance().GetTable("buff") as Hashtable;
					Hashtable item = buffTable[id.ToString()] as Hashtable;
					bool isBuff = false;
					if (item["IsDebuff"] != null)
					{
						int buffType = (int)item["IsDebuff"];	
						isBuff = buffType==0 ? true : false;
					}
					if (isBuff)
					{
						if (buffList[buffNum] == null) continue;
						sdBuffIcon icon = buffList[buffNum] as sdBuffIcon;
						icon.SetBuffId(buff);
						buffNum++;
					}
					else
					{
						if (debuffList[buffNum] == null) continue;
						sdBuffIcon icon = debuffList[buffNum] as sdBuffIcon;
						icon.SetBuffId(buff);
						debuffNum++;
					}
				}
			}
		}

		for(int i = buffNum; i < 6; ++i)
		{
			sdBuffIcon icon = buffList[i] as sdBuffIcon;
			icon.SetBuffId(null);
		}
		
		for(int i = debuffNum; i < 6; ++i)
		{
			sdBuffIcon icon = debuffList[i] as sdBuffIcon;
			icon.SetBuffId(null);
		}
	}
	
	void OnFinish(ResLoadParams para)
	{
		if (modeBtn1 != null)
		{
			modeBtn1.GetComponent<UIButton>().enabled = true;	
		}
		
		if (modeBtn2 != null)
		{
			modeBtn2.GetComponent<UIButton>().enabled = true;	
		}
		//changeEffect.SetActive(false);	
	}
	
	public void ShowChangeEffect()
	{
		if (modeBtn1 != null)
		{
			modeBtn1.GetComponent<UIButton>().enabled = false;	
		}
		
		if (modeBtn2 != null)
		{
			modeBtn2.GetComponent<UIButton>().enabled = false;	
		}
		
		//if (changeEffect != null)
		{
			//changeEffect.SetActive(true);	
			ResLoadParams para = new ResLoadParams();
			sdGameLevel.OnTime time = new sdGameLevel.OnTime(OnFinish);
			sdGameLevel.instance.AddTimeEvent(0.3f,para,time);
		}
	}
}