using UnityEngine;
using System.Collections;

public class sdMysteryShopWnd : MonoBehaviour
{
    UILabel label_timetick = null;
    UILabel label_crystal = null;
    GameObject template_item = null;
    UILabel label_freefreshtime = null;
    Transform panel = null;
    UISprite sprite_freshbtn = null;
    UILabel lb_refreshinfo = null;
    GameObject refresh_type = null;

    int m_HZRefresh = 0;

    void Awake()
    {
        GameObject root = GameObject.Find("mysteryshopwnd(Clone)");
        sprite_freshbtn = root.transform.FindChild("Button_fresh").FindChild("Background").GetComponent<UISprite>();
        label_timetick = root.transform.FindChild("Sprite_text_bg").FindChild("Label_timetick").GetComponent<UILabel>();
        label_crystal = root.transform.FindChild("Label_crystal").GetComponent<UILabel>();
        template_item = root.transform.FindChild("panel_items").FindChild("item0").gameObject;
        lb_refreshinfo = root.transform.FindChild("Sprite_text_bg").FindChild("Label_cost").GetComponent<UILabel>();
        panel = root.transform.FindChild("panel_items");
        label_freefreshtime = root.transform.FindChild("Label_refreshtime").GetComponent<UILabel>();
        refresh_type = root.transform.FindChild("Sprite_cost").gameObject;
        sdMysteryShopMgr.Instance.RefreshData += Refresh;
    }

