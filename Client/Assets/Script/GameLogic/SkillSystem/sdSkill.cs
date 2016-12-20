using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 技能冷却回调aa
public delegate void NotifySkillCooldownStartDelegate(sdSkill kSkill);
public delegate void NotifySkillCooldownEndDelegate(sdSkill kSkill);

/// <summary>
/// 技能信息aa
/// </summary>
public class sdSkill : object
{
	public	enum State
	{
		eSS_PreCoolDown,
		eSS_CoolDown,
		eSS_OK,
	}
	public 	int id 				= -1;
	public	int skillLevel		=	0;
	public string skillName 	= "invalid";
	public string skillEnName 	= "unamed";
	public bool isPassive 		= false;
	public int learnPoints 		= 0;
	
	public int spellingIndex 	= -1;
	public List<sdBaseState> actionStateList = new List<sdBaseState>();
	
	public int cooldown
	{
		get
		{
			return (int)skillProperty["dwCooldown"];
		}
		set
		{
			skillProperty["dwCooldown"]=value;
		}
	}
	public bool skillEnabled 	= true;
	public int skillState 	= (int)State.eSS_OK;
	
	//< from zym
	public sdGameSkill skillInfo 		= null;
	public	Hashtable	skillProperty	=	null;
	
	// 技能冷却时间aa
	protected float ct = 0.0f;
    public bool bUnLimitedAction = false;
//	// 技能冷却事件aa
//	public NotifySkillCooldownStartDelegate NotifySkillCooldownStart;
//	public NotifySkillCooldownEndDelegate NotifySkillCooldownEnd;

	public sdSkill(string name, bool passive, int id_)
	{
		skillName = name;
		isPassive = passive;
		id = id_;
	}
	
	// 手动设置CD时间aa
	public void Setct(float time)
	{
		ct = time;
	}
	
	// 获取剩余CD时间aa
	public int GetCD()
	{
		int time = cooldown - (int)(ct*1000.0f);
		return time>0?time:0;
	}
	
	public bool	CheckSP(sdGameActor _gameActor)
	{
		if(actionStateList.Count == 0)
			return true;
		if(actionStateList[0]!=null)
		{
			int iSP	=	actionStateList[0].GetCostSP();
			if(iSP==0)
			{
				return true;
			}
			int iCurrentSP	=	_gameActor["SP"];
			if(iSP <= iCurrentSP)
			{
				return true;
			}
			return false;
		}
		return true;
	}

	// 初始化技能信息aa
	public virtual void SetInfo(sdGameSkill info, sdGameActor kGameActor)
	{
		skillInfo = info;

		if (skillInfo != null)
		{
			cooldown = (int)skillProperty["dwCooldown"];
            bUnLimitedAction = ((int)skillProperty["UnLimited"]) != 0;
		}

		if (isPassive)
		{
			if (skillProperty != null)
			{
				string level = skillProperty["byLevel"] as string;
				if (level != null)
				{
					skillLevel = int.Parse(level);
				}

				string str = skillProperty["Buffid"] as string;
				if (str != null)
				{
					int id = int.Parse(str);
					if (id != 0)
					{
						//调用AddBuff会崩溃  暂时注释掉...
						kGameActor.AddBuff(id, 0, kGameActor);
					}
				}
			}
		}
	} 
	
	public virtual bool assembleActionStates(sdGameActor _gameActor)
	{
		
		return true;
	}
	//call it after all skill be add in skilltree
	public	virtual	void	PostInit(sdGameActor _gameActor)
	{
		/*
		foreach(sdBaseState s in actionStateList)
		{
			if(s.timer!=null)
			{
				s.timer.targetSkill	=	this;
				s.timer.gameActor	=	_gameActor;
				s.timer.init();
				
			}
		}
		*/
	}
	
	public virtual bool enter(sdGameActor _gameActor)
	{		
		// 开始冷却aa
		skillState = (int)State.eSS_PreCoolDown;
		ct = 0.0f;
		
		// 冷却回调aa
		
		
		return true;
	}
	
	public virtual bool leave(sdGameActor _gameActor)
	{		
		skillState	=	(int)State.eSS_CoolDown;
		if(sdUICharacter.Instance != null && _gameActor == sdGameLevel.instance.mainChar)
		{
			int iSkillID	=	id*100+_gameActor.Job;
			sdUICharacter.Instance.ShowShortCutCd(iSkillID);
			sdUICharacter.Instance.SetSkillCurStage(iSkillID, 0);
		}
		return true;
	}	
	
	
	public virtual int getActionCount(sdGameActor _gameActor)
	{
		return actionStateList.Count;
	}
	
	public virtual float getActionInterval(sdGameActor _gameActor, int index)
	{
		float ft = 0.0f;
		if(_gameActor != null && index < actionStateList.Count)
		{
			sdBaseState baseState = actionStateList[index] as sdBaseState;				
			//<
			if(baseState != null && _gameActor.AnimController != null)
			{
				AnimationClip clip = _gameActor.AnimController.GetClip(baseState.info);
				if(clip != null)
					ft = clip.length;
			}
		}
		
		return ft;
	}
	
	public virtual void startActionState(sdGameActor _gameActor, int index)
	{
		spellingIndex = index;
	}
	
	public virtual void tick()
	{
		if(skillState!=(int)State.eSS_OK)
		{
			ct += Time.deltaTime;
			
			if(ct*1000.0f >= cooldown)
			{
				skillState = (int)State.eSS_OK;
				ct = 0.0f;
			}
		}
	}
	
	public	sdBaseState	AddAction(sdGameActor	actor,string str)
	{
		sdBaseState	state	=	actor.logicTSM.AddActionState(str);
		state.belongToSkill	=	this;
		actionStateList.Add(state);
		state.actionID	=	actionStateList.Count-1;
		return state;
	}
	public	sdBaseState	AddAction(int id,Hashtable actionInfo)
	{
		string	classname	=	actionInfo["ClassName"] as string;
		sdBaseState state	=	(sdBaseState)sdTransitGraph.CreateObject(classname);
		state.id	=	id;
		state.belongToSkill	=	this;
		state.bEnable		=	(int)actionInfo["Enable"]!=0;
		actionStateList.Add(state);
		state.actionID	=	actionStateList.Count-1;
		return state;
	}
	public	sdBaseState	GetFirstValidAction()
	{
		for(int i=0;i<actionStateList.Count;i++)
		{	
			if(actionStateList[i].bEnable)
			{
				return actionStateList[i];
			}
		}
		return null;
	}

	public	virtual	void	OnAttack(sdGameActor _gameActor,sdBaseState state)
	{
		
	}
	public	virtual	void	OnHitBegin(sdGameActor _gameActor,sdActorInterface monster,sdBaseState state,int index,object userdata)
	{
		
	}
	public	virtual	void	OnHitEnd(sdActorInterface _gameActor,sdActorInterface monster,sdBaseState state,int damage,int index,object userdata)
	{
		
	}
	public	virtual	void	OnActionStateEnter(sdGameActor _gameActor,sdBaseState state)
	{
		
	}
	public	virtual	void	OnActionStateLeave(sdGameActor _gameActor,sdBaseState state)
	{
		
	}
	public	int	GetValidActionCount()
	{
		int count	=	0;
		actionStateList.ForEach
		(
			delegate(sdBaseState obj) 
			{
				if(obj.bEnable)
				{
					count++;
				}
			}
		);
		return count;
	}
}
