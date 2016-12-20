using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 服务器发过来的位置同步包
/// </summary>
public class sdSynMovePacket : object
{
	public bool mIsEnd = false;			//< 表明这是一个终止包,角色将停止移动aa
	public float mPosX = 0.0f;			//< 角色位置aa
	public float mPosY = 0.0f;			//< 
	public float mPosZ = 0.0f;			//< 
	public float mOrientation = 0.0f;	//< 角色朝向aa
	public float mSendTime = 0.0f;		//< 数据包发送时间(对方客户端)aa
	public float mRecvTime = 0.0f;		//< 数据包接收时间aa
}

/// <summary>
/// 其他玩家角色对象(用于主城移动同步)aa
/// </summary>
public class sdOtherChar : sdGameActor_Impl
{
	// 服务器发过来的位置同步包aa
	protected List<sdSynMovePacket> mSynPackets = new List<sdSynMovePacket>();
	float moveSpeed	=	5.0f;

	// 移动同步处理aa
	protected sdSynMovePacket mCurSynPacket = null;				//< 当前所处理的移动同步包aa
	
	protected float mMinDistance = 0.1f;						//< 最小移动距离aa
	protected List<Vector3> mWalkPath = new List<Vector3>();	//< 当前路径aa

	// 当前角色是否仅仅进行位移同步aa
	protected bool mOnlyUsedAsMoveSync = true;


    public NotifyDisappear notifyDisappear;

    public void NotifyDisappearEvent()
    {
        if (notifyDisappear != null)
            notifyDisappear();
    }

	
//	// 移动方向aa
//	protected Vector3 mMoveVector = Vector3.zero;
//	public Vector3 MoveVector 
//	{
//		get 
//		{
//			return mMoveVector;
//		}
//		set	
//		{
//			mMoveVector = value; 
//			mMoveVector.y = 0.0f; 
//			mMoveVector.Normalize();
//		}
//	}

	// 动画aa
	protected string kIdleAnim = null;
	protected string kWalkAnim = null;
	
	// 初始化aa
	public new bool init(sdGameActorCreateInfo kInfo)
	{
		base.init(kInfo);

		// 设置角色初始朝向aa
		mFaceDirection = transform.rotation * Vector3.forward;

		// 加载角色动画aa
		LoadSimpleAnimationFile(kInfo.mJob);

		return true;
	}

	// 更新(继承自sdBaseTrigger)
	protected override void Update()
	{
		base.Update();

		tickFrame();
	}
	
	// 每帧更新aa
	public void tickFrame()
	{
		// 更新位移aa	
		tickMove();			
	}
	
	// 更新位移aa
	private void tickMove()
	{	
		/*
		if (mSynPackets.Count > 0)
		{		
			mCurSynPacket = mSynPackets[0];
			mSynPackets.RemoveAt(0);
				
			Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
			this.gameObject.transform.position = kRemotePos;
			mCurSynPacket = null;	
		}					
		*/
		
		
		if (mSynPackets.Count > 0)
		{		
			mCurSynPacket = mSynPackets[0];
			mSynPackets.RemoveAt(0);
			
			Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
			if (mNavAgent != null)
			{	
				mNavAgent.speed = moveSpeed;
				mNavAgent.SetDestination(kRemotePos);
			}
		}
		else
			mCurSynPacket = null;
			
		/*
		if (mCurSynPacket == null)
		{
			if (mSynPackets.Count > 0)
			{		
				mCurSynPacket = mSynPackets[0];
				mSynPackets.RemoveAt(0);
				
				Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
				if (mNavAgent != null)
				{	
					mNavAgent.speed = this.moveSpeed;
					mNavAgent.SetDestination(kRemotePos);
				}
			}	
		}
		else
		{
			float fMinSqlDis = 0.5f * 0.5f;
			Vector3 kLocalPos = this.gameObject.transform.position;
			Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
			Vector3 kDeltaPos = kRemotePos - kLocalPos;
			float fSqlDis = kDeltaPos.x * kDeltaPos.x + kDeltaPos.z * kDeltaPos.z;	//< 只检测.xz方向
			if (fSqlDis < fMinSqlDis)
			{
				mCurSynPacket = null;
				
				if (mSynPackets.Count > 0)
				{		
					mCurSynPacket = mSynPackets[0];
					mSynPackets.RemoveAt(0);
						
					if (mNavAgent != null)
					{	
						mNavAgent.speed = this.moveSpeed;
						mNavAgent.SetDestination(kRemotePos);			
					}
				}
			}
			else
			{
				
			}
		}
		*/
		/*
		if (mCurSynPacket == null)
		{
			if (mSynPackets.Count > 0)
			{		
				mCurSynPacket = mSynPackets[0];
				mSynPackets.RemoveAt(0);
				
				MoveBegin();
			}	
		}
		*/
		// 控制动画播放aa
		{
			string kAction = kIdleAnim;
			NavMeshAgent kNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
			if (kNavMeshAgent != null && kNavMeshAgent.velocity.sqrMagnitude > 0.01f)
			{
				kAction = kWalkAnim;
			}
	
			if (mAnimController != null && !mAnimController.IsPlaying(kAction))
			{
				AnimationState aniState = mAnimController[kAction];
				if (aniState != null)
				{
					mAnimController.CrossFade(kAction, 0.15f);
				}
			}
		}
	}
	
