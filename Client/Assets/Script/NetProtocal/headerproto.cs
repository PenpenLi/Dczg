using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

//      Version Number: 1          
public class HeaderProto{
//Defines Begin
public const int MAX_SERVICENAME_LENGTH         =16;
public const int MAX_SERVICEPARAM_LENGTH        =128;
public const int MAX_PLUGINPACKET_LENGTH        =10240;
public const int MAX_TRANS_SKILL_NUM            =100;
public const int MAX_ERROR_STRING               =4096;
public const int MAX_PET_UP_LEVEL               =120;
public const int MAX_GEM_COUNT                  =3;
public const int MAX_PET_BAG_COUNT              =210;
public const int MAX_PET_EQUIP_COUNT            =3;
public const int MAX_ITEM_COUNT                 =200;
public const int MAX_SUIT_ITEM_COUNT            =20;
public const int MAX_ITEM_UP_USE_COUNT          =6;
public const int MAX_PET_MT_ROLE_LEVEL          =20;
public const int MAX_PROD_ITEM_COUNT            =5;
public const int MAX_EP_COUNT                   =1000;
public const int MAX_MONEY                      =1000000000;
public const int MAX_CASH                       =500000000;
public const int MAX_NONCASH                    =500000000;
public const int MAX_ALLCASH                    =(MAX_CASH + MAX_NONCASH);
public const int INVALID_MSG_ID                 =0;
public const int SWIM_STATE_NEED_WATER_DEPTH    =1500;
public const int ACCOUNT_LEN                    =21;
public const int PTID_LEN                       =21;
public const int ROLE_NAME_LEN                  =21;
public const int PASSWORD_LEN                   =30;
public const int ITEM_NAME_LEN                  =61;
public const int DESCRIPTION_LEN                =255;
public const int MAX_SKILL_COOLDOWN_SIZE        =200;
public const int MAX_SCENE_NAME_LEN             =12;
public const int ACTIVE_ROLE_NUM                =4;
public const int AVATAR_LEN                     =8;
public const int ROLE_MALE                      =0;
public const int ROLE_FEMALE                    =1;
public const int MAX_INSTANCED_COUNT            =20;
public const int MAX_GUILD_NAME_LEN             =21;
public const int MAX_GUILD_LIST_PER_PAGE        =100;
public const int MAX_GUILD_AIM_LEN              =120;
public const int MAX_GUILD_NOTICE_LEN           =120;
public const int MAX_GUILD_LOG_COUNT            =100;
public const int MAX_GUILD_MEMBER_COUNT         =100;
public const int MAX_GUILD_ENTER_COUNT          =50;
public const int MAX_MESSAGE_LEN                =4096;
public const int MAX_MSG_CONTENT_LEN            =4096;
public const int MAX_LINK_SIZE                  =4096;
public const int MAX_XMLDATA_COUNT              =100000;
public const int MAX_LEVEL_COUNT                =512;
public const int MAX_BATTLE_COUNT               =64;
public const int MAX_BATTLE_BOX_COUNT           =256;
public const int MAX_LEVEL_SELECT_CARD_COUNT    =5;
public const int MAX_SCENE_NAME                 =36;
public const int MAX_TASK_ITEM_NUM              =20;
public const int MAX_LOOT_ITEM_NUM              =20;
public const int ROLE_LOOT_ALL_ITEM             =255;
public const int MAX_XML_LEN                    =40960;
public const int MAX_JSON_LEN                   =10240;
public const int MAX_BIGXML_LEN                 =204800;
public const int WORLD_NUM                      =100;
public const int HEAVEN_EXP_WORLD_NUM           =100;
public const int MAX_SEARCHCONTENT_LEN          =17;
public const int MAX_MOUNT_COUNT                =255;
public const int MAX_SERVER_NAME_LEN            =32;
public const int MAX_SERVER_DESC_LEN            =295;
public const int MAX_SERVER_STATE_LEN           =196;
public const int MAX_WORLDCORERANK_COUNT        =100;
public const int MAX_BAG_ITEM_NUM               =108;
public const int MAX_CREATE_WORLD_BAG_ITEM_NUM  =148;
public const int MAX_EQUIP_ITEM_NUM             =10;
public const int MAX_EQUIPANDBAG_ITEM_NUM       =(MAX_BAG_ITEM_NUM+MAX_EQUIP_ITEM_NUM);
public const int MAX_MOUNT_EQUIP_ITEM_NUM       =4;
public const int MAX_ITEM_DESC_LEN              =64;
public const int MAX_BANK_ITEM_NUM              =144;
public const int MAX_SEND_ITEM_NUM              =200;
public const int MAX_ITEM_HOLE_NUM              =3;
public const int MAX_CHEST_LOOT_NUM             =5;
public const int INVALID_EXINFO_INDEX           =255;
public const int INVALID_MAKER_INFO_INDEX       =255;
public const int SELL_COST_MAX_COUNT            =1;
public const int MAX_NPC_WARE_COUNT             =50;
public const int MAX_BUYBACK_COUNT              =24;
public const int MAX_EXCHANGE_SIZE              =8;
public const int MAX_EQUIP_HEAD                 =8;
public const int ITEM_EX_TYPE_EQUIP             =0;
public const int ITEM_EX_TYPE_LOOT              =1;
public const int ITEM_EX_TYPE_NULL              =101;
public const int BATTLE_TROPHY_CLASS_MAX        =8;
public const int MAX_WONDERSTRING_LEN           =128;
public const int MAX_MOVE_INCREASE_LEN          =64;
public const int SERVERSTATE_BIT_IS_QUEUE       =(1<<0);
public const int SQL_SELECT                     =1;
public const int SQL_UPDATE                     =2;
public const int SQL_UPDATE_NORSP               =3;
public const int SQL_INSERT                     =4;
public const int SQL_DELETE                     =5;
public const int SQL_LOCK                       =6;
public const int SQL_UNLOCK                     =7;
public const int OPT_BOTH                       =0;
public const int OPT_MEMORY                     =1;
public const int OPT_DB                         =2;
public const int MAX_BLOB_LEN                   =4096;
public const int MAX_BIGBLOB_LEN                =20480;
public const int MAX_MEDIUMBLOB_LEN             =51200;
public const int MAX_BIG_MORE_BLOB_LEN          =30720;
public const int MAX_EXEBIGBLOB_LEN             =102400;
public const int MAX_ROLE_ITEM_COUNT            =200;
public const int MIN_ROLE_ITEM_COUNT            =110;
public const int MAX_PET_ITEM_COUNT             =100;
public const int MAX_PET_COUNT                  =110;
public const int MAX_PET_RECORD_COUNT           =1000;
public const int MAX_PACKET_LEN                 =(2 * 1024 * 1024);
public const int WORLD_CHAT_COOLDOWN_TIME_MS    =(2 * 60 * 1000);
public const int WORLD_CHAT_LEVEL_LIMIT         =20;
public const int TS_BROADCAST_ID_COUNT          =4096;
public const int TS_BROADCAST_LEFT_SIZE         =(TS_BROADCAST_ID_COUNT * 8 + 1024);
public const int TS_BROADCAST_MSG_SIZE          =(MAX_PACKET_LEN - (TS_BROADCAST_LEFT_SIZE));
public const int MAX_FRIENDS_COUNT              =50;
public const int MAX_FRIENDS_COUNT_PER_PAGE     =20;
public const int MAX_FRIENDS_PET_COUNT          =3;
public const int MAX_FRIENDS_INVITE_COUNT       =30;
public const int MAX_FRIENDASSIST_COUNT         =30;
public const int MAX_BATTLE_PET_COUNT           =3;
public const int MAX_FRIEND_COUNT               =50;
public const int MAX_ABYSS_OPEN_COUNT           =50;
public const int MAX_XABYSS_RECORD_COUNT        =20;
public const int MAX_ABYSS_NAME_STRING_COUNT    =1024;
public const int MAX_MAIL_TITLE_LEN             =100;
public const int MAX_MAIL_CONTENT_LEN           =400;
public const int MAX_MAIL_ITEM_COUNT            =5;
public const int MAX_MAIL_COUNT                 =100;
public const int MAX_RANK_ROLE_COUNT            =10000;
public const int MAX_RANK_PAGE_COUNT            =50;
public const int MAX_SHOP_BUY_PET_RESULT_COUNT  =255;
public const int MAX_SHOP_GOODS_COUNT           =100;
public const int MAX_SHOP_BUY_ITEM_COUNT        =100;
public const int MAX_SHOP_SECRET_ITEM_COUNT     =6;
public const int MAX_GIFT_VIP_COUNT             =31;
public const int MAX_GIFT_LEVEL_COUNT           =100;
public const int MAX_GIFT_LEVEL_ITEM_COUNT      =100;
public const int MAX_DAY_QUEST_COUNT            =100;
public const int MAX_DAY_QUEST_BOX_COUNT        =100;
public const int MAX_JIHUOMA_LEN                =100;
public const int MAX_FOODS_COUNT                =10;
public const int MAX_MONTHCARD_COUNT            =100;
public const int MAX_PVP_MATCH_COUNT            =5;
public const int MAX_PVP_RANK_COUNT             =10000;
public const int MAX_PVP_ROLE_INFO_STRING_COUNT =409600;
public const int MAX_WB_RANK_COUNT              =10;
public const int MAX_WB_ATK_COUNT               =10000;
public const int MAX_PVP_ROBOT_BUFF_COUNT       =3;
public const int MAX_PVP_ROBOT_SKILL_COUNT      =25;
public const	int HEADERPROTO_MSGID_RANGE =1;
public const	int HEADERPROTO_MSGID_OFFSET =0;
//Defines End


/*Enums Begin*/
public enum EGTLSQuerySceneType
{
	QUERY_TYPE_LOGON                 = 0,
	QUERY_TYPE_SWITCH_SCENE          = 1,
	QUERY_TYPE_SWITCH_SCENE_INSTANCE = 2,
}

public enum EEnterGameType
{
	ENTER_GAME_TYPE_LOGON        = 0,
	ENTER_GAME_TYPE_SWITCH_SCENE = 1,
}

public enum ESceneType
{
	SCENE_TYPE_NORMAL   = 0,
	QUERY_TYPE_INSTANCE = 1,
}

public enum EErrorType
{
	ERROR_TYPE_COMMON  = 0,
	ERROR_TYPE_DEBUG   = 1,
	ERROR_TYPE_SHOWBOX = 2,
}

public enum ERankType
{
	RANKTYPE_LEVEL     = 0,
	RANKTYPE_ATTACK    = 1,
	RANKTYPE_PVPWINS   = 2,
	RANKTYPE_PVPREPUTE = 3,
}

public enum EMergeType
{
	MERGETYPE_PET_SUBCARD = 0,
}

public enum EChatType
{
	CHAT_TYPE_SYSTEM  = 0,
	CHAT_TYPE_WORLD   = 1,
	CHAT_TYPE_GUILD   = 2,
	CHAT_TYPE_GROUP   = 3,
	CHAT_TYPE_PRIVATE = 4,
	CHAT_TYPE_MAX     = 4,
}

public enum ESpecialItemID
{
	SPECIALITEMID_nonMoney = 1,
	SPECIALITEMID_nonCash  = 2,
	SPECIALITEMID_cash     = 3,
}

public enum EPetEquipPos
{
	PET_EQUIP_POS1 = 0,
	PET_EQUIP_POS2 = 1,
	PET_EQUIP_POS3 = 2,
	PET_EQUIP_MAX,
}

public enum EPetJob
{
	PET_JOB_NONE     = 0,
	PET_JOB_Warrior  = 1,
	PET_JOB_Magic    = 4,
	PET_JOB_Rogue    = 7,
	PET_JOB_Minister = 10,
}

public enum ERoleJob
{
	ROLE_JOB_NONE           = 0,
	ROLE_JOB_Warrior        = 1,
	ROLE_JOB_Berserker      = 2,
	ROLE_JOB_Knight         = 3,
	ROLE_JOB_Magic          = 4,
	ROLE_JOB_Wizard         = 5,
	ROLE_JOB_Warlock        = 6,
	ROLE_JOB_Rogue          = 7,
	ROLE_JOB_Assassin       = 8,
	ROLE_JOB_Gunner         = 9,
	ROLE_JOB_Minister       = 10,
	ROLE_JOB_Priest         = 11,
	ROLE_JOB_Pastor         = 12,
	ROLE_JOB_Max,
	ROLE_JOB_FLAG_NONE      = 1 << ROLE_JOB_NONE,
	ROLE_JOB_FLAG_Warrior   = 1 << ROLE_JOB_Warrior,
	ROLE_JOB_FLAG_Berserker = 1 << ROLE_JOB_Berserker,
	ROLE_JOB_FLAG_Knight    = 1 << ROLE_JOB_Knight,
	ROLE_JOB_FLAG_Magic     = 1 << ROLE_JOB_Magic,
	ROLE_JOB_FLAG_Wizard    = 1 << ROLE_JOB_Wizard,
	ROLE_JOB_FLAG_Warlock   = 1 << ROLE_JOB_Warlock,
	ROLE_JOB_FLAG_Rogue     = 1 << ROLE_JOB_Rogue,
	ROLE_JOB_FLAG_Assassin  = 1 << ROLE_JOB_Assassin,
	ROLE_JOB_FLAG_Gunner    = 1 << ROLE_JOB_Gunner,
	ROLE_JOB_FLAG_Minister  = 1 << ROLE_JOB_Minister,
	ROLE_JOB_FLAG_Priest    = 1 << ROLE_JOB_Priest,
	ROLE_JOB_FLAG_Pastor    = 1 << ROLE_JOB_Pastor,
}

public enum EFBHard
{
	EFBHard_Normal      = 1,
	EFBHard_Outstanding,
	EFBHard_Expert,
	EFBHard_Other,
}

public enum ECalcType
{
	CALC_BASE  = 0,
	CALC_EX,
	CALC_TOTAL,
	CALC_MAX,
}

public enum EItemObjType
{
	ITEM_OBJ_ROLE_BASE = 0,
	ITEM_OBJ_PET_BASE,
}

public enum EProperty
{
	PROPERTY_BaseBegin       = 0,
	PROPERTY_Str             = 0,
	PROPERTY_Int             = 1,
	PROPERTY_Dex             = 2,
	PROPERTY_Sta             = 3,
	PROPERTY_Fai             = 4,
	PROPERTY_BaseMax,
	PROPERTY_MaxHP           = 5,
	PROPERTY_MaxSP           = 6,
	PROPERTY_HPTick          = 7,
	PROPERTY_SPTick          = 8,
	PROPERTY_AtkDmgMin       = 9,
	PROPERTY_AtkDmgMax       = 10,
	PROPERTY_Def             = 11,
	PROPERTY_IceAtt          = 12,
	PROPERTY_FireAtt         = 13,
	PROPERTY_PoisonAtt       = 14,
	PROPERTY_ThunderAtt      = 15,
	PROPERTY_IceDef          = 16,
	PROPERTY_FireDef         = 17,
	PROPERTY_PoisonDef       = 18,
	PROPERTY_ThunderDef      = 19,
	PROPERTY_Pierce          = 20,
	PROPERTY_Hit             = 21,
	PROPERTY_Dodge           = 22,
	PROPERTY_Cri             = 23,
	PROPERTY_Flex            = 24,
	PROPERTY_CriDmg          = 25,
	PROPERTY_CriDmgDef       = 26,
	PROPERTY_BodySize        = 27,
	PROPERTY_AttSize         = 28,
	PROPERTY_TableBaseMax,
	PROPERTY_AttSpeedModPer  = 29,
	PROPERTY_MoveSpeedModPer = 30,
	PROPERTY_PiercePer       = 31,
	PROPERTY_HitPer          = 32,
	PROPERTY_DodgePer        = 33,
	PROPERTY_CriPer          = 34,
	PROPERTY_FlexPer         = 35,
	PROPERTY_AttSpeed        = 36,
	PROPERTY_MoveSpeed       = 37,
	PROPERTY_MaxEP           = 38,
	PROPERTY_MaxAP           = 39,
	PROPERTY_ExpPer          = 40,
	PROPERTY_MoneyPer        = 41,
	PROPERTY_CanModMax,
	PROPERTY_HP              = 64,
	PROPERTY_SP              = 65,
	PROPERTY_Money           = 66,
	PROPERTY_BindingBadge    = 67,
	PROPERTY_Badge           = 68,
	PROPERTY_MakeDmg         = 69,
	PROPERTY_MakeAtt         = 70,
	PROPERTY_MakeIce         = 71,
	PROPERTY_MakeFire        = 72,
	PROPERTY_MakePoison      = 73,
	PROPERTY_MakeThunder     = 74,
	PROPERTY_MakeShufu       = 75,
	PROPERTY_MakeHunshui     = 76,
	PROPERTY_MakeHunmi       = 77,
	PROPERTY_MakeDef         = 78,
	PROPERTY_Max,
}

public enum ESummonObjectPro
{
	SUMMONOBJECT_PRO_TargetType         = 0,
	SUMMONOBJECT_PRO_HitPer,
	SUMMONOBJECT_PRO_CriticalPer,
	SUMMONOBJECT_PRO_CriticalDmgPer,
	SUMMONOBJECT_PRO_AoeAimNum,
	SUMMONOBJECT_PRO_Mode,
	SUMMONOBJECT_PRO_PeriodTime,
	SUMMONOBJECT_PRO_Count,
	SUMMONOBJECT_PRO_AoeArea,
	SUMMONOBJECT_PRO_AoeAreaType,
	SUMMONOBJECT_PRO_AoeAreaData,
	SUMMONOBJECT_PRO_IgnoreDef,
	SUMMONOBJECT_PRO_IgnoreDefendAmount,
	SUMMONOBJECT_PRO_DamagePer,
	SUMMONOBJECT_PRO_BodyType_Small,
	SUMMONOBJECT_PRO_BodyType_Middle,
	SUMMONOBJECT_PRO_BodyType_Big,
	SUMMONOBJECT_PRO_BodyType_Boss,
	SUMMONOBJECT_PRO_SkillEffect,
	SUMMONOBJECT_PRO_Dmg,
	SUMMONOBJECT_PRO_AtkPowerPer,
	SUMMONOBJECT_PRO_Skilleffect0,
	SUMMONOBJECT_PRO_Skilleffect1,
	SUMMONOBJECT_PRO_Skilleffect2,
	SUMMONOBJECT_PRO_Skilleffect3,
	SUMMONOBJECT_PRO_Skilleffect4,
	SUMMONOBJECT_PRO_Skilleffect5,
	SUMMONOBJECT_PRO_Skilleffect6,
	SUMMONOBJECT_PRO_Skilleffect7,
	SUMMONOBJECT_PRO_MoveSpeed,
	SUMMONOBJECT_PRO_LifeTime,
}

public enum EGenderType
{
	GENDER_TYPE_m   = 0,
	GENDER_TYPE_w   = 1,
	GENDER_TYPE_max,
}

public enum ESkillShape
{
	SKILL_SHAPE_ZHANQIE        = 0,
	SKILL_SHAPE_SHOUHU,
	SKILL_SHAPE_HUIFU,
	SKILL_SHAPE_POHUAI,
	SKILL_SHAPE_MOFA,
	SKILL_SHAPE_AOSHU,
	SKILL_SHAPE_JIQIAO,
	SKILL_SHAPE_XUNJIE,
	SKILL_SHAPE_SHENSHENG,
	SKILL_SHAPE_MAX,
	SKILL_SHAPE_FLAG_ZHANQIE   = 1 << SKILL_SHAPE_ZHANQIE,
	SKILL_SHAPE_FLAG_SHOUHU    = 1 << SKILL_SHAPE_SHOUHU,
	SKILL_SHAPE_FLAG_HUIFU     = 1 << SKILL_SHAPE_HUIFU,
	SKILL_SHAPE_FLAG_POHUAI    = 1 << SKILL_SHAPE_POHUAI,
	SKILL_SHAPE_FLAG_MOFA      = 1 << SKILL_SHAPE_MOFA,
	SKILL_SHAPE_FLAG_AOSHU     = 1 << SKILL_SHAPE_AOSHU,
	SKILL_SHAPE_FLAG_JIQIAO    = 1 << SKILL_SHAPE_JIQIAO,
	SKILL_SHAPE_FLAG_XUNJIE    = 1 << SKILL_SHAPE_XUNJIE,
	SKILL_SHAPE_FLAG_SHENSHENG = 1 << SKILL_SHAPE_SHENSHENG,
}

public enum EItemEnterType
{
	ITEM_ENTER_TYPE_DEFAULT = 0,
	ITEM_ENTER_TYPE_UNEQUIP = 1,
}

public enum EDamageProperty
{
	DAMAGE_PROPERTY_PHY          = 0,
	DAMAGE_PROPERTY_ICE,
	DAMAGE_PROPERTY_FIRE,
	DAMAGE_PROPERTY_POISON,
	DAMAGE_PROPERTY_THUNDER,
	DAMAGE_PROPERTY_NONE,
	DAMAGE_PROPERTY_MAX,
	DAMAGE_PROPERTY_FLAG_PHY     = 1 << DAMAGE_PROPERTY_PHY,
	DAMAGE_PROPERTY_FLAG_ICE     = 1 << DAMAGE_PROPERTY_ICE,
	DAMAGE_PROPERTY_FLAG_FIRE    = 1 << DAMAGE_PROPERTY_FIRE,
	DAMAGE_PROPERTY_FLAG_POISON  = 1 << DAMAGE_PROPERTY_POISON,
	DAMAGE_PROPERTY_FLAG_THUNDER = 1 << DAMAGE_PROPERTY_THUNDER,
	DAMAGE_PROPERTY_FLAG_NONE    = 1 << DAMAGE_PROPERTY_NONE,
}

public enum ECreatureActionState
{
	CREATURE_ACTION_STATE_STAY             = 0,
	CREATURE_ACTION_STATE_HOLD,
	CREATURE_ACTION_STATE_STUN,
	CREATURE_ACTION_STATE_LIMIT_SKILL,
	CREATURE_ACTION_STATE_UNBEAT,
	CREATURE_ACTION_STATE_KNOCKBACK,
	CREATURE_ACTION_STATE_MAX,
	CREATURE_ACTION_STATE_TELEPORT         = CREATURE_ACTION_STATE_MAX,
	CREATURE_ACTION_STATE_IDLE,
	CREATURE_ACTION_STATE_FLAG_STAY        = 1 << CREATURE_ACTION_STATE_STAY,
	CREATURE_ACTION_STATE_FLAG_HOLD        = 1 << CREATURE_ACTION_STATE_HOLD,
	CREATURE_ACTION_STATE_FLAG_STUN        = 1 << CREATURE_ACTION_STATE_STUN,
	CREATURE_ACTION_STATE_FLAG_LIMIT_SKILL = 1 << CREATURE_ACTION_STATE_LIMIT_SKILL,
	CREATURE_ACTION_STATE_FLAG_UNBEAT      = 1 << CREATURE_ACTION_STATE_UNBEAT,
	CREATURE_ACTION_STATE_FLAG_TELEPORT    = 1 << CREATURE_ACTION_STATE_TELEPORT,
	CREATURE_ACTION_STATE_FLAG_IDLE        = 1 << CREATURE_ACTION_STATE_IDLE,
}

public enum ESkillObjType
{
	SKILL_OBJ_SELF              = 0,
	SKILL_OBJ_TEAM,
	SKILL_OBJ_TEAM_EXCLOUD_SELF,
	SKILL_OBJ_FRIENDLY_ROLE,
	SKILL_OBJ_FRIENDLY,
	SKILL_OBJ_ENEMY,
	SKILL_OBJ_ALL,
	SKILL_OBJ_ALL_EXCLOUD_SELF,
	SKILL_OBJ_MAX,
}

public enum EAoeCenter
{
	AOE_CETER_FORMER = 0,
	AOE_CETER_FRONT,
	AOE_CETER_BACK,
}

public enum ESkillAreaType
{
	SKILL_AREA_TYPE_SINGLE   = 0,
	SKILL_AREA_TYPE_ROUND,
	SKILL_AREA_TYPE_RECT,
	SKILL_AREA_TYPE_CROSS,
	SKILL_AREA_TYPE_X,
	SKILL_AREA_TYPE_FACE_ARC,
	SKILL_AREA_TYPE_BACK_ARC,
	SKILL_AREA_TYPE_FACE,
	SKILL_AREA_TYPE_MAP,
	SKILL_AREA_TYPE_RING,
	SKILL_AREA_TYPE_MAX,
}

public enum EIgnoreDefendFlag
{
	IGNORE_DEFEND_FLAG_NONE       = 0,
	IGNORE_DEFEND_FLAG_phy        = 1,
	IGNORE_DEFEND_FLAG_IceDef     = 1 << 1,
	IGNORE_DEFEND_FLAG_FireDef    = 1 << 2,
	IGNORE_DEFEND_FLAG_PoisonDef  = 1 << 3,
	IGNORE_DEFEND_FLAG_ThunderDef = 1 << 4,
}

public enum ESkillEffect
{
	SKILL_EFFECT_DAMAGE_HP      = 0,
	SKILL_EFFECT_DAMAGE_SP,
	SKILL_EFFECT_BALEFUL,
	SKILL_EFFECT_CURE_HP,
	SKILL_EFFECT_CURE_MP,
	SKILL_EFFECT_HELPFUL,
	SKILL_EFFECT_DAMAGE_OR_HEAL,
	SKILL_EFFECT_NONE,
}

public enum EDamageType
{
	DAMAGE_TYPE_NONE       = 0,
	DAMAGE_TYPE_COMMON,
	DAMAGE_TYPE_DEADLINESS,
}

public enum EDamageResult
{
	DAMAGE_RESULT_ISDAMAGE = 0,
	DAMAGE_RESULT_ISCURE,
}

public enum EDamageBring
{
	DAMAGE_BRING_SKILL      = 0,
	DAMAGE_BRING_BUFF,
	DAMAGE_BRING_ITEM,
	DAMAGE_BRING_TRAP,
	DAMAGE_BRING_MAX,
	DAMAGE_BRING_FLAG_SKILL = 1 << DAMAGE_BRING_SKILL,
	DAMAGE_BRING_FLAG_BUFF  = 1 << DAMAGE_BRING_BUFF,
	DAMAGE_BRING_FLAG_ITEM  = 1 << DAMAGE_BRING_ITEM,
	DAMAGE_BRING_FLAG_TRAP  = 1 << DAMAGE_BRING_TRAP,
}

public enum ESkillTimingConditionType
{
	SKILL_TIMING_CONDITION_TYPE_START = 0,
	SKILL_TIMING_CONDITION_TYPE_END,
	SKILL_TIMING_CONDITION_TYPE_HIT,
}

public enum ESkillEffectHitCondition
{
	SKILL_EFT_CONDITOION_ALWAYS     = 0,
	SKILL_EFT_CONDITOION_HIT,
	SKILL_EFT_CONDITOION_DEADLINESS,
	SKILL_EFT_CONDITOION_MISS,
}

public enum ESkillEffectCountCondition
{
	SKILL_EFFECT_COUNT_CONDITION_ONCE = 0,
	SKILL_EFFECT_COUNT_CONDITION_MORE,
}

public enum ECheckBuffType
{
	CHECK_BUFF_NONE        = 0,
	CHECK_BUFF_CLASS,
	CHECK_BUFF_SUB_CLASS,
	CHECK_BUFF_CLASS_ID,
	CHECK_BUFF_TEMPLATE_ID,
	CHECK_BUFF_DAMAGE_PRO,
}

public enum EBuffClass
{
	BUFF_CLASS_CONTROL  = 0,
	BUFF_CLASS_INCREASE,
	BUFF_CLASS_DECREASE,
	BUFF_CLASS_NONE,
}

public enum EControlSubClass
{
	CONTROL_SUBCLASS_STAY        = 0,
	CONTROL_SUBCLASS_HOLD,
	CONTROL_SUBCLASS_STUN,
	CONTROL_SUBCLASS_LIMIT_SKILL,
}

public enum EIncreaseSubClass
{
	INCREASE_SUBCLASS_NORMAL        = 0,
	INCREASE_SUBCLASS_SPEED_UP_ATT,
	INCREASE_SUBCLASS_SPEED_UP_MOVE,
}

public enum EDecreaseSubClass
{
	DECREASE_SUBCLASS_NORMAL        = 0,
	DECREASE_SUBCLASS_SPEED_UP_ATT,
	DECREASE_SUBCLASS_SPEED_UP_MOVE,
}

public enum EBuffFlag
{
	BUFF_FLAG_HIDE           = 1 << 0,
	BUFF_FLAG_OFFLINE_SAVE   = 1 << 1,
	BUFF_FLAG_OFFLINE_TIME   = 1 << 2,
	BUFF_FLAG_SHARE_MULTI    = 1 << 3,
	BUFF_FLAG_SWAP_OTHERS    = 1 << 4,
	BUFF_FLAG_DEAD_HOLD      = 1 << 5,
	BUFF_FLAG_REFRESH_TIME   = 1 << 6,
	BUFF_FLAG_AURA_EXCLUDE   = 1 << 7,
	BUFF_FLAG_SCENE_CHG_SAVE = 1 << 8,
	BUFF_FLAG_TYPE_DEBUFF    = 1 << 9,
}

public enum EBuffRemoveFlag
{
	BUFF_REMOVE_FLAG_EFFECT = 1 << 0,
	BUFF_REMOVE_FLAG_HAND   = 1 << 1,
}

public enum EBuffEffectType
{
	BUFF_EFFECT_NONE                  = 0,
	BUFF_EFFECT_START,
	BUFF_EFFECT_PERIODIC,
	BUFF_EFFECT_END,
	BUFF_EFFECT_SPRING,
	BUFF_EFFECT_FULL_LAYER,
	BUFF_EFFECT_MOD_PRO,
	BUFF_EFFECT_MOD_SKILL_INFO_PRO,
	BUFF_EFFECT_MOD_SKILL_ACTION_PRO,
	BUFF_EFFECT_MOD_SKILL_EFFECT_PRO,
	BUFF_EFFECT_MOD_SUMMONOBJECT_PRO ,
}

public enum EBuffEndType
{
	BUFF_END_NONE        = 0,
	BUFF_END_TIMEUP,
	BUFF_END_DISPEL,
	BUFF_END_USEUP,
	BUFF_END_DEAD,
	BUFF_END_HAND,
	BUFF_END_LEAVE_SCENE,
	BUFF_END_LEAVE_GAME,
	BUFF_END_UNKNOWN,
}

public enum EEffectSkillBuffOptType
{
	EFFECT_SKILL_BUFF_OPT_ADDBUFF             = 0,
	EFFECT_SKILL_BUFF_OPT_MOD_SKILL,
	EFFECT_SKILL_BUFF_OPT_MOD_BUFF_TEMPLATEID,
	EFFECT_SKILL_BUFF_OPT_MOD_BUFF_CLASS,
	EFFECT_SKILL_BUFF_OPT_LEVEL_UP_SKILL,
}

public enum EImmunityType
{
	IMMUNITY_TYPE_STAY             = 0,
	IMMUNITY_TYPE_HOLD,
	IMMUNITY_TYPE_STUN,
	IMMUNITY_TYPE_LIMIT_SKILL,
	IMMUNITY_TYPE_SLOW_ATT,
	IMMUNITY_TYPE_SLOW_MOVE,
	IMMUNITY_TYPE_CATCH,
	IMMUNITY_TYPE_ROCK,
	IMMUNITY_TYPE_ATT_PHY,
	IMMUNITY_TYPE_ATT_ICE,
	IMMUNITY_TYPE_ATT_FIRE,
	IMMUNITY_TYPE_ATT_POISON,
	IMMUNITY_TYPE_ATT_THUNDER,
	IMMUNITY_TYPE_MAX,
	IMMUNITY_TYPE_FLAG_STAY        = 1 << IMMUNITY_TYPE_STAY,
	IMMUNITY_TYPE_FLAG_HOLD        = 1 << IMMUNITY_TYPE_HOLD,
	IMMUNITY_TYPE_FLAG_STUN        = 1 << IMMUNITY_TYPE_STUN,
	IMMUNITY_TYPE_FLAG_LIMIT_SKILL = 1 << IMMUNITY_TYPE_LIMIT_SKILL,
	IMMUNITY_TYPE_FLAG_SLOW_ATT    = 1 << IMMUNITY_TYPE_SLOW_ATT,
	IMMUNITY_TYPE_FLAGSLOW_MOVE    = 1 << IMMUNITY_TYPE_SLOW_MOVE,
	IMMUNITY_TYPE_FLAG_CATCH       = 1 << IMMUNITY_TYPE_CATCH,
	IMMUNITY_TYPE_FLAG_ROCK        = 1 << IMMUNITY_TYPE_ROCK,
	IMMUNITY_TYPE_FLAG_ATT_PHY     = 1 << IMMUNITY_TYPE_ATT_PHY,
	IMMUNITY_TYPE_FLAG_ATT_ICE     = 1 << IMMUNITY_TYPE_ATT_ICE,
	IMMUNITY_TYPE_FLAG_ATT_FIRE    = 1 << IMMUNITY_TYPE_ATT_FIRE,
	IMMUNITY_TYPE_FLAG_ATT_POISON  = 1 << IMMUNITY_TYPE_ATT_POISON,
	IMMUNITY_TYPE_FLAG_ATT_THUNDER = 1 << IMMUNITY_TYPE_ATT_THUNDER,
}

public enum EOpreationType
{
	OPREATION_TYPE_NONE                 = 0,
	OPREATION_TYPE_FOREVER,
	OPREATION_TYPE_STATE,
	OPREATION_TYPE_ADD_BUFF,
	OPREATION_TYPE_DISPEL,
	OPREATION_TYPE_DO_BUFF_DAMAGE,
	OPREATION_TYPE_ADD_SKILL_POINT ,
	OPREATION_TYPE_DO_SKILL_COOLDOWN ,
	OPREATION_TYPE_CLEAR_SKILL_COOLDOWN,
	OPREATION_TYPE_INTERUPT_SKILL,
	OPREATION_TYPE_SUMMON_MONSTER,
	OPREATION_TYPE_TELEPORT,
	OPREATION_TYPE_DOACTION,
	OPREATION_TYPE_COMBO,
	OPREATION_TYPE_ATTACKGETHP,
	OPERATION_TYPE_HURTMODIFYBUFF,
	OPERATION_TYPE_HURTMODIFY,
	OPERATION_TYPE_SUMMON,
	OPERATION_TYPE_HIDESHOW,
	OPERATION_TYPE_FLASH,
}

public enum ETeleportType
{
	TELEPORT_TYPE_KNOCK = 0,
	TELEPORT_TYPE_DRAG  = 0,
}

public enum ESkillCooldownType
{
	SKILL_COOLDOWN_TYPE_TEMPLATE_ID = 0,
	SKILL_COOLDOWN_TYPE_SHAPE,
	SKILL_COOLDOWN_TYPE_ALL,
}

public enum EOpreationFlag
{
	OPREATION_FLAG_MOD_HP  = 0,
	OPREATION_FLAG_MOD_SP  = 1,
	OPREATION_FLAG_ADD     = 0,
	OPREATION_FLAG_REDUCE  = 1,
	OPREATION_FLAG_SET     = 2,
	OPREATION_FLAG_INTEGER = 0,
	OPREATION_FLAG_PERCENT = 1,
}

public enum EDoOperationType
{
	DO_OPERATION_TYPE_START  = 0,
	DO_OPERATION_TYPE_ADD,
	DO_OPERATION_TYPE_REDUCE,
	DO_OPERATION_TYPE_REMOVE,
}

public enum ESkillInfoPro
{
	SKILL_INFO_PRO_Cooldown = 0,
}

public enum ESkillActionPro
{
	SKILL_ACTION_PRO_CostSP                         = 0,
	SKILL_ACTION_PRO_IgnoreAction,
	SKILL_ACTION_PRO_TargetType,
	SKILL_ACTION_PRO_HitPer,
	SKILL_ACTION_PRO_CriticalPer,
	SKILL_ACTION_PRO_CriticalDmgPer,
	SKILL_ACTION_PRO_oeAimNum,
	SKILL_ACTION_PRO_AoeCenterType,
	SKILL_ACTION_PRO_oeCenterData,
	SKILL_ACTION_PRO_AoeAreaType,
	SKILL_ACTION_PRO_oeAreaData,
	SKILL_ACTION_PRO_IgnoreDef,
	SKILL_ACTION_PRO_IgnoreDefendAmount,
	SKILL_ACTION_PRO_DamagePer,
	SKILL_ACTION_PRO_MoreDamagePer_BODY_TYPE_small,
	SKILL_ACTION_PRO_MoreDamagePer_BODY_TYPE_middle,
	SKILL_ACTION_PRO_MoreDamagePer_BODY_TYPE_big,
	SKILL_ACTION_PRO_MoreDamagePer_BODY_TYPE_boss,
	SKILL_ACTION_PRO_SkillEffect,
	SKILL_ACTION_PRO_DirectDmgMin,
	SKILL_ACTION_PRO_DirectDmgMax,
	SKILL_ACTION_PRO_Dmg,
	SKILL_ACTION_PRO_AtkPowerPer,
	SKILL_ACTION_PRO_skillEffectSelf0,
	SKILL_ACTION_PRO_skillEffectSelf1,
	SKILL_ACTION_PRO_skillEffectSelf2,
	SKILL_ACTION_PRO_skillEffectSelf3,
	SKILL_ACTION_PRO_skillEffectSelf4,
	SKILL_ACTION_PRO_skillEffectSelf5,
	SKILL_ACTION_PRO_skillEffectSelf6,
	SKILL_ACTION_PRO_skillEffectSelf7,
	SKILL_ACTION_PRO_skillEffect0,
	SKILL_ACTION_PRO_skillEffect1,
	SKILL_ACTION_PRO_skillEffect2,
	SKILL_ACTION_PRO_skillEffect3,
	SKILL_ACTION_PRO_skillEffect4,
	SKILL_ACTION_PRO_skillEffect5,
	SKILL_ACTION_PRO_skillEffect6,
	SKILL_ACTION_PRO_skillEffect7,
	SKILL_ACTION_PRO_HealRatio,
}

public enum ESkillEffectPro
{
	SKILL_EFFECT_PRO_HitCondition        = 0,
	SKILL_EFFECT_PRO_MonsterBodyTypeFlag,
	SKILL_EFFECT_PRO_SelfCheckType,
	SKILL_EFFECT_PRO_SelfBuffID,
	SKILL_EFFECT_PRO_TgtCheckType,
	SKILL_EFFECT_PRO_TgtBuffID,
	SKILL_EFFECT_PRO_Probability,
	SKILL_EFFECT_PRO_Flag,
	SKILL_EFFECT_PRO_OperationID,
	SKILL_EFFECT_PRO_OperationData,
	SKILL_EFFECT_PRO_OperationData1,
}

public enum EPetQuality
{
	PET_QUALITY_GRAY   = 0,
	PET_QUALITY_WHITE  = 1,
	PET_QUALITY_GREEN  = 2,
	PET_QUALITY_BLUE   = 3,
	PET_QUALITY_PURPLE = 4,
	PET_QUALITY_GOD    = 5,
	PET_QUALITY_CS     = 6,
	PET_QUALITY_SS     = 7,
	PET_QUALITY_MAX,
}

public enum EPetTeam
{
	PET_TEAM_MIN = 0,
	PET_TEAM1    = 0,
	PET_TEAM2,
	PET_TEAM3,
	PET_TEAM_MAX,
}

public enum EPetBattlePos
{
	PET_BTPOS_TEAM        = 7,
	PET_BTPOS_UNKNOWN     = -1,
	PET_BTPOS_CAPTAIN     = 0,
	PET_BTPOS_JOIN1,
	PET_BTPOS_JOIN2,
	PET_BTPOS_JOIN_MAX,
	PET_BTPOS_ASSISTANT1  = PET_BTPOS_JOIN_MAX,
	PET_BTPOS_ASSISTANT2,
	PET_BTPOS_ASSISTANT3,
	PET_BTPOS_ASSISTANT4,
	PET_BTPOS1_CAPTAIN,
	PET_BTPOS1_JOIN1,
	PET_BTPOS1_JOIN2,
	PET_BTPOS1_ASSISTANT1,
	PET_BTPOS1_ASSISTANT2,
	PET_BTPOS1_ASSISTANT3,
	PET_BTPOS1_ASSISTANT4,
	PET_BTPOS2_CAPTAIN,
	PET_BTPOS2_JOIN1,
	PET_BTPOS2_JOIN2,
	PET_BTPOS2_ASSISTANT1,
	PET_BTPOS2_ASSISTANT2,
	PET_BTPOS2_ASSISTANT3,
	PET_BTPOS2_ASSISTANT4,
	PET_BTPOS_MAX,
}

public enum EPetUp
{
	PET_UP_MIN = 0,
	PET_UP_0   = 0,
	PET_UP_1,
	PET_UP_2,
	PET_UP_3,
	PET_UP_4,
	PET_UP_5,
	PET_UP_6,
	PET_UP_7,
	PET_UP_8,
	PET_UP_9,
	PET_UP_MAX = 5,
}

public enum ELockType
{
	ROLE_ITEM_LOCK = 1,
	PET_ITEM_LOCK  = 2,
	PET_LOCK       = 3,
}

public enum EReturnType
{
	GS_RESULT_YES     = 1,
	GS_RESULT_NO      = 0,
	GS_RESULT_SUCCESS = 1,
	GS_RESULT_FAIL    = 0,
}

public enum ERoleItemEvent
{
	ROLE_ITEM_EVENT_SELL = 0,
}

public enum EItemUpEvent
{
	ITEM_UP_LJ  = 0,
	ITEM_UP_PER = 1,
	ITEM_UP_EXP = 2,
}

public enum EPetEvent
{
	PET_ITEM_EVENT_SELL = 0,
	PET_EVENT_SELL      = 1,
}

public enum EMonsterAbility
{
	MONSTER_ABILITY_normal        = 0,
	MONSTER_ABILITY_outstand,
	MONSTER_ABILITY_copy,
	MONSTER_ABILITY_boss,
	MONSTER_ABILITY_boss_big,
	MONSTER_ABILITY_boss_large,
	MONSTER_ABILITY_NPC,
	MONSTER_ABILITY_max,
	MONSTER_ABILITY_FLAG_normal   = 1 << MONSTER_ABILITY_normal,
	MONSTER_ABILITY_FLAG_outstand = 1 << MONSTER_ABILITY_outstand,
	MONSTER_ABILITY_FLAG_copy     = 1 << MONSTER_ABILITY_copy,
	MONSTER_ABILITY_FLAG_boss     = 1 << MONSTER_ABILITY_boss,
	MONSTER_ABILITY_FLAG_NPC      = 1 << MONSTER_ABILITY_NPC,
}

public enum EMonsterBodyType
{
	MONSTER_BODY_TYPE_small       = 0,
	MONSTER_BODY_TYPE_middle,
	MONSTER_BODY_TYPE_big,
	MONSTER_BODY_TYPE_boss,
	MONSTER_BODY_TYPE_max,
	MONSTER_BODY_TYPE_FLAG_small  = 1 << MONSTER_BODY_TYPE_small,
	MONSTER_BODY_TYPE_FLAG_middle = 1 << MONSTER_BODY_TYPE_middle,
	MONSTER_BODY_TYPE_FLAG_big    = 1 << MONSTER_BODY_TYPE_big,
	MONSTER_BODY_TYPE_FLAG_boss   = 1 << MONSTER_BODY_TYPE_boss,
}

public enum EItemPos
{
	ItemPos_Bag       = 1,
	ItemPos_Equip     = 2,
	ItemPos_Warehouse = 4,
	ItemPos_None      = 0,
}

public enum EItemQuality
{
	ITEM_QUALITY_GRAY   = 0,
	ITEM_QUALITY_WHITE  = 1,
	ITEM_QUALITY_GREEN  = 2,
	ITEM_QUALITY_BLUE   = 3,
	ITEM_QUALITY_PURPLE = 4,
	ITEM_QUALITY_GOD    = 5,
	ITEM_QUALITY_CS     = 6,
	ITEM_QUALITY_SS     = 7,
	ITEM_QUALITY_MAX,
}

public enum ERoleItemEquipSlot
{
	RoleItemEquipSlot_Equip_Min    = 0,
	RoleItemEquipSlot_Weapon       = 0,
	RoleItemEquipSlot_Helmet       = 1,
	RoleItemEquipSlot_Clothing     = 2,
	RoleItemEquipSlot_Gloves       = 3,
	RoleItemEquipSlot_Trousers     = 4,
	RoleItemEquipSlot_Mantle       = 5,
	RoleItemEquipSlot_Ring         = 6,
	RoleItemEquipSlot_Adron        = 7,
	RoleItemEquipSlot_Necklace     = 8,
	RoleItemEquipSlot_SecondWeapon = 9,
	RoleItemEquipSlot_Equip_Max    = 10,
}

public enum EPetItemEquipSlot
{
	PetItemEquipSlot_Equip_Min = 0,
	PetItemEquipSlot_Weapon    = 0,
	PetItemEquipSlot_Armor     = 1,
	PetItemEquipSlot_Adorn     = 2,
	PetItemEquipSlot_Equip_Max = 3,
}

public enum EItemClass
{
	ItemClass_Role_Equip_Start = 0,
	ItemClass_Weapon           = 0,
	ItemClass_Armor            = 1,
	ItemClass_Adorn            = 2,
	ItemClass_Role_Equip_End,
	ItemClass_Role_Item        = 51,
	ItemClass_Pet_Equip        = 100,
	ItemClass_Pet_Item         = 101,
}

public enum EItemSubClass_Role_Item
{
	ItemSubClass_Role_Item      = 0,
	ItemSubClass_Role_Item_Hole = 1,
	ItemSubClass_Role_Item_Prod = 2,
}

public enum EItemSubClass_Pet_Equip
{
	ItemSubClass_Pet_Equip = 0,
}

public enum EItemSubClass_Pet_Item
{
	ItemSubClass_Pet_Item    = 0,
	ItemSubClass_Pet_Card    = 1,
	ItemSubClass_Pet_SubCard = 2,
}

public enum EItemSubClass_Weapon
{
	ItemSubClass_Sword       = 0,
	ItemSubClass_Wand,
	ItemSubClass_Bow,
	ItemSubClass_Hammer,
	ItemSubClass_Shield,
	ItemSubClass_Hudun,
	ItemSubClass_Hufu,
	ItemSubClass_Jiandai,
	ItemSubClassFlag_Sword   = 1 << ItemSubClass_Sword,
	ItemSubClassFlag_Wand    = 1 << ItemSubClass_Wand,
	ItemSubClassFlag_Bow     = 1 << ItemSubClass_Bow,
	ItemSubClassFlag_Hammer  = 1 << ItemSubClass_Hammer,
	ItemSubClassFlag_Shield  = 1 << ItemSubClass_Shield,
	ItemSubClassFlag_Hudun   = 1 << ItemSubClass_Hudun,
	ItemSubClassFlag_Hufu    = 1 << ItemSubClass_Hufu,
	ItemSubClassFlag_Jiandai = 1 << ItemSubClass_Jiandai,
}

public enum EItemSubClass_Armor
{
	ItemSubClass_Helmet   = 0,
	ItemSubClass_Clothing,
	ItemSubClass_Gloves,
	ItemSubClass_Trousers,
	ItemSubClass_Mantle,
}

public enum EItemSubClass_Adorn
{
	ItemSubClass_Ring     = 0,
	ItemSubClass_Adron,
	ItemSubClass_Necklace,
}

public enum ELevelCompleteResult
{
	LEVEL_COMPLETE_RESULT_NO  = 0,
	LEVEL_COMPLETE_RESULT_B,
	LEVEL_COMPLETE_RESULT_A,
	LEVEL_COMPLETE_RESULT_S,
	LEVEL_COMPLETE_RESULT_MAX,
}

public enum RS_ROLE_PROERTY_TYPE
{
	RS_ROLE_PROPERTY_NULL   = 0,
	RS_ROLE_PROPERTY_BASE   = 1,
	RS_ROLE_PROPERTY_PET    = 2,
	RS_ROLE_PROPERTY_EQUIP  = 3,
	RS_ROLE_PROPERTY_ONLINE = 4,
}

public enum RS_ACTIVITY_ABYSS_STATE_TYPE
{
	RS_ACTIVITY_ABYSS_STATE_NULL         = 0,
	RS_ACTIVITY_ABYSS_STATE_TRIGGER      = 1,
	RS_ACTIVITY_ABYSS_STATE_TRIGGER_OVER = 1,
	RS_ACTIVITY_ABYSS_STATE_OPEN         = 2,
	RS_ACTIVITY_ABYSS_STATE_KILLED       = 3,
	RS_ACTIVITY_ABYSS_STATE_RUN          = 4,
}

public enum LEVEL_BATTLE_TYPE
{
	LEVEL_BATTLE_TYPE_NORMAL         = 0,
	LEVEL_BATTLE_TYPE_ABYSS          = 1,
	LEVEL_BATTLE_TYPE_WORLD_BOSS     = 2,
	LEVEL_BATTLE_TYPE_PET_TRAIN      = 3,
	LEVEL_BATTLE_TYPE_MONEY_DUNGEONS = 4,
	LEVEL_BATTLE_TYPE_FIRE_GRAGON    = 5,
}

public enum MAIL_TYPE
{
	mail_type_system = 0,
	mail_type_user   = 1,
	mail_type_friend = 2,
}

public enum EBuyPetType
{
	EBuyPetType_Exchange    = 0,
	EBuyPetType_Cheap       = 1,
	EBuyPetType_Expensive   = 2,
	EBuyPetType_Expensive10 = 3,
}

public enum EShopPetType
{
	EShopPetType_Exchange  = 0,
	EShopPetType_Cheap     = 1,
	EShopPetType_Expensive = 2,
	EShopPetType_Critical  = 3,
}

public enum EBuyEPResult
{
	EBuyEPResult_Success = 0,
	EBuyEPResult_EPMax   = 1,
}

public enum EShopLimitType
{
	EShopLimitType_None    = 0,
	EShopLimitType_Forever = 1,
	EShopLimitType_Day     = 2,
}

public enum EShopBuyResult
{
	EShopBuyResult_Success   = 0,
	EShopBuyResult_Fail      = 1,
	EShopBuyResult_BagFull   = 2,
	EShopBuyResult_NoCrystal = 3,
	EShopBuyResult_NoCash    = 4,
	EShopBuyResult_NoMoney   = 5,
}

public enum EMonthCardAwardResult
{
	EMonthCardAwardResult_Success = 0,
	EMonthCardAwardResult_Fail    = 1,
	EMonthCardAwardResult_Expired = 2,
}

public enum EDayQuestType
{
	EDayQuestType_INVALID = 100,
	EDayQuestType_FB1     = 101,
	EDayQuestType_FB2     = 102,
	EDayQuestType_FB3     = 103,
	EDayQuestType_ItemUp  = 104,
	EDayQuestType_GEM     = 105,
	EDayQuestType_PVP     = 106,
	EDayQuestType_PetUp   = 107,
	EDayQuestType_AP      = 108,
	EDayQuestType_MAX     = 109,
}

public enum EJihuoma
{
	EJihuomaSuccess       = 1,
	EJihuomaCodeLenExceed = 2,
	EJihuomaGDSysError    = 3,
	EJihuomaSysError      = 4,
	EJihuomaMsgFieldError = 5,
	EJihuomaCodeIllegal   = 6,
	EJihuomaAlreadyUsed   = 7,
	EJihuomaAddItemFailed = 8,
}

public enum EReceiveEPResult
{
	EReceiveEPResult_Success    = 1,
	EReceiveEPResult_NotInTime  = 2,
	EReceiveEPResult_AlreadyGet = 3,
	EReceiveEPResult_SysError   = 4,
}

public enum MILITARY_NAME_COLOR_TYPE
{
	MILITARY_NAME_COLOR_WHITE  = 0,
	MILITARY_NAME_COLOR_GREEN  = 1,
	MILITARY_NAME_COLOR_BLUE   = 2,
	MILITARY_NAME_COLOR_PURPLE = 3,
	MILITARY_NAME_COLOR_ORANGE = 4,
}

public enum PVP_GET_POINTS_TYPE
{
	PVP_GET_POINTS_TYPE_NORMAL = 0,
	PVP_GET_POINTS_TYPE_LARGE  = 1,
	PVP_GET_POINTS_TYPE_SUPER  = 2,
}

public enum WORLD_BOSS_STATUS_TYPE
{
	WORLD_BOSS_STATUS_TYPE_NORMAL  = 0,
	WORLD_BOSS_STATUS_TYPE_PREPARE = 1,
	WORLD_BOSS_STATUS_TYPE_BEGIN   = 2,
	WORLD_BOSS_STATUS_TYPE_RUN     = 3,
	WORLD_BOSS_STATUS_TYPE_KILLED  = 4,
	WORLD_BOSS_STATUS_TYPE_CALC    = 5,
	WORLD_BOSS_STATUS_TYPE_END     = 6,
}

public enum EGuildPosition
{
	EGuildPosition_President     = 1,
	EGuildPosition_VicePresident = 2,
	EGuildPosition_Elite         = 3,
	EGuildPosition_Common        = 4,
}

public enum EGuildListResult
{
	EGuildListResult_OK      = 1,
	EGuildListResult_PageMax = 2,
}

public enum EGuildEnterResult
{
	EGuildEnterResult_OK        = 1,
	EGuildEnterResult_InGuild   = 2,
	EGuildEnterResult_EnterMax  = 3,
	EGuildEnterResult_MemberMax = 4,
}

public enum EGuildLeaveResult
{
	EGuildLeaveResult_OK        = 1,
	EGuildLeaveResult_President = 2,
	EGuildLeaveResult_SysError  = 4,
}

public enum EGuildCreateResult
{
	EGuildCreateResult_OK        = 1,
	EGuildCreateResult_NameExist = 2,
	EGuildCreateResult_NameShort = 3,
	EGuildCreateResult_NameLong  = 4,
	EGuildCreateResult_AimShort  = 5,
	EGuildCreateResult_AimLong   = 6,
}

public enum EGuildLogType
{
	EGuildLogType_MemberEnter      = 11,
	EGuildLogType_MemberKick       = 12,
	EGuildLogType_PotitionChange   = 13,
	EGuildLogType_PresidentChange  = 14,
	EGuildLogType_PresidentImpeach = 15,
	EGuildLogType_LevelUp          = 20,
	EGuildLogType_TechnologyUp     = 30,
	EGuildLogType_Pray             = 40,
	EGuildLogType_NoticeChange     = 50,
	EGuildLogType_AimChange        = 60,
}

public enum EGuildLogResult
{
	EGuildLogResult_OK        = 1,
	EGuildLogResult_NotMember = 2,
	EGuildLogResult_SysError  = 3,
}

public enum EGuildChangeNoticeResult
{
	EGuildChangeNoticeResult_OK        = 1,
	EGuildChangeNoticeResult_NotMember = 2,
	EGuildChangeNoticeResult_SysError  = 3,
}

public enum EGuildEnterAcceptResult
{
	EGuildEnterAcceptResult_OK        = 1,
	EGuildEnterAcceptResult_NotInList = 2,
	EGuildEnterAcceptResult_InGuild   = 3,
	EGuildEnterAcceptResult_Canceled  = 4,
	EGuildEnterAcceptResult_SysError  = 5,
}

/*Enums End*/


public	enum EN_HeaderProto_MessageID
{
};

/*Define Structs and Unions        Begin*/

public class SError	:	CMessage{
	public	int              m_Type;	/// 错误码类型       .
	public	byte            [] m_Error;	/// 错误信息         .
	public SError()
	{
		m_Error=new byte[(int)MAX_ERROR_STRING];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Error";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_ERROR_STRING;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_Error;

		}
		return 0;
	}
};

