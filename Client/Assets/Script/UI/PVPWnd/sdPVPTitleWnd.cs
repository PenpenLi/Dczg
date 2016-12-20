using UnityEngine;
using System.Collections;

public class sdPVPTitleWnd: MonoBehaviour
{
    GameObject current = null;
    GameObject next = null;

    void Awake()
    {
        current = GameObject.Find("Sprite_current");
        next = GameObject.Find("Sprite_next");
    }
    void Start()
    {

    }

    public void Refresh()
    {
        int militaryLevel = sdPVPManager.Instance.nMilitaryLevel;
        int upgradevalue = 0;
        Hashtable military = sdConfDataMgr.Instance().GetTable("militarylevel");
        int totalReputation = 0;
        if (militaryLevel > 1)
        {
            for (int index = 1; index < militaryLevel; index++) //计算当前等级之前的声望值aaa
            {
                Hashtable militaryTable = military[index.ToString()] as Hashtable;
                totalReputation += int.Parse((string)militaryTable["reputation"]);
            }
        }

        //当前的aaa
        if (military.ContainsKey(militaryLevel.ToString()))
        {
            Hashtable table = military[militaryLevel.ToString()] as Hashtable;
            GameObject curHead = current.transform.FindChild("Sprite_headicon1").gameObject;
            ResLoadParams param = new ResLoadParams();
            param.userdata0 = curHead;
            param.userdata1 = (string)table["icon"];
            sdResourceMgr.Instance.LoadResource("UI/$PVP/pvp.prefab", LoadAtlas, param, typeof(UIAtlas));
            UILabel phypower = current.transform.FindChild("Label_tili").GetComponent<UILabel>();
            phypower.text = (string)table["phypower"];
            UILabel hp = current.transform.FindChild("Label_hp").GetComponent<UILabel>();
            hp.text = (string)table["hp"];
            UILabel attack = current.transform.FindChild("Label_attack").GetComponent<UILabel>();
            attack.text = (string)table["attack"];
            UILabel reputation = current.transform.FindChild("Label_reputation").GetComponent<UILabel>();
            reputation.text = totalReputation.ToString();
            UILabel experience = current.transform.FindChild("Label_experience").GetComponent<UILabel>();
            experience.text = "+" + (string)table["experience"] + "%";
            UILabel money = current.transform.FindChild("Label_money").GetComponent<UILabel>();
            money.text = "+" + (string)table["money"] + "%";
            UILabel title = current.transform.FindChild("Label_title").GetComponent<UILabel>();
            title.text = (string)table["name"];
            upgradevalue = int.Parse((string)table["reputation"]);
        }
        //next 

        if (military.ContainsKey((militaryLevel + 1).ToString()))
        {
            Hashtable table = military[(militaryLevel + 1).ToString()] as Hashtable;
            GameObject curHead = next.transform.FindChild("Sprite_headicon1").gameObject;
            ResLoadParams param = new ResLoadParams();
            param.userdata0 = curHead;
            param.userdata1 = (string)table["icon"];
            sdResourceMgr.Instance.LoadResource("UI/$PVP/pvp.prefab", LoadAtlas, param, typeof(UIAtlas));
            UILabel phypower = next.transform.FindChild("Label_tili").GetComponent<UILabel>();
            phypower.text = (string)table["phypower"];
            UILabel hp = next.transform.FindChild("Label_hp").GetComponent<UILabel>();
            hp.text = (string)table["hp"];
            UILabel attack = next.transform.FindChild("Label_attack").GetComponent<UILabel>();
            attack.text = (string)table["attack"];
            UILabel reputation = next.transform.FindChild("Label_reputation").GetComponent<UILabel>();
            reputation.text = (upgradevalue + totalReputation).ToString();
            UILabel experience = next.transform.FindChild("Label_experience").GetComponent<UILabel>();
            experience.text = "+" + (string)table["experience"] + "%";
            UILabel money = next.transform.FindChild("Label_money").GetComponent<UILabel>();
            money.text = "+" + (string)table["money"] + "%";
            UILabel title = next.transform.FindChild("Label_title").GetComponent<UILabel>();
            title.text = (string)table["name"];
        }
        else
            next.SetActive(false);
    }


    void LoadAtlas(ResLoadParams param, Object obj)
    {
        UIAtlas atlas = obj as UIAtlas;
        GameObject headicon = param.userdata0 as GameObject;
        UISprite sprite = headicon.GetComponent<UISprite>();
        sprite.atlas = atlas;
        sprite.spriteName = (string)param.userdata1;
    }

    void OnClick()
    {
        if (gameObject.name == "Button_titleinfoclose")
            sdUICharacter.Instance.ShowPVPTitle(false);
    }
}