	// 从装备列表更新装备aa
	public void updateAvatar(uint [] auiEquipID, uint uiCount)
	{
		if (itemInfo == null)
			return;
	
		List<sdGameItem> akItemRemain = new List<sdGameItem>();	//< 需要保留的装备aa
		List<sdGameItem> akItemAdd = new List<sdGameItem>();	//< 需要添加的装备aa
		
		// 从新装备列表找出需要保留的装备和需要新添加的装备aa
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
				item.equipPos		=	int.Parse(hashItem["Character"].ToString());
				if (item.equipPos == 0)
				{
					item.anchorNodeName = "dummy_right_weapon_at";
				}
				else if(item.equipPos == 9)
				{
					item.anchorNodeName = "dummy_left_shield_at";
				}
				item.itemClass = int.Parse(hashItem["Class"].ToString());
				item.subClass = int.Parse(hashItem["SubClass"].ToString());
			}
		}
	
		// 从原装备列表找出需要被卸载的装备,并区分是否是默认装备或是否将要换上新装备aa
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
				continue;	//< 该装备被保留aa
			
			bool bHasNewEquip = false; 
			foreach (sdGameItem itemEntry in akItemAdd)
			{
				if (itemEntry.equipPos == item.equipPos)
				{
					bHasNewEquip = true;
					break;
				}
			}
			
			if (bHasNewEquip)
				continue;	//< 该装备位有新装备被换上aa
				
			sdGameItem dummyItem = getStartItem(item.equipPos);
			if (dummyItem != null)
			{
				if (dummyItem.templateID == item.templateID)
					akItemRemain.Add(item);		//< 该装备是默认装备,不需要被替换aa
				else
					akItemAdd.Add(dummyItem);	//< 该装备被卸载,需要换上默认装备aa
			}
		}
		
		// 清空装备表aa
		itemInfo.Clear();
		
		// 保留的装备加入装备表aa
		foreach (sdGameItem itemEntry in akItemRemain)
		{
			itemInfo[itemEntry.templateID] = itemEntry;
		}
		
		// 新添加的装备(移除的装备换上默认装备)加入装备表并换装aa
		foreach (sdGameItem itemEntry in akItemAdd)
		{
			itemInfo[itemEntry.templateID] = itemEntry;
			if(itemEntry.mdlPartName.Length > 0)
				changeAvatar(itemEntry.mdlPath, itemEntry.mdlPartName, itemEntry.anchorNodeName);
		}
	}
	
	// 添加位置同步包aa
	public void PushSynPacket(sdSynMovePacket kPacket)
	{
		mSynPackets.Add(kPacket);	
	}
	
	// 仅仅加载Idle和Run的动画文件aa
	protected void LoadSimpleAnimationFile(int iJob)
	{
		switch (mJob)
		{
			case 1:
			case 2:
			case 3:
				{
					loadAnimation("Model/Mdl_mainChar_0/warrior_action/$warrior_idle_01.FBX");
					loadAnimation("Model/Mdl_mainChar_0/warrior_action/$warrior_run_01.FBX");

					kIdleAnim = "warrior_idle_01";
					kWalkAnim = "warrior_run_01";
				} break;
			case 4:
			case 5:
			case 6:
				{
					loadAnimation("Model/Mdl_mainChar_1/mage_action/$mage_idle_04.FBX");
					loadAnimation("Model/Mdl_mainChar_1/mage_action/$mage_run_01.FBX");

					kIdleAnim = "$mage_idle_04";
					kWalkAnim = "$mage_run_01"; 
				} break;
			case 7:
			case 8:
			case 9:
				{
					loadAnimation("Model/Mdl_mainChar_0/ranger_action/$ranger_idle_03.FBX");
					loadAnimation("Model/Mdl_mainChar_0/ranger_action/$ranger_run_04.FBX");

					kIdleAnim = "$ranger_idle_03";
					kWalkAnim = "$ranger_run_04";	
				} break;
			case 10:
			case 11:
			case 12:
				{
					loadAnimation("Model/Mdl_mainChar_1/cleric_action/$cleric_idle_04.FBX");
					loadAnimation("Model/Mdl_mainChar_1/cleric_action/$cleric_run_01.FBX");

					kIdleAnim = "$cleric_idle_04";
					kWalkAnim = "$cleric_run_01"; 
				} break;
			default:
				{
				} break;
		}
	}