public class SPosInfo	:	CMessage{
	public	int              m_X;	/// X坐标，单位0.001米  .
	public	int              m_Y;	/// Y坐标，单位0.001米  .
	public	int              m_Z;	/// Z坐标，单位0.001米  .
	public	int              m_Orientation;	/// 方向，单位角度   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="X";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Y";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Z";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Orientation";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_X;
		case	1:return m_Y;
		case	2:return m_Z;
		case	3:return m_Orientation;

		}
		return 0;
	}
};

public class SRoleDBInfo	:	CMessage{
	public	int              m_DBIndex;	/// 数据库索引       .
	public	int              m_HP;	/// 当前血量         .
	public	int              m_SP;	/// 当前技力         .
	public	int              m_EP;	/// 活力值           .
	public	int              m_VipEP;	/// vip活力值        .
	public	int              m_AP;	/// 活动协助次数     .
	public	Int64            m_Experience;	/// 当前经验值       .
	public	uint             m_NonMoney;	/// 当前绑定游戏币（金币）  .
	public	uint             m_NonCash;	/// 当前绑定勋章     .
	public	uint             m_Cash;	/// 当前流通勋章     .
	public	UInt64           m_LatestWorldChatTime;	/// 最近一次世界聊天的时间  .
	public	byte            [] m_XData;	/// xml扩展数据      .
	public	byte            [] m_XData2;	/// 关卡数据等       .
	public	byte            [] m_XData3;	/// 保留数据         .
	public	byte            [] m_ActivityInfo;	/// 活动的xml扩展数据 .
	public	byte            [] m_Shop;	/// 商城的xml扩展数据 .
	public	byte            [] m_ClentCfg;	/// 客户端配置       .
	public SRoleDBInfo()
	{
		m_XData=new byte[(int)MAX_BIGXML_LEN];
		m_XData2=new byte[(int)MAX_XML_LEN];
		m_XData3=new byte[(int)MAX_XML_LEN];
		m_ActivityInfo=new byte[(int)MAX_XML_LEN];
		m_Shop=new byte[(int)MAX_XML_LEN];
		m_ClentCfg=new byte[(int)512];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[17];
		miArray[0].name="DBIndex";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="HP";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="SP";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="EP";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="VipEP";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="AP";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Experience";miArray[6].type=MemberInfo.MIType.eInt64;miArray[6].size=8;miArray[6].count=(int)0;
		miArray[7].name="NonMoney";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="NonCash";miArray[8].type=MemberInfo.MIType.eUInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="Cash";miArray[9].type=MemberInfo.MIType.eUInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="LatestWorldChatTime";miArray[10].type=MemberInfo.MIType.eUInt64;miArray[10].size=8;miArray[10].count=(int)0;
		miArray[11].name="XData";miArray[11].type=MemberInfo.MIType.eByte;miArray[11].size=1;miArray[11].count=(int)MAX_BIGXML_LEN;
		miArray[12].name="XData2";miArray[12].type=MemberInfo.MIType.eByte;miArray[12].size=1;miArray[12].count=(int)MAX_XML_LEN;
		miArray[13].name="XData3";miArray[13].type=MemberInfo.MIType.eByte;miArray[13].size=1;miArray[13].count=(int)MAX_XML_LEN;
		miArray[14].name="ActivityInfo";miArray[14].type=MemberInfo.MIType.eByte;miArray[14].size=1;miArray[14].count=(int)MAX_XML_LEN;
		miArray[15].name="Shop";miArray[15].type=MemberInfo.MIType.eByte;miArray[15].size=1;miArray[15].count=(int)MAX_XML_LEN;
		miArray[16].name="ClentCfg";miArray[16].type=MemberInfo.MIType.eByte;miArray[16].size=1;miArray[16].count=(int)512;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBIndex;
		case	1:return m_HP;
		case	2:return m_SP;
		case	3:return m_EP;
		case	4:return m_VipEP;
		case	5:return m_AP;
		case	6:return m_Experience;
		case	7:return m_NonMoney;
		case	8:return m_NonCash;
		case	9:return m_Cash;
		case	10:return m_LatestWorldChatTime;
		case	11:return m_XData;
		case	12:return m_XData2;
		case	13:return m_XData3;
		case	14:return m_ActivityInfo;
		case	15:return m_Shop;
		case	16:return m_ClentCfg;

		}
		return 0;
	}
};

