using UnityEngine;
using System;
using System.Collections;
using	System.Collections.Generic;

public partial class sdTransitGraph : object
{
	
	
	
	public	static void loadAndLinkActionState(sdGameActor _actor,int iJob)
	{
		{	
			
			Hashtable	table	=	sdConfDataMgr.Instance().m_vecJobSkillAction[iJob];
			Hashtable	action	=	sdConfDataMgr.CloneHashTable(table);
			
			_actor.SetSkillAction(action);
		}
	}
}

