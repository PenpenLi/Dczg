using UnityEngine;
using System.Collections;


[AddComponentMenu("SNDA/GameLogic/Trigger/PhysicsTrigger")]
public class sdPhysicsTrigger : sdBaseTrigger {
    public bool mainChar = true;
    public bool AllActorType = false;
    public ActorType[] actorType = null;
    public bool AllGroupType = false;
    public GroupIDType[] groupType = null; 


	protected override void Awake()
	{
		base.Awake();

	}
	
	// Use this for initialization
    virtual protected void Start()
    {
        sdGameLevel.instance.actorMgr.AddTrigger(this);
	}
    protected bool CheckCondition(sdGameActor actor)
    {
        if (mainChar && actor.DBID == sdGameLevel.instance.mainChar.DBID)
        {
            return true;
        }
		
		
	    bool bAllowAT = false;
        if (AllActorType)
        {
            bAllowAT = true;
        }
        else
        {
            if (actorType != null)
            {
                if (actorType.Length > 0)
                {
                    for (int i = 0; i < actorType.Length; i++)
                    {
                        if (actor.actorType == actorType[i])
                        {
                            bAllowAT = true;
                            break;
                        }
                    }
                    if (!bAllowAT)
                    {
                        return false;
                    }
                }
            }
        }
		bool bAllowGT = true;
        if (AllGroupType)
        {
            bAllowGT = true;
        }
        else
        {
            if (groupType != null)
            {
                if (groupType.Length > 0)
                {
                    bAllowGT = false;
                    for (int i = 0; i < groupType.Length; i++)
                    {
                        if (actor.groupid == groupType[i])
                        {
                            bAllowGT = true;
                            break;
                        }
                    }
                    if (!bAllowGT)
                    {
                        return false;
                    }
                }
            }
        }

		return bAllowAT && bAllowGT;
		
		
    }
	
	// Update is called once per frame
	protected override void Update () {
		
	}

    public virtual void OnTriggerEnter(Collider obj)
	{
		
		if(!live)
			return;
		sdGameActor actor = obj.GetComponent<sdGameActor>();
        if (actor == null)
        {
            return;
        }

        if (CheckCondition(actor))
        {
            WhenEnterTrigger(actor.gameObject, new int[4] { 0, 0, 0, 0 });
        }
	}
	
	public  virtual void OnTriggerExit(Collider obj)
	{
        if (!live)
            return;
        sdGameActor actor = obj.GetComponent<sdGameActor>();
        if (actor == null)
        {
            return;
        }

        if (CheckCondition(actor))
        {
            WhenLeaveTrigger(actor.gameObject, new int[4] { 0, 0, 0, 0 });
        }
	}
	
	protected override void WhenEnterTrigger (GameObject obj,int[] param)
	{
		base.WhenEnterTrigger (obj,param);
		if(OnceLife)
		{
			live = false;
		}
		
		for(int i = 0; i < enterReceivers.Length; i++)
		{
			if(enterReceivers[i] != null)
			{
				enterReceivers[i].OnTriggerHitted(gameObject,iEnterParams[i].v);
			}
		}
	}
	
	protected override void WhenLeaveTrigger (GameObject obj,int[] param)
	{
		base.WhenLeaveTrigger (obj,param);
		
		for(int i = 0; i < leaveReceivers.Length; i++)
		{
			if(leaveReceivers[i] != null)
			{

				leaveReceivers[i].OnTriggerHitted(gameObject,iLeaveParams[i].v);
			}
		}
	}
    public void ManualCheckTrigger(sdGameActor actor, Vector3 pos, Vector3 v)
    {
        if (!live)
            return;

        float fDistance = v.magnitude;
        Vector3 vDir = v.normalized;
        Ray r1 = new Ray(pos + new Vector3(0, 0.1f, 0), vDir);
        Ray r2 = new Ray(pos + new Vector3(0, actor.getHeight() - 0.1f, 0), vDir);
        RaycastHit hit;
        if (GetComponent<Collider>().Raycast(r1, out hit, fDistance))
        {
            if (CheckCondition(actor))
            {
                WhenEnterTrigger(actor.gameObject, new int[4] { 0, 0, 0, 0 });
                return;
            }
        }
        if (GetComponent<Collider>().Raycast(r2, out hit, fDistance))
        {
            if (CheckCondition(actor))
            {
                WhenEnterTrigger(actor.gameObject, new int[4] { 0, 0, 0, 0 });
                return;
            }
        }
    }
}