public class SItemInfo	:	CMessage{
	public	byte             m_Index;
	public	byte             m_Slot;	/// 所在位置         .
	public	byte             m_ExInfoIndex;	/// 装备附属性对应位置，数组元素最大个数为没有 .
	public	byte             m_MakerInfoIndex;	/// 制作人信息对应位置，数组元素最大个数为没有该属性 .
	public	byte             m_TimeInfoIndex;	/// 时间信息对应位置，数组元素最大个数为没有该属性 .
	public	ushort           m_Count;	/// 数量             .
	public	uint             m_Flag;	/// 物品标记,见EItemInfoFlag .
	public	byte             m_PersonalFlag;	/// 特殊标记EItemPersonalFlag .
	public	uint             m_TemplateId;	/// 物品模板ID       .
	public	uint             m_ObjId;	/// 物品obj id       .
	public	UInt64           m_ItemUUId;	/// 物品模板ID       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[11];
		miArray[0].name="Index";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Slot";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="ExInfoIndex";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="MakerInfoIndex";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="TimeInfoIndex";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="Count";miArray[5].type=MemberInfo.MIType.eUShort;miArray[5].size=2;miArray[5].count=(int)0;
		miArray[6].name="Flag";miArray[6].type=MemberInfo.MIType.eUInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="PersonalFlag";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="TemplateId";miArray[8].type=MemberInfo.MIType.eUInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="ObjId";miArray[9].type=MemberInfo.MIType.eUInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="ItemUUId";miArray[10].type=MemberInfo.MIType.eUInt64;miArray[10].size=8;miArray[10].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Index;
		case	1:return m_Slot;
		case	2:return m_ExInfoIndex;
		case	3:return m_MakerInfoIndex;
		case	4:return m_TimeInfoIndex;
		case	5:return m_Count;
		case	6:return m_Flag;
		case	7:return m_PersonalFlag;
		case	8:return m_TemplateId;
		case	9:return m_ObjId;
		case	10:return m_ItemUUId;

		}
		return 0;
	}
};

public class SRollItemData	:	CMessage{
	public	byte             m_Type;	/// 参见EActCellObtainType .
	public	uint             m_ID;
	public	ushort           m_Num;
	public	int              m_BroadcastFlag;
	public	int              m_Rare;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Num";miArray[2].type=MemberInfo.MIType.eUShort;miArray[2].size=2;miArray[2].count=(int)0;
		miArray[3].name="BroadcastFlag";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Rare";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_ID;
		case	2:return m_Num;
		case	3:return m_BroadcastFlag;
		case	4:return m_Rare;

		}
		return 0;
	}
};

public class SRollData_Common	:	CMessage{
};

public class SRollData_Money	:	CMessage{
};

public class SRollData_Cell	:	CMessage{
};

public class SRollPoolItem	:	CMessage{
	public	uint             m_Template;
	public	int              m_Num;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Template";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Num";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Template;
		case	1:return m_Num;

		}
		return 0;
	}
};

/* 用于传输的物品结构体。仅适用于用templateId可以唯一确定的物品，不能使用于随机属性、强化等物品 */
public class SBaseItemInfo	:	CMessage{
	public	uint             m_TemplateId;
	public	int              m_Num;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="TemplateId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Num";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateId;
		case	1:return m_Num;

		}
		return 0;
	}
};

/* 用于传输的物品结构体，包括物品TemplateID/物品数量 */
public class SMultiItems	:	CMessage{
	public	int              m_TypeCount;	/// 物品种类数       .
	public	SBaseItemInfo   [] m_Items;
	public SMultiItems()
	{
		m_Items=new SBaseItemInfo[(int)MAX_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="TypeCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Items";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TypeCount;
		case	1:return m_Items;

		}
		return 0;
	}
};

public class SBlob	:	CMessage{
	public	uint             m_Len;	/// 数据长度         .
	public	byte            [] m_Data;	/// 数据             .
	public SBlob()
	{
		m_Data=new byte[(int)MAX_BLOB_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Len";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Data";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_BLOB_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Len;
		case	1:return m_Data;

		}
		return 0;
	}
};

public class SBigBlob	:	CMessage{
	public	uint             m_Len;	/// 数据长度         .
	public	byte            [] m_Data;	/// 数据             .
	public SBigBlob()
	{
		m_Data=new byte[(int)MAX_BIGBLOB_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Len";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Data";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_BIGBLOB_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Len;
		case	1:return m_Data;

		}
		return 0;
	}
};

public class SMediumBlob	:	CMessage{
	public	uint             m_Len;	/// 数据长度         .
	public	byte            [] m_Data;	/// 数据             .
	public SMediumBlob()
	{
		m_Data=new byte[(int)MAX_MEDIUMBLOB_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Len";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Data";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_MEDIUMBLOB_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Len;
		case	1:return m_Data;

		}
		return 0;
	}
};

public class SBigMoreBlob	:	CMessage{
	public	uint             m_Len;	/// 数据长度         .
	public	byte            [] m_Data;	/// 数据             .
	public SBigMoreBlob()
	{
		m_Data=new byte[(int)MAX_BIG_MORE_BLOB_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Len";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Data";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_BIG_MORE_BLOB_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Len;
		case	1:return m_Data;

		}
		return 0;
	}
};

public class SEXBigBlob	:	CMessage{
	public	uint             m_Len;	/// 数据长度         .
	public	byte            [] m_Data;	/// 数据             .
	public SEXBigBlob()
	{
		m_Data=new byte[(int)MAX_EXEBIGBLOB_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Len";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Data";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_EXEBIGBLOB_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Len;
		case	1:return m_Data;

		}
		return 0;
	}
};

public class SEquipInfo	:	CMessage{
	public	byte             m_Slot;	/// 装备位置         .
	public	uint             m_TemplateId;	/// 装备模板ID       .
	public	uint             m_HuanhuaTemplateId;	/// 幻化模板ID       .
	public	byte             m_StarCount;	/// 当前星级         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Slot";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="TemplateId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="HuanhuaTemplateId";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="StarCount";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Slot;
		case	1:return m_TemplateId;
		case	2:return m_HuanhuaTemplateId;
		case	3:return m_StarCount;

		}
		return 0;
	}
};

/* 基本角色信息 */
public class SRoleInfo	:	CMessage{
	public	UInt64           m_DBRoleId;	/// DB中的角色ID     .
	public	byte            [] m_RoleName;	/// 角色名           .
	public	int              m_Level;	/// 等级             .
	public	byte             m_BaseJob;	/// 基础职业         .
	public	byte             m_Job;	/// 职业             .
	public	byte             m_Gender;	/// 性别             .
	public	byte             m_SkinColor;	/// 肤色             .
	public	byte             m_HairStyle;	/// 发型             .
	public	byte            [] m_createtime;	/// 创建时间         .
	public	UInt64           m_LastLoginTime;	/// 最近登陆时间     .
	public	UInt64           m_LastLogoutTime;	/// 最近登出时间     .
	public	uint             m_SceneId;	/// 角色最后所在场景的模板id .
	public	uint             m_InstanceSceneId;	/// 角色最后所在场景的实例id .
	public	uint             m_LatestSceneId;	/// 角色上一次所在场景的模板id .
	public	uint             m_LatestInstanceSceneId;	/// 角色上一次所在场景的实例id .
	public	UInt64           m_ForbidenTime;	/// 禁止登陆到期时间 .
	public SRoleInfo()
	{
		m_RoleName=new byte[(int)ROLE_NAME_LEN];
		m_createtime=new byte[(int)64];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[16];
		miArray[0].name="DBRoleId";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RoleName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="Level";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="BaseJob";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="Job";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="Gender";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="SkinColor";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="HairStyle";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="createtime";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)64;
		miArray[9].name="LastLoginTime";miArray[9].type=MemberInfo.MIType.eUInt64;miArray[9].size=8;miArray[9].count=(int)0;
		miArray[10].name="LastLogoutTime";miArray[10].type=MemberInfo.MIType.eUInt64;miArray[10].size=8;miArray[10].count=(int)0;
		miArray[11].name="SceneId";miArray[11].type=MemberInfo.MIType.eUInt;miArray[11].size=4;miArray[11].count=(int)0;
		miArray[12].name="InstanceSceneId";miArray[12].type=MemberInfo.MIType.eUInt;miArray[12].size=4;miArray[12].count=(int)0;
		miArray[13].name="LatestSceneId";miArray[13].type=MemberInfo.MIType.eUInt;miArray[13].size=4;miArray[13].count=(int)0;
		miArray[14].name="LatestInstanceSceneId";miArray[14].type=MemberInfo.MIType.eUInt;miArray[14].size=4;miArray[14].count=(int)0;
		miArray[15].name="ForbidenTime";miArray[15].type=MemberInfo.MIType.eUInt64;miArray[15].size=8;miArray[15].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBRoleId;
		case	1:return m_RoleName;
		case	2:return m_Level;
		case	3:return m_BaseJob;
		case	4:return m_Job;
		case	5:return m_Gender;
		case	6:return m_SkinColor;
		case	7:return m_HairStyle;
		case	8:return m_createtime;
		case	9:return m_LastLoginTime;
		case	10:return m_LastLogoutTime;
		case	11:return m_SceneId;
		case	12:return m_InstanceSceneId;
		case	13:return m_LatestSceneId;
		case	14:return m_LatestInstanceSceneId;
		case	15:return m_ForbidenTime;

		}
		return 0;
	}
};

public class SXGEM	:	CMessage{
	public	int              m_TID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TID;

		}
		return 0;
	}
};

public class SXITEM	:	CMessage{
	public	UInt64           m_UID;
	public	int              m_TID;
	public	int              m_UP;
	public	int              m_CT;
	public	int              m_EXP;
	public	int              m_LK;
	public	ushort           m_GEMCount;
	public	SXGEM           [] m_GEM;
	public SXITEM()
	{
		m_GEM=new SXGEM[(int)MAX_GEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[8];
		miArray[0].name="UID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="TID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="UP";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="CT";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="EXP";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="LK";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="GEMCount";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="GEM";miArray[7].type=MemberInfo.MIType.eMessage;miArray[7].size=0;miArray[7].count=(int)MAX_GEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UID;
		case	1:return m_TID;
		case	2:return m_UP;
		case	3:return m_CT;
		case	4:return m_EXP;
		case	5:return m_LK;
		case	6:return m_GEMCount;
		case	7:return m_GEM;

		}
		return 0;
	}
};

public class SXITEMS	:	CMessage{
	public	ushort           m_ItemCount;
	public	SXITEM          [] m_Items;
	public SXITEMS()
	{
		m_Items=new SXITEM[(int)MAX_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemCount";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="Items";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemCount;
		case	1:return m_Items;

		}
		return 0;
	}
};

/* 带装备信息的角色信息 */
public class SRoleInfoWithEquip	:	CMessage{
	public	SRoleInfo        m_RoleInfo	=	new SRoleInfo();	/// roleinfo         .
	public	SXITEMS          m_Equip	=	new SXITEMS();	/// equip            .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="Equip";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleInfo;
		case	1:return m_Equip;

		}
		return 0;
	}
};

/* 带装备信息的角色信息 */
public class SRoleInfoWithEquipAll	:	CMessage{
	public	SRoleInfo        m_RoleInfo	=	new SRoleInfo();	/// SRoleInfo        .
	public	SRoleDBInfo      m_RoleDBInfo	=	new SRoleDBInfo();	/// SRoleDBInfo      .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="RoleDBInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleInfo;
		case	1:return m_RoleDBInfo;

		}
		return 0;
	}
};

/* 套装物品 */
public class SROLE_SUIT_ITEM	:	CMessage{
	public	UInt64           m_ItemUUID;	/// 物品UUID         .
	public	int              m_ItemID;	/// 物品模板ID       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="ItemID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_ItemID;

		}
		return 0;
	}
};

/* 强化某物品回应 */
public class SROLE_SUIT	:	CMessage{
	public	int              m_SuitID;	/// 套装ID           .
	public	int              m_SuitCount;	/// 套装激活套数     .
	public	SROLE_SUIT_ITEM [] m_SuitItem;	/// 物品信息         .
	public SROLE_SUIT()
	{
		m_SuitItem=new SROLE_SUIT_ITEM[(int)MAX_SUIT_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="SuitID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="SuitCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="SuitItem";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)MAX_SUIT_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SuitID;
		case	1:return m_SuitCount;
		case	2:return m_SuitItem;

		}
		return 0;
	}
};

/*  装备字头  */
public class SHeadRecord	:	CMessage{
	public	byte             m_RandomValue;	///  浮动值          .
	public	ushort           m_HeadID;	///  字头id          .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RandomValue";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="HeadID";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RandomValue;
		case	1:return m_HeadID;

		}
		return 0;
	}
};

/*  装备随机字头  */
public class SRandomEquipHead	:	CMessage{
	public	byte             m_IsIdentifyed;	/// 是否鉴定过       .
	public	byte             m_Count;	/// 字头数量         .
	public	SHeadRecord     [] m_Heads;	/// 字头id数组       .
	public SRandomEquipHead()
	{
		m_Heads=new SHeadRecord[(int)MAX_EQUIP_HEAD];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="IsIdentifyed";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="Heads";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)MAX_EQUIP_HEAD;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IsIdentifyed;
		case	1:return m_Count;
		case	2:return m_Heads;

		}
		return 0;
	}
};

public class SItemEquipExInfo	:	CMessage{
	public	byte             m_Level;	/// 等级             .
	public	byte             m_MaxLevel;	/// 最大等级         .
	public	byte             m_MaxHoleNum;	/// 最大孔数         .
	public	byte             m_Hole;	/// 孔数             .
	public	SRandomEquipHead m_RandomHead	=	new SRandomEquipHead();	/// 随机字头         .
	public	uint             m_Durability;	/// 耐久             .
	public	uint             m_MaxDurability;	/// 耐久度上限       .
	public	uint            [] m_GemID;	/// 镶嵌的宝石       .
	public	byte             m_SmithingRandom;	/// 装备锻造浮动值   .
	public	byte             m_StarRandom;	/// 装备升星浮动值   .
	public	byte             m_ReduceEquipNeedLevel;	/// 降低装备需求等级 .
	public	uint             m_HunhuaTemplateID;	/// 幻化模板id       .
	public	uint             m_HunhuaEndTime;	/// 幻化结束时间     .
	public	uint             m_BuffID;	/// BUFF模板ID       .
	public	uint             m_StarExp;	/// 星值             .
	public SItemEquipExInfo()
	{
		m_GemID=new uint[(int)MAX_ITEM_HOLE_NUM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[15];
		miArray[0].name="Level";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="MaxLevel";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="MaxHoleNum";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="Hole";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="RandomHead";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)0;
		miArray[5].name="Durability";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="MaxDurability";miArray[6].type=MemberInfo.MIType.eUInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="GemID";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)MAX_ITEM_HOLE_NUM;
		miArray[8].name="SmithingRandom";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;
		miArray[9].name="StarRandom";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="ReduceEquipNeedLevel";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="HunhuaTemplateID";miArray[11].type=MemberInfo.MIType.eUInt;miArray[11].size=4;miArray[11].count=(int)0;
		miArray[12].name="HunhuaEndTime";miArray[12].type=MemberInfo.MIType.eUInt;miArray[12].size=4;miArray[12].count=(int)0;
		miArray[13].name="BuffID";miArray[13].type=MemberInfo.MIType.eUInt;miArray[13].size=4;miArray[13].count=(int)0;
		miArray[14].name="StarExp";miArray[14].type=MemberInfo.MIType.eUInt;miArray[14].size=4;miArray[14].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Level;
		case	1:return m_MaxLevel;
		case	2:return m_MaxHoleNum;
		case	3:return m_Hole;
		case	4:return m_RandomHead;
		case	5:return m_Durability;
		case	6:return m_MaxDurability;
		case	7:return m_GemID;
		case	8:return m_SmithingRandom;
		case	9:return m_StarRandom;
		case	10:return m_ReduceEquipNeedLevel;
		case	11:return m_HunhuaTemplateID;
		case	12:return m_HunhuaEndTime;
		case	13:return m_BuffID;
		case	14:return m_StarExp;

		}
		return 0;
	}
};

public class SItemMakerInfo	:	CMessage{
	public	byte            [] m_Maker;	/// 制造者           .
	public	uint             m_Virtue;	/// 正值             .
	public	uint             m_Evil;	/// 邪值             .
	public	uint             m_Goodness;	/// 善值             .
	public	uint             m_Badness;	/// 恶值             .
	public SItemMakerInfo()
	{
		m_Maker=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Maker";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)ROLE_NAME_LEN;
		miArray[1].name="Virtue";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Evil";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Goodness";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Badness";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Maker;
		case	1:return m_Virtue;
		case	2:return m_Evil;
		case	3:return m_Goodness;
		case	4:return m_Badness;

		}
		return 0;
	}
};

public class SItemTimeInfo	:	CMessage{
	public	byte             m_UseNum;	/// 剩余使用次数     .
	public	uint             m_DestroyTime;	/// 消失剩余时间     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="UseNum";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="DestroyTime";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UseNum;
		case	1:return m_DestroyTime;

		}
		return 0;
	}
};

public class SNull	:	CMessage{
};

public class UItemExInfo	:	CMessage{
	public	SNull            m_Null	=	new SNull();
	public	SItemEquipExInfo m_EquipExInfo	=	new SItemEquipExInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Null";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="EquipExInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Null;
		case	1:return m_EquipExInfo;

		}
		return 0;
	}
};

public class SItemExInfo	:	CMessage{
	public	byte             m_Type;
	public	UItemExInfo      m_ExInfo	=	new UItemExInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ExInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_ExInfo;

		}
		return 0;
	}
};

public class SExchangeItem	:	CMessage{
	public	byte             m_Add;	/// 添加还是删除     .
	public	uint             m_TemplateId;	/// 物品模板ID       .
	public	byte             m_FromIndex;	/// 从，序列号       .
	public	byte             m_FromSlot;	/// 从，位置         .
	public	byte             m_ToSlot;	/// 到,位置          .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Add";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="TemplateId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="FromIndex";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="FromSlot";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="ToSlot";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Add;
		case	1:return m_TemplateId;
		case	2:return m_FromIndex;
		case	3:return m_FromSlot;
		case	4:return m_ToSlot;

		}
		return 0;
	}
};

public class SItemShortcut	:	CMessage{
	public	byte             m_Slot;	/// 所在位置         .
	public	uint             m_TemplateId;	/// 物品模板ID       .
	public	UInt64           m_ItemUUId;	/// 物品模板ID       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Slot";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="TemplateId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="ItemUUId";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Slot;
		case	1:return m_TemplateId;
		case	2:return m_ItemUUId;

		}
		return 0;
	}
};

public class SRoleName	:	CMessage{
	public	byte            [] m_RoleName;	/// 角色名字         .
	public	UInt64           m_RoleID;
	public	uint             m_Level;	/// 角色等级         .
	public	uint             m_Job;	/// 角色门派         .
	public SRoleName()
	{
		m_RoleName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="RoleName";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)ROLE_NAME_LEN;
		miArray[1].name="RoleID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Level";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Job";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleName;
		case	1:return m_RoleID;
		case	2:return m_Level;
		case	3:return m_Job;

		}
		return 0;
	}
};

public class SServerInfo	:	CMessage{
	public	uint             m_ServerID;
	public	uint             m_ListenIP;
	public	ushort           m_ListenPort;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ServerID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="ListenIP";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="ListenPort";miArray[2].type=MemberInfo.MIType.eUShort;miArray[2].size=2;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ServerID;
		case	1:return m_ListenIP;
		case	2:return m_ListenPort;

		}
		return 0;
	}
};

/* 月卡信息 */
public class MonthCardInfo	:	CMessage{
	public	int              m_UId;	/// 月卡ID           .
	public	UInt64           m_BTm;	/// 购买时间         .
	public	int              m_Days;	/// 持续时间，一个月卡可以多次购买，时间叠加 .
	public	UInt64           m_UTm;	/// 上次领取时间，0表未领取 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="UId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BTm";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Days";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="UTm";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UId;
		case	1:return m_BTm;
		case	2:return m_Days;
		case	3:return m_UTm;

		}
		return 0;
	}
};

/* 账号的VIP相关信息,目前包括充值、GM等级 */
public class SLMAccountInfo	:	CMessage{
	public	UInt64           m_AccountId;	/// 账号 即digitid   .
	public	int              m_RMBCount;	/// 账号充值次数(RMB) .
	public	int              m_RMBSum;	/// 账号充值总金额(RMB) .
	public	uint             m_Cash;	/// 徽章数           .
	public	int              m_VIPLevel;	/// 账号VIP等级      .
	public	byte             m_GMLevel;	/// 账号GM等级       .
	public	byte             m_MonthCardCount;	/// 月卡数量         .
	public	MonthCardInfo   [] m_MonthCardInfos;	/// 账号月卡信息     .
	public SLMAccountInfo()
	{
		m_MonthCardInfos=new MonthCardInfo[(int)MAX_MONTHCARD_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[8];
		miArray[0].name="AccountId";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RMBCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="RMBSum";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Cash";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="VIPLevel";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="GMLevel";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="MonthCardCount";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="MonthCardInfos";miArray[7].type=MemberInfo.MIType.eMessage;miArray[7].size=0;miArray[7].count=(int)MAX_MONTHCARD_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AccountId;
		case	1:return m_RMBCount;
		case	2:return m_RMBSum;
		case	3:return m_Cash;
		case	4:return m_VIPLevel;
		case	5:return m_GMLevel;
		case	6:return m_MonthCardCount;
		case	7:return m_MonthCardInfos;

		}
		return 0;
	}
};

public class SGameServerDescInfo	:	CMessage{
	public	byte             m_OptLen;
	public	byte            [] m_Opt;
	public	ushort           m_DescLen;
	public	byte            [] m_Desc;
	public	ushort           m_StateLen;
	public	byte            [] m_State;
	public SGameServerDescInfo()
	{
		m_Opt=new byte[(int)255];
		m_Desc=new byte[(int)MAX_SERVER_DESC_LEN];
		m_State=new byte[(int)MAX_SERVER_STATE_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="OptLen";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Opt";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)255;
		miArray[2].name="DescLen";miArray[2].type=MemberInfo.MIType.eUShort;miArray[2].size=2;miArray[2].count=(int)0;
		miArray[3].name="Desc";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)MAX_SERVER_DESC_LEN;
		miArray[4].name="StateLen";miArray[4].type=MemberInfo.MIType.eUShort;miArray[4].size=2;miArray[4].count=(int)0;
		miArray[5].name="State";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)MAX_SERVER_STATE_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_OptLen;
		case	1:return m_Opt;
		case	2:return m_DescLen;
		case	3:return m_Desc;
		case	4:return m_StateLen;
		case	5:return m_State;

		}
		return 0;
	}
};

public class SGameServerInfo	:	CMessage{
	public	SServerInfo      m_Server	=	new SServerInfo();
	public	ushort           m_PlayerCount;
	public	byte             m_ServerState;
	public	SGameServerDescInfo m_Desc	=	new SGameServerDescInfo();
	public	uint             m_ServerUniqueId;	/// 服的唯一id，跨区唯一 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Server";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="PlayerCount";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)0;
		miArray[2].name="ServerState";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="Desc";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="ServerUniqueId";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Server;
		case	1:return m_PlayerCount;
		case	2:return m_ServerState;
		case	3:return m_Desc;
		case	4:return m_ServerUniqueId;

		}
		return 0;
	}
};

public class CTDT_CONNECTION_INFO	:	CMessage{
	public	uint             m_IncomingBytesPerMinute;
	public	uint             m_OutgoingBytesPerMinute;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="IncomingBytesPerMinute";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="OutgoingBytesPerMinute";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IncomingBytesPerMinute;
		case	1:return m_OutgoingBytesPerMinute;

		}
		return 0;
	}
};

/* 物品信息结构 */
public class SUUItemInfo	:	CMessage{
	public	ushort           m_Count;	///  数量            .
	public	uint             m_TemplateId;	///  物品模板ID      .
	public	UInt64           m_UUID;	/// UUID             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="TemplateId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="UUID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_TemplateId;
		case	2:return m_UUID;

		}
		return 0;
	}
};

/* 物品清单，[DB]要进数据库，请保持子结构SUUItemInfo不含嵌套结构 */
public class SItemList	:	CMessage{
	public	ushort           m_Count;	/// 物品数量         .
	public	SUUItemInfo     [] m_ItemList;
	public SItemList()
	{
		m_ItemList=new SUUItemInfo[(int)MAX_ROLE_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="ItemList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ROLE_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_ItemList;

		}
		return 0;
	}
};

/* 关卡数据 */
public class SLevelInfo	:	CMessage{
	public	uint             m_LevelID;	/// 关卡id           .
	public	byte             m_Crystal;	/// 获得的星数       .
	public	ushort           m_UsedTimes;	/// 当日进入次数，次数上限请查阅level.csv表 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="LevelID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Crystal";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="UsedTimes";miArray[2].type=MemberInfo.MIType.eUShort;miArray[2].size=2;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LevelID;
		case	1:return m_Crystal;
		case	2:return m_UsedTimes;

		}
		return 0;
	}
};

/* 战役相关数据 */
public class SBattleInfo	:	CMessage{
	public	uint             m_BattleID;	/// 战役id           .
	public	ushort           m_GetStars;	/// 战役已获取的星数 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="BattleID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GetStars";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattleID;
		case	1:return m_GetStars;

		}
		return 0;
	}
};

public class SCHAT	:	CMessage{
	public	byte             m_ChatType;	/// 见EChatType      .
	public	byte            [] m_Who;
	public	byte            [] m_ToWho;
	public	byte            [] m_Content;
	public	byte             m_GmLevel;	/// GM命令使用者的GM等级  .
	public	byte             m_SpeakerVIPLevel;	/// 发送者的vip等级  .
	public SCHAT()
	{
		m_Who=new byte[(int)ROLE_NAME_LEN];
		m_ToWho=new byte[(int)ROLE_NAME_LEN];
		m_Content=new byte[(int)MAX_MSG_CONTENT_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="ChatType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Who";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="ToWho";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="Content";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)MAX_MSG_CONTENT_LEN;
		miArray[4].name="GmLevel";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="SpeakerVIPLevel";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ChatType;
		case	1:return m_Who;
		case	2:return m_ToWho;
		case	3:return m_Content;
		case	4:return m_GmLevel;
		case	5:return m_SpeakerVIPLevel;

		}
		return 0;
	}
};

public class SFriendEquipInfo	:	CMessage{
	public	uint             m_TemplateID;	/// 装备模板id       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateID;

		}
		return 0;
	}
};

public class SFriendPetInfo	:	CMessage{
	public	uint             m_TemplateID;	/// 模板id           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateID;

		}
		return 0;
	}
};

public class SRSNull	:	CMessage{
};

/* 角色基础属性 */
public class SRSRoleBaseData	:	CMessage{
	public	uint             m_Level;
	public	uint             m_Attack;
	public	int              m_PVPWins;
	public	int              m_PVPRepute;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Level";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Attack";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="PVPWins";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="PVPRepute";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Level;
		case	1:return m_Attack;
		case	2:return m_PVPWins;
		case	3:return m_PVPRepute;

		}
		return 0;
	}
};

/* 角色装备信息 */
public class SRSRoleEquipData	:	CMessage{
	public	byte             m_Count;
	public	SXITEM          [] m_EquipInfo;
	public SRSRoleEquipData()
	{
		m_EquipInfo=new SXITEM[(int)MAX_EQUIP_ITEM_NUM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="EquipInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_EQUIP_ITEM_NUM;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_EquipInfo;

		}
		return 0;
	}
};

/* 角色宠物信息 */
public class SRSRolePetData	:	CMessage{
	public	byte             m_Count;
	public	SFriendPetInfo  [] m_PetInfo;
	public SRSRolePetData()
	{
		m_PetInfo=new SFriendPetInfo[(int)MAX_FRIENDS_PET_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="PetInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_FRIENDS_PET_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_PetInfo;

		}
		return 0;
	}
};

/* 角色在线信息 */
public class SRSRoleOnlineData	:	CMessage{
	public	byte             m_IsOnline;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="IsOnline";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IsOnline;

		}
		return 0;
	}
};

