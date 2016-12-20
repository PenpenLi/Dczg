using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class sdActorManager  {
	Dictionary<int,List<sdActorInterface>> AllActor = new Dictionary<int, List<sdActorInterface>>();
    List<sdPhysicsTrigger> TriggerList = new List<sdPhysicsTrigger>();
	public	sdActorManager()
	{
		
	}

	public Dictionary<int, List<sdActorInterface>> GetAllActor()
	{
		return AllActor;
	}
	
	// 添加角色aa
	public void AddActor(sdActorInterface kActor)
	{
		if (kActor == null)
			return;

		int GroupID = (int)kActor.GetGroupID();
		List<sdActorInterface> kActorList = null;
		if(AllActor.ContainsKey(GroupID))
		{
			kActorList = AllActor[GroupID];
		}
		else
		{
			kActorList = new List<sdActorInterface>();
			AllActor[GroupID] = kActorList;
		}

		kActorList.Add(kActor);
	}
	
	// 移除角色aa
	public void RemoveActor(sdActorInterface kActor)
	{
		if (kActor == null)
			return;

		int iGroupID = (int)kActor.GetGroupID();
		List<sdActorInterface> kActorList = null;
		if (AllActor.TryGetValue(iGroupID, out kActorList))
		{
			if (kActorList != null)
				kActorList.Remove(kActor);
		}
	}
	
	void	GetGroupActor(int iGroupID,ref List<sdActorInterface> lstActor,bool    bActive)
	{
		if(!AllActor.ContainsKey(iGroupID))
		{
			return;
		}
		List<sdActorInterface>	lst =	AllActor[iGroupID];
		if(lst!=null)
		{
			foreach(sdActorInterface a in lst)
			{
				if(bActive)
				{
					if(!a.IsActive() || a.GetCurrentHP() <=0 )
					{
						continue;
					}
				}
                if (a.Hide)
                    continue;
				lstActor.Add (a);
			}
		}
	}
	void	GetGroupActor(int iGroupID,int groupMask,ref List<sdActorInterface> lstActor,bool    bActive)
	{
		for(int i=1;i<= (int)GroupIDType.GIT_MonsterB;i++)
		{
			if(i	!= iGroupID)
			{
				if(((1<<i )&groupMask)!= 0)
				{
					GetGroupActor(i,ref lstActor,bActive);
				}
			}
		}
	}
	public	List<sdActorInterface>	GetTargetActor(sdActorInterface actor,HeaderProto.ESkillObjType  itt,bool    bActive)
	{
		if(actor==null)
		{
			return null;
		}
		int iGroupID	=	(int)actor.GetGroupID();
		List<sdActorInterface> lst = new List<sdActorInterface>();
		
		GroupType[,] gtArray	=	sdConfDataMgr.Instance().m_Group;
		
		switch((int)itt)
		{
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_SELF:{
				lst.Add(actor);
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_TEAM:{
				GetGroupActor(iGroupID,ref lst,bActive); 
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_TEAM_EXCLOUD_SELF:{
				GetGroupActor(iGroupID,ref lst,bActive); 
				lst.Remove(actor);
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_FRIENDLY_ROLE:{
				for(int i=0;i<=(int)GroupIDType.GIT_MonsterB;i++)
				{
					GroupType	t	=	gtArray[iGroupID,i];
					if(t==GroupType.GT_Friend)
					{
						GetGroupActor(i,ref lst,bActive); 
					}
				}
				
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_FRIENDLY:{
				for(int i=0;i<=(int)GroupIDType.GIT_MonsterB;i++)
				{
					GroupType	t	=	gtArray[iGroupID,i];
					if(t==GroupType.GT_Friend)
					{
						GetGroupActor(i,ref lst,bActive); 
					}
				}
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_ENEMY:{
				for(int i=0;i<=(int)GroupIDType.GIT_MonsterB;i++)
				{
					GroupType	t	=	gtArray[iGroupID,i];
					if(t==GroupType.GT_Enemy)
					{
						GetGroupActor(i,ref lst,bActive); 
					}
				}
				
			}break;
			case (int)HeaderProto.ESkillObjType.SKILL_OBJ_ALL:{
				for(int i=0;i<=(int)GroupIDType.GIT_MonsterB;i++)
				{
					GetGroupActor(i,ref lst,bActive);
				} 
			}break;
		case (int)HeaderProto.ESkillObjType.SKILL_OBJ_ALL_EXCLOUD_SELF:{
				for(int i = 0; i <= (int)GroupIDType.GIT_MonsterB; i++)
				{
					GetGroupActor(i, ref lst, bActive);
				}
				lst.Remove(actor);
			}break;
		}
		
		return lst;
			
		
	}
	//寻找最近active的怪，如果没有找最近的未active的怪aaa
	public  sdActorInterface  FindNearestActorActiveFirst(
		sdActorInterface actor,
		HeaderProto.ESkillObjType itt,
		Vector3 vPos,
		Vector3 vDir,
		int 	strikeType,
		int 	strikeAngle,
		float 	strikeDis
		)
	{
		List<sdActorInterface> lstActor	=	GetTargetActor(actor,itt,false);
		if(lstActor==null)
		{
			return null;
		}
		if(lstActor.Count==0)
		{
			return null;	
		}
		
		float fCurrentDis		=	99999.0f;
		float fCurrentActiveDis =   99999.0f;
		sdActorInterface	ret	=	null;
		sdActorInterface    activeret = null;
		List<sdActorInterface>	retList	=	new List<sdActorInterface>();
		foreach(sdActorInterface a in lstActor)
		{
			if((	a	!= null) &&
					(a.GetCurrentHP() > 0) &&
                    a.actorType != ActorType.AT_Static)
				{
					bool hit = false;
					
					if(strikeType == 5)
					{
						hit = SDGlobal.CheckHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.5f,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 1)
					{
						hit = SDGlobal.CheckRoundHit(
							vPos,
							strikeDis,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 7)
					{
						hit = SDGlobal.CheckBoxHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.001f,
							a.transform.position,
							a.getRadius());
					}
					
	
					if(hit)
					{
						Vector3 v	=	a.transform.position	-	vPos;
						if(a.IsActive())
						{
							if(fCurrentActiveDis > v.magnitude)
							{
								fCurrentActiveDis = v.magnitude;
								activeret = a;
							}
						}
						else
						{
							if(fCurrentDis >	v.magnitude)
							{
								fCurrentDis	=	v.magnitude;
								ret	=	a;
							}
						}
					}
					
				}
		}
		return activeret != null ? activeret : ret;
	}
	
	public	sdActorInterface	FindNearestActor(
		sdActorInterface actor,
		HeaderProto.ESkillObjType itt,
		Vector3 vPos,
		Vector3 vDir,
		int 	strikeType,
		int 	strikeAngle,
		float 	strikeDis,
		bool    bActive)
	{
		List<sdActorInterface> lstActor	=	GetTargetActor(actor,itt,bActive);
		if(lstActor==null)
		{
			return null;
		}
		if(lstActor.Count==0)
		{
			return null;	
		}
		
		float fCurrentDis		=	99999.0f;
		sdActorInterface	ret	=	null;
		List<sdActorInterface>	retList	=	new List<sdActorInterface>();
		foreach(sdActorInterface a in lstActor)
		{
			if((	a	!= null) &&
					(a.GetCurrentHP() > 0) &&
                a.actorType !=ActorType.AT_Static)
				{
					bool hit = false;
					if(strikeType == 5)
					{
						hit = SDGlobal.CheckHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.5f,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 1)
					{
						hit = SDGlobal.CheckRoundHit(
							vPos,
							strikeDis,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 7)
					{
						hit = SDGlobal.CheckBoxHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.001f,
							a.transform.position,
							a.getRadius());
					}
					if(hit)
					{
						Vector3 v	=	a.transform.position	-	vPos;
						if(fCurrentDis >	v.magnitude)
						{
							fCurrentDis	=	v.magnitude;
							ret	=	a;
						}
					}
					
				}
		}
		return ret;
	}
	public	List<sdActorInterface>	FindActor(
		sdActorInterface actor,
		HeaderProto.ESkillObjType itt,
		Vector3 vPos,
		Vector3 vDir,
		int 	strikeType,
		int 	strikeAngle,
		float 	strikeDis,
		bool    bActive
		)
	{
		List<sdActorInterface> lstActor	=	GetTargetActor(actor,itt,bActive);
		if(lstActor==null)
		{
			return null;
		}
		if(lstActor.Count==0)
		{
			return null;	
		}
		List<sdActorInterface>	retList	=	new List<sdActorInterface>();
		foreach(sdActorInterface a in lstActor)
		{
			if((	a	!= null) &&
					a.IsActive() &&
					(a.GetCurrentHP() > 0))
				{
					bool hit = false;
					if(strikeType == 5)
					{
						hit = SDGlobal.CheckHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.5f,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 1)
					{
						hit = SDGlobal.CheckRoundHit(
							vPos,
							strikeDis,
							a.transform.position,
							a.getRadius());
					}
					else if(strikeType == 7)
					{
						hit = SDGlobal.CheckBoxHit(
							vPos,
							vDir,
							strikeDis,strikeAngle*0.001f,
							a.transform.position,
							a.getRadius());
					}
					if(hit)
					{
						retList.Add(a);
					}
					
				}
		}
		return retList;
	}
	public	static	bool IsActorEnemy(sdActorInterface actorA,sdActorInterface actorB)
	{
		int iGroupA	=	(int)actorA.groupid;
		int iGroupB	=	(int)actorB.groupid;
		GroupType[,] gtArray	=	sdConfDataMgr.Instance().m_Group;
		GroupType	type	=	gtArray[iGroupA,iGroupB];
		if(type==GroupType.GT_Enemy)
		{
			return true;
		}
		return false;
	}
	
	public sdGameActor FindActorbyName(string strActorName)
	{
		List<sdActorInterface> lst = new List<sdActorInterface>();
		for(int i = 0; i <= (int)GroupIDType.GIT_MonsterB; i++)
		{
			GetGroupActor(i, ref lst, true);
			for(int j = 0; j < lst.Count; ++j)
			{
				sdGameActor actor = lst[j] as sdGameActor;
				if(actor != null && actor.gameObject.transform.name == strActorName)
				{
					return actor;
				}
			}
		}
		return null;
	}

	public List<sdActorInterface> SortActor(Vector3 srcPoint, List<sdActorInterface> listActor)
	{
		List<sdActorInterface> lst = new List<sdActorInterface>();
		if(listActor == null)
			return lst;
		float fMin = float.MaxValue;
		int index = 0;
		for(int i = 0; i < listActor.Count;)
		{
            if (listActor[i].actorType != ActorType.AT_Static)
            {
                Vector3 targetPoint = listActor[i].transform.position;
                targetPoint = targetPoint - srcPoint;
                float fDis = targetPoint.magnitude;
                if (fMin > fDis)
                {
                    fMin = fDis;
                    index = i;
                }
            }
			i++;
			if(i == listActor.Count)
			{
				lst.Add(listActor[index]);
				listActor.RemoveAt(index);
				i = 0;
				index = 0;
				fMin = float.MaxValue;
			}
		}
		return lst;
	}
    //查找dis范围内与dir夹角最小的actor
    public sdActorInterface FindNearestAngle(sdActorInterface actor, HeaderProto.ESkillObjType objType, Vector3 dir, float dis, float angle)
    {
        sdActorInterface ret = null;
        List<sdActorInterface> lst = new List<sdActorInterface>();
        lst = GetTargetActor(actor, objType, true);
        float fcos = Mathf.Cos(angle/180.0f*Mathf.PI);
        for (int index = 0; index < lst.Count; ++index)
        {
            sdActorInterface target = lst[index];
            Vector3 targetDir = target.transform.position - actor.transform.position;
            if (targetDir.magnitude > dis)
                continue;
            targetDir.y = 0;
            targetDir.Normalize();
            Vector3 faceDir = dir;
            faceDir.y = 0;
            faceDir.Normalize();
            float value = Vector3.Dot(targetDir, faceDir);
            if (value > fcos)
            {
                fcos = value;
                ret = lst[index];
            }
        }
        return ret;
    }
    public void AddTrigger(sdPhysicsTrigger trigger)
    {
        TriggerList.Add(trigger);
    }
    public void ManualCheckTrigger(sdGameActor actor,Vector3 pos,Vector3 v)
    {
        foreach (sdPhysicsTrigger t in TriggerList)
        {
            t.ManualCheckTrigger(actor,pos, v);
        }
    }
}
