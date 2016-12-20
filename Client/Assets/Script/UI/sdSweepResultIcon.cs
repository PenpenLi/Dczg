using System;
using System.Collections;
using UnityEngine;

public class sdSweepResultIcon : MonoBehaviour
{
    public ulong itemId = 0;
    public UISprite icon = null;
    public UISprite bg = null;
    public UISprite color = null;

    void OnSetAtlas(ResLoadParams param, UnityEngine.Object obj)
    {
        UIAtlas atlas = obj as UIAtlas;
        if (atlas != null)
        {
            icon.atlas = atlas;
        }
    }

    public void SetInfo(ulong id)
    {
        itemId = id;
        sdGameItem item = sdGameItemMgr.Instance.getItem(id);
        if (item == null) return;
        Hashtable info = sdConfDataMgr.Instance().GetItemById(item.templateID.ToString());
        bg.spriteName = "IconFrame0";
        icon.spriteName = info["IconPath"].ToString();
        sdConfDataMgr.Instance().LoadItemAtlas(info["IconID"].ToString(), OnSetAtlas);
        color.spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(info["Quility"].ToString())); ;
    }

    void Clear()
    {
        icon.spriteName = "";
        color.spriteName = "";
        bg.spriteName = "";
    }
}
