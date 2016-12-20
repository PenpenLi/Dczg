using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HideShowInfo
{
	public sdActorInterface actor;
	public float fHideTime;
	public float fDistance;
}
public class sdHideShowMgr : Singleton<sdHideShowMgr>
{
    static void SetActorActive(sdGameActor actor,bool active)
    {
        actor.MotionController.enabled = active;
        for (int i = 0; i < actor.RenderNode.transform.childCount; i++)
        {
            actor.RenderNode.transform.GetChild(i).gameObject.SetActive(active);
        }
        actor.Hide = !active;
    }
	List<HideShowInfo>  m_listActor = new List<HideShowInfo>();
	public void AddActor(HideShowInfo info)
	{
        //Vector3 hitpos = Vector3.zero;
        //sdTuiTuLogic.BornPosition(info.actor.gameObject.transform.position, info.fDistance, ref hitpos);
        //info.actor.gameObject.transform.position = hitpos;

        SetActorActive((sdGameActor)info.actor, false);
		m_listActor.Add(info);
	}
    public void AddActorNoRandomPosition(HideShowInfo info)
    {
        SetActorActive((sdGameActor)info.actor,false);
        m_listActor.Add(info);
    }
	void Update()
	{
		float fdelta = Time.deltaTime;
		for(int index = 0; index < m_listActor.Count;)
		{
			HideShowInfo info = m_listActor[index];
			if(info.fHideTime < float.Epsilon)
			{
                SetActorActive((sdGameActor)info.actor, true);
                Vector3 hitpos = Vector3.zero;
                sdTuiTuLogic.BornPosition(info.actor.gameObject.transform.position, info.fDistance, ref hitpos);
                info.actor.gameObject.transform.position = hitpos;
				m_listActor.RemoveAt(index);
			}
			else
			{
				info.fHideTime -= fdelta;
				++index;
			}
		}
	}
}