    void Start()
    {
        Hashtable configTable = sdConfDataMgr.Instance().GetTable("config");
        if (configTable.ContainsKey("mystertyshoprefreshcost"))
        {
            Hashtable table = configTable["mystertyshoprefreshcost"] as Hashtable;
            m_HZRefresh = int.Parse(table["value"] as string);
            lb_refreshinfo.text = m_HZRefresh.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {
        string str = "";
        long timetick = sdMysteryShopMgr.Instance.m_nTimeTick - System.DateTime.Now.Ticks/10000000L;
        //商店刷新aaa
        if(timetick <= 0)
            sdMysteryShopMsg.Send_SHOP_SECRET_GOODS_REQ();
        if (timetick >= 3600L)
        {
            int hour = (int)(timetick / 3600L);
            timetick = timetick % 3600;
            if (hour < 10)
                str = "0" + hour.ToString() + ":";
            else
                str = hour.ToString() + ":";
        }
        else
            str = str + "00:";
        if (timetick >= 60L)
        {
            int minute = (int)(timetick / 60L);
            timetick = timetick % 60;
            if (minute < 10)
                str = str + "0" + minute.ToString() + ":";
            else
                str = str + minute.ToString() + ":";
        }
        else
            str = str + "00:";
        if (timetick < 10)
            str = str + "0" + timetick.ToString();
        else
            str = str + timetick.ToString();
        label_timetick.text = str;
    }

    public void Refresh()
    {
        if (this == null)
            return;
        if (sdMysteryShopMgr.Instance.m_freeRefreshTime > 0)
        {
            refresh_type.SetActive(false);
            label_freefreshtime.text = "免费刷新次数：" + sdMysteryShopMgr.Instance.m_freeRefreshTime.ToString() + "次";
        }
        else if (sdGameItemMgr.Instance.GetItemCount(103) > 0)
        {
            refresh_type.SetActive(true);
            Vector3 pos = refresh_type.transform.localPosition;
            refresh_type.transform.localPosition = new Vector3(525, pos.y, pos.z);

            label_freefreshtime.text = "刷新消耗:1/" + sdGameItemMgr.Instance.GetItemCount(103).ToString();
            UISprite sprite = refresh_type.GetComponent<UISprite>();
            sdUICharacter.Instance.LoadAtlas("UI/$rankList/ranklistAtlas.prefab", sprite, "sxlp");
        }
        else
        {
            refresh_type.SetActive(true);
            label_freefreshtime.text = "刷新消耗:" + m_HZRefresh.ToString();
            Vector3 pos = refresh_type.transform.localPosition;
            refresh_type.transform.localPosition = new Vector3(520, pos.y, pos.z);
            UISprite sprite = refresh_type.GetComponent<UISprite>();
            sprite.atlas = sdConfDataMgr.Instance().commonAtlas;
            sprite.spriteName = "icon_xz";
        }
        label_crystal.text = sdGameItemMgr.Instance.GetItemCount(102).ToString();
        Hashtable shopTable = sdConfDataMgr.Instance().GetTable("dmdsshopconfig.shop_secrets");
        for (int index = 0; index < sdMysteryShopMgr.Instance.m_lstItem.Count; ++index)
        {
            CliProto.SSecretItemInfo item = sdMysteryShopMgr.Instance.m_lstItem[index];

            GameObject uiItem = null;
            Transform trans_item = panel.FindChild("item" + index.ToString());
            if (trans_item == null)
                uiItem = GameObject.Instantiate(template_item) as GameObject;
            else
                uiItem = trans_item.gameObject;
            uiItem.SetActive(true);
            uiItem.name = "item" + index.ToString();
            uiItem.transform.parent = panel;
            uiItem.transform.localPosition = new Vector3(template_item.transform.localPosition.x, template_item.transform.localPosition.y - 130.0f * index, 0);
            uiItem.transform.localScale = Vector3.one;
            uiItem.transform.localRotation = Quaternion.identity;

            bool bCanBuy = false;
            if(shopTable.ContainsKey(item.m_UID.ToString()))
            {
                Hashtable subTable = shopTable[item.m_UID.ToString()] as Hashtable;
                sdSlotIcon sloticon = uiItem.transform.FindChild("Sprite_item").GetComponent<sdSlotIcon>();
                UISprite sprite_item = uiItem.transform.FindChild("Sprite_item").GetComponent<UISprite>();
                sprite_item.atlas = sdConfDataMgr.Instance().commonAtlas;
                sprite_item.spriteName = "IconFrame0";
                Hashtable itemInfo = null;
                itemInfo = sdConfDataMgr.Instance().GetItemById(subTable["ItemId"] as string);
                sloticon.SetInfo(subTable["ItemId"] as string, itemInfo);
                int nCount = int.Parse(subTable["ItemNum"] as string);
                UILabel lb_num = uiItem.transform.FindChild("Sprite_item").FindChild("Label_num").GetComponent<UILabel>();
                lb_num.text = nCount.ToString();

                UILabel lb_money = uiItem.transform.FindChild("Label_money").GetComponent<UILabel>();
                int costNum = int.Parse(subTable["CostNum"].ToString());
                lb_money.text = costNum.ToString();

                UISprite sprite_moneytype = uiItem.transform.FindChild("Sprite_monetytype").GetComponent<UISprite>();
                int moneyType = int.Parse(subTable["CostType"].ToString());
                
                if (moneyType == 3)//水晶aaaa
                {
                    sprite_moneytype.width = 40;
                    sprite_moneytype.height = 48;
                    sdUICharacter.Instance.LoadAtlas("UI/$common2/common2.prefab", sprite_moneytype, "smsj");
                    if (sdGameItemMgr.Instance.GetItemCount(102) >= costNum)
                        bCanBuy = true;
                }
                else if (moneyType == 1)//勋章aaa
                {
                    sprite_moneytype.width = 40;
                    sprite_moneytype.height = 47;
                    sprite_moneytype.atlas = sdConfDataMgr.Instance().commonAtlas;
                    sprite_moneytype.spriteName = "icon_xz";
                    int Cash = int.Parse(((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["Cash"].ToString());
                    if (Cash >= costNum)
                        bCanBuy = true;
                }
                else if (moneyType == 2)//游戏币aa
                {
                    sprite_moneytype.width = 45;
                    sprite_moneytype.height = 38;
                    sprite_moneytype.atlas = sdConfDataMgr.Instance().commonAtlas;
                    sprite_moneytype.spriteName = "icon_jinb";
                    int money = int.Parse(((Hashtable)sdGlobalDatabase.Instance.globalData["MainCharBaseProp"])["NonMoney"].ToString());
                    if(money >= costNum)
                        bCanBuy = true;
                }
                UIPlaySound sound = sloticon.gameObject.GetComponent<UIPlaySound>();
                if(sound)
                {
                    int id = int.Parse(subTable["ItemId"] as string);
                    sound.enabled = (id != 100 && id != 101 && id != 200);
                }
                lb_money.color = bCanBuy ? Color.white : Color.red;
                UILabel lb_itemName = uiItem.transform.FindChild("Label_name").GetComponent<UILabel>();
                Hashtable itemTable = sdConfDataMgr.Instance().GetItemById(subTable["ItemId"] as string);
                lb_itemName.text = itemTable["ShowName"].ToString();
                lb_itemName.color = sdConfDataMgr.Instance().GetItemQuilityColor(int.Parse(itemInfo["Quility"].ToString()));
                UISprite sprite_quality = uiItem.transform.FindChild("Sprite_item").FindChild("Sprite").GetComponent<UISprite>();
                sprite_quality.spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(itemInfo["Quility"].ToString()));
            }
            uiItem.transform.FindChild("Button_buy").gameObject.SetActive(item.m_Bought == 0);
            uiItem.transform.FindChild("Sprite_ydh").gameObject.SetActive(item.m_Bought != 0);
        }
    }
}