public class URSRoleProperty	:	CMessage{
	public	SRSNull          m_Null	=	new SRSNull();
	public	SRSRoleBaseData  m_Base	=	new SRSRoleBaseData();
	public	SRSRoleEquipData m_Equip	=	new SRSRoleEquipData();
	public	SRSRolePetData   m_Pet	=	new SRSRolePetData();
	public	SRSRoleOnlineData m_Online	=	new SRSRoleOnlineData();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Null";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="Base";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Equip";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;
		miArray[3].name="Pet";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="Online";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Null;
		case	1:return m_Base;
		case	2:return m_Equip;
		case	3:return m_Pet;
		case	4:return m_Online;

		}
		return 0;
	}
};

public class SRSRoleProperty	:	CMessage{
	public	byte             m_Type;	/// 类型 RS_ROLE_PROERTY_TYPE .
	public	URSRoleProperty  m_Property	=	new URSRoleProperty();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Property";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_Property;

		}
		return 0;
	}
};

public class SFriendInfo	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public	uint             m_Head;	/// 头像             .
	public	byte            [] m_Name;	/// 名字             .
	public	byte             m_SkinColor;	/// 肤色             .
	public	byte             m_Sex;	/// 性别             .
	public	byte             m_HairStyle;	/// 发型             .
	public	ushort           m_Level;	/// 等级             .
	public	byte             m_Job;	/// 职业             .
	public	uint             m_Attack;	/// 战斗力           .
	public	byte             m_IsOnline;	/// 是否在线         .
	public	uint             m_ActionPoint;	/// 此好友给自己赠送的体力 .
	public	byte             m_CanDonateActionPoint;	/// 是否可给此好友赠送体力 .
	public	SRSRoleEquipData m_Equip	=	new SRSRoleEquipData();	/// 装备信息         .
	public	SRSRolePetData   m_Pet	=	new SRSRolePetData();	/// 宠物信息         .
	public SFriendInfo()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[14];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Head";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Name";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="SkinColor";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="Sex";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="HairStyle";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="Level";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="Job";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="Attack";miArray[8].type=MemberInfo.MIType.eUInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="IsOnline";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="ActionPoint";miArray[10].type=MemberInfo.MIType.eUInt;miArray[10].size=4;miArray[10].count=(int)0;
		miArray[11].name="CanDonateActionPoint";miArray[11].type=MemberInfo.MIType.eByte;miArray[11].size=1;miArray[11].count=(int)0;
		miArray[12].name="Equip";miArray[12].type=MemberInfo.MIType.eMessage;miArray[12].size=0;miArray[12].count=(int)0;
		miArray[13].name="Pet";miArray[13].type=MemberInfo.MIType.eMessage;miArray[13].size=0;miArray[13].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Head;
		case	2:return m_Name;
		case	3:return m_SkinColor;
		case	4:return m_Sex;
		case	5:return m_HairStyle;
		case	6:return m_Level;
		case	7:return m_Job;
		case	8:return m_Attack;
		case	9:return m_IsOnline;
		case	10:return m_ActionPoint;
		case	11:return m_CanDonateActionPoint;
		case	12:return m_Equip;
		case	13:return m_Pet;

		}
		return 0;
	}
};

/* 邀请者信息 */
public class SFriendInviteInfo	:	CMessage{
	public	uint             m_Time;	/// 邀请时间         .
	public	SFriendInfo      m_Info	=	new SFriendInfo();	/// 邀请者信息       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_Info;

		}
		return 0;
	}
};

/* 邀请列表 */
public class SFriendInviteList	:	CMessage{
	public	uint             m_InviteCount;	/// 数量             .
	public	SFriendInviteInfo[] m_InviteList;	/// 邀请列表         .
	public SFriendInviteList()
	{
		m_InviteList=new SFriendInviteInfo[(int)MAX_FRIENDS_INVITE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="InviteCount";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="InviteList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_FRIENDS_INVITE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_InviteCount;
		case	1:return m_InviteList;

		}
		return 0;
	}
};

public class SRoleBaseInfo	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public	byte            [] m_Name;	/// 名字             .
	public	byte             m_SkinColor;	/// 肤色             .
	public	byte             m_Sex;	/// 性别             .
	public	byte             m_HairStyle;	/// 发型             .
	public	ushort           m_Level;	/// 等级             .
	public	byte             m_Job;	/// 职业             .
	public	uint             m_Attack;	/// 战斗力           .
	public	int              m_PVPWins;
	public	int              m_PVPRepute;
	public	byte             m_IsOnline;	/// 是否在线         .
	public	int              m_VIPLevel;	/// 账号VIP等级      .
	public SRoleBaseInfo()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[12];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Name";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="SkinColor";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="Sex";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="HairStyle";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="Level";miArray[5].type=MemberInfo.MIType.eUShort;miArray[5].size=2;miArray[5].count=(int)0;
		miArray[6].name="Job";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="Attack";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="PVPWins";miArray[8].type=MemberInfo.MIType.eInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="PVPRepute";miArray[9].type=MemberInfo.MIType.eInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="IsOnline";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="VIPLevel";miArray[11].type=MemberInfo.MIType.eInt;miArray[11].size=4;miArray[11].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Name;
		case	2:return m_SkinColor;
		case	3:return m_Sex;
		case	4:return m_HairStyle;
		case	5:return m_Level;
		case	6:return m_Job;
		case	7:return m_Attack;
		case	8:return m_PVPWins;
		case	9:return m_PVPRepute;
		case	10:return m_IsOnline;
		case	11:return m_VIPLevel;

		}
		return 0;
	}
};

public class SDBPET	:	CMessage{
	public	UInt64           m_UID;	/// 唯一ID           .
	public	uint             m_TID;	/// 模板ID           .
	public	int              m_LV;	/// 等级             .
	public	Int64            m_EXP;	/// 经验             .
	public	int              m_HP;	/// HP               .
	public	int              m_SP;	/// SP               .
	public	int              m_UP;	/// 强化等级         .
	public	int              m_LOCK;	/// 是否锁定         .
	public	SXITEMS          m_EQ	=	new SXITEMS();	/// 装备             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[9];
		miArray[0].name="UID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="TID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="LV";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="EXP";miArray[3].type=MemberInfo.MIType.eInt64;miArray[3].size=8;miArray[3].count=(int)0;
		miArray[4].name="HP";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="SP";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="UP";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="LOCK";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="EQ";miArray[8].type=MemberInfo.MIType.eMessage;miArray[8].size=0;miArray[8].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UID;
		case	1:return m_TID;
		case	2:return m_LV;
		case	3:return m_EXP;
		case	4:return m_HP;
		case	5:return m_SP;
		case	6:return m_UP;
		case	7:return m_LOCK;
		case	8:return m_EQ;

		}
		return 0;
	}
};

public class SRSBattlePets	:	CMessage{
	public	byte             m_Count;	/// 数量             .
	public	SDBPET          [] m_Pets;	/// 宠物             .
	public SRSBattlePets()
	{
		m_Pets=new SDBPET[(int)MAX_BATTLE_PET_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Pets";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_BATTLE_PET_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Pets;

		}
		return 0;
	}
};

public class SRoleRsServerInfo	:	CMessage{
	public	SRoleBaseInfo    m_BaseInfo	=	new SRoleBaseInfo();	/// 基础信息         .
	public	SRSRoleEquipData m_Equip	=	new SRSRoleEquipData();	/// 装备信息         .
	public	SRSRolePetData   m_Pet	=	new SRSRolePetData();	/// 宠物信息         .
	public	SRSBattlePets    m_BattlePets	=	new SRSBattlePets();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="BaseInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="Equip";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Pet";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;
		miArray[3].name="BattlePets";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BaseInfo;
		case	1:return m_Equip;
		case	2:return m_Pet;
		case	3:return m_BattlePets;

		}
		return 0;
	}
};

public class SAssistBattlePersonInfo	:	CMessage{
	public	byte             m_IsFriend;	/// 是否是好友       .
	public	SFriendInfo      m_Info	=	new SFriendInfo();	/// 助战人信息       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="IsFriend";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IsFriend;
		case	1:return m_Info;

		}
		return 0;
	}
};

public class SSeverAssistBattleInfo	:	CMessage{
	public	SAssistBattlePersonInfo m_Assistor	=	new SAssistBattlePersonInfo();	/// 助战人信息       .
	public	SDBPET           m_BattlePet	=	new SDBPET();	/// 战斗宠物         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Assistor";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="BattlePet";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Assistor;
		case	1:return m_BattlePet;

		}
		return 0;
	}
};

/* 好友列表通知 */
public class FRIENDS_LIST_NTF	:	CMessage{
	public	uint             m_Count;	/// 数量             .
	public	SFriendInfo     [] m_List;	/// 列表             .
	public	SFriendInviteList m_InviteList	=	new SFriendInviteList();	/// 邀请列表         .
	public FRIENDS_LIST_NTF()
	{
		m_List=new SFriendInfo[(int)MAX_FRIENDS_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="List";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_FRIENDS_COUNT;
		miArray[2].name="InviteList";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_List;
		case	2:return m_InviteList;

		}
		return 0;
	}
};

/* 查询指定名字角色 */
public class QUERY_ROLE_REQ	:	CMessage{
	public	uint             m_Page;	/// 请求的页码       .
	public	byte            [] m_KeyWords;	/// 关键字           .
	public QUERY_ROLE_REQ()
	{
		m_KeyWords=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Page";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="KeyWords";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Page;
		case	1:return m_KeyWords;

		}
		return 0;
	}
};

/* 查询指定名字角色反馈 */
public class QUERY_ROLE_ACK	:	CMessage{
	public	uint             m_Count;	/// 条数             .
	public	uint             m_Page;	/// 当前页数         .
	public	uint             m_TotalPage;	/// 总业数           .
	public	SFriendInfo     [] m_Lists;	/// 查询列表         .
	public QUERY_ROLE_ACK()
	{
		m_Lists=new SFriendInfo[(int)MAX_FRIENDS_COUNT_PER_PAGE];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Page";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="TotalPage";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Lists";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)MAX_FRIENDS_COUNT_PER_PAGE;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Page;
		case	2:return m_TotalPage;
		case	3:return m_Lists;

		}
		return 0;
	}
};

/* 添加好友 */
public class ADD_FRIEND_REQ	:	CMessage{
	public	byte            [] m_Name;	/// 名字             .
	public ADD_FRIEND_REQ()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Name";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Name;

		}
		return 0;
	}
};

/* 添加好友反馈 */
public class ADD_FRIEND_ACK	:	CMessage{
	public	byte             m_Result;	/// 结果             .
	public	byte             m_Count;	/// 数量             .
	public	SFriendInfo     [] m_Friends;	/// 好友数量,用数组是为了避免失败时不用额外发送好友信息 .
	public ADD_FRIEND_ACK()
	{
		m_Friends=new SFriendInfo[(int)1];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="Friends";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)1;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Count;
		case	2:return m_Friends;

		}
		return 0;
	}
};

/* 删除好友 */
public class REMOVE_FRIEND_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 删除好友反馈 */
public class REMOVE_FRIEND_ACK	:	CMessage{
	public	byte             m_Result;	/// 结果             .
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="RoleDBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_RoleDBID;

		}
		return 0;
	}
};

/* 赠送体力 */
public class DONATE_ACTION_POINT_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 赠送体力反馈 */
public class DONATE_ACTION_POINT_ACK	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 被赠送角色dbid   .
	public	byte             m_Result;	/// 结果             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Result;

		}
		return 0;
	}
};

/* 赠送体力通知被赠送者 */
public class DONATE_ACTION_POINT_OTHER_NTF	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 增送者dbid       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 领取体力 */
public class RECEIVE_ACTION_POINT_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 被领取好友dbid   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 领取体力反馈 */
public class RECEIVE_ACTION_POINT_ACK	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 被领取好友dbid   .
	public	byte             m_Result;	/// 结果             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Result;

		}
		return 0;
	}
};

/* 别人请求成为好友通知 */
public class REQUEST_ADD_FRIEND_NTF	:	CMessage{
	public	SFriendInviteInfo m_NewInfo	=	new SFriendInviteInfo();	/// 好友信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="NewInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_NewInfo;

		}
		return 0;
	}
};

/* 回应添加好友请求 */
public class RESPONSE_ADD_FRIEND_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public	byte             m_Agree;	/// 是否同意         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Agree";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Agree;

		}
		return 0;
	}
};

/* 删除邀请列表中的请 */
public class FRIEND_REMOVE_INVITE_NTF	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 好友信息变化通知 */
public class FRIEND_INFO_CHANGE_NTF	:	CMessage{
	public	SRSRoleProperty  m_Info	=	new SRSRoleProperty();
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="RoleDBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;
		case	1:return m_RoleDBID;

		}
		return 0;
	}
};

/* 好友助战列表请求 */
public class FRIEND_ASSIST_LIST_REQ	:	CMessage{
};

/* 选择助战好友请求 */
public class SELECT_FRIEND_ASSIST_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 选择助战好友反馈 */
public class SELECT_FRIEND_ASSIST_ACK	:	CMessage{
	public	UInt64           m_RoleDBID;	/// 角色dbid         .
	public	byte             m_Result;	/// 结果             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_Result;

		}
		return 0;
	}
};

public class SAbyssRsServerInfo	:	CMessage{
	public	UInt64           m_ActDBID;	/// 活动dbid         .
	public	uint             m_ActTmpId;	/// 活动配置模板的index .
	public	int              m_Blood;	/// 怪物血量         .
	public	UInt64           m_Roleid;	/// 拥有者的角色id   .
	public	byte            [] m_Rolename;
	public	int              m_Atkcount;	/// 攻击者数量       .
	public	UInt64          [] m_Atklist;	/// 攻击深渊的角色id列表 .
	public	int              m_Status;	/// 状态 0-初始 1-触发 2-打开 3-被杀死 4-逃跑 .
	public	UInt64           m_Tritime;	/// 触发时间         .
	public	UInt64           m_Opentime;	/// 打开时间         .
	public	UInt64           m_Killtime;	/// 杀死时间         .
	public	UInt64           m_EntranceExistTime;	/// 触发的入口存在的时间 .
	public	UInt64           m_AbyssExistTime;	/// 打开的入口存在时间 .
	public SAbyssRsServerInfo()
	{
		m_Rolename=new byte[(int)ROLE_NAME_LEN];
		m_Atklist=new UInt64[(int)MAX_FRIEND_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[13];
		miArray[0].name="ActDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="ActTmpId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Blood";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Roleid";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;
		miArray[4].name="Rolename";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)ROLE_NAME_LEN;
		miArray[5].name="Atkcount";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Atklist";miArray[6].type=MemberInfo.MIType.eUInt64;miArray[6].size=8;miArray[6].count=(int)MAX_FRIEND_COUNT;
		miArray[7].name="Status";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="Tritime";miArray[8].type=MemberInfo.MIType.eUInt64;miArray[8].size=8;miArray[8].count=(int)0;
		miArray[9].name="Opentime";miArray[9].type=MemberInfo.MIType.eUInt64;miArray[9].size=8;miArray[9].count=(int)0;
		miArray[10].name="Killtime";miArray[10].type=MemberInfo.MIType.eUInt64;miArray[10].size=8;miArray[10].count=(int)0;
		miArray[11].name="EntranceExistTime";miArray[11].type=MemberInfo.MIType.eUInt64;miArray[11].size=8;miArray[11].count=(int)0;
		miArray[12].name="AbyssExistTime";miArray[12].type=MemberInfo.MIType.eUInt64;miArray[12].size=8;miArray[12].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ActDBID;
		case	1:return m_ActTmpId;
		case	2:return m_Blood;
		case	3:return m_Roleid;
		case	4:return m_Rolename;
		case	5:return m_Atkcount;
		case	6:return m_Atklist;
		case	7:return m_Status;
		case	8:return m_Tritime;
		case	9:return m_Opentime;
		case	10:return m_Killtime;
		case	11:return m_EntranceExistTime;
		case	12:return m_AbyssExistTime;

		}
		return 0;
	}
};

/* 触发深渊请求 */
public class ABYSS_TRIGGER_REQ	:	CMessage{
	public	UInt64           m_AbyssDBID;
	public	uint             m_AbyTemplateID;	/// 配置的深渊id模板 .
	public	byte            [] m_AbyName;
	public	uint             m_MonsterID;	/// 怪物id           .
	public	uint             m_TotalBlood;	/// 怪物总血量       .
	public	UInt64           m_TriggerTime;	/// 触发时间         .
	public	UInt64           m_EntranceExistTime;	/// 触发的入口存在的时间 .
	public	UInt64           m_AbyssExistTime;	/// 打开的入口存在时间 .
	public	byte            [] m_Rolename;
	public	byte             m_Job;	/// 职业             .
	public ABYSS_TRIGGER_REQ()
	{
		m_AbyName=new byte[(int)MAX_ABYSS_NAME_STRING_COUNT];
		m_Rolename=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[10];
		miArray[0].name="AbyssDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="AbyTemplateID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="AbyName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)MAX_ABYSS_NAME_STRING_COUNT;
		miArray[3].name="MonsterID";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="TotalBlood";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="TriggerTime";miArray[5].type=MemberInfo.MIType.eUInt64;miArray[5].size=8;miArray[5].count=(int)0;
		miArray[6].name="EntranceExistTime";miArray[6].type=MemberInfo.MIType.eUInt64;miArray[6].size=8;miArray[6].count=(int)0;
		miArray[7].name="AbyssExistTime";miArray[7].type=MemberInfo.MIType.eUInt64;miArray[7].size=8;miArray[7].count=(int)0;
		miArray[8].name="Rolename";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)ROLE_NAME_LEN;
		miArray[9].name="Job";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssDBID;
		case	1:return m_AbyTemplateID;
		case	2:return m_AbyName;
		case	3:return m_MonsterID;
		case	4:return m_TotalBlood;
		case	5:return m_TriggerTime;
		case	6:return m_EntranceExistTime;
		case	7:return m_AbyssExistTime;
		case	8:return m_Rolename;
		case	9:return m_Job;

		}
		return 0;
	}
};

/* 触发深渊反馈 */
public class ABYSS_TRIGGER_ACK	:	CMessage{
	public	byte             m_IfTrigger;	/// 是否触发 1表示触发 其他不触发 .
	public	SAbyssRsServerInfo m_AbyssInfo	=	new SAbyssRsServerInfo();	/// 深渊信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="IfTrigger";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="AbyssInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IfTrigger;
		case	1:return m_AbyssInfo;

		}
		return 0;
	}
};

/* 打开深渊请求 */
public class ABYSS_OPEN_REQ	:	CMessage{
	public	UInt64           m_AbyssDBID;
	public	UInt64           m_OpenTime;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="AbyssDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="OpenTime";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssDBID;
		case	1:return m_OpenTime;

		}
		return 0;
	}
};

/* 打开深渊反馈 */
public class ABYSS_OPEN_ACK	:	CMessage{
	public	SAbyssRsServerInfo m_AbyssInfo	=	new SAbyssRsServerInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="AbyssInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssInfo;

		}
		return 0;
	}
};

/* 通知朋友有打开的深渊 */
public class ABYSS_OPEN_NTF	:	CMessage{
	public	int              m_AbyssCount;
	public	SAbyssRsServerInfo[] m_AbyssInfo;
	public ABYSS_OPEN_NTF()
	{
		m_AbyssInfo=new SAbyssRsServerInfo[(int)MAX_ABYSS_OPEN_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="AbyssCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="AbyssInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ABYSS_OPEN_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssCount;
		case	1:return m_AbyssInfo;

		}
		return 0;
	}
};

/* 获取触发的深渊列表请求 */
public class GET_ABYSS_TRIGGER_LIST_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 获取触发的深渊列表的反馈 */
public class GET_ABYSS_TRIGGER_LIST_ACK	:	CMessage{
	public	int              m_AbyssCount;
	public	SAbyssRsServerInfo[] m_AbyssInfo;
	public GET_ABYSS_TRIGGER_LIST_ACK()
	{
		m_AbyssInfo=new SAbyssRsServerInfo[(int)MAX_ABYSS_OPEN_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="AbyssCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="AbyssInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ABYSS_OPEN_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssCount;
		case	1:return m_AbyssInfo;

		}
		return 0;
	}
};

/* 获取打开的深渊列表的请求 */
public class GET_ABYSS_OPEN_LIST_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 获取打开的深渊列表的反馈 */
public class GET_ABYSS_OPEN_LIST_ACK	:	CMessage{
	public	int              m_AbyssCount;
	public	SAbyssRsServerInfo[] m_AbyssInfo;
	public GET_ABYSS_OPEN_LIST_ACK()
	{
		m_AbyssInfo=new SAbyssRsServerInfo[(int)MAX_ABYSS_OPEN_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="AbyssCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="AbyssInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_ABYSS_OPEN_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AbyssCount;
		case	1:return m_AbyssInfo;

		}
		return 0;
	}
};

public class ABYSS_OPEN_RECORD	:	CMessage{
	public	UInt64           m_Abydbid;	/// 深渊dbid         .
	public	uint             m_AbyTmpid;
	public	UInt64           m_Opentime;
	public	UInt64           m_Roleid;
	public	byte            [] m_Rolename;
	public ABYSS_OPEN_RECORD()
	{
		m_Rolename=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Abydbid";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="AbyTmpid";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Opentime";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;
		miArray[3].name="Roleid";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;
		miArray[4].name="Rolename";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Abydbid;
		case	1:return m_AbyTmpid;
		case	2:return m_Opentime;
		case	3:return m_Roleid;
		case	4:return m_Rolename;

		}
		return 0;
	}
};

/* 获取打开记录请求 */
public class GET_ABYSS_OPEN_REC_REQ	:	CMessage{
	public	UInt64           m_RoleDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;

		}
		return 0;
	}
};

/* 获取打开记录反馈 */
public class GET_ABYSS_OPEN_REC_ACK	:	CMessage{
	public	int              m_Count;	/// 数量             .
	public	ABYSS_OPEN_RECORD[] m_Records;
	public GET_ABYSS_OPEN_REC_ACK()
	{
		m_Records=new ABYSS_OPEN_RECORD[(int)MAX_XABYSS_RECORD_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Records";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_XABYSS_RECORD_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Records;

		}
		return 0;
	}
};

/* 攻击深渊的角色 */
public class ATK_INFO	:	CMessage{
	public	UInt64           m_Roleid;	/// 攻击者id         .
	public	byte             m_Job;	/// 职业             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Roleid";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Job";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Roleid;
		case	1:return m_Job;

		}
		return 0;
	}
};

/* 攻击深渊的角色列表 */
public class ATK_INFO_LIST	:	CMessage{
	public	int              m_Atkcount;	/// 攻击者数量       .
	public	ATK_INFO        [] m_Atklist;	/// 攻击深渊的角色列表 .
	public ATK_INFO_LIST()
	{
		m_Atklist=new ATK_INFO[(int)MAX_FRIEND_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Atkcount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Atklist";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_FRIEND_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Atkcount;
		case	1:return m_Atklist;

		}
		return 0;
	}
};

/* 深渊信息 */
public class ABYSS_SEND_REWARD_MAIL	:	CMessage{
	public	uint             m_TmpID;
	public	UInt64           m_Roleid;	/// 拥有者的角色id   .
	public	byte            [] m_Rolename;
	public	byte             m_Job;	/// 职业             .
	public	int              m_Status;	/// 状态 0-初始 1-触发 2-打开 3-被杀死 4-逃跑 .
	public	ATK_INFO_LIST    m_AtkInfoList	=	new ATK_INFO_LIST();	/// 攻击者           .
	public ABYSS_SEND_REWARD_MAIL()
	{
		m_Rolename=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="TmpID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Roleid";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Rolename";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="Job";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="Status";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="AtkInfoList";miArray[5].type=MemberInfo.MIType.eMessage;miArray[5].size=0;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TmpID;
		case	1:return m_Roleid;
		case	2:return m_Rolename;
		case	3:return m_Job;
		case	4:return m_Status;
		case	5:return m_AtkInfoList;

		}
		return 0;
	}
};

public class SMailDetail	:	CMessage{
	public	byte             m_Type;	/// 邮件类型         .
	public	UInt64           m_UniqueID;	/// 邮件唯一id       .
	public	int              m_Money;	/// 金钱             .
	public	uint             m_SendTime;	/// 发送时间         .
	public	uint             m_ReadTime;	/// 阅读时间         .
	public	byte            [] m_Sender;	/// 发件人姓名       .
	public	byte            [] m_Receiver;	/// 收件人姓名       .
	public	byte            [] m_Title;	/// 标题             .
	public	byte            [] m_Content;	/// 内容             .
	public	byte             m_ItemCount;	/// 物品数量         .
	public	SXITEM          [] m_Items;
	public SMailDetail()
	{
		m_Sender=new byte[(int)ROLE_NAME_LEN];
		m_Receiver=new byte[(int)ROLE_NAME_LEN];
		m_Title=new byte[(int)MAX_MAIL_TITLE_LEN];
		m_Content=new byte[(int)MAX_MAIL_CONTENT_LEN];
		m_Items=new SXITEM[(int)MAX_MAIL_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[11];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="UniqueID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Money";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="SendTime";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="ReadTime";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Sender";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)ROLE_NAME_LEN;
		miArray[6].name="Receiver";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)ROLE_NAME_LEN;
		miArray[7].name="Title";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)MAX_MAIL_TITLE_LEN;
		miArray[8].name="Content";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)MAX_MAIL_CONTENT_LEN;
		miArray[9].name="ItemCount";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="Items";miArray[10].type=MemberInfo.MIType.eMessage;miArray[10].size=0;miArray[10].count=(int)MAX_MAIL_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_UniqueID;
		case	2:return m_Money;
		case	3:return m_SendTime;
		case	4:return m_ReadTime;
		case	5:return m_Sender;
		case	6:return m_Receiver;
		case	7:return m_Title;
		case	8:return m_Content;
		case	9:return m_ItemCount;
		case	10:return m_Items;

		}
		return 0;
	}
};

public class MAIL_LIST	:	CMessage{
	public	ushort           m_Count;	/// 邮件数量         .
	public	SMailDetail     [] m_Mails;
	public MAIL_LIST()
	{
		m_Mails=new SMailDetail[(int)MAX_MAIL_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="Mails";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_MAIL_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Mails;

		}
		return 0;
	}
};

public class SEND_MAIL	:	CMessage{
	public	int              m_Money;	/// 金钱             .
	public	byte            [] m_Receiver;	/// 收件人姓名       .
	public	byte            [] m_Title;	/// 标题             .
	public	byte            [] m_Content;	/// 内容             .
	public	byte             m_ItemCount;	/// 物品数量         .
	public	UInt64          [] m_Items;
	public SEND_MAIL()
	{
		m_Receiver=new byte[(int)ROLE_NAME_LEN];
		m_Title=new byte[(int)MAX_MAIL_TITLE_LEN];
		m_Content=new byte[(int)MAX_MAIL_CONTENT_LEN];
		m_Items=new UInt64[(int)MAX_MAIL_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="Money";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Receiver";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="Title";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)MAX_MAIL_TITLE_LEN;
		miArray[3].name="Content";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)MAX_MAIL_CONTENT_LEN;
		miArray[4].name="ItemCount";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="Items";miArray[5].type=MemberInfo.MIType.eUInt64;miArray[5].size=8;miArray[5].count=(int)MAX_MAIL_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Money;
		case	1:return m_Receiver;
		case	2:return m_Title;
		case	3:return m_Content;
		case	4:return m_ItemCount;
		case	5:return m_Items;

		}
		return 0;
	}
};

public class MAIL_ID_LIST	:	CMessage{
	public	ushort           m_Count;
	public	UInt64          [] m_MailIDs;
	public MAIL_ID_LIST()
	{
		m_MailIDs=new UInt64[(int)MAX_MAIL_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="MailIDs";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)MAX_MAIL_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_MailIDs;

		}
		return 0;
	}
};

public class SPVPRoleAllData	:	CMessage{
	public	UInt64           m_Roleid;	/// 角色id           .
	public	UInt64           m_Digitid;	/// 账号数字id       .
	public	byte            [] m_RoleName;	/// 角色名字         .
	public	int              m_RoleLevel;	/// 角色等级         .
	public	int              m_MiLevel;	/// 军阶等级         .
	public	int              m_Repute;	/// 声望             .
	public	int              m_Points;	/// 积分             .
	public	int              m_Ranks;	/// 排名             .
	public	byte             m_BaseJob;	/// 基础门派         .
	public	byte             m_Sex;	/// 对象性别         .
	public	byte             m_HairStyle;	/// 发型             .
	public	byte             m_HairColor;	/// 颜色             .
	public	int              m_Attack;	/// 战斗力           .
	public SPVPRoleAllData()
	{
		m_RoleName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[13];
		miArray[0].name="Roleid";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Digitid";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="RoleName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="RoleLevel";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="MiLevel";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Repute";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Points";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="Ranks";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="BaseJob";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;
		miArray[9].name="Sex";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="HairStyle";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="HairColor";miArray[11].type=MemberInfo.MIType.eByte;miArray[11].size=1;miArray[11].count=(int)0;
		miArray[12].name="Attack";miArray[12].type=MemberInfo.MIType.eInt;miArray[12].size=4;miArray[12].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Roleid;
		case	1:return m_Digitid;
		case	2:return m_RoleName;
		case	3:return m_RoleLevel;
		case	4:return m_MiLevel;
		case	5:return m_Repute;
		case	6:return m_Points;
		case	7:return m_Ranks;
		case	8:return m_BaseJob;
		case	9:return m_Sex;
		case	10:return m_HairStyle;
		case	11:return m_HairColor;
		case	12:return m_Attack;

		}
		return 0;
	}
};

