using UnityEngine;
using System.Collections;
using System;
using System.Text;

public class sdBBSMsg : UnityEngine.Object 
{
    public static bool init()
    {
        SDGlobal.Log("sdbbsmsg init");
        SDNetGlobal.setCallBackFunc((ushort)CliProto.EN_CliProto_MessageID.GCID_PUBLIC_STRING, msg_GCID_PUBLIC_STRING);
        return true;
    }

    static void msg_GCID_PUBLIC_STRING(int iMsgID, ref CMessage msg)
    {
        CliProto.GC_PUBLIC_STRING refMsg = (CliProto.GC_PUBLIC_STRING)msg;
        sdUICharacter.Instance.m_strbbs = Encoding.UTF8.GetString(refMsg.m_Info);
    }
}
