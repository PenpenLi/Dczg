using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 场景怪物逻辑叶节点aa
/// </summary>
public class sdLevelArea : sdBaseTrigger 
{
	// 节点IDaa
	public int id =	0;

	// 区域父节点aa
	protected GameObject mParNode = null;
	
	// 区域所属子节点列表aa
	protected List<GameObject> mSubNodeList	= new List<GameObject>();
	protected List<GameObject> mSubNodeClearedList = new List<GameObject>();

	// 区域所属怪物列表aa
	protected List<GameObject> mMonsterList = new List<GameObject>();
	protected List<GameObject> mMonsterKilledList = new List<GameObject>();

    //延迟触发leaveTrigger
    public bool bDelayLeaveTrigger = false;
    //延迟触发时间aa
    float fDelayTime = 3.0f;
    //
    bool  bCanLeave = false;

	// 初始化(继承自MonoBehaviour)aa
	protected override void Start()
	{
		if (this.transform.childCount > 0)
		{
			for (int i = 0; i < this.transform.childCount; i++)
			{
				GameObject kObject = this.transform.GetChild(i).gameObject;
				sdGameMonster kMonster = kObject.GetComponent<sdGameMonster>();
				if (kMonster != null)
				{
					kMonster.NotifyKilled += NotifyMonsterKilled;
					kMonster.NotifyHurt += NotifyMonsterHurt;
					kMonster.NotifyChangeTarget += NotifyMonsterFindFirstTarget;

					mMonsterKilledList.Add(kObject);
					continue;
				}

				sdLevelArea	kArea = kObject.GetComponent<sdLevelArea>();
				if (kArea!=null)
				{
					mSubNodeClearedList.Add(kObject);
					continue;
				}
			}
		}

		if (this.transform.parent != null)
		{
			sdLevelArea kArea = this.transform.parent.gameObject.GetComponent<sdLevelArea>();
			if (kArea != null)
			{
				mParNode = this.transform.parent.gameObject;
			}
		}
	}

	// Trigger被触发(继承自sdTriggerReceiver)aa
	public override void OnTriggerHitted(GameObject kObject, int[] kParam)
	{
		base.OnTriggerHitted(kObject, kParam);

		WhenEnterTrigger(kObject, kParam);

		if (kParam[3] == 0)
		{
			foreach (GameObject kMonsterObject in mMonsterKilledList)
			{
				sdGameMonster kMonster = kMonsterObject.GetComponent<sdGameMonster>();
				if (kMonster != null)
				{
					if (!kMonster.IsActive())
					{
						if (kMonster.GetCurrentHP() <= 0)
						{
							kMonster.gameObject.transform.position = kMonster.FirstSummonedPosition;
							kMonster.gameObject.transform.rotation = kMonster.FirstSummonedRotation;
							kMonster.SetCurrentHP(0);
							kMonster.AddHP(kMonster.GetMaxHP());
						}

						kMonster.OnTriggerHitted(kObject, kParam);

						if (kMonster.IsActive() && kMonster.GetCurrentHP() > 0)
							mMonsterList.Add(kMonsterObject);
					}
				}
			}

			foreach (GameObject kMonsterObject in mMonsterList)
			{
				sdGameMonster kMonster = kMonsterObject.GetComponent<sdGameMonster>();
				if (kMonster != null)
				{
					mMonsterKilledList.Remove(kMonsterObject);
				}
			}

			if (mMonsterList.Count == 0 && mSubNodeList.Count == 0)
			{
				WhenLeaveTrigger(kObject, kParam);
			}
		}
		else if (kParam[3] == 1)
		{
			List<GameObject> kMonsterList = new List<GameObject>(mMonsterList.ToArray());//< 防止递归修改mMonsterList对象aa
			foreach (GameObject kMonsterObject in kMonsterList)
			{
				sdGameMonster kMonster = kMonsterObject.GetComponent<sdGameMonster>();
				if (kMonster != null)
				{
					kMonster.OnTriggerHitted(kObject, kParam);
				}
			}
		}
		else
		{

		}

//		foreach (GameObject kLevalAreaObject in mSubNodeClearedList)
//		{
//			sdLevelArea kArea = kLevalAreaObject.GetComponent<sdLevelArea>();
//			if (kArea != null)
//			{
//				kArea.OnTriggerHitted(kObject, kParam);
//				mSubNodeList.Add(kLevalAreaObject);
//			}
//		}
	}
	
	// 子区域被清除通知aa
	protected void OnSubAreaCleared(GameObject kObject)
	{
		mSubNodeList.Remove(kObject);
		mSubNodeClearedList.Add(kObject);
		
		if (mMonsterList.Count == 0 && mSubNodeList.Count == 0)
		{
			WhenLeaveTrigger(this.gameObject, new int[4]{0,0,0,0});
		}
	}

