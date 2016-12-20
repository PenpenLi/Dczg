using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdRankListMgr : Singleton<sdRankListMgr>
{
    public int m_Count = 0;
    public int m_TotalCount = 0;
    public int m_SelfRank = 0;
    public HeaderProto.ERankType rankType;

    public List<sdFriend> m_Avatar = new List<sdFriend>(HeaderProto.MAX_RANK_PAGE_COUNT);

    public delegate void RefreshData();
    public event RefreshData RefreshEvent;


    public void Refresh()
    {
        if (RefreshEvent != null)
            RefreshEvent();
    }
}