public class PVP_MATCH	:	CMessage{
	public	UInt64           m_Roleid;	/// 角色id           .
	public	byte            [] m_RoleName;	/// 角色名字         .
	public	int              m_RoleLevel;	/// 角色等级         .
	public	int              m_MilitaryLevel;	/// 军阶等级         .
	public	int              m_Points;	/// 角色当前积分     .
	public	byte             m_BaseJob;	/// 基础门派         .
	public	byte             m_Sex;	/// 对象性别         .
	public	byte             m_HairStyle;	/// 发型             .
	public	byte             m_HairColor;	/// 颜色             .
	public PVP_MATCH()
	{
		m_RoleName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[9];
		miArray[0].name="Roleid";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RoleName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="RoleLevel";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="MilitaryLevel";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Points";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="BaseJob";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="Sex";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="HairStyle";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="HairColor";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Roleid;
		case	1:return m_RoleName;
		case	2:return m_RoleLevel;
		case	3:return m_MilitaryLevel;
		case	4:return m_Points;
		case	5:return m_BaseJob;
		case	6:return m_Sex;
		case	7:return m_HairStyle;
		case	8:return m_HairColor;

		}
		return 0;
	}
};

/* 获取pvp对手回应  */
public class PVP_MATCH_LIST	:	CMessage{
	public	ushort           m_Count;
	public	PVP_MATCH       [] m_PVPMatchList;
	public PVP_MATCH_LIST()
	{
		m_PVPMatchList=new PVP_MATCH[(int)MAX_PVP_MATCH_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="PVPMatchList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_PVP_MATCH_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_PVPMatchList;

		}
		return 0;
	}
};

public class PVP_RANK	:	CMessage{
	public	UInt64           m_Roleid;	/// 角色id           .
	public	byte            [] m_RoleName;	/// 角色名字         .
	public	int              m_RoleLevel;	/// 角色等级         .
	public	int              m_MilitaryLevel;	/// 军阶等级         .
	public	int              m_Points;	/// 角色当前积分     .
	public	int              m_Rank;	/// 角色排名         .
	public	byte             m_BaseJob;	/// 基础门派         .
	public	byte             m_Sex;	/// 对象性别         .
	public	byte             m_HairStyle;	/// 发型             .
	public	byte             m_HairColor;	/// 颜色             .
	public PVP_RANK()
	{
		m_RoleName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[10];
		miArray[0].name="Roleid";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RoleName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="RoleLevel";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="MilitaryLevel";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Points";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Rank";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="BaseJob";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="Sex";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="HairStyle";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;
		miArray[9].name="HairColor";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Roleid;
		case	1:return m_RoleName;
		case	2:return m_RoleLevel;
		case	3:return m_MilitaryLevel;
		case	4:return m_Points;
		case	5:return m_Rank;
		case	6:return m_BaseJob;
		case	7:return m_Sex;
		case	8:return m_HairStyle;
		case	9:return m_HairColor;

		}
		return 0;
	}
};

/* 获取pvp排行榜  */
public class PVP_RANK_LIST	:	CMessage{
	public	ushort           m_Count;
	public	PVP_RANK        [] m_PVPRankList;
	public PVP_RANK_LIST()
	{
		m_PVPRankList=new PVP_RANK[(int)MAX_PVP_RANK_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="PVPRankList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_PVP_RANK_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_PVPRankList;

		}
		return 0;
	}
};

public class SRobotPet	:	CMessage{
	public	int              m_TemplateId;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateId;

		}
		return 0;
	}
};

public class SRobotPets	:	CMessage{
	public	ushort           m_PetCount;	/// 拥有宠物个数     .
	public	SRobotPet       [] m_PetsInfo;
	public	int              m_PetLevel;
	public	int              m_PetUp;
	public SRobotPets()
	{
		m_PetsInfo=new SRobotPet[(int)EPetBattlePos.PET_BTPOS_TEAM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="PetCount";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="PetsInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)EPetBattlePos.PET_BTPOS_TEAM;
		miArray[2].name="PetLevel";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="PetUp";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PetCount;
		case	1:return m_PetsInfo;
		case	2:return m_PetLevel;
		case	3:return m_PetUp;

		}
		return 0;
	}
};

public class SRobosEquip	:	CMessage{
	public	int              m_TemplateId;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateId;

		}
		return 0;
	}
};

public class SRobosEquips	:	CMessage{
	public	byte             m_EquipCount;
	public	SRobosEquip     [] m_EquipInfo;
	public SRobosEquips()
	{
		m_EquipInfo=new SRobosEquip[(int)MAX_EQUIP_ITEM_NUM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="EquipCount";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="EquipInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_EQUIP_ITEM_NUM;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EquipCount;
		case	1:return m_EquipInfo;

		}
		return 0;
	}
};

public class SRobotBuff	:	CMessage{
	public	int              m_TemplateId;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateId;

		}
		return 0;
	}
};

public class SRobotBuffs	:	CMessage{
	public	byte             m_BuffCount;
	public	SRobotBuff      [] m_BuffInfo;
	public SRobotBuffs()
	{
		m_BuffInfo=new SRobotBuff[(int)MAX_PVP_ROBOT_BUFF_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="BuffCount";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="BuffInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_PVP_ROBOT_BUFF_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuffCount;
		case	1:return m_BuffInfo;

		}
		return 0;
	}
};

public class SPVPRobot	:	CMessage{
	public	UInt64           m_UID;
	public	UInt64           m_DGID;
	public	byte            [] m_Name;
	public	int              m_Level;
	public	byte             m_Job;
	public	int              m_Quality;
	public	int              m_Repute;
	public	int              m_Militery;
	public	int              m_Points;
	public	byte             m_Sex;
	public	byte             m_HairStyle;	/// 发型             .
	public	byte             m_HairColor;	/// 颜色             .
	public	SRobosEquips     m_Equips	=	new SRobosEquips();
	public	SRobotPets       m_Pets	=	new SRobotPets();
	public	SRobotBuffs      m_Buffs	=	new SRobotBuffs();
	public SPVPRobot()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[15];
		miArray[0].name="UID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="DGID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Name";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="Level";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Job";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;
		miArray[5].name="Quality";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Repute";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="Militery";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="Points";miArray[8].type=MemberInfo.MIType.eInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="Sex";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="HairStyle";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="HairColor";miArray[11].type=MemberInfo.MIType.eByte;miArray[11].size=1;miArray[11].count=(int)0;
		miArray[12].name="Equips";miArray[12].type=MemberInfo.MIType.eMessage;miArray[12].size=0;miArray[12].count=(int)0;
		miArray[13].name="Pets";miArray[13].type=MemberInfo.MIType.eMessage;miArray[13].size=0;miArray[13].count=(int)0;
		miArray[14].name="Buffs";miArray[14].type=MemberInfo.MIType.eMessage;miArray[14].size=0;miArray[14].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UID;
		case	1:return m_DGID;
		case	2:return m_Name;
		case	3:return m_Level;
		case	4:return m_Job;
		case	5:return m_Quality;
		case	6:return m_Repute;
		case	7:return m_Militery;
		case	8:return m_Points;
		case	9:return m_Sex;
		case	10:return m_HairStyle;
		case	11:return m_HairColor;
		case	12:return m_Equips;
		case	13:return m_Pets;
		case	14:return m_Buffs;

		}
		return 0;
	}
};

public class SSeverPVPBattleInfo	:	CMessage{
	public	SPVPRoleAllData  m_PVPProperty	=	new SPVPRoleAllData();	/// PVP信息          .
	public	byte            [] m_RoleAllInfo;	/// 人物所有信息，包括装备、宠物、技能、属性等 .
	public	SPVPRobot        m_PVPRobot	=	new SPVPRobot();	/// 机器人           .
	public SSeverPVPBattleInfo()
	{
		m_RoleAllInfo=new byte[(int)MAX_PVP_ROLE_INFO_STRING_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="PVPProperty";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="RoleAllInfo";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_PVP_ROLE_INFO_STRING_COUNT;
		miArray[2].name="PVPRobot";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PVPProperty;
		case	1:return m_RoleAllInfo;
		case	2:return m_PVPRobot;

		}
		return 0;
	}
};

public class WBAtkInfo	:	CMessage{
	public	UInt64           m_Id;
	public	byte            [] m_Nm;
	public	int              m_Dmg;
	public	int              m_Buf;
	public	int              m_BufId;
	public	int              m_Rank;
	public WBAtkInfo()
	{
		m_Nm=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="Id";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Nm";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="Dmg";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Buf";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="BufId";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Rank";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Id;
		case	1:return m_Nm;
		case	2:return m_Dmg;
		case	3:return m_Buf;
		case	4:return m_BufId;
		case	5:return m_Rank;

		}
		return 0;
	}
};

public class WBBossBuff	:	CMessage{
	public	int              m_Percent;
	public	int              m_BuffID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Percent";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BuffID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Percent;
		case	1:return m_BuffID;

		}
		return 0;
	}
};

public class SWorldBossInfo	:	CMessage{
	public	UInt64           m_WBDBID;	/// 世界boss的dbid   .
	public	uint             m_WBTmpId;	/// 世界boss配置模板的index .
	public	int              m_TotalNum;	/// 参与总人数       .
	public	int              m_Blood;	/// 怪物血量         .
	public	WBBossBuff       m_BossBuff	=	new WBBossBuff();	/// 怪物属性加成     .
	public	int              m_Atkcount;	/// 攻击者数量       .
	public	WBAtkInfo       [] m_Atklist;	/// 攻击角色列表     .
	public	WBAtkInfo        m_AtkInfo	=	new WBAtkInfo();	/// 个人攻击信息     .
	public	int              m_Status;	/// 状态 0-初始 1-准备 2-打开 3-结算 4-杀死 5-逃跑 .
	public	UInt64           m_StartTime;	/// 打开时间         .
	public	UInt64           m_EndTime;	/// 结束时间         .
	public	UInt64           m_Killtime;	/// 杀死时间         .
	public	UInt64           m_ReliveTime;	/// 死亡冷却时间     .
	public	UInt64           m_NextTime;	/// 下一场开始的标准时间 .
	public	int              m_Time;	/// 角色还可以打世界boss的次数 .
	public SWorldBossInfo()
	{
		m_Atklist=new WBAtkInfo[(int)MAX_WB_RANK_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[15];
		miArray[0].name="WBDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="WBTmpId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="TotalNum";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Blood";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="BossBuff";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)0;
		miArray[5].name="Atkcount";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Atklist";miArray[6].type=MemberInfo.MIType.eMessage;miArray[6].size=0;miArray[6].count=(int)MAX_WB_RANK_COUNT;
		miArray[7].name="AtkInfo";miArray[7].type=MemberInfo.MIType.eMessage;miArray[7].size=0;miArray[7].count=(int)0;
		miArray[8].name="Status";miArray[8].type=MemberInfo.MIType.eInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="StartTime";miArray[9].type=MemberInfo.MIType.eUInt64;miArray[9].size=8;miArray[9].count=(int)0;
		miArray[10].name="EndTime";miArray[10].type=MemberInfo.MIType.eUInt64;miArray[10].size=8;miArray[10].count=(int)0;
		miArray[11].name="Killtime";miArray[11].type=MemberInfo.MIType.eUInt64;miArray[11].size=8;miArray[11].count=(int)0;
		miArray[12].name="ReliveTime";miArray[12].type=MemberInfo.MIType.eUInt64;miArray[12].size=8;miArray[12].count=(int)0;
		miArray[13].name="NextTime";miArray[13].type=MemberInfo.MIType.eUInt64;miArray[13].size=8;miArray[13].count=(int)0;
		miArray[14].name="Time";miArray[14].type=MemberInfo.MIType.eInt;miArray[14].size=4;miArray[14].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_WBDBID;
		case	1:return m_WBTmpId;
		case	2:return m_TotalNum;
		case	3:return m_Blood;
		case	4:return m_BossBuff;
		case	5:return m_Atkcount;
		case	6:return m_Atklist;
		case	7:return m_AtkInfo;
		case	8:return m_Status;
		case	9:return m_StartTime;
		case	10:return m_EndTime;
		case	11:return m_Killtime;
		case	12:return m_ReliveTime;
		case	13:return m_NextTime;
		case	14:return m_Time;

		}
		return 0;
	}
};

public class SRankRoleReq	:	CMessage{
	public	UInt64           m_RoleDBID;
	public	int              m_RankType;	/// 参见ERankType    .
	public	int              m_Page;	/// 第一页=0 依此类推 每页最多MAX_RANK_PAGE_COUNT .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="RoleDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RankType";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Page";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleDBID;
		case	1:return m_RankType;
		case	2:return m_Page;

		}
		return 0;
	}
};

public class SRankRole	:	CMessage{
	public	SRoleRsServerInfo m_RoleInfo	=	new SRoleRsServerInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleInfo;

		}
		return 0;
	}
};

public class SRankRoleAck	:	CMessage{
	public	SRankRoleReq     m_Req	=	new SRankRoleReq();
	public	int              m_TotalCount;	/// 总的人数         .
	public	int              m_SelfRank;	/// 自己的排名=-1代表不在MAX_RANK_ROLE_COUNT内 .
	public	int              m_RankCount;	/// 指定Page有多少名玩家 .
	public	SRankRole       [] m_Rank;
	public SRankRoleAck()
	{
		m_Rank=new SRankRole[(int)MAX_RANK_PAGE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="TotalCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="SelfRank";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="RankCount";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Rank";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)MAX_RANK_PAGE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;
		case	1:return m_TotalCount;
		case	2:return m_SelfRank;
		case	3:return m_RankCount;
		case	4:return m_Rank;

		}
		return 0;
	}
};

/* 公会列表请求，非入会玩家  */
public class SGuildListReq	:	CMessage{
	public	uint             m_Page;	/// 第几页           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Page";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Page;

		}
		return 0;
	}
};

/* 公会列表信息  */
public class SGuildListInfo	:	CMessage{
	public	int              m_GuildId;	/// 公会ID           .
	public	byte            [] m_GuildName;
	public	int              m_GuildLv;	/// 公会等级         .
	public	byte            [] m_PresidentName;
	public	int              m_MemberCount;	/// 公会人数         .
	public	int              m_MemberMax;	/// 公会最大人数     .
	public SGuildListInfo()
	{
		m_GuildName=new byte[(int)MAX_GUILD_NAME_LEN];
		m_PresidentName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="GuildId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GuildName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_GUILD_NAME_LEN;
		miArray[2].name="GuildLv";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="PresidentName";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)ROLE_NAME_LEN;
		miArray[4].name="MemberCount";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="MemberMax";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GuildId;
		case	1:return m_GuildName;
		case	2:return m_GuildLv;
		case	3:return m_PresidentName;
		case	4:return m_MemberCount;
		case	5:return m_MemberMax;

		}
		return 0;
	}
};

/* 公会列表回应，非入会玩家  */
public class SGuildListAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildListResult .
	public	byte             m_Count;	/// 信息数量         .
	public	SGuildListInfo  [] m_Info;	/// 公会信息         .
	public SGuildListAck()
	{
		m_Info=new SGuildListInfo[(int)MAX_GUILD_LIST_PER_PAGE];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="Info";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)MAX_GUILD_LIST_PER_PAGE;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Count;
		case	2:return m_Info;

		}
		return 0;
	}
};

/* 公会入会申请  */
public class SGuildEnterReq	:	CMessage{
	public	int              m_GuildId;	/// 公会ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="GuildId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GuildId;

		}
		return 0;
	}
};

/* 公会入会申请返回  */
public class SGuildEnterAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildEnterResult .
	public	int              m_GuildId;	/// 公会ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="GuildId";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_GuildId;

		}
		return 0;
	}
};

/* 离开公会返回  */
public class SGuildLeaveAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildLeaveResult .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;

		}
		return 0;
	}
};

/* 创建公会  */
public class SGuildCreateReq	:	CMessage{
	public	byte            [] m_GuildName;
	public	byte            [] m_GuildAim;
	public	byte             m_CostType;	/// 花费类型(1金币2徽章) .
	public SGuildCreateReq()
	{
		m_GuildName=new byte[(int)MAX_GUILD_NAME_LEN];
		m_GuildAim=new byte[(int)MAX_GUILD_AIM_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="GuildName";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_GUILD_NAME_LEN;
		miArray[1].name="GuildAim";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_GUILD_AIM_LEN;
		miArray[2].name="CostType";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GuildName;
		case	1:return m_GuildAim;
		case	2:return m_CostType;

		}
		return 0;
	}
};

/* 创建公会结果  */
public class SGuildCreateAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildCreateResult .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;

		}
		return 0;
	}
};

/* 公会基本信息  */
public class SGuildBaseInfo	:	CMessage{
	public	int              m_GuildId;	/// 公会ID           .
	public	byte            [] m_GuildName;
	public	byte            [] m_GuildNotice;
	public	int              m_MemberCount;	/// 公会人数         .
	public	int              m_MemberMax;	/// 公会最大人数     .
	public	int              m_MyContribution;	/// 我的贡献         .
	public	int              m_MyContributionMax;	/// 我的贡献最大值   .
	public SGuildBaseInfo()
	{
		m_GuildName=new byte[(int)MAX_GUILD_NAME_LEN];
		m_GuildNotice=new byte[(int)MAX_GUILD_NOTICE_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[7];
		miArray[0].name="GuildId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GuildName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_GUILD_NAME_LEN;
		miArray[2].name="GuildNotice";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)MAX_GUILD_NOTICE_LEN;
		miArray[3].name="MemberCount";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="MemberMax";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="MyContribution";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="MyContributionMax";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GuildId;
		case	1:return m_GuildName;
		case	2:return m_GuildNotice;
		case	3:return m_MemberCount;
		case	4:return m_MemberMax;
		case	5:return m_MyContribution;
		case	6:return m_MyContributionMax;

		}
		return 0;
	}
};

/* 公会LOG.成员加入  */
public class SGuildLog_MemberEnter	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_MemberName;
	public SGuildLog_MemberEnter()
	{
		m_MemberName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="MemberName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_MemberName;

		}
		return 0;
	}
};

/* 公会LOG.成员踢出  */
public class SGuildLog_MemberKick	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_MemberName;
	public	byte            [] m_KickName;
	public SGuildLog_MemberKick()
	{
		m_MemberName=new byte[(int)ROLE_NAME_LEN];
		m_KickName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="MemberName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="KickName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_MemberName;
		case	2:return m_KickName;

		}
		return 0;
	}
};

/* 公会LOG.职位调整  */
public class SGuildLog_PotitionChange	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_MemberName;
	public	byte            [] m_PositionChangeName;
	public	byte             m_NewPosition;	/// 调整后职位 参考EGuildPosition .
	public SGuildLog_PotitionChange()
	{
		m_MemberName=new byte[(int)ROLE_NAME_LEN];
		m_PositionChangeName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="MemberName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="PositionChangeName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="NewPosition";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_MemberName;
		case	2:return m_PositionChangeName;
		case	3:return m_NewPosition;

		}
		return 0;
	}
};

/* 公会LOG.会长转让  */
public class SGuildLog_PresidentChange	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_FromName;
	public	byte            [] m_ToName;
	public SGuildLog_PresidentChange()
	{
		m_FromName=new byte[(int)ROLE_NAME_LEN];
		m_ToName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="FromName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="ToName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_FromName;
		case	2:return m_ToName;

		}
		return 0;
	}
};

/* 公会LOG.会长弹劾  */
public class SGuildLog_PresidentImpeach	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_FromName;
	public	byte            [] m_ToName;
	public SGuildLog_PresidentImpeach()
	{
		m_FromName=new byte[(int)ROLE_NAME_LEN];
		m_ToName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="FromName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;
		miArray[2].name="ToName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_FromName;
		case	2:return m_ToName;

		}
		return 0;
	}
};

/* 公会LOG.公会升级  */
public class SGuildLog_LevelUp	:	CMessage{
	public	uint             m_Time;
	public	int              m_FromLevel;
	public	int              m_ToLevel;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="FromLevel";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="ToLevel";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_FromLevel;
		case	2:return m_ToLevel;

		}
		return 0;
	}
};

/* 公会LOG.公会科技升级  */
public class SGuildLog_TechnologyUp	:	CMessage{
	public	uint             m_Time;
	public	byte             m_TechnologyType;	/// 科技类型，目前包括经验提升、商品开发 .
	public	byte            [] m_OpName;
	public	int              m_FromLevel;
	public	int              m_ToLevel;
	public SGuildLog_TechnologyUp()
	{
		m_OpName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="TechnologyType";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="OpName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="FromLevel";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="ToLevel";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_TechnologyType;
		case	2:return m_OpName;
		case	3:return m_FromLevel;
		case	4:return m_ToLevel;

		}
		return 0;
	}
};

/* 公会LOG.公会祈福  */
public class SGuildLog_Pray	:	CMessage{
	public	uint             m_Time;
	public	byte             m_PrayType;	/// 祈福类型，1普通2高级3神圣 .
	public	byte            [] m_OpName;
	public	int              m_Money;
	public SGuildLog_Pray()
	{
		m_OpName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="PrayType";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="OpName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)ROLE_NAME_LEN;
		miArray[3].name="Money";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_PrayType;
		case	2:return m_OpName;
		case	3:return m_Money;

		}
		return 0;
	}
};

/* 公会LOG.公告修改  */
public class SGuildLog_NoticeChange	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_OpName;
	public SGuildLog_NoticeChange()
	{
		m_OpName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="OpName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_OpName;

		}
		return 0;
	}
};

/* 公会LOG.宗旨修改  */
public class SGuildLog_AimChange	:	CMessage{
	public	uint             m_Time;
	public	byte            [] m_OpName;
	public SGuildLog_AimChange()
	{
		m_OpName=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="OpName";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_OpName;

		}
		return 0;
	}
};

/* 公会日志结果  */
public class SGuildLogAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildLogResult .
	public	byte             m_Count11;	/// 信息数量         .
	public	SGuildLog_MemberEnter[] m_MemberEnter;	/// 成员加入         .
	public	byte             m_Count12;	/// 信息数量         .
	public	SGuildLog_MemberKick[] m_MemberKick;	/// 成员踢出         .
	public	byte             m_Count13;	/// 信息数量         .
	public	SGuildLog_PotitionChange[] m_PotitionChange;	/// 职位调整         .
	public	byte             m_Count14;	/// 信息数量         .
	public	SGuildLog_PresidentChange[] m_PresidentChange;	/// 会长转让         .
	public	byte             m_Count15;	/// 信息数量         .
	public	SGuildLog_PresidentImpeach[] m_PresidentImpeach;	/// 会长弹劾         .
	public	byte             m_Count20;	/// 信息数量         .
	public	SGuildLog_LevelUp[] m_LevelUp;	/// 公会升级         .
	public	byte             m_Count30;	/// 信息数量         .
	public	SGuildLog_TechnologyUp[] m_TechnologyUp;	/// 公会科技升级     .
	public	byte             m_Count40;	/// 信息数量         .
	public	SGuildLog_Pray  [] m_Pray;	/// 公会祈福         .
	public	byte             m_Count50;	/// 信息数量         .
	public	SGuildLog_NoticeChange[] m_NoticeChange;	/// 公告修改         .
	public	byte             m_Count60;	/// 信息数量         .
	public	SGuildLog_AimChange[] m_AimChange;	/// 宗旨修改         .
	public SGuildLogAck()
	{
		m_MemberEnter=new SGuildLog_MemberEnter[(int)MAX_GUILD_LOG_COUNT];
		m_MemberKick=new SGuildLog_MemberKick[(int)MAX_GUILD_LOG_COUNT];
		m_PotitionChange=new SGuildLog_PotitionChange[(int)MAX_GUILD_LOG_COUNT];
		m_PresidentChange=new SGuildLog_PresidentChange[(int)MAX_GUILD_LOG_COUNT];
		m_PresidentImpeach=new SGuildLog_PresidentImpeach[(int)MAX_GUILD_LOG_COUNT];
		m_LevelUp=new SGuildLog_LevelUp[(int)MAX_GUILD_LOG_COUNT];
		m_TechnologyUp=new SGuildLog_TechnologyUp[(int)MAX_GUILD_LOG_COUNT];
		m_Pray=new SGuildLog_Pray[(int)MAX_GUILD_LOG_COUNT];
		m_NoticeChange=new SGuildLog_NoticeChange[(int)MAX_GUILD_LOG_COUNT];
		m_AimChange=new SGuildLog_AimChange[(int)MAX_GUILD_LOG_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[21];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Count11";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="MemberEnter";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[3].name="Count12";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="MemberKick";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[5].name="Count13";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="PotitionChange";miArray[6].type=MemberInfo.MIType.eMessage;miArray[6].size=0;miArray[6].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[7].name="Count14";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="PresidentChange";miArray[8].type=MemberInfo.MIType.eMessage;miArray[8].size=0;miArray[8].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[9].name="Count15";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)0;
		miArray[10].name="PresidentImpeach";miArray[10].type=MemberInfo.MIType.eMessage;miArray[10].size=0;miArray[10].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[11].name="Count20";miArray[11].type=MemberInfo.MIType.eByte;miArray[11].size=1;miArray[11].count=(int)0;
		miArray[12].name="LevelUp";miArray[12].type=MemberInfo.MIType.eMessage;miArray[12].size=0;miArray[12].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[13].name="Count30";miArray[13].type=MemberInfo.MIType.eByte;miArray[13].size=1;miArray[13].count=(int)0;
		miArray[14].name="TechnologyUp";miArray[14].type=MemberInfo.MIType.eMessage;miArray[14].size=0;miArray[14].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[15].name="Count40";miArray[15].type=MemberInfo.MIType.eByte;miArray[15].size=1;miArray[15].count=(int)0;
		miArray[16].name="Pray";miArray[16].type=MemberInfo.MIType.eMessage;miArray[16].size=0;miArray[16].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[17].name="Count50";miArray[17].type=MemberInfo.MIType.eByte;miArray[17].size=1;miArray[17].count=(int)0;
		miArray[18].name="NoticeChange";miArray[18].type=MemberInfo.MIType.eMessage;miArray[18].size=0;miArray[18].count=(int)MAX_GUILD_LOG_COUNT;
		miArray[19].name="Count60";miArray[19].type=MemberInfo.MIType.eByte;miArray[19].size=1;miArray[19].count=(int)0;
		miArray[20].name="AimChange";miArray[20].type=MemberInfo.MIType.eMessage;miArray[20].size=0;miArray[20].count=(int)MAX_GUILD_LOG_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Count11;
		case	2:return m_MemberEnter;
		case	3:return m_Count12;
		case	4:return m_MemberKick;
		case	5:return m_Count13;
		case	6:return m_PotitionChange;
		case	7:return m_Count14;
		case	8:return m_PresidentChange;
		case	9:return m_Count15;
		case	10:return m_PresidentImpeach;
		case	11:return m_Count20;
		case	12:return m_LevelUp;
		case	13:return m_Count30;
		case	14:return m_TechnologyUp;
		case	15:return m_Count40;
		case	16:return m_Pray;
		case	17:return m_Count50;
		case	18:return m_NoticeChange;
		case	19:return m_Count60;
		case	20:return m_AimChange;

		}
		return 0;
	}
};

/* 公会修改公告请求  */
public class SGuildChangeNoticeReq	:	CMessage{
	public	byte            [] m_NewNotice;
	public SGuildChangeNoticeReq()
	{
		m_NewNotice=new byte[(int)MAX_GUILD_NOTICE_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="NewNotice";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_GUILD_NOTICE_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_NewNotice;

		}
		return 0;
	}
};

/* 公会修改公告结果  */
public class SGuildChangeNoticeAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildChangeNoticeResult .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;

		}
		return 0;
	}
};

/* 公会成员列表信息  */
public class SGuildMemberListInfo	:	CMessage{
	public	byte            [] m_Name;
	public	int              m_Level;	/// 等级             .
	public	int              m_Attack;	/// 战力             .
	public	byte             m_Position;	/// 职位 参考EGuildPosition .
	public	int              m_Contribution;	/// 贡献             .
	public	UInt64           m_OfflineTime;	/// 离线时间         .
	public SGuildMemberListInfo()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="Name";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)ROLE_NAME_LEN;
		miArray[1].name="Level";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Attack";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Position";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="Contribution";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="OfflineTime";miArray[5].type=MemberInfo.MIType.eUInt64;miArray[5].size=8;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Name;
		case	1:return m_Level;
		case	2:return m_Attack;
		case	3:return m_Position;
		case	4:return m_Contribution;
		case	5:return m_OfflineTime;

		}
		return 0;
	}
};

