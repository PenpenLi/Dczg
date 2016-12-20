using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class sdLightningChainState0 : sdBaseState 
{
	int 	iCount			=	0;
	int 	iMaxMonster		=	3;
	float 	fMaxTime		=	10.0f;
	float	fCurrentTime	=	0.0f;
	sdChainControl	control	=	null;
	public	sdLightningChainState0()
	{
		id = 17;
		info = "$mage_skill01_06";

		LoadPrefab("Effect/MainChar/$Mage/Fx_Lighting.prefab");
	}
	public	void	LoadPrefab(string prefab)
	{
		ResLoadParams param = new ResLoadParams();

		param.pos	=	Vector3.zero;
		param.scale	=	new Vector3(1,1,1);
		param.rot	=	Quaternion.identity;
		sdResourceMgr.Instance.LoadResource(prefab,OnLoadPrefab,param);
	}
	protected void OnLoadPrefab(ResLoadParams param,UnityEngine.Object obj)
	{
		GameObject gObj	=	GameObject.Instantiate(obj) as GameObject;
		if(gObj!=null)
		{
			control	=	gObj.GetComponent<sdChainControl>();
			if(control!=null)
			{
				control.SetPositionArray(null);
			}
		}
	}
	public	override	void	Enter(sdGameActor _gameActor)
	{
		base.Enter(_gameActor);

		iCount	=	0;
		fCurrentTime	=	0.0f;
		
		
	}
	
	public override	void Leave(sdGameActor _gameActor)
	{
		base.Leave(_gameActor);
		if(control!=null)
		{
			control.SetPositionArray(null);
		}
	}
	
	public override	void Update(sdGameActor _gameActor)
	{
		base.Update(_gameActor);
		
		
		fCurrentTime+=Time.deltaTime;
		if(fCurrentTime>fMaxTime)
		{
			_gameActor.logicTSM.nextState	=	_gameActor.logicTSM.GetCurrentPassiveState();
		}
	}
	public	override	void	OnAttack(sdActorInterface _gameActor,int index)
	{
		sdBattleSystem battleField = sdGameLevel.instance.battleSystem;
		int nCombo = battleField.DoSDAttack(
			_gameActor, 
			stateData,
			_gameActor.transform.position,
			iCount,
			this);
		
		iCount++;
        if (_gameActor == sdGameLevel.instance.mainChar && nCombo > 0)
            sdUICharacter.Instance.ShowComboWnd(true, nCombo);
	}
	static	sdActorInterface	GetNearestMonster(List<sdActorInterface> lstMonserHit,Vector3 vPos,List<sdActorInterface>	lstMonster)
	{
		Vector3 vTempPos	=	vPos;

		sdActorInterface	ret	=	null;
		float fDis	=	10000.0f;
		lstMonster.ForEach(delegate(sdActorInterface monster) {
			bool bIsInList	=	false;
			foreach(sdGameMonster obj in lstMonserHit )
			{
				if(monster == obj && !bIsInList)
				{
					bIsInList	=	true;
					vTempPos	=	obj.transform.position;
				}
			}//lstMonserHit
			if(!bIsInList)
			{
				Vector3 v	=	monster.transform.position-vTempPos;
				float f	=	v.magnitude;
				if(f	< 	fDis)
				{
					fDis	=	f;
					ret	=	monster;
				}
			}
		});
		return ret;
	}
	public	override	int	OnHit(sdActorInterface _gameActor,List<sdActorInterface>	lstMonster,int iHitPointIndex,object userdata, HeaderProto.ESkillEffect skilleffect)
	{
        int nRet = 0;
		if(lstMonster.Count==0)
		{
			sdGameActor	ga	=	(sdGameActor)_gameActor;
			//如果没有怪物,停止闪电连...
			if(ga.logicTSM.nextState==null)
			{
				ga.logicTSM.nextState	=	ga.logicTSM.GetCurrentPassiveState();
			}
			if(control!=null)
			{
				control.SetPositionArray(null);
			}
            return nRet;
		}
		List<sdActorInterface> lstMonserHit	=	new List<sdActorInterface>();


		Vector3 vPos	=	_gameActor.transform.position;
		sdActorInterface tempMonster	=	GetNearestMonster(lstMonserHit,vPos,lstMonster);
		lstMonserHit.Add(tempMonster);
		
		Vector3 v	=	lstMonserHit[0].transform.position	-	_gameActor.transform.position;
		v.y=0.0f;
		v.Normalize();
		((sdMainChar)_gameActor).spinToTargetDirection(v,true);
		
		int Count	=	1;
		for(int i=1;i<iMaxMonster;i++)
		{
			tempMonster	=	GetNearestMonster(lstMonserHit,vPos,lstMonster);
			if(tempMonster	!=null)
			{
				lstMonserHit.Add (tempMonster);
				Count++;
			}
		}
	
		
		GameObject[] vMonsterPos = new GameObject[Count];
		for(int i=0;i<Count;i++)
		{
			sdActorInterface	monster	=	lstMonserHit[i];
			vMonsterPos[i]	=	monster.gameObject;

		}
		if(control!=null)
		{
			control.SetPositionArray(vMonsterPos);
		}
		//base.OnHit(_gameActor,lstMonserHit,iHitPointIndex,userdata);
		sdTuiTuLogic	tuiTuLogic	=	sdGameLevel.instance.battleSystem.tuiTuLogic;
        List<Bubble.BubbleType> lstBubbleType = new List<Bubble.BubbleType>();
		for(int i=0;i<lstMonserHit.Count;i++) 
		{
			sdActorInterface monster	=	lstMonserHit[i];

            CallStateEvent(eStateEventType.eSET_hit);
					
			sdBattleSystem	bs	=	sdGameLevel.instance.battleSystem;
			DamageResult dr = bs.testHurt(
				_gameActor,
				stateData,
				monster,
				i,
				skilleffect
				);
            //播放命中特效.
            if(dr.damage > 0)
                PlayHitEffect(monster);
            lstBubbleType.Add(dr.bubbleType);
            if (Bubble.IsHurtOther(dr.bubbleType))
                nRet++;
		}
        AddSkillEffect(_gameActor, lstMonserHit, lstBubbleType,skillEffect, null, ((int)stateData["ParentID"]) / 100);
        return nRet;
    }
}