using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 聊天窗口aa
/// </summary>
public class sdChatWnd : MonoBehaviour
{
    float sendTime = 0f;
    public UIButton sendBtn = null;
    public sdTextList textList = null;

    void Start()
    {
        sendBtn.onClick.Add(new EventDelegate(OnSendChat));
        Refresh();
    }

    public void AddChatInfo(string txt)
    {
        textList.AddText(txt);
    }

    void OnSendChat()
    {
		string value = GetComponentInChildren<UIInput>().value;
		if (value.Trim() == "") 
			return;

		// 客户端GM命令(不检查GM权限)aa
		if (value.StartsWith("@ccmd"))
		{
			char[] akSeparator = new char[] { ' ' };
			string[] akSpiltValue = value.Split(akSeparator, System.StringSplitOptions.RemoveEmptyEntries);
			if (akSpiltValue.Length > 1) 
			{
				if (akSpiltValue[1].Equals("loadtestmap"))
				{
					BundleGlobal.Instance.StartLoadBundleLevel(
						"Level/testScene/$testScene3.unity.unity3d",
						"$testScene3");
				}
			}
		}

		// GM权限检查aa
        bool isGm = sdUICharacter.Instance.gmLevel > 0 ? true : false;
        if (!isGm)
        {
            if (sdGameLevel.instance.mainChar.Level < 20 )
            {
                sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("ChatLevel"), Color.yellow);
                return;
            }

            float time = Time.time - sendTime;
            if (time < 120)
            {
                sdUICharacter.Instance.ShowMsgLine(string.Format(sdConfDataMgr.Instance().GetShowStr("ChatTime"), (int)(120- time)), Color.yellow);
                return;
            }
        }

		// 排版aa
        string str = string.Format("{0}[{1}]{2}:{3}{4}", sdChatMgr.Instance.color_Self, sdConfDataMgr.Instance().GetShowStr("World"), sdGameLevel.instance.mainChar.Name, sdChatMgr.Instance.color_Word, GetComponentInChildren<UIInput>().value);
		sdTextList txt = GetComponentInChildren<sdTextList>();
        txt.AddText(str);

		// 同步消息到服务器aa
        if (isGm)
        {
            sdChatMsg.SendChat(HeaderProto.EChatType.CHAT_TYPE_SYSTEM, GetComponentInChildren<UIInput>().value, "");
        }
        else
        {
            sdChatMsg.SendChat(HeaderProto.EChatType.CHAT_TYPE_WORLD, GetComponentInChildren<UIInput>().value, "");
        }

		// 清除输入aa
        GetComponentInChildren<UIInput>().value = "";
        GetComponentInChildren<UIInput>().text = "";
    }

    public void Refresh()
    {
        List<string> list = sdChatMgr.Instance.GetChatInfo();
        sdTextList txt = GetComponentInChildren<sdTextList>();
        foreach (string str in list)
        {
            txt.AddText(str);
        }
    }
}