/* 公会详细信息返回  */
public class SGuildDetailAck	:	CMessage{
	public	byte            [] m_GuildName;
	public	int              m_GuildId;	/// 公会ID           .
	public	int              m_GuildLevel;	/// 公会等级         .
	public	int              m_MemberCount;	/// 公会人数         .
	public	int              m_MemberMax;	/// 公会最大人数     .
	public	int              m_GuildAttack;	/// 公会总战力       .
	public	int              m_Attack;	/// 公会活跃度       .
	public	int              m_Reputation;
	public	int              m_Money;
	public	byte            [] m_GuildAim;
	public	byte             m_Count;	/// 信息数量         .
	public	SGuildMemberListInfo[] m_MemberList;	/// 成员列表         .
	public SGuildDetailAck()
	{
		m_GuildName=new byte[(int)MAX_GUILD_NAME_LEN];
		m_GuildAim=new byte[(int)MAX_GUILD_AIM_LEN];
		m_MemberList=new SGuildMemberListInfo[(int)MAX_GUILD_MEMBER_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[12];
		miArray[0].name="GuildName";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_GUILD_NAME_LEN;
		miArray[1].name="GuildId";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="GuildLevel";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="MemberCount";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="MemberMax";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="GuildAttack";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Attack";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="Reputation";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="Money";miArray[8].type=MemberInfo.MIType.eInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="GuildAim";miArray[9].type=MemberInfo.MIType.eByte;miArray[9].size=1;miArray[9].count=(int)MAX_GUILD_AIM_LEN;
		miArray[10].name="Count";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="MemberList";miArray[11].type=MemberInfo.MIType.eMessage;miArray[11].size=0;miArray[11].count=(int)MAX_GUILD_MEMBER_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GuildName;
		case	1:return m_GuildId;
		case	2:return m_GuildLevel;
		case	3:return m_MemberCount;
		case	4:return m_MemberMax;
		case	5:return m_GuildAttack;
		case	6:return m_Attack;
		case	7:return m_Reputation;
		case	8:return m_Money;
		case	9:return m_GuildAim;
		case	10:return m_Count;
		case	11:return m_MemberList;

		}
		return 0;
	}
};

/* 公会申请列表信息  */
public class SGuildEnterInfo	:	CMessage{
	public	byte            [] m_Name;
	public	int              m_Level;	/// 等级             .
	public	int              m_Attack;	/// 战力             .
	public	byte             m_BaseJob;	/// 基础门派         .
	public	byte             m_Job;	/// 当前门派         .
	public SGuildEnterInfo()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Name";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)ROLE_NAME_LEN;
		miArray[1].name="Level";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Attack";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="BaseJob";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;
		miArray[4].name="Job";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Name;
		case	1:return m_Level;
		case	2:return m_Attack;
		case	3:return m_BaseJob;
		case	4:return m_Job;

		}
		return 0;
	}
};

/* 公会申请列表返回  */
public class SGuildEnterListAck	:	CMessage{
	public	byte             m_Count;	/// 信息数量         .
	public	SGuildEnterInfo [] m_EnterList;	/// 申请列表         .
	public SGuildEnterListAck()
	{
		m_EnterList=new SGuildEnterInfo[(int)MAX_GUILD_ENTER_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="EnterList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_GUILD_ENTER_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_EnterList;

		}
		return 0;
	}
};

/* 公会申请.同意/拒绝  */
public class SGuildEnterAcceptReq	:	CMessage{
	public	byte             m_Accept;	/// 1同意2拒绝 其他无意义 .
	public	byte            [] m_Name;
	public SGuildEnterAcceptReq()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Accept";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Name";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Accept;
		case	1:return m_Name;

		}
		return 0;
	}
};

/* 公会申请.同意/拒绝 返回  */
public class SGuildEnterAcceptAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildEnterAcceptResult .
	public	byte            [] m_Name;
	public SGuildEnterAcceptAck()
	{
		m_Name=new byte[(int)ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Name";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)ROLE_NAME_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Name;

		}
		return 0;
	}
};

/* 公会申请.清空 返回  */
public class SGuildEnterClearAck	:	CMessage{
	public	byte             m_Result;	/// 操作结果 参考EGuildEnterAcceptResult .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;

		}
		return 0;
	}
};

/*Define Structs and Unions        End*/


public	static	void	AddEncode_Decode(){
}
public static	int		EncodeSError(CMessage msg,ref CNetData poNetData)
{
	SError pstIn = (SError)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Error))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSError(ref CNetData poNetData)
{
	SError pstOut = new SError();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Error))
		return null;

	return pstOut;
}

public static	int		EncodeSPosInfo(CMessage msg,ref CNetData poNetData)
{
	SPosInfo pstIn = (SPosInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_X))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Y))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Z))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Orientation))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSPosInfo(ref CNetData poNetData)
{
	SPosInfo pstOut = new SPosInfo();

	if(-1 == poNetData.Del(ref pstOut.m_X))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Y))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Z))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Orientation))
		return null;

	return pstOut;
}

public static	int		EncodeSRoleDBInfo(CMessage msg,ref CNetData poNetData)
{
	SRoleDBInfo pstIn = (SRoleDBInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_DBIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_VipEP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Experience))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonMoney))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonCash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LatestWorldChatTime))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_XData))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_XData2))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_XData3))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ActivityInfo))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Shop))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ClentCfg))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleDBInfo(ref CNetData poNetData)
{
	SRoleDBInfo pstOut = new SRoleDBInfo();

	if(-1 == poNetData.Del(ref pstOut.m_DBIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_VipEP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Experience))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonMoney))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonCash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LatestWorldChatTime))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_XData))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_XData2))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_XData3))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ActivityInfo))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Shop))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ClentCfg))
		return null;

	return pstOut;
}

public static	int		EncodeSItemInfo(CMessage msg,ref CNetData poNetData)
{
	SItemInfo pstIn = (SItemInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Index))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Slot))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ExInfoIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MakerInfoIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TimeInfoIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Flag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PersonalFlag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ObjId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemInfo(ref CNetData poNetData)
{
	SItemInfo pstOut = new SItemInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Index))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Slot))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ExInfoIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MakerInfoIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TimeInfoIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Flag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PersonalFlag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ObjId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUId))
		return null;

	return pstOut;
}

public static	int		EncodeSRollItemData(CMessage msg,ref CNetData poNetData)
{
	SRollItemData pstIn = (SRollItemData)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Num))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BroadcastFlag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Rare))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRollItemData(ref CNetData poNetData)
{
	SRollItemData pstOut = new SRollItemData();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Num))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BroadcastFlag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Rare))
		return null;

	return pstOut;
}

public static	int		EncodeSRollData_Common(CMessage msg,ref CNetData poNetData)
{
	SRollData_Common pstIn = (SRollData_Common)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRollData_Common(ref CNetData poNetData)
{
	SRollData_Common pstOut = new SRollData_Common();

	return pstOut;
}

public static	int		EncodeSRollData_Money(CMessage msg,ref CNetData poNetData)
{
	SRollData_Money pstIn = (SRollData_Money)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRollData_Money(ref CNetData poNetData)
{
	SRollData_Money pstOut = new SRollData_Money();

	return pstOut;
}

public static	int		EncodeSRollData_Cell(CMessage msg,ref CNetData poNetData)
{
	SRollData_Cell pstIn = (SRollData_Cell)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRollData_Cell(ref CNetData poNetData)
{
	SRollData_Cell pstOut = new SRollData_Cell();

	return pstOut;
}

public static	int		EncodeSRollPoolItem(CMessage msg,ref CNetData poNetData)
{
	SRollPoolItem pstIn = (SRollPoolItem)msg;

	if(-1 == poNetData.Add(pstIn.m_Template))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Num))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRollPoolItem(ref CNetData poNetData)
{
	SRollPoolItem pstOut = new SRollPoolItem();

	if(-1 == poNetData.Del(ref pstOut.m_Template))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Num))
		return null;

	return pstOut;
}

public static	int		EncodeSBaseItemInfo(CMessage msg,ref CNetData poNetData)
{
	SBaseItemInfo pstIn = (SBaseItemInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Num))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBaseItemInfo(ref CNetData poNetData)
{
	SBaseItemInfo pstOut = new SBaseItemInfo();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Num))
		return null;

	return pstOut;
}

public static	int		EncodeSMultiItems(CMessage msg,ref CNetData poNetData)
{
	SMultiItems pstIn = (SMultiItems)msg;

	if(-1 == poNetData.Add(pstIn.m_TypeCount))
		return -1;

	int i;
	if ((int)pstIn.m_TypeCount > (int)MAX_ITEM_COUNT || (int)pstIn.m_TypeCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_TypeCount)
			break;
		EncodeSBaseItemInfo(pstIn.m_Items[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMultiItems(ref CNetData poNetData)
{
	SMultiItems pstOut = new SMultiItems();

	if(-1 == poNetData.Del(ref pstOut.m_TypeCount))
		return null;

	int i;
	if ((int)pstOut.m_TypeCount > (int)MAX_ITEM_COUNT || (int)pstOut.m_TypeCount < 0)
       return null;
	for(i = 0; i < (int)MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_TypeCount)
			break;
		pstOut.m_Items[i]	= (SBaseItemInfo)DecodeSBaseItemInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSBlob(CMessage msg,ref CNetData poNetData)
{
	SBlob pstIn = (SBlob)msg;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_BLOB_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Data,(int)pstIn.m_Len))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBlob(ref CNetData poNetData)
{
	SBlob pstOut = new SBlob();

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_BLOB_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Data,(int)pstOut.m_Len))
		return null;

	return pstOut;
}

public static	int		EncodeSBigBlob(CMessage msg,ref CNetData poNetData)
{
	SBigBlob pstIn = (SBigBlob)msg;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_BIGBLOB_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Data,(int)pstIn.m_Len))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBigBlob(ref CNetData poNetData)
{
	SBigBlob pstOut = new SBigBlob();

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_BIGBLOB_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Data,(int)pstOut.m_Len))
		return null;

	return pstOut;
}

public static	int		EncodeSMediumBlob(CMessage msg,ref CNetData poNetData)
{
	SMediumBlob pstIn = (SMediumBlob)msg;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_MEDIUMBLOB_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Data,(int)pstIn.m_Len))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMediumBlob(ref CNetData poNetData)
{
	SMediumBlob pstOut = new SMediumBlob();

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_MEDIUMBLOB_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Data,(int)pstOut.m_Len))
		return null;

	return pstOut;
}

public static	int		EncodeSBigMoreBlob(CMessage msg,ref CNetData poNetData)
{
	SBigMoreBlob pstIn = (SBigMoreBlob)msg;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_BIG_MORE_BLOB_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Data,(int)pstIn.m_Len))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBigMoreBlob(ref CNetData poNetData)
{
	SBigMoreBlob pstOut = new SBigMoreBlob();

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_BIG_MORE_BLOB_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Data,(int)pstOut.m_Len))
		return null;

	return pstOut;
}

public static	int		EncodeSEXBigBlob(CMessage msg,ref CNetData poNetData)
{
	SEXBigBlob pstIn = (SEXBigBlob)msg;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_EXEBIGBLOB_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Data,(int)pstIn.m_Len))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSEXBigBlob(ref CNetData poNetData)
{
	SEXBigBlob pstOut = new SEXBigBlob();

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_EXEBIGBLOB_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Data,(int)pstOut.m_Len))
		return null;

	return pstOut;
}

public static	int		EncodeSEquipInfo(CMessage msg,ref CNetData poNetData)
{
	SEquipInfo pstIn = (SEquipInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Slot))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HuanhuaTemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_StarCount))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSEquipInfo(ref CNetData poNetData)
{
	SEquipInfo pstOut = new SEquipInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Slot))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HuanhuaTemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_StarCount))
		return null;

	return pstOut;
}

public static	int		EncodeSRoleInfo(CMessage msg,ref CNetData poNetData)
{
	SRoleInfo pstIn = (SRoleInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_DBRoleId))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_RoleName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Gender))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SkinColor))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_createtime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LastLoginTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LastLogoutTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_InstanceSceneId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LatestSceneId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LatestInstanceSceneId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ForbidenTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleInfo(ref CNetData poNetData)
{
	SRoleInfo pstOut = new SRoleInfo();

	if(-1 == poNetData.Del(ref pstOut.m_DBRoleId))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_RoleName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Gender))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SkinColor))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_createtime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LastLoginTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LastLogoutTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_InstanceSceneId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LatestSceneId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LatestInstanceSceneId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ForbidenTime))
		return null;

	return pstOut;
}

public static	int		EncodeSXGEM(CMessage msg,ref CNetData poNetData)
{
	SXGEM pstIn = (SXGEM)msg;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSXGEM(ref CNetData poNetData)
{
	SXGEM pstOut = new SXGEM();

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	return pstOut;
}

public static	int		EncodeSXITEM(CMessage msg,ref CNetData poNetData)
{
	SXITEM pstIn = (SXITEM)msg;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CT))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EXP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LK))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GEMCount))
		return -1;

	int i;
	if ((int)pstIn.m_GEMCount > (int)MAX_GEM_COUNT || (int)pstIn.m_GEMCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_GEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_GEMCount)
			break;
		EncodeSXGEM(pstIn.m_GEM[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSXITEM(ref CNetData poNetData)
{
	SXITEM pstOut = new SXITEM();

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CT))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EXP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LK))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GEMCount))
		return null;

	int i;
	if ((int)pstOut.m_GEMCount > (int)MAX_GEM_COUNT || (int)pstOut.m_GEMCount < 0)
       return null;
	for(i = 0; i < (int)MAX_GEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_GEMCount)
			break;
		pstOut.m_GEM[i]	= (SXGEM)DecodeSXGEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSXITEMS(CMessage msg,ref CNetData poNetData)
{
	SXITEMS pstIn = (SXITEMS)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if ((int)pstIn.m_ItemCount > (int)MAX_ITEM_COUNT || (int)pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		EncodeSXITEM(pstIn.m_Items[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSXITEMS(ref CNetData poNetData)
{
	SXITEMS pstOut = new SXITEMS();

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)MAX_ITEM_COUNT || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		pstOut.m_Items[i]	= (SXITEM)DecodeSXITEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRoleInfoWithEquip(CMessage msg,ref CNetData poNetData)
{
	SRoleInfoWithEquip pstIn = (SRoleInfoWithEquip)msg;

		EncodeSRoleInfo(pstIn.m_RoleInfo,ref poNetData);
		EncodeSXITEMS(pstIn.m_Equip,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleInfoWithEquip(ref CNetData poNetData)
{
	SRoleInfoWithEquip pstOut = new SRoleInfoWithEquip();

	pstOut.m_RoleInfo	= (SRoleInfo)DecodeSRoleInfo(ref poNetData);
	pstOut.m_Equip	= (SXITEMS)DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeSRoleInfoWithEquipAll(CMessage msg,ref CNetData poNetData)
{
	SRoleInfoWithEquipAll pstIn = (SRoleInfoWithEquipAll)msg;

		EncodeSRoleInfo(pstIn.m_RoleInfo,ref poNetData);
		EncodeSRoleDBInfo(pstIn.m_RoleDBInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleInfoWithEquipAll(ref CNetData poNetData)
{
	SRoleInfoWithEquipAll pstOut = new SRoleInfoWithEquipAll();

	pstOut.m_RoleInfo	= (SRoleInfo)DecodeSRoleInfo(ref poNetData);
	pstOut.m_RoleDBInfo	= (SRoleDBInfo)DecodeSRoleDBInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSROLE_SUIT_ITEM(CMessage msg,ref CNetData poNetData)
{
	SROLE_SUIT_ITEM pstIn = (SROLE_SUIT_ITEM)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_SUIT_ITEM(ref CNetData poNetData)
{
	SROLE_SUIT_ITEM pstOut = new SROLE_SUIT_ITEM();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemID))
		return null;

	return pstOut;
}

public static	int		EncodeSROLE_SUIT(CMessage msg,ref CNetData poNetData)
{
	SROLE_SUIT pstIn = (SROLE_SUIT)msg;

	if(-1 == poNetData.Add(pstIn.m_SuitID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SuitCount))
		return -1;

	int i;
	if ((int)pstIn.m_SuitCount > (int)MAX_SUIT_ITEM_COUNT || (int)pstIn.m_SuitCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_SUIT_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_SuitCount)
			break;
		EncodeSROLE_SUIT_ITEM(pstIn.m_SuitItem[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_SUIT(ref CNetData poNetData)
{
	SROLE_SUIT pstOut = new SROLE_SUIT();

	if(-1 == poNetData.Del(ref pstOut.m_SuitID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SuitCount))
		return null;

	int i;
	if ((int)pstOut.m_SuitCount > (int)MAX_SUIT_ITEM_COUNT || (int)pstOut.m_SuitCount < 0)
       return null;
	for(i = 0; i < (int)MAX_SUIT_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_SuitCount)
			break;
		pstOut.m_SuitItem[i]	= (SROLE_SUIT_ITEM)DecodeSROLE_SUIT_ITEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSHeadRecord(CMessage msg,ref CNetData poNetData)
{
	SHeadRecord pstIn = (SHeadRecord)msg;

	if(-1 == poNetData.Add(pstIn.m_RandomValue))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HeadID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSHeadRecord(ref CNetData poNetData)
{
	SHeadRecord pstOut = new SHeadRecord();

	if(-1 == poNetData.Del(ref pstOut.m_RandomValue))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HeadID))
		return null;

	return pstOut;
}

public static	int		EncodeSRandomEquipHead(CMessage msg,ref CNetData poNetData)
{
	SRandomEquipHead pstIn = (SRandomEquipHead)msg;

	if(-1 == poNetData.Add(pstIn.m_IsIdentifyed))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_EQUIP_HEAD || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_EQUIP_HEAD; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSHeadRecord(pstIn.m_Heads[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRandomEquipHead(ref CNetData poNetData)
{
	SRandomEquipHead pstOut = new SRandomEquipHead();

	if(-1 == poNetData.Del(ref pstOut.m_IsIdentifyed))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_EQUIP_HEAD || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_EQUIP_HEAD; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Heads[i]	= (SHeadRecord)DecodeSHeadRecord( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSItemEquipExInfo(CMessage msg,ref CNetData poNetData)
{
	SItemEquipExInfo pstIn = (SItemEquipExInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxHoleNum))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Hole))
		return -1;

		EncodeSRandomEquipHead(pstIn.m_RandomHead,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Durability))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxDurability))
		return -1;

	int i;
	for(i = 0; i < (int)MAX_ITEM_HOLE_NUM; ++i)
	{
		if(-1 == poNetData.Add(pstIn.m_GemID[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_SmithingRandom))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_StarRandom))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ReduceEquipNeedLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HunhuaTemplateID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HunhuaEndTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuffID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_StarExp))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemEquipExInfo(ref CNetData poNetData)
{
	SItemEquipExInfo pstOut = new SItemEquipExInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxHoleNum))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Hole))
		return null;

	pstOut.m_RandomHead	= (SRandomEquipHead)DecodeSRandomEquipHead(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Durability))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxDurability))
		return null;

	int i;
	for(i = 0; i < (int)MAX_ITEM_HOLE_NUM; ++i)
	{
		if(-1 == poNetData.Del(ref pstOut.m_GemID[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_SmithingRandom))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_StarRandom))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ReduceEquipNeedLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HunhuaTemplateID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HunhuaEndTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuffID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_StarExp))
		return null;

	return pstOut;
}

public static	int		EncodeSItemMakerInfo(CMessage msg,ref CNetData poNetData)
{
	SItemMakerInfo pstIn = (SItemMakerInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Maker,(int)ROLE_NAME_LEN))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Virtue))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Evil))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Goodness))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Badness))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemMakerInfo(ref CNetData poNetData)
{
	SItemMakerInfo pstOut = new SItemMakerInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Maker,(int)ROLE_NAME_LEN))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Virtue))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Evil))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Goodness))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Badness))
		return null;

	return pstOut;
}

public static	int		EncodeSItemTimeInfo(CMessage msg,ref CNetData poNetData)
{
	SItemTimeInfo pstIn = (SItemTimeInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_UseNum))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DestroyTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemTimeInfo(ref CNetData poNetData)
{
	SItemTimeInfo pstOut = new SItemTimeInfo();

	if(-1 == poNetData.Del(ref pstOut.m_UseNum))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DestroyTime))
		return null;

	return pstOut;
}

public static	int		EncodeSNull(CMessage msg,ref CNetData poNetData)
{
	SNull pstIn = (SNull)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSNull(ref CNetData poNetData)
{
	SNull pstOut = new SNull();

	return pstOut;
}

public static	int EncodeUnUItemExInfo(CMessage msg,int iSelect, ref CNetData poNetData)
{
	UItemExInfo	pstIn	=	(UItemExInfo)msg;
	switch(iSelect){
	case (int)ITEM_EX_TYPE_NULL:
		return EncodeSNull(pstIn.m_Null,ref poNetData);
	case (int)ITEM_EX_TYPE_EQUIP:
		return EncodeSItemEquipExInfo(pstIn.m_EquipExInfo,ref poNetData);
	default: return -1;
	}
}

public	static	void DecodeUnUItemExInfo(CMessage msg,int iSelect, ref CNetData poNetData)
{
	UItemExInfo pstOut = (UItemExInfo)msg;
	switch(iSelect){
	case (int)ITEM_EX_TYPE_NULL:
		pstOut.m_Null	=	(SNull)DecodeSNull(ref poNetData);
		break;
	case (int)ITEM_EX_TYPE_EQUIP:
		pstOut.m_EquipExInfo	=	(SItemEquipExInfo)DecodeSItemEquipExInfo(ref poNetData);
		break;
	}
}

public static	int		EncodeSItemExInfo(CMessage msg,ref CNetData poNetData)
{
	SItemExInfo pstIn = (SItemExInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	EncodeUnUItemExInfo(pstIn.m_ExInfo,(int)pstIn.m_Type,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemExInfo(ref CNetData poNetData)
{
	SItemExInfo pstOut = new SItemExInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	DecodeUnUItemExInfo(pstOut.m_ExInfo,(int)pstOut.m_Type,ref poNetData);
	return pstOut;
}

public static	int		EncodeSExchangeItem(CMessage msg,ref CNetData poNetData)
{
	SExchangeItem pstIn = (SExchangeItem)msg;

	if(-1 == poNetData.Add(pstIn.m_Add))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FromIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FromSlot))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ToSlot))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSExchangeItem(ref CNetData poNetData)
{
	SExchangeItem pstOut = new SExchangeItem();

	if(-1 == poNetData.Del(ref pstOut.m_Add))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FromIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FromSlot))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ToSlot))
		return null;

	return pstOut;
}

public static	int		EncodeSItemShortcut(CMessage msg,ref CNetData poNetData)
{
	SItemShortcut pstIn = (SItemShortcut)msg;

	if(-1 == poNetData.Add(pstIn.m_Slot))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemShortcut(ref CNetData poNetData)
{
	SItemShortcut pstOut = new SItemShortcut();

	if(-1 == poNetData.Del(ref pstOut.m_Slot))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUId))
		return null;

	return pstOut;
}

public static	int		EncodeSRoleName(CMessage msg,ref CNetData poNetData)
{
	SRoleName pstIn = (SRoleName)msg;

	if(-1 == poNetData.AddString(pstIn.m_RoleName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleName(ref CNetData poNetData)
{
	SRoleName pstOut = new SRoleName();

	if(-1 == poNetData.DelString(ref pstOut.m_RoleName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	return pstOut;
}

public static	int		EncodeSServerInfo(CMessage msg,ref CNetData poNetData)
{
	SServerInfo pstIn = (SServerInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_ServerID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ListenIP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ListenPort))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSServerInfo(ref CNetData poNetData)
{
	SServerInfo pstOut = new SServerInfo();

	if(-1 == poNetData.Del(ref pstOut.m_ServerID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ListenIP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ListenPort))
		return null;

	return pstOut;
}

public static	int		EncodeMonthCardInfo(CMessage msg,ref CNetData poNetData)
{
	MonthCardInfo pstIn = (MonthCardInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_UId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BTm))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Days))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UTm))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeMonthCardInfo(ref CNetData poNetData)
{
	MonthCardInfo pstOut = new MonthCardInfo();

	if(-1 == poNetData.Del(ref pstOut.m_UId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BTm))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Days))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UTm))
		return null;

	return pstOut;
}

public static	int		EncodeSLMAccountInfo(CMessage msg,ref CNetData poNetData)
{
	SLMAccountInfo pstIn = (SLMAccountInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_AccountId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RMBCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RMBSum))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_VIPLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GMLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MonthCardCount))
		return -1;

	int i;
	if ((int)pstIn.m_MonthCardCount > (int)MAX_MONTHCARD_COUNT || (int)pstIn.m_MonthCardCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_MONTHCARD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_MonthCardCount)
			break;
		EncodeMonthCardInfo(pstIn.m_MonthCardInfos[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSLMAccountInfo(ref CNetData poNetData)
{
	SLMAccountInfo pstOut = new SLMAccountInfo();

	if(-1 == poNetData.Del(ref pstOut.m_AccountId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RMBCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RMBSum))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_VIPLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GMLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MonthCardCount))
		return null;

	int i;
	if ((int)pstOut.m_MonthCardCount > (int)MAX_MONTHCARD_COUNT || (int)pstOut.m_MonthCardCount < 0)
       return null;
	for(i = 0; i < (int)MAX_MONTHCARD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_MonthCardCount)
			break;
		pstOut.m_MonthCardInfos[i]	= (MonthCardInfo)DecodeMonthCardInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGameServerDescInfo(CMessage msg,ref CNetData poNetData)
{
	SGameServerDescInfo pstIn = (SGameServerDescInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_OptLen))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Opt,(int)255))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DescLen))
		return -1;

   if ((int)pstIn.m_DescLen > (int)MAX_SERVER_DESC_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Desc,(int)pstIn.m_DescLen))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_StateLen))
		return -1;

   if ((int)pstIn.m_StateLen > (int)MAX_SERVER_STATE_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_State,(int)pstIn.m_StateLen))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGameServerDescInfo(ref CNetData poNetData)
{
	SGameServerDescInfo pstOut = new SGameServerDescInfo();

	if(-1 == poNetData.Del(ref pstOut.m_OptLen))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Opt,(int)255))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DescLen))
		return null;

   if ((int)pstOut.m_DescLen > (int)MAX_SERVER_DESC_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Desc,(int)pstOut.m_DescLen))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_StateLen))
		return null;

   if ((int)pstOut.m_StateLen > (int)MAX_SERVER_STATE_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_State,(int)pstOut.m_StateLen))
		return null;

	return pstOut;
}

public static	int		EncodeSGameServerInfo(CMessage msg,ref CNetData poNetData)
{
	SGameServerInfo pstIn = (SGameServerInfo)msg;

		EncodeSServerInfo(pstIn.m_Server,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_PlayerCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ServerState))
		return -1;

		EncodeSGameServerDescInfo(pstIn.m_Desc,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_ServerUniqueId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGameServerInfo(ref CNetData poNetData)
{
	SGameServerInfo pstOut = new SGameServerInfo();

	pstOut.m_Server	= (SServerInfo)DecodeSServerInfo(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_PlayerCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ServerState))
		return null;

	pstOut.m_Desc	= (SGameServerDescInfo)DecodeSGameServerDescInfo(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_ServerUniqueId))
		return null;

	return pstOut;
}

public static	int		EncodeCTDT_CONNECTION_INFO(CMessage msg,ref CNetData poNetData)
{
	CTDT_CONNECTION_INFO pstIn = (CTDT_CONNECTION_INFO)msg;

	if(-1 == poNetData.Add(pstIn.m_IncomingBytesPerMinute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OutgoingBytesPerMinute))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCTDT_CONNECTION_INFO(ref CNetData poNetData)
{
	CTDT_CONNECTION_INFO pstOut = new CTDT_CONNECTION_INFO();

	if(-1 == poNetData.Del(ref pstOut.m_IncomingBytesPerMinute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OutgoingBytesPerMinute))
		return null;

	return pstOut;
}

public static	int		EncodeSUUItemInfo(CMessage msg,ref CNetData poNetData)
{
	SUUItemInfo pstIn = (SUUItemInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSUUItemInfo(ref CNetData poNetData)
{
	SUUItemInfo pstOut = new SUUItemInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	return pstOut;
}

public static	int		EncodeSItemList(CMessage msg,ref CNetData poNetData)
{
	SItemList pstIn = (SItemList)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_ROLE_ITEM_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_ROLE_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSUUItemInfo(pstIn.m_ItemList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSItemList(ref CNetData poNetData)
{
	SItemList pstOut = new SItemList();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_ROLE_ITEM_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_ROLE_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_ItemList[i]	= (SUUItemInfo)DecodeSUUItemInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSLevelInfo(CMessage msg,ref CNetData poNetData)
{
	SLevelInfo pstIn = (SLevelInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_LevelID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Crystal))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UsedTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSLevelInfo(ref CNetData poNetData)
{
	SLevelInfo pstOut = new SLevelInfo();

	if(-1 == poNetData.Del(ref pstOut.m_LevelID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Crystal))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UsedTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSBattleInfo(CMessage msg,ref CNetData poNetData)
{
	SBattleInfo pstIn = (SBattleInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_BattleID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GetStars))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBattleInfo(ref CNetData poNetData)
{
	SBattleInfo pstOut = new SBattleInfo();

	if(-1 == poNetData.Del(ref pstOut.m_BattleID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GetStars))
		return null;

	return pstOut;
}

public static	int		EncodeSCHAT(CMessage msg,ref CNetData poNetData)
{
	SCHAT pstIn = (SCHAT)msg;

	if(-1 == poNetData.Add(pstIn.m_ChatType))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Who))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ToWho))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Content))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GmLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SpeakerVIPLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSCHAT(ref CNetData poNetData)
{
	SCHAT pstOut = new SCHAT();

	if(-1 == poNetData.Del(ref pstOut.m_ChatType))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Who))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ToWho))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Content))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GmLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SpeakerVIPLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSFriendEquipInfo(CMessage msg,ref CNetData poNetData)
{
	SFriendEquipInfo pstIn = (SFriendEquipInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSFriendEquipInfo(ref CNetData poNetData)
{
	SFriendEquipInfo pstOut = new SFriendEquipInfo();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateID))
		return null;

	return pstOut;
}

public static	int		EncodeSFriendPetInfo(CMessage msg,ref CNetData poNetData)
{
	SFriendPetInfo pstIn = (SFriendPetInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSFriendPetInfo(ref CNetData poNetData)
{
	SFriendPetInfo pstOut = new SFriendPetInfo();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateID))
		return null;

	return pstOut;
}

