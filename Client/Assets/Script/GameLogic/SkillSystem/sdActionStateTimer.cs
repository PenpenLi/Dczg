using UnityEngine;
using System.Collections;

/// <summary>
/// 检查连招系统的有效性aa
/// </summary>
public class sdActionStateTimer : object
{
	public 	sdSkill 	targetSkill	= 	null;	//< init by skill
	public	sdBaseState	state		=	null;

	// 时钟时长aaaa
	public float _interval	= 0.0f;

	// 按键有效时间aaaaa
	public float _triggerTime = 0.0f;
	
	// 中间变量aaa
	public bool _enabled 	= false;
	public float t 			= 0.0f;

	public void begin( sdGameActor _gameActor,sdBaseState action,float interval_, float triggerTime_)
	{
		if(_enabled)
		{
			if(targetSkill!=action.belongToSkill)
			{
				end (_gameActor);
			}
		}
		state			=	action;
		targetSkill		=	state.belongToSkill;
		_interval 		= interval_;
		_triggerTime 	= triggerTime_;
		begin();
	}

	// 启动时钟aaaa
	void begin()
	{		
		_enabled = true;
		t 		 = 0.0f;
	}
	
	public void end(sdGameActor _gameActor)
	{
		_enabled 		= false;
		t 				= 0.0f;
		if(targetSkill!=null)
		{
			targetSkill.leave(_gameActor);
		}
		state	 = null;
		targetSkill=null;
	}	

	// 更新时钟aaaaa
	public void tick(sdGameActor _gameActor)
	{
		if(_enabled)
		{
			if(t >= _interval)
			{
				end(_gameActor);	
			}
			t += Time.deltaTime;
		}
	}

	public	bool	OnCastSkill(sdGameActor actor,sdSkill skill)
	{
		if(_enabled)
		{
			return state.OnCastSkill(actor,skill);
		}
		return false;
	}
}
