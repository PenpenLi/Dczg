using UnityEngine;
using System.Collections;

/// <summary>
/// 被动状态和连招的切换
/// </summary>
public class sdTSM 
{
	// 角色aa
	public sdGameActor _gameActor = null;

	// 当前技能aa
	public sdSkill _currentSkill = null;

	// 当前状态aa
	public sdBaseState statePointer = null;

	// 下一个技能连招状态aa
    private sdBaseState __nextSwitchState = null;
	public sdBaseState nextState
    {
        get
        {
            return __nextSwitchState;
        }
        set
        {
            if (__nextSwitchState != null)
            {
                __nextSwitchState.CallStateEvent(eStateEventType.eSET_leave);   
            }
            __nextSwitchState   =   value;
        }
    }
	
	// 被动状态aa
	public	sdBaseState	idle	=	null;
	public	sdBaseState	run		=	null;
	public	sdBaseState	die		=	null;
	public	sdBaseState	stun	=	null;
	
	// 所有状态列表aa
	public Hashtable states = new Hashtable();
	
	// 释放技能aa
	public bool OnCastSkill(sdSkill s)
	{
		if(statePointer==die)
		{
			return false;
		}

		if(statePointer==null)
		{
			return false;
		}
        if (!sdGameLevel.instance.AutoMode && _gameActor == sdGameLevel.instance.mainChar)
            JoyStickCastSkill(s);
		if(statePointer.bPassive)
		{
			SwitchSkill(_gameActor,s);
			sdBaseState	state	=	s.GetFirstValidAction();
			SwitchToState(_gameActor,state);
			return (state != null);
		}
		else
		{
			return statePointer.OnCastSkill(_gameActor,s);
		}
	}

	// 摇杆模式下施放技能时辅助调整方向aa
    protected void JoyStickCastSkill(sdSkill s)
    {
		// 翻滚技能不使用辅助施放aaa
		if (s.id == 1002)
			return;

        // 自动选取朝向上的怪物aaaa
        HeaderProto.ESkillObjType objType = 0;
		if (statePointer.bPassive)
		{
			sdBaseState state = s.GetFirstValidAction();
			objType = (HeaderProto.ESkillObjType)state.stateData["byTargetType"];
		}
		else
		{
			objType = (HeaderProto.ESkillObjType)statePointer.stateData["byTargetType"];
		}

        float xbias = 0.0f;
        float ybias = 0.0f;
        sdGameLevel.instance.mainCharMoveControl(out xbias, out ybias);

        sdActorInterface actor = null;
        bool bTurnDir = false;

        // 释放技能前，检查下方向键方向范围内是否有目标，如果有就转向释放技能，没有就不转向aaa
        if (Mathf.Abs(xbias) > 0.1f || Mathf.Abs(ybias) > 0.1f)
        {
            Vector3 worldDir = Vector3.right * xbias + Vector3.forward * ybias;
            worldDir.y = 0.0f;
            worldDir.Normalize();
            actor = sdGameLevel.instance.actorMgr.FindNearestAngle(_gameActor, objType, worldDir, 5.0f, 80.0f);
            if (actor != null)
            {
                //_gameActor._moveVector = worldDir;
                _gameActor.TargetFaceDirection = worldDir;
                bTurnDir = true;
            }
        }

        // 在角色当前方向上寻找最小夹角的目标aaa
        if (!bTurnDir)
        {
			actor = sdGameLevel.instance.actorMgr.FindNearestAngle(_gameActor, objType, _gameActor.GetDirection(), 5.0f, 180.0f);
			if (actor != null)
            {
				Vector3 dir = actor.transform.position - _gameActor.transform.position;
				dir.y = 0;
				dir.Normalize();
				_gameActor.TargetFaceDirection = dir;
			}
        }
    }

	// 获取当前被动状态aa
	public sdBaseState GetCurrentPassiveState()
	{
		if (_gameActor.GetCurrentHP() <= 0)
			return die;

		if (_gameActor._moveVector.sqrMagnitude > 0.001f)
			return run;

		return idle;
	}

	//
	public bool IsAnimLoop()
	{
		if(_gameActor.AnimController == null)
		{
			return false;
		}
		if(statePointer==null)
		{
			return false;		
		}
		AnimationState s =	statePointer.aniState;
		if(s ==	null)
		{
			string animationName = statePointer.info;
			s = _gameActor.AnimController[animationName];
		}
		if(s==null)
		{
			return false;
		}
			
		if(s.wrapMode	==	WrapMode.Loop)
		{
			return true;
		}
		return false;
	}

	//
	public bool IsAnimPlayOver()
	{
		if(_gameActor.AnimController == null)
		{
			return true;
		}
		AnimationState s =	statePointer.aniState;
		if(s ==	null)
		{
			string animationName = statePointer.info;
			s = _gameActor.AnimController[animationName];
		}
		if(s==null)
		{
			return true;
		}
			
		if(s.wrapMode	==	WrapMode.Loop)
		{
			return false;
		}
		if(statePointer.aniState == null)
			return true;
        if (!_gameActor.AnimController.IsPlaying(statePointer.info))
		{								
			return true;
		}
			
		return false;
	}

