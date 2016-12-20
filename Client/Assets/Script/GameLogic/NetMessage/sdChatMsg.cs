
using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class sdChatMsg : UnityEngine.Object 
{
	public static bool init()
	{
		SDGlobal.Log("sdChatMsg.init");
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_CHAT, msg_SCID_CHAT);
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.SCID_CLIENT_CONFIG_NTF, msg_SCID_CLIENT_CONFIG_NTF);
		return true;
	}

    static void msg_SCID_CLIENT_CONFIG_NTF(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_CLIENT_CONFIG_NTF refMSG = (CliProto.SC_CLIENT_CONFIG_NTF)msg;
        string config = Encoding.UTF8.GetString(refMSG.m_ConfigInfo);
        sdConfDataMgr.Instance().LoadRoleSetting(config);
    }

    public static bool notifyUpdateConfig(string info)
    {
        CliProto.CS_CLIENT_CONFIG_UPDATE refMsg = new CliProto.CS_CLIENT_CONFIG_UPDATE();
        refMsg.m_ConfigInfo = Encoding.UTF8.GetBytes(info);
        SDNetGlobal.SendMessage(refMsg);
        return true;
    }

    static void msg_SCID_CHAT(int iMsgID, ref CMessage msg)
    {
        CliProto.SC_CHAT refMSG = (CliProto.SC_CHAT)msg;
        if (Encoding.UTF8.GetString(refMSG.m_Chat.m_Who) == sdGameLevel.instance.mainChar.Name)
        {
            return;
        }
        sdChatMgr.Instance.AddChatInfo(refMSG.m_Chat.m_ChatType, Encoding.UTF8.GetString(refMSG.m_Chat.m_Who), Encoding.UTF8.GetString(refMSG.m_Chat.m_ToWho), Encoding.UTF8.GetString(refMSG.m_Chat.m_Content));
//         switch (refMSG.m_Chat.m_ChatType)
//         {
//             case (byte)HeaderProto.EChatType.CHAT_TYPE_SYSTEM:
//                 {
//                     //sdUICharacter.Instance.ShowMsgLine(Encoding.UTF8.GetString(refMSG.m_Chat.m_Content), Color.yellow);
//                     
//                     break;
//                 }
//             case (byte)HeaderProto.EChatType.CHAT_TYPE_WORLD:
//                 {
//                     break;
//                 }
//             case (byte)HeaderProto.EChatType.CHAT_TYPE_GUILD:
//                 {
//                     break;
//                 }
//             case (byte)HeaderProto.EChatType.CHAT_TYPE_GROUP:
//                 {
//                     break;
//                 }
//             case (byte)HeaderProto.EChatType.CHAT_TYPE_PRIVATE:
//                 {
//                     break;
//                 }
//             default:
//                 {
//                     if (sdGameLevel.instance.levelType != sdGameLevel.LevelType.MainCity && sdGameLevel.instance.levelType != sdGameLevel.LevelType.WorldMap)
//                         return;
//                     sdUICharacter.Instance.m_lstScrollText.Add(Encoding.UTF8.GetString(refMSG.m_Chat.m_Content));
//                     break;
//                 }
 //       }
    }

    public static bool SendChat(HeaderProto.EChatType type ,string info, string toWho)
	{
		CliProto.CS_CHAT refMSG = new CliProto.CS_CHAT();
        refMSG.m_Chat.m_ChatType = (byte)type;
        refMSG.m_Chat.m_Content = Encoding.UTF8.GetBytes(info);
        refMSG.m_Chat.m_ToWho = Encoding.UTF8.GetBytes(toWho);
		SDNetGlobal.SendMessage(refMSG);
		return true;
	}
}