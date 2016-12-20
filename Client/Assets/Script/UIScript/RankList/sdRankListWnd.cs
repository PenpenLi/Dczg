using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JobAtlasInfo
{
    public string jobname;
    public UISprite sprite;
}
public class sdRankListWnd : MonoBehaviour
{
    GameObject panel = null;
    GameObject item = null;
    UILabel lb_selfRank = null;
    UISprite sprite_pk = null;
    UISprite sprite_level = null;
    UISprite sprite_pvp = null;
    UISprite sprite_reputation = null;

    UISprite sprite_pk_foreground = null;
    UISprite sprite_level_foreground = null;
    UISprite sprite_pvp_foreground = null;
    UISprite sprite_reputation_foreground = null;
    UILabel lb_title = null;

    List<JobAtlasInfo> lstjobAtlas = new List<JobAtlasInfo>();
     void Awake()
    {
        GameObject root = GameObject.Find("$ranklist(Clone)");
        panel = root.transform.FindChild("panel").gameObject;
        item = panel.transform.FindChild("item0").gameObject;
        lb_selfRank = root.transform.FindChild("panel_botton").FindChild("Label_selfRank").GetComponent<UILabel>();
        sprite_pk = root.transform.FindChild("btn_ranklist_pk").FindChild("Background").GetComponent<UISprite>();
        sprite_level = root.transform.FindChild("btn_ranklist_level").FindChild("Background").GetComponent<UISprite>();
        sprite_pvp = root.transform.FindChild("btn_ranklist_pvp").FindChild("Background").GetComponent<UISprite>();
        sprite_reputation = root.transform.FindChild("btn_ranklist_reputation").FindChild("Background").GetComponent<UISprite>();

        sprite_pk_foreground = root.transform.FindChild("btn_ranklist_pk").FindChild("foreground").GetComponent<UISprite>();
        sprite_level_foreground = root.transform.FindChild("btn_ranklist_level").FindChild("foreground").GetComponent<UISprite>();
        sprite_pvp_foreground = root.transform.FindChild("btn_ranklist_pvp").FindChild("foreground").GetComponent<UISprite>();
        sprite_reputation_foreground = root.transform.FindChild("btn_ranklist_reputation").FindChild("foreground").GetComponent<UISprite>();

        lb_title = root.transform.FindChild("FullscreenFrame").FindChild("Sprite_headline").FindChild("Label_attack").GetComponent<UILabel>();

        sdRankListMgr.Instance.RefreshEvent += Refresh;
    }
    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        for (int index = 0; index < lstjobAtlas.Count;)
        {
            UIAtlas atlas = sdConfDataMgr.Instance().GetAtlas(lstjobAtlas[index].jobname);
            if (atlas != null)
            {
                lstjobAtlas[index].sprite.atlas = atlas;
                lstjobAtlas.RemoveAt(index);
            }
            else
                ++index;
        }
    }

    void UpdateBtnSprite()
    {
        switch (sdRankListMgr.Instance.rankType)
        {
            case HeaderProto.ERankType.RANKTYPE_ATTACK:
                {
                    sprite_pk.spriteName = "btn_Tab_click";
                    sprite_level.spriteName = "btn_Tab_nml";
                    sprite_pvp.spriteName = "btn_Tab_nml";
                    sprite_reputation.spriteName = "btn_Tab_nml";

                    sprite_pk_foreground.spriteName = "zlb1";
                    sprite_level_foreground.spriteName = "djb2";
                    sprite_pvp_foreground.spriteName = "zjb2";
                    sprite_reputation_foreground.spriteName = "swb2";
                    lb_title.text = "战斗力";
                }
                break;
            case HeaderProto.ERankType.RANKTYPE_LEVEL:
                {
                    sprite_pk.spriteName = "btn_Tab_nml";
                    sprite_level.spriteName = "btn_Tab_click";
                    sprite_pvp.spriteName = "btn_Tab_nml";
                    sprite_reputation.spriteName = "btn_Tab_nml";

                    sprite_pk_foreground.spriteName = "zlb2";
                    sprite_level_foreground.spriteName = "djb1";
                    sprite_pvp_foreground.spriteName = "zjb2";
                    sprite_reputation_foreground.spriteName = "swb2";
                    lb_title.text = "等级";
                }
                break;
            case HeaderProto.ERankType.RANKTYPE_PVPWINS:
                {
                    sprite_pk.spriteName = "btn_Tab_nml";
                    sprite_level.spriteName = "btn_Tab_nml";
                    sprite_pvp.spriteName = "btn_Tab_click";
                    sprite_reputation.spriteName = "btn_Tab_nml";

                    sprite_pk_foreground.spriteName = "zlb2";
                    sprite_level_foreground.spriteName = "djb2";
                    sprite_pvp_foreground.spriteName = "zjb1";
                    sprite_reputation_foreground.spriteName = "swb2";
                    lb_title.text = "战绩";
                }
                break;
            case HeaderProto.ERankType.RANKTYPE_PVPREPUTE:
                {
                    sprite_pk.spriteName = "btn_Tab_nml";
                    sprite_level.spriteName = "btn_Tab_nml";
                    sprite_pvp.spriteName = "btn_Tab_nml";
                    sprite_reputation.spriteName = "btn_Tab_click";

                    sprite_pk_foreground.spriteName = "zlb2";
                    sprite_level_foreground.spriteName = "djb2";
                    sprite_pvp_foreground.spriteName = "zjb2";
                    sprite_reputation_foreground.spriteName = "swb1";
                    lb_title.text = "声望";
                }
                break;
        }
    }

    public void Refresh()
    {
        if (panel == null)
            return;
        UpdateBtnSprite();
        List<sdFriend> pklist = sdRankListMgr.Instance.m_Avatar;
        int nChildCount = panel.transform.childCount;
        int index = 0;
        if (sdRankListMgr.Instance.m_SelfRank == -1)
            lb_selfRank.text = "10000名之后";
        else
            lb_selfRank.text = (sdRankListMgr.Instance.m_SelfRank + 1).ToString();
        lstjobAtlas.Clear();
        for (; index < sdRankListMgr.Instance.m_Count; ++index)
        {
            sdFriend roleInfo = pklist[index];
            GameObject uiItem = null;
            Transform tranChild = panel.transform.FindChild("item" + index.ToString());
            if (tranChild != null)
                uiItem = tranChild.gameObject;
            else
            {
                uiItem = GameObject.Instantiate(item) as GameObject;
                uiItem.name = "item" + index.ToString();
                uiItem.transform.parent = panel.transform;
                uiItem.transform.localScale = Vector3.one;
                uiItem.transform.localPosition = new Vector3(item.transform.localPosition.x, item.transform.localPosition.y - index * 100, item.transform.localPosition.z);
                uiItem.transform.localRotation = Quaternion.identity;
            }
            uiItem.SetActive(true);

            GameObject spriterank_obj = uiItem.transform.FindChild("Sprite_rank").gameObject;
            GameObject lbrank_ojb = uiItem.transform.FindChild("ranking").gameObject;

            spriterank_obj.SetActive(true);
            UISprite spriterank = spriterank_obj.GetComponent<UISprite>();
            if (index == 0)
                spriterank.spriteName = "h";
            else if (index == 1)
                spriterank.spriteName = "hu";
            else if (index == 2)
                spriterank.spriteName = "l";
            else
                spriterank_obj.SetActive(false);  
            lbrank_ojb.SetActive(true);
            UILabel lbrank = lbrank_ojb.GetComponent<UILabel>();
            lbrank.text = (index + 1).ToString();
            UILabel lbname = uiItem.transform.FindChild("name").GetComponent<UILabel>();
            lbname.text = roleInfo.name;

            UILabel lbpkvalue = uiItem.transform.FindChild("pkvalue").GetComponent<UILabel>();
            switch (sdRankListMgr.Instance.rankType)
            {
                case HeaderProto.ERankType.RANKTYPE_ATTACK:
                    lbpkvalue.text = roleInfo.power.ToString();
                    break;
                case HeaderProto.ERankType.RANKTYPE_LEVEL:
                    lbpkvalue.text = roleInfo.level;
                    break;
                case HeaderProto.ERankType.RANKTYPE_PVPREPUTE:
                    lbpkvalue.text = roleInfo.pvprepute.ToString();
                    break;
                case HeaderProto.ERankType.RANKTYPE_PVPWINS:
                    lbpkvalue.text = roleInfo.pvpwin.ToString();
                    break;
            }
            

            //UILabel lbprofession = uiItem.transform.FindChild("profession").GetComponent<UILabel>();
            string str = sdConfDataMgr.Instance().GetProfessionText(int.Parse(roleInfo.job));
            //lbprofession.text = sdConfDataMgr.Instance().GetProfessionText(int.Parse(roleInfo.job));
            UISprite spriteprofession = uiItem.transform.FindChild("Sprite_profession").GetComponent<UISprite>();
            UIAtlas jobAtlas = sdConfDataMgr.Instance().GetAtlas(roleInfo.job);
            if (jobAtlas != null)
                spriteprofession.atlas = jobAtlas;
            else
            {
                sdConfDataMgr.Instance().LoadJobAtlas(int.Parse(roleInfo.job));
                JobAtlasInfo item = new JobAtlasInfo();
                item.jobname = roleInfo.job;
                item.sprite = spriteprofession;
                lstjobAtlas.Add(item);
            }
            spriteprofession.spriteName = sdConfDataMgr.Instance().GetJobIcon(roleInfo.job);
        }
        //多余的隐藏aaa
        for (; index < nChildCount; ++index)
        {
            panel.transform.FindChild("item" + index.ToString()).gameObject.SetActive(false);
        }
    }
}