	// 更新aa
	public void UpdateSelf()
	{
		if(statePointer!=null)
		{
			statePointer.Update(_gameActor);
			if(_gameActor.GetCurrentHP()<=0)
			{
				nextState	=	null;
				if(statePointer!=die)
				{
					SwitchSkill(_gameActor,null);
					SwitchToState(_gameActor,die);
				}
				return;
			}
			else if(_gameActor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN) 
				|| _gameActor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD))
			{
				nextState	=	null;
				sdBaseState switchState	=	stun;
				if(stun==null)
				{
					switchState	=	idle;
				}
				if(statePointer!=switchState)
				{
					SwitchSkill(_gameActor,null);
					SwitchToState(_gameActor,switchState);
				}
				return;
			}
            else if (_gameActor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_IDLE))
            {
                nextState = null;
                sdBaseState switchState = idle;
                if (idle == null)
                {
                    switchState = idle;
                }
                if (statePointer != switchState)
                {
                    SwitchSkill(_gameActor, null);
                    SwitchToState(_gameActor, switchState);
                }
                return;
            }
				
			if(!statePointer.bPassive)
			{
				if(IsAnimLoop())
				{
					//判断是否死亡..
					if(GetCurrentPassiveState()==die)
					{
						nextState=null;
						SwitchToState(_gameActor,die);
						return;
					}
					sdBaseState	next	=	nextState;
					nextState=null;
					if(next!=null && next!=statePointer)
					{
						SwitchSkill(_gameActor,next.belongToSkill);
						SwitchToState(_gameActor,next);
					}
				}
				else if(IsAnimPlayOver())
				{
					sdBaseState	next	=	nextState;
					if(next==null)
					{
						next	=	GetCurrentPassiveState();
					}
					nextState=null;
					
					SwitchSkill(_gameActor,next.belongToSkill);
					SwitchToState(_gameActor,next);					
				}
				else
				{
					if(nextState!=null)
					{
						if(nextState.belongToSkill.id==1002 && statePointer.bAllHitPointDone)
						{
							sdBaseState	next	=	nextState;
							nextState=null;
							SwitchSkill(_gameActor,next.belongToSkill);
							SwitchToState(_gameActor,next);	
						}
					}
				}
			}
			else
			{
				sdBaseState	next	=	nextState;
				if(next==null)
				{
					next	=	GetCurrentPassiveState();
				}
				nextState=null;
				if(statePointer!=next)
				{
					SwitchSkill(_gameActor,next.belongToSkill);
					SwitchToState(_gameActor,next);
					
				}
			}
		}
		else
		{
			if(_gameActor.AnimController==null)
			{
				return;
			}
			sdBaseState	next	=	nextState;
			if(next==null)
			{
				next	=	GetCurrentPassiveState();
			}
			if(!next.IsAnimValid(_gameActor))
			{
				return;
			}
			nextState=null;
			
			SwitchToState(_gameActor,next);	
		}
	}

	// 直接切换状态aa
	protected void SwitchToState(sdGameActor _gameActor, sdBaseState destState)
	{
		//if(CheckMP(destState))
		{
			if (statePointer != null)
				statePointer.Leave(_gameActor);

			statePointer = destState;

			if (statePointer != null)
				statePointer.Enter(_gameActor);
		}
	}

	// 切换技能aa
	protected void SwitchSkill(sdGameActor _gameActor, sdSkill s)
	{
		if (_currentSkill!=s)
		{
			if (_currentSkill != null)
			{
				if(_gameActor.actionStateTimer.targetSkill != _currentSkill)
				{
					_currentSkill.leave(_gameActor);
				}
			}
			
			_currentSkill = s;

			if (s != null)
				s.enter(_gameActor);	
		}
	}

	// 强制终止当前状态aa
	public void ForceStop()
	{
		sdBaseState	next = GetCurrentPassiveState();
					
		nextState = null;
		if (statePointer != next)
		{
			SwitchSkill(_gameActor, next.belongToSkill);
			SwitchToState(_gameActor, next);	
		}
	}

	// 添加状态aa
	public sdBaseState AddActionState(string str)
	{
		sdBaseState state = (sdBaseState)null;//CreateObject(str);
		AddActionState(state);
		return state;
	}

	// 添加状态aa
	public void AddActionState(sdBaseState state)
	{
		if (state != null)
		{
			states[state.id] = state;
			switch(state.id)
			{
				case 100:{
					die		=	state;
				}break;
				case 101:{
					idle	=	state;
				}break;
				case 102:{}break;
				case 103:{
					run		=	state;
					run.bMoveState	=	true;
				}break;
				case 104:{
					stun	=	state;
				}break;	
			}
		}
	}
}
