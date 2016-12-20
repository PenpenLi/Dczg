using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sdPTWnd : MonoBehaviour
{
    UILabel m_times;
    UILabel m_costTili;
    UILabel m_needlevel;

    void Awake()
    {
        GameObject root = GameObject.Find("$PTWnd(Clone)");
        m_times = root.transform.FindChild("Label_times").GetComponent<UILabel>();
        sdPTManager.Instance.RefreshPT += Refresh;
        Hashtable ptTable = sdConfDataMgr.Instance().GetTable("dmdsxactivitytemplateconfig.pttemplates");

        for (int index = 1; index < 4; ++index)
        {
            if(ptTable.ContainsKey(index.ToString()))
            {
                Hashtable table = ptTable[index.ToString()] as Hashtable;
                Transform child = root.transform.FindChild("Sprite_level" + index.ToString());
                UISprite btn_bg = child.FindChild("Button_PT_enter" + index.ToString()).FindChild("Background").GetComponent<UISprite>();
                btn_bg.spriteName = sdPTManager.Instance.m_PassLevel + 1 >= index ? "btn" : "btn_dis";

                UILabel lb_costliti = child.FindChild("Sprite_info").FindChild("Label_tili").GetComponent<UILabel>();
                lb_costliti.text = table["Tili"].ToString();
                UILabel lb_needlevel = child.FindChild("Sprite_info").FindChild("Label_level").GetComponent<UILabel>();
                lb_needlevel.text = table["RecLvl"].ToString();
                int level = int.Parse(table["LevelID"].ToString());
                for (int i = 0; i < sdLevelInfo.levelInfos.Length; ++i )
                {
                    if (sdLevelInfo.levelInfos[i].levelID == level)
                    {
                        string[] strItems = ((string)sdLevelInfo.levelInfos[i].levelProp["Drop"]).Split(';');
                        int j = 0;
                        for (; j < strItems.Length && j < 3; ++j)
                        {
                            Transform sprite_info = child.FindChild("Sprite_info");
                            sprite_info.transform.FindChild("Sprite_reward" + j).gameObject.SetActive(true);
                            sdSlotIcon sloticon = sprite_info.FindChild("Sprite_reward" + j.ToString()).GetComponent<sdSlotIcon>();
                            Hashtable iteminfo = sdConfDataMgr.Instance().GetItemById(strItems[j].ToString());
                            if (iteminfo == null)
                            {
                                sprite_info.transform.FindChild("Sprite_reward" + j).gameObject.SetActive(false);
                                break;
                            }
                            
                            UISprite sprite_item = sprite_info.transform.FindChild("Sprite_reward" + j).GetComponent<UISprite>();
                            sprite_item.atlas = sdConfDataMgr.Instance().commonAtlas;
                            sprite_item.spriteName = "IconFrame0";
                            sloticon.SetInfo(strItems[j].ToString(), iteminfo);

                            UISprite sprite_quality = sprite_info.transform.FindChild("Sprite_reward" + j).FindChild("Sprite").GetComponent<UISprite>();
                            sprite_quality.atlas = sdConfDataMgr.Instance().commonAtlas;
                            sprite_quality.spriteName = sdConfDataMgr.Instance().GetItemQuilityBorder(int.Parse(iteminfo["Quility"].ToString()));

                        }
                        for (; j < 3;++j )
                        {
                            Transform sprite_info = child.FindChild("Sprite_info");
                            sprite_info.transform.FindChild("Sprite_reward" + j).gameObject.SetActive(false);
                        }
                        break;
                    }
                }
            }
        }
    }

    public void Refresh()
    {
        if (this == null) return;
        m_times.text = sdPTManager.Instance.m_Times.ToString() + "/" + (10 - sdPTManager.Instance.m_BuyTimes);
    }
}