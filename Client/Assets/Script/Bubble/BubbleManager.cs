using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BubbleInfo
{
    public string str;
    public Vector3 pos;
    public Bubble.BubbleType type;
    public bool bSelf;
}
public class BubbleManager : Singleton<BubbleManager>
{
	static int   mActiveCount  = 5;
	float mInterval = 0.15f;   //如果有多个bubble，激活间隔时间
    List<BubbleInfo> lstBubbleInfo = new List<BubbleInfo>();
    public int mOpenWndCount = 0; //bubble有个bug 显示层级在窗口之上，临时解决方案，当有窗口打开时，不显示bubble 该参数计数当前是否有窗口打开aa
	
	Dictionary<int,List<Bubble>> mActiveBubble = new Dictionary<int, List<Bubble>>();	//已经激活的Bubble
	Dictionary<int,List<Bubble>> mDeactiveBubble = new Dictionary<int, List<Bubble>>();
	Dictionary<int,float> mActiveTime = new Dictionary<int, float>();//上一个激活的时间
		
	public void Update()
	{
        UpdateLoadBubble();

		foreach (KeyValuePair<int, List<Bubble>> pair in mActiveBubble)
		{
			List<Bubble>  lstBubble = (List<Bubble>)pair.Value;
			int nCount = mActiveCount - lstBubble.Count;
			if(nCount > 0 && mDeactiveBubble.ContainsKey(pair.Key))
			{
				float fTime = 0.0f;
				if(mActiveTime.ContainsKey(pair.Key))
				{
					fTime = mActiveTime[pair.Key];
				}			
				if(Time.time - fTime > mInterval)
				{
					List<Bubble> lstDeactive = (List<Bubble>)mDeactiveBubble[pair.Key];
					if(lstDeactive.Count > 0)
					{
						Bubble bubble = lstDeactive[0];
						bubble.bPause = false;
						bubble.enabled = true;
						if(lstDeactive.Count > 1)
							bubble.Speed = (1.0f + (lstDeactive.Count - 1)*0.1f);
						lstBubble.Add(bubble);
						lstDeactive.RemoveAt(0);
						mActiveTime[pair.Key] = Time.time;
					}
				}
			}
		}
		
//		int nActiveCount = 0;
//		foreach(KeyValuePair<int, List<Bubble>> pair in mActiveBubble)
//		{
//			List<Bubble> lst = pair.Value;
//			nActiveCount += lst.Count;
//		}
//		
//		int nDeactivecount = 0;
//		foreach(KeyValuePair<int, List<Bubble>> pair in mDeactiveBubble)
//		{
//			List<Bubble> lst = pair.Value;
//			nDeactivecount += lst.Count;
//		}
//		Debug.Log("active bubble is" + nActiveCount + "\n");
//		Debug.Log("deactive bubble is" + nDeactivecount + "\n");
	}
	
	public void DeleteBubble(Bubble bubble)
	{
		int ownerID = bubble.OwnerID;
		if(mActiveBubble.ContainsKey(ownerID))
		{
			mActiveBubble[ownerID].Remove(bubble);
		}
	}
	
	void AddBubble(Bubble bubble)
	{
		int ownerID = bubble.OwnerID;
		bool bEnable = true;
		List<Bubble> lstActive = null;
		if(mActiveBubble.ContainsKey(ownerID))
		{
			lstActive = (List<Bubble>)mActiveBubble[ownerID];
			if(lstActive.Count >= mActiveCount)
			{
				bEnable = false;
			}
			else
			{
				if(mActiveTime.ContainsKey(ownerID))
				{
					float fTime = mActiveTime[ownerID];
					if(Time.time - fTime > mInterval)
						bEnable = true;
					else
						bEnable = false;
				}
				else
				{
					bEnable = true;
				}
			}
		}
		else
		{
			lstActive = new List<Bubble>();
		}
		if(bEnable)
		{
			bubble.bPause = false;
			bubble.enabled = true;
			lstActive.Add(bubble);	
			mActiveBubble[ownerID] = lstActive;
			mActiveTime[ownerID] = Time.time;
		}
		else
		{
			List<Bubble> lstDeactive = null;
			if(mDeactiveBubble.ContainsKey(ownerID))
				lstDeactive = (List<Bubble>)mDeactiveBubble[ownerID];
			else
				lstDeactive = new List<Bubble>();
			lstDeactive.Add(bubble);
			bubble.bPause = true;
			bubble.enabled = false;
			mDeactiveBubble[ownerID] = lstDeactive;
		}
		
	}

