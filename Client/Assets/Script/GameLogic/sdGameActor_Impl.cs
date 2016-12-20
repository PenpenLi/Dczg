using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 游戏角色创建信息
/// </summary>
public class sdGameActorCreateInfo : object
{
	public ulong mDBID = ulong.MaxValue;
	public ulong mObjID = ulong.MaxValue;
	public byte mGender = 0;
	public byte mSkinColor = 0;
	public byte mHairStyle = 0;
	public byte mBaseJob = 0;
	public byte mJob = 0;
	public int mLevel = 0;
	public string mRoleName = null;
}

/// <summary>
/// 游戏角色对象(提供游戏角色对象的共有策略)
/// </summary>
public class sdGameActor_Impl : sdGameActor
{
	Hashtable 		changeRecord	=	new Hashtable();
	protected bool	bLoadVisiable	=	true;
	public	void	SetVisiable_WhenLoading(bool b)
	{
		bLoadVisiable	=	b;
		Renderer[] rs = GetComponentsInChildren<Renderer>();
		foreach(Renderer r in rs)
		{
			r.enabled	=	b;
		}
	}
	// 头发颜色表aa
	protected static int msColorTableSize = 8;
	protected static Color [] msColorTable = new Color[8]
	{ 	
		new Color(85.0f/255.0f, 	114.0f/255.0f, 	219.0f/255.0f),		//< 1
		new Color(184.0f/255.0f, 	0.0f, 			0.0f),				//< 2
		new Color(240.0f/255.0f, 	194.0f/255.0f, 	0.0f),				//< 3
		new Color(0.0f, 			129.0f/255.0f, 	6.0f/255.0f),		//< 4	
		new Color(149.0f/255.0f, 	0.0f, 			172.0f/255.0f),		//< 5
		new Color(62.0f/255.0f, 	44.0f/255.0f, 	25.0f/255.0f),		//< 6
		new Color(186.0f/255.0f, 	78.0f/255.0f, 	34.0f/255.0f),		//< 7
		new Color(225.0f/255.0f, 	225.0f/255.0f, 	225.0f/255.0f),		//< 8	
	};
	public	uint	HairIndex	=	0;
	protected bool isChangeFace = false;
	
	// 角色裸模装备表aa
	protected Hashtable mNakeItemTable = null; 
	protected List<string> 	lstChangeAvatar	=	new List<string>();
	
	protected override void	Awake()
	{
		base.Awake();
		
		actorType =	ActorType.AT_Player;	//< 标记为玩家aa
	}
	
	// 换装aa
	public void changeAvatar(string path, string partName, string anchorName)
	{
		if(renderNode==null)
		{
			string op	=	path+";"+partName+";"+anchorName;
			lstChangeAvatar.Add(op);
			return;
		}
		
		//脱下装备..
		if(path.Length==0)
		{
			changeAvatarInteral(null, path, partName, anchorName);
			//RefreshAvatar();
			return;
		}
		//穿装备//..
		ResLoadParams param = new ResLoadParams();
		param.info		=	path;
		param.userdata0	=	partName;
		param.userdata1	=	anchorName;
		//uint val	=	InitAvatarCount;
		sdResourceMgr.Instance.LoadResourceImmediately(path,OnLoadAvatar,param);

		
	}
	void	OnLoadAvatar(ResLoadParams param, UnityEngine.Object obj)
	{
		string	strPartName	=	param.userdata0 as string;
		if(obj!=null)
		{
			bool bNeedChange	=	true;
			
			if(strPartName=="face")
			{
				
				if(param._reqIndex < HairIndex)
				{
					bNeedChange	=	false;
				}
				else
				{
					HairIndex	=	param._reqIndex;
				}
			}
			if(bNeedChange)
			{
				GameObject avatar	=	changeAvatarInteral(obj as GameObject, param.info, strPartName, param.userdata1 as string);
				
				if(avatar==null) 
				{
					Debug.Log (param.info +" (" +strPartName+") == null");	
				}
				else
				{
					if(!bLoadVisiable)
					{
						Renderer r = avatar.GetComponent<Renderer>();
						if(r!=null)
						{
							r.enabled	=	false;
						}
					}
				}
			}
		}
		
	}
	
	// 获取角色初始装备aa
	public sdGameItem getStartItem(int equipPos)
	{
		if (mNakeItemTable == null)
			return null;
		
		foreach(DictionaryEntry kItem in mNakeItemTable)
		{
			sdGameItem kGameItem = (sdGameItem)kItem.Value;
			if (kGameItem.equipPos	==	equipPos)
			{
				return kGameItem;	
			}
		}
		
		return null;
	}
	