public static	int		EncodeSRSNull(CMessage msg,ref CNetData poNetData)
{
	SRSNull pstIn = (SRSNull)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSNull(ref CNetData poNetData)
{
	SRSNull pstOut = new SRSNull();

	return pstOut;
}

public static	int		EncodeSRSRoleBaseData(CMessage msg,ref CNetData poNetData)
{
	SRSRoleBaseData pstIn = (SRSRoleBaseData)msg;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PVPWins))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PVPRepute))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSRoleBaseData(ref CNetData poNetData)
{
	SRSRoleBaseData pstOut = new SRSRoleBaseData();

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PVPWins))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PVPRepute))
		return null;

	return pstOut;
}

public static	int		EncodeSRSRoleEquipData(CMessage msg,ref CNetData poNetData)
{
	SRSRoleEquipData pstIn = (SRSRoleEquipData)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_EQUIP_ITEM_NUM || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_EQUIP_ITEM_NUM; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSXITEM(pstIn.m_EquipInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSRoleEquipData(ref CNetData poNetData)
{
	SRSRoleEquipData pstOut = new SRSRoleEquipData();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_EQUIP_ITEM_NUM || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_EQUIP_ITEM_NUM; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_EquipInfo[i]	= (SXITEM)DecodeSXITEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRSRolePetData(CMessage msg,ref CNetData poNetData)
{
	SRSRolePetData pstIn = (SRSRolePetData)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_FRIENDS_PET_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_FRIENDS_PET_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSFriendPetInfo(pstIn.m_PetInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSRolePetData(ref CNetData poNetData)
{
	SRSRolePetData pstOut = new SRSRolePetData();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_FRIENDS_PET_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_FRIENDS_PET_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_PetInfo[i]	= (SFriendPetInfo)DecodeSFriendPetInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRSRoleOnlineData(CMessage msg,ref CNetData poNetData)
{
	SRSRoleOnlineData pstIn = (SRSRoleOnlineData)msg;

	if(-1 == poNetData.Add(pstIn.m_IsOnline))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSRoleOnlineData(ref CNetData poNetData)
{
	SRSRoleOnlineData pstOut = new SRSRoleOnlineData();

	if(-1 == poNetData.Del(ref pstOut.m_IsOnline))
		return null;

	return pstOut;
}

public static	int EncodeUnURSRoleProperty(CMessage msg,int iSelect, ref CNetData poNetData)
{
	URSRoleProperty	pstIn	=	(URSRoleProperty)msg;
	switch(iSelect){
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_NULL:
		return EncodeSRSNull(pstIn.m_Null,ref poNetData);
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_BASE:
		return EncodeSRSRoleBaseData(pstIn.m_Base,ref poNetData);
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_EQUIP:
		return EncodeSRSRoleEquipData(pstIn.m_Equip,ref poNetData);
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_PET:
		return EncodeSRSRolePetData(pstIn.m_Pet,ref poNetData);
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_ONLINE:
		return EncodeSRSRoleOnlineData(pstIn.m_Online,ref poNetData);
	default: return -1;
	}
}

public	static	void DecodeUnURSRoleProperty(CMessage msg,int iSelect, ref CNetData poNetData)
{
	URSRoleProperty pstOut = (URSRoleProperty)msg;
	switch(iSelect){
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_NULL:
		pstOut.m_Null	=	(SRSNull)DecodeSRSNull(ref poNetData);
		break;
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_BASE:
		pstOut.m_Base	=	(SRSRoleBaseData)DecodeSRSRoleBaseData(ref poNetData);
		break;
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_EQUIP:
		pstOut.m_Equip	=	(SRSRoleEquipData)DecodeSRSRoleEquipData(ref poNetData);
		break;
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_PET:
		pstOut.m_Pet	=	(SRSRolePetData)DecodeSRSRolePetData(ref poNetData);
		break;
	case (int)RS_ROLE_PROERTY_TYPE.RS_ROLE_PROPERTY_ONLINE:
		pstOut.m_Online	=	(SRSRoleOnlineData)DecodeSRSRoleOnlineData(ref poNetData);
		break;
	}
}

public static	int		EncodeSRSRoleProperty(CMessage msg,ref CNetData poNetData)
{
	SRSRoleProperty pstIn = (SRSRoleProperty)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	EncodeUnURSRoleProperty(pstIn.m_Property,(int)pstIn.m_Type,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSRoleProperty(ref CNetData poNetData)
{
	SRSRoleProperty pstOut = new SRSRoleProperty();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	DecodeUnURSRoleProperty(pstOut.m_Property,(int)pstOut.m_Type,ref poNetData);
	return pstOut;
}

public static	int		EncodeSFriendInfo(CMessage msg,ref CNetData poNetData)
{
	SFriendInfo pstIn = (SFriendInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Head))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SkinColor))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IsOnline))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ActionPoint))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CanDonateActionPoint))
		return -1;

		EncodeSRSRoleEquipData(pstIn.m_Equip,ref poNetData);
		EncodeSRSRolePetData(pstIn.m_Pet,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSFriendInfo(ref CNetData poNetData)
{
	SFriendInfo pstOut = new SFriendInfo();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Head))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SkinColor))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IsOnline))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ActionPoint))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CanDonateActionPoint))
		return null;

	pstOut.m_Equip	= (SRSRoleEquipData)DecodeSRSRoleEquipData(ref poNetData);
	pstOut.m_Pet	= (SRSRolePetData)DecodeSRSRolePetData(ref poNetData);
	return pstOut;
}

public static	int		EncodeSFriendInviteInfo(CMessage msg,ref CNetData poNetData)
{
	SFriendInviteInfo pstIn = (SFriendInviteInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

		EncodeSFriendInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSFriendInviteInfo(ref CNetData poNetData)
{
	SFriendInviteInfo pstOut = new SFriendInviteInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	pstOut.m_Info	= (SFriendInfo)DecodeSFriendInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSFriendInviteList(CMessage msg,ref CNetData poNetData)
{
	SFriendInviteList pstIn = (SFriendInviteList)msg;

	if(-1 == poNetData.Add(pstIn.m_InviteCount))
		return -1;

	int i;
	if ((int)pstIn.m_InviteCount > (int)MAX_FRIENDS_INVITE_COUNT || (int)pstIn.m_InviteCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_FRIENDS_INVITE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_InviteCount)
			break;
		EncodeSFriendInviteInfo(pstIn.m_InviteList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSFriendInviteList(ref CNetData poNetData)
{
	SFriendInviteList pstOut = new SFriendInviteList();

	if(-1 == poNetData.Del(ref pstOut.m_InviteCount))
		return null;

	int i;
	if ((int)pstOut.m_InviteCount > (int)MAX_FRIENDS_INVITE_COUNT || (int)pstOut.m_InviteCount < 0)
       return null;
	for(i = 0; i < (int)MAX_FRIENDS_INVITE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_InviteCount)
			break;
		pstOut.m_InviteList[i]	= (SFriendInviteInfo)DecodeSFriendInviteInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRoleBaseInfo(CMessage msg,ref CNetData poNetData)
{
	SRoleBaseInfo pstIn = (SRoleBaseInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SkinColor))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PVPWins))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PVPRepute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IsOnline))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_VIPLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleBaseInfo(ref CNetData poNetData)
{
	SRoleBaseInfo pstOut = new SRoleBaseInfo();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SkinColor))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PVPWins))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PVPRepute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IsOnline))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_VIPLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSDBPET(CMessage msg,ref CNetData poNetData)
{
	SDBPET pstIn = (SDBPET)msg;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LV))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EXP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LOCK))
		return -1;

		EncodeSXITEMS(pstIn.m_EQ,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSDBPET(ref CNetData poNetData)
{
	SDBPET pstOut = new SDBPET();

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LV))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EXP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LOCK))
		return null;

	pstOut.m_EQ	= (SXITEMS)DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeSRSBattlePets(CMessage msg,ref CNetData poNetData)
{
	SRSBattlePets pstIn = (SRSBattlePets)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_BATTLE_PET_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_BATTLE_PET_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSDBPET(pstIn.m_Pets[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRSBattlePets(ref CNetData poNetData)
{
	SRSBattlePets pstOut = new SRSBattlePets();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_BATTLE_PET_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_BATTLE_PET_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Pets[i]	= (SDBPET)DecodeSDBPET( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRoleRsServerInfo(CMessage msg,ref CNetData poNetData)
{
	SRoleRsServerInfo pstIn = (SRoleRsServerInfo)msg;

		EncodeSRoleBaseInfo(pstIn.m_BaseInfo,ref poNetData);
		EncodeSRSRoleEquipData(pstIn.m_Equip,ref poNetData);
		EncodeSRSRolePetData(pstIn.m_Pet,ref poNetData);
		EncodeSRSBattlePets(pstIn.m_BattlePets,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRoleRsServerInfo(ref CNetData poNetData)
{
	SRoleRsServerInfo pstOut = new SRoleRsServerInfo();

	pstOut.m_BaseInfo	= (SRoleBaseInfo)DecodeSRoleBaseInfo(ref poNetData);
	pstOut.m_Equip	= (SRSRoleEquipData)DecodeSRSRoleEquipData(ref poNetData);
	pstOut.m_Pet	= (SRSRolePetData)DecodeSRSRolePetData(ref poNetData);
	pstOut.m_BattlePets	= (SRSBattlePets)DecodeSRSBattlePets(ref poNetData);
	return pstOut;
}

public static	int		EncodeSAssistBattlePersonInfo(CMessage msg,ref CNetData poNetData)
{
	SAssistBattlePersonInfo pstIn = (SAssistBattlePersonInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_IsFriend))
		return -1;

		EncodeSFriendInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSAssistBattlePersonInfo(ref CNetData poNetData)
{
	SAssistBattlePersonInfo pstOut = new SAssistBattlePersonInfo();

	if(-1 == poNetData.Del(ref pstOut.m_IsFriend))
		return null;

	pstOut.m_Info	= (SFriendInfo)DecodeSFriendInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSSeverAssistBattleInfo(CMessage msg,ref CNetData poNetData)
{
	SSeverAssistBattleInfo pstIn = (SSeverAssistBattleInfo)msg;

		EncodeSAssistBattlePersonInfo(pstIn.m_Assistor,ref poNetData);
		EncodeSDBPET(pstIn.m_BattlePet,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSSeverAssistBattleInfo(ref CNetData poNetData)
{
	SSeverAssistBattleInfo pstOut = new SSeverAssistBattleInfo();

	pstOut.m_Assistor	= (SAssistBattlePersonInfo)DecodeSAssistBattlePersonInfo(ref poNetData);
	pstOut.m_BattlePet	= (SDBPET)DecodeSDBPET(ref poNetData);
	return pstOut;
}

public static	int		EncodeFRIENDS_LIST_NTF(CMessage msg,ref CNetData poNetData)
{
	FRIENDS_LIST_NTF pstIn = (FRIENDS_LIST_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_FRIENDS_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_FRIENDS_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSFriendInfo(pstIn.m_List[i],ref poNetData);
	}

		EncodeSFriendInviteList(pstIn.m_InviteList,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeFRIENDS_LIST_NTF(ref CNetData poNetData)
{
	FRIENDS_LIST_NTF pstOut = new FRIENDS_LIST_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_FRIENDS_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_FRIENDS_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_List[i]	= (SFriendInfo)DecodeSFriendInfo( ref poNetData);
	}

	pstOut.m_InviteList	= (SFriendInviteList)DecodeSFriendInviteList(ref poNetData);
	return pstOut;
}

public static	int		EncodeQUERY_ROLE_REQ(CMessage msg,ref CNetData poNetData)
{
	QUERY_ROLE_REQ pstIn = (QUERY_ROLE_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Page))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_KeyWords))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeQUERY_ROLE_REQ(ref CNetData poNetData)
{
	QUERY_ROLE_REQ pstOut = new QUERY_ROLE_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Page))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_KeyWords))
		return null;

	return pstOut;
}

public static	int		EncodeQUERY_ROLE_ACK(CMessage msg,ref CNetData poNetData)
{
	QUERY_ROLE_ACK pstIn = (QUERY_ROLE_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Page))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TotalPage))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_FRIENDS_COUNT_PER_PAGE || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_FRIENDS_COUNT_PER_PAGE; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSFriendInfo(pstIn.m_Lists[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeQUERY_ROLE_ACK(ref CNetData poNetData)
{
	QUERY_ROLE_ACK pstOut = new QUERY_ROLE_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Page))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TotalPage))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_FRIENDS_COUNT_PER_PAGE || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_FRIENDS_COUNT_PER_PAGE; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Lists[i]	= (SFriendInfo)DecodeSFriendInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeADD_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	ADD_FRIEND_REQ pstIn = (ADD_FRIEND_REQ)msg;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeADD_FRIEND_REQ(ref CNetData poNetData)
{
	ADD_FRIEND_REQ pstOut = new ADD_FRIEND_REQ();

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	return pstOut;
}

public static	int		EncodeADD_FRIEND_ACK(CMessage msg,ref CNetData poNetData)
{
	ADD_FRIEND_ACK pstIn = (ADD_FRIEND_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)1 || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)1; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSFriendInfo(pstIn.m_Friends[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeADD_FRIEND_ACK(ref CNetData poNetData)
{
	ADD_FRIEND_ACK pstOut = new ADD_FRIEND_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)1 || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)1; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Friends[i]	= (SFriendInfo)DecodeSFriendInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeREMOVE_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	REMOVE_FRIEND_REQ pstIn = (REMOVE_FRIEND_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeREMOVE_FRIEND_REQ(ref CNetData poNetData)
{
	REMOVE_FRIEND_REQ pstOut = new REMOVE_FRIEND_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeREMOVE_FRIEND_ACK(CMessage msg,ref CNetData poNetData)
{
	REMOVE_FRIEND_ACK pstIn = (REMOVE_FRIEND_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeREMOVE_FRIEND_ACK(ref CNetData poNetData)
{
	REMOVE_FRIEND_ACK pstOut = new REMOVE_FRIEND_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeDONATE_ACTION_POINT_REQ(CMessage msg,ref CNetData poNetData)
{
	DONATE_ACTION_POINT_REQ pstIn = (DONATE_ACTION_POINT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeDONATE_ACTION_POINT_REQ(ref CNetData poNetData)
{
	DONATE_ACTION_POINT_REQ pstOut = new DONATE_ACTION_POINT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeDONATE_ACTION_POINT_ACK(CMessage msg,ref CNetData poNetData)
{
	DONATE_ACTION_POINT_ACK pstIn = (DONATE_ACTION_POINT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeDONATE_ACTION_POINT_ACK(ref CNetData poNetData)
{
	DONATE_ACTION_POINT_ACK pstOut = new DONATE_ACTION_POINT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeDONATE_ACTION_POINT_OTHER_NTF(CMessage msg,ref CNetData poNetData)
{
	DONATE_ACTION_POINT_OTHER_NTF pstIn = (DONATE_ACTION_POINT_OTHER_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeDONATE_ACTION_POINT_OTHER_NTF(ref CNetData poNetData)
{
	DONATE_ACTION_POINT_OTHER_NTF pstOut = new DONATE_ACTION_POINT_OTHER_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeRECEIVE_ACTION_POINT_REQ(CMessage msg,ref CNetData poNetData)
{
	RECEIVE_ACTION_POINT_REQ pstIn = (RECEIVE_ACTION_POINT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeRECEIVE_ACTION_POINT_REQ(ref CNetData poNetData)
{
	RECEIVE_ACTION_POINT_REQ pstOut = new RECEIVE_ACTION_POINT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeRECEIVE_ACTION_POINT_ACK(CMessage msg,ref CNetData poNetData)
{
	RECEIVE_ACTION_POINT_ACK pstIn = (RECEIVE_ACTION_POINT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeRECEIVE_ACTION_POINT_ACK(ref CNetData poNetData)
{
	RECEIVE_ACTION_POINT_ACK pstOut = new RECEIVE_ACTION_POINT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeREQUEST_ADD_FRIEND_NTF(CMessage msg,ref CNetData poNetData)
{
	REQUEST_ADD_FRIEND_NTF pstIn = (REQUEST_ADD_FRIEND_NTF)msg;

		EncodeSFriendInviteInfo(pstIn.m_NewInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeREQUEST_ADD_FRIEND_NTF(ref CNetData poNetData)
{
	REQUEST_ADD_FRIEND_NTF pstOut = new REQUEST_ADD_FRIEND_NTF();

	pstOut.m_NewInfo	= (SFriendInviteInfo)DecodeSFriendInviteInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeRESPONSE_ADD_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	RESPONSE_ADD_FRIEND_REQ pstIn = (RESPONSE_ADD_FRIEND_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Agree))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeRESPONSE_ADD_FRIEND_REQ(ref CNetData poNetData)
{
	RESPONSE_ADD_FRIEND_REQ pstOut = new RESPONSE_ADD_FRIEND_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Agree))
		return null;

	return pstOut;
}

public static	int		EncodeFRIEND_REMOVE_INVITE_NTF(CMessage msg,ref CNetData poNetData)
{
	FRIEND_REMOVE_INVITE_NTF pstIn = (FRIEND_REMOVE_INVITE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeFRIEND_REMOVE_INVITE_NTF(ref CNetData poNetData)
{
	FRIEND_REMOVE_INVITE_NTF pstOut = new FRIEND_REMOVE_INVITE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeFRIEND_INFO_CHANGE_NTF(CMessage msg,ref CNetData poNetData)
{
	FRIEND_INFO_CHANGE_NTF pstIn = (FRIEND_INFO_CHANGE_NTF)msg;

		EncodeSRSRoleProperty(pstIn.m_Info,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeFRIEND_INFO_CHANGE_NTF(ref CNetData poNetData)
{
	FRIEND_INFO_CHANGE_NTF pstOut = new FRIEND_INFO_CHANGE_NTF();

	pstOut.m_Info	= (SRSRoleProperty)DecodeSRSRoleProperty(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeFRIEND_ASSIST_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	FRIEND_ASSIST_LIST_REQ pstIn = (FRIEND_ASSIST_LIST_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeFRIEND_ASSIST_LIST_REQ(ref CNetData poNetData)
{
	FRIEND_ASSIST_LIST_REQ pstOut = new FRIEND_ASSIST_LIST_REQ();

	return pstOut;
}

public static	int		EncodeSELECT_FRIEND_ASSIST_REQ(CMessage msg,ref CNetData poNetData)
{
	SELECT_FRIEND_ASSIST_REQ pstIn = (SELECT_FRIEND_ASSIST_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSELECT_FRIEND_ASSIST_REQ(ref CNetData poNetData)
{
	SELECT_FRIEND_ASSIST_REQ pstOut = new SELECT_FRIEND_ASSIST_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeSELECT_FRIEND_ASSIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SELECT_FRIEND_ASSIST_ACK pstIn = (SELECT_FRIEND_ASSIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSELECT_FRIEND_ASSIST_ACK(ref CNetData poNetData)
{
	SELECT_FRIEND_ASSIST_ACK pstOut = new SELECT_FRIEND_ASSIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSAbyssRsServerInfo(CMessage msg,ref CNetData poNetData)
{
	SAbyssRsServerInfo pstIn = (SAbyssRsServerInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_ActDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ActTmpId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Blood))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Rolename))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Atkcount))
		return -1;

	int i;
	for(i = 0; i < (int)MAX_FRIEND_COUNT; ++i)
	{
		if(-1 == poNetData.Add(pstIn.m_Atklist[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_Status))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Tritime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Opentime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Killtime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EntranceExistTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyssExistTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSAbyssRsServerInfo(ref CNetData poNetData)
{
	SAbyssRsServerInfo pstOut = new SAbyssRsServerInfo();

	if(-1 == poNetData.Del(ref pstOut.m_ActDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ActTmpId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Blood))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Rolename))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Atkcount))
		return null;

	int i;
	for(i = 0; i < (int)MAX_FRIEND_COUNT; ++i)
	{
		if(-1 == poNetData.Del(ref pstOut.m_Atklist[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_Status))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Tritime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Opentime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Killtime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EntranceExistTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyssExistTime))
		return null;

	return pstOut;
}

public static	int		EncodeABYSS_TRIGGER_REQ(CMessage msg,ref CNetData poNetData)
{
	ABYSS_TRIGGER_REQ pstIn = (ABYSS_TRIGGER_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_AbyssDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyTemplateID))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_AbyName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MonsterID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TotalBlood))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TriggerTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EntranceExistTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyssExistTime))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Rolename))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_TRIGGER_REQ(ref CNetData poNetData)
{
	ABYSS_TRIGGER_REQ pstOut = new ABYSS_TRIGGER_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_AbyssDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyTemplateID))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_AbyName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MonsterID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TotalBlood))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TriggerTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EntranceExistTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyssExistTime))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Rolename))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	return pstOut;
}

public static	int		EncodeABYSS_TRIGGER_ACK(CMessage msg,ref CNetData poNetData)
{
	ABYSS_TRIGGER_ACK pstIn = (ABYSS_TRIGGER_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_IfTrigger))
		return -1;

		EncodeSAbyssRsServerInfo(pstIn.m_AbyssInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_TRIGGER_ACK(ref CNetData poNetData)
{
	ABYSS_TRIGGER_ACK pstOut = new ABYSS_TRIGGER_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_IfTrigger))
		return null;

	pstOut.m_AbyssInfo	= (SAbyssRsServerInfo)DecodeSAbyssRsServerInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeABYSS_OPEN_REQ(CMessage msg,ref CNetData poNetData)
{
	ABYSS_OPEN_REQ pstIn = (ABYSS_OPEN_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_AbyssDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OpenTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_OPEN_REQ(ref CNetData poNetData)
{
	ABYSS_OPEN_REQ pstOut = new ABYSS_OPEN_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_AbyssDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OpenTime))
		return null;

	return pstOut;
}

public static	int		EncodeABYSS_OPEN_ACK(CMessage msg,ref CNetData poNetData)
{
	ABYSS_OPEN_ACK pstIn = (ABYSS_OPEN_ACK)msg;

		EncodeSAbyssRsServerInfo(pstIn.m_AbyssInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_OPEN_ACK(ref CNetData poNetData)
{
	ABYSS_OPEN_ACK pstOut = new ABYSS_OPEN_ACK();

	pstOut.m_AbyssInfo	= (SAbyssRsServerInfo)DecodeSAbyssRsServerInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeABYSS_OPEN_NTF(CMessage msg,ref CNetData poNetData)
{
	ABYSS_OPEN_NTF pstIn = (ABYSS_OPEN_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_AbyssCount))
		return -1;

	int i;
	if ((int)pstIn.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstIn.m_AbyssCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_AbyssCount)
			break;
		EncodeSAbyssRsServerInfo(pstIn.m_AbyssInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_OPEN_NTF(ref CNetData poNetData)
{
	ABYSS_OPEN_NTF pstOut = new ABYSS_OPEN_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_AbyssCount))
		return null;

	int i;
	if ((int)pstOut.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstOut.m_AbyssCount < 0)
       return null;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_AbyssCount)
			break;
		pstOut.m_AbyssInfo[i]	= (SAbyssRsServerInfo)DecodeSAbyssRsServerInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeGET_ABYSS_TRIGGER_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_TRIGGER_LIST_REQ pstIn = (GET_ABYSS_TRIGGER_LIST_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_TRIGGER_LIST_REQ(ref CNetData poNetData)
{
	GET_ABYSS_TRIGGER_LIST_REQ pstOut = new GET_ABYSS_TRIGGER_LIST_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeGET_ABYSS_TRIGGER_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_TRIGGER_LIST_ACK pstIn = (GET_ABYSS_TRIGGER_LIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_AbyssCount))
		return -1;

	int i;
	if ((int)pstIn.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstIn.m_AbyssCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_AbyssCount)
			break;
		EncodeSAbyssRsServerInfo(pstIn.m_AbyssInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_TRIGGER_LIST_ACK(ref CNetData poNetData)
{
	GET_ABYSS_TRIGGER_LIST_ACK pstOut = new GET_ABYSS_TRIGGER_LIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_AbyssCount))
		return null;

	int i;
	if ((int)pstOut.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstOut.m_AbyssCount < 0)
       return null;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_AbyssCount)
			break;
		pstOut.m_AbyssInfo[i]	= (SAbyssRsServerInfo)DecodeSAbyssRsServerInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeGET_ABYSS_OPEN_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_OPEN_LIST_REQ pstIn = (GET_ABYSS_OPEN_LIST_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_OPEN_LIST_REQ(ref CNetData poNetData)
{
	GET_ABYSS_OPEN_LIST_REQ pstOut = new GET_ABYSS_OPEN_LIST_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeGET_ABYSS_OPEN_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_OPEN_LIST_ACK pstIn = (GET_ABYSS_OPEN_LIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_AbyssCount))
		return -1;

	int i;
	if ((int)pstIn.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstIn.m_AbyssCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_AbyssCount)
			break;
		EncodeSAbyssRsServerInfo(pstIn.m_AbyssInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_OPEN_LIST_ACK(ref CNetData poNetData)
{
	GET_ABYSS_OPEN_LIST_ACK pstOut = new GET_ABYSS_OPEN_LIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_AbyssCount))
		return null;

	int i;
	if ((int)pstOut.m_AbyssCount > (int)MAX_ABYSS_OPEN_COUNT || (int)pstOut.m_AbyssCount < 0)
       return null;
	for(i = 0; i < (int)MAX_ABYSS_OPEN_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_AbyssCount)
			break;
		pstOut.m_AbyssInfo[i]	= (SAbyssRsServerInfo)DecodeSAbyssRsServerInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeABYSS_OPEN_RECORD(CMessage msg,ref CNetData poNetData)
{
	ABYSS_OPEN_RECORD pstIn = (ABYSS_OPEN_RECORD)msg;

	if(-1 == poNetData.Add(pstIn.m_Abydbid))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyTmpid))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Opentime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Rolename))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_OPEN_RECORD(ref CNetData poNetData)
{
	ABYSS_OPEN_RECORD pstOut = new ABYSS_OPEN_RECORD();

	if(-1 == poNetData.Del(ref pstOut.m_Abydbid))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyTmpid))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Opentime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Rolename))
		return null;

	return pstOut;
}

public static	int		EncodeGET_ABYSS_OPEN_REC_REQ(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_OPEN_REC_REQ pstIn = (GET_ABYSS_OPEN_REC_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_OPEN_REC_REQ(ref CNetData poNetData)
{
	GET_ABYSS_OPEN_REC_REQ pstOut = new GET_ABYSS_OPEN_REC_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeGET_ABYSS_OPEN_REC_ACK(CMessage msg,ref CNetData poNetData)
{
	GET_ABYSS_OPEN_REC_ACK pstIn = (GET_ABYSS_OPEN_REC_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_XABYSS_RECORD_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_XABYSS_RECORD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeABYSS_OPEN_RECORD(pstIn.m_Records[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGET_ABYSS_OPEN_REC_ACK(ref CNetData poNetData)
{
	GET_ABYSS_OPEN_REC_ACK pstOut = new GET_ABYSS_OPEN_REC_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_XABYSS_RECORD_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_XABYSS_RECORD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Records[i]	= (ABYSS_OPEN_RECORD)DecodeABYSS_OPEN_RECORD( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeATK_INFO(CMessage msg,ref CNetData poNetData)
{
	ATK_INFO pstIn = (ATK_INFO)msg;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeATK_INFO(ref CNetData poNetData)
{
	ATK_INFO pstOut = new ATK_INFO();

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	return pstOut;
}

public static	int		EncodeATK_INFO_LIST(CMessage msg,ref CNetData poNetData)
{
	ATK_INFO_LIST pstIn = (ATK_INFO_LIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Atkcount))
		return -1;

	int i;
	if ((int)pstIn.m_Atkcount > (int)MAX_FRIEND_COUNT || (int)pstIn.m_Atkcount < 0)
       return -1;
	for(i = 0; i < (int)MAX_FRIEND_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Atkcount)
			break;
		EncodeATK_INFO(pstIn.m_Atklist[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeATK_INFO_LIST(ref CNetData poNetData)
{
	ATK_INFO_LIST pstOut = new ATK_INFO_LIST();

	if(-1 == poNetData.Del(ref pstOut.m_Atkcount))
		return null;

	int i;
	if ((int)pstOut.m_Atkcount > (int)MAX_FRIEND_COUNT || (int)pstOut.m_Atkcount < 0)
       return null;
	for(i = 0; i < (int)MAX_FRIEND_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Atkcount)
			break;
		pstOut.m_Atklist[i]	= (ATK_INFO)DecodeATK_INFO( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeABYSS_SEND_REWARD_MAIL(CMessage msg,ref CNetData poNetData)
{
	ABYSS_SEND_REWARD_MAIL pstIn = (ABYSS_SEND_REWARD_MAIL)msg;

	if(-1 == poNetData.Add(pstIn.m_TmpID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Rolename))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Status))
		return -1;

		EncodeATK_INFO_LIST(pstIn.m_AtkInfoList,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeABYSS_SEND_REWARD_MAIL(ref CNetData poNetData)
{
	ABYSS_SEND_REWARD_MAIL pstOut = new ABYSS_SEND_REWARD_MAIL();

	if(-1 == poNetData.Del(ref pstOut.m_TmpID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Rolename))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Status))
		return null;

	pstOut.m_AtkInfoList	= (ATK_INFO_LIST)DecodeATK_INFO_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeSMailDetail(CMessage msg,ref CNetData poNetData)
{
	SMailDetail pstIn = (SMailDetail)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UniqueID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SendTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ReadTime))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Sender))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Receiver))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Title))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Content))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if ((int)pstIn.m_ItemCount > (int)MAX_MAIL_ITEM_COUNT || (int)pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_MAIL_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		EncodeSXITEM(pstIn.m_Items[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMailDetail(ref CNetData poNetData)
{
	SMailDetail pstOut = new SMailDetail();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UniqueID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SendTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ReadTime))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Sender))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Receiver))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Title))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Content))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)MAX_MAIL_ITEM_COUNT || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)MAX_MAIL_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		pstOut.m_Items[i]	= (SXITEM)DecodeSXITEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeMAIL_LIST(CMessage msg,ref CNetData poNetData)
{
	MAIL_LIST pstIn = (MAIL_LIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_MAIL_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_MAIL_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSMailDetail(pstIn.m_Mails[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeMAIL_LIST(ref CNetData poNetData)
{
	MAIL_LIST pstOut = new MAIL_LIST();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_MAIL_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_MAIL_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Mails[i]	= (SMailDetail)DecodeSMailDetail( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSEND_MAIL(CMessage msg,ref CNetData poNetData)
{
	SEND_MAIL pstIn = (SEND_MAIL)msg;

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Receiver))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Title))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Content))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if((int)pstIn.m_ItemCount > MAX_MAIL_ITEM_COUNT || pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_MAIL_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_Items[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSEND_MAIL(ref CNetData poNetData)
{
	SEND_MAIL pstOut = new SEND_MAIL();

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Receiver))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Title))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Content))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)MAX_MAIL_ITEM_COUNT || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)MAX_MAIL_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_Items[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeMAIL_ID_LIST(CMessage msg,ref CNetData poNetData)
{
	MAIL_ID_LIST pstIn = (MAIL_ID_LIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > MAX_MAIL_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_MAIL_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_MailIDs[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeMAIL_ID_LIST(ref CNetData poNetData)
{
	MAIL_ID_LIST pstOut = new MAIL_ID_LIST();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_MAIL_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_MAIL_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_MailIDs[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeSPVPRoleAllData(CMessage msg,ref CNetData poNetData)
{
	SPVPRoleAllData pstIn = (SPVPRoleAllData)msg;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Digitid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_RoleName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MiLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Repute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Ranks))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairColor))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSPVPRoleAllData(ref CNetData poNetData)
{
	SPVPRoleAllData pstOut = new SPVPRoleAllData();

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Digitid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_RoleName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MiLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Repute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Ranks))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairColor))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	return pstOut;
}

public static	int		EncodePVP_MATCH(CMessage msg,ref CNetData poNetData)
{
	PVP_MATCH pstIn = (PVP_MATCH)msg;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_RoleName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MilitaryLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairColor))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodePVP_MATCH(ref CNetData poNetData)
{
	PVP_MATCH pstOut = new PVP_MATCH();

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_RoleName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MilitaryLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairColor))
		return null;

	return pstOut;
}

