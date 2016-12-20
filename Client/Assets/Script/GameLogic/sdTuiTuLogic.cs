using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class AreaMonster
{
	public List<sdGameMonster> monsters = new List<sdGameMonster>();
}

public class CameraShaker
{
	private bool cameraShake = false;//相机震动是否开启...
	private Vector3 shakeDir;//相机振动方向...
	private float shakeTime;//振动时间...
	private float shakeW;//震动频率...
	private float shakeAtt;//震动衰减幅度...
	public GameObject mainCamera;
	
	
	public void Update () {
		if(cameraShake)
		{
			float w = 1.0f - shakeAtt*shakeTime;
			if(w <= 0)
			{
				cameraShake = false;
			}
			else
			{
				Vector3 cmove = shakeDir * Mathf.Sin(shakeTime*shakeW)*w;
				shakeTime += Time.deltaTime;
				mainCamera.transform.position += cmove;
			}
		}
	}
	
	public void StartCameraShake(Vector3 dir,float w,float attanuation)
	{
		cameraShake = true;
		shakeDir = dir;
		shakeW = w;
		shakeAtt = attanuation;
		shakeTime = 0.0f;
	}
	
	public void AddRandomCameraShake(float minPower,float maxPower,float w,float attenuation)
	{
		Vector3 dir;
		dir = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f));
		float w1 = UnityEngine.Random.Range(minPower, maxPower);
		dir.Normalize();
		dir *= w1;
		StartCameraShake(dir,w,attenuation);
	}
}

public class sdTuiTuLogic : MonoBehaviour {
	
	private GameObject resultUI = null;
	private GameObject arrowUI = null;
	private GameObject sonArrowUI = null;

	private GameObject JiesuanButton = null;
	
	public CameraShaker cameraShaker;
	
	bool tuituOver = false;
	
	public GameObject uiPanel = null;
	
	public string levelID;

	// 宠物对象列表aa
	protected List<sdGameMonster> mPetList = new List<sdGameMonster>();
	
	// 当前出战的宠物aa
	protected sdGameMonster mActivePet = null;
	public sdGameMonster ActivePet
	{
		get { return mActivePet;}
	}

	// 好友出战宠物aa
	protected sdGameMonster mFriendPet = null;
	public sdGameMonster FriendPet
	{
		get { return mFriendPet;}
	}
	
	private GameObject mainCharacter = null;
	
	private Shader monsterBlendShader;

	//临时怪物音效...
	
	//public AudioClip[] tempSound;

	public bool  bFightInitVisible = true;
	void Awake()
	{
		bFightInitVisible = true;
	}
	
	void Start()
	{
		if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.WorldMap)
		{
			ResLoadParams param3 = new ResLoadParams();
			param3.info = "fight";
			sdResourceMgr.Instance.LoadResourceImmediately("UI/UIPrefab/$Fight.prefab",loadUIObj,param3);

			// 修改摄像机剪切模式.
			GameObject obj = GameObject.Find("UICamera");
			if( obj != null )
				obj.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		}
		
		//if(resultUI != null)
		//	resultUI.SetActive(false);
		CreateMonster();
		
//		SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_TREASURE_CHEST_NTF,
//			OnMessage_SCID_TREASURE_CHEST_NIF);
		
		cameraShaker = new CameraShaker();
		cameraShaker.mainCamera = sdGameLevel.instance.mainCamera.gameObject;
	
		GameObject shaderObj = Resources.Load("BlendShader") as GameObject;
		ShaderList sList = shaderObj.GetComponent<ShaderList>();
		monsterBlendShader = sList.shaderList[0];