	// 初始化角色aa	
	protected bool init(sdGameActorCreateInfo kInfo)
	{
		mDBID = kInfo.mDBID;
		mObjID = kInfo.mObjID;
		mGender = kInfo.mGender;
		mSkinColor = kInfo.mSkinColor;
		mHairStyle = kInfo.mHairStyle;
		mBaseJob = kInfo.mBaseJob;
		mJob = kInfo.mJob;
		mLevel = kInfo.mLevel;
        mName = kInfo.mRoleName;
//		name = kInfo.mRoleName;
		
		itemInfo 			= new Hashtable();
		strikedInfo			= new ArrayList();
		
		//< for test
		//skillTree = sdSkillTree.createWarriorSkillTree(this);	
		
		// 根据角色信息构建裸模装备表aa
		mNakeItemTable = new Hashtable();
		
		Hashtable kEquipTable = (Hashtable)sdConfDataMgr.Instance().GetStartEquipTable();
		if (kEquipTable != null)
		{
			foreach(DictionaryEntry kEquipItem in kEquipTable)
			{
				Hashtable kEquipItemInfo = (Hashtable)kEquipItem.Value;
				sdGameItem kItem = new sdGameItem();
				
				int iNeedGender = int.Parse(kEquipItemInfo["NeedSex"].ToString());
				if (iNeedGender != mGender)
					continue;
				
//				int iNeedJob = int.Parse(kEquipItemInfo["NeedJob"].ToString());
//				if (iNeedJob != mJob)
//					continue;
				
				kItem.templateID = int.Parse(kEquipItemInfo["Id"].ToString());
				kItem.mdlPath = kEquipItemInfo["Filename"].ToString();
				kItem.mdlPartName = kEquipItemInfo["FilePart"].ToString();
				kItem.anchorNodeName = kEquipItemInfo["Dummy"].ToString();
				kItem.equipPos = int.Parse(kEquipItemInfo["Character"].ToString());
				
				
				mNakeItemTable[kItem.templateID] = kItem;
			}
		}
		
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
					// 从初始装备表移除旧的发型aa
					int equipPos = int.Parse(kHairItemInfo["Character"].ToString());	
					sdGameItem kOldHairItem = getStartItem(equipPos);
					if (kOldHairItem != null)
						mNakeItemTable.Remove(kOldHairItem.templateID);
					
					// 从初始装备表添加新的发型aa
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
		LoadNake();
		m_summonInfo = sdConfDataMgr.CloneHashTable(sdConfDataMgr.Instance().m_BaseSummon);
		m_SkillEffect = sdConfDataMgr.CloneHashTable(sdConfDataMgr.Instance().m_BaseSkillEffect);
		
		
		
		return true;
	}
	
	void LoadNake()
	{	
		string nakemodle	="";
		switch (mJob)
		{
			case 1:
			case 7:{
				nakemodle=("Model/Mdl_mainChar_0/$base_hero/base_hero.fbx");
			}break;
			case 4:
			case 10:
			{
				nakemodle=("Model/Mdl_mainChar_1/$base_heroine/base_heroine.fbx");
			}break;
			default:
			{
				nakemodle=("Model/Mdl_mainChar_0/$base_hero/base_hero.fbx");
			}break;
		}
		ResLoadParams	param = new ResLoadParams();
		sdResourceMgr.Instance.LoadResourceImmediately(nakemodle,OnLoadNake,param);
	}
	void 	TakeOnDefaultHair()
	{
		sdGameItem kHairItem = getStartItem(1);
		if (kHairItem != null && !itemInfo.Contains(kHairItem.templateID))
		{
			//itemInfo[kHairItem.templateID] = kHairItem;
			if(kHairItem.mdlPartName.Length > 0)
			{
				changeAvatar(kHairItem.mdlPath, kHairItem.mdlPartName, kHairItem.anchorNodeName);	
				isChangeFace = true;
			}
		}
	}
	protected virtual	void	OnLoadNake(ResLoadParams param, UnityEngine.Object obj)
	{
		GameObject newNode = GameObject.Instantiate(obj) as GameObject;
		newNode.name = "@RenderNode";
		newNode.transform.parent = gameObject.transform;
		newNode.transform.localPosition = Vector3.zero;
		newNode.transform.localRotation = Quaternion.identity;
		newNode.transform.localScale = Vector3.one;
		gatherRenderNodesInfo(newNode);
		gatherMeshes(newNode);
		renderNode = newNode;
		
		if(!bLoadVisiable)
		{
			SetVisiable_WhenLoading(bLoadVisiable);
		}
		 
		Animation aniCom = renderNode.GetComponent<Animation>();
		aniCom.playAutomatically = false;
		aniCom.cullingType = AnimationCullingType.AlwaysAnimate;
		if(aniCom == null)
		{
			Debug.LogError("renderNode must have a animation component!");
		}
		else
		{
			mAnimController = aniCom;
		}
		foreach(GameObject animObj in lstAnimation)
		{
			OnLoadAnimation(null,animObj);
		}
		
		TakeOnDefaultHair();
		
		foreach(string s in lstChangeAvatar)
		{
			string[] op	=	s.Split(';');
			changeAvatar(op[0],op[1],op[2]);
		}
		
		for(int k = 0; k < newNode.transform.childCount; ++k)
		{
			Transform childNode = newNode.transform.GetChild(k);
			if(childNode.name.Equals("Bip01"))
			{
				//Debug.Log("Has a Bip01 Node!");
				boneRoot = childNode.gameObject;
				break;
			}
		}
		
		// 关闭自投影(注意,只遍历了RenderNode下的第一层)aa
		for(int k = 0; k < newNode.transform.childCount; ++k)
		{
			Transform kChildTransform = newNode.transform.GetChild(k);
			GameObject kChildObject = kChildTransform.gameObject;

			SkinnedMeshRenderer kSkinnedMeshRenderer = kChildObject.GetComponent<SkinnedMeshRenderer>();
			if (kSkinnedMeshRenderer != null)
				kSkinnedMeshRenderer.receiveShadows = false;
			
			MeshRenderer kStaticMeshRenderer = kChildObject.GetComponent<MeshRenderer>();
			if (kStaticMeshRenderer != null)
				kStaticMeshRenderer.receiveShadows = false;
		}
		
		LoadShadow();
		
	}