//	//
//    protected void MoveBegin()
//    {
//        if (mCurSynPacket == null)
//            return;
//		
//        mWalkPath.Clear();
//        MoveVector = Vector3.zero;
//					
//        Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
//        if (mNavAgent != null)
//        {			
//            NavMeshPath kNavPath = new NavMeshPath();
//            if(mNavAgent.CalculatePath(kRemotePos, kNavPath))
//            {
//                for(int i = 1 ; i < kNavPath.corners.Length; i++)
//                {
//                    mWalkPath.Add(kNavPath.corners[i]);
//                }
//            }
//        }
//    }
//	
//    protected void MoveEnd()
//    {
//        if (mCurSynPacket == null)
//            return;
//		
//        mWalkPath.Clear();
//        MoveVector = Vector3.zero;
//    }
//	
//    protected void MoveUpdate()
//    {
//        if (mCurSynPacket == null)
//            return;
//		
//        Vector3 kLocalPos = this.gameObject.transform.position;
//        Vector3 kRemotePos = new Vector3(mCurSynPacket.mPosX, mCurSynPacket.mPosY, mCurSynPacket.mPosZ);
//        if (Vector3.Distance(kLocalPos, kRemotePos) < mMinDistance)
//        {
//            // 停止移动aa
//        }
//		
//        if (mWalkPath.Count > 0)
//        {
//            Vector3 kOffset	= mWalkPath[0] - kLocalPos;
//            float fSqlDis = kOffset.x * kOffset.x + kOffset.z * kOffset.z;	//< 只检测.xz方向
//            float fMinSqlDis = mMinDistance * mMinDistance;
//            if (fSqlDis < fMinSqlDis)
//            {
//                mWalkPath.RemoveAt(0);
//            }
//			
//            if (mWalkPath.Count > 0)
//            {
//                Vector3 kDir = mWalkPath[0] - kLocalPos;
//                kDir.y = 0.0f;
//                kDir.Normalize();
//				
//                MoveVector = kDir;
//            }
//            else
//            {
//                // 停止移动aa
//				
//            }
//        }
//    }
//	
//    // 旋转到目标方向aa
//    protected void SpinToTargetDirection(Vector3 kTargetDirection)
//    {
//        if (renderNode == null)
//            return;
//		
//        if (Mathf.Abs(kTargetDirection.x) > 0.1f || Mathf.Abs(kTargetDirection.y) > 0.1f || Mathf.Abs(kTargetDirection.z) > 0.1f)
//        {
//            float fDot = Vector3.Dot(faceDirection, kTargetDirection);
//            if (fDot > 0.9999f)
//                return;
//			
//            Quaternion kTargetQuat;
//            if(Vector3.Dot(Vector3.forward, kTargetDirection) < -0.9999f)
//                kTargetQuat = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));	
//            else
//                kTargetQuat = Quaternion.FromToRotation(Vector3.forward, kTargetDirection);	
//			
//            faceDirection = kTargetQuat * Vector3.forward;
//            this.gameObject.transform.rotation = kTargetQuat;
//        }
//    }
}