public static	int		EncodePVP_MATCH_LIST(CMessage msg,ref CNetData poNetData)
{
	PVP_MATCH_LIST pstIn = (PVP_MATCH_LIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_PVP_MATCH_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_PVP_MATCH_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodePVP_MATCH(pstIn.m_PVPMatchList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodePVP_MATCH_LIST(ref CNetData poNetData)
{
	PVP_MATCH_LIST pstOut = new PVP_MATCH_LIST();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_PVP_MATCH_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_PVP_MATCH_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_PVPMatchList[i]	= (PVP_MATCH)DecodePVP_MATCH( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodePVP_RANK(CMessage msg,ref CNetData poNetData)
{
	PVP_RANK pstIn = (PVP_RANK)msg;

	if(-1 == poNetData.Add(pstIn.m_Roleid))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_RoleName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MilitaryLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Rank))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairColor))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodePVP_RANK(ref CNetData poNetData)
{
	PVP_RANK pstOut = new PVP_RANK();

	if(-1 == poNetData.Del(ref pstOut.m_Roleid))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_RoleName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MilitaryLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Rank))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairColor))
		return null;

	return pstOut;
}

public static	int		EncodePVP_RANK_LIST(CMessage msg,ref CNetData poNetData)
{
	PVP_RANK_LIST pstIn = (PVP_RANK_LIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_PVP_RANK_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_PVP_RANK_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodePVP_RANK(pstIn.m_PVPRankList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodePVP_RANK_LIST(ref CNetData poNetData)
{
	PVP_RANK_LIST pstOut = new PVP_RANK_LIST();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_PVP_RANK_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_PVP_RANK_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_PVPRankList[i]	= (PVP_RANK)DecodePVP_RANK( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRobotPet(CMessage msg,ref CNetData poNetData)
{
	SRobotPet pstIn = (SRobotPet)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobotPet(ref CNetData poNetData)
{
	SRobotPet pstOut = new SRobotPet();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	return pstOut;
}

public static	int		EncodeSRobotPets(CMessage msg,ref CNetData poNetData)
{
	SRobotPets pstIn = (SRobotPets)msg;

	if(-1 == poNetData.Add(pstIn.m_PetCount))
		return -1;

	int i;
	if ((int)pstIn.m_PetCount > (int)EPetBattlePos.PET_BTPOS_TEAM || (int)pstIn.m_PetCount < 0)
       return -1;
	for(i = 0; i < (int)EPetBattlePos.PET_BTPOS_TEAM; ++i)
	{
		if(i >= (int)pstIn.m_PetCount)
			break;
		EncodeSRobotPet(pstIn.m_PetsInfo[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_PetLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PetUp))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobotPets(ref CNetData poNetData)
{
	SRobotPets pstOut = new SRobotPets();

	if(-1 == poNetData.Del(ref pstOut.m_PetCount))
		return null;

	int i;
	if ((int)pstOut.m_PetCount > (int)EPetBattlePos.PET_BTPOS_TEAM || (int)pstOut.m_PetCount < 0)
       return null;
	for(i = 0; i < (int)EPetBattlePos.PET_BTPOS_TEAM; ++i)
	{
		if(i >= (int)pstOut.m_PetCount)
			break;
		pstOut.m_PetsInfo[i]	= (SRobotPet)DecodeSRobotPet( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_PetLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PetUp))
		return null;

	return pstOut;
}

public static	int		EncodeSRobosEquip(CMessage msg,ref CNetData poNetData)
{
	SRobosEquip pstIn = (SRobosEquip)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobosEquip(ref CNetData poNetData)
{
	SRobosEquip pstOut = new SRobosEquip();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	return pstOut;
}

public static	int		EncodeSRobosEquips(CMessage msg,ref CNetData poNetData)
{
	SRobosEquips pstIn = (SRobosEquips)msg;

	if(-1 == poNetData.Add(pstIn.m_EquipCount))
		return -1;

	int i;
	if ((int)pstIn.m_EquipCount > (int)MAX_EQUIP_ITEM_NUM || (int)pstIn.m_EquipCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_EQUIP_ITEM_NUM; ++i)
	{
		if(i >= (int)pstIn.m_EquipCount)
			break;
		EncodeSRobosEquip(pstIn.m_EquipInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobosEquips(ref CNetData poNetData)
{
	SRobosEquips pstOut = new SRobosEquips();

	if(-1 == poNetData.Del(ref pstOut.m_EquipCount))
		return null;

	int i;
	if ((int)pstOut.m_EquipCount > (int)MAX_EQUIP_ITEM_NUM || (int)pstOut.m_EquipCount < 0)
       return null;
	for(i = 0; i < (int)MAX_EQUIP_ITEM_NUM; ++i)
	{
		if(i >= (int)pstOut.m_EquipCount)
			break;
		pstOut.m_EquipInfo[i]	= (SRobosEquip)DecodeSRobosEquip( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSRobotBuff(CMessage msg,ref CNetData poNetData)
{
	SRobotBuff pstIn = (SRobotBuff)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobotBuff(ref CNetData poNetData)
{
	SRobotBuff pstOut = new SRobotBuff();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateId))
		return null;

	return pstOut;
}

public static	int		EncodeSRobotBuffs(CMessage msg,ref CNetData poNetData)
{
	SRobotBuffs pstIn = (SRobotBuffs)msg;

	if(-1 == poNetData.Add(pstIn.m_BuffCount))
		return -1;

	int i;
	if ((int)pstIn.m_BuffCount > (int)MAX_PVP_ROBOT_BUFF_COUNT || (int)pstIn.m_BuffCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_PVP_ROBOT_BUFF_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_BuffCount)
			break;
		EncodeSRobotBuff(pstIn.m_BuffInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRobotBuffs(ref CNetData poNetData)
{
	SRobotBuffs pstOut = new SRobotBuffs();

	if(-1 == poNetData.Del(ref pstOut.m_BuffCount))
		return null;

	int i;
	if ((int)pstOut.m_BuffCount > (int)MAX_PVP_ROBOT_BUFF_COUNT || (int)pstOut.m_BuffCount < 0)
       return null;
	for(i = 0; i < (int)MAX_PVP_ROBOT_BUFF_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_BuffCount)
			break;
		pstOut.m_BuffInfo[i]	= (SRobotBuff)DecodeSRobotBuff( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSPVPRobot(CMessage msg,ref CNetData poNetData)
{
	SPVPRobot pstIn = (SPVPRobot)msg;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DGID))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Quality))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Repute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Militery))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairStyle))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HairColor))
		return -1;

		EncodeSRobosEquips(pstIn.m_Equips,ref poNetData);
		EncodeSRobotPets(pstIn.m_Pets,ref poNetData);
		EncodeSRobotBuffs(pstIn.m_Buffs,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSPVPRobot(ref CNetData poNetData)
{
	SPVPRobot pstOut = new SPVPRobot();

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DGID))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Quality))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Repute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Militery))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairStyle))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HairColor))
		return null;

	pstOut.m_Equips	= (SRobosEquips)DecodeSRobosEquips(ref poNetData);
	pstOut.m_Pets	= (SRobotPets)DecodeSRobotPets(ref poNetData);
	pstOut.m_Buffs	= (SRobotBuffs)DecodeSRobotBuffs(ref poNetData);
	return pstOut;
}

public static	int		EncodeSSeverPVPBattleInfo(CMessage msg,ref CNetData poNetData)
{
	SSeverPVPBattleInfo pstIn = (SSeverPVPBattleInfo)msg;

		EncodeSPVPRoleAllData(pstIn.m_PVPProperty,ref poNetData);
	if(-1 == poNetData.AddString(pstIn.m_RoleAllInfo))
		return -1;

		EncodeSPVPRobot(pstIn.m_PVPRobot,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSSeverPVPBattleInfo(ref CNetData poNetData)
{
	SSeverPVPBattleInfo pstOut = new SSeverPVPBattleInfo();

	pstOut.m_PVPProperty	= (SPVPRoleAllData)DecodeSPVPRoleAllData(ref poNetData);
	if(-1 == poNetData.DelString(ref pstOut.m_RoleAllInfo))
		return null;

	pstOut.m_PVPRobot	= (SPVPRobot)DecodeSPVPRobot(ref poNetData);
	return pstOut;
}

public static	int		EncodeWBAtkInfo(CMessage msg,ref CNetData poNetData)
{
	WBAtkInfo pstIn = (WBAtkInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Id))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Nm))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Dmg))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Buf))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BufId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Rank))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeWBAtkInfo(ref CNetData poNetData)
{
	WBAtkInfo pstOut = new WBAtkInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Id))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Nm))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Dmg))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Buf))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BufId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Rank))
		return null;

	return pstOut;
}

public static	int		EncodeWBBossBuff(CMessage msg,ref CNetData poNetData)
{
	WBBossBuff pstIn = (WBBossBuff)msg;

	if(-1 == poNetData.Add(pstIn.m_Percent))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuffID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeWBBossBuff(ref CNetData poNetData)
{
	WBBossBuff pstOut = new WBBossBuff();

	if(-1 == poNetData.Del(ref pstOut.m_Percent))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuffID))
		return null;

	return pstOut;
}

public static	int		EncodeSWorldBossInfo(CMessage msg,ref CNetData poNetData)
{
	SWorldBossInfo pstIn = (SWorldBossInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_WBDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_WBTmpId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TotalNum))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Blood))
		return -1;

		EncodeWBBossBuff(pstIn.m_BossBuff,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Atkcount))
		return -1;

	int i;
	for(i = 0; i < (int)MAX_WB_RANK_COUNT; ++i)
	{
		EncodeWBAtkInfo(pstIn.m_Atklist[i],ref poNetData);
	}

		EncodeWBAtkInfo(pstIn.m_AtkInfo,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Status))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_StartTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EndTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Killtime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ReliveTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NextTime))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSWorldBossInfo(ref CNetData poNetData)
{
	SWorldBossInfo pstOut = new SWorldBossInfo();

	if(-1 == poNetData.Del(ref pstOut.m_WBDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_WBTmpId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TotalNum))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Blood))
		return null;

	pstOut.m_BossBuff	= (WBBossBuff)DecodeWBBossBuff(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Atkcount))
		return null;

	int i;
	for(i = 0; i < (int)MAX_WB_RANK_COUNT; ++i)
	{
		pstOut.m_Atklist[i] = (WBAtkInfo)DecodeWBAtkInfo(ref poNetData);
	}

	pstOut.m_AtkInfo	= (WBAtkInfo)DecodeWBAtkInfo(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Status))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_StartTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EndTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Killtime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ReliveTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NextTime))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	return pstOut;
}

public static	int		EncodeSRankRoleReq(CMessage msg,ref CNetData poNetData)
{
	SRankRoleReq pstIn = (SRankRoleReq)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RankType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Page))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRankRoleReq(ref CNetData poNetData)
{
	SRankRoleReq pstOut = new SRankRoleReq();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RankType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Page))
		return null;

	return pstOut;
}

public static	int		EncodeSRankRole(CMessage msg,ref CNetData poNetData)
{
	SRankRole pstIn = (SRankRole)msg;

		EncodeSRoleRsServerInfo(pstIn.m_RoleInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRankRole(ref CNetData poNetData)
{
	SRankRole pstOut = new SRankRole();

	pstOut.m_RoleInfo	= (SRoleRsServerInfo)DecodeSRoleRsServerInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSRankRoleAck(CMessage msg,ref CNetData poNetData)
{
	SRankRoleAck pstIn = (SRankRoleAck)msg;

		EncodeSRankRoleReq(pstIn.m_Req,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_TotalCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SelfRank))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RankCount))
		return -1;

	int i;
	if ((int)pstIn.m_RankCount > (int)MAX_RANK_PAGE_COUNT || (int)pstIn.m_RankCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_RANK_PAGE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_RankCount)
			break;
		EncodeSRankRole(pstIn.m_Rank[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSRankRoleAck(ref CNetData poNetData)
{
	SRankRoleAck pstOut = new SRankRoleAck();

	pstOut.m_Req	= (SRankRoleReq)DecodeSRankRoleReq(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_TotalCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SelfRank))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RankCount))
		return null;

	int i;
	if ((int)pstOut.m_RankCount > (int)MAX_RANK_PAGE_COUNT || (int)pstOut.m_RankCount < 0)
       return null;
	for(i = 0; i < (int)MAX_RANK_PAGE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_RankCount)
			break;
		pstOut.m_Rank[i]	= (SRankRole)DecodeSRankRole( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGuildListReq(CMessage msg,ref CNetData poNetData)
{
	SGuildListReq pstIn = (SGuildListReq)msg;

	if(-1 == poNetData.Add(pstIn.m_Page))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildListReq(ref CNetData poNetData)
{
	SGuildListReq pstOut = new SGuildListReq();

	if(-1 == poNetData.Del(ref pstOut.m_Page))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildListInfo(CMessage msg,ref CNetData poNetData)
{
	SGuildListInfo pstIn = (SGuildListInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_GuildId))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_GuildName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GuildLv))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_PresidentName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberMax))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildListInfo(ref CNetData poNetData)
{
	SGuildListInfo pstOut = new SGuildListInfo();

	if(-1 == poNetData.Del(ref pstOut.m_GuildId))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_GuildName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GuildLv))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_PresidentName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberMax))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildListAck(CMessage msg,ref CNetData poNetData)
{
	SGuildListAck pstIn = (SGuildListAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_GUILD_LIST_PER_PAGE || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LIST_PER_PAGE; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSGuildListInfo(pstIn.m_Info[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildListAck(ref CNetData poNetData)
{
	SGuildListAck pstOut = new SGuildListAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_GUILD_LIST_PER_PAGE || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LIST_PER_PAGE; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Info[i]	= (SGuildListInfo)DecodeSGuildListInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGuildEnterReq(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterReq pstIn = (SGuildEnterReq)msg;

	if(-1 == poNetData.Add(pstIn.m_GuildId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterReq(ref CNetData poNetData)
{
	SGuildEnterReq pstOut = new SGuildEnterReq();

	if(-1 == poNetData.Del(ref pstOut.m_GuildId))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildEnterAck(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterAck pstIn = (SGuildEnterAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GuildId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterAck(ref CNetData poNetData)
{
	SGuildEnterAck pstOut = new SGuildEnterAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GuildId))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLeaveAck(CMessage msg,ref CNetData poNetData)
{
	SGuildLeaveAck pstIn = (SGuildLeaveAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLeaveAck(ref CNetData poNetData)
{
	SGuildLeaveAck pstOut = new SGuildLeaveAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildCreateReq(CMessage msg,ref CNetData poNetData)
{
	SGuildCreateReq pstIn = (SGuildCreateReq)msg;

	if(-1 == poNetData.AddString(pstIn.m_GuildName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_GuildAim))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CostType))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildCreateReq(ref CNetData poNetData)
{
	SGuildCreateReq pstOut = new SGuildCreateReq();

	if(-1 == poNetData.DelString(ref pstOut.m_GuildName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_GuildAim))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CostType))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildCreateAck(CMessage msg,ref CNetData poNetData)
{
	SGuildCreateAck pstIn = (SGuildCreateAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildCreateAck(ref CNetData poNetData)
{
	SGuildCreateAck pstOut = new SGuildCreateAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildBaseInfo(CMessage msg,ref CNetData poNetData)
{
	SGuildBaseInfo pstIn = (SGuildBaseInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_GuildId))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_GuildName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_GuildNotice))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberMax))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MyContribution))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MyContributionMax))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildBaseInfo(ref CNetData poNetData)
{
	SGuildBaseInfo pstOut = new SGuildBaseInfo();

	if(-1 == poNetData.Del(ref pstOut.m_GuildId))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_GuildName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_GuildNotice))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberMax))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MyContribution))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MyContributionMax))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_MemberEnter(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_MemberEnter pstIn = (SGuildLog_MemberEnter)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_MemberName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_MemberEnter(ref CNetData poNetData)
{
	SGuildLog_MemberEnter pstOut = new SGuildLog_MemberEnter();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_MemberName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_MemberKick(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_MemberKick pstIn = (SGuildLog_MemberKick)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_MemberName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_KickName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_MemberKick(ref CNetData poNetData)
{
	SGuildLog_MemberKick pstOut = new SGuildLog_MemberKick();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_MemberName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_KickName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_PotitionChange(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_PotitionChange pstIn = (SGuildLog_PotitionChange)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_MemberName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_PositionChangeName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NewPosition))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_PotitionChange(ref CNetData poNetData)
{
	SGuildLog_PotitionChange pstOut = new SGuildLog_PotitionChange();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_MemberName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_PositionChangeName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NewPosition))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_PresidentChange(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_PresidentChange pstIn = (SGuildLog_PresidentChange)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_FromName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ToName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_PresidentChange(ref CNetData poNetData)
{
	SGuildLog_PresidentChange pstOut = new SGuildLog_PresidentChange();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_FromName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ToName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_PresidentImpeach(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_PresidentImpeach pstIn = (SGuildLog_PresidentImpeach)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_FromName))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ToName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_PresidentImpeach(ref CNetData poNetData)
{
	SGuildLog_PresidentImpeach pstOut = new SGuildLog_PresidentImpeach();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_FromName))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ToName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_LevelUp(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_LevelUp pstIn = (SGuildLog_LevelUp)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FromLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ToLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_LevelUp(ref CNetData poNetData)
{
	SGuildLog_LevelUp pstOut = new SGuildLog_LevelUp();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FromLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ToLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_TechnologyUp(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_TechnologyUp pstIn = (SGuildLog_TechnologyUp)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TechnologyType))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_OpName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FromLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ToLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_TechnologyUp(ref CNetData poNetData)
{
	SGuildLog_TechnologyUp pstOut = new SGuildLog_TechnologyUp();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TechnologyType))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_OpName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FromLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ToLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_Pray(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_Pray pstIn = (SGuildLog_Pray)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PrayType))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_OpName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_Pray(ref CNetData poNetData)
{
	SGuildLog_Pray pstOut = new SGuildLog_Pray();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PrayType))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_OpName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_NoticeChange(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_NoticeChange pstIn = (SGuildLog_NoticeChange)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_OpName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_NoticeChange(ref CNetData poNetData)
{
	SGuildLog_NoticeChange pstOut = new SGuildLog_NoticeChange();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_OpName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLog_AimChange(CMessage msg,ref CNetData poNetData)
{
	SGuildLog_AimChange pstIn = (SGuildLog_AimChange)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_OpName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLog_AimChange(ref CNetData poNetData)
{
	SGuildLog_AimChange pstOut = new SGuildLog_AimChange();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_OpName))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildLogAck(CMessage msg,ref CNetData poNetData)
{
	SGuildLogAck pstIn = (SGuildLogAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count11))
		return -1;

	int i;
	if ((int)pstIn.m_Count11 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count11 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count11)
			break;
		EncodeSGuildLog_MemberEnter(pstIn.m_MemberEnter[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count12))
		return -1;

	if ((int)pstIn.m_Count12 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count12 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count12)
			break;
		EncodeSGuildLog_MemberKick(pstIn.m_MemberKick[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count13))
		return -1;

	if ((int)pstIn.m_Count13 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count13 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count13)
			break;
		EncodeSGuildLog_PotitionChange(pstIn.m_PotitionChange[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count14))
		return -1;

	if ((int)pstIn.m_Count14 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count14 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count14)
			break;
		EncodeSGuildLog_PresidentChange(pstIn.m_PresidentChange[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count15))
		return -1;

	if ((int)pstIn.m_Count15 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count15 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count15)
			break;
		EncodeSGuildLog_PresidentImpeach(pstIn.m_PresidentImpeach[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count20))
		return -1;

	if ((int)pstIn.m_Count20 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count20 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count20)
			break;
		EncodeSGuildLog_LevelUp(pstIn.m_LevelUp[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count30))
		return -1;

	if ((int)pstIn.m_Count30 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count30 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count30)
			break;
		EncodeSGuildLog_TechnologyUp(pstIn.m_TechnologyUp[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count40))
		return -1;

	if ((int)pstIn.m_Count40 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count40 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count40)
			break;
		EncodeSGuildLog_Pray(pstIn.m_Pray[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count50))
		return -1;

	if ((int)pstIn.m_Count50 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count50 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count50)
			break;
		EncodeSGuildLog_NoticeChange(pstIn.m_NoticeChange[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Count60))
		return -1;

	if ((int)pstIn.m_Count60 > (int)MAX_GUILD_LOG_COUNT || (int)pstIn.m_Count60 < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count60)
			break;
		EncodeSGuildLog_AimChange(pstIn.m_AimChange[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildLogAck(ref CNetData poNetData)
{
	SGuildLogAck pstOut = new SGuildLogAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count11))
		return null;

	int i;
	if ((int)pstOut.m_Count11 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count11 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count11)
			break;
		pstOut.m_MemberEnter[i]	= (SGuildLog_MemberEnter)DecodeSGuildLog_MemberEnter( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count12))
		return null;

	if ((int)pstOut.m_Count12 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count12 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count12)
			break;
		pstOut.m_MemberKick[i]	= (SGuildLog_MemberKick)DecodeSGuildLog_MemberKick( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count13))
		return null;

	if ((int)pstOut.m_Count13 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count13 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count13)
			break;
		pstOut.m_PotitionChange[i]	= (SGuildLog_PotitionChange)DecodeSGuildLog_PotitionChange( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count14))
		return null;

	if ((int)pstOut.m_Count14 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count14 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count14)
			break;
		pstOut.m_PresidentChange[i]	= (SGuildLog_PresidentChange)DecodeSGuildLog_PresidentChange( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count15))
		return null;

	if ((int)pstOut.m_Count15 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count15 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count15)
			break;
		pstOut.m_PresidentImpeach[i]	= (SGuildLog_PresidentImpeach)DecodeSGuildLog_PresidentImpeach( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count20))
		return null;

	if ((int)pstOut.m_Count20 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count20 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count20)
			break;
		pstOut.m_LevelUp[i]	= (SGuildLog_LevelUp)DecodeSGuildLog_LevelUp( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count30))
		return null;

	if ((int)pstOut.m_Count30 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count30 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count30)
			break;
		pstOut.m_TechnologyUp[i]	= (SGuildLog_TechnologyUp)DecodeSGuildLog_TechnologyUp( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count40))
		return null;

	if ((int)pstOut.m_Count40 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count40 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count40)
			break;
		pstOut.m_Pray[i]	= (SGuildLog_Pray)DecodeSGuildLog_Pray( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count50))
		return null;

	if ((int)pstOut.m_Count50 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count50 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count50)
			break;
		pstOut.m_NoticeChange[i]	= (SGuildLog_NoticeChange)DecodeSGuildLog_NoticeChange( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Count60))
		return null;

	if ((int)pstOut.m_Count60 > (int)MAX_GUILD_LOG_COUNT || (int)pstOut.m_Count60 < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_LOG_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count60)
			break;
		pstOut.m_AimChange[i]	= (SGuildLog_AimChange)DecodeSGuildLog_AimChange( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGuildChangeNoticeReq(CMessage msg,ref CNetData poNetData)
{
	SGuildChangeNoticeReq pstIn = (SGuildChangeNoticeReq)msg;

	if(-1 == poNetData.AddString(pstIn.m_NewNotice))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildChangeNoticeReq(ref CNetData poNetData)
{
	SGuildChangeNoticeReq pstOut = new SGuildChangeNoticeReq();

	if(-1 == poNetData.DelString(ref pstOut.m_NewNotice))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildChangeNoticeAck(CMessage msg,ref CNetData poNetData)
{
	SGuildChangeNoticeAck pstIn = (SGuildChangeNoticeAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildChangeNoticeAck(ref CNetData poNetData)
{
	SGuildChangeNoticeAck pstOut = new SGuildChangeNoticeAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildMemberListInfo(CMessage msg,ref CNetData poNetData)
{
	SGuildMemberListInfo pstIn = (SGuildMemberListInfo)msg;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Position))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Contribution))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OfflineTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildMemberListInfo(ref CNetData poNetData)
{
	SGuildMemberListInfo pstOut = new SGuildMemberListInfo();

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Position))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Contribution))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OfflineTime))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildDetailAck(CMessage msg,ref CNetData poNetData)
{
	SGuildDetailAck pstIn = (SGuildDetailAck)msg;

	if(-1 == poNetData.AddString(pstIn.m_GuildName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GuildId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GuildLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MemberMax))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GuildAttack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Reputation))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_GuildAim))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_GUILD_MEMBER_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_MEMBER_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSGuildMemberListInfo(pstIn.m_MemberList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildDetailAck(ref CNetData poNetData)
{
	SGuildDetailAck pstOut = new SGuildDetailAck();

	if(-1 == poNetData.DelString(ref pstOut.m_GuildName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GuildId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GuildLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MemberMax))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GuildAttack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Reputation))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_GuildAim))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_GUILD_MEMBER_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_MEMBER_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_MemberList[i]	= (SGuildMemberListInfo)DecodeSGuildMemberListInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGuildEnterInfo(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterInfo pstIn = (SGuildEnterInfo)msg;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterInfo(ref CNetData poNetData)
{
	SGuildEnterInfo pstOut = new SGuildEnterInfo();

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildEnterListAck(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterListAck pstIn = (SGuildEnterListAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_GUILD_ENTER_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_GUILD_ENTER_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSGuildEnterInfo(pstIn.m_EnterList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterListAck(ref CNetData poNetData)
{
	SGuildEnterListAck pstOut = new SGuildEnterListAck();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_GUILD_ENTER_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_GUILD_ENTER_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_EnterList[i]	= (SGuildEnterInfo)DecodeSGuildEnterInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSGuildEnterAcceptReq(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterAcceptReq pstIn = (SGuildEnterAcceptReq)msg;

	if(-1 == poNetData.Add(pstIn.m_Accept))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterAcceptReq(ref CNetData poNetData)
{
	SGuildEnterAcceptReq pstOut = new SGuildEnterAcceptReq();

	if(-1 == poNetData.Del(ref pstOut.m_Accept))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildEnterAcceptAck(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterAcceptAck pstIn = (SGuildEnterAcceptAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterAcceptAck(ref CNetData poNetData)
{
	SGuildEnterAcceptAck pstOut = new SGuildEnterAcceptAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	return pstOut;
}

public static	int		EncodeSGuildEnterClearAck(CMessage msg,ref CNetData poNetData)
{
	SGuildEnterClearAck pstIn = (SGuildEnterClearAck)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGuildEnterClearAck(ref CNetData poNetData)
{
	SGuildEnterClearAck pstOut = new SGuildEnterClearAck();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

}
