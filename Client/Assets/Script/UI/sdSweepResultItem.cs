using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;



public class sdSweepResultItem : MonoBehaviour
{
    public sdSweepResultIcon[] table = new sdSweepResultIcon[5];
    public UILabel moneyLbl = null;
    public UILabel expLbl = null;
    public UILabel title = null;
    public int index = 0;

    public void SetInfo(CliProto.SC_TREASURE_CHEST_NTF msg)
    {
        float expRate = 1;
        float moneyRate = 1;
        Hashtable militarylevelTable = sdConfDataMgr.Instance().GetTable("militarylevel");
        if (militarylevelTable.ContainsKey(sdPVPManager.Instance.nMilitaryLevel.ToString()))
        {
            Hashtable military = militarylevelTable[(sdPVPManager.Instance.nMilitaryLevel).ToString()] as Hashtable;
            expRate = float.Parse(military["experience"].ToString()) / 100 + 1;
            moneyRate = float.Parse(military["money"].ToString()) / 100 + 1;
        }

        int exp = (int)(((float)msg.m_Experience) * expRate);
        int money = (int)(((float)msg.m_Money) * moneyRate);

        expLbl.text = exp.ToString();
        moneyLbl.text = moneyLbl.ToString();

        int slotNum = 0;
        int num = (int)msg.m_Items.m_ItemCount;
        List<sdGameItem> itemList = new List<sdGameItem>();
        List<sdGamePetItem> petItemList = new List<sdGamePetItem>();
        List<sdGameItem> petCardList = new List<sdGameItem>();
        for (int i = 0; i < num; ++i)
        {
            sdGameItem item = sdGameItemMgr.Instance.getItem(msg.m_Items.m_Items[i].m_UID);
            if (item == null)
            {
                sdGamePetItem petItem = sdNewPetMgr.Instance.getPetItem(msg.m_Items.m_Items[i].m_UID);
                if (petItem == null)
                {
                    sdGameItem temp = new sdGameItem();
                    temp.templateID = msg.m_Items.m_Items[i].m_TID;
                    Hashtable tempInfo = sdConfDataMgr.Instance().GetItemById(msg.m_Items.m_Items[i].m_TID.ToString());
                    if (tempInfo == null) continue;
                    if (int.Parse(tempInfo["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
                    {
                        petCardList.Add(temp);
                    }
                    else
                    {
                        itemList.Add(temp);
                    }

                    continue;
                }
                petItemList.Add(petItem);
                continue;
            }
            itemList.Add(item);
        }

        num = msg.m_PetAboutCount;
        for (int i = 0; i < num; ++i)
        {
            if (table.Length > slotNum && table[slotNum] != null)
            {
                sdSweepResultIcon icon = table[slotNum];
                if (icon != null)
                {
                    Hashtable pet = sdConfDataMgr.Instance().GetItemById(msg.m_PetAbout[i].ToString());
                    if (pet == null) continue;
                    if (int.Parse(pet["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Equip)
                    {
                        icon.SetInfo(msg.m_PetAbout[i]);
                    }
                    else if (int.Parse(pet["Class"].ToString()) == (int)HeaderProto.EItemClass.ItemClass_Pet_Item)
                    {
                        icon.SetInfo(msg.m_PetAbout[i]);
                    }
                }
            }
            ++slotNum;
        }

        foreach (sdGameItem item in petCardList)
        {
            if (table.Length > slotNum && table[slotNum] != null)
            {
                sdSweepResultIcon icon = table[slotNum];
                if (icon != null)
                {
                    if (item != null)
                    {
                        icon.SetInfo(item.instanceID);
                    }
                }
            }
            ++slotNum;
        }

        foreach (sdGamePetItem item in petItemList)
        {
            if (table.Length > slotNum && table[slotNum] != null)
            {
                sdSweepResultIcon icon = table[slotNum];
                if (icon != null)
                {
                    if (item != null)
                    {
                        icon.SetInfo(item.instanceID);
                    }
                }
            }
            ++slotNum;
        }

        //itemList.Sort(CompareItem);

        foreach (sdGameItem item in itemList)
        {
            if (table.Length > slotNum && table[slotNum] != null)
            {
                sdSweepResultIcon icon = table[slotNum];
                if (icon != null)
                {
                    if (item != null)
                    {
                        icon.SetInfo(item.instanceID);
                    }
                }
            }
            ++slotNum;
        }

        while (table.Length > slotNum && table[slotNum] != null)
        {
            sdSweepResultIcon icon = table[slotNum];
            if (icon != null)
            {
                icon.SetInfo(0);
            }
            ++slotNum;
        }

        int treasureNum = msg.m_GetCount;
        for (int i = 0; i < treasureNum; ++i)
        {
            sdSweepResultIcon icon = table[4];
            icon.SetInfo(msg.m_CardInfos[i].m_ItemID);
        }
    }
}
