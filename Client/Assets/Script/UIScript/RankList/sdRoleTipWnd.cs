using UnityEngine;
using System.Collections;

//查看其他角色的窗口aaa
public class sdRoleTipWnd : MonoBehaviour
{
    sdFriAvatar avatar = new sdFriAvatar();
    GameObject UI_avatar = null;
    UILabel lb_name = null;
    UILabel lb_power = null;
    sdSlotIcon[] euqipSlot = new sdSlotIcon[10];
    public string m_strName = null;
    UISprite sprite_profession = null;

    GameObject equipPanel = null;
    GameObject petPanel = null;
    GameObject petTemplate = null;
    GameObject[] btnPanel = new GameObject[2];

    GameObject tab_equip = null;
    GameObject tab_fashion = null;
    GameObject tab_pet = null;
    sdGameActorCreateInfo m_kInfo = null;
    uint[] m_equiplist = null;

    int nTab = 0;

    static string[] spritename_level = {"IconL2w", "IconL2g", "IconL2b", "IconL2p", "IconL2y"};

    void Awake()
    {
        GameObject root = GameObject.Find("roletipwnd_bg");
        UI_avatar = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("equip").FindChild("avatar").FindChild("Sprite_avatar").gameObject;
        lb_name = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("label_name").GetComponent<UILabel>();
        lb_power = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("equip").FindChild("label_point").GetComponent<UILabel>();
        Transform equippanel = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("equip").FindChild("equipPanel");
        sprite_profession = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("equip").FindChild("avatar").FindChild("Sprite_profession").GetComponent<UISprite>();
        euqipSlot[9] = equippanel.FindChild("offhand").GetComponent<sdSlotIcon>();
        euqipSlot[6] = equippanel.FindChild("ring1").GetComponent<sdSlotIcon>();
        euqipSlot[7] = equippanel.FindChild("ring2").GetComponent<sdSlotIcon>();
        euqipSlot[2] = equippanel.FindChild("clothes").GetComponent<sdSlotIcon>();
        euqipSlot[3] = equippanel.FindChild("gloves").GetComponent<sdSlotIcon>();
        euqipSlot[4] = equippanel.FindChild("pants").GetComponent<sdSlotIcon>();
        euqipSlot[0] = equippanel.FindChild("weapon").GetComponent<sdSlotIcon>();
        euqipSlot[5] = equippanel.FindChild("cloak").GetComponent<sdSlotIcon>();
        euqipSlot[1] = equippanel.FindChild("hat").GetComponent<sdSlotIcon>();
        euqipSlot[8] = equippanel.FindChild("jewelry").GetComponent<sdSlotIcon>();

        equipPanel = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("equip").gameObject;
        petPanel = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("pet").gameObject;
        petTemplate = petPanel.transform.FindChild("item0").gameObject;

        Transform parent = GameObject.Find("$RoleTipWnd(Clone)").transform;
        btnPanel[0] = parent.FindChild("btnpanel_firend").gameObject;
        btnPanel[1] = parent.FindChild("btnpanel_ranklist").gameObject;

        tab_equip = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("tab_equip_roletip").gameObject;
        tab_fashion = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("tab_fashion_roletip").gameObject;
        tab_pet = root.transform.FindChild("bg2").FindChild("avatarbg").FindChild("tab_pet_roletip").gameObject;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowEquip()
    {
        nTab = 0;
        equipPanel.SetActive(true);
        petPanel.SetActive(false);
        if (avatar != null)
            avatar.PlayIdle();
    }

    public void ShowFashion()
    {

    }
    public void ShowPet()
    {
        nTab = 1;
        equipPanel.SetActive(false);
        petPanel.SetActive(true);
    }

    //临时代码aaa
    public void Keep()
    {
        if (nTab == 0)
        {
            tab_equip.GetComponent<sdRadioButton>().Active(true);
            tab_fashion.GetComponent<sdRadioButton>().Active(false);
            tab_pet.GetComponent<sdRadioButton>().Active(false);
        }
        else
        {
            tab_equip.GetComponent<sdRadioButton>().Active(false);
            tab_fashion.GetComponent<sdRadioButton>().Active(false);
            tab_pet.GetComponent<sdRadioButton>().Active(true);
        }
    }

    public void ShowAvatar()
    {
        if (UI_avatar != null)
        {
            UI_avatar.SetActive(true);
            avatar = UI_avatar.AddComponent<sdFriAvatar>();
            avatar.init(m_kInfo);
            avatar.updateAvatar(m_equiplist, (uint)m_equiplist.Length);
            avatar.RequestEnd();            
        }
    }

    public void HideAvatar()
    {
        if (UI_avatar != null)
            UI_avatar.SetActive(false);
    }

    public void Refresh(sdFriend roleInfo, int nType)
    {
        for (int i = 0; i < btnPanel.Length; ++i)
            btnPanel[i].SetActive(i == nType);
        lb_name.text = roleInfo.name;
        //euqip
        if (UI_avatar != null)
        {
            UI_avatar.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
            for (int i = 0; i < UI_avatar.transform.childCount; ++i)
            {
                if (UI_avatar.transform.GetChild(i).gameObject != null)
                    GameObject.Destroy(UI_avatar.transform.GetChild(i).gameObject);
            }
            m_kInfo = new sdGameActorCreateInfo();
            m_kInfo.mDBID = ulong.Parse(roleInfo.id);
            m_kInfo.mGender = roleInfo.gender;
            m_kInfo.mHairStyle = roleInfo.hairStyle;
            m_kInfo.mSkinColor = roleInfo.color;
            m_kInfo.mBaseJob = byte.Parse(roleInfo.job);
            m_kInfo.mJob = byte.Parse(roleInfo.job);
            m_kInfo.mLevel = int.Parse(roleInfo.level);

            m_equiplist = roleInfo.equipList.ToArray();
            
            m_strName = roleInfo.name;
            //lb_power.gameObject.SetActive(int.Parse(roleInfo.power) != 0);
            lb_power.text = "战斗力 " + roleInfo.power;

            string name = "";
            int job = int.Parse(roleInfo.job);
            switch (job)
            {
                case (int)HeaderProto.ERoleJob.ROLE_JOB_Warrior:
                    {
                        name = "warrior";
                        break;
                    }
                case (int)HeaderProto.ERoleJob.ROLE_JOB_Magic:
                    {
                        name = "magic";
                        break;
                    }
                case (int)HeaderProto.ERoleJob.ROLE_JOB_Rogue:
                    {
                        name = "rogue";
                        break;
                    }
                case (int)HeaderProto.ERoleJob.ROLE_JOB_Minister:
                    {
                        name = "minister";
                        break;
                    }
            }
            ResLoadParams para = new ResLoadParams();
            para.info = "back";
            para.userdata0 = job;
            para.userdata1 = false;
            string namePreb = string.Format("UI/Icon/$icon_{0}_back/icon_{0}_back.prefab", name);
            sdUICharacter.Instance.LoadAtlas(namePreb, sprite_profession, sdConfDataMgr.Instance().GetJobBack(roleInfo.job));
            UpdateEquip(roleInfo);
        }
        //fashion

        //pet
        int nChildCount = petPanel.transform.childCount;
        for (int index = 0; index < nChildCount; ++index)
        {
            GameObject uiItem = null;
            Transform trans = petPanel.transform.FindChild("item" + index.ToString());
            if (trans != null)
                uiItem = trans.gameObject;
            else
            {
                uiItem = GameObject.Instantiate(petTemplate) as GameObject;
                uiItem.name = "item" + index.ToString();
                uiItem.transform.parent = petPanel.transform;
                uiItem.transform.localScale = Vector3.one;
                uiItem.transform.localPosition = new Vector3(petTemplate.transform.localPosition.x, petTemplate.transform.localPosition.y - index * 120, petTemplate.transform.localPosition.z);
                uiItem.transform.localRotation = Quaternion.identity;
            }
            uiItem.SetActive(true);
            Transform tran_head = uiItem.transform.FindChild("border").FindChild("head");
            GameObject obj_petName = uiItem.transform.FindChild("name").gameObject;
            UISprite sprite_level = uiItem.transform.FindChild("border").GetComponent<UISprite>();

            UILabel lb_power = uiItem.transform.FindChild("pow").GetComponent<UILabel>();
            UILabel lb_level = uiItem.transform.FindChild("level").GetComponent<UILabel>();
            UILabel lb_intensify = uiItem.transform.FindChild("intensify").GetComponent<UILabel>();
            if (index < roleInfo.petList.Count)
            {
                SClientPetInfo petInfo = roleInfo.petList[index];
                Hashtable Info = sdConfDataMgr.Instance().GetPetTemplate(petInfo.m_uiTemplateID.ToString());
                if (Info == null)
                    continue;
                tran_head.gameObject.SetActive(true);
                sprite_level.gameObject.SetActive(true);
                obj_petName.SetActive(true);
                lb_power.gameObject.SetActive(true);
                lb_level.gameObject.SetActive(true);
                lb_intensify.gameObject.SetActive(true);

                UISprite sprite_head = tran_head.GetComponent<UISprite>();
                sprite_head.spriteName = Info["Icon"].ToString();                           
                int iAbility = int.Parse(Info["Ability"].ToString());                  
                sprite_level.spriteName = spritename_level[iAbility - 1];
                sdNewPetMgr.SetLabelColorByAbility(iAbility, obj_petName);
                obj_petName.GetComponent<UILabel>().text = Info["Name"].ToString();

                //星级aaa
                //int parentWidth = sprite_head.width;
                for (int i = 1; i <= 5; ++i)
                {
                    GameObject obj_start = tran_head.FindChild("start" + i.ToString()).gameObject;
                    obj_start.SetActive(true);
                    int nWidth = obj_start.GetComponent<UISprite>().width * iAbility;
                    obj_start.SetActive(i <= iAbility);

                    Vector3 pos = obj_start.transform.localPosition;
                    obj_start.transform.localPosition = new Vector3(-nWidth / 2 + i * 15, pos.y, pos.z);
                }           
                
                lb_power.text = "战斗力 " + sdConfDataMgr.Instance().GetPetScoreByTemplateID((int)petInfo.m_uiTemplateID, petInfo.m_iUp, petInfo.m_iLevel).ToString();   
                lb_level.text = "等级：" + petInfo.m_iLevel.ToString();               
                lb_intensify.text = "强化：+" + petInfo.m_iUp.ToString();
            }
            else
            {
                tran_head.gameObject.SetActive(false);
                sprite_level.gameObject.SetActive(false);
                obj_petName.SetActive(false);
                lb_power.gameObject.SetActive(false);
                lb_level.gameObject.SetActive(false);
                lb_intensify.gameObject.SetActive(false);
                for (int i = 1; i <= 5; ++i)
                {
                    GameObject obj_start = tran_head.FindChild("start" + i.ToString()).gameObject;
                    obj_start.SetActive(false);
                } 
            }
        }
        ShowEquip();
        tab_equip.GetComponent<sdRadioButton>().Active(true);
        tab_fashion.GetComponent<sdRadioButton>().Active(false);
        tab_pet.GetComponent<sdRadioButton>().Active(false);
    }

    void FixedUpdate()
    {
        //if (UI_avatar != null && UI_avatar.GetComponent<sdFriAvatar>() != null)
         //   UI_avatar.GetComponent<sdFriAvatar>().tickFrame();

    }

    void UpdateEquip(sdFriend roleInfo)
    {
        for (int i = 0; i < euqipSlot.Length; ++i)
        {
            euqipSlot[i].gameObject.transform.FindChild("iconbg").GetComponent<UISprite>().spriteName = "IconFrame1";
        }
        for (int index = 0; index < roleInfo.equipList.Count; ++index)
        {
            uint templateID = roleInfo.equipList[index];
            Hashtable itemTable = sdConfDataMgr.Instance().GetItemById(templateID.ToString());
            if (itemTable != null)
            {
                int pos = int.Parse(itemTable["Character"].ToString());
                euqipSlot[pos].SetInfo(templateID.ToString(), sdConfDataMgr.Instance().GetItemById(templateID.ToString()));
            }
        }
    }

    void OnDrag(Vector2 delta)
    {
        if (gameObject.name == "avatar")
        {
            if (UI_avatar != null)
                UI_avatar.transform.Rotate(new Vector3(0, -delta.x * 0.5f, 0));
        }
    }
}