	// 处理头发着色aa
	protected override void NotifySkinnedMeshChanged(SkinnedMeshRenderer kSkinnedMeshRenderer)
	{
		if (kSkinnedMeshRenderer == null)
			return;
			
		if (!kSkinnedMeshRenderer.name.Contains("face"))
			return;
		
		foreach (Material kMaterial in kSkinnedMeshRenderer.materials)
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
	
	// 加载动画aa
	protected void	LoadAnimationFile(int job)
	{
		Hashtable jobAnimFile =	sdConfDataMgr.Instance().GetTable("animation");
		if(jobAnimFile!=null)
		{
			if(jobAnimFile.Contains(job.ToString()))
			{
				Hashtable	animations	=	jobAnimFile[job.ToString()] as Hashtable;
				foreach(DictionaryEntry de in animations)
				{
					string	name	=	de.Key	as string;
					string	str		=	de.Value as string;
					if(str.Length==0)
					{
						continue;
					}
					if(name=="JobId")
					{
						continue;
					}
				//	Debug.Log("Load Animation File="+str);
					loadAnimation(str);
					
				}
			}
		}
	}
	
	// 技能动画表aa
	protected void CreateSkill_Action(int job)
	{
		// 被动Action表aa
		Hashtable passive =	sdConfDataMgr.Instance().GetTable("passiveaction");
		foreach(DictionaryEntry de1 in passive)
		{
			int id					=	int.Parse(de1.Key as string);
			Hashtable	animation	=	de1.Value as Hashtable;
			sdBaseState passiveState	=	new sdBaseState();
			passiveState.id			=	id;
			passiveState.info		=	animation[job.ToString()] as string;
			passiveState.name		=	animation["strName"] as string;
			passiveState.bPassive	=	true;
			
			logicTSM.AddActionState(passiveState);	
		}
		
		// 主动Action表aa
		skillTree = sdSkillTree.createSkillTree(this,job);	//< 技能表aa
		
		Hashtable lstAction = sdConfDataMgr.Instance().m_vecJobSkillAction[job];
		if (lstAction != null)
		{
			skillIDRange range = sdConfDataMgr.Instance().m_JobSkillRange[job];
			for(int index = range.min; index <= range.max; ++index)
			{
				foreach(DictionaryEntry de in lstAction)
				{
					int id	=	(int)de.Key;
					if(id == index)
					{
						Hashtable	action	=	(Hashtable)de.Value;
						int pid	=	(int)action["ParentID"];
						int skillid	=	pid/100;
						sdSkill s = skillTree.getSkill(skillid);
						if(s==null)
						{
							continue;
						}
						sdBaseState state	=	s.AddAction(id,action);
						logicTSM.AddActionState(state);
						break;
					}
				}
			}
		}
		
		sdTransitGraph.loadAndLinkActionState(this,job);	
	}

	void	OnRequestEnd(ResLoadParams param,UnityEngine.Object obj)
	{
        //SetVisiable_WhenLoading(true);
        Debug.Log("LoadFinished!");
        OnAllLoadFinished();
	}
    protected virtual void OnAllLoadFinished()
    { 
    
    }

	// 从装备属性表初始化装备aa
	public void SetItemInfo(Hashtable table)
	{
		if (table != null)
		{
			foreach (DictionaryEntry entry in table)
			{
				UInt64 itemID = UInt64.Parse(entry.Key.ToString());
				sdGameItem item = entry.Value as sdGameItem;
				item.takeOn(this);
			}
		}
	}

	// 从技能属性表初始化技能aa
	public void SetSkillInfo(Hashtable table)
	{
		if (table != null)
		{
			foreach (DictionaryEntry entry in table)
			{
				int skillID = (int)entry.Key;
				sdSkill skill = skillTree.getSkill(skillID / 100);
				if (skill != null)
				{
					skill.SetInfo((sdGameSkill)entry.Value, this);
				}
			}
			skillTree.skillPoint = sdGameSkillMgr.Instance.GetSkillPoint();
		}
	}
    public void RequestEnd()
    {
        BundleGlobal.Instance.LoadNull(OnRequestEnd);
    }
}