using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void RefreshPTEvent();

public class sdPTManager : Singleton<sdPTManager>
{
    public ushort m_Times;           //试炼次数aaa
    public ushort m_BuyTimes;        //可购买次数aaa
    public byte m_PassLevel;         //通关难度aaaa
    public byte m_ChoiceLevel;       //当前选择的关卡难度aaa

    public byte m_nAttack;    //攻击次数aa

    public RefreshPTEvent RefreshPT;

    public void Refresh()
    {
        if (RefreshPT != null)
            RefreshPT();
    }

    public void AddAttack()
    {
        m_nAttack++;
        GameObject fightUI = sdUICharacter.Instance.GetFightUi();
        if (fightUI != null)
        {
            sdFightUi fight = fightUI.GetComponent<sdFightUi>();
            if (fight != null)
            {
                fight.SetPT(m_nAttack);
            }
        }
    }

    public void Fail()
    {
        sdGameLevel.instance.DestroyFingerObject();
        sdMovieDialogue.BuffChange(true);
        sdUICharacter.Instance.ShowOkMsg(sdConfDataMgr.Instance().GetShowStr("ptfail"), new sdMsgBox.OnConfirm(OnFail));
    }

    void OnFail()
    {
        CliProto.CS_LEVEL_RESULT_NTF refMSG = new CliProto.CS_LEVEL_RESULT_NTF();
        refMSG.m_Result = 1; // 主动放弃当前关卡.
        SDNetGlobal.SendMessage(refMSG);

        sdUICharacter.Instance.ShowPTWnd(true);
    }

 }