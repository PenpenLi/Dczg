using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ActorPropertyMonitor : MonoBehaviour {
    sdGameActor actor = null;
	// Use this for initialization
    public int Level;	/// 当前等级         .
    //public byte BaseJob;	/// 基础门派         .
    //public byte Job;	/// 当前门派         .
    //public byte Sex;	/// 对象性别         .
    //public Int64 Experience;	/// 当前等级累计经验  .
    public uint NonMoney;	/// 绑定游戏币（金币）  .
    public uint NonCash;	/// 绑定勋章         .
    public uint Cash;	/// 流通勋章         .
    public int HP;	/// 当前血量         .
    public int SP;	/// 当前技力         .
    public int EP;	/// 当前活力值       .
    public int MoveSpeed;	/// 基础移动速度 1秒移动多少mm  .
    public int AttSpeed;	/// 攻击速度         .
    public int Str;	/// 当前力量         .
    public int Int;	/// 当前智力         .
    public int Dex;	/// 当前敏捷         .
    public int Sta;	/// 当前体力         .
    public int Fai;	/// 当前信念         .
    public int MaxHP;	/// 血量上限         .
    public int MaxSP;	/// 技力上限         .
    public int HPTick;	/// 每轮回复血量     .
    public int SPTick;	/// 每轮回复技力量   .
    public int AtkDmgMin;	/// 当前伤害下限     .
    public int AtkDmgMax;	/// 当前伤害上限     .
    public int Def;	/// 当前防御         .
    public int IceAtt;	/// 冰属性攻击       .
    public int FireAtt;	/// 火属性攻击       .
    public int PoisonAtt;	/// 毒属性攻击       .
    public int ThunderAtt;	/// 雷属性攻击       .
    public int IceDef;	/// 冰属性抵抗       .
    public int FireDef;	/// 火属性抵抗       .
    public int PoisonDef;	/// 毒属性抵抗       .
    public int ThunderDef;	/// 雷属性抵抗       .
    public int Pierce;	/// 穿透点数         .
    public int Hit;	/// 命中点数         .
    public int Dodge;	/// 闪避点数         .
    public int Cri;	/// 致命一击点数     .
    public int Flex;	/// 韧性点数         .
    public int CriDmg;	/// 致命一击伤害系数 15000 = 1.5  .
    public int CriDmgDef;	/// 致命一击防御系数修正 正负 万分比  .
    public int BodySize;	/// 体型半径 mm      .
    public int AttSize;	/// 攻击半径 mm      .
    public int AttSpeedModPer;	/// 攻击加速百分比   .
    public int MoveSpeedModPer;	/// 移动加速百分比   .
    public int PiercePer;	/// 穿透百分比修正 正负 万分比  .
    public int HitPer;	/// 命中率修正  正负  万分比  .
    public int DodgePer;	/// 闪避率修正 正负  万分比  .
    public int CriPer;	/// 致命一击率修正 正负 万分比  .
    public int FlexPer;	/// 韧性率修正  正负 万分比  .
    public int MaxEP;	/// 最大活力值       .
    public int AP;	/// 协助点数         .
    public int MaxAP;	/// 最大协助点数     .
    public int ExpPer;	/// 经验值 正负 万分比  .
    public int MoneyPer;	/// 金钱 正负 万分比   .
                            /// 
    public bool STAY = false;
    public bool HOLD = false;
    public bool STUN = false;
    public bool LIMIT_SKILL = false;
    public bool UNBEAT = false;
    public bool KNOCKBACK = false;

    public string[] buff = null;

	void Start () {
        actor = gameObject.GetComponent<sdGameActor>();
        //property = actor.Property;
	}
	
	// Update is called once per frame
	void Update () {
        if (actor != null)
        { 
            foreach(DictionaryEntry e in actor.Property)
            {
                sdConfDataMgr.SetMemberValue(this, e.Key as string, e.Value);
            }
        }
        
        { 
            List<sdBuff> lstBuff    =   actor.GetBuffs();
            if (lstBuff.Count > 0)
            {
                Hashtable table = (Hashtable)sdConfDataMgr.Instance().GetTable("buff");
                if (buff==null)
                {
                    buff = new string[lstBuff.Count];
                }
                if (buff.Length != lstBuff.Count)
                {
                    buff = new string[lstBuff.Count];
                }
                for (int i = 0; i < lstBuff.Count; i++)
                {
                    sdBuff sbuff = lstBuff[i];
                    int id = sbuff.GetTemplateID();
                    Hashtable prop = table[id.ToString()] as Hashtable;

                    buff[i] = id.ToString() + "_" +
                                prop["szName[ROLE_NAME_LEN]"] as string + "_" +
                                prop["szDescription[DESCRIPTION_LEN]"] as string;
                }
            }
            else
            {
                buff = null;
            }
        }

        STAY        = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STAY);
        HOLD        = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_HOLD);
        STUN        = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_STUN);
        LIMIT_SKILL = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_LIMIT_SKILL);
        UNBEAT      = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_UNBEAT);
        KNOCKBACK   = actor.CheckDebuffState(HeaderProto.ECreatureActionState.CREATURE_ACTION_STATE_KNOCKBACK);
	}
}