    void ClearAll()
    {
        foreach (KeyValuePair<int, List<Bubble>> pair in mActiveBubble)
        {
            List<Bubble> lstBubble = (List<Bubble>)pair.Value;
            for (int i = 0; i < lstBubble.Count; ++i)
            {
                if (lstBubble[i] != null)
                {
                    GameObject.Destroy(lstBubble[i].gameObject);
                }
            }
            lstBubble.Clear();
        }
        mActiveBubble.Clear();
        foreach (KeyValuePair<int, List<Bubble>> pair2 in mDeactiveBubble)
        {
            List<Bubble> lstBubble2 = (List<Bubble>)pair2.Value;
            for (int j = 0; j < lstBubble2.Count; ++j )
			{
				if (lstBubble2[j] != null)
				{
                	GameObject.Destroy(lstBubble2[j].gameObject);
				}
			}
            lstBubble2.Clear();
        }
        mDeactiveBubble.Clear();
        mActiveTime.Clear();
    }

    public void OnOpenWnd()
    {
        mOpenWndCount++;
        ClearAll();
    }

    public void OnCloseWnd()
    {
        mOpenWndCount--;
    }


    void UpdateLoadBubble()
    {
        if (lstBubbleInfo.Count > 0)
        {
            BubbleInfo info = lstBubbleInfo[0];
            lstBubbleInfo.RemoveAt(0);
            Bubble bubble = Load(info.str, info.pos, info.type, info.bSelf);
            if (bubble)
            {
                bubble.OwnerID = bubble.GetInstanceID();
                AddBubble(bubble);
            }
        }
    }
    public void AddBubble(string str, Vector3 pos, Bubble.BubbleType type, bool bSelf)
    {
        BubbleInfo info = new BubbleInfo();
        info.str = str;
        info.pos = pos;
        info.type = type;
        info.bSelf = bSelf;
        lstBubbleInfo.Add(info);
    }
    static Bubble Load(string str, Vector3 pos, Bubble.BubbleType type, bool bSelf)
    {
        if (BubbleSystem.system == null)
            return null;
        if (BubbleManager.Instance.mOpenWndCount > 0)
            return null;

        GameObject obj = GameObject.Instantiate(Resources.Load("Bubble/Bubble")) as GameObject;
        if (type == Bubble.BubbleType.eBT_AddSp)
        {
            obj.transform.parent = null;
            GameObject playerHead = GameObject.Find("Player");
            if (playerHead != null)
            {
                Vector3 playerPostion = playerHead.transform.position;
                float offsetY = 0.15f * playerHead.transform.localScale.y;
                float offsetX = 0.4f * playerHead.transform.localScale.x;
                obj.transform.localPosition = playerPostion + new Vector3(-1.0f * offsetX, offsetY, 0.0f);
            }
            obj.layer = 11;
        }
        else
        {
            obj.transform.parent = BubbleSystem.system.GetSelfTransform();//Transform();//
            Vector3 v = BubbleSystem.system.WorldToScreen(pos);
            v *= 2.0f;
            v -= new Vector3(1, 1, 1);//new Vector3(0.5f,0.5f,0);
            v.z = 0;
            obj.transform.localPosition = v;//(pos);//
            obj.layer = 11;

        }
        if (!bSelf)
        {
            obj.transform.localPosition += new Vector3(Random.Range(-0.05f, 0.05f), 0, 0);
        }

        Bubble n = obj.GetComponent<Bubble>();
        if (n != null)
        {
            n.rule = Bubble.NewRule(type);
            n.rule.bubbleType = type;
            n.bSelf = bSelf;
            if (bSelf)
            {
                n.rule.SetSelf();
            }
            n.transform.localScale = Vector3.one * n.rule.CalcScale(0.0f); ;
            Color c = Bubble.GetBubbleColor(type);
            switch (type)
            {
                case Bubble.BubbleType.eBT_Miss:
                case Bubble.BubbleType.eBT_Dodge:
                    {
                        n.SetOther(type, c);
                    } break;
                default:
                    {
                        if (type == Bubble.BubbleType.eBT_CriticalBaseHurt || type == Bubble.BubbleType.eBT_CriticalSkillHurt)
                        {
                            n.SetNumber(">?" + str, c);
                        }
                        else
                        {
                            n.SetNumber(str, c);
                        }
                    } break;
            }

        }
        return n;
    }
}