	// 区域中的怪物被杀死回调aa
	protected void NotifyMonsterKilled(sdActorInterface kActor)
	{
		mMonsterList.Remove(kActor.gameObject);
		mMonsterKilledList.Add (kActor.gameObject);

		if (mMonsterList.Count ==0 && mSubNodeList.Count == 0)
		{

            if (!bDelayLeaveTrigger)
                WhenLeaveTrigger(gameObject, new int[4] { 0, 0, 0, 0 });
            else
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("pttip1"), Color.yellow);
                bCanLeave = true;
                fDelayTime = 3.0f;
            }
            if (sdGameLevel.instance.levelType == sdGameLevel.LevelType.PET_TRAIN)
            {
                sdPTManager.Instance.AddAttack();
                if (gameObject.name == "Area2")//挑战结束
                    PT_End();
            }
		}
	}


    void PT_End()
    {
        // 禁用所有的按键和手势响应aa
        sdGameLevel.instance.DestroyFingerObject();
        sdGameLevel.instance.mainChar.AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY);
        sdGameLevel.instance.mainChar.AddDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL);
        GPUSH_API.Vibrate(500);

        // 初始化胜利动画aa
        sdGameLevel.instance.mainCamera.InitVictory(null);

        // 请求结算aa
        Debug.Log("TuituEnd and Request Jiesuan!");
        sdGameLevel.instance.tuiTuLogic.ShowFightResult(null);
    }

    protected override void Update()
    {
        if (bCanLeave && bDelayLeaveTrigger)
        {
            fDelayTime -= Time.deltaTime;
            if (fDelayTime <= float.Epsilon)
            {
                WhenLeaveTrigger(gameObject, new int[4] { 0, 0, 0, 0 });
                bDelayLeaveTrigger = false;
                bCanLeave = false;
            }
        }
    }

	// 区域中的怪物被攻击事件回调aa
	protected void NotifyMonsterHurt(sdActorInterface kActor, sdActorInterface kAttacker, int iHurt)
	{
		foreach (GameObject kChild in mMonsterList)
		{
			sdGameMonster kMonster = kChild.GetComponent<sdGameMonster>();
			if (kMonster != null && kMonster != kActor)
			{
				kMonster.OnAreaGroupAlert(kAttacker);
			}
		}
	}
	
	// 区域中的怪物发现第一个攻击目标事件回调aa
	protected bool mMonsterFindFirstTarget = false;	//< 防止函数被嵌套调用aa
	protected void NotifyMonsterFindFirstTarget(sdActorInterface kActor, sdActorInterface kPreviousTarget, sdActorInterface kTarget)
	{
		if (mMonsterFindFirstTarget)
			return;

		mMonsterFindFirstTarget = true;
		if (kPreviousTarget == null && kTarget != null)
		{
			foreach (GameObject kChild in mMonsterList)
			{
				sdGameMonster kMonster = kChild.GetComponent<sdGameMonster>();
				if (kMonster != null && kMonster != kActor)
				{
					kMonster.OnAreaGroupAlert(kTarget);
				}
			}
		}
		mMonsterFindFirstTarget = false;
	}

	// 进入触发器(继承自sdBaseTrigger)aa
	protected override void WhenEnterTrigger(GameObject kObject, int[] kParam)
	{
		base.WhenEnterTrigger (kObject, kParam);

		if (OnceLife)
		{
			live = false;
		}
		
		for (int i = 0; i < enterReceivers.Length; i++)
		{
			if (enterReceivers[i] != null)
			{
				enterReceivers[i].OnTriggerHitted(this.gameObject, iEnterParams[i].v);
			}
		}
	}

	// 离开触发器(继承自sdBaseTrigger)aa
	protected override void WhenLeaveTrigger(GameObject kObject, int[] kParam)
	{
		base.WhenLeaveTrigger(kObject, kParam);
		
		if (mParNode != null)
		{
			sdLevelArea kArea = mParNode.GetComponent<sdLevelArea>();
			if (kArea != null)
			{
				kArea.OnSubAreaCleared(this.gameObject);
			}
		}
		
		for (int i = 0; i < leaveReceivers.Length; i++)
		{
			if (leaveReceivers[i] != null)
			{
				leaveReceivers[i].OnTriggerHitted(this.gameObject, iLeaveParams[i].v);
			}
		}
	}

	//
	public void SetMonsterStatus(HeaderProto.ECreatureActionState[] state, bool bAdd)
	{
		for(int i = 0;i < mMonsterList.Count;i++)
		{
			GameObject obj = mMonsterList[i];
			sdGameMonster monster = obj.GetComponent<sdGameMonster>();
			if(monster != null)
			{
				for(int j=0; j<state.Length;j++)
				{
					if(bAdd)
						monster.AddDebuffState(state[j]);
					else
						monster.RemoveDebuffState(state[j]);
				}
			}
		}
	}
}