		SDGlobal.tmpBag.Clear();
	}	
	
	public Shader GetBlendShader()
	{
		return monsterBlendShader;
	}
	
	public void loadUIObj(ResLoadParams param, UnityEngine.Object obj)
	{
		if(param.info == "arrow")
		{
			if(arrowUI == null)
			{
				arrowUI = GameObject.Instantiate(obj) as GameObject;
			
				/*if(uiPanel != null)
				{
					arrowUI.transform.parent = uiPanel.transform;
			
					arrowUI.transform.localPosition = Vector3.zero;
					arrowUI.transform.localRotation = Quaternion.identity;
					arrowUI.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
				}*/
			
				sonArrowUI = arrowUI.transform.FindChild("sonArrow").gameObject;
		
				arrowUI.SetActive(false);
			}
		}
		else if(param.info == "fight")
		{
			if(sdUICharacter.Instance.GetFightUi() == null)
			{
				GameObject fightUI = GameObject.Instantiate(obj) as GameObject;
				
				sdUICharacter.Instance.SetFightUi(fightUI);
				fightUI.name = "FightUi";
				
				Vector3 localPos = fightUI.transform.localPosition;
				Quaternion localRot = fightUI.transform.localRotation;
				Vector3 localScale = fightUI.transform.localScale;
			
				if(uiPanel != null)
				{
					fightUI.transform.parent = uiPanel.transform;
			
					fightUI.transform.localPosition = localPos;
					fightUI.transform.localRotation = localRot;
					fightUI.transform.localScale = localScale;
				}
				
				
				GameObject kHeadShow = GameObject.Find("HeadShow");	//< 头像显示aa
				if (kHeadShow != null)
				{
					UIAtlas kAtlas = null;
					string kAtlasSprite = null;
					sdGameLevel kGameLevel = sdGameLevel.instance;
					if (kGameLevel != null)
					{
						kAtlas = kGameLevel.AvatarHeadAtlas;
						kAtlasSprite = kGameLevel.AvatarHeadSprite;
					}
					
					UISprite kUISprite = kHeadShow.GetComponent<UISprite>();	
					if (kUISprite)
					{
						kUISprite.atlas = kAtlas;
						kUISprite.spriteName = kAtlasSprite;	
					}	
				}				
				//bool bHide = sdConfDataMgr.Instance().ShowMovie(sdUICharacter.Instance.iCurrentLevelID);
				if(!bFightInitVisible)
				{
					sdUICharacter.Instance.HideFightUi();
					bFightInitVisible = true;
				}
			}
		}
		else if(param.info == "jiesuan")
		{
			if(JiesuanButton == null)
			{
				JiesuanButton = GameObject.Instantiate(obj) as GameObject;
				
				JiesuanButton.name = "JieSuanButon";
			
				Vector3 localPos = JiesuanButton.transform.localPosition;
				Quaternion localRot = JiesuanButton.transform.localRotation;
				Vector3 localScale = JiesuanButton.transform.localScale;
				
				if(uiPanel != null)
				{
					JiesuanButton.transform.parent = uiPanel.transform;
			
					JiesuanButton.transform.localPosition = localPos;
					JiesuanButton.transform.localRotation = localRot;
					JiesuanButton.transform.localScale = localScale;
				}
				
				JiesuanButton.SetActive(false);
			}
		}
		
	}
	
	void Update () {
		
		if(sdGameLevel.instance != null && sdGameLevel.instance.battleSystem.tuiTuLogic == null)
			sdGameLevel.instance.battleSystem.tuiTuLogic = this;
		
		if(mainCharacter == null)
			mainCharacter = sdGameLevel.instance.mainChar.gameObject;
		
		if(tuituOver)
		{
			resultUI.SetActive(true);
		}
		if(cameraShaker!=null)
			cameraShaker.Update();
	}
	
	public void ActiveJieSuanButton()
	{
		if(JiesuanButton != null)
		{
			JiesuanButton.SetActive(true);
		
			sdUICharacter.Instance.IsFight = false;
		}
	}
	
	// 宠物被杀死回调aa
	protected void OnPetKilled(sdActorInterface kMonster)
	{
		int iLastActivePetIndex = sdNewPetMgr.Instance.ActivePetIndex;
		sdNewPetMgr.Instance.DeactivePet();
		ActiveNextPet(iLastActivePetIndex);
	}

	// 好友宠物被杀死回调aa
	protected void OnFriendPetKilled(sdActorInterface kMonster)
	{
		sdNewPetMgr.Instance.DeactiveFriendPet();
	}
	
	// 反激活当前出战宠物aa
	public void DeactivePet()
	{
		if (mActivePet == null)
			return;
		
		//
		mActivePet.NotifyKilled -= OnPetKilled;

		// 
		Hashtable kPetProp = sdNewPetMgr.Instance.GetPetPropertyFromDBID(mActivePet.DBID);
		if (kPetProp != null)
		{
			int iCurHP = mActivePet.GetCurrentHP();
			if (iCurHP < 0)
				iCurHP = 0;
			
			kPetProp["HP"] = iCurHP;	//< 写回当前血量aa
		}
		
		// 
		if (mActivePet.Master != null)
		{
			sdGameActor kActor = mActivePet.Master as sdGameActor;
			kActor.Retainer = null;
	
			mActivePet.Master = null;
		}

		// 
		mActivePet.SetCurrentHP(-1);
		mActivePet.AddHP(0);
		
		// 更新界面aa
		GameObject kFightUI = GameObject.Find("FightUi");
		if (kFightUI != null) 
			kFightUI.GetComponent<sdFightUi>().RefreshPet();
	
		mActivePet = null;
	}

	// 反激活好友宠物aa
	public void DeactiveFriendPet()
	{
		if (mFriendPet == null)
			return;
		
		//
		mFriendPet.NotifyKilled -= OnFriendPetKilled;

		// 
		if (mFriendPet.Master != null)
			mFriendPet.Master = null;

		// 
		mFriendPet.SetCurrentHP(-1);
		mFriendPet.AddHP(0);
		
		// 更新界面aa
		GameObject kFightUI = GameObject.Find("FightUi");
		if (kFightUI != null) 
			kFightUI.GetComponent<sdFightUi>().RefreshPet();

		//
		mFriendPet = null;
	}

	// 激活下一个宠物aa
	public bool ActiveNextPet(int iLastActivePetIndex)
	{
		int iActivePetIndex = -1;
		for (int i = iLastActivePetIndex + 1; i < iLastActivePetIndex + 3; ++i)
		{
			int iPetIndex = (i < sdNewPetMgr.Instance.BattlePetNum) ? i : i - sdNewPetMgr.Instance.BattlePetNum;
			if (sdNewPetMgr.Instance.GetPetActiveCD(iPetIndex) <= 0.0f)
			{
				iActivePetIndex = iPetIndex;
				break;
			}
		}

		if (iActivePetIndex != -1)
		{
			GameObject kFightUI = GameObject.Find("FightUi");
			if (kFightUI != null) 
			{
				kFightUI.GetComponent<sdFightUi>().ActivePet(iActivePetIndex);
				return true;
			}
		}

		return false; ;
	}

	// 激活当前出战的宠物aa
	public void CreatePet()
	{
		if (mActivePet != null)
			DeactivePet();
		
		int iActivePetIndex = sdNewPetMgr.Instance.ActivePetIndex;
		if (iActivePetIndex < 0)
			return;
		
		ulong ulDBID = sdNewPetMgr.Instance.GetPetFromTeamByIndex(iActivePetIndex);
		if (ulDBID == ulong.MaxValue)
			return;

		Hashtable kPetProp = sdNewPetMgr.Instance.GetPetPropertyFromDBID(ulDBID);
		if (kPetProp == null)
			return;
		
		// 
		sdGameMonster kPet = null;
		foreach (sdGameMonster kMonster in mPetList)
		{
			if (kMonster.DBID == ulDBID)
			{
				kPet = kMonster;
				break;
			}
		}
		
		// 显示激活宠物aa
		//	1.更新血量aa
		//	2.设置主从关系aa
		//	3.随机宠物位置aa
		//	4.注册死亡回调aa
		if (kPet != null)
		{
			mActivePet = kPet;
			
			mActivePet.Master = sdGameLevel.instance.mainChar;
			sdGameLevel.instance.mainChar.Retainer = mActivePet;
			
			GameObject kMainPlayer =sdGameLevel.instance.mainChar.gameObject;
			Vector3 kMainPlayerPos = kMainPlayer.transform.position;
			Vector3 kBornPosition = kMainPlayerPos;
			float fFollowDistance = ((int)(kPetProp["FollowDistance"])) / 1000.0f;
			BornPosition(kMainPlayerPos, fFollowDistance, ref kBornPosition);
			mActivePet.gameObject.transform.position = kBornPosition;
			mActivePet.gameObject.transform.rotation = Quaternion.identity;
			
			mActivePet.NotifyKilled += OnPetKilled;

			mActivePet.SetCurrentHP((int)kPetProp["HP"]);	//< 
			mActivePet.AddHP(0);

			// 宠物战队Buffaa
			List<int> kBuffList = sdNewPetMgr.Instance.GetPetGroupBuff(mActivePet);
			if (kBuffList != null)
			{
				foreach (int iBuffId in kBuffList)
				{
					mActivePet.AddBuff(iBuffId, 0, mActivePet);
				}
			}
		}
		else
		{
			string kPath = kPetProp["Res"] as string;
			ResLoadParams kParam = new ResLoadParams();
			kParam.info = "pet";
			kParam.userdata0 = ulDBID;
			sdResourceMgr.Instance.LoadResource(kPath, OnLoadPet, kParam);
		}
	}
	
	// 激活好友出战的宠物aa
	public void CreateFriendPet()
	{
		if (mFriendPet != null)
			return;
		
		Hashtable kPetProp = sdNewPetMgr.Instance.FriendPetProperty;
		if (kPetProp == null)
			return;
		
		string kPath = kPetProp["Res"] as string;
		ResLoadParams kParam = new ResLoadParams();
		kParam.info = "friendPet";
		sdResourceMgr.Instance.LoadResource(kPath, OnLoadPet, kParam);
	}
	
	// 宠物加载回调aa
	protected void OnLoadPet(ResLoadParams kParam, UnityEngine.Object kObj)
	{
		if (kObj == null)
			return;

		// 获取宠物属性表aa
		Hashtable kPetProp = null;
		if (kParam.info == "pet")
		{
			int iActivePetIndex = sdNewPetMgr.Instance.ActivePetIndex;
			if (iActivePetIndex < 0)
				return;
			
			// 检查DBID有效性aa
			ulong ulDBID = sdNewPetMgr.Instance.GetPetFromTeamByIndex(iActivePetIndex);
			if (ulDBID == ulong.MaxValue)
				return;

			// 检查当前激活DBID是否加载的DBIDaa
			ulong ulLoadDBID = (ulong)kParam.userdata0;
			if (ulLoadDBID != ulDBID)
				return;

			kPetProp = sdNewPetMgr.Instance.GetPetPropertyFromDBID(ulDBID);
		}
		else
		{
			kPetProp = sdNewPetMgr.Instance.FriendPetProperty;
		}

		if (kPetProp == null)
			return;
		
		// 随机位置aa
		GameObject kMainPlayer = sdGameLevel.instance.mainChar.gameObject;
		Vector3 kMainPlayerPos = kMainPlayer.transform.position;
		Vector3 kBornPosition = kMainPlayerPos;
		float fFollowDistance = ((int)(kPetProp["FollowDistance"])) / 1000.0f;
		BornPosition(kMainPlayerPos, fFollowDistance, ref kBornPosition);
		
		// 初始化对象aa
		GameObject kPetObj = GameObject.Instantiate(kObj, kBornPosition, Quaternion.identity) as GameObject;
		if (kParam.info == "pet")
		{
			mActivePet = kPetObj.GetComponent<sdGameMonster>();
			if (mActivePet != null)
			{
				mActivePet.templateId = (int)kPetProp["TemplateID"];
				mActivePet.bornLive   = true;
				mActivePet.DBID 	  = (ulong)kPetProp["DBID"];
				mActivePet.Master     = sdGameLevel.instance.mainChar;
				sdGameLevel.instance.mainChar.Retainer = mActivePet;

				mActivePet.NotifyKilled += OnPetKilled;
				
				mPetList.Add(mActivePet);

				// 宠物战队Buffaa
				List<int> kBuffList = sdNewPetMgr.Instance.GetPetGroupBuff(mActivePet);
				if (kBuffList != null)
				{
					foreach (int iBuffId in kBuffList)
					{
						mActivePet.AddBuff(iBuffId, 0, mActivePet);
					}
				}

				// 主角宠物的光圈aa
				Transform t = mActivePet.transform.GetChild(0);
				sdActorInterface.AddHitEffect("Effect/MainChar/FX_UI/$Fx_Go/Fx_Go_Pet_prefab.prefab", t, 1000000.0f, Vector3.zero, true);
			}
		}
		else
		{
			mFriendPet = kPetObj.GetComponent<sdGameMonster>();
			if (mFriendPet != null)
			{
				mFriendPet.templateId = (int)kPetProp["TemplateID"];
				mFriendPet.bornLive   = true;
				mFriendPet.DBID 	  = (ulong)kPetProp["DBID"];
				mFriendPet.Master     = sdGameLevel.instance.mainChar;

				mFriendPet.NotifyKilled += OnFriendPetKilled;
			}
		}
	}
	
	// 宠物手动技能aa
	public void UsePetSkill()
	{
		if (mActivePet != null)
		{
			int iErroeCode = 0;
			if (!mActivePet.DoXPSkill(ref iErroeCode))
			{
				string msg = string.Format("Error_{0}", iErroeCode);
				sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr(msg), Color.yellow);
			}
		}
	}
	
	// 初始化怪物aa
	public void CreateMonster()
	{	
		GameObject[] kMonsterList = GameObject.FindGameObjectsWithTag("Monster");
		if (kMonsterList != null)
		{
			for (int i = 0; i < kMonsterList.Length; i++)
			{
				sdGameMonster kMonster = kMonsterList[i].GetComponent<sdGameMonster>();
				if (kMonster != null)
				{
					if (SDGlobal.msMonsterDropTable != null)
					{
						object kDropItem = SDGlobal.msMonsterDropTable[(uint)kMonster.uniqueId];
						if (kDropItem != null)
						{
							kMonster.DropInfo = kDropItem as SDMonsterDrop;
						}
					}
				}
			}
		}
	}
	
	private void OnMessage_SCID_TREASURE_CHEST_NIF(int iMsgID, ref CMessage msg)
	{
		//Debug.Log("treasure chest");
		tuituOver = true;
		if(JiesuanButton != null)
			JiesuanButton.SetActive(false);
	}
	
	public GameObject GetArrowUI()
	{
		return arrowUI;
	}
	
	public GameObject GetSonArrowUI()
	{
		return sonArrowUI;
	}
	
	// 选择宠物出生点(选择出存在NavMesh但是不跟Monster存在碰撞的位置)aa
	public static bool BornPosition(Vector3 kPos, float fRadius, ref Vector3 kBornPos)
	{
		for(int iCount = 0; iCount < 16; ++iCount)
		{
			float fRandom = UnityEngine.Random.Range(0.0f, 1.0f);
			float fBiasX = Mathf.Sin(fRandom * 2.0f * Mathf.PI) * fRadius;
			float fBiasZ = Mathf.Cos(fRandom * 2.0f * Mathf.PI) * fRadius;
			
			Vector3 kOrigin = kPos;
			kOrigin.y += 2.0f;
			kOrigin.x += fBiasX;
			kOrigin.z += fBiasZ;
			
			Vector3 kDirection = new Vector3(0.0f, -1.0f, 0.0f);
			Ray kRay = new Ray(kOrigin, kDirection);
			bool bHasNavMesh = FingerGesturesInitializer.NavMesh_RayCast(kRay, ref kBornPos, 10.0f);
			if (!bHasNavMesh)
				continue;
			else
			{
                if (Mathf.Abs(kBornPos.y - kPos.y) > 0.5f)
                    continue;
				kRay =	new Ray(kPos, kBornPos - kPos);
				RaycastHit hit;
				if(Physics.Raycast(kRay,out hit))
				{
					float distance = (kBornPos - kPos).magnitude;
					float hitdistance = (hit.point - kPos).magnitude;
                    if (distance > hitdistance)
                        continue;
                    else                    
                        return true;
				}
				else
					return true;
			}
		}		
		kBornPos = kPos;	//< 选择主角位置aa
		return false;
	}

	public static bool NavMesh_RayCast(Vector3 kPos, ref Vector3 hitPoint)
	{
		Vector3 kDirection = new Vector3(0.0f, -1.0f, 0.0f);
		Ray kRay = new Ray(kPos, kDirection);
		bool bHasNavMesh = FingerGesturesInitializer.NavMesh_RayCast(kRay, ref hitPoint, 10.0f);
		return bHasNavMesh;
	}

	// 发送结算消息aa
	public	void ShowFightResult(GameObject boss)
	{
		sdUICharacter.Instance.oldExp = int.Parse(sdGameLevel.instance.mainChar.Property["Experience"].ToString());
		sdUICharacter.Instance.oldLevel = int.Parse(sdGameLevel.instance.mainChar.Property["Level"].ToString());
        CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();

        if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.PET_TRAIN)
        {
            sdGameMonster monster = boss.GetComponent<sdGameMonster>();
            if (monster.DropInfo != null)
            {
                refMSG.m_Item = new CliProto.SDropInfo[monster.DropInfo.items.Length];
                refMSG.m_ItemCount = (ushort)monster.DropInfo.items.Length;
                int i = 0;
                foreach (int id in monster.DropInfo.items)
                {
                    refMSG.m_Item[i] = new CliProto.SDropInfo();
                    refMSG.m_Item[i].m_TemplateID = (uint)id;
                    refMSG.m_Item[i].m_Count = (ushort)1;
                    ++i;
                }
                refMSG.m_Money = (uint)(SDGlobal.tmpBag.money + monster.DropInfo.money);
            }
        }
        else
        {
            if (SDGlobal.tmpBag.itemList.Count > 0)
            {
                refMSG.m_Item = new CliProto.SDropInfo[SDGlobal.tmpBag.itemList.Count];
                refMSG.m_ItemCount = (ushort)SDGlobal.tmpBag.itemList.Count;

                for (int i = 0; i < refMSG.m_Item.Length; i++)
                {
                    refMSG.m_Item[i] = new CliProto.SDropInfo();
                    refMSG.m_Item[i].m_TemplateID = (uint)SDGlobal.tmpBag.itemList[i].itemId;
                    refMSG.m_Item[i].m_Count = (ushort)SDGlobal.tmpBag.itemList[i].itemCount;
                }
            }
        }

		refMSG.m_Result = 0;

		//refMSG.m_Potion0Count = (ushort)sdUICharacter.Instance.MedicineNum();
		refMSG.m_ReliveCount = (ushort)sdUICharacter.Instance.ReliveNum();

		// 深渊结算评级需要特殊处理aa
		if (sdUICharacter.Instance.GetBattleType()==(byte)HeaderProto.LEVEL_BATTLE_TYPE.LEVEL_BATTLE_TYPE_ABYSS)
		{
			//消耗Boss的血量..
			refMSG.m_ActivityAbyssTotalDamage = (int)(sdActGameMgr.Instance.m_uuLapBossLastBlood - sdActGameMgr.Instance.m_uuLapBossNowBlood);
			sdUICharacter.Instance.fightScore = (int)sdConfDataMgr.Instance().GetLapBossResult(sdLevelInfo.GetCurLevelId(),
			                                                                                   sdActGameMgr.Instance.m_uuLapBossLastBlood,
			                                                                                   refMSG.m_ActivityAbyssTotalDamage);
		}
		else
		{
			sdUICharacter.Instance.fightScore = (int)sdConfDataMgr.Instance().GetResult(sdLevelInfo.GetCurLevelId(),sdUICharacter.Instance.fightTime);
		}

		refMSG.m_CompleteResult = (byte)sdUICharacter.Instance.fightScore;
		refMSG.m_LevelBattleType = sdUICharacter.Instance.GetBattleType();	//< 结算类型aa
		
		SDNetGlobal.SendMessage(refMSG);
		//gameObject.SetActive(false);
	}
}
