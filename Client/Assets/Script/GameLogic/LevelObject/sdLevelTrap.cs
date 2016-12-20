using UnityEngine;
using System.Collections;
public  enum TrapRotateType
{
	X,
	Y,
	Z
}
public	enum TrapMoveType
{
	Loop,		//	0-1-2-3-4,0-1-2-3-4
	PingPang,   //  0-1-2-3-4-3-2-1-0
}

public class sdLevelTrap : sdActorInterface
{
	public	int					PropertyID		=	101;
	TrapRotateType				rotType			=	TrapRotateType.X;
	public	float				rotSpeed		=	0.0f;
	public	TrapMoveType		moveType		=	TrapMoveType.Loop;
	public	Vector3				initVelocity	=	Vector3.zero;
	public	Vector3				acceleration	=	Vector3.zero;
	public	int					SummonID		=	0;
	public	bool				bornlive		=	false;
	public	float				life			=	1000.0f;
	public	float				loopTime		=	-1.0f;
	
	Vector3	vInitPos							=	Vector3.zero;
	
	Vector3	velocity	=		Vector3.zero;
	float	rot			=	0.0f;
	
	protected override	void Start () {
		velocity	=	initVelocity;
		Hashtable basePro	=	GetBaseProperty();
		Property	=	sdConfDataMgr.CloneHashTable(basePro);
		if(bornlive)
		{
			OnBorn();
		}
	}
	override public Hashtable GetBaseProperty()
	{
		Hashtable MonsterProperty	=	sdConfDataMgr.Instance().GetTable("MonsterProperty");
		return MonsterProperty[PropertyID] as Hashtable;
	}
	
	protected override	void Update () {
		base.Update();
		if(!bornlive)
		{
			return;
		}
		
		Vector3 axis	=	Vector3.zero;
		switch(rotType)
		{
			case TrapRotateType.X:{axis	=	new Vector3(1,0,0);}break;
			case TrapRotateType.Y:{axis	=	new Vector3(0,1,0);}break;
			case TrapRotateType.Z:{axis	=	new Vector3(0,0,1);}break;
		}
		rot+=Time.deltaTime;
		transform.localRotation	=	Quaternion.AngleAxis(rot*rotSpeed*90.0f,axis);
		
		velocity	+=	acceleration*Time.deltaTime;
		switch(moveType)
		{
			case TrapMoveType.Loop:{
				
			}break;
			case TrapMoveType.PingPang:{
			
			}break;
		}
		
	}
	void	OnBorn()
	{
		Hashtable	table	=	sdConfDataMgr.Instance().m_BaseSummon[SummonID] as Hashtable;
		
		ResLoadParams param = new ResLoadParams();

		param.userdata0	=	SummonID;
		param.userdata1	=	null;
		param.userdata2 =   0;
		sdSkillSummon	summon	=	GetComponent<sdSkillSummon>();
		if(summon!=null)
		{
			summon.SetInfo(this,param);
			
			summon.SetLife(life);
		}
	}
	

	public override void OnTriggerHitted(GameObject obj,int[] param)
	{
		base.OnTriggerHitted(obj,param);
		OnBorn();
	}
	protected override void WhenEnterTrigger (GameObject obj,int[] param)
	{
		base.WhenEnterTrigger (obj, param);
		if(OnceLife)
		{
			live = false;
		}
		
		for(int i = 0; i < enterReceivers.Length; i++)
		{
			if(enterReceivers[i] != null)
			{
				enterReceivers[i].OnTriggerHitted(obj,iEnterParams[i].v);
			}
		}
	}
	
	protected override void WhenLeaveTrigger (GameObject obj,int[] param)
	{
		base.WhenLeaveTrigger (obj, param);
		
		for(int i = 0; i < leaveReceivers.Length; i++)
		{
			if(leaveReceivers[i] != null)
			{
				leaveReceivers[i].OnTriggerHitted(gameObject,iLeaveParams[i].v);
			}
		}
	}
}
