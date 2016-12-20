using System;
using System.Collections;
using System.Collections.Generic;


public class sdMysteryShopMgr : Singleton<sdMysteryShopMgr>
{
    public List<CliProto.SSecretItemInfo> m_lstItem = new List<CliProto.SSecretItemInfo>();
    public int m_freeRefreshTime;
    public long m_nTimeTick = 0; //单位秒aaa

    public delegate void RefreshEvent();
    public event RefreshEvent RefreshData;

    public void Buy(int index)
    {
        if (index < 0 || index >= m_lstItem.Count)
            return;
        CliProto.SSecretItemInfo item = m_lstItem[index];
        if (item.m_Bought == 1)
        {
            sdUICharacter.Instance.ShowOkMsg("不能重复购买！", null);
        }
        else
            sdMysteryShopMsg.Send_SHOP_SECRET_BUY_REQ(item.m_UID);
    }

    public void Refresh()
    {
        if (RefreshData != null)
            RefreshData();
    }
}