using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public struct sdChatInfo
{
    public byte channelType;
    public string from;
    public string txt;
    public string to;
}

public class sdChatMgr : Singleton<sdChatMgr>
{
    public string color_Word = "[FFFFFF]";
    public string color_World = "[1DB2FF]";
    public string color_Guild = "[1DFF12]";
    public string color_Private = "[B142FF]";
    public string color_Self = "[FFF280]";
    public string color_System = "[EA491D]";

    List<sdChatInfo> chatList = new List<sdChatInfo>();

    public void AddChatInfo(byte channelType, string from, string to,string txt)
    {
        sdChatInfo info = new sdChatInfo();
        info.channelType = channelType;
        info.from = from;
        info.txt = txt;
        info.to = to;
        if (chatList.Count >= 50)
        {
            chatList.Remove(chatList[0]);
        }
        chatList.Add(info);

        string str = "";
        switch (info.channelType)
        {
            case (byte)HeaderProto.EChatType.CHAT_TYPE_SYSTEM:
                {
                    str = string.Format("{0}[{1}]:{2}{3}", color_System, sdConfDataMgr.Instance().GetShowStr("System"), color_System, info.txt);
                    break;
                }
            case (byte)HeaderProto.EChatType.CHAT_TYPE_WORLD:
                {
                    str = string.Format("{0}[{1}]{2}:{3}{4}", color_World, sdConfDataMgr.Instance().GetShowStr("World"), info.from, color_Word, info.txt);
                    break;
                }
            case (byte)HeaderProto.EChatType.CHAT_TYPE_GUILD:
                {
                    break;
                }
            case (byte)HeaderProto.EChatType.CHAT_TYPE_GROUP:
                {
                    break;
                }
            case (byte)HeaderProto.EChatType.CHAT_TYPE_PRIVATE:
                {
                    break;
                }
        }

        sdUICharacter.Instance.AddChatInfo(str);
        if (sdUICharacter.Instance.GetTownUI() != null)
        {
            sdTown town = sdUICharacter.Instance.GetTownUI().GetComponent<sdTown>();
            town.AddChat(str);
        }
    }

    public List<string> GetChatInfo()
    {
        List<string> retList = new List<string>();
        foreach (sdChatInfo info in chatList)
        {
            string str = "";
            switch (info.channelType)
            {
                case (byte)HeaderProto.EChatType.CHAT_TYPE_SYSTEM:
                    {
                        str = string.Format("{0}[{1}]: {2}{3}", color_System, sdConfDataMgr.Instance().GetShowStr("System"), color_System, info.txt);
                        break;
                    }
                case (byte)HeaderProto.EChatType.CHAT_TYPE_WORLD:
                    {
                        str = string.Format("{0}[{1}]{2}: {3}{4}", color_World, sdConfDataMgr.Instance().GetShowStr("World"), info.from, color_Word, info.txt);
                        break;
                    }
                case (byte)HeaderProto.EChatType.CHAT_TYPE_GUILD:
                    {
                        break;
                    }
                case (byte)HeaderProto.EChatType.CHAT_TYPE_GROUP:
                    {
                        break;
                    }
                case (byte)HeaderProto.EChatType.CHAT_TYPE_PRIVATE:
                    {
                        break;
                    }
            }

            if (str.Trim() != "")
            {
                retList.Add(str);
            }
            
        }
        return retList;
    }
}
