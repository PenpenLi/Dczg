using UnityEngine;
using System.Collections;

public class sdPVPTitleUpWnd : MonoBehaviour
{

    // Use this for initialization
    GameObject root = null;
    void Awake()
    {
        root = GameObject.Find("Sprite_titleupgrade_bg");
    }
    void Start()
    {

    }

    public void Refresh()
    {
        sdResourceMgr.Instance.LoadResource("UI/$PVP/pvp.prefab", LoadAtlas, null, typeof(UIAtlas));
        Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
        Hashtable table = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel).ToString()] as Hashtable;

        int totalReputation = 0;
        if (sdPVPManager.Instance.nMilitaryLevel > 1)
        {
            for (int index = 1; index < sdPVPManager.Instance.nMilitaryLevel; index++) //计算当前等级之前的声望值aaa
            {
                Hashtable militaryTable = militarylevelTable[index.ToString()] as Hashtable;
                totalReputation += int.Parse((string)militaryTable["reputation"]);
            }
        }

        UILabel phypower = root.transform.FindChild("Label_tili").GetComponent<UILabel>();
        phypower.text =(string)table["phypower"];
        UILabel hp = root.transform.FindChild("Label_hp").GetComponent<UILabel>();
        hp.text = (string)table["hp"];
        UILabel attack = root.transform.FindChild("Label_attack").GetComponent<UILabel>();
        attack.text = (string)table["attack"];
        UILabel reputation = root.transform.FindChild("Label_reputation").GetComponent<UILabel>();
        reputation.text = totalReputation.ToString();
        UILabel experience = root.transform.FindChild("Label_experience").GetComponent<UILabel>();
        experience.text = "+" + (string)table["experience"] + "%";
        UILabel money = root.transform.FindChild("Label_money").GetComponent<UILabel>();
        money.text = "+" + (string)table["money"] + "%";
        UILabel title = root.transform.FindChild("Label_congratulation").GetComponent<UILabel>();
        title.text = "恭喜你获得" + (string)table["name"] + "军阶";

        UILabel titleinfo = root.transform.FindChild("Label_title").GetComponent<UILabel>();
        titleinfo.text = (string)table["name"];
    }

    void LoadAtlas(ResLoadParams param, Object obj)
    {
        UIAtlas atlas = obj as UIAtlas;
        UISprite headicon = root.transform.FindChild("Sprite_headicon1").GetComponent<UISprite>();
        headicon.atlas = atlas;
        Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
        Hashtable table = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel).ToString()] as Hashtable;
        headicon.spriteName = (string)table["icon"];
    }

    void OnClick()
    {
        if (gameObject.name == "Button_titleupgradeclose")
            sdUICharacter.Instance.ShowPVPTitleUpWnd(false);
    }
}
