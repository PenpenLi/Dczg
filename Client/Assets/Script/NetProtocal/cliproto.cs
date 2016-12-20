using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

//      Version Number: 1          
public class CliProto{
//Defines Begin
public const int LG_SUCCESS             =0;
public const int LG_FAIL                =1;
public const int LG_INTER               =2;
public const int MAX_GATHER_ITEM_COUNT  =500;
public const int LG_USERNAME_LEN        =64;
public const int LG_DIGITID_LEN         =21;
public const int LG_SESSIONID_LEN       =8;
public const int LG_SIG_LEN             =32;
public const int LG_TYPE_LEN            =21;
public const int LG_MAX_GAMESERVER      =255;
public const int LG_CLIENTKEY_LEN       =8;
public const int LG_INFO_LEN            =255;
public const int CGPROTO_BEGIN          =1;
public const int CSPROTO_BEGIN          =2000;
public const int MAX_DYNCODE_LEN        =20480;
public const int GS_STATE_ONLINE        =1;
public const int GS_STATE_OFFLINE       =0;
public const int GS_LOGOUT_EXIT         =1;
public const int GS_LOGOUT_QUIT         =2;
public const int GS_LOGOUT_SWITCH_SCENE =3;
public const int GC_MAX_GATESERVER      =255;
public const int MAX_PATH_LEN           =260;
public const int MAX_MONSTER_IN_LEVEL   =100;
public const int MAX_MONSTER_DROP       =5;
public const int MAX_LEVEL_DROP         =50;
public const int MAX_LEVEL_SKILL        =100;
public const int MAX_ERROR_STRING_LEN   =1024;
public const int GS_MAX_SYNC_ROLE_COUNT =100;
public const int MAX_INITIAL_BUFF_COUNT =16;
public const	int CLIPROTO_MSGID_RANGE =4227;
public const	int CLIPROTO_MSGID_OFFSET =1;
//Defines End


/*Enums Begin*/
public enum EMoveSyncType
{
	MOVE_SYNC_TYPE_Normal       = 0,
	MOVE_SYNC_TYPE_NoOther,
	MOVE_SYNC_TYPE_ReducedOther,
}

/*Enums End*/


public	enum EN_CliProto_MessageID
{
	CGID_LOGIN                           = 1,
	GCID_LOGIN                           = 2,
	CGID_LOGOUT                          = 3,
	GCID_LOGOUT                          = 4,
	GCID_ROLELIST                        = 5,
	CGID_CREATEROLE                      = 6,
	GCID_CREATEROLE                      = 7,
	CGID_DELROLE                         = 8,
	GCID_DELROLE                         = 9,
	CGID_SELECTROLE                      = 14,
	GCID_SELECTROLE                      = 15,
	GCID_QUEUE_INFO                      = 21,
	GCID_GPK_DYN_CODE                    = 26,
	CGID_GPK_NEGLECT                     = 28,
	GCID_GPK_CAL_ORIGIN                  = 30,
	CGID_GPK_CAL_RESULT                  = 32,
	GCID_ERROR                           = 100,
	GCID_ERRORSTRING                     = 101,
	CGID_HEART_BEAT                      = 102,
	GCID_HEART_BEAT                      = 103,
	GCID_PUBLIC_STRING                   = 104,
	LGID_LOGIN_REQ                       = 1001,
	LGID_LOGIN_ACK                       = 1003,
	LGID_GAMESERVER_LIST_REQ             = 1011,
	LGID_GAMESERVER_LIST_ACK             = 1012,
	GSID_SERVERINFO_RPT                  = 1100,
	GSID_SERVER_GROUP_DESC_RPT           = 1110,
	GSID_ENTERGAME_REQ                   = 1211,
	GSID_ENTERGAME_ACK                   = 1212,
	GSID_LEAVEGAME_REQ                   = 1221,
	GSID_LEAVEGAME_ACK                   = 1222,
	GSID_KICKOUT_NTF                     = 1231,
	GSID_MSG_BROADCAST                   = 1232,
	GSID_LOAD_GSDATA_NTF                 = 1242,
	GSID_UPDATEGMLEVEL_NTF               = 1246,
	GSID_SWITCHSCENE_REQ                 = 1247,
	GSID_SWITCHSCENE_ACK                 = 1248,
	SCID_SELF_LOADSCENE                  = 2000,
	CSID_ENTERSCENE                      = 2001,
	SCID_SELF_ENTERSCENE                 = 2002,
	SCID_SELF_DATA_END                   = 2003,
	SCID_SELF_APPEAR                     = 2004,
	SCID_SELF_BASE_PRO                   = 2005,
	CSID_LEVEL_REQ                       = 2006,
	SCID_LEVEL_ACK                       = 2007,
	CSID_LEVEL_RESULT_NTF                = 2010,
	SCID_TREASURE_CHEST_NTF              = 2011,
	CSID_OPEN_TREASURE_CHEST_REQ         = 2012,
	SCID_OPEN_TREASURE_CHEST_ACK         = 2013,
	SCID_LEVEL_INFO                      = 2014,
	CSID_GET_BATTLE_BOX_REQ              = 2015,
	SCID_GET_BATTLE_BOX_ACK              = 2016,
	SCID_BATTLE_GOT_STAR_NTF             = 2017,
	SCID_SELF_VOLATILE_PRO               = 2030,
	SCID_CLIENT_CONFIG_NTF               = 2040,
	CSID_CLIENT_CONFIG_UPDATE            = 2041,
	SCID_ERROR_NTF                       = 2050,
	SCID_SELF_ITEM_NTF                   = 2100,
	CSID_MOVE_ITEM_REQ                   = 2101,
	SCID_MOVE_ITEM_ACK                   = 2102,
	SCID_ROLE_BAG_NTF                    = 2110,
	SCID_ROLE_EQUIP_NTF                  = 2111,
	CSID_ROLE_EQUIP_REQ                  = 2112,
	CSID_ROLE_EVENT_REQ                  = 2113,
	SCID_ROLE_BAG_SINGLE_LEAVE_NTF       = 2114,
	SCID_ROLE_BAG_SINGLE_ENTER_NTF       = 2115,
	SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF     = 2116,
	SCID_ROLE_EQUIP_SINGLE_ENTER_NTF     = 2117,
	CSID_ROLE_ITEMUP_RPT                 = 2118,
	SCID_ROLE_ITEMUP_NTF                 = 2119,
	SCID_ROLE_ITEMUP_ACK                 = 2120,
	SCID_ROLE_SUIT_NTF                   = 2121,
	CSID_ROLE_HOLE_ON_REQ                = 2122,
	SCID_ROLE_HOLE_ON_ACK                = 2123,
	CSID_ROLE_HOLE_OFF_REQ               = 2124,
	SCID_ROLE_HOLE_OFF_ACK               = 2125,
	CSID_ROLE_HOLE_REFRESH_REQ           = 2126,
	SCID_ROLE_HOLE_REFRESH_ACK           = 2127,
	CSID_ROLE_HOLE_MERGE_REQ             = 2128,
	SCID_ROLE_HOLE_MERGE_ACK             = 2129,
	CSID_ROLE_HOLE_MERGE2_REQ            = 2130,
	SCID_ROLE_HOLE_MERGE2_ACK            = 2131,
	CSID_LOCK_RPT                        = 2140,
	CSID_ROLE_USEITEM_REQ                = 2141,
	CSID_PET_USEITEM_REQ                 = 2143,
	SCID_PET_USEITEM_ACK                 = 2144,
	SCID_ROLE_LIMIT_NTF                  = 2145,
	CSID_GATHER_ITEM_MERGE_REQ           = 2150,
	SCID_GATHER_ITEM_MERGE_ACK           = 2152,
	SCID_GATHER_INFO_NTF                 = 2153,
	SCID_GATHER_SINGLE_ENTER_NTF         = 2154,
	SCID_GATHER_SINGLE_LEAVE_NTF         = 2155,
	CSID_ROLE_PROD_REQ                   = 2156,
	SCID_ROLE_PROD_ACK                   = 2157,
	SCID_ROLE_USEITEM_START_ACK          = 2160,
	SCID_ROLE_USEITEM_END_ACK            = 2161,
	SCID_USER_SKILLS_NTF                 = 2200,
	CSID_LEARN_SKILL                     = 2201,
	SCID_SKILL_INFO_NTF                  = 2202,
	SCID_REMOVE_SKILL_NTF                = 2203,
	SCID_SKILL_POINT_NTF                 = 2204,
	CSID_SKILL_RESET_ALL_REQ             = 2205,
	SCID_USER_PETS_NTF                   = 2300,
	SCID_PET_INFO_NTF                    = 2301,
	CSID_SET_BATTLE_PET_REQ              = 2302,
	SCID_SET_BATTLE_PET_NTF              = 2303,
	CSID_PET_GET_EXP_RPT                 = 2310,
	SCID_PET_LEVEL_NTF                   = 2311,
	CSID_PET_UP_REQ                      = 2312,
	SCID_PET_UP_ACK                      = 2313,
	CSID_PET_MERGE_REQ                   = 2314,
	SCID_PET_ADD_NTF                     = 2315,
	SCID_PETS_RECORD_NTF                 = 2316,
	SCID_PET_BAG_NTF                     = 2317,
	CSID_PET_EQUIP_REQ                   = 2330,
	CSID_PET_EVENT_REQ                   = 2331,
	SCID_PET_BAG_SINGLE_LEAVE_NTF        = 2332,
	SCID_PET_BAG_SINGLE_ENTER_NTF        = 2333,
	CSID_PET_TEAM_RPT                    = 2334,
	SCID_PET_TEAM_NTF                    = 2335,
	SCID_PET_GET_EXP_ACK                 = 2336,
	SCID_PET_SINGLE_ENTER_NTF            = 2337,
	SCID_PET_SINGLE_LEAVE_NTF            = 2338,
	SCID_PET_EVENT_ACK                   = 2350,
	CSID_MOVE_SYNC_MODE                  = 2400,
	CSID_MOVE_BEGIN                      = 2401,
	CSID_MOVE_STOP                       = 2402,
	SCID_MOVE_BEGIN                      = 2403,
	SCID_MOVE_STOP                       = 2404,
	SCID_OTHER_APPEAR                    = 2405,
	SCID_OTHER_UPDATE                    = 2406,
	SCID_OTHER_DISAPPEAR                 = 2407,
	CSID_CHAT                            = 2600,
	SCID_CHAT                            = 2601,
	SCID_CHAT_ACK                        = 2602,
	SCID_FRIENDS_LIST_NTF                = 2800,
	CSID_QUERY_ROLE_REQ                  = 2801,
	SCID_QUERY_ROLE_ACK                  = 2802,
	CSID_ADD_FRIEND_REQ                  = 2803,
	SCID_ADD_FRIEND_ACK                  = 2804,
	CSID_REMOVE_FRIEND_REQ               = 2805,
	SCID_REMOVE_FRIEND_ACK               = 2806,
	CSID_DONATE_ACTION_POINT_REQ         = 2807,
	SCID_DONATE_ACTION_POINT_ACK         = 2808,
	SCID_DONATE_ACTION_POINT_OTHER_NTF   = 2809,
	CSID_RECEIVE_ACTION_POINT_REQ        = 2810,
	SCID_RECEIVE_ACTION_POINT_ACK        = 2811,
	CSID_RESPONSE_ADD_FRIEND_REQ         = 2812,
	SCID_REQUEST_ADD_FRIEND_NTF          = 2813,
	SCID_FRIEND_INFO_CHANGE_NTF          = 2814,
	SCID_FRIEND_REMOVE_INVITE_NTF        = 2815,
	CSID_FRIEND_ASSIST_LIST_REQ          = 2816,
	SCID_FRIEND_ASSIST_LIST_ACK          = 2817,
	CSID_SELECT_FRIEND_ASSIST_REQ        = 2818,
	SCID_SELECT_FRIEND_ASSIST_ACK        = 2819,
	SCID_FRIEND_EPINFO_NTF               = 2820,
	CSID_FRIENDS_LIST_REQ                = 2821,
	SCID_ABYSS_TRIGGER_ACK               = 3000,
	CSID_ABYSS_OPEN_REQ                  = 3001,
	SCID_ABYSS_OPEN_ACK                  = 3002,
	SCID_ABYSS_OPEN_NTF                  = 3003,
	CSID_GET_ABYSS_TRIGGER_LIST_REQ      = 3004,
	SCID_GET_ABYSS_TRIGGER_LIST_ACK      = 3005,
	CSID_GET_ABYSS_OPEN_LIST_REQ         = 3006,
	SCID_GET_ABYSS_OPEN_LIST_ACK         = 3007,
	CSID_GET_ABYSS_OPEN_REC_REQ          = 3008,
	SCID_GET_ABYSS_OPEN_REC_ACK          = 3009,
	SCID_ENTER_ABYSS_ACK                 = 3011,
	SCID_ABYSS_RUN_NTF                   = 3016,
	SCID_MAIL_LIST_NTF                   = 3200,
	CSID_SEND_MAIL_REQ                   = 3201,
	SCID_SEND_MAIL_ACK                   = 3202,
	CSID_GET_ITEM_FROM_MAIL_REQ          = 3203,
	CSID_GET_MONEY_FROM_MAIL_REQ         = 3204,
	CSID_READ_MAIL_REQ                   = 3205,
	SCID_READ_MAIL_ACK                   = 3206,
	CSID_DELETE_MAIL_REQ                 = 3207,
	SCID_DELETE_MAIL_ACK                 = 3208,
	SCID_UPDATE_MAIL_NTF                 = 3209,
	SCID_SHOP_ACTION_POINT_BOUGHT_NTF    = 3300,
	CSID_SHOP_BUY_ACTION_POINT_REQ       = 3301,
	SCID_SHOP_BUY_ACTION_POINT_ACK       = 3302,
	SCID_SHOP_BUY_PET_INFO_CHEAP_NTF     = 3310,
	SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF = 3311,
	CSID_SHOP_BUY_PET_REQ                = 3312,
	SCID_SHOP_BUY_PET_ACK                = 3313,
	CSID_SHOP_GET_GOODSLIST_REQ          = 3320,
	SCID_SHOP_GET_GOODSLIST_ACK          = 3321,
	CSID_SHOP_BUY_GOODS_REQ              = 3322,
	SCID_SHOP_BUY_GOODS_ACK              = 3323,
	CSID_SHOP_SECRET_GOODS_REQ           = 3350,
	SCID_SHOP_SECRET_GOODS_ACK           = 3351,
	CSID_SHOP_SECRET_BUY_REQ             = 3360,
	SCID_SHOP_SECRET_BUY_ACK             = 3361,
	CSID_SHOP_SECRET_REFRESH_REQ         = 3370,
	CSID_SELF_PVP_PRO_REQ                = 3400,
	SCID_SELF_PVP_PRO_ACK                = 3401,
	CSID_GET_PVP_MATCH_REQ               = 3402,
	SCID_GET_PVP_MATCH_ACK               = 3403,
	CSID_ENTER_PVP_REQ                   = 3404,
	SCID_ENTER_PVP_ACK                   = 3405,
	CSID_PVP_RETULT_REQ                  = 3406,
	SCID_PVP_RETULT_ACK                  = 3407,
	CSID_GET_RANK_LIST_REQ               = 3408,
	SCID_GET_RANK_LIST_ACK               = 3409,
	CSID_GET_PVP_MILITARY_REWARD_REQ     = 3410,
	SCID_GET_PVP_MILITARY_REWARD_ACK     = 3411,
	CSID_GET_PVP_REPUTE_REWARD_REQ       = 3412,
	SCID_GET_PVP_REPUTE_REWARD_ACK       = 3413,
	CSID_PVP_BUY_CHALLENGE_TIMES_REQ     = 3414,
	SCID_PVP_BUY_CHALLENGE_TIMES_ACK     = 3415,
	SCID_PVP_TIME_NTF                    = 3416,
	CSID_GET_PVP_ROLE_INFO_REQ           = 3417,
	SCID_GET_PVP_ROLE_INFO_ACK           = 3418,
	SCID_VIP_INFO_NTF                    = 3500,
	SCID_VIP_RMBINFO_NTF                 = 3501,
	SCID_VIP_MONTHCARD_NTF               = 3520,
	CSID_VIP_MONTHCARD_GETITEM_REQ       = 3521,
	SCID_VIP_MONTHCARD_GETITEM_ACK       = 3522,
	SCID_GIFT_SIGN_NTF                   = 3600,
	SCID_GIFT_SIGN_VIP_NTF               = 3601,
	CSID_GIFT_SIGN                       = 3602,
	CSID_GIFT_SIGN_VIP                   = 3603,
	SCID_GIFT_LEVEL_NTF                  = 3610,
	CSID_GIFT_LEVEL                      = 3611,
	SCID_GIFT_DAY_NTF                    = 3620,
	SCID_GIFT_DAY_UPD                    = 3621,
	CSID_GIFT_DAY_SCORE_REQ              = 3622,
	CSID_GIFT_DAY_BOX_REQ                = 3623,
	SCID_GIFT_DAY_BOX_NTF                = 3624,
	CSID_GIFT_JIHUOMA_REQ                = 3660,
	SCID_GIFT_JIHUOMA_ACK                = 3661,
	CSID_GIFT_EP_INFO_REQ                = 3670,
	SCID_GIFT_EP_INFO_ACK                = 3671,
	CSID_GIFT_RECEIVE_EP_REQ             = 3672,
	SCID_GIFT_RECEIVE_EP_ACK             = 3673,
	CSID_WB_INFO_REFRESH_REQ             = 3700,
	SCID_WB_INFO_REFRESH_ACK             = 3701,
	CSID_WB_ADD_BUF_REQ                  = 3702,
	SCID_WB_ADD_BUF_ACK                  = 3703,
	CSID_WB_RELIVE_REQ                   = 3704,
	SCID_WB_RELIVE_ACK                   = 3705,
	SCID_WB_KILLED_NTF                   = 3706,
	SCID_WB_RUN_NTF                      = 3707,
	CSID_WB_ENTER_REQ                    = 3708,
	SCID_WB_ENTER_ACK                    = 3709,
	CSID_WB_RESULT_REQ                   = 3710,
	SCID_WB_RESULT_ACK                   = 3711,
	CSID_ROLE_RANK_REQ                   = 3800,
	SCID_ROLE_RANK_ACK                   = 3801,
	CSID_GET_PT_BASEINFO_REQ             = 3900,
	SCID_GET_PT_BASEINFO_ACK             = 3901,
	CSID_BUY_PT_TIMES_REQ                = 3902,
	SCID_BUY_PT_TIMES_ACK                = 3903,
	CSID_GET_MD_BASEINFO_REQ             = 4000,
	SCID_GET_MD_BASEINFO_ACK             = 4001,
	CSID_BUY_MD_TIMES_REQ                = 4002,
	SCID_BUY_MD_TIMES_ACK                = 4003,
	CSID_GET_FD_BASEINFO_REQ             = 4100,
	SCID_GET_FD_BASEINFO_ACK             = 4101,
	CSID_BUY_FD_TIMES_REQ                = 4102,
	SCID_BUY_FD_TIMES_ACK                = 4103,
	CSID_GUILD_INFO_REQ                  = 4200,
	CSID_GUILD_LIST_REQ                  = 4201,
	SCID_GUILD_LIST_ACK                  = 4202,
	CSID_GUILD_ENTER_REQ                 = 4203,
	SCID_GUILD_ENTER_ACK                 = 4204,
	CSID_GUILD_CREATE_REQ                = 4205,
	SCID_GUILD_CREATE_ACK                = 4206,
	CSID_GUILD_LEAVE_REQ                 = 4207,
	SCID_GUILD_LEAVE_ACK                 = 4208,
	SCID_GUILD_BASEINFO_ACK              = 4210,
	CSID_GUILD_LOG_REQ                   = 4211,
	SCID_GUILD_LOG_ACK                   = 4212,
	CSID_GUILD_CHANGE_NOTICE_REQ         = 4213,
	CSID_GUILD_DETAIL_REQ                = 4220,
	SCID_GUILD_DETAIL_ACK                = 4221,
	CSID_GUILD_ENTERLIST_REQ             = 4222,
	SCID_GUILD_ENTERLIST_ACK             = 4223,
	CSID_GUILD_ENTER_ACCEPT_REQ          = 4224,
	SCID_GUILD_ENTER_ACCEPT_ACK          = 4225,
	CSID_GUILD_ENTER_CLEAR_REQ           = 4226,
	SCID_GUILD_ENTER_CLEAR_ACK           = 4227,
};

/*Define Structs and Unions        Begin*/

public class LGPKG_LOGIN_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.LGID_LOGIN_REQ;}
	public	byte            [] m_Type;	/// 目前只支持gplus和pt登陆方式 .
	public	uint             m_TokenLen;
	public	byte            [] m_Token;	/// G+认证方式填g+的返回串， pt认证方式填dfw12345;12345 .
	public	uint             m_ServerGroup;	/// 选择的是哪个组，提供组号 .
	public	uint             m_ResourceLevel;	/// 选择的资源包类型，平台+资源包等级，0 android， 1 ios， 2 win8 .
	public LGPKG_LOGIN_REQ()
	{
		m_Type=new byte[(int)LG_TYPE_LEN];
		m_Token=new byte[(int)LG_INFO_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)LG_TYPE_LEN;
		miArray[1].name="TokenLen";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Token";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)LG_INFO_LEN;
		miArray[3].name="ServerGroup";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="ResourceLevel";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_TokenLen;
		case	2:return m_Token;
		case	3:return m_ServerGroup;
		case	4:return m_ResourceLevel;

		}
		return 0;
	}
};

public class LGPKG_ACT_REQ	:	CMessage{
	public	int              m_SubReq;
	public	int              m_InfoLen;
	public	byte            [] m_Info;
	public LGPKG_ACT_REQ()
	{
		m_Info=new byte[(int)LG_INFO_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="SubReq";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="InfoLen";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Info";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)LG_INFO_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SubReq;
		case	1:return m_InfoLen;
		case	2:return m_Info;

		}
		return 0;
	}
};

public class SCDNInfo	:	CMessage{
	public	byte            [] m_Path;
	public SCDNInfo()
	{
		m_Path=new byte[(int)MAX_PATH_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Path";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_PATH_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Path;

		}
		return 0;
	}
};

public class SBundleInfoList	:	CMessage{
	public	byte            [] m_Path;
	public	uint             m_CDNCount;
	public	SCDNInfo        [] m_CDN;	/// 可选择的cdn基地址，用于拼接bundle相对路径得到下载地址 .
	public SBundleInfoList()
	{
		m_Path=new byte[(int)MAX_PATH_LEN];
		m_CDN=new SCDNInfo[(int)8];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Path";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_PATH_LEN;
		miArray[1].name="CDNCount";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="CDN";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)8;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Path;
		case	1:return m_CDNCount;
		case	2:return m_CDN;

		}
		return 0;
	}
};

public class LGDT_LOGIN_SUCCESS	:	CMessage{
	public	byte            [] m_Username;
	public	byte            [] m_DigitID;
	public	byte            [] m_SessionID;
	public	byte            [] m_Sig;
	public	int              m_InfoLen;
	public	byte            [] m_Info;
	public	HeaderProto.SServerInfo m_GateInfo	=	new HeaderProto.SServerInfo();	/// 服务器地址       .
	public	SBundleInfoList  m_BundleInfo	=	new SBundleInfoList();	/// 更新包数据       .
	public LGDT_LOGIN_SUCCESS()
	{
		m_Username=new byte[(int)LG_USERNAME_LEN];
		m_DigitID=new byte[(int)LG_DIGITID_LEN];
		m_SessionID=new byte[(int)LG_SESSIONID_LEN];
		m_Sig=new byte[(int)LG_SIG_LEN];
		m_Info=new byte[(int)LG_INFO_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[8];
		miArray[0].name="Username";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)LG_USERNAME_LEN;
		miArray[1].name="DigitID";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)LG_DIGITID_LEN;
		miArray[2].name="SessionID";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)LG_SESSIONID_LEN;
		miArray[3].name="Sig";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)LG_SIG_LEN;
		miArray[4].name="InfoLen";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Info";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)LG_INFO_LEN;
		miArray[6].name="GateInfo";miArray[6].type=MemberInfo.MIType.eMessage;miArray[6].size=0;miArray[6].count=(int)0;
		miArray[7].name="BundleInfo";miArray[7].type=MemberInfo.MIType.eMessage;miArray[7].size=0;miArray[7].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Username;
		case	1:return m_DigitID;
		case	2:return m_SessionID;
		case	3:return m_Sig;
		case	4:return m_InfoLen;
		case	5:return m_Info;
		case	6:return m_GateInfo;
		case	7:return m_BundleInfo;

		}
		return 0;
	}
};

public class LGDT_LOGIN_FAIL	:	CMessage{
	public	int              m_ErrCode;
	public	int              m_SubErrCode;
	public	byte            [] m_MaintainInfo;	/// 维护公告         .
	public LGDT_LOGIN_FAIL()
	{
		m_MaintainInfo=new byte[(int)LG_INFO_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="SubErrCode";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="MaintainInfo";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)LG_INFO_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_SubErrCode;
		case	2:return m_MaintainInfo;

		}
		return 0;
	}
};

public class LGDT_LOGIN_INTERMEDIATE	:	CMessage{
	public	int              m_SubAck;
	public	int              m_InfoLen;
	public	byte            [] m_Info;
	public LGDT_LOGIN_INTERMEDIATE()
	{
		m_Info=new byte[(int)LG_INFO_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="SubAck";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="InfoLen";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Info";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)LG_INFO_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SubAck;
		case	1:return m_InfoLen;
		case	2:return m_Info;

		}
		return 0;
	}
};

public class LGDT_ACT_REPLY	:	CMessage{
	public	LGDT_LOGIN_SUCCESS m_Succ	=	new LGDT_LOGIN_SUCCESS();
	public	LGDT_LOGIN_FAIL  m_Fail	=	new LGDT_LOGIN_FAIL();
	public	LGDT_LOGIN_INTERMEDIATE m_Inter	=	new LGDT_LOGIN_INTERMEDIATE();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Succ";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="Fail";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Inter";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Succ;
		case	1:return m_Fail;
		case	2:return m_Inter;

		}
		return 0;
	}
};

public class LGPKG_LOGIN_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.LGID_LOGIN_ACK;}
	public	byte             m_AckType;
	public	LGDT_ACT_REPLY   m_Reply	=	new LGDT_ACT_REPLY();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="AckType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Reply";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_AckType;
		case	1:return m_Reply;

		}
		return 0;
	}
};

public class LGPKG_GAMESERVER_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.LGID_GAMESERVER_LIST_REQ;}
	public	byte             m_Flags;
	public	byte            [] m_Sig;
	public LGPKG_GAMESERVER_LIST_REQ()
	{
		m_Sig=new byte[(int)LG_SIG_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Flags";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Sig";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)LG_SIG_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Flags;
		case	1:return m_Sig;

		}
		return 0;
	}
};

public class LGPKG_GAMESERVER_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.LGID_GAMESERVER_LIST_ACK;}
	public	ushort           m_Count;
	public	HeaderProto.SGameServerInfo[] m_List;
	public LGPKG_GAMESERVER_LIST_ACK()
	{
		m_List=new HeaderProto.SGameServerInfo[(int)LG_MAX_GAMESERVER];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="List";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)LG_MAX_GAMESERVER;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_List;

		}
		return 0;
	}
};

public class GC_ERROR	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_ERROR;}
	public	int              m_ErrCode;	/// 错误号           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;

		}
		return 0;
	}
};

public class GC_ERRORSTRING	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_ERRORSTRING;}
	public	byte            [] m_ErrCode;	/// 错误文本         .
	public GC_ERRORSTRING()
	{
		m_ErrCode=new byte[(int)MAX_ERROR_STRING_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)MAX_ERROR_STRING_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;

		}
		return 0;
	}
};

public class CG_HEART_BEAT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_HEART_BEAT;}
};

public class GC_HEART_BEAT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_HEART_BEAT;}
};

/* 系统公告 */
public class GC_PUBLIC_STRING	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_PUBLIC_STRING;}
	public	byte            [] m_Info;	/// 公告内容         .
	public GC_PUBLIC_STRING()
	{
		m_Info=new byte[(int)512];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)512;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 客户端登录Gate Server请求   */
public class CG_LOGIN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_LOGIN;}
	public	byte            [] m_PTID;	/// 客户端的账号     .
	public	int              m_LoginDataLen;	/// 登录数据长度     .
	public	byte            [] m_LoginData;	/// 登录数据，从SDLoginClient组件中获取的数据 .
	public	byte            [] m_ProtoVersion;	/// 协议md5          .
	public	int              m_MainVersion;	/// 主版本号         .
	public	int              m_SubVersion;	/// 次版本号         .
	public	int              m_BuildVersion;	/// 编译版本号       .
	public	int              m_ResourceVersion;	/// 资源版本号       .
	public CG_LOGIN()
	{
		m_PTID=new byte[(int)HeaderProto.PTID_LEN];
		m_LoginData=new byte[(int)64];
		m_ProtoVersion=new byte[(int)33];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[8];
		miArray[0].name="PTID";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)HeaderProto.PTID_LEN;
		miArray[1].name="LoginDataLen";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="LoginData";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)64;
		miArray[3].name="ProtoVersion";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)33;
		miArray[4].name="MainVersion";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="SubVersion";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="BuildVersion";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="ResourceVersion";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PTID;
		case	1:return m_LoginDataLen;
		case	2:return m_LoginData;
		case	3:return m_ProtoVersion;
		case	4:return m_MainVersion;
		case	5:return m_SubVersion;
		case	6:return m_BuildVersion;
		case	7:return m_ResourceVersion;

		}
		return 0;
	}
};

/* 客户端登录Gate Server的响应  */
public class GC_LOGIN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_LOGIN;}
	public	int              m_ErrCode;	/// 错误号           .
	public	byte            [] m_ErrMsg;	/// 错误描述文字     .
	public	byte             m_EncryptType;	/// 加密类型:0不加密, 1动态加密 .
	public	int              m_Len;	/// DynCode的长度    .
	public	byte            [] m_DynCode;	/// DynCode          .
	public	uint             m_QueueToken;	/// QueueToken排队流水号，0表示不用排队。若需要排队，则可以期待定期收到排队状态更新消息 .
	public	uint             m_LastEnterQueueToken;	/// 该服务器组最近一个进入游戏的玩家的排队流水号 .
	public GC_LOGIN()
	{
		m_ErrMsg=new byte[(int)255];
		m_DynCode=new byte[(int)MAX_DYNCODE_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[7];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="ErrMsg";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)255;
		miArray[2].name="EncryptType";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="Len";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="DynCode";miArray[4].type=MemberInfo.MIType.eByte;miArray[4].size=1;miArray[4].count=(int)MAX_DYNCODE_LEN;
		miArray[5].name="QueueToken";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="LastEnterQueueToken";miArray[6].type=MemberInfo.MIType.eUInt;miArray[6].size=4;miArray[6].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_ErrMsg;
		case	2:return m_EncryptType;
		case	3:return m_Len;
		case	4:return m_DynCode;
		case	5:return m_QueueToken;
		case	6:return m_LastEnterQueueToken;

		}
		return 0;
	}
};

/* 客户端退出游戏的请求  */
public class CG_LOGOUT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_LOGOUT;}
	public	byte             m_Type;	/// 退出类型(GS_LOGOUT_EXIT:大退, GS_LOGOUT_QUIT:小退) .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;

		}
		return 0;
	}
};

/* 客户端退出Gate Server的响应 */
public class GC_LOGOUT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_LOGOUT;}
	public	int              m_ErrCode;	/// 错误号           .
	public	byte             m_Type;	/// 退出类型(GS_LOGOUT_EXIT:大退, GS_LOGOUT_QUIT:小退)  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Type";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_Type;

		}
		return 0;
	}
};

/* 排队信息  */
public class GC_QUEUE_INFO	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_QUEUE_INFO;}
	public	uint             m_LastPassToken;	/// 前面还有多少人   .
	public	int              m_Priority;	/// 排队优先级, 0:vip队列, 1:普通队列 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="LastPassToken";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Priority";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LastPassToken;
		case	1:return m_Priority;

		}
		return 0;
	}
};

/* 返回角色列表  */
public class GC_ROLELIST	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_ROLELIST;}
	public	byte             m_Count;	/// 角色个数         .
	public	byte             m_LastSelect;	/// 上次选择角色的序号 .
	public	HeaderProto.SRoleInfoWithEquip[] m_RoleInfoList;	/// 角色信息         .
	public GC_ROLELIST()
	{
		m_RoleInfoList=new HeaderProto.SRoleInfoWithEquip[(int)HeaderProto.ACTIVE_ROLE_NUM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="LastSelect";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="RoleInfoList";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)HeaderProto.ACTIVE_ROLE_NUM;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_LastSelect;
		case	2:return m_RoleInfoList;

		}
		return 0;
	}
};

/* 请求创建角色  */
public class CG_CREATEROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_CREATEROLE;}
	public	HeaderProto.SRoleInfo m_RoleInfo	=	new HeaderProto.SRoleInfo();	/// 角色信息         .
	public	uint             m_ServerUniqueID;	/// 创建角色所在的服务器，来自服务器列表，是所选服对应的ServerUniqueID .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="ServerUniqueID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleInfo;
		case	1:return m_ServerUniqueID;

		}
		return 0;
	}
};

/* 回应创建角色  */
public class GC_CREATEROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_CREATEROLE;}
	public	int              m_ErrCode;	/// 错误号 0表示正常 .
	public	HeaderProto.SRoleInfo m_RoleInfo	=	new HeaderProto.SRoleInfo();	/// 角色信息         .
	public	byte            [] m_ErrString;	/// 错误文本         .
	public GC_CREATEROLE()
	{
		m_ErrString=new byte[(int)MAX_ERROR_STRING_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="RoleInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="ErrString";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)MAX_ERROR_STRING_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_RoleInfo;
		case	2:return m_ErrString;

		}
		return 0;
	}
};

/* 请求删除角色  */
public class CG_DELROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_DELROLE;}
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

/* 回应删除角色  */
public class GC_DELROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_DELROLE;}
	public	int              m_ErrCode;	/// 错误号           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;

		}
		return 0;
	}
};

/* 选取角色  */
public class CG_SELECTROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_SELECTROLE;}
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

/* 回应选取角色  */
public class GC_SELECTROLE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_SELECTROLE;}
	public	int              m_ErrCode;	/// 错误号           .
	public	byte            [] m_ErrString;	/// 错误文本         .
	public GC_SELECTROLE()
	{
		m_ErrString=new byte[(int)MAX_ERROR_STRING_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="ErrString";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)MAX_ERROR_STRING_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_ErrString;

		}
		return 0;
	}
};

public class GC_GPK_CLT_DYN_CODE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_GPK_DYN_CODE;}
	public	uint             m_Count;
	public	byte            [] m_Code;
	public GC_GPK_CLT_DYN_CODE()
	{
		m_Code=new byte[(int)12288];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Code";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)12288;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Code;

		}
		return 0;
	}
};

public class CG_GPK_CLT_NEGLECT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_GPK_NEGLECT;}
	public	byte            [] m_Nothing;
	public CG_GPK_CLT_NEGLECT()
	{
		m_Nothing=new byte[(int)196];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Nothing";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)196;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Nothing;

		}
		return 0;
	}
};

/* 客户端对下面的内容进行转换，结果通过CG_GPK_CAL_RESULT返回服务器，服务器调用同一函数验证 */
public class GC_GPK_CAL_ORIGIN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GCID_GPK_CAL_ORIGIN;}
	public	byte            [] m_Original;
	public GC_GPK_CAL_ORIGIN()
	{
		m_Original=new byte[(int)24];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Original";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)24;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Original;

		}
		return 0;
	}
};

public class CG_GPK_CAL_RESULT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CGID_GPK_CAL_RESULT;}
	public	byte            [] m_Result;
	public CG_GPK_CAL_RESULT()
	{
		m_Result=new byte[(int)24];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)24;

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

/* Gate向Gameserver报告自己的身份 */
public class GSPKG_SERVERINFO_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_SERVERINFO_RPT;}
	public	uint             m_ServerID;	/// ServerID         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ServerID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ServerID;

		}
		return 0;
	}
};

public class GSPKG_SERVER_GROUP_DESC_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_SERVER_GROUP_DESC_RPT;}
	public	byte            [] m_GroupName;
	public GSPKG_SERVER_GROUP_DESC_RPT()
	{
		m_GroupName=new byte[(int)HeaderProto.MAX_SERVER_DESC_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="GroupName";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)HeaderProto.MAX_SERVER_DESC_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GroupName;

		}
		return 0;
	}
};

public class GSPKG_PLAYER_GPK_RPT	:	CMessage{
};

/* GateServer 向GS转发登录GS请求 */
public class GSPKG_ENTERGAME_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_ENTERGAME_REQ;}
	public	byte            [] m_PTID;	/// PT帐号名         .
	public	UInt64           m_DigitID;
	public	byte            [] m_AccountName;
	public	UInt64           m_RoleId;	/// 角色名           .
	public	uint             m_Flags;	/// 标志             .
	public	byte             m_GmLevel;	/// GmLevel          .
	public	ushort           m_SessionID;	/// SESSIONID        .
	public	uint             m_SceneId;	/// 场景ID           .
	public	byte             m_EnterType;	/// 进入场景的方式 0上线，1切换场景  .
	public	uint             m_GateSvrID;
	public	HeaderProto.SLMAccountInfo m_AccountInfo	=	new HeaderProto.SLMAccountInfo();
	public GSPKG_ENTERGAME_REQ()
	{
		m_PTID=new byte[(int)HeaderProto.PTID_LEN];
		m_AccountName=new byte[(int)HeaderProto.ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[11];
		miArray[0].name="PTID";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)HeaderProto.PTID_LEN;
		miArray[1].name="DigitID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="AccountName";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)HeaderProto.ROLE_NAME_LEN;
		miArray[3].name="RoleId";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;
		miArray[4].name="Flags";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="GmLevel";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="SessionID";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="SceneId";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="EnterType";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;
		miArray[9].name="GateSvrID";miArray[9].type=MemberInfo.MIType.eUInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="AccountInfo";miArray[10].type=MemberInfo.MIType.eMessage;miArray[10].size=0;miArray[10].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PTID;
		case	1:return m_DigitID;
		case	2:return m_AccountName;
		case	3:return m_RoleId;
		case	4:return m_Flags;
		case	5:return m_GmLevel;
		case	6:return m_SessionID;
		case	7:return m_SceneId;
		case	8:return m_EnterType;
		case	9:return m_GateSvrID;
		case	10:return m_AccountInfo;

		}
		return 0;
	}
};

/* 登录GS回应  */
public class GSPKG_ENTERGAME_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_ENTERGAME_ACK;}
	public	int              m_ErrCode;	/// 错误码           .
	public	uint             m_Flags;	/// 标志             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Flags";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_Flags;

		}
		return 0;
	}
};

/* 退出GS请求  */
public class GSPKG_LEAVEGAME_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_LEAVEGAME_REQ;}
	public	byte             m_Type;	/// 退出类型 (GS_LOGOUT_EXIT:大退, GS_LOGOUT_QUIT:小退)  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;

		}
		return 0;
	}
};

/* 退出GS回应  */
public class GSPKG_LEAVEGAME_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_LEAVEGAME_ACK;}
	public	int              m_ErrCode;	/// 错误码           .
	public	byte             m_Type;	/// 退出类型         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Type";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_Type;

		}
		return 0;
	}
};

/* 跳转场景请求  */
public class GSPKG_SWITCHSCENE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_SWITCHSCENE_REQ;}
	public	uint             m_SceneTemplateID;	/// 场景模板id       .
	public	uint             m_SceneID;	/// 场景实例id，用于跳线, 如果不指定实例id 赋值-1  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="SceneTemplateID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="SceneID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SceneTemplateID;
		case	1:return m_SceneID;

		}
		return 0;
	}
};

/* 跳转场景请求回应  */
public class GSPKG_SWITCHSCENE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_SWITCHSCENE_ACK;}
	public	int              m_ErrCode;	/// 错误码           .
	public	uint             m_SceneTemplateID;	/// 场景模板id       .
	public	uint             m_SceneID;	/// 场景实例id，用于跳线, 如果不指定实例id 赋值-1  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="SceneTemplateID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="SceneID";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_SceneTemplateID;
		case	2:return m_SceneID;

		}
		return 0;
	}
};

/* 踢人通知  */
public class GSPKG_KICKOUT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_KICKOUT_NTF;}
	public	int              m_ErrCode;	/// 错误码           .
	public	byte            [] m_ErrString;	/// 错误信息         .
	public GSPKG_KICKOUT_NTF()
	{
		m_ErrString=new byte[(int)255];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ErrCode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="ErrString";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)255;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ErrCode;
		case	1:return m_ErrString;

		}
		return 0;
	}
};

/* GS到GT之间的广播消息  */
public class GSPKG_MSG_BROADCAST	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_MSG_BROADCAST;}
	public	ushort           m_IDCount;
	public	ushort          [] m_ID;
	public	ushort           m_MsgBufLen;
	public	byte            [] m_MsgBuf;
	public GSPKG_MSG_BROADCAST()
	{
		m_ID=new ushort[(int)HeaderProto.TS_BROADCAST_ID_COUNT];
		m_MsgBuf=new byte[(int)HeaderProto.TS_BROADCAST_MSG_SIZE];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="IDCount";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="ID";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)HeaderProto.TS_BROADCAST_ID_COUNT;
		miArray[2].name="MsgBufLen";miArray[2].type=MemberInfo.MIType.eUShort;miArray[2].size=2;miArray[2].count=(int)0;
		miArray[3].name="MsgBuf";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)HeaderProto.TS_BROADCAST_MSG_SIZE;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IDCount;
		case	1:return m_ID;
		case	2:return m_MsgBufLen;
		case	3:return m_MsgBuf;

		}
		return 0;
	}
};

public class GSPKG_LOAD_GSDATA_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_LOAD_GSDATA_NTF;}
	public	int              m_Count;
	public	byte            [] m_XmlData;
	public GSPKG_LOAD_GSDATA_NTF()
	{
		m_XmlData=new byte[(int)HeaderProto.MAX_XMLDATA_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="XmlData";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)HeaderProto.MAX_XMLDATA_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_XmlData;

		}
		return 0;
	}
};

public class GSPKG_UPDATEGMLEVEL_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.GSID_UPDATEGMLEVEL_NTF;}
	public	UInt64           m_DigitID;
	public	byte             m_GMLevel;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="DigitID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="GMLevel";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DigitID;
		case	1:return m_GMLevel;

		}
		return 0;
	}
};

/* 错误码 */
public class SC_ERROR_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ERROR_NTF;}
	public	HeaderProto.SError m_Data	=	new HeaderProto.SError();	/// 错误信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Data";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Data;

		}
		return 0;
	}
};

/* 推图关卡状态 */
public class SC_LEVEL_INFO	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_LEVEL_INFO;}
	public	uint             m_Count;	/// 关卡id 不在关卡里面的表示没有获得过星 .
	public	HeaderProto.SLevelInfo[] m_Info;	/// 关卡id           .
	public	uint             m_BattleCount;	/// 战役数量         .
	public	HeaderProto.SBattleInfo[] m_BtInfo;	/// 战役信息，没有在列表里表示没有获得星 .
	public	uint             m_BoxCount;	/// 宝箱数量         .
	public	uint            [] m_GotBattleBox;	/// 已经获得的宝箱id .
	public SC_LEVEL_INFO()
	{
		m_Info=new HeaderProto.SLevelInfo[(int)HeaderProto.MAX_LEVEL_COUNT];
		m_BtInfo=new HeaderProto.SBattleInfo[(int)HeaderProto.MAX_BATTLE_COUNT];
		m_GotBattleBox=new uint[(int)HeaderProto.MAX_BATTLE_BOX_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_LEVEL_COUNT;
		miArray[2].name="BattleCount";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="BtInfo";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)HeaderProto.MAX_BATTLE_COUNT;
		miArray[4].name="BoxCount";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="GotBattleBox";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)HeaderProto.MAX_BATTLE_BOX_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Info;
		case	2:return m_BattleCount;
		case	3:return m_BtInfo;
		case	4:return m_BoxCount;
		case	5:return m_GotBattleBox;

		}
		return 0;
	}
};

/* 申请进入推图 */
public class CS_LEVEL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_LEVEL_REQ;}
	public	byte             m_LevelBattleType;	/// 推图关卡类型 LEVEL_BATTLE_TYPE枚举 .
	public	uint             m_LevelID;	/// 关卡id           .
	public	uint             m_BuffID;	/// 选择使用的buff   .
	public	UInt64           m_AbyssDBID;	/// 深渊dbid         .
	public	uint             m_FriendPetID;	/// 好友助战宠物ID,没有时默认发-1 .
	public	byte             m_QuickEnd;	/// 是否扫荡模式 0非扫荡，1扫荡 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="LevelBattleType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="LevelID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="BuffID";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="AbyssDBID";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;
		miArray[4].name="FriendPetID";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="QuickEnd";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LevelBattleType;
		case	1:return m_LevelID;
		case	2:return m_BuffID;
		case	3:return m_AbyssDBID;
		case	4:return m_FriendPetID;
		case	5:return m_QuickEnd;

		}
		return 0;
	}
};

/* 领取战役宝箱星值奖励 */
public class CS_GET_BATTLE_BOX_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_BATTLE_BOX_REQ;}
	public	uint             m_BattleboxID;	/// 宝箱id           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BattleboxID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattleboxID;

		}
		return 0;
	}
};

/* 领取战役宝箱星值奖励的反馈 */
public class SC_GET_BATTLE_BOX_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_BATTLE_BOX_ACK;}
	public	uint             m_BattleboxID;	/// 宝箱id           .
	public	byte             m_Result;	/// 为GS_RESULT_YES表示领取成功 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="BattleboxID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattleboxID;
		case	1:return m_Result;

		}
		return 0;
	}
};

/* 更新战役的星数 */
public class SC_BATTLE_GOT_STAR_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_BATTLE_GOT_STAR_NTF;}
	public	uint             m_BattleID;	/// 战役id           .
	public	uint             m_GotStar;	/// 获得的星数       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="BattleID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GotStar";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattleID;
		case	1:return m_GotStar;

		}
		return 0;
	}
};

public class SDropInfo	:	CMessage{
	public	uint             m_TemplateID;	/// 物品id           .
	public	int              m_Count;	/// 掉落数量         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="TemplateID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_TemplateID;
		case	1:return m_Count;

		}
		return 0;
	}
};

public class SDropInfoList	:	CMessage{
	public	ushort           m_Count;
	public	SDropInfo       [] m_List;
	public SDropInfoList()
	{
		m_List=new SDropInfo[(int)HeaderProto.MAX_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="List";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_List;

		}
		return 0;
	}
};

public class SDropInfos	:	CMessage{
	public	ushort           m_Count;
	public	SDropInfo       [] m_List;
	public	uint             m_Money;	/// 掉落游戏币数量   .
	public SDropInfos()
	{
		m_List=new SDropInfo[(int)MAX_MONSTER_DROP];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="List";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_MONSTER_DROP;
		miArray[2].name="Money";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_List;
		case	2:return m_Money;

		}
		return 0;
	}
};

public class SMonsterInfo	:	CMessage{
	public	uint             m_Index;	/// 场景内怪的索引号 .
	public	SDropInfos       m_Drop	=	new SDropInfos();	/// 怪物掉落物品列表 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Index";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Drop";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Index;
		case	1:return m_Drop;

		}
		return 0;
	}
};

public class SMonsterInfos	:	CMessage{
	public	ushort           m_Count;
	public	SMonsterInfo    [] m_List;
	public SMonsterInfos()
	{
		m_List=new SMonsterInfo[(int)MAX_MONSTER_IN_LEVEL];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="List";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_MONSTER_IN_LEVEL;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_List;

		}
		return 0;
	}
};

/* 推图数据 */
public class SC_LEVEL_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_LEVEL_ACK;}
	public	byte             m_LevelBattleType;	/// 推图关卡类型 LEVEL_BATTLE_TYPE枚举 .
	public	uint             m_Result;	/// 0 表示顺利开始推图，请使用下列数据初始化。非0表示推图申请未能执行，有错误，理由查表 .
	public	uint             m_AbyssLevelID;	/// 深渊关卡id       .
	public	SMonsterInfos    m_Monster	=	new SMonsterInfos();
	public	ushort           m_FreeReliveCount;	/// 关卡免费复活数量 .
	public	uint             m_RelivePrice;	/// 复活代价，徽章   .
	public	ushort           m_InitialBuffCount;	/// 选择的buff数量   .
	public	uint            [] m_InitialBuffID;	/// 选择的buff，扣费 .
	public	byte             m_QuickEnd;	/// 是否扫荡模式 0非扫荡，1扫荡 .
	public SC_LEVEL_ACK()
	{
		m_InitialBuffID=new uint[(int)MAX_INITIAL_BUFF_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[9];
		miArray[0].name="LevelBattleType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="AbyssLevelID";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Monster";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="FreeReliveCount";miArray[4].type=MemberInfo.MIType.eUShort;miArray[4].size=2;miArray[4].count=(int)0;
		miArray[5].name="RelivePrice";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="InitialBuffCount";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="InitialBuffID";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)MAX_INITIAL_BUFF_COUNT;
		miArray[8].name="QuickEnd";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LevelBattleType;
		case	1:return m_Result;
		case	2:return m_AbyssLevelID;
		case	3:return m_Monster;
		case	4:return m_FreeReliveCount;
		case	5:return m_RelivePrice;
		case	6:return m_InitialBuffCount;
		case	7:return m_InitialBuffID;
		case	8:return m_QuickEnd;

		}
		return 0;
	}
};

/* 技能数 */
public class SSkillCount	:	CMessage{
	public	uint             m_SkillID;	/// 技能id           .
	public	ushort           m_Count;	/// 使用次数，统计时请注意，超过UInt16.Max的时候请保留UInt16.Max .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="SkillID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillID;
		case	1:return m_Count;

		}
		return 0;
	}
};

/* 关卡结果汇报 */
public class CS_LEVEL_RESULT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_LEVEL_RESULT_NTF;}
	public	byte             m_LevelBattleType;	/// 推图关卡类型 LEVEL_BATTLE_TYPE枚举 .
	public	uint             m_Result;	/// 0表示通关，非0表示尚未通关：1表示中途退出 .
	public	uint             m_Money;	/// 拾取的游戏币总数 .
	public	ushort           m_ItemCount;	/// 拾取的物品总数   .
	public	SDropInfo       [] m_Item;	/// 拾取的物品列表   .
	public	ushort           m_ReliveCount;	/// 使用复活次数     .
	public	ushort           m_KillCount;	/// 杀怪数量         .
	public	uint            [] m_Kills;	/// 杀怪ID           .
	public	uint             m_MaxCombo;	/// 最大连击数       .
	public	uint             m_Critical;	/// 致命一击数       .
	public	ushort           m_SkillCount;	/// 使用的技能种类数 .
	public	SSkillCount     [] m_Skills;	/// 使用的技能列表   .
	public	byte             m_CompleteResult;	/// 完成评价 ELevelCompleteResult .
	public	int              m_ActivityAbyssTotalDamage;	/// 打出的伤害总数   .
	public	int              m_CompleteSec;	/// 完成的秒数       .
	public	int              m_PetChangeTimes;	/// 宠物切换次数     .
	public CS_LEVEL_RESULT_NTF()
	{
		m_Item=new SDropInfo[(int)MAX_LEVEL_DROP];
		m_Kills=new uint[(int)MAX_MONSTER_IN_LEVEL];
		m_Skills=new SSkillCount[(int)100];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[16];
		miArray[0].name="LevelBattleType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Money";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="ItemCount";miArray[3].type=MemberInfo.MIType.eUShort;miArray[3].size=2;miArray[3].count=(int)0;
		miArray[4].name="Item";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)MAX_LEVEL_DROP;
		miArray[5].name="ReliveCount";miArray[5].type=MemberInfo.MIType.eUShort;miArray[5].size=2;miArray[5].count=(int)0;
		miArray[6].name="KillCount";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="Kills";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)MAX_MONSTER_IN_LEVEL;
		miArray[8].name="MaxCombo";miArray[8].type=MemberInfo.MIType.eUInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="Critical";miArray[9].type=MemberInfo.MIType.eUInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="SkillCount";miArray[10].type=MemberInfo.MIType.eUShort;miArray[10].size=2;miArray[10].count=(int)0;
		miArray[11].name="Skills";miArray[11].type=MemberInfo.MIType.eMessage;miArray[11].size=0;miArray[11].count=(int)100;
		miArray[12].name="CompleteResult";miArray[12].type=MemberInfo.MIType.eByte;miArray[12].size=1;miArray[12].count=(int)0;
		miArray[13].name="ActivityAbyssTotalDamage";miArray[13].type=MemberInfo.MIType.eInt;miArray[13].size=4;miArray[13].count=(int)0;
		miArray[14].name="CompleteSec";miArray[14].type=MemberInfo.MIType.eInt;miArray[14].size=4;miArray[14].count=(int)0;
		miArray[15].name="PetChangeTimes";miArray[15].type=MemberInfo.MIType.eInt;miArray[15].size=4;miArray[15].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LevelBattleType;
		case	1:return m_Result;
		case	2:return m_Money;
		case	3:return m_ItemCount;
		case	4:return m_Item;
		case	5:return m_ReliveCount;
		case	6:return m_KillCount;
		case	7:return m_Kills;
		case	8:return m_MaxCombo;
		case	9:return m_Critical;
		case	10:return m_SkillCount;
		case	11:return m_Skills;
		case	12:return m_CompleteResult;
		case	13:return m_ActivityAbyssTotalDamage;
		case	14:return m_CompleteSec;
		case	15:return m_PetChangeTimes;

		}
		return 0;
	}
};

/* 关卡翻牌信息 */
public class SLevelSelectCardInfo	:	CMessage{
	public	byte             m_IndexID;	/// 唯一标识         .
	public	uint             m_ItemID;	/// 奖励物品模板ID 为1表示是金钱 2是绑定徽章，3是徽章 .
	public	uint             m_Count;	/// 数量             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="IndexID";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Count";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_IndexID;
		case	1:return m_ItemID;
		case	2:return m_Count;

		}
		return 0;
	}
};

/* 已获得奖励及宝箱信息 */
public class SC_TREASURE_CHEST_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_TREASURE_CHEST_NTF;}
	public	uint             m_LevelID;	/// 关卡id           .
	public	byte             m_CurGetStar;	/// 本次获得的星数   .
	public	byte             m_QuickEnd;	/// 是否扫荡模式 0非扫荡，1扫荡 .
	public	HeaderProto.SXITEMS m_Items	=	new HeaderProto.SXITEMS();	/// 物品列表         .
	public	uint             m_Money;	/// 本关卡确认获得了的金钱数量 .
	public	uint             m_Experience;	/// 本关卡确认获得了的经验数量 .
	public	ushort           m_PetAboutCount;	/// 确认获得的宠物和宠物物品 .
	public	uint            [] m_PetAbout;	/// 宠物和宠物物品的模板id .
	public	byte             m_GetCount;	/// 获得的数量       .
	public	SLevelSelectCardInfo[] m_CardInfos;	/// 获得的宝箱       .
	public	byte             m_TempCount;	/// 假数据数量       .
	public	SLevelSelectCardInfo[] m_TempCardInfos;	/// 假数据           .
	public SC_TREASURE_CHEST_NTF()
	{
		m_PetAbout=new uint[(int)HeaderProto.MAX_PET_COUNT];
		m_CardInfos=new SLevelSelectCardInfo[(int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT];
		m_TempCardInfos=new SLevelSelectCardInfo[(int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[12];
		miArray[0].name="LevelID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="CurGetStar";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="QuickEnd";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="Items";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="Money";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Experience";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="PetAboutCount";miArray[6].type=MemberInfo.MIType.eUShort;miArray[6].size=2;miArray[6].count=(int)0;
		miArray[7].name="PetAbout";miArray[7].type=MemberInfo.MIType.eUInt;miArray[7].size=4;miArray[7].count=(int)HeaderProto.MAX_PET_COUNT;
		miArray[8].name="GetCount";miArray[8].type=MemberInfo.MIType.eByte;miArray[8].size=1;miArray[8].count=(int)0;
		miArray[9].name="CardInfos";miArray[9].type=MemberInfo.MIType.eMessage;miArray[9].size=0;miArray[9].count=(int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT;
		miArray[10].name="TempCount";miArray[10].type=MemberInfo.MIType.eByte;miArray[10].size=1;miArray[10].count=(int)0;
		miArray[11].name="TempCardInfos";miArray[11].type=MemberInfo.MIType.eMessage;miArray[11].size=0;miArray[11].count=(int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LevelID;
		case	1:return m_CurGetStar;
		case	2:return m_QuickEnd;
		case	3:return m_Items;
		case	4:return m_Money;
		case	5:return m_Experience;
		case	6:return m_PetAboutCount;
		case	7:return m_PetAbout;
		case	8:return m_GetCount;
		case	9:return m_CardInfos;
		case	10:return m_TempCount;
		case	11:return m_TempCardInfos;

		}
		return 0;
	}
};

/* 玩家开宝箱 */
public class CS_OPEN_TREASURE_CHEST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_OPEN_TREASURE_CHEST_REQ;}
	public	byte             m_ItemIndexID;	/// 物品唯一标识     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ItemIndexID";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemIndexID;

		}
		return 0;
	}
};

/* 开宝箱结果 */
public class SC_OPEN_TREASURE_CHEST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_OPEN_TREASURE_CHEST_ACK;}
	public	byte             m_ItemIndexID;	/// 物品唯一标识     .
	public	byte             m_Result;	/// 为GS_RESULT_YES表示领取成功 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemIndexID";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemIndexID;
		case	1:return m_Result;

		}
		return 0;
	}
};

/* GS通知Load场景 */
public class SC_SELF_LOADSCENE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_LOADSCENE;}
	public	uint             m_SceneId;	/// 场景ID           .
	public	HeaderProto.SPosInfo m_EnterPos	=	new HeaderProto.SPosInfo();	/// 场景位置         .
	public	uint             m_Time;	/// 服务器时间       .
	public	uint             m_SceneType;	/// 地图类型         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="SceneId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="EnterPos";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Time";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="SceneType";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SceneId;
		case	1:return m_EnterPos;
		case	2:return m_Time;
		case	3:return m_SceneType;

		}
		return 0;
	}
};

/* 客户端Load场景完成 */
public class CS_ENTERSCENE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ENTERSCENE;}
	public	uint             m_Error;	/// 错误码           .
	public	uint             m_SceneId;	/// 场景ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="SceneId";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_SceneId;

		}
		return 0;
	}
};

/* 进入场景回应 */
public class SC_SELF_ENTERSCENE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_ENTERSCENE;}
	public	byte             m_Flag;	/// 进场景标志位     .
	public	byte             m_Error;	/// 错误码           .
	public	UInt64           m_ServerTime;	/// 服务器当前时间戳,ms  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Flag";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Error";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="ServerTime";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Flag;
		case	1:return m_Error;
		case	2:return m_ServerTime;

		}
		return 0;
	}
};

/* 个人所必须的所有数据通知完毕 */
public class SC_SELF_DATA_END	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_DATA_END;}
};

/* 角色进入游戏第一条消息，客户端开始加载地图信息  */
public class SC_SELF_APPEAR	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_APPEAR;}
	public	HeaderProto.SPosInfo m_EnterPos	=	new HeaderProto.SPosInfo();	/// 场景位置         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="EnterPos";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EnterPos;

		}
		return 0;
	}
};

/* 角色常规易变信息  */
public class SC_SELF_VOLATILE_PRO	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_VOLATILE_PRO;}
	public	int              m_Level;	/// 当前等级         .
	public	uint             m_NonMoney;	/// 绑定游戏币（金币）  .
	public	uint             m_NonCash;	/// 绑定勋章         .
	public	uint             m_Cash;	/// 流通勋章         .
	public	int              m_EP;	/// 当前活力值       .
	public	Int64            m_Experience;	/// 当前等级累计经验  .
	public	int              m_AP;	/// 当前协助点数     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[7];
		miArray[0].name="Level";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="NonMoney";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="NonCash";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Cash";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="EP";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Experience";miArray[5].type=MemberInfo.MIType.eInt64;miArray[5].size=8;miArray[5].count=(int)0;
		miArray[6].name="AP";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Level;
		case	1:return m_NonMoney;
		case	2:return m_NonCash;
		case	3:return m_Cash;
		case	4:return m_EP;
		case	5:return m_Experience;
		case	6:return m_AP;

		}
		return 0;
	}
};

/* 客户端配置  */
public class SC_CLIENT_CONFIG_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_CLIENT_CONFIG_NTF;}
	public	byte            [] m_ConfigInfo;	/// 配置信息         .
	public SC_CLIENT_CONFIG_NTF()
	{
		m_ConfigInfo=new byte[(int)512];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ConfigInfo";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)512;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ConfigInfo;

		}
		return 0;
	}
};

/* 客户端配置更新  */
public class CS_CLIENT_CONFIG_UPDATE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_CLIENT_CONFIG_UPDATE;}
	public	byte            [] m_ConfigInfo;	/// 配置信息         .
	public CS_CLIENT_CONFIG_UPDATE()
	{
		m_ConfigInfo=new byte[(int)512];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ConfigInfo";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)512;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ConfigInfo;

		}
		return 0;
	}
};

/* 角色基础属性信息  */
public class SC_SELF_BASE_PRO	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_BASE_PRO;}
	public	UInt64           m_ObjID;	/// 出现对象ID 临时id  .
	public	UInt64           m_DBID;	/// 唯一id           .
	public	int              m_RoleIndex;	/// 本服唯一id       .
	public	byte            [] m_Name;	/// 对象名字         .
	public	int              m_Level;	/// 当前等级         .
	public	byte             m_BaseJob;	/// 基础门派         .
	public	byte             m_Job;	/// 当前门派         .
	public	byte             m_Sex;	/// 对象性别         .
	public	Int64            m_Experience;	/// 当前等级累计经验  .
	public	uint             m_NonMoney;	/// 绑定游戏币（金币）  .
	public	uint             m_NonCash;	/// 绑定勋章         .
	public	uint             m_Cash;	/// 流通勋章         .
	public	int              m_HP;	/// 当前血量         .
	public	int              m_SP;	/// 当前技力         .
	public	int              m_EP;	/// 当前活力值       .
	public	int              m_MoveSpeed;	/// 基础移动速度 1秒移动多少mm  .
	public	int              m_AttSpeed;	/// 攻击速度         .
	public	int              m_Str;	/// 当前力量         .
	public	int              m_Int;	/// 当前智力         .
	public	int              m_Dex;	/// 当前敏捷         .
	public	int              m_Sta;	/// 当前体力         .
	public	int              m_Fai;	/// 当前信念         .
	public	int              m_MaxHP;	/// 血量上限         .
	public	int              m_MaxSP;	/// 技力上限         .
	public	int              m_HPTick;	/// 每轮回复血量     .
	public	int              m_SPTick;	/// 每轮回复技力量   .
	public	int              m_AtkDmgMin;	/// 当前伤害下限     .
	public	int              m_AtkDmgMax;	/// 当前伤害上限     .
	public	int              m_Def;	/// 当前防御         .
	public	int              m_IceAtt;	/// 冰属性攻击       .
	public	int              m_FireAtt;	/// 火属性攻击       .
	public	int              m_PoisonAtt;	/// 毒属性攻击       .
	public	int              m_ThunderAtt;	/// 雷属性攻击       .
	public	int              m_IceDef;	/// 冰属性抵抗       .
	public	int              m_FireDef;	/// 火属性抵抗       .
	public	int              m_PoisonDef;	/// 毒属性抵抗       .
	public	int              m_ThunderDef;	/// 雷属性抵抗       .
	public	int              m_Pierce;	/// 穿透点数         .
	public	int              m_Hit;	/// 命中点数         .
	public	int              m_Dodge;	/// 闪避点数         .
	public	int              m_Cri;	/// 致命一击点数     .
	public	int              m_Flex;	/// 韧性点数         .
	public	int              m_CriDmg;	/// 致命一击伤害系数 15000 = 1.5  .
	public	int              m_CriDmgDef;	/// 致命一击防御系数修正 正负 万分比  .
	public	int              m_BodySize;	/// 体型半径 mm      .
	public	int              m_AttSize;	/// 攻击半径 mm      .
	public	int              m_AttSpeedModPer;	/// 攻击加速百分比   .
	public	int              m_MoveSpeedModPer;	/// 移动加速百分比   .
	public	int              m_PiercePer;	/// 穿透百分比修正 正负 万分比  .
	public	int              m_HitPer;	/// 命中率修正  正负  万分比  .
	public	int              m_DodgePer;	/// 闪避率修正 正负  万分比  .
	public	int              m_CriPer;	/// 致命一击率修正 正负 万分比  .
	public	int              m_FlexPer;	/// 韧性率修正  正负 万分比  .
	public	int              m_MaxEP;	/// 最大活力值       .
	public	int              m_AP;	/// 协助点数         .
	public	int              m_MaxAP;	/// 最大协助点数     .
	public	int              m_ExpPer;	/// 经验值 正负 万分比  .
	public	int              m_MoneyPer;	/// 金钱 正负 万分比  .
	public SC_SELF_BASE_PRO()
	{
		m_Name=new byte[(int)HeaderProto.ROLE_NAME_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[58];
		miArray[0].name="ObjID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="DBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="RoleIndex";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Name";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)HeaderProto.ROLE_NAME_LEN;
		miArray[4].name="Level";miArray[4].type=MemberInfo.MIType.eInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="BaseJob";miArray[5].type=MemberInfo.MIType.eByte;miArray[5].size=1;miArray[5].count=(int)0;
		miArray[6].name="Job";miArray[6].type=MemberInfo.MIType.eByte;miArray[6].size=1;miArray[6].count=(int)0;
		miArray[7].name="Sex";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="Experience";miArray[8].type=MemberInfo.MIType.eInt64;miArray[8].size=8;miArray[8].count=(int)0;
		miArray[9].name="NonMoney";miArray[9].type=MemberInfo.MIType.eUInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="NonCash";miArray[10].type=MemberInfo.MIType.eUInt;miArray[10].size=4;miArray[10].count=(int)0;
		miArray[11].name="Cash";miArray[11].type=MemberInfo.MIType.eUInt;miArray[11].size=4;miArray[11].count=(int)0;
		miArray[12].name="HP";miArray[12].type=MemberInfo.MIType.eInt;miArray[12].size=4;miArray[12].count=(int)0;
		miArray[13].name="SP";miArray[13].type=MemberInfo.MIType.eInt;miArray[13].size=4;miArray[13].count=(int)0;
		miArray[14].name="EP";miArray[14].type=MemberInfo.MIType.eInt;miArray[14].size=4;miArray[14].count=(int)0;
		miArray[15].name="MoveSpeed";miArray[15].type=MemberInfo.MIType.eInt;miArray[15].size=4;miArray[15].count=(int)0;
		miArray[16].name="AttSpeed";miArray[16].type=MemberInfo.MIType.eInt;miArray[16].size=4;miArray[16].count=(int)0;
		miArray[17].name="Str";miArray[17].type=MemberInfo.MIType.eInt;miArray[17].size=4;miArray[17].count=(int)0;
		miArray[18].name="Int";miArray[18].type=MemberInfo.MIType.eInt;miArray[18].size=4;miArray[18].count=(int)0;
		miArray[19].name="Dex";miArray[19].type=MemberInfo.MIType.eInt;miArray[19].size=4;miArray[19].count=(int)0;
		miArray[20].name="Sta";miArray[20].type=MemberInfo.MIType.eInt;miArray[20].size=4;miArray[20].count=(int)0;
		miArray[21].name="Fai";miArray[21].type=MemberInfo.MIType.eInt;miArray[21].size=4;miArray[21].count=(int)0;
		miArray[22].name="MaxHP";miArray[22].type=MemberInfo.MIType.eInt;miArray[22].size=4;miArray[22].count=(int)0;
		miArray[23].name="MaxSP";miArray[23].type=MemberInfo.MIType.eInt;miArray[23].size=4;miArray[23].count=(int)0;
		miArray[24].name="HPTick";miArray[24].type=MemberInfo.MIType.eInt;miArray[24].size=4;miArray[24].count=(int)0;
		miArray[25].name="SPTick";miArray[25].type=MemberInfo.MIType.eInt;miArray[25].size=4;miArray[25].count=(int)0;
		miArray[26].name="AtkDmgMin";miArray[26].type=MemberInfo.MIType.eInt;miArray[26].size=4;miArray[26].count=(int)0;
		miArray[27].name="AtkDmgMax";miArray[27].type=MemberInfo.MIType.eInt;miArray[27].size=4;miArray[27].count=(int)0;
		miArray[28].name="Def";miArray[28].type=MemberInfo.MIType.eInt;miArray[28].size=4;miArray[28].count=(int)0;
		miArray[29].name="IceAtt";miArray[29].type=MemberInfo.MIType.eInt;miArray[29].size=4;miArray[29].count=(int)0;
		miArray[30].name="FireAtt";miArray[30].type=MemberInfo.MIType.eInt;miArray[30].size=4;miArray[30].count=(int)0;
		miArray[31].name="PoisonAtt";miArray[31].type=MemberInfo.MIType.eInt;miArray[31].size=4;miArray[31].count=(int)0;
		miArray[32].name="ThunderAtt";miArray[32].type=MemberInfo.MIType.eInt;miArray[32].size=4;miArray[32].count=(int)0;
		miArray[33].name="IceDef";miArray[33].type=MemberInfo.MIType.eInt;miArray[33].size=4;miArray[33].count=(int)0;
		miArray[34].name="FireDef";miArray[34].type=MemberInfo.MIType.eInt;miArray[34].size=4;miArray[34].count=(int)0;
		miArray[35].name="PoisonDef";miArray[35].type=MemberInfo.MIType.eInt;miArray[35].size=4;miArray[35].count=(int)0;
		miArray[36].name="ThunderDef";miArray[36].type=MemberInfo.MIType.eInt;miArray[36].size=4;miArray[36].count=(int)0;
		miArray[37].name="Pierce";miArray[37].type=MemberInfo.MIType.eInt;miArray[37].size=4;miArray[37].count=(int)0;
		miArray[38].name="Hit";miArray[38].type=MemberInfo.MIType.eInt;miArray[38].size=4;miArray[38].count=(int)0;
		miArray[39].name="Dodge";miArray[39].type=MemberInfo.MIType.eInt;miArray[39].size=4;miArray[39].count=(int)0;
		miArray[40].name="Cri";miArray[40].type=MemberInfo.MIType.eInt;miArray[40].size=4;miArray[40].count=(int)0;
		miArray[41].name="Flex";miArray[41].type=MemberInfo.MIType.eInt;miArray[41].size=4;miArray[41].count=(int)0;
		miArray[42].name="CriDmg";miArray[42].type=MemberInfo.MIType.eInt;miArray[42].size=4;miArray[42].count=(int)0;
		miArray[43].name="CriDmgDef";miArray[43].type=MemberInfo.MIType.eInt;miArray[43].size=4;miArray[43].count=(int)0;
		miArray[44].name="BodySize";miArray[44].type=MemberInfo.MIType.eInt;miArray[44].size=4;miArray[44].count=(int)0;
		miArray[45].name="AttSize";miArray[45].type=MemberInfo.MIType.eInt;miArray[45].size=4;miArray[45].count=(int)0;
		miArray[46].name="AttSpeedModPer";miArray[46].type=MemberInfo.MIType.eInt;miArray[46].size=4;miArray[46].count=(int)0;
		miArray[47].name="MoveSpeedModPer";miArray[47].type=MemberInfo.MIType.eInt;miArray[47].size=4;miArray[47].count=(int)0;
		miArray[48].name="PiercePer";miArray[48].type=MemberInfo.MIType.eInt;miArray[48].size=4;miArray[48].count=(int)0;
		miArray[49].name="HitPer";miArray[49].type=MemberInfo.MIType.eInt;miArray[49].size=4;miArray[49].count=(int)0;
		miArray[50].name="DodgePer";miArray[50].type=MemberInfo.MIType.eInt;miArray[50].size=4;miArray[50].count=(int)0;
		miArray[51].name="CriPer";miArray[51].type=MemberInfo.MIType.eInt;miArray[51].size=4;miArray[51].count=(int)0;
		miArray[52].name="FlexPer";miArray[52].type=MemberInfo.MIType.eInt;miArray[52].size=4;miArray[52].count=(int)0;
		miArray[53].name="MaxEP";miArray[53].type=MemberInfo.MIType.eInt;miArray[53].size=4;miArray[53].count=(int)0;
		miArray[54].name="AP";miArray[54].type=MemberInfo.MIType.eInt;miArray[54].size=4;miArray[54].count=(int)0;
		miArray[55].name="MaxAP";miArray[55].type=MemberInfo.MIType.eInt;miArray[55].size=4;miArray[55].count=(int)0;
		miArray[56].name="ExpPer";miArray[56].type=MemberInfo.MIType.eInt;miArray[56].size=4;miArray[56].count=(int)0;
		miArray[57].name="MoneyPer";miArray[57].type=MemberInfo.MIType.eInt;miArray[57].size=4;miArray[57].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ObjID;
		case	1:return m_DBID;
		case	2:return m_RoleIndex;
		case	3:return m_Name;
		case	4:return m_Level;
		case	5:return m_BaseJob;
		case	6:return m_Job;
		case	7:return m_Sex;
		case	8:return m_Experience;
		case	9:return m_NonMoney;
		case	10:return m_NonCash;
		case	11:return m_Cash;
		case	12:return m_HP;
		case	13:return m_SP;
		case	14:return m_EP;
		case	15:return m_MoveSpeed;
		case	16:return m_AttSpeed;
		case	17:return m_Str;
		case	18:return m_Int;
		case	19:return m_Dex;
		case	20:return m_Sta;
		case	21:return m_Fai;
		case	22:return m_MaxHP;
		case	23:return m_MaxSP;
		case	24:return m_HPTick;
		case	25:return m_SPTick;
		case	26:return m_AtkDmgMin;
		case	27:return m_AtkDmgMax;
		case	28:return m_Def;
		case	29:return m_IceAtt;
		case	30:return m_FireAtt;
		case	31:return m_PoisonAtt;
		case	32:return m_ThunderAtt;
		case	33:return m_IceDef;
		case	34:return m_FireDef;
		case	35:return m_PoisonDef;
		case	36:return m_ThunderDef;
		case	37:return m_Pierce;
		case	38:return m_Hit;
		case	39:return m_Dodge;
		case	40:return m_Cri;
		case	41:return m_Flex;
		case	42:return m_CriDmg;
		case	43:return m_CriDmgDef;
		case	44:return m_BodySize;
		case	45:return m_AttSize;
		case	46:return m_AttSpeedModPer;
		case	47:return m_MoveSpeedModPer;
		case	48:return m_PiercePer;
		case	49:return m_HitPer;
		case	50:return m_DodgePer;
		case	51:return m_CriPer;
		case	52:return m_FlexPer;
		case	53:return m_MaxEP;
		case	54:return m_AP;
		case	55:return m_MaxAP;
		case	56:return m_ExpPer;
		case	57:return m_MoneyPer;

		}
		return 0;
	}
};

/* 宠物信息结构  */
public class SPetInfo	:	CMessage{
	public	UInt64           m_DBID;	/// 唯一id           .
	public	int              m_BattlePos;	/// 出战位置 见EPetBattlePos，PET_BTPOS_UNKNOWN为不出战  .
	public	uint             m_TemplateID;	/// 模板id           .
	public	int              m_Level;	/// 当前等级         .
	public	Int64            m_Experience;	/// 当前累计经验     .
	public	int              m_Up;	/// 当前强化等级     .
	public	int              m_Lock;	/// 锁定状态         .
	public	int              m_HP;	/// 当前血量         .
	public	int              m_SP;	/// 当前技力         .
	public	int              m_Str;
	public	int              m_Int;
	public	int              m_Dex;
	public	int              m_Sta;
	public	int              m_Fai;
	public	int              m_MaxHP;
	public	int              m_MaxSP;
	public	int              m_HPTick;
	public	int              m_SPTick;
	public	int              m_AtkDmgMin;
	public	int              m_AtkDmgMax;
	public	int              m_Def;
	public	int              m_IceAtt;
	public	int              m_FireAtt;
	public	int              m_PoisonAtt;
	public	int              m_ThunderAtt;
	public	int              m_IceDef;
	public	int              m_FireDef;
	public	int              m_PoisonDef;
	public	int              m_ThunderDef;
	public	int              m_Pierce;
	public	int              m_Hit;
	public	int              m_Dodge;
	public	int              m_Cri;
	public	int              m_Flex;
	public	int              m_CriDmg;
	public	int              m_CriDmgDef;
	public	int              m_BodySize;
	public	int              m_AttSize;
	public	int              m_AttSpeedModPer;
	public	int              m_MoveSpeedModPer;
	public	int              m_PiercePer;
	public	int              m_HitPer;
	public	int              m_DodgePer;
	public	int              m_CriPer;
	public	int              m_FlexPer;
	public	int              m_AttSpeed;
	public	int              m_MoveSpeed;
	public	HeaderProto.SXITEMS m_Equip	=	new HeaderProto.SXITEMS();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[48];
		miArray[0].name="DBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="BattlePos";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="TemplateID";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Level";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Experience";miArray[4].type=MemberInfo.MIType.eInt64;miArray[4].size=8;miArray[4].count=(int)0;
		miArray[5].name="Up";miArray[5].type=MemberInfo.MIType.eInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Lock";miArray[6].type=MemberInfo.MIType.eInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="HP";miArray[7].type=MemberInfo.MIType.eInt;miArray[7].size=4;miArray[7].count=(int)0;
		miArray[8].name="SP";miArray[8].type=MemberInfo.MIType.eInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="Str";miArray[9].type=MemberInfo.MIType.eInt;miArray[9].size=4;miArray[9].count=(int)0;
		miArray[10].name="Int";miArray[10].type=MemberInfo.MIType.eInt;miArray[10].size=4;miArray[10].count=(int)0;
		miArray[11].name="Dex";miArray[11].type=MemberInfo.MIType.eInt;miArray[11].size=4;miArray[11].count=(int)0;
		miArray[12].name="Sta";miArray[12].type=MemberInfo.MIType.eInt;miArray[12].size=4;miArray[12].count=(int)0;
		miArray[13].name="Fai";miArray[13].type=MemberInfo.MIType.eInt;miArray[13].size=4;miArray[13].count=(int)0;
		miArray[14].name="MaxHP";miArray[14].type=MemberInfo.MIType.eInt;miArray[14].size=4;miArray[14].count=(int)0;
		miArray[15].name="MaxSP";miArray[15].type=MemberInfo.MIType.eInt;miArray[15].size=4;miArray[15].count=(int)0;
		miArray[16].name="HPTick";miArray[16].type=MemberInfo.MIType.eInt;miArray[16].size=4;miArray[16].count=(int)0;
		miArray[17].name="SPTick";miArray[17].type=MemberInfo.MIType.eInt;miArray[17].size=4;miArray[17].count=(int)0;
		miArray[18].name="AtkDmgMin";miArray[18].type=MemberInfo.MIType.eInt;miArray[18].size=4;miArray[18].count=(int)0;
		miArray[19].name="AtkDmgMax";miArray[19].type=MemberInfo.MIType.eInt;miArray[19].size=4;miArray[19].count=(int)0;
		miArray[20].name="Def";miArray[20].type=MemberInfo.MIType.eInt;miArray[20].size=4;miArray[20].count=(int)0;
		miArray[21].name="IceAtt";miArray[21].type=MemberInfo.MIType.eInt;miArray[21].size=4;miArray[21].count=(int)0;
		miArray[22].name="FireAtt";miArray[22].type=MemberInfo.MIType.eInt;miArray[22].size=4;miArray[22].count=(int)0;
		miArray[23].name="PoisonAtt";miArray[23].type=MemberInfo.MIType.eInt;miArray[23].size=4;miArray[23].count=(int)0;
		miArray[24].name="ThunderAtt";miArray[24].type=MemberInfo.MIType.eInt;miArray[24].size=4;miArray[24].count=(int)0;
		miArray[25].name="IceDef";miArray[25].type=MemberInfo.MIType.eInt;miArray[25].size=4;miArray[25].count=(int)0;
		miArray[26].name="FireDef";miArray[26].type=MemberInfo.MIType.eInt;miArray[26].size=4;miArray[26].count=(int)0;
		miArray[27].name="PoisonDef";miArray[27].type=MemberInfo.MIType.eInt;miArray[27].size=4;miArray[27].count=(int)0;
		miArray[28].name="ThunderDef";miArray[28].type=MemberInfo.MIType.eInt;miArray[28].size=4;miArray[28].count=(int)0;
		miArray[29].name="Pierce";miArray[29].type=MemberInfo.MIType.eInt;miArray[29].size=4;miArray[29].count=(int)0;
		miArray[30].name="Hit";miArray[30].type=MemberInfo.MIType.eInt;miArray[30].size=4;miArray[30].count=(int)0;
		miArray[31].name="Dodge";miArray[31].type=MemberInfo.MIType.eInt;miArray[31].size=4;miArray[31].count=(int)0;
		miArray[32].name="Cri";miArray[32].type=MemberInfo.MIType.eInt;miArray[32].size=4;miArray[32].count=(int)0;
		miArray[33].name="Flex";miArray[33].type=MemberInfo.MIType.eInt;miArray[33].size=4;miArray[33].count=(int)0;
		miArray[34].name="CriDmg";miArray[34].type=MemberInfo.MIType.eInt;miArray[34].size=4;miArray[34].count=(int)0;
		miArray[35].name="CriDmgDef";miArray[35].type=MemberInfo.MIType.eInt;miArray[35].size=4;miArray[35].count=(int)0;
		miArray[36].name="BodySize";miArray[36].type=MemberInfo.MIType.eInt;miArray[36].size=4;miArray[36].count=(int)0;
		miArray[37].name="AttSize";miArray[37].type=MemberInfo.MIType.eInt;miArray[37].size=4;miArray[37].count=(int)0;
		miArray[38].name="AttSpeedModPer";miArray[38].type=MemberInfo.MIType.eInt;miArray[38].size=4;miArray[38].count=(int)0;
		miArray[39].name="MoveSpeedModPer";miArray[39].type=MemberInfo.MIType.eInt;miArray[39].size=4;miArray[39].count=(int)0;
		miArray[40].name="PiercePer";miArray[40].type=MemberInfo.MIType.eInt;miArray[40].size=4;miArray[40].count=(int)0;
		miArray[41].name="HitPer";miArray[41].type=MemberInfo.MIType.eInt;miArray[41].size=4;miArray[41].count=(int)0;
		miArray[42].name="DodgePer";miArray[42].type=MemberInfo.MIType.eInt;miArray[42].size=4;miArray[42].count=(int)0;
		miArray[43].name="CriPer";miArray[43].type=MemberInfo.MIType.eInt;miArray[43].size=4;miArray[43].count=(int)0;
		miArray[44].name="FlexPer";miArray[44].type=MemberInfo.MIType.eInt;miArray[44].size=4;miArray[44].count=(int)0;
		miArray[45].name="AttSpeed";miArray[45].type=MemberInfo.MIType.eInt;miArray[45].size=4;miArray[45].count=(int)0;
		miArray[46].name="MoveSpeed";miArray[46].type=MemberInfo.MIType.eInt;miArray[46].size=4;miArray[46].count=(int)0;
		miArray[47].name="Equip";miArray[47].type=MemberInfo.MIType.eMessage;miArray[47].size=0;miArray[47].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBID;
		case	1:return m_BattlePos;
		case	2:return m_TemplateID;
		case	3:return m_Level;
		case	4:return m_Experience;
		case	5:return m_Up;
		case	6:return m_Lock;
		case	7:return m_HP;
		case	8:return m_SP;
		case	9:return m_Str;
		case	10:return m_Int;
		case	11:return m_Dex;
		case	12:return m_Sta;
		case	13:return m_Fai;
		case	14:return m_MaxHP;
		case	15:return m_MaxSP;
		case	16:return m_HPTick;
		case	17:return m_SPTick;
		case	18:return m_AtkDmgMin;
		case	19:return m_AtkDmgMax;
		case	20:return m_Def;
		case	21:return m_IceAtt;
		case	22:return m_FireAtt;
		case	23:return m_PoisonAtt;
		case	24:return m_ThunderAtt;
		case	25:return m_IceDef;
		case	26:return m_FireDef;
		case	27:return m_PoisonDef;
		case	28:return m_ThunderDef;
		case	29:return m_Pierce;
		case	30:return m_Hit;
		case	31:return m_Dodge;
		case	32:return m_Cri;
		case	33:return m_Flex;
		case	34:return m_CriDmg;
		case	35:return m_CriDmgDef;
		case	36:return m_BodySize;
		case	37:return m_AttSize;
		case	38:return m_AttSpeedModPer;
		case	39:return m_MoveSpeedModPer;
		case	40:return m_PiercePer;
		case	41:return m_HitPer;
		case	42:return m_DodgePer;
		case	43:return m_CriPer;
		case	44:return m_FlexPer;
		case	45:return m_AttSpeed;
		case	46:return m_MoveSpeed;
		case	47:return m_Equip;

		}
		return 0;
	}
};

/* 宠物属性更新，需要修改策略  */
public class PET_RECORD	:	CMessage{
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

/* 角色宠物列表  */
public class SC_PETS_RECORD_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PETS_RECORD_NTF;}
	public	ushort           m_PetRecordCount;	/// 拥有宠物个数     .
	public	PET_RECORD      [] m_Record;
	public SC_PETS_RECORD_NTF()
	{
		m_Record=new PET_RECORD[(int)HeaderProto.MAX_PET_RECORD_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="PetRecordCount";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="Record";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_PET_RECORD_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PetRecordCount;
		case	1:return m_Record;

		}
		return 0;
	}
};

/* 角色宠物列表  */
public class SC_USER_PETS_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_USER_PETS_NTF;}
	public	int              m_MaxPetCount;	/// 最多拥有宠物个数  .
	public	ushort           m_PetCount;	/// 拥有宠物个数     .
	public	SPetInfo        [] m_PetsInfo;
	public SC_USER_PETS_NTF()
	{
		m_PetsInfo=new SPetInfo[(int)HeaderProto.MAX_PET_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="MaxPetCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="PetCount";miArray[1].type=MemberInfo.MIType.eUShort;miArray[1].size=2;miArray[1].count=(int)0;
		miArray[2].name="PetsInfo";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)HeaderProto.MAX_PET_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MaxPetCount;
		case	1:return m_PetCount;
		case	2:return m_PetsInfo;

		}
		return 0;
	}
};

/* 角色宠物列表  */
public class SC_USER_JOIN_PETS	:	CMessage{
	public	ushort           m_PetCount;	/// 拥有宠物个数     .
	public	SPetInfo        [] m_PetsInfo;
	public SC_USER_JOIN_PETS()
	{
		m_PetsInfo=new SPetInfo[(int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="PetCount";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="PetsInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PetCount;
		case	1:return m_PetsInfo;

		}
		return 0;
	}
};

/* 宠物属性更新，需要修改策略  */
public class SC_PET_INFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_INFO_NTF;}
	public	SPetInfo         m_PetInfo	=	new SPetInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="PetInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PetInfo;

		}
		return 0;
	}
};

/* 宠物属性更新，需要修改策略  */
public class SC_PET_SINGLE_ENTER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_SINGLE_ENTER_NTF;}
	public	SPetInfo         m_PetInfo	=	new SPetInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="PetInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PetInfo;

		}
		return 0;
	}
};

/* 宠物属性更新，需要修改策略  */
public class SC_PET_SINGLE_LEAVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_SINGLE_LEAVE_NTF;}
	public	UInt64           m_DBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="DBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBID;

		}
		return 0;
	}
};

/*   */
public class SC_GATHER_ITEM	:	CMessage{
	public	int              m_ID;
	public	int              m_CT;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="CT";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ID;
		case	1:return m_CT;

		}
		return 0;
	}
};

/* 收集品列表  */
public class SC_GATHER_INFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GATHER_INFO_NTF;}
	public	int              m_Count;
	public	SC_GATHER_ITEM  [] m_Gathers;
	public SC_GATHER_INFO_NTF()
	{
		m_Gathers=new SC_GATHER_ITEM[(int)MAX_GATHER_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Gathers";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)MAX_GATHER_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Gathers;

		}
		return 0;
	}
};

/* 收集品单条更新或者增加  */
public class SC_GATHER_SINGLE_ENTER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GATHER_SINGLE_ENTER_NTF;}
	public	int              m_ID;
	public	int              m_CT;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="CT";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ID;
		case	1:return m_CT;

		}
		return 0;
	}
};

/* 收集品销毁  */
public class SC_GATHER_SINGLE_LEAVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GATHER_SINGLE_LEAVE_NTF;}
	public	int              m_ID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ID;

		}
		return 0;
	}
};

/* 物品合成  */
public class CS_GATHER_ITEM_MERGE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GATHER_ITEM_MERGE_REQ;}
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

/* 物品合成回应  */
public class SC_GATHER_ITEM_MERGE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GATHER_ITEM_MERGE_ACK;}
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

/* 物品制作  */
public class CS_ROLE_PROD_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_PROD_REQ;}
	public	UInt64           m_DBID;
	public	int              m_Count;
	public	UInt64          [] m_OtherDBID;
	public CS_ROLE_PROD_REQ()
	{
		m_OtherDBID=new UInt64[(int)HeaderProto.MAX_PROD_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="DBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="OtherDBID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)HeaderProto.MAX_PROD_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBID;
		case	1:return m_Count;
		case	2:return m_OtherDBID;

		}
		return 0;
	}
};

/* 物品制作回应  */
public class SC_ROLE_PROD_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_PROD_ACK;}
	public	HeaderProto.SXITEMS m_Items	=	new HeaderProto.SXITEMS();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Items";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Items;

		}
		return 0;
	}
};

/* 宠物经验获取  */
public class CS_PET_GET_EXP_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_GET_EXP_RPT;}
	public	UInt64           m_OwnerDBID;
	public	int              m_OtherCount;
	public	UInt64          [] m_OtherDBID;
	public CS_PET_GET_EXP_RPT()
	{
		m_OtherDBID=new UInt64[(int)HeaderProto.MAX_PET_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="OwnerDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="OtherCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="OtherDBID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)HeaderProto.MAX_PET_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_OwnerDBID;
		case	1:return m_OtherCount;
		case	2:return m_OtherDBID;

		}
		return 0;
	}
};

/* 宠物经验获取  */
public class SC_PET_GET_EXP_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_GET_EXP_ACK;}
	public	UInt64           m_OwnerDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="OwnerDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_OwnerDBID;

		}
		return 0;
	}
};

/* 宠物升级  */
public class SC_PET_LEVEL_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_LEVEL_NTF;}
	public	UInt64           m_DBID;
	public	int              m_Level;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="DBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Level";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBID;
		case	1:return m_Level;

		}
		return 0;
	}
};

/* 宠物强化  */
public class CS_PET_UP_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_UP_REQ;}
	public	UInt64           m_OwnerDBID;
	public	UInt64           m_OtherDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="OwnerDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="OtherDBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_OwnerDBID;
		case	1:return m_OtherDBID;

		}
		return 0;
	}
};

/* 宠物强化  */
public class SC_PET_UP_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_UP_ACK;}
	public	UInt64           m_DBID;
	public	int              m_Up;
	public	byte             m_Ok;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="DBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Up";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Ok";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DBID;
		case	1:return m_Up;
		case	2:return m_Ok;

		}
		return 0;
	}
};

/* 设置出战宠物  */
public class CS_SET_BATTLE_PET_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SET_BATTLE_PET_REQ;}
	public	int              m_BattlePos;	/// 出战位置 见EPetBattlePos .
	public	UInt64           m_DBID;	/// 唯一id           .
	public	byte             m_Ok;	/// 出战1 取消出战0   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="BattlePos";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="DBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Ok";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattlePos;
		case	1:return m_DBID;
		case	2:return m_Ok;

		}
		return 0;
	}
};

/* 通知宠物出战  */
public class SC_SET_BATTLE_PET_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SET_BATTLE_PET_NTF;}
	public	int              m_BattlePos;	/// 出战位置 见EPetBattlePos PET_BTPOS_UNKNOWN为不出战  .
	public	UInt64           m_DBID;	/// 唯一id 为INVALID_64BIT_VALUE表示取消该位置的出战宠物  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="BattlePos";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="DBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BattlePos;
		case	1:return m_DBID;

		}
		return 0;
	}
};

/* 宠物融合  */
public class CS_PET_MERGE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_MERGE_REQ;}
	public	UInt64           m_OwnerDBID;
	public	UInt64           m_OtherDBID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="OwnerDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="OtherDBID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_OwnerDBID;
		case	1:return m_OtherDBID;

		}
		return 0;
	}
};

/* 获得宠物  */
public class SC_PET_ADD_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_ADD_NTF;}
	public	int              m_TemplateID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="TemplateID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

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

/* 场景内其他角色信息 */
public class SOtherInfo	:	CMessage{
	public	UInt64           m_ObjID;	/// 出现对象ID 临时id  .
	public	HeaderProto.SRoleInfoWithEquip m_RoleInfo	=	new HeaderProto.SRoleInfoWithEquip();	/// 角色信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ObjID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="RoleInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ObjID;
		case	1:return m_RoleInfo;

		}
		return 0;
	}
};

/* 场景内其他角色信息 */
public class SOtherInfoWithPos	:	CMessage{
	public	SOtherInfo       m_RoleInfo	=	new SOtherInfo();	/// 角色信息         .
	public	HeaderProto.SPosInfo m_Pos	=	new HeaderProto.SPosInfo();	/// 场景位置         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RoleInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="Pos";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleInfo;
		case	1:return m_Pos;

		}
		return 0;
	}
};

/* 场景内出现其他角色 */
public class SC_OTHER_APPEAR	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_OTHER_APPEAR;}
	public	ushort           m_Count;	/// 数量             .
	public	SOtherInfoWithPos[] m_RoleInfo;	/// 角色信息         .
	public SC_OTHER_APPEAR()
	{
		m_RoleInfo=new SOtherInfoWithPos[(int)GS_MAX_SYNC_ROLE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="RoleInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)GS_MAX_SYNC_ROLE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_RoleInfo;

		}
		return 0;
	}
};

/* 更新场景内角色的avatar等信息 */
public class SC_OTHER_UPDATE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_OTHER_UPDATE;}
	public	ushort           m_Count;	/// 数量             .
	public	SOtherInfo      [] m_RoleInfo;	/// 角色信息         .
	public SC_OTHER_UPDATE()
	{
		m_RoleInfo=new SOtherInfo[(int)GS_MAX_SYNC_ROLE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="RoleInfo";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)GS_MAX_SYNC_ROLE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_RoleInfo;

		}
		return 0;
	}
};

/* 场景内其他角色消失 */
public class SC_OTHER_DISAPPEAR	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_OTHER_DISAPPEAR;}
	public	ushort           m_Count;	/// 数量             .
	public	UInt64          [] m_ObjID;	/// 出现对象ID 临时id  .
	public SC_OTHER_DISAPPEAR()
	{
		m_ObjID=new UInt64[(int)GS_MAX_SYNC_ROLE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="ObjID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)GS_MAX_SYNC_ROLE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_ObjID;

		}
		return 0;
	}
};

/* 更改移动同步模式 */
public class CS_MOVE_SYNC_MODE	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_MOVE_SYNC_MODE;}
	public	int              m_Mode;	/// 见：EMoveSyncType .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Mode";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Mode;

		}
		return 0;
	}
};

/* 开始移动 */
public class CS_MOVE_BEGIN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_MOVE_BEGIN;}
	public	UInt64           m_Time;	/// 当前场景的同步时间,单位ms .
	public	HeaderProto.SPosInfo m_Position	=	new HeaderProto.SPosInfo();	/// 当前位置         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Position";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_Position;

		}
		return 0;
	}
};

/* 停止移动 */
public class CS_MOVE_STOP	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_MOVE_STOP;}
	public	UInt64           m_Time;	/// 当前场景的同步时间,单位ms .
	public	HeaderProto.SPosInfo m_Position	=	new HeaderProto.SPosInfo();	/// 停止位置         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Position";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;
		case	1:return m_Position;

		}
		return 0;
	}
};

public class SMoveObj	:	CMessage{
	public	UInt64           m_ObjectID;	/// 场景内对象的唯一id .
	public	UInt64           m_Time;	/// 当前场景的同步时间,单位ms .
	public	HeaderProto.SPosInfo m_Position	=	new HeaderProto.SPosInfo();	/// 当前位置         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ObjectID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Time";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Position";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ObjectID;
		case	1:return m_Time;
		case	2:return m_Position;

		}
		return 0;
	}
};

/* 开始移动 */
public class SC_MOVE_BEGIN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_MOVE_BEGIN;}
	public	ushort           m_Count;	/// 对象数量         .
	public	SMoveObj        [] m_Objs;	/// 其他移动信息     .
	public SC_MOVE_BEGIN()
	{
		m_Objs=new SMoveObj[(int)GS_MAX_SYNC_ROLE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="Objs";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)GS_MAX_SYNC_ROLE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Objs;

		}
		return 0;
	}
};

/* 停止移动 */
public class SC_MOVE_STOP	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_MOVE_STOP;}
	public	ushort           m_Count;	/// 对象数量         .
	public	SMoveObj        [] m_Objs;	/// 其他移动信息     .
	public SC_MOVE_STOP()
	{
		m_Objs=new SMoveObj[(int)GS_MAX_SYNC_ROLE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUShort;miArray[0].size=2;miArray[0].count=(int)0;
		miArray[1].name="Objs";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)GS_MAX_SYNC_ROLE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Objs;

		}
		return 0;
	}
};

/* 技能信息结构  */
public class SSkillInfo	:	CMessage{
	public	uint             m_SkillID;	/// 技能ID           .
	public	uint             m_CooldownTime;	/// 剩余冷却时间     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="SkillID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="CooldownTime";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillID;
		case	1:return m_CooldownTime;

		}
		return 0;
	}
};

/* 角色技能列表  */
public class SC_USER_SKILLS_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_USER_SKILLS_NTF;}
	public	uint             m_SkillPoint;	/// 当前技能点       .
	public	uint             m_TotalSkillTreePoint;	/// 总的已花费的技能点  .
	public	byte             m_Count;	/// 数量             .
	public	SSkillInfo      [] m_SkillsInfo;
	public SC_USER_SKILLS_NTF()
	{
		m_SkillsInfo=new SSkillInfo[(int)HeaderProto.MAX_TRANS_SKILL_NUM];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="SkillPoint";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="TotalSkillTreePoint";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Count";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="SkillsInfo";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)HeaderProto.MAX_TRANS_SKILL_NUM;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillPoint;
		case	1:return m_TotalSkillTreePoint;
		case	2:return m_Count;
		case	3:return m_SkillsInfo;

		}
		return 0;
	}
};

/* 学习技能请求  */
public class CS_LEARN_SKILL	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_LEARN_SKILL;}
	public	uint             m_SkillID;	/// 技能ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="SkillID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillID;

		}
		return 0;
	}
};

/* 技能信息，有则更新，无则添加  */
public class SC_SKILL_INFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SKILL_INFO_NTF;}
	public	SSkillInfo       m_Info	=	new SSkillInfo();	/// 技能信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 删除技能  */
public class SC_REMOVE_SKILL_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_REMOVE_SKILL_NTF;}
	public	uint             m_SkillID;	/// 删除的技能id，为全F表示删除全部技能  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="SkillID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillID;

		}
		return 0;
	}
};

/* 技能点通知  */
public class SC_SKILL_POINT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SKILL_POINT_NTF;}
	public	uint             m_SkillPoint;	/// 当前技能点       .
	public	uint             m_TotalSkillTreePoint;	/// 总的已花费的技能点  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="SkillPoint";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="TotalSkillTreePoint";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SkillPoint;
		case	1:return m_TotalSkillTreePoint;

		}
		return 0;
	}
};

/* 重置技能  */
public class CS_SKILL_RESET_ALL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SKILL_RESET_ALL_REQ;}
};

/* 自己装备列表 */
public class SC_SELF_ITEM_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_ITEM_NTF;}
	public	byte             m_Pos;	/// 见 EItemPos      .
	public	HeaderProto.SItemList m_ItemList	=	new HeaderProto.SItemList();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Pos";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemList";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Pos;
		case	1:return m_ItemList;

		}
		return 0;
	}
};

/* 请求转移物品 */
public class CS_MOVE_ITEM_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_MOVE_ITEM_REQ;}
	public	byte             m_FromPos;	/// 见 EItemPos      .
	public	byte             m_ToPos;	/// 见 EItemPos      .
	public	UInt64           m_UUID;
	public	UInt64           m_AnotherUUID;	/// -1表示非交换操作， 非-1表示交换操作 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="FromPos";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ToPos";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="UUID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;
		miArray[3].name="AnotherUUID";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_FromPos;
		case	1:return m_ToPos;
		case	2:return m_UUID;
		case	3:return m_AnotherUUID;

		}
		return 0;
	}
};

/* 转移物品反馈 */
public class SC_MOVE_ITEM_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_MOVE_ITEM_ACK;}
	public	CS_MOVE_ITEM_REQ m_Info	=	new CS_MOVE_ITEM_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 角色背包同步 */
public class SC_ROLE_BAG_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_BAG_NTF;}
	public	int              m_MaxRoleBagCount;
	public	HeaderProto.SXITEMS m_Items	=	new HeaderProto.SXITEMS();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="MaxRoleBagCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Items";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MaxRoleBagCount;
		case	1:return m_Items;

		}
		return 0;
	}
};

/* 角色上限同步 */
public class SC_ROLE_LIMIT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_LIMIT_NTF;}
	public	int              m_MaxRoleBagCount;
	public	int              m_MaxPetBagCount;
	public	int              m_MaxPetCount;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="MaxRoleBagCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="MaxPetBagCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="MaxPetCount";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MaxRoleBagCount;
		case	1:return m_MaxPetBagCount;
		case	2:return m_MaxPetCount;

		}
		return 0;
	}
};

/* 角色装备同步 */
public class SC_ROLE_EQUIP_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_EQUIP_NTF;}
	public	HeaderProto.SXITEMS m_Items	=	new HeaderProto.SXITEMS();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Items";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Items;

		}
		return 0;
	}
};

/* 角色背包物品销毁 */
public class SC_ROLE_BAG_SINGLE_LEAVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_LEAVE_NTF;}
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;

		}
		return 0;
	}
};

/* 角色背包物品出现或刷新 */
public class SC_ROLE_BAG_SINGLE_ENTER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_ENTER_NTF;}
	public	HeaderProto.SXITEM m_Item	=	new HeaderProto.SXITEM();
	public	int              m_EnterType;	/// 参见EItemEnterType .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Item";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;
		miArray[1].name="EnterType";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Item;
		case	1:return m_EnterType;

		}
		return 0;
	}
};

/* 角色装备物品销毁 */
public class SC_ROLE_EQUIP_SINGLE_LEAVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF;}
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;

		}
		return 0;
	}
};

/* 角色装备物品出现或刷新 */
public class SC_ROLE_EQUIP_SINGLE_ENTER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_ENTER_NTF;}
	public	HeaderProto.SXITEM m_Item	=	new HeaderProto.SXITEM();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Item";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Item;

		}
		return 0;
	}
};

/* 请求装备物品 */
public class CS_ROLE_EQUIP_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_EQUIP_REQ;}
	public	UInt64           m_ItemUUID;
	public	byte             m_IsEquip;	/// 1代表装备 0代表卸载 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="IsEquip";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_IsEquip;

		}
		return 0;
	}
};

/* 请求处理某物品 */
public class CS_ROLE_EVENT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_EVENT_REQ;}
	public	byte             m_EventType;	/// 参见ERoleItemEvent .
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="EventType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EventType;
		case	1:return m_ItemUUID;

		}
		return 0;
	}
};

/* 请求强化某物品 */
public class CS_ROLE_ITEMUP_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_ITEMUP_RPT;}
	public	byte             m_UpType;	/// 参见EItemUpEvent .
	public	UInt64           m_ItemUUID;
	public	byte             m_Count;
	public	UInt64          [] m_OtherItemUUID;
	public CS_ROLE_ITEMUP_RPT()
	{
		m_OtherItemUUID=new UInt64[(int)HeaderProto.MAX_ITEM_UP_USE_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="UpType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Count";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="OtherItemUUID";miArray[3].type=MemberInfo.MIType.eUInt64;miArray[3].size=8;miArray[3].count=(int)HeaderProto.MAX_ITEM_UP_USE_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UpType;
		case	1:return m_ItemUUID;
		case	2:return m_Count;
		case	3:return m_OtherItemUUID;

		}
		return 0;
	}
};

/* 请求强化某物品 */
public class SC_ROLE_ITEMUP_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_ITEMUP_ACK;}
	public	byte             m_UpType;	/// 参见EItemUpEvent .
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="UpType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UpType;
		case	1:return m_ItemUUID;

		}
		return 0;
	}
};

/* 强化某物品回应 */
public class SC_ROLE_ITEMUP_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_ITEMUP_NTF;}
	public	UInt64           m_ItemUUID;	/// 物品UUID         .
	public	int              m_Up;	/// 砸多少           .
	public	int              m_CurUp;	/// 当前Up如果=-1说明爆了 .
	public	byte             m_OK;	/// 成功失败         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Up";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="CurUp";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="OK";miArray[3].type=MemberInfo.MIType.eByte;miArray[3].size=1;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_Up;
		case	2:return m_CurUp;
		case	3:return m_OK;

		}
		return 0;
	}
};

/* 强化某物品回应 */
public class SC_ROLE_SUIT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_SUIT_NTF;}
	public	int              m_Count;	/// 套装数量         .
	public	HeaderProto.SROLE_SUIT[] m_Suit;	/// 套装物品         .
	public SC_ROLE_SUIT_NTF()
	{
		m_Suit=new HeaderProto.SROLE_SUIT[(int)HeaderProto.MAX_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Suit";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Suit;

		}
		return 0;
	}
};

/* 镶嵌宝石 */
public class SROLE_HOLE_ON	:	CMessage{
	public	byte             m_HoleIndex;	/// 宝石镶嵌位下标索引 .
	public	UInt64           m_ItemUUID;
	public	UInt64           m_OtherUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="HoleIndex";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="OtherUUID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_HoleIndex;
		case	1:return m_ItemUUID;
		case	2:return m_OtherUUID;

		}
		return 0;
	}
};

/* 请求镶嵌宝石 */
public class CS_ROLE_HOLE_ON_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_HOLE_ON_REQ;}
	public	SROLE_HOLE_ON    m_Hole	=	new SROLE_HOLE_ON();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Hole";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Hole;

		}
		return 0;
	}
};

/* 镶嵌宝石回应 */
public class SC_ROLE_HOLE_ON_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_HOLE_ON_ACK;}
	public	int              m_Error;
	public	SROLE_HOLE_ON    m_Hole	=	new SROLE_HOLE_ON();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Hole";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_Hole;

		}
		return 0;
	}
};

/* 拆卸宝石 */
public class SROLE_HOLE_OFF	:	CMessage{
	public	byte             m_HoleIndex;	/// 宝石镶嵌位下标索引 .
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="HoleIndex";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_HoleIndex;
		case	1:return m_ItemUUID;

		}
		return 0;
	}
};

/* 拆除宝石 */
public class CS_ROLE_HOLE_OFF_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_HOLE_OFF_REQ;}
	public	SROLE_HOLE_OFF   m_Hole	=	new SROLE_HOLE_OFF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Hole";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Hole;

		}
		return 0;
	}
};

/* 拆除宝石回应 */
public class SC_ROLE_HOLE_OFF_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_HOLE_OFF_ACK;}
	public	int              m_Error;
	public	SROLE_HOLE_OFF   m_Hole	=	new SROLE_HOLE_OFF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Hole";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_Hole;

		}
		return 0;
	}
};

/* 置换宝石 */
public class SROLE_HOLE_REFRESH	:	CMessage{
	public	UInt64           m_ItemUUID;
	public	UInt64           m_Item2UUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Item2UUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_Item2UUID;

		}
		return 0;
	}
};

/* 置换宝石 */
public class CS_ROLE_HOLE_REFRESH_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_HOLE_REFRESH_REQ;}
	public	SROLE_HOLE_REFRESH m_Hole	=	new SROLE_HOLE_REFRESH();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Hole";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Hole;

		}
		return 0;
	}
};

/* 置换宝石回应 */
public class SC_ROLE_HOLE_REFRESH_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_HOLE_REFRESH_ACK;}
	public	int              m_Error;
	public	SDropInfoList    m_Drops	=	new SDropInfoList();
	public	SROLE_HOLE_REFRESH m_Hole	=	new SROLE_HOLE_REFRESH();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Drops";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Hole";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_Drops;
		case	2:return m_Hole;

		}
		return 0;
	}
};

/* 合成宝石 */
public class SROLE_HOLE_MERGE	:	CMessage{
	public	UInt64           m_ItemUUID;
	public	UInt64           m_Item2UUID;
	public	UInt64           m_Item3UUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Item2UUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="Item3UUID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_Item2UUID;
		case	2:return m_Item3UUID;

		}
		return 0;
	}
};

/* 合成宝石 */
public class CS_ROLE_HOLE_MERGE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_HOLE_MERGE_REQ;}
	public	SROLE_HOLE_MERGE m_Hole	=	new SROLE_HOLE_MERGE();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Hole";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Hole;

		}
		return 0;
	}
};

/* 合成宝石回应 */
public class SC_ROLE_HOLE_MERGE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_HOLE_MERGE_ACK;}
	public	int              m_Error;
	public	SDropInfoList    m_Drops	=	new SDropInfoList();
	public	SROLE_HOLE_MERGE m_Hole	=	new SROLE_HOLE_MERGE();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Drops";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Hole";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_Drops;
		case	2:return m_Hole;

		}
		return 0;
	}
};

/* 合成宝石 */
public class SROLE_HOLE_MERGE2	:	CMessage{
	public	UInt64           m_ItemUUID;
	public	int              m_Count;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="Count";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_Count;

		}
		return 0;
	}
};

/* 合成宝石 */
public class CS_ROLE_HOLE_MERGE2_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_HOLE_MERGE2_REQ;}
	public	SROLE_HOLE_MERGE2 m_Hole	=	new SROLE_HOLE_MERGE2();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Hole";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Hole;

		}
		return 0;
	}
};

/* 合成宝石回应 */
public class SC_ROLE_HOLE_MERGE2_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_HOLE_MERGE2_ACK;}
	public	int              m_Error;
	public	SDropInfoList    m_Drops	=	new SDropInfoList();
	public	SROLE_HOLE_MERGE2 m_Hole	=	new SROLE_HOLE_MERGE2();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Drops";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Hole";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_Drops;
		case	2:return m_Hole;

		}
		return 0;
	}
};

/* 合成宝石回应 */
public class CS_LOCK_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_LOCK_RPT;}
	public	int              m_LockType;	/// 参见ELockType    .
	public	int              m_Lock;
	public	UInt64           m_UUID;	/// 唯一ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="LockType";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Lock";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="UUID";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_LockType;
		case	1:return m_Lock;
		case	2:return m_UUID;

		}
		return 0;
	}
};

/* 使用角色背包物品 */
public class CS_ROLE_USEITEM_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_USEITEM_REQ;}
	public	UInt64           m_UUID;	/// 唯一ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="UUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UUID;

		}
		return 0;
	}
};

/* 使用角色背包物品回应 */
public class SC_ROLE_USEITEM_START_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_USEITEM_START_ACK;}
	public	int              m_Error;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;

		}
		return 0;
	}
};

/* 使用角色背包物品回应 */
public class SC_ROLE_USEITEM_END_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_USEITEM_END_ACK;}
	public	int              m_Error;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;

		}
		return 0;
	}
};

/* 使用宠物背包物品 */
public class CS_PET_USEITEM_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_USEITEM_REQ;}
	public	UInt64           m_UUID;	/// 唯一ID           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="UUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UUID;

		}
		return 0;
	}
};

/* 使用宠物背包物品回应 */
public class SC_PET_USEITEM_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_USEITEM_ACK;}
};

/* 宠物背包同步 */
public class SC_PET_BAG_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_BAG_NTF;}
	public	int              m_MaxPetBagCount;
	public	HeaderProto.SXITEMS m_Items	=	new HeaderProto.SXITEMS();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="MaxPetBagCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Items";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MaxPetBagCount;
		case	1:return m_Items;

		}
		return 0;
	}
};

/* 宠物背包物品销毁 */
public class SC_PET_BAG_SINGLE_LEAVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_LEAVE_NTF;}
	public	UInt64           m_ItemUUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;

		}
		return 0;
	}
};

/* 宠物背包物品出现 */
public class SC_PET_BAG_SINGLE_ENTER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_ENTER_NTF;}
	public	HeaderProto.SXITEM m_Item	=	new HeaderProto.SXITEM();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Item";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Item;

		}
		return 0;
	}
};

/* 请求装备物品 */
public class CS_PET_EQUIP_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_EQUIP_REQ;}
	public	UInt64           m_ItemUUID;
	public	UInt64           m_PetUUID;
	public	byte             m_IsEquip;	/// 1代表装备 0代表卸载 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="ItemUUID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="PetUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;
		miArray[2].name="IsEquip";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ItemUUID;
		case	1:return m_PetUUID;
		case	2:return m_IsEquip;

		}
		return 0;
	}
};

/* 请求处理某物品 */
public class CS_PET_EVENT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_EVENT_REQ;}
	public	byte             m_EventType;	/// 参见EPetEvent    .
	public	UInt64           m_UUID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="EventType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="UUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EventType;
		case	1:return m_UUID;

		}
		return 0;
	}
};

/* 请求处理某物品 */
public class SC_PET_EVENT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_EVENT_ACK;}
	public	byte             m_EventType;	/// 参见EPetEvent    .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="EventType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EventType;

		}
		return 0;
	}
};

/* 切换队伍 */
public class CS_PET_TEAM_RPT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PET_TEAM_RPT;}
	public	int              m_Team;	/// 参见EPetTeam     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Team";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Team;

		}
		return 0;
	}
};

/* 切换队伍 */
public class SC_PET_TEAM_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PET_TEAM_NTF;}
	public	int              m_Team;	/// 参见EPetTeam     .
	public	UInt64          [] m_PetUUID;	/// 物品UUID         .
	public SC_PET_TEAM_NTF()
	{
		m_PetUUID=new UInt64[(int)HeaderProto.EPetBattlePos.PET_BTPOS_MAX];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Team";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="PetUUID";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)HeaderProto.EPetBattlePos.PET_BTPOS_MAX;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Team;
		case	1:return m_PetUUID;

		}
		return 0;
	}
};

public class CS_CHAT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_CHAT;}
	public	HeaderProto.SCHAT m_Chat	=	new HeaderProto.SCHAT();	/// chat数据         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Chat";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Chat;

		}
		return 0;
	}
};

public class SC_CHAT	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_CHAT;}
	public	HeaderProto.SCHAT m_Chat	=	new HeaderProto.SCHAT();	/// chat数据         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Chat";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Chat;

		}
		return 0;
	}
};

public class SC_CHAT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_CHAT_ACK;}
	public	byte             m_Error;	/// 错误id, 当有错误发生时才返回ack 1冷却时间限制，2等级限制 .
	public	uint             m_CooldownTime;	/// 世界聊天剩余冷却时间 毫秒 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Error";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="CooldownTime";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Error;
		case	1:return m_CooldownTime;

		}
		return 0;
	}
};

/* 到刷新时间时，客户端请求一次此消息，回SC_FRIENDS_LIST_NTF和SC_FRIEND_EPINFO_NTF */
public class CS_FRIENDS_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_FRIENDS_LIST_REQ;}
};

/* 好友列表通知 */
public class SC_FRIENDS_LIST_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_FRIENDS_LIST_NTF;}
	public	HeaderProto.FRIENDS_LIST_NTF m_Info	=	new HeaderProto.FRIENDS_LIST_NTF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 好友体力领取次数、体力赠送次数信息通知，上线通知，VIP改变时通知一次，其他情况客户端算 */
public class SC_FRIEND_EPINFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_FRIEND_EPINFO_NTF;}
	public	int              m_EPSent;	/// EP已赠送次数     .
	public	int              m_EPSendMax;	/// EP赠送最大值     .
	public	int              m_EPRecv;	/// EP已接收次数     .
	public	int              m_EPRecvMax;	/// EP收次数最大值   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="EPSent";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="EPSendMax";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="EPRecv";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="EPRecvMax";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_EPSent;
		case	1:return m_EPSendMax;
		case	2:return m_EPRecv;
		case	3:return m_EPRecvMax;

		}
		return 0;
	}
};

/* 查询指定名字角色 */
public class CS_QUERY_ROLE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_QUERY_ROLE_REQ;}
	public	HeaderProto.QUERY_ROLE_REQ m_Info	=	new HeaderProto.QUERY_ROLE_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 查询指定名字角色反馈 */
public class SC_QUERY_ROLE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_QUERY_ROLE_ACK;}
	public	HeaderProto.QUERY_ROLE_ACK m_Info	=	new HeaderProto.QUERY_ROLE_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 添加好友 */
public class CS_ADD_FRIEND_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ADD_FRIEND_REQ;}
	public	HeaderProto.ADD_FRIEND_REQ m_Info	=	new HeaderProto.ADD_FRIEND_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 添加好友反馈 */
public class SC_ADD_FRIEND_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ADD_FRIEND_ACK;}
	public	HeaderProto.ADD_FRIEND_ACK m_Info	=	new HeaderProto.ADD_FRIEND_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 删除好友 */
public class CS_REMOVE_FRIEND_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_REMOVE_FRIEND_REQ;}
	public	HeaderProto.REMOVE_FRIEND_REQ m_Info	=	new HeaderProto.REMOVE_FRIEND_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 删除好友反馈 */
public class SC_REMOVE_FRIEND_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_REMOVE_FRIEND_ACK;}
	public	HeaderProto.REMOVE_FRIEND_ACK m_Info	=	new HeaderProto.REMOVE_FRIEND_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 赠送体力 */
public class CS_DONATE_ACTION_POINT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_DONATE_ACTION_POINT_REQ;}
	public	HeaderProto.DONATE_ACTION_POINT_REQ m_Info	=	new HeaderProto.DONATE_ACTION_POINT_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 赠送体力反馈 */
public class SC_DONATE_ACTION_POINT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_ACK;}
	public	HeaderProto.DONATE_ACTION_POINT_ACK m_Info	=	new HeaderProto.DONATE_ACTION_POINT_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 赠送体力通知被赠送者 */
public class SC_DONATE_ACTION_POINT_OTHER_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_OTHER_NTF;}
	public	HeaderProto.DONATE_ACTION_POINT_OTHER_NTF m_Info	=	new HeaderProto.DONATE_ACTION_POINT_OTHER_NTF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 领取体力 */
public class CS_RECEIVE_ACTION_POINT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_RECEIVE_ACTION_POINT_REQ;}
	public	HeaderProto.RECEIVE_ACTION_POINT_REQ m_Info	=	new HeaderProto.RECEIVE_ACTION_POINT_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 领取体力反馈 */
public class SC_RECEIVE_ACTION_POINT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_RECEIVE_ACTION_POINT_ACK;}
	public	HeaderProto.RECEIVE_ACTION_POINT_ACK m_Info	=	new HeaderProto.RECEIVE_ACTION_POINT_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 回应添加好友请求 */
public class CS_RESPONSE_ADD_FRIEND_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_RESPONSE_ADD_FRIEND_REQ;}
	public	HeaderProto.RESPONSE_ADD_FRIEND_REQ m_Info	=	new HeaderProto.RESPONSE_ADD_FRIEND_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 别人请求成为好友通知 */
public class SC_REQUEST_ADD_FRIEND_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_REQUEST_ADD_FRIEND_NTF;}
	public	HeaderProto.REQUEST_ADD_FRIEND_NTF m_Info	=	new HeaderProto.REQUEST_ADD_FRIEND_NTF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 好友信息变化通知 */
public class SC_FRIEND_INFO_CHANGE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_FRIEND_INFO_CHANGE_NTF;}
	public	HeaderProto.FRIEND_INFO_CHANGE_NTF m_Info	=	new HeaderProto.FRIEND_INFO_CHANGE_NTF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 删除邀请列表中的请求 */
public class SC_FRIEND_REMOVE_INVITE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_FRIEND_REMOVE_INVITE_NTF;}
	public	HeaderProto.FRIEND_REMOVE_INVITE_NTF m_Info	=	new HeaderProto.FRIEND_REMOVE_INVITE_NTF();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 好友助战列表请求 */
public class CS_FRIEND_ASSIST_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_FRIEND_ASSIST_LIST_REQ;}
	public	HeaderProto.FRIEND_ASSIST_LIST_REQ m_Info	=	new HeaderProto.FRIEND_ASSIST_LIST_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

public class SAssistBattleInfo	:	CMessage{
	public	HeaderProto.SAssistBattlePersonInfo m_Assistor	=	new HeaderProto.SAssistBattlePersonInfo();	/// 助战人信息       .
	public	SPetInfo         m_BattlePet	=	new SPetInfo();	/// 战斗宠物         .
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

/* 好友助战列表请求 */
public class SC_FRIEND_ASSIST_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_FRIEND_ASSIST_LIST_ACK;}
	public	byte             m_Count;	/// 数量             .
	public	SAssistBattleInfo[] m_Assistors;
	public SC_FRIEND_ASSIST_LIST_ACK()
	{
		m_Assistors=new SAssistBattleInfo[(int)HeaderProto.MAX_FRIENDASSIST_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Assistors";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_FRIENDASSIST_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Assistors;

		}
		return 0;
	}
};

/* 选择助战好友请求 */
public class CS_SELECT_FRIEND_ASSIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SELECT_FRIEND_ASSIST_REQ;}
	public	HeaderProto.SELECT_FRIEND_ASSIST_REQ m_Info	=	new HeaderProto.SELECT_FRIEND_ASSIST_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 选择助战好友反馈 */
public class SC_SELECT_FRIEND_ASSIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELECT_FRIEND_ASSIST_ACK;}
	public	HeaderProto.SELECT_FRIEND_ASSIST_ACK m_Info	=	new HeaderProto.SELECT_FRIEND_ASSIST_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 触发深渊反馈 */
public class SC_ABYSS_TRIGGER_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ABYSS_TRIGGER_ACK;}
	public	HeaderProto.ABYSS_TRIGGER_ACK m_Info	=	new HeaderProto.ABYSS_TRIGGER_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 打开请求 */
public class CS_ABYSS_OPEN_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ABYSS_OPEN_REQ;}
	public	HeaderProto.ABYSS_OPEN_REQ m_Info	=	new HeaderProto.ABYSS_OPEN_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 打开深渊反馈 */
public class SC_ABYSS_OPEN_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ABYSS_OPEN_ACK;}
	public	HeaderProto.ABYSS_OPEN_ACK m_Info	=	new HeaderProto.ABYSS_OPEN_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 打开深渊通知 */
public class SC_ABYSS_OPEN_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ABYSS_OPEN_NTF;}
	public	HeaderProto.ABYSS_OPEN_ACK m_Info	=	new HeaderProto.ABYSS_OPEN_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取触发列表请求 */
public class CS_GET_ABYSS_TRIGGER_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_ABYSS_TRIGGER_LIST_REQ;}
	public	HeaderProto.GET_ABYSS_TRIGGER_LIST_REQ m_Info	=	new HeaderProto.GET_ABYSS_TRIGGER_LIST_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取触发列表反馈 */
public class SC_GET_ABYSS_TRIGGER_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_ABYSS_TRIGGER_LIST_ACK;}
	public	HeaderProto.GET_ABYSS_TRIGGER_LIST_ACK m_Info	=	new HeaderProto.GET_ABYSS_TRIGGER_LIST_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取打开列表请求 */
public class CS_GET_ABYSS_OPEN_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_ABYSS_OPEN_LIST_REQ;}
	public	HeaderProto.GET_ABYSS_OPEN_LIST_REQ m_Info	=	new HeaderProto.GET_ABYSS_OPEN_LIST_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取打开列表反馈 */
public class SC_GET_ABYSS_OPEN_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_LIST_ACK;}
	public	HeaderProto.GET_ABYSS_OPEN_LIST_ACK m_Info	=	new HeaderProto.GET_ABYSS_OPEN_LIST_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取打开记录请求 */
public class CS_GET_ABYSS_OPEN_REC_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_ABYSS_OPEN_REC_REQ;}
	public	HeaderProto.GET_ABYSS_OPEN_REC_REQ m_Info	=	new HeaderProto.GET_ABYSS_OPEN_REC_REQ();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 获取打开记录反馈 */
public class SC_GET_ABYSS_OPEN_REC_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_REC_ACK;}
	public	HeaderProto.GET_ABYSS_OPEN_REC_ACK m_Info	=	new HeaderProto.GET_ABYSS_OPEN_REC_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 进入深渊活动场景表反馈 */
public class SC_ENTER_ABYSS_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ENTER_ABYSS_ACK;}
	public	uint             m_Result;	/// GS_RESULT_SUCCESS，GS_RESULT_FAIL .
	public	HeaderProto.GET_ABYSS_OPEN_LIST_ACK m_Info	=	new HeaderProto.GET_ABYSS_OPEN_LIST_ACK();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Info;

		}
		return 0;
	}
};

/* 深渊活动boss逃跑通知 */
public class SC_ABYSS_RUN_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ABYSS_RUN_NTF;}
	public	UInt64           m_ActivityAbyssDBID;	/// 深渊活动的DBID   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="ActivityAbyssDBID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_ActivityAbyssDBID;

		}
		return 0;
	}
};

/* 邮件通知 */
public class SC_MAIL_LIST_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_MAIL_LIST_NTF;}
	public	HeaderProto.MAIL_LIST m_List	=	new HeaderProto.MAIL_LIST();	/// 邮件列表         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="List";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_List;

		}
		return 0;
	}
};

/* 发送邮件请求 */
public class CS_SEND_MAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SEND_MAIL_REQ;}
	public	HeaderProto.SEND_MAIL m_MailInfo	=	new HeaderProto.SEND_MAIL();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="MailInfo";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MailInfo;

		}
		return 0;
	}
};

/* 发送邮件回应 */
public class SC_SEND_MAIL_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SEND_MAIL_ACK;}
	public	byte             m_Result;
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

/* 提取邮件物品 */
public class CS_GET_ITEM_FROM_MAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_ITEM_FROM_MAIL_REQ;}
	public	UInt64           m_MailID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="MailID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MailID;

		}
		return 0;
	}
};

/* 提取邮件金钱 */
public class CS_GET_MONEY_FROM_MAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_MONEY_FROM_MAIL_REQ;}
	public	UInt64           m_MailID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="MailID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MailID;

		}
		return 0;
	}
};

/* 邮件阅读请求 */
public class CS_READ_MAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_READ_MAIL_REQ;}
	public	UInt64           m_MailID;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="MailID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MailID;

		}
		return 0;
	}
};

/* 邮件阅读回应 */
public class SC_READ_MAIL_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_READ_MAIL_ACK;}
	public	UInt64           m_MailID;
	public	uint             m_ReadTime;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="MailID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="ReadTime";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MailID;
		case	1:return m_ReadTime;

		}
		return 0;
	}
};

/* 删除邮件请求 */
public class CS_DELETE_MAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_DELETE_MAIL_REQ;}
	public	HeaderProto.MAIL_ID_LIST m_Mails	=	new HeaderProto.MAIL_ID_LIST();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Mails";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Mails;

		}
		return 0;
	}
};

/* 删除邮件回应 */
public class SC_DELETE_MAIL_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_DELETE_MAIL_ACK;}
	public	HeaderProto.MAIL_ID_LIST m_Mails	=	new HeaderProto.MAIL_ID_LIST();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Mails";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Mails;

		}
		return 0;
	}
};

/* 更新邮件通知 */
public class SC_UPDATE_MAIL_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_UPDATE_MAIL_NTF;}
	public	HeaderProto.MAIL_LIST m_List	=	new HeaderProto.MAIL_LIST();	/// 邮件列表         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="List";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_List;

		}
		return 0;
	}
};

/* 体力购买次数通知客户端 */
public class SC_SHOP_ACTION_POINT_BOUGHT_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_ACTION_POINT_BOUGHT_NTF;}
	public	uint             m_BoughtCount;	/// 已经购买次数     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BoughtCount";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BoughtCount;

		}
		return 0;
	}
};

/* 购买体力 */
public class CS_SHOP_BUY_ACTION_POINT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_BUY_ACTION_POINT_REQ;}
};

/* 购买体力结果 */
public class SC_SHOP_BUY_ACTION_POINT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_BUY_ACTION_POINT_ACK;}
	public	uint             m_Result;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

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

/* 购买低档战魂信息通知 */
public class SC_SHOP_BUY_PET_INFO_CHEAP_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_CHEAP_NTF;}
	public	UInt64           m_RemainTimeCheap;	/// 低档免费剩余时间(单位毫秒) .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RemainTimeCheap";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RemainTimeCheap;

		}
		return 0;
	}
};

/* 购买高档战魂信息通知 */
public class SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF;}
	public	byte             m_RemainCount;	/// 一定得紫色战魂剩余次数 .
	public	UInt64           m_RemainTimeExpensive;	/// 高档免费剩余时间(单位毫秒) .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RemainCount";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="RemainTimeExpensive";miArray[1].type=MemberInfo.MIType.eUInt64;miArray[1].size=8;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RemainCount;
		case	1:return m_RemainTimeExpensive;

		}
		return 0;
	}
};

/* 购买战魂 */
public class CS_SHOP_BUY_PET_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_BUY_PET_REQ;}
	public	byte             m_BuyType;	/// 见 EBuyPetType   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BuyType";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuyType;

		}
		return 0;
	}
};

/* 购买战魂结果 */
public class SC_SHOP_BUY_PET_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_ACK;}
	public	byte             m_Type;	/// 购买类型         .
	public	byte             m_Result;
	public	byte             m_Count;	/// 结果数量         .
	public	uint            [] m_TemplateID;	/// 模板id           .
	public SC_SHOP_BUY_PET_ACK()
	{
		m_TemplateID=new uint[(int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Result";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;
		miArray[2].name="Count";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;
		miArray[3].name="TemplateID";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;
		case	1:return m_Result;
		case	2:return m_Count;
		case	3:return m_TemplateID;

		}
		return 0;
	}
};

/* 获取商城物品列表 */
public class CS_SHOP_GET_GOODSLIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_GET_GOODSLIST_REQ;}
};

/* 商城物品信息 */
public class SGoodsInfo	:	CMessage{
	public	uint             m_GoodsId;	/// 商城道具ID       .
	public	uint             m_Num;	/// 数量             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="GoodsId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Num";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GoodsId;
		case	1:return m_Num;

		}
		return 0;
	}
};

/* 返回商城物品列表 */
public class SC_SHOP_GET_GOODSLIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_GET_GOODSLIST_ACK;}
	public	byte             m_Count;	/// 数量             .
	public	SGoodsInfo      [] m_GOODSINFOS;
	public SC_SHOP_GET_GOODSLIST_ACK()
	{
		m_GOODSINFOS=new SGoodsInfo[(int)HeaderProto.MAX_SHOP_GOODS_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="GOODSINFOS";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_SHOP_GOODS_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_GOODSINFOS;

		}
		return 0;
	}
};

/* 商城购买物品 */
public class CS_SHOP_BUY_GOODS_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_BUY_GOODS_REQ;}
	public	uint             m_GoodsId;	/// 购买商城道具ID   .
	public	int              m_GoodsCount;	/// 购买数量         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="GoodsId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GoodsCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_GoodsId;
		case	1:return m_GoodsCount;

		}
		return 0;
	}
};

/* 商城购买物品结果 */
public class SC_SHOP_BUY_GOODS_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_BUY_GOODS_ACK;}
	public	byte             m_Result;
	public	int              m_ItemCount;	/// 获得物品数量     .
	public	SGoodsInfo      [] m_TemplateID;	/// 获得物品模板ID   .
	public SC_SHOP_BUY_GOODS_ACK()
	{
		m_TemplateID=new SGoodsInfo[(int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="ItemCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="TemplateID";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_ItemCount;
		case	2:return m_TemplateID;

		}
		return 0;
	}
};

/* 神秘商店物品列表请求 */
public class CS_SHOP_SECRET_GOODS_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_SECRET_GOODS_REQ;}
};

/* 商城.神秘商店物品信息 */
public class SSecretItemInfo	:	CMessage{
	public	int              m_UID;	/// 对应的兑换唯一ID .
	public	byte             m_Bought;	/// 是否已经购买过了，0没买1买了 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="UID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Bought";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UID;
		case	1:return m_Bought;

		}
		return 0;
	}
};

/* 神秘商店物品列表返回 */
public class SC_SHOP_SECRET_GOODS_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_SECRET_GOODS_ACK;}
	public	UInt64           m_RefreshRemainTimeMS;	/// 刷新剩余时间(单位毫秒) .
	public	int              m_FreeRefreshRemainCount;	/// 免费刷新剩余次数 .
	public	int              m_ItemCount;	/// 神秘商店物品数量 .
	public	SSecretItemInfo [] m_SecretItems;	/// 神秘商店物品     .
	public SC_SHOP_SECRET_GOODS_ACK()
	{
		m_SecretItems=new SSecretItemInfo[(int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="RefreshRemainTimeMS";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;
		miArray[1].name="FreeRefreshRemainCount";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="ItemCount";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="SecretItems";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RefreshRemainTimeMS;
		case	1:return m_FreeRefreshRemainCount;
		case	2:return m_ItemCount;
		case	3:return m_SecretItems;

		}
		return 0;
	}
};

/* 神秘商店购买物品请求 */
public class CS_SHOP_SECRET_BUY_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_SECRET_BUY_REQ;}
	public	int              m_UID;	/// 对应的兑换唯一ID .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="UID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UID;

		}
		return 0;
	}
};

/* 神秘商店购买物品返回 */
public class SC_SHOP_SECRET_BUY_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SHOP_SECRET_BUY_ACK;}
	public	byte             m_Result;	/// 结果 EShopBuyResult 枚举 .
	public	int              m_UID;	/// 对应的兑换唯一ID .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="UID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_UID;

		}
		return 0;
	}
};

/* 神秘商店刷新请求，如果成功直接返回SC_SHOP_SECRET_GOODS_ACK列表信息，失败不返回 */
public class CS_SHOP_SECRET_REFRESH_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SHOP_SECRET_REFRESH_REQ;}
};

/* VIP信息通知 */
public class SC_VIP_INFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_VIP_INFO_NTF;}
	public	uint             m_VIPLevel;	/// VIP等级          .
	public	byte             m_GMLevel;	/// 账号GM等级       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="VIPLevel";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="GMLevel";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_VIPLevel;
		case	1:return m_GMLevel;

		}
		return 0;
	}
};

/* VIP.RMB充值信息通知 */
public class SC_VIP_RMBINFO_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_VIP_RMBINFO_NTF;}
	public	int              m_RMBCount;	/// 充值次数         .
	public	int              m_RMBSum;	/// 充值RMB总额      .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="RMBCount";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="RMBSum";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RMBCount;
		case	1:return m_RMBSum;

		}
		return 0;
	}
};

/* VIP.月卡信息通知 */
public class SC_VIP_MONTHCARD_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_VIP_MONTHCARD_NTF;}
	public	byte             m_MonthCardCount;	/// 月卡数量         .
	public	HeaderProto.MonthCardInfo[] m_MonthCardInfos;	/// 账号月卡信息     .
	public SC_VIP_MONTHCARD_NTF()
	{
		m_MonthCardInfos=new HeaderProto.MonthCardInfo[(int)HeaderProto.MAX_MONTHCARD_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="MonthCardCount";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="MonthCardInfos";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_MONTHCARD_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MonthCardCount;
		case	1:return m_MonthCardInfos;

		}
		return 0;
	}
};

/* VIP.月卡领取奖励请求 */
public class CS_VIP_MONTHCARD_GETITEM_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_VIP_MONTHCARD_GETITEM_REQ;}
	public	int              m_UId;	/// 领取的月卡ID     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="UId";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_UId;

		}
		return 0;
	}
};

/* VIP.月卡领取奖励回应 */
public class SC_VIP_MONTHCARD_GETITEM_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_VIP_MONTHCARD_GETITEM_ACK;}
	public	byte             m_Result;	/// 结果 EMonthCardAwardResult 枚举 .
	public	int              m_UId;	/// 领取的月卡ID     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="UId";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_UId;

		}
		return 0;
	}
};

/* 角色PVP信息  */
public class CS_SELF_PVP_PRO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_SELF_PVP_PRO_REQ;}
};

/* 角色PVP信息  */
public class SC_SELF_PVP_PRO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_SELF_PVP_PRO_ACK;}
	public	int              m_MilitaryLevel;	/// 军阶等级         .
	public	uint             m_Repute;	/// 声望             .
	public	uint             m_Points;	/// 积分             .
	public	uint             m_Times;
	public	uint             m_Wins;
	public	uint             m_Loses;
	public	uint             m_Ranks;
	public	byte             m_MiFlag;
	public	uint             m_DayRepute;
	public	UInt64           m_DayReFlag;
	public	int              m_BuyTimesLeft;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[11];
		miArray[0].name="MilitaryLevel";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Repute";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Points";miArray[2].type=MemberInfo.MIType.eUInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Times";miArray[3].type=MemberInfo.MIType.eUInt;miArray[3].size=4;miArray[3].count=(int)0;
		miArray[4].name="Wins";miArray[4].type=MemberInfo.MIType.eUInt;miArray[4].size=4;miArray[4].count=(int)0;
		miArray[5].name="Loses";miArray[5].type=MemberInfo.MIType.eUInt;miArray[5].size=4;miArray[5].count=(int)0;
		miArray[6].name="Ranks";miArray[6].type=MemberInfo.MIType.eUInt;miArray[6].size=4;miArray[6].count=(int)0;
		miArray[7].name="MiFlag";miArray[7].type=MemberInfo.MIType.eByte;miArray[7].size=1;miArray[7].count=(int)0;
		miArray[8].name="DayRepute";miArray[8].type=MemberInfo.MIType.eUInt;miArray[8].size=4;miArray[8].count=(int)0;
		miArray[9].name="DayReFlag";miArray[9].type=MemberInfo.MIType.eUInt64;miArray[9].size=8;miArray[9].count=(int)0;
		miArray[10].name="BuyTimesLeft";miArray[10].type=MemberInfo.MIType.eInt;miArray[10].size=4;miArray[10].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_MilitaryLevel;
		case	1:return m_Repute;
		case	2:return m_Points;
		case	3:return m_Times;
		case	4:return m_Wins;
		case	5:return m_Loses;
		case	6:return m_Ranks;
		case	7:return m_MiFlag;
		case	8:return m_DayRepute;
		case	9:return m_DayReFlag;
		case	10:return m_BuyTimesLeft;

		}
		return 0;
	}
};

/* 获取pvp对手请求  */
public class CS_GET_PVP_MATCH_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_PVP_MATCH_REQ;}
};

/* 获取pvp对手回应  */
public class SC_GET_PVP_MATCH_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_PVP_MATCH_ACK;}
	public	HeaderProto.PVP_MATCH_LIST m_PVPMatchList	=	new HeaderProto.PVP_MATCH_LIST();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="PVPMatchList";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_PVPMatchList;

		}
		return 0;
	}
};

/* 挑战请求  */
public class CS_ENTER_PVP_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ENTER_PVP_REQ;}
	public	UInt64           m_RoleID;	/// 挑战的角色id     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleID;

		}
		return 0;
	}
};

/* 挑战反馈  */
public class SC_ENTER_PVP_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ENTER_PVP_ACK;}
	public	uint             m_Result;	/// 0表示成功，非0表示失败 .
	public	SC_SELF_BASE_PRO m_BasePro	=	new SC_SELF_BASE_PRO();	/// 基本属性         .
	public	SC_USER_JOIN_PETS m_Pet	=	new SC_USER_JOIN_PETS();	/// 宠物             .
	public	SC_USER_SKILLS_NTF m_Skill	=	new SC_USER_SKILLS_NTF();	/// 技能             .
	public	SC_ROLE_EQUIP_NTF m_Equip	=	new SC_ROLE_EQUIP_NTF();	/// 装备             .
	public	HeaderProto.SRobotBuffs m_RobotBuff	=	new HeaderProto.SRobotBuffs();	/// buffs            .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[6];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BasePro";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Pet";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;
		miArray[3].name="Skill";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="Equip";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)0;
		miArray[5].name="RobotBuff";miArray[5].type=MemberInfo.MIType.eMessage;miArray[5].size=0;miArray[5].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_BasePro;
		case	2:return m_Pet;
		case	3:return m_Skill;
		case	4:return m_Equip;
		case	5:return m_RobotBuff;

		}
		return 0;
	}
};

/* PVP结算请求  */
public class CS_PVP_RETULT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PVP_RETULT_REQ;}
	public	uint             m_Result;	/// 0表示挑战成功，非0表示挑战失败：1表示中途退出 .
	public	int              m_Timeout;	/// 是否时间耗尽结束 否-0  是-1 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Timeout";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Timeout;

		}
		return 0;
	}
};

/* PVP结算反馈  */
public class SC_PVP_RETULT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PVP_RETULT_ACK;}
	public	uint             m_Result;	/// 0表示成功，非0表示失败 .
	public	SC_SELF_PVP_PRO_ACK m_Pro	=	new SC_SELF_PVP_PRO_ACK();	/// 结算后PVP信息    .
	public	int              m_Points;	/// 积分             .
	public	int              m_Repute;	/// 声望             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[4];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Pro";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;
		miArray[2].name="Points";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;
		miArray[3].name="Repute";miArray[3].type=MemberInfo.MIType.eInt;miArray[3].size=4;miArray[3].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Pro;
		case	2:return m_Points;
		case	3:return m_Repute;

		}
		return 0;
	}
};

/* 获取军阶奖励请求  */
public class CS_GET_PVP_MILITARY_REWARD_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_PVP_MILITARY_REWARD_REQ;}
};

/* 获取军阶奖励反馈  */
public class SC_GET_PVP_MILITARY_REWARD_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_PVP_MILITARY_REWARD_ACK;}
	public	uint             m_Result;	/// 0表示成功，非0表示失败 .
	public	byte             m_MiFlag;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="MiFlag";miArray[1].type=MemberInfo.MIType.eByte;miArray[1].size=1;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_MiFlag;

		}
		return 0;
	}
};

/* 获取声望奖励请求  */
public class CS_GET_PVP_REPUTE_REWARD_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_PVP_REPUTE_REWARD_REQ;}
	public	uint             m_RewardId;	/// 领取声望奖励id 1-领取20点 2-40点 3-60点 4-80点 5-100点 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RewardId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RewardId;

		}
		return 0;
	}
};

/* 获取声望奖励反馈  */
public class SC_GET_PVP_REPUTE_REWARD_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_PVP_REPUTE_REWARD_ACK;}
	public	uint             m_Result;	/// 0表示成功，非0表示失败 .
	public	uint             m_DayRepute;
	public	UInt64           m_DayReFlag;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="DayRepute";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="DayReFlag";miArray[2].type=MemberInfo.MIType.eUInt64;miArray[2].size=8;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_DayRepute;
		case	2:return m_DayReFlag;

		}
		return 0;
	}
};

/* 获取排行榜请求  */
public class CS_GET_RANK_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_RANK_LIST_REQ;}
};

/* 获取排行榜反馈  */
public class SC_GET_RANK_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_RANK_LIST_ACK;}
	public	HeaderProto.PVP_RANK_LIST m_RankList	=	new HeaderProto.PVP_RANK_LIST();	/// 排行榜           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RankList";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RankList;

		}
		return 0;
	}
};

/* 买挑战次数请求  */
public class CS_PVP_BUY_CHALLENGE_TIMES_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_PVP_BUY_CHALLENGE_TIMES_REQ;}
	public	int              m_BuyTimes;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BuyTimes";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuyTimes;

		}
		return 0;
	}
};

/* 买挑战次数反馈  */
public class SC_PVP_BUY_CHALLENGE_TIMES_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PVP_BUY_CHALLENGE_TIMES_ACK;}
	public	uint             m_Result;	/// 非0表示购买的挑战次数 .
	public	int              m_BuyTimesLeft;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BuyTimesLeft";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_BuyTimesLeft;

		}
		return 0;
	}
};

/* 通知pvp时间  */
public class SC_PVP_TIME_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_PVP_TIME_NTF;}
	public	UInt64           m_Time;	/// 到PVP开始的毫秒数 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Time";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Time;

		}
		return 0;
	}
};

/* 获取PVP角色信息请求  */
public class CS_GET_PVP_ROLE_INFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_PVP_ROLE_INFO_REQ;}
	public	UInt64           m_RoleID;	/// 挑战的角色id     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="RoleID";miArray[0].type=MemberInfo.MIType.eUInt64;miArray[0].size=8;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_RoleID;

		}
		return 0;
	}
};

/* 获取PVP角色信息反馈  */
public class SC_GET_PVP_ROLE_INFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_PVP_ROLE_INFO_ACK;}
	public	uint             m_Result;	/// 0表示人，1表示机器人，其他失败 .
	public	int              m_Attack;	/// 战斗力           .
	public	SC_ROLE_EQUIP_NTF m_Equip	=	new SC_ROLE_EQUIP_NTF();	/// 装备             .
	public	SC_USER_JOIN_PETS m_Pet	=	new SC_USER_JOIN_PETS();	/// 宠物             .
	public	SC_USER_SKILLS_NTF m_Skill	=	new SC_USER_SKILLS_NTF();	/// 技能             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[5];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Attack";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Equip";miArray[2].type=MemberInfo.MIType.eMessage;miArray[2].size=0;miArray[2].count=(int)0;
		miArray[3].name="Pet";miArray[3].type=MemberInfo.MIType.eMessage;miArray[3].size=0;miArray[3].count=(int)0;
		miArray[4].name="Skill";miArray[4].type=MemberInfo.MIType.eMessage;miArray[4].size=0;miArray[4].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Attack;
		case	2:return m_Equip;
		case	3:return m_Pet;
		case	4:return m_Skill;

		}
		return 0;
	}
};

/* 每日签到信息通知，上线发一次，签到时发一次，刷新时发一次 */
public class SC_GIFT_SIGN_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_SIGN_NTF;}
	public	byte             m_CanSign;	/// 是否可以签到 0不可以 1可以 .
	public	uint             m_SignCount;	/// 当月签到次数     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="CanSign";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="SignCount";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_CanSign;
		case	1:return m_SignCount;

		}
		return 0;
	}
};

/* 每日签到VIP补领情况通知 */
public class SC_GIFT_SIGN_VIP_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_SIGN_VIP_NTF;}
	public	uint             m_Count;	/// VIP补领次数      .
	public	uint            [] m_Day;	/// VIP补领已补领次数 .
	public SC_GIFT_SIGN_VIP_NTF()
	{
		m_Day=new uint[(int)HeaderProto.MAX_GIFT_VIP_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Day";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)HeaderProto.MAX_GIFT_VIP_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Day;

		}
		return 0;
	}
};

/* 签到，会直接触发领奖，VIP会直接领奖两次 */
public class CS_GIFT_SIGN	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_SIGN;}
};

/* VIP补领奖励 */
public class CS_GIFT_SIGN_VIP	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_SIGN_VIP;}
	public	uint             m_SignDate;	/// 补领当月的第几次，发次数 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="SignDate";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_SignDate;

		}
		return 0;
	}
};

/* 成长已领奖信息通知 */
public class SC_GIFT_LEVEL_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_LEVEL_NTF;}
	public	uint             m_Count;	/// 已领次数         .
	public	uint            [] m_Level;	/// 已领取等级       .
	public SC_GIFT_LEVEL_NTF()
	{
		m_Level=new uint[(int)HeaderProto.MAX_GIFT_LEVEL_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Level";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)HeaderProto.MAX_GIFT_LEVEL_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Level;

		}
		return 0;
	}
};

/* 成长领奖 */
public class CS_GIFT_LEVEL	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_LEVEL;}
	public	int              m_Level;	/// 奖励的等级       .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Level";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Level;

		}
		return 0;
	}
};

/* 每日任务完成信息 */
public class SDayQuestInfo	:	CMessage{
	public	uint             m_QuestId;	/// 每日任务类型ID   .
	public	uint             m_FinishProgress;	/// 已完成次数       .
	public	byte             m_ScoreReceived;	/// 积分已领取0未领取1领取 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="QuestId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="FinishProgress";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="ScoreReceived";miArray[2].type=MemberInfo.MIType.eByte;miArray[2].size=1;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_QuestId;
		case	1:return m_FinishProgress;
		case	2:return m_ScoreReceived;

		}
		return 0;
	}
};

/* 每日任务完成信息，上线发一次，刷新时发一次 */
public class SC_GIFT_DAY_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_DAY_NTF;}
	public	uint             m_Count;	/// 信息数量         .
	public	SDayQuestInfo   [] m_Info;	/// 完成信息         .
	public SC_GIFT_DAY_NTF()
	{
		m_Info=new SDayQuestInfo[(int)HeaderProto.MAX_DAY_QUEST_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)HeaderProto.MAX_DAY_QUEST_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_Info;

		}
		return 0;
	}
};

/* 每日任务完成信息增量更新 */
public class SC_GIFT_DAY_UPD	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_DAY_UPD;}
	public	SDayQuestInfo    m_Info	=	new SDayQuestInfo();	/// 完成信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 每日任务领取积分，回 SC_GIFT_DAY_NTF 消息 */
public class CS_GIFT_DAY_SCORE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_DAY_SCORE_REQ;}
	public	uint             m_QuestId;	/// 每日任务类型ID   .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="QuestId";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_QuestId;

		}
		return 0;
	}
};

/* 每日任务领取宝箱 */
public class CS_GIFT_DAY_BOX_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_DAY_BOX_REQ;}
	public	uint             m_BoxID;	/// 宝箱id           .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BoxID";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BoxID;

		}
		return 0;
	}
};

/* 每日任务已领取宝箱列表 */
public class SC_GIFT_DAY_BOX_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_DAY_BOX_NTF;}
	public	byte             m_Count;	/// 信息数量         .
	public	uint            [] m_BoxID;	/// 宝箱id           .
	public SC_GIFT_DAY_BOX_NTF()
	{
		m_BoxID=new uint[(int)HeaderProto.MAX_DAY_QUEST_BOX_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="BoxID";miArray[1].type=MemberInfo.MIType.eUInt;miArray[1].size=4;miArray[1].count=(int)HeaderProto.MAX_DAY_QUEST_BOX_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_BoxID;

		}
		return 0;
	}
};

/* 激活码领奖请求 */
public class CS_GIFT_JIHUOMA_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_JIHUOMA_REQ;}
	public	byte            [] m_JiHuoMa;	/// 激活码           .
	public CS_GIFT_JIHUOMA_REQ()
	{
		m_JiHuoMa=new byte[(int)HeaderProto.MAX_JIHUOMA_LEN];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="JiHuoMa";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)HeaderProto.MAX_JIHUOMA_LEN;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_JiHuoMa;

		}
		return 0;
	}
};

/* 激活码领奖回应 */
public class SC_GIFT_JIHUOMA_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_JIHUOMA_ACK;}
	public	byte             m_Ret;	/// 结果 参见枚举 EJihuoma  .
	public	HeaderProto.SMultiItems m_Items	=	new HeaderProto.SMultiItems();	/// 获得物品         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Ret";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="Items";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ret;
		case	1:return m_Items;

		}
		return 0;
	}
};

/* 获取体力领取信息，每次打开面板时请求一次，刷新时间点到时请求一次 */
public class CS_GIFT_EP_INFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_EP_INFO_REQ;}
};

/* 体力领取信息,所有已领取信息的 DayFoodID  */
public class SC_GIFT_EP_INFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_EP_INFO_ACK;}
	public	byte             m_Count;	/// 信息数量         .
	public	int             [] m_DayFoodID;	/// 已领取美食的id   .
	public SC_GIFT_EP_INFO_ACK()
	{
		m_DayFoodID=new int[(int)HeaderProto.MAX_FOODS_COUNT];
	}
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Count";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="DayFoodID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)HeaderProto.MAX_FOODS_COUNT;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Count;
		case	1:return m_DayFoodID;

		}
		return 0;
	}
};

/* 领取体力请求 */
public class CS_GIFT_RECEIVE_EP_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GIFT_RECEIVE_EP_REQ;}
	public	int              m_DayFoodID;	/// 领取美食的id     .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="DayFoodID";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_DayFoodID;

		}
		return 0;
	}
};

/* 领取体力回应 */
public class SC_GIFT_RECEIVE_EP_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GIFT_RECEIVE_EP_ACK;}
	public	byte             m_Result;	/// 结果 见枚举 EReceiveEPResult  .
	public	int              m_DayFoodID;	/// 领取的美食ID，只有在Result成功时有意义  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eByte;miArray[0].size=1;miArray[0].count=(int)0;
		miArray[1].name="DayFoodID";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_DayFoodID;

		}
		return 0;
	}
};

/* 世界boss信息刷新请求 */
public class CS_WB_INFO_REFRESH_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_WB_INFO_REFRESH_REQ;}
};

/* 世界boss信息刷新反馈 */
public class SC_WB_INFO_REFRESH_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_INFO_REFRESH_ACK;}
	public	HeaderProto.SWorldBossInfo m_Info	=	new HeaderProto.SWorldBossInfo();	/// 信息             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Info";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Info;

		}
		return 0;
	}
};

/* 世界boss鼓舞buff请求 */
public class CS_WB_ADD_BUF_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_WB_ADD_BUF_REQ;}
	public	int              m_Type;	/// 鼓舞类别（0-金币、1-徽章） .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Type";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Type;

		}
		return 0;
	}
};

/* 世界boss鼓舞buff反馈 */
public class SC_WB_ADD_BUF_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_ADD_BUF_ACK;}
	public	int              m_Result;	/// 结果信息 0-成功  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

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

/* 世界boss立即复活请求 */
public class CS_WB_RELIVE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_WB_RELIVE_REQ;}
};

/* 世界boss立即复活反馈 */
public class SC_WB_RELIVE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_RELIVE_ACK;}
	public	int              m_Result;	/// 结果信息 0-成功  .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

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

/* 世界boss被杀通知 */
public class SC_WB_KILLED_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_KILLED_NTF;}
};

/* 世界boss逃跑通知 */
public class SC_WB_RUN_NTF	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_RUN_NTF;}
};

/* 进入世界boss请求 */
public class CS_WB_ENTER_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_WB_ENTER_REQ;}
};

/* 进入世界boss反馈 */
public class SC_WB_ENTER_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_ENTER_ACK;}
	public	int              m_Result;	/// 结果信息 0-成功  .
	public	HeaderProto.SWorldBossInfo m_Info	=	new HeaderProto.SWorldBossInfo();	/// 信息             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Info;

		}
		return 0;
	}
};

/* 进入世界boss请求 */
public class CS_WB_RESULT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_WB_RESULT_REQ;}
	public	int              m_Result;	/// 结果信息 0-成功  .
	public	int              m_Damage;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Damage";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Damage;

		}
		return 0;
	}
};

/* 进入世界boss反馈 */
public class SC_WB_RESULT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_WB_RESULT_ACK;}
	public	int              m_Result;	/// 结果信息 0-成功  .
	public	HeaderProto.SWorldBossInfo m_Info	=	new HeaderProto.SWorldBossInfo();	/// 信息             .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Info";miArray[1].type=MemberInfo.MIType.eMessage;miArray[1].size=0;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_Info;

		}
		return 0;
	}
};

/* 申请排名信息 */
public class CS_ROLE_RANK_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_ROLE_RANK_REQ;}
	public	HeaderProto.SRankRoleReq m_Req	=	new HeaderProto.SRankRoleReq();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 申请排名信息回应 */
public class SC_ROLE_RANK_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_ROLE_RANK_ACK;}
	public	HeaderProto.SRankRoleAck m_Ack	=	new HeaderProto.SRankRoleAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 获取战魂试炼基本信息请求 */
public class CS_GET_PT_BASEINFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_PT_BASEINFO_REQ;}
};

/* 获取战魂试炼基本信息回应 */
public class SC_GET_PT_BASEINFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_PT_BASEINFO_ACK;}
	public	int              m_Times;	/// 剩余挑战次数     .
	public	int              m_Buys;	/// 剩余购买次数     .
	public	int              m_Pass;	/// 已过难度 1-难度1 2-难度2 3-难度3 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Times";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Buys";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Pass";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Times;
		case	1:return m_Buys;
		case	2:return m_Pass;

		}
		return 0;
	}
};

/* 买挑战次数请求  */
public class CS_BUY_PT_TIMES_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_BUY_PT_TIMES_REQ;}
	public	int              m_BuyTimes;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BuyTimes";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuyTimes;

		}
		return 0;
	}
};

/* 买挑战次数反馈  */
public class SC_BUY_PT_TIMES_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_BUY_PT_TIMES_ACK;}
	public	uint             m_Result;	/// 非0表示购买的挑战次数 .
	public	int              m_BuyTimesLeft;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BuyTimesLeft";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_BuyTimesLeft;

		}
		return 0;
	}
};

/* 获取金钱副本基本信息请求 */
public class CS_GET_MD_BASEINFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_MD_BASEINFO_REQ;}
};

/* 获取金钱副本基本信息回应 */
public class SC_GET_MD_BASEINFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_MD_BASEINFO_ACK;}
	public	int              m_Times;	/// 剩余挑战次数     .
	public	int              m_Buys;	/// 剩余购买次数     .
	public	int              m_Pass;	/// 已过难度 1-难度1 2-难度2 3-难度3 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Times";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Buys";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Pass";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Times;
		case	1:return m_Buys;
		case	2:return m_Pass;

		}
		return 0;
	}
};

/* 买挑战次数请求  */
public class CS_BUY_MD_TIMES_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_BUY_MD_TIMES_REQ;}
	public	int              m_BuyTimes;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BuyTimes";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuyTimes;

		}
		return 0;
	}
};

/* 买挑战次数反馈  */
public class SC_BUY_MD_TIMES_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_BUY_MD_TIMES_ACK;}
	public	uint             m_Result;	/// 非0表示购买的挑战次数 .
	public	int              m_BuyTimesLeft;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BuyTimesLeft";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_BuyTimesLeft;

		}
		return 0;
	}
};

/* 获取火龙塔基本信息请求 */
public class CS_GET_FD_BASEINFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GET_FD_BASEINFO_REQ;}
};

/* 获取火龙塔基本信息回应 */
public class SC_GET_FD_BASEINFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GET_FD_BASEINFO_ACK;}
	public	int              m_Times;	/// 剩余挑战次数     .
	public	int              m_Buys;	/// 剩余购买次数     .
	public	int              m_Pass;	/// 已过难度 1-难度1 2-难度2 3-难度3 .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[3];
		miArray[0].name="Times";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="Buys";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;
		miArray[2].name="Pass";miArray[2].type=MemberInfo.MIType.eInt;miArray[2].size=4;miArray[2].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Times;
		case	1:return m_Buys;
		case	2:return m_Pass;

		}
		return 0;
	}
};

/* 买挑战次数请求  */
public class CS_BUY_FD_TIMES_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_BUY_FD_TIMES_REQ;}
	public	int              m_BuyTimes;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="BuyTimes";miArray[0].type=MemberInfo.MIType.eInt;miArray[0].size=4;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_BuyTimes;

		}
		return 0;
	}
};

/* 买挑战次数反馈  */
public class SC_BUY_FD_TIMES_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_BUY_FD_TIMES_ACK;}
	public	uint             m_Result;	/// 非0表示购买的挑战次数 .
	public	int              m_BuyTimesLeft;
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[2];
		miArray[0].name="Result";miArray[0].type=MemberInfo.MIType.eUInt;miArray[0].size=4;miArray[0].count=(int)0;
		miArray[1].name="BuyTimesLeft";miArray[1].type=MemberInfo.MIType.eInt;miArray[1].size=4;miArray[1].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Result;
		case	1:return m_BuyTimesLeft;

		}
		return 0;
	}
};

/* 公会信息请求，非入会返回SC_GUILD_LIST_ACK(第一页) 入会返回SC_GUILD_BASEINFO_ACK公会基本信息 */
public class CS_GUILD_INFO_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_INFO_REQ;}
};

/* 公会列表请求，非入会玩家，返回SC_GUILD_LIST_ACK  */
public class CS_GUILD_LIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_LIST_REQ;}
	public	HeaderProto.SGuildListReq m_Req	=	new HeaderProto.SGuildListReq();	/// 请求信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 公会列表回应，非入会玩家  */
public class SC_GUILD_LIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_LIST_ACK;}
	public	HeaderProto.SGuildListAck m_Ack	=	new HeaderProto.SGuildListAck();	/// 返回信息         .
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会入会申请  */
public class CS_GUILD_ENTER_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_ENTER_REQ;}
	public	HeaderProto.SGuildEnterReq m_Req	=	new HeaderProto.SGuildEnterReq();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 公会入会申请返回  */
public class SC_GUILD_ENTER_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_ENTER_ACK;}
	public	HeaderProto.SGuildEnterAck m_Ack	=	new HeaderProto.SGuildEnterAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 创建公会  */
public class CS_GUILD_CREATE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_CREATE_REQ;}
	public	HeaderProto.SGuildEnterAck m_Req	=	new HeaderProto.SGuildEnterAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 创建公会  */
public class SC_GUILD_CREATE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_CREATE_ACK;}
	public	HeaderProto.SGuildEnterAck m_Ack	=	new HeaderProto.SGuildEnterAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 离开公会请求  */
public class CS_GUILD_LEAVE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_LEAVE_REQ;}
};

/* 离开公会返回  */
public class SC_GUILD_LEAVE_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_LEAVE_ACK;}
	public	HeaderProto.SGuildLeaveAck m_Ack	=	new HeaderProto.SGuildLeaveAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会基本信息  */
public class SC_GUILD_BASEINFO_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_BASEINFO_ACK;}
	public	HeaderProto.SGuildBaseInfo m_Ack	=	new HeaderProto.SGuildBaseInfo();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会日志查询  */
public class CS_GUILD_LOG_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_LOG_REQ;}
};

/* 公会日志结果  */
public class SC_GUILD_LOG_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_LOG_ACK;}
	public	HeaderProto.SGuildLogAck m_Ack	=	new HeaderProto.SGuildLogAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会修改公告请求  */
public class CS_GUILD_CHANGE_NOTICE_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_CHANGE_NOTICE_REQ;}
	public	HeaderProto.SGuildChangeNoticeReq m_Req	=	new HeaderProto.SGuildChangeNoticeReq();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 公会修改公告结果  */
public class SC_GUILD_CHANGE_NOTICE_ACK	:	CMessage{
	public	HeaderProto.SGuildChangeNoticeAck m_Ack	=	new HeaderProto.SGuildChangeNoticeAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会详细信息请求（公会大厅）  */
public class CS_GUILD_DETAIL_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_DETAIL_REQ;}
};

/* 公会详细信息返回（公会大厅）  */
public class SC_GUILD_DETAIL_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_DETAIL_ACK;}
	public	HeaderProto.SGuildDetailAck m_Ack	=	new HeaderProto.SGuildDetailAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会申请列表请求  */
public class CS_GUILD_ENTERLIST_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_ENTERLIST_REQ;}
};

/* 公会申请列表返回  */
public class SC_GUILD_ENTERLIST_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_ENTERLIST_ACK;}
	public	HeaderProto.SGuildEnterListAck m_Ack	=	new HeaderProto.SGuildEnterListAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会申请.同意/拒绝  */
public class CS_GUILD_ENTER_ACCEPT_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_ENTER_ACCEPT_REQ;}
	public	HeaderProto.SGuildEnterAcceptReq m_Req	=	new HeaderProto.SGuildEnterAcceptReq();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Req";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Req;

		}
		return 0;
	}
};

/* 公会申请.同意/拒绝 返回  */
public class SC_GUILD_ENTER_ACCEPT_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_ENTER_ACCEPT_ACK;}
	public	HeaderProto.SGuildEnterAcceptAck m_Ack	=	new HeaderProto.SGuildEnterAcceptAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/* 公会申请.清空 请求  */
public class CS_GUILD_ENTER_CLEAR_REQ	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.CSID_GUILD_ENTER_CLEAR_REQ;}
};

/* 公会申请.清空 返回  */
public class SC_GUILD_ENTER_CLEAR_ACK	:	CMessage{
	public	override	ushort	GetID(){	return (ushort)EN_CliProto_MessageID.SCID_GUILD_ENTER_CLEAR_ACK;}
	public	HeaderProto.SGuildEnterClearAck m_Ack	=	new HeaderProto.SGuildEnterClearAck();
	public override MemberInfo[] GetDesc()
	{
		MemberInfo[] miArray	=	new MemberInfo[1];
		miArray[0].name="Ack";miArray[0].type=MemberInfo.MIType.eMessage;miArray[0].size=0;miArray[0].count=(int)0;

		return miArray;
	}
	public override object GetMemberValue(int idx)
	{
		switch(idx)
		{
		case	0:return m_Ack;

		}
		return 0;
	}
};

/*Define Structs and Unions        End*/


public	static	void	AddEncode_Decode(){
	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_LOGIN,EncodeCG_LOGIN,DecodeCG_LOGIN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_LOGIN,EncodeGC_LOGIN,DecodeGC_LOGIN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_LOGOUT,EncodeCG_LOGOUT,DecodeCG_LOGOUT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_LOGOUT,EncodeGC_LOGOUT,DecodeGC_LOGOUT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_ROLELIST,EncodeGC_ROLELIST,DecodeGC_ROLELIST);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_CREATEROLE,EncodeCG_CREATEROLE,DecodeCG_CREATEROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_CREATEROLE,EncodeGC_CREATEROLE,DecodeGC_CREATEROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_DELROLE,EncodeCG_DELROLE,DecodeCG_DELROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_DELROLE,EncodeGC_DELROLE,DecodeGC_DELROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_SELECTROLE,EncodeCG_SELECTROLE,DecodeCG_SELECTROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_SELECTROLE,EncodeGC_SELECTROLE,DecodeGC_SELECTROLE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_QUEUE_INFO,EncodeGC_QUEUE_INFO,DecodeGC_QUEUE_INFO);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_GPK_DYN_CODE,EncodeGC_GPK_CLT_DYN_CODE,DecodeGC_GPK_CLT_DYN_CODE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_GPK_NEGLECT,EncodeCG_GPK_CLT_NEGLECT,DecodeCG_GPK_CLT_NEGLECT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_GPK_CAL_ORIGIN,EncodeGC_GPK_CAL_ORIGIN,DecodeGC_GPK_CAL_ORIGIN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_GPK_CAL_RESULT,EncodeCG_GPK_CAL_RESULT,DecodeCG_GPK_CAL_RESULT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_ERROR,EncodeGC_ERROR,DecodeGC_ERROR);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_ERRORSTRING,EncodeGC_ERRORSTRING,DecodeGC_ERRORSTRING);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CGID_HEART_BEAT,EncodeCG_HEART_BEAT,DecodeCG_HEART_BEAT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_HEART_BEAT,EncodeGC_HEART_BEAT,DecodeGC_HEART_BEAT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GCID_PUBLIC_STRING,EncodeGC_PUBLIC_STRING,DecodeGC_PUBLIC_STRING);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.LGID_LOGIN_REQ,EncodeLGPKG_LOGIN_REQ,DecodeLGPKG_LOGIN_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.LGID_LOGIN_ACK,EncodeLGPKG_LOGIN_ACK,DecodeLGPKG_LOGIN_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.LGID_GAMESERVER_LIST_REQ,EncodeLGPKG_GAMESERVER_LIST_REQ,DecodeLGPKG_GAMESERVER_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.LGID_GAMESERVER_LIST_ACK,EncodeLGPKG_GAMESERVER_LIST_ACK,DecodeLGPKG_GAMESERVER_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_SERVERINFO_RPT,EncodeGSPKG_SERVERINFO_RPT,DecodeGSPKG_SERVERINFO_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_SERVER_GROUP_DESC_RPT,EncodeGSPKG_SERVER_GROUP_DESC_RPT,DecodeGSPKG_SERVER_GROUP_DESC_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_ENTERGAME_REQ,EncodeGSPKG_ENTERGAME_REQ,DecodeGSPKG_ENTERGAME_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_ENTERGAME_ACK,EncodeGSPKG_ENTERGAME_ACK,DecodeGSPKG_ENTERGAME_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_LEAVEGAME_REQ,EncodeGSPKG_LEAVEGAME_REQ,DecodeGSPKG_LEAVEGAME_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_LEAVEGAME_ACK,EncodeGSPKG_LEAVEGAME_ACK,DecodeGSPKG_LEAVEGAME_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_KICKOUT_NTF,EncodeGSPKG_KICKOUT_NTF,DecodeGSPKG_KICKOUT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_MSG_BROADCAST,EncodeGSPKG_MSG_BROADCAST,DecodeGSPKG_MSG_BROADCAST);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_LOAD_GSDATA_NTF,EncodeGSPKG_LOAD_GSDATA_NTF,DecodeGSPKG_LOAD_GSDATA_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_UPDATEGMLEVEL_NTF,EncodeGSPKG_UPDATEGMLEVEL_NTF,DecodeGSPKG_UPDATEGMLEVEL_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_SWITCHSCENE_REQ,EncodeGSPKG_SWITCHSCENE_REQ,DecodeGSPKG_SWITCHSCENE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.GSID_SWITCHSCENE_ACK,EncodeGSPKG_SWITCHSCENE_ACK,DecodeGSPKG_SWITCHSCENE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_LOADSCENE,EncodeSC_SELF_LOADSCENE,DecodeSC_SELF_LOADSCENE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ENTERSCENE,EncodeCS_ENTERSCENE,DecodeCS_ENTERSCENE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_ENTERSCENE,EncodeSC_SELF_ENTERSCENE,DecodeSC_SELF_ENTERSCENE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_DATA_END,EncodeSC_SELF_DATA_END,DecodeSC_SELF_DATA_END);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_APPEAR,EncodeSC_SELF_APPEAR,DecodeSC_SELF_APPEAR);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_BASE_PRO,EncodeSC_SELF_BASE_PRO,DecodeSC_SELF_BASE_PRO);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_LEVEL_REQ,EncodeCS_LEVEL_REQ,DecodeCS_LEVEL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_LEVEL_ACK,EncodeSC_LEVEL_ACK,DecodeSC_LEVEL_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_LEVEL_RESULT_NTF,EncodeCS_LEVEL_RESULT_NTF,DecodeCS_LEVEL_RESULT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_TREASURE_CHEST_NTF,EncodeSC_TREASURE_CHEST_NTF,DecodeSC_TREASURE_CHEST_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_OPEN_TREASURE_CHEST_REQ,EncodeCS_OPEN_TREASURE_CHEST_REQ,DecodeCS_OPEN_TREASURE_CHEST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_OPEN_TREASURE_CHEST_ACK,EncodeSC_OPEN_TREASURE_CHEST_ACK,DecodeSC_OPEN_TREASURE_CHEST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_LEVEL_INFO,EncodeSC_LEVEL_INFO,DecodeSC_LEVEL_INFO);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_BATTLE_BOX_REQ,EncodeCS_GET_BATTLE_BOX_REQ,DecodeCS_GET_BATTLE_BOX_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_BATTLE_BOX_ACK,EncodeSC_GET_BATTLE_BOX_ACK,DecodeSC_GET_BATTLE_BOX_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_BATTLE_GOT_STAR_NTF,EncodeSC_BATTLE_GOT_STAR_NTF,DecodeSC_BATTLE_GOT_STAR_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_VOLATILE_PRO,EncodeSC_SELF_VOLATILE_PRO,DecodeSC_SELF_VOLATILE_PRO);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_CLIENT_CONFIG_NTF,EncodeSC_CLIENT_CONFIG_NTF,DecodeSC_CLIENT_CONFIG_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_CLIENT_CONFIG_UPDATE,EncodeCS_CLIENT_CONFIG_UPDATE,DecodeCS_CLIENT_CONFIG_UPDATE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ERROR_NTF,EncodeSC_ERROR_NTF,DecodeSC_ERROR_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_ITEM_NTF,EncodeSC_SELF_ITEM_NTF,DecodeSC_SELF_ITEM_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_MOVE_ITEM_REQ,EncodeCS_MOVE_ITEM_REQ,DecodeCS_MOVE_ITEM_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_MOVE_ITEM_ACK,EncodeSC_MOVE_ITEM_ACK,DecodeSC_MOVE_ITEM_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_BAG_NTF,EncodeSC_ROLE_BAG_NTF,DecodeSC_ROLE_BAG_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_EQUIP_NTF,EncodeSC_ROLE_EQUIP_NTF,DecodeSC_ROLE_EQUIP_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_EQUIP_REQ,EncodeCS_ROLE_EQUIP_REQ,DecodeCS_ROLE_EQUIP_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_EVENT_REQ,EncodeCS_ROLE_EVENT_REQ,DecodeCS_ROLE_EVENT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_LEAVE_NTF,EncodeSC_ROLE_BAG_SINGLE_LEAVE_NTF,DecodeSC_ROLE_BAG_SINGLE_LEAVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_BAG_SINGLE_ENTER_NTF,EncodeSC_ROLE_BAG_SINGLE_ENTER_NTF,DecodeSC_ROLE_BAG_SINGLE_ENTER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_LEAVE_NTF,EncodeSC_ROLE_EQUIP_SINGLE_LEAVE_NTF,DecodeSC_ROLE_EQUIP_SINGLE_LEAVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_EQUIP_SINGLE_ENTER_NTF,EncodeSC_ROLE_EQUIP_SINGLE_ENTER_NTF,DecodeSC_ROLE_EQUIP_SINGLE_ENTER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_ITEMUP_RPT,EncodeCS_ROLE_ITEMUP_RPT,DecodeCS_ROLE_ITEMUP_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_ITEMUP_NTF,EncodeSC_ROLE_ITEMUP_NTF,DecodeSC_ROLE_ITEMUP_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_ITEMUP_ACK,EncodeSC_ROLE_ITEMUP_ACK,DecodeSC_ROLE_ITEMUP_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_SUIT_NTF,EncodeSC_ROLE_SUIT_NTF,DecodeSC_ROLE_SUIT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_HOLE_ON_REQ,EncodeCS_ROLE_HOLE_ON_REQ,DecodeCS_ROLE_HOLE_ON_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_HOLE_ON_ACK,EncodeSC_ROLE_HOLE_ON_ACK,DecodeSC_ROLE_HOLE_ON_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_HOLE_OFF_REQ,EncodeCS_ROLE_HOLE_OFF_REQ,DecodeCS_ROLE_HOLE_OFF_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_HOLE_OFF_ACK,EncodeSC_ROLE_HOLE_OFF_ACK,DecodeSC_ROLE_HOLE_OFF_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_HOLE_REFRESH_REQ,EncodeCS_ROLE_HOLE_REFRESH_REQ,DecodeCS_ROLE_HOLE_REFRESH_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_HOLE_REFRESH_ACK,EncodeSC_ROLE_HOLE_REFRESH_ACK,DecodeSC_ROLE_HOLE_REFRESH_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_HOLE_MERGE_REQ,EncodeCS_ROLE_HOLE_MERGE_REQ,DecodeCS_ROLE_HOLE_MERGE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_HOLE_MERGE_ACK,EncodeSC_ROLE_HOLE_MERGE_ACK,DecodeSC_ROLE_HOLE_MERGE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_HOLE_MERGE2_REQ,EncodeCS_ROLE_HOLE_MERGE2_REQ,DecodeCS_ROLE_HOLE_MERGE2_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_HOLE_MERGE2_ACK,EncodeSC_ROLE_HOLE_MERGE2_ACK,DecodeSC_ROLE_HOLE_MERGE2_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_LOCK_RPT,EncodeCS_LOCK_RPT,DecodeCS_LOCK_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_USEITEM_REQ,EncodeCS_ROLE_USEITEM_REQ,DecodeCS_ROLE_USEITEM_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_USEITEM_REQ,EncodeCS_PET_USEITEM_REQ,DecodeCS_PET_USEITEM_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_USEITEM_ACK,EncodeSC_PET_USEITEM_ACK,DecodeSC_PET_USEITEM_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_LIMIT_NTF,EncodeSC_ROLE_LIMIT_NTF,DecodeSC_ROLE_LIMIT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GATHER_ITEM_MERGE_REQ,EncodeCS_GATHER_ITEM_MERGE_REQ,DecodeCS_GATHER_ITEM_MERGE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GATHER_ITEM_MERGE_ACK,EncodeSC_GATHER_ITEM_MERGE_ACK,DecodeSC_GATHER_ITEM_MERGE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GATHER_INFO_NTF,EncodeSC_GATHER_INFO_NTF,DecodeSC_GATHER_INFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GATHER_SINGLE_ENTER_NTF,EncodeSC_GATHER_SINGLE_ENTER_NTF,DecodeSC_GATHER_SINGLE_ENTER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GATHER_SINGLE_LEAVE_NTF,EncodeSC_GATHER_SINGLE_LEAVE_NTF,DecodeSC_GATHER_SINGLE_LEAVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_PROD_REQ,EncodeCS_ROLE_PROD_REQ,DecodeCS_ROLE_PROD_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_PROD_ACK,EncodeSC_ROLE_PROD_ACK,DecodeSC_ROLE_PROD_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_USEITEM_START_ACK,EncodeSC_ROLE_USEITEM_START_ACK,DecodeSC_ROLE_USEITEM_START_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_USEITEM_END_ACK,EncodeSC_ROLE_USEITEM_END_ACK,DecodeSC_ROLE_USEITEM_END_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_USER_SKILLS_NTF,EncodeSC_USER_SKILLS_NTF,DecodeSC_USER_SKILLS_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_LEARN_SKILL,EncodeCS_LEARN_SKILL,DecodeCS_LEARN_SKILL);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SKILL_INFO_NTF,EncodeSC_SKILL_INFO_NTF,DecodeSC_SKILL_INFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_REMOVE_SKILL_NTF,EncodeSC_REMOVE_SKILL_NTF,DecodeSC_REMOVE_SKILL_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SKILL_POINT_NTF,EncodeSC_SKILL_POINT_NTF,DecodeSC_SKILL_POINT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SKILL_RESET_ALL_REQ,EncodeCS_SKILL_RESET_ALL_REQ,DecodeCS_SKILL_RESET_ALL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_USER_PETS_NTF,EncodeSC_USER_PETS_NTF,DecodeSC_USER_PETS_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_INFO_NTF,EncodeSC_PET_INFO_NTF,DecodeSC_PET_INFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SET_BATTLE_PET_REQ,EncodeCS_SET_BATTLE_PET_REQ,DecodeCS_SET_BATTLE_PET_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SET_BATTLE_PET_NTF,EncodeSC_SET_BATTLE_PET_NTF,DecodeSC_SET_BATTLE_PET_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_GET_EXP_RPT,EncodeCS_PET_GET_EXP_RPT,DecodeCS_PET_GET_EXP_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_LEVEL_NTF,EncodeSC_PET_LEVEL_NTF,DecodeSC_PET_LEVEL_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_UP_REQ,EncodeCS_PET_UP_REQ,DecodeCS_PET_UP_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_UP_ACK,EncodeSC_PET_UP_ACK,DecodeSC_PET_UP_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_MERGE_REQ,EncodeCS_PET_MERGE_REQ,DecodeCS_PET_MERGE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_ADD_NTF,EncodeSC_PET_ADD_NTF,DecodeSC_PET_ADD_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PETS_RECORD_NTF,EncodeSC_PETS_RECORD_NTF,DecodeSC_PETS_RECORD_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_BAG_NTF,EncodeSC_PET_BAG_NTF,DecodeSC_PET_BAG_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_EQUIP_REQ,EncodeCS_PET_EQUIP_REQ,DecodeCS_PET_EQUIP_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_EVENT_REQ,EncodeCS_PET_EVENT_REQ,DecodeCS_PET_EVENT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_LEAVE_NTF,EncodeSC_PET_BAG_SINGLE_LEAVE_NTF,DecodeSC_PET_BAG_SINGLE_LEAVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_BAG_SINGLE_ENTER_NTF,EncodeSC_PET_BAG_SINGLE_ENTER_NTF,DecodeSC_PET_BAG_SINGLE_ENTER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PET_TEAM_RPT,EncodeCS_PET_TEAM_RPT,DecodeCS_PET_TEAM_RPT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_TEAM_NTF,EncodeSC_PET_TEAM_NTF,DecodeSC_PET_TEAM_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_GET_EXP_ACK,EncodeSC_PET_GET_EXP_ACK,DecodeSC_PET_GET_EXP_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_SINGLE_ENTER_NTF,EncodeSC_PET_SINGLE_ENTER_NTF,DecodeSC_PET_SINGLE_ENTER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_SINGLE_LEAVE_NTF,EncodeSC_PET_SINGLE_LEAVE_NTF,DecodeSC_PET_SINGLE_LEAVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PET_EVENT_ACK,EncodeSC_PET_EVENT_ACK,DecodeSC_PET_EVENT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_MOVE_SYNC_MODE,EncodeCS_MOVE_SYNC_MODE,DecodeCS_MOVE_SYNC_MODE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_MOVE_BEGIN,EncodeCS_MOVE_BEGIN,DecodeCS_MOVE_BEGIN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_MOVE_STOP,EncodeCS_MOVE_STOP,DecodeCS_MOVE_STOP);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_MOVE_BEGIN,EncodeSC_MOVE_BEGIN,DecodeSC_MOVE_BEGIN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_MOVE_STOP,EncodeSC_MOVE_STOP,DecodeSC_MOVE_STOP);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_OTHER_APPEAR,EncodeSC_OTHER_APPEAR,DecodeSC_OTHER_APPEAR);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_OTHER_UPDATE,EncodeSC_OTHER_UPDATE,DecodeSC_OTHER_UPDATE);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_OTHER_DISAPPEAR,EncodeSC_OTHER_DISAPPEAR,DecodeSC_OTHER_DISAPPEAR);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_CHAT,EncodeCS_CHAT,DecodeCS_CHAT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_CHAT,EncodeSC_CHAT,DecodeSC_CHAT);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_CHAT_ACK,EncodeSC_CHAT_ACK,DecodeSC_CHAT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_FRIENDS_LIST_NTF,EncodeSC_FRIENDS_LIST_NTF,DecodeSC_FRIENDS_LIST_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_QUERY_ROLE_REQ,EncodeCS_QUERY_ROLE_REQ,DecodeCS_QUERY_ROLE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_QUERY_ROLE_ACK,EncodeSC_QUERY_ROLE_ACK,DecodeSC_QUERY_ROLE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ADD_FRIEND_REQ,EncodeCS_ADD_FRIEND_REQ,DecodeCS_ADD_FRIEND_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ADD_FRIEND_ACK,EncodeSC_ADD_FRIEND_ACK,DecodeSC_ADD_FRIEND_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_REMOVE_FRIEND_REQ,EncodeCS_REMOVE_FRIEND_REQ,DecodeCS_REMOVE_FRIEND_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_REMOVE_FRIEND_ACK,EncodeSC_REMOVE_FRIEND_ACK,DecodeSC_REMOVE_FRIEND_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_DONATE_ACTION_POINT_REQ,EncodeCS_DONATE_ACTION_POINT_REQ,DecodeCS_DONATE_ACTION_POINT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_ACK,EncodeSC_DONATE_ACTION_POINT_ACK,DecodeSC_DONATE_ACTION_POINT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_DONATE_ACTION_POINT_OTHER_NTF,EncodeSC_DONATE_ACTION_POINT_OTHER_NTF,DecodeSC_DONATE_ACTION_POINT_OTHER_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_RECEIVE_ACTION_POINT_REQ,EncodeCS_RECEIVE_ACTION_POINT_REQ,DecodeCS_RECEIVE_ACTION_POINT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_RECEIVE_ACTION_POINT_ACK,EncodeSC_RECEIVE_ACTION_POINT_ACK,DecodeSC_RECEIVE_ACTION_POINT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_RESPONSE_ADD_FRIEND_REQ,EncodeCS_RESPONSE_ADD_FRIEND_REQ,DecodeCS_RESPONSE_ADD_FRIEND_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_REQUEST_ADD_FRIEND_NTF,EncodeSC_REQUEST_ADD_FRIEND_NTF,DecodeSC_REQUEST_ADD_FRIEND_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_FRIEND_INFO_CHANGE_NTF,EncodeSC_FRIEND_INFO_CHANGE_NTF,DecodeSC_FRIEND_INFO_CHANGE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_FRIEND_REMOVE_INVITE_NTF,EncodeSC_FRIEND_REMOVE_INVITE_NTF,DecodeSC_FRIEND_REMOVE_INVITE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_FRIEND_ASSIST_LIST_REQ,EncodeCS_FRIEND_ASSIST_LIST_REQ,DecodeCS_FRIEND_ASSIST_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_FRIEND_ASSIST_LIST_ACK,EncodeSC_FRIEND_ASSIST_LIST_ACK,DecodeSC_FRIEND_ASSIST_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SELECT_FRIEND_ASSIST_REQ,EncodeCS_SELECT_FRIEND_ASSIST_REQ,DecodeCS_SELECT_FRIEND_ASSIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELECT_FRIEND_ASSIST_ACK,EncodeSC_SELECT_FRIEND_ASSIST_ACK,DecodeSC_SELECT_FRIEND_ASSIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_FRIEND_EPINFO_NTF,EncodeSC_FRIEND_EPINFO_NTF,DecodeSC_FRIEND_EPINFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_FRIENDS_LIST_REQ,EncodeCS_FRIENDS_LIST_REQ,DecodeCS_FRIENDS_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ABYSS_TRIGGER_ACK,EncodeSC_ABYSS_TRIGGER_ACK,DecodeSC_ABYSS_TRIGGER_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ABYSS_OPEN_REQ,EncodeCS_ABYSS_OPEN_REQ,DecodeCS_ABYSS_OPEN_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ABYSS_OPEN_ACK,EncodeSC_ABYSS_OPEN_ACK,DecodeSC_ABYSS_OPEN_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ABYSS_OPEN_NTF,EncodeSC_ABYSS_OPEN_NTF,DecodeSC_ABYSS_OPEN_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_ABYSS_TRIGGER_LIST_REQ,EncodeCS_GET_ABYSS_TRIGGER_LIST_REQ,DecodeCS_GET_ABYSS_TRIGGER_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_ABYSS_TRIGGER_LIST_ACK,EncodeSC_GET_ABYSS_TRIGGER_LIST_ACK,DecodeSC_GET_ABYSS_TRIGGER_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_ABYSS_OPEN_LIST_REQ,EncodeCS_GET_ABYSS_OPEN_LIST_REQ,DecodeCS_GET_ABYSS_OPEN_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_LIST_ACK,EncodeSC_GET_ABYSS_OPEN_LIST_ACK,DecodeSC_GET_ABYSS_OPEN_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_ABYSS_OPEN_REC_REQ,EncodeCS_GET_ABYSS_OPEN_REC_REQ,DecodeCS_GET_ABYSS_OPEN_REC_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_ABYSS_OPEN_REC_ACK,EncodeSC_GET_ABYSS_OPEN_REC_ACK,DecodeSC_GET_ABYSS_OPEN_REC_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ENTER_ABYSS_ACK,EncodeSC_ENTER_ABYSS_ACK,DecodeSC_ENTER_ABYSS_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ABYSS_RUN_NTF,EncodeSC_ABYSS_RUN_NTF,DecodeSC_ABYSS_RUN_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_MAIL_LIST_NTF,EncodeSC_MAIL_LIST_NTF,DecodeSC_MAIL_LIST_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SEND_MAIL_REQ,EncodeCS_SEND_MAIL_REQ,DecodeCS_SEND_MAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SEND_MAIL_ACK,EncodeSC_SEND_MAIL_ACK,DecodeSC_SEND_MAIL_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_ITEM_FROM_MAIL_REQ,EncodeCS_GET_ITEM_FROM_MAIL_REQ,DecodeCS_GET_ITEM_FROM_MAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_MONEY_FROM_MAIL_REQ,EncodeCS_GET_MONEY_FROM_MAIL_REQ,DecodeCS_GET_MONEY_FROM_MAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_READ_MAIL_REQ,EncodeCS_READ_MAIL_REQ,DecodeCS_READ_MAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_READ_MAIL_ACK,EncodeSC_READ_MAIL_ACK,DecodeSC_READ_MAIL_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_DELETE_MAIL_REQ,EncodeCS_DELETE_MAIL_REQ,DecodeCS_DELETE_MAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_DELETE_MAIL_ACK,EncodeSC_DELETE_MAIL_ACK,DecodeSC_DELETE_MAIL_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_UPDATE_MAIL_NTF,EncodeSC_UPDATE_MAIL_NTF,DecodeSC_UPDATE_MAIL_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_ACTION_POINT_BOUGHT_NTF,EncodeSC_SHOP_ACTION_POINT_BOUGHT_NTF,DecodeSC_SHOP_ACTION_POINT_BOUGHT_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_BUY_ACTION_POINT_REQ,EncodeCS_SHOP_BUY_ACTION_POINT_REQ,DecodeCS_SHOP_BUY_ACTION_POINT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_BUY_ACTION_POINT_ACK,EncodeSC_SHOP_BUY_ACTION_POINT_ACK,DecodeSC_SHOP_BUY_ACTION_POINT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_CHEAP_NTF,EncodeSC_SHOP_BUY_PET_INFO_CHEAP_NTF,DecodeSC_SHOP_BUY_PET_INFO_CHEAP_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_INFO_EXPENSIVE_NTF,EncodeSC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF,DecodeSC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_BUY_PET_REQ,EncodeCS_SHOP_BUY_PET_REQ,DecodeCS_SHOP_BUY_PET_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_BUY_PET_ACK,EncodeSC_SHOP_BUY_PET_ACK,DecodeSC_SHOP_BUY_PET_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_GET_GOODSLIST_REQ,EncodeCS_SHOP_GET_GOODSLIST_REQ,DecodeCS_SHOP_GET_GOODSLIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_GET_GOODSLIST_ACK,EncodeSC_SHOP_GET_GOODSLIST_ACK,DecodeSC_SHOP_GET_GOODSLIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_BUY_GOODS_REQ,EncodeCS_SHOP_BUY_GOODS_REQ,DecodeCS_SHOP_BUY_GOODS_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_BUY_GOODS_ACK,EncodeSC_SHOP_BUY_GOODS_ACK,DecodeSC_SHOP_BUY_GOODS_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_SECRET_GOODS_REQ,EncodeCS_SHOP_SECRET_GOODS_REQ,DecodeCS_SHOP_SECRET_GOODS_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_SECRET_GOODS_ACK,EncodeSC_SHOP_SECRET_GOODS_ACK,DecodeSC_SHOP_SECRET_GOODS_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_SECRET_BUY_REQ,EncodeCS_SHOP_SECRET_BUY_REQ,DecodeCS_SHOP_SECRET_BUY_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SHOP_SECRET_BUY_ACK,EncodeSC_SHOP_SECRET_BUY_ACK,DecodeSC_SHOP_SECRET_BUY_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SHOP_SECRET_REFRESH_REQ,EncodeCS_SHOP_SECRET_REFRESH_REQ,DecodeCS_SHOP_SECRET_REFRESH_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_SELF_PVP_PRO_REQ,EncodeCS_SELF_PVP_PRO_REQ,DecodeCS_SELF_PVP_PRO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_SELF_PVP_PRO_ACK,EncodeSC_SELF_PVP_PRO_ACK,DecodeSC_SELF_PVP_PRO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_PVP_MATCH_REQ,EncodeCS_GET_PVP_MATCH_REQ,DecodeCS_GET_PVP_MATCH_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_PVP_MATCH_ACK,EncodeSC_GET_PVP_MATCH_ACK,DecodeSC_GET_PVP_MATCH_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ENTER_PVP_REQ,EncodeCS_ENTER_PVP_REQ,DecodeCS_ENTER_PVP_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ENTER_PVP_ACK,EncodeSC_ENTER_PVP_ACK,DecodeSC_ENTER_PVP_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PVP_RETULT_REQ,EncodeCS_PVP_RETULT_REQ,DecodeCS_PVP_RETULT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PVP_RETULT_ACK,EncodeSC_PVP_RETULT_ACK,DecodeSC_PVP_RETULT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_RANK_LIST_REQ,EncodeCS_GET_RANK_LIST_REQ,DecodeCS_GET_RANK_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_RANK_LIST_ACK,EncodeSC_GET_RANK_LIST_ACK,DecodeSC_GET_RANK_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_PVP_MILITARY_REWARD_REQ,EncodeCS_GET_PVP_MILITARY_REWARD_REQ,DecodeCS_GET_PVP_MILITARY_REWARD_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_PVP_MILITARY_REWARD_ACK,EncodeSC_GET_PVP_MILITARY_REWARD_ACK,DecodeSC_GET_PVP_MILITARY_REWARD_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_PVP_REPUTE_REWARD_REQ,EncodeCS_GET_PVP_REPUTE_REWARD_REQ,DecodeCS_GET_PVP_REPUTE_REWARD_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_PVP_REPUTE_REWARD_ACK,EncodeSC_GET_PVP_REPUTE_REWARD_ACK,DecodeSC_GET_PVP_REPUTE_REWARD_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_PVP_BUY_CHALLENGE_TIMES_REQ,EncodeCS_PVP_BUY_CHALLENGE_TIMES_REQ,DecodeCS_PVP_BUY_CHALLENGE_TIMES_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PVP_BUY_CHALLENGE_TIMES_ACK,EncodeSC_PVP_BUY_CHALLENGE_TIMES_ACK,DecodeSC_PVP_BUY_CHALLENGE_TIMES_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_PVP_TIME_NTF,EncodeSC_PVP_TIME_NTF,DecodeSC_PVP_TIME_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_PVP_ROLE_INFO_REQ,EncodeCS_GET_PVP_ROLE_INFO_REQ,DecodeCS_GET_PVP_ROLE_INFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_PVP_ROLE_INFO_ACK,EncodeSC_GET_PVP_ROLE_INFO_ACK,DecodeSC_GET_PVP_ROLE_INFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_VIP_INFO_NTF,EncodeSC_VIP_INFO_NTF,DecodeSC_VIP_INFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_VIP_RMBINFO_NTF,EncodeSC_VIP_RMBINFO_NTF,DecodeSC_VIP_RMBINFO_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_VIP_MONTHCARD_NTF,EncodeSC_VIP_MONTHCARD_NTF,DecodeSC_VIP_MONTHCARD_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_VIP_MONTHCARD_GETITEM_REQ,EncodeCS_VIP_MONTHCARD_GETITEM_REQ,DecodeCS_VIP_MONTHCARD_GETITEM_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_VIP_MONTHCARD_GETITEM_ACK,EncodeSC_VIP_MONTHCARD_GETITEM_ACK,DecodeSC_VIP_MONTHCARD_GETITEM_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_SIGN_NTF,EncodeSC_GIFT_SIGN_NTF,DecodeSC_GIFT_SIGN_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_SIGN_VIP_NTF,EncodeSC_GIFT_SIGN_VIP_NTF,DecodeSC_GIFT_SIGN_VIP_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_SIGN,EncodeCS_GIFT_SIGN,DecodeCS_GIFT_SIGN);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_SIGN_VIP,EncodeCS_GIFT_SIGN_VIP,DecodeCS_GIFT_SIGN_VIP);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_LEVEL_NTF,EncodeSC_GIFT_LEVEL_NTF,DecodeSC_GIFT_LEVEL_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_LEVEL,EncodeCS_GIFT_LEVEL,DecodeCS_GIFT_LEVEL);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_DAY_NTF,EncodeSC_GIFT_DAY_NTF,DecodeSC_GIFT_DAY_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_DAY_UPD,EncodeSC_GIFT_DAY_UPD,DecodeSC_GIFT_DAY_UPD);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_DAY_SCORE_REQ,EncodeCS_GIFT_DAY_SCORE_REQ,DecodeCS_GIFT_DAY_SCORE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_DAY_BOX_REQ,EncodeCS_GIFT_DAY_BOX_REQ,DecodeCS_GIFT_DAY_BOX_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_DAY_BOX_NTF,EncodeSC_GIFT_DAY_BOX_NTF,DecodeSC_GIFT_DAY_BOX_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_JIHUOMA_REQ,EncodeCS_GIFT_JIHUOMA_REQ,DecodeCS_GIFT_JIHUOMA_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_JIHUOMA_ACK,EncodeSC_GIFT_JIHUOMA_ACK,DecodeSC_GIFT_JIHUOMA_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_EP_INFO_REQ,EncodeCS_GIFT_EP_INFO_REQ,DecodeCS_GIFT_EP_INFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_EP_INFO_ACK,EncodeSC_GIFT_EP_INFO_ACK,DecodeSC_GIFT_EP_INFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GIFT_RECEIVE_EP_REQ,EncodeCS_GIFT_RECEIVE_EP_REQ,DecodeCS_GIFT_RECEIVE_EP_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GIFT_RECEIVE_EP_ACK,EncodeSC_GIFT_RECEIVE_EP_ACK,DecodeSC_GIFT_RECEIVE_EP_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_WB_INFO_REFRESH_REQ,EncodeCS_WB_INFO_REFRESH_REQ,DecodeCS_WB_INFO_REFRESH_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_INFO_REFRESH_ACK,EncodeSC_WB_INFO_REFRESH_ACK,DecodeSC_WB_INFO_REFRESH_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_WB_ADD_BUF_REQ,EncodeCS_WB_ADD_BUF_REQ,DecodeCS_WB_ADD_BUF_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_ADD_BUF_ACK,EncodeSC_WB_ADD_BUF_ACK,DecodeSC_WB_ADD_BUF_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_WB_RELIVE_REQ,EncodeCS_WB_RELIVE_REQ,DecodeCS_WB_RELIVE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_RELIVE_ACK,EncodeSC_WB_RELIVE_ACK,DecodeSC_WB_RELIVE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_KILLED_NTF,EncodeSC_WB_KILLED_NTF,DecodeSC_WB_KILLED_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_RUN_NTF,EncodeSC_WB_RUN_NTF,DecodeSC_WB_RUN_NTF);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_WB_ENTER_REQ,EncodeCS_WB_ENTER_REQ,DecodeCS_WB_ENTER_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_ENTER_ACK,EncodeSC_WB_ENTER_ACK,DecodeSC_WB_ENTER_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_WB_RESULT_REQ,EncodeCS_WB_RESULT_REQ,DecodeCS_WB_RESULT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_WB_RESULT_ACK,EncodeSC_WB_RESULT_ACK,DecodeSC_WB_RESULT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_ROLE_RANK_REQ,EncodeCS_ROLE_RANK_REQ,DecodeCS_ROLE_RANK_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_ROLE_RANK_ACK,EncodeSC_ROLE_RANK_ACK,DecodeSC_ROLE_RANK_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_PT_BASEINFO_REQ,EncodeCS_GET_PT_BASEINFO_REQ,DecodeCS_GET_PT_BASEINFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_PT_BASEINFO_ACK,EncodeSC_GET_PT_BASEINFO_ACK,DecodeSC_GET_PT_BASEINFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_BUY_PT_TIMES_REQ,EncodeCS_BUY_PT_TIMES_REQ,DecodeCS_BUY_PT_TIMES_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_BUY_PT_TIMES_ACK,EncodeSC_BUY_PT_TIMES_ACK,DecodeSC_BUY_PT_TIMES_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_MD_BASEINFO_REQ,EncodeCS_GET_MD_BASEINFO_REQ,DecodeCS_GET_MD_BASEINFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_MD_BASEINFO_ACK,EncodeSC_GET_MD_BASEINFO_ACK,DecodeSC_GET_MD_BASEINFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_BUY_MD_TIMES_REQ,EncodeCS_BUY_MD_TIMES_REQ,DecodeCS_BUY_MD_TIMES_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_BUY_MD_TIMES_ACK,EncodeSC_BUY_MD_TIMES_ACK,DecodeSC_BUY_MD_TIMES_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GET_FD_BASEINFO_REQ,EncodeCS_GET_FD_BASEINFO_REQ,DecodeCS_GET_FD_BASEINFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GET_FD_BASEINFO_ACK,EncodeSC_GET_FD_BASEINFO_ACK,DecodeSC_GET_FD_BASEINFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_BUY_FD_TIMES_REQ,EncodeCS_BUY_FD_TIMES_REQ,DecodeCS_BUY_FD_TIMES_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_BUY_FD_TIMES_ACK,EncodeSC_BUY_FD_TIMES_ACK,DecodeSC_BUY_FD_TIMES_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_INFO_REQ,EncodeCS_GUILD_INFO_REQ,DecodeCS_GUILD_INFO_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_LIST_REQ,EncodeCS_GUILD_LIST_REQ,DecodeCS_GUILD_LIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_LIST_ACK,EncodeSC_GUILD_LIST_ACK,DecodeSC_GUILD_LIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_ENTER_REQ,EncodeCS_GUILD_ENTER_REQ,DecodeCS_GUILD_ENTER_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_ENTER_ACK,EncodeSC_GUILD_ENTER_ACK,DecodeSC_GUILD_ENTER_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_CREATE_REQ,EncodeCS_GUILD_CREATE_REQ,DecodeCS_GUILD_CREATE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_CREATE_ACK,EncodeSC_GUILD_CREATE_ACK,DecodeSC_GUILD_CREATE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_LEAVE_REQ,EncodeCS_GUILD_LEAVE_REQ,DecodeCS_GUILD_LEAVE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_LEAVE_ACK,EncodeSC_GUILD_LEAVE_ACK,DecodeSC_GUILD_LEAVE_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_BASEINFO_ACK,EncodeSC_GUILD_BASEINFO_ACK,DecodeSC_GUILD_BASEINFO_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_LOG_REQ,EncodeCS_GUILD_LOG_REQ,DecodeCS_GUILD_LOG_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_LOG_ACK,EncodeSC_GUILD_LOG_ACK,DecodeSC_GUILD_LOG_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_CHANGE_NOTICE_REQ,EncodeCS_GUILD_CHANGE_NOTICE_REQ,DecodeCS_GUILD_CHANGE_NOTICE_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_DETAIL_REQ,EncodeCS_GUILD_DETAIL_REQ,DecodeCS_GUILD_DETAIL_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_DETAIL_ACK,EncodeSC_GUILD_DETAIL_ACK,DecodeSC_GUILD_DETAIL_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_ENTERLIST_REQ,EncodeCS_GUILD_ENTERLIST_REQ,DecodeCS_GUILD_ENTERLIST_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_ENTERLIST_ACK,EncodeSC_GUILD_ENTERLIST_ACK,DecodeSC_GUILD_ENTERLIST_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_ENTER_ACCEPT_REQ,EncodeCS_GUILD_ENTER_ACCEPT_REQ,DecodeCS_GUILD_ENTER_ACCEPT_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_ENTER_ACCEPT_ACK,EncodeSC_GUILD_ENTER_ACCEPT_ACK,DecodeSC_GUILD_ENTER_ACCEPT_ACK);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.CSID_GUILD_ENTER_CLEAR_REQ,EncodeCS_GUILD_ENTER_CLEAR_REQ,DecodeCS_GUILD_ENTER_CLEAR_REQ);

	SGDP.GetInstance().AddEncode_Decode((int)EN_CliProto_MessageID.SCID_GUILD_ENTER_CLEAR_ACK,EncodeSC_GUILD_ENTER_CLEAR_ACK,DecodeSC_GUILD_ENTER_CLEAR_ACK);

}
public static	int		EncodeLGPKG_LOGIN_REQ(CMessage msg,ref CNetData poNetData)
{
	LGPKG_LOGIN_REQ pstIn = (LGPKG_LOGIN_REQ)msg;

	if(-1 == poNetData.AddString(pstIn.m_Type))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TokenLen))
		return -1;

   if ((int)pstIn.m_TokenLen > (int)LG_INFO_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Token,(int)pstIn.m_TokenLen))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ServerGroup))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ResourceLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGPKG_LOGIN_REQ(ref CNetData poNetData)
{
	LGPKG_LOGIN_REQ pstOut = new LGPKG_LOGIN_REQ();

	if(-1 == poNetData.DelString(ref pstOut.m_Type))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TokenLen))
		return null;

   if ((int)pstOut.m_TokenLen > (int)LG_INFO_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Token,(int)pstOut.m_TokenLen))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ServerGroup))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ResourceLevel))
		return null;

	return pstOut;
}

public static	int		EncodeLGPKG_ACT_REQ(CMessage msg,ref CNetData poNetData)
{
	LGPKG_ACT_REQ pstIn = (LGPKG_ACT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_SubReq))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_InfoLen))
		return -1;

   if ((int)pstIn.m_InfoLen > (int)LG_INFO_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Info,(int)pstIn.m_InfoLen))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGPKG_ACT_REQ(ref CNetData poNetData)
{
	LGPKG_ACT_REQ pstOut = new LGPKG_ACT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_SubReq))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_InfoLen))
		return null;

   if ((int)pstOut.m_InfoLen > (int)LG_INFO_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Info,(int)pstOut.m_InfoLen))
		return null;

	return pstOut;
}

public static	int		EncodeSCDNInfo(CMessage msg,ref CNetData poNetData)
{
	SCDNInfo pstIn = (SCDNInfo)msg;

	if(-1 == poNetData.AddString(pstIn.m_Path))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSCDNInfo(ref CNetData poNetData)
{
	SCDNInfo pstOut = new SCDNInfo();

	if(-1 == poNetData.DelString(ref pstOut.m_Path))
		return null;

	return pstOut;
}

public static	int		EncodeSBundleInfoList(CMessage msg,ref CNetData poNetData)
{
	SBundleInfoList pstIn = (SBundleInfoList)msg;

	if(-1 == poNetData.AddString(pstIn.m_Path))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CDNCount))
		return -1;

	int i;
	if ((int)pstIn.m_CDNCount > (int)8 || (int)pstIn.m_CDNCount < 0)
       return -1;
	for(i = 0; i < (int)8; ++i)
	{
		if(i >= (int)pstIn.m_CDNCount)
			break;
		EncodeSCDNInfo(pstIn.m_CDN[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSBundleInfoList(ref CNetData poNetData)
{
	SBundleInfoList pstOut = new SBundleInfoList();

	if(-1 == poNetData.DelString(ref pstOut.m_Path))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CDNCount))
		return null;

	int i;
	if ((int)pstOut.m_CDNCount > (int)8 || (int)pstOut.m_CDNCount < 0)
       return null;
	for(i = 0; i < (int)8; ++i)
	{
		if(i >= (int)pstOut.m_CDNCount)
			break;
		pstOut.m_CDN[i]	= (SCDNInfo)DecodeSCDNInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeLGDT_LOGIN_SUCCESS(CMessage msg,ref CNetData poNetData)
{
	LGDT_LOGIN_SUCCESS pstIn = (LGDT_LOGIN_SUCCESS)msg;

	if(-1 == poNetData.AddString(pstIn.m_Username))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_DigitID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SessionID,(int)LG_SESSIONID_LEN))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sig,(int)LG_SIG_LEN))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_InfoLen))
		return -1;

   if ((int)pstIn.m_InfoLen > (int)LG_INFO_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Info,(int)pstIn.m_InfoLen))
		return -1;

		HeaderProto.EncodeSServerInfo(pstIn.m_GateInfo,ref poNetData);
		EncodeSBundleInfoList(pstIn.m_BundleInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGDT_LOGIN_SUCCESS(ref CNetData poNetData)
{
	LGDT_LOGIN_SUCCESS pstOut = new LGDT_LOGIN_SUCCESS();

	if(-1 == poNetData.DelString(ref pstOut.m_Username))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_DigitID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SessionID,(int)LG_SESSIONID_LEN))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sig,(int)LG_SIG_LEN))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_InfoLen))
		return null;

   if ((int)pstOut.m_InfoLen > (int)LG_INFO_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Info,(int)pstOut.m_InfoLen))
		return null;

	pstOut.m_GateInfo	= (HeaderProto.SServerInfo)HeaderProto.DecodeSServerInfo(ref poNetData);
	pstOut.m_BundleInfo	= (SBundleInfoList)DecodeSBundleInfoList(ref poNetData);
	return pstOut;
}

public static	int		EncodeLGDT_LOGIN_FAIL(CMessage msg,ref CNetData poNetData)
{
	LGDT_LOGIN_FAIL pstIn = (LGDT_LOGIN_FAIL)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SubErrCode))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_MaintainInfo))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGDT_LOGIN_FAIL(ref CNetData poNetData)
{
	LGDT_LOGIN_FAIL pstOut = new LGDT_LOGIN_FAIL();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SubErrCode))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_MaintainInfo))
		return null;

	return pstOut;
}

public static	int		EncodeLGDT_LOGIN_INTERMEDIATE(CMessage msg,ref CNetData poNetData)
{
	LGDT_LOGIN_INTERMEDIATE pstIn = (LGDT_LOGIN_INTERMEDIATE)msg;

	if(-1 == poNetData.Add(pstIn.m_SubAck))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_InfoLen))
		return -1;

   if ((int)pstIn.m_InfoLen > (int)LG_INFO_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Info,(int)pstIn.m_InfoLen))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGDT_LOGIN_INTERMEDIATE(ref CNetData poNetData)
{
	LGDT_LOGIN_INTERMEDIATE pstOut = new LGDT_LOGIN_INTERMEDIATE();

	if(-1 == poNetData.Del(ref pstOut.m_SubAck))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_InfoLen))
		return null;

   if ((int)pstOut.m_InfoLen > (int)LG_INFO_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Info,(int)pstOut.m_InfoLen))
		return null;

	return pstOut;
}

public static	int EncodeUnLGDT_ACT_REPLY(CMessage msg,int iSelect, ref CNetData poNetData)
{
	LGDT_ACT_REPLY	pstIn	=	(LGDT_ACT_REPLY)msg;
	switch(iSelect){
	case (int)LG_SUCCESS:
		return EncodeLGDT_LOGIN_SUCCESS(pstIn.m_Succ,ref poNetData);
	case (int)LG_FAIL:
		return EncodeLGDT_LOGIN_FAIL(pstIn.m_Fail,ref poNetData);
	case (int)LG_INTER:
		return EncodeLGDT_LOGIN_INTERMEDIATE(pstIn.m_Inter,ref poNetData);
	default: return -1;
	}
}

public	static	void DecodeUnLGDT_ACT_REPLY(CMessage msg,int iSelect, ref CNetData poNetData)
{
	LGDT_ACT_REPLY pstOut = (LGDT_ACT_REPLY)msg;
	switch(iSelect){
	case (int)LG_SUCCESS:
		pstOut.m_Succ	=	(LGDT_LOGIN_SUCCESS)DecodeLGDT_LOGIN_SUCCESS(ref poNetData);
		break;
	case (int)LG_FAIL:
		pstOut.m_Fail	=	(LGDT_LOGIN_FAIL)DecodeLGDT_LOGIN_FAIL(ref poNetData);
		break;
	case (int)LG_INTER:
		pstOut.m_Inter	=	(LGDT_LOGIN_INTERMEDIATE)DecodeLGDT_LOGIN_INTERMEDIATE(ref poNetData);
		break;
	}
}

public static	int		EncodeLGPKG_LOGIN_ACK(CMessage msg,ref CNetData poNetData)
{
	LGPKG_LOGIN_ACK pstIn = (LGPKG_LOGIN_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_AckType))
		return -1;

	EncodeUnLGDT_ACT_REPLY(pstIn.m_Reply,(int)pstIn.m_AckType,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGPKG_LOGIN_ACK(ref CNetData poNetData)
{
	LGPKG_LOGIN_ACK pstOut = new LGPKG_LOGIN_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_AckType))
		return null;

	DecodeUnLGDT_ACT_REPLY(pstOut.m_Reply,(int)pstOut.m_AckType,ref poNetData);
	return pstOut;
}

public static	int		EncodeLGPKG_GAMESERVER_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	LGPKG_GAMESERVER_LIST_REQ pstIn = (LGPKG_GAMESERVER_LIST_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Flags))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sig,(int)LG_SIG_LEN))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGPKG_GAMESERVER_LIST_REQ(ref CNetData poNetData)
{
	LGPKG_GAMESERVER_LIST_REQ pstOut = new LGPKG_GAMESERVER_LIST_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Flags))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sig,(int)LG_SIG_LEN))
		return null;

	return pstOut;
}

public static	int		EncodeLGPKG_GAMESERVER_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	LGPKG_GAMESERVER_LIST_ACK pstIn = (LGPKG_GAMESERVER_LIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)LG_MAX_GAMESERVER || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)LG_MAX_GAMESERVER; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		HeaderProto.EncodeSGameServerInfo(pstIn.m_List[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeLGPKG_GAMESERVER_LIST_ACK(ref CNetData poNetData)
{
	LGPKG_GAMESERVER_LIST_ACK pstOut = new LGPKG_GAMESERVER_LIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)LG_MAX_GAMESERVER || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)LG_MAX_GAMESERVER; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_List[i]	= (HeaderProto.SGameServerInfo)HeaderProto.DecodeSGameServerInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeGC_ERROR(CMessage msg,ref CNetData poNetData)
{
	GC_ERROR pstIn = (GC_ERROR)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_ERROR(ref CNetData poNetData)
{
	GC_ERROR pstOut = new GC_ERROR();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	return pstOut;
}

public static	int		EncodeGC_ERRORSTRING(CMessage msg,ref CNetData poNetData)
{
	GC_ERRORSTRING pstIn = (GC_ERRORSTRING)msg;

	if(-1 == poNetData.AddString(pstIn.m_ErrCode))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_ERRORSTRING(ref CNetData poNetData)
{
	GC_ERRORSTRING pstOut = new GC_ERRORSTRING();

	if(-1 == poNetData.DelString(ref pstOut.m_ErrCode))
		return null;

	return pstOut;
}

public static	int		EncodeCG_HEART_BEAT(CMessage msg,ref CNetData poNetData)
{
	CG_HEART_BEAT pstIn = (CG_HEART_BEAT)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_HEART_BEAT(ref CNetData poNetData)
{
	CG_HEART_BEAT pstOut = new CG_HEART_BEAT();

	return pstOut;
}

public static	int		EncodeGC_HEART_BEAT(CMessage msg,ref CNetData poNetData)
{
	GC_HEART_BEAT pstIn = (GC_HEART_BEAT)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_HEART_BEAT(ref CNetData poNetData)
{
	GC_HEART_BEAT pstOut = new GC_HEART_BEAT();

	return pstOut;
}

public static	int		EncodeGC_PUBLIC_STRING(CMessage msg,ref CNetData poNetData)
{
	GC_PUBLIC_STRING pstIn = (GC_PUBLIC_STRING)msg;

	if(-1 == poNetData.AddString(pstIn.m_Info))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_PUBLIC_STRING(ref CNetData poNetData)
{
	GC_PUBLIC_STRING pstOut = new GC_PUBLIC_STRING();

	if(-1 == poNetData.DelString(ref pstOut.m_Info))
		return null;

	return pstOut;
}

public static	int		EncodeCG_LOGIN(CMessage msg,ref CNetData poNetData)
{
	CG_LOGIN pstIn = (CG_LOGIN)msg;

	if(-1 == poNetData.AddString(pstIn.m_PTID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LoginDataLen))
		return -1;

   if ((int)pstIn.m_LoginDataLen > (int)64)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_LoginData,(int)pstIn.m_LoginDataLen))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ProtoVersion))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MainVersion))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SubVersion))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuildVersion))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ResourceVersion))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_LOGIN(ref CNetData poNetData)
{
	CG_LOGIN pstOut = new CG_LOGIN();

	if(-1 == poNetData.DelString(ref pstOut.m_PTID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LoginDataLen))
		return null;

   if ((int)pstOut.m_LoginDataLen > (int)64)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_LoginData,(int)pstOut.m_LoginDataLen))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ProtoVersion))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MainVersion))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SubVersion))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuildVersion))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ResourceVersion))
		return null;

	return pstOut;
}

public static	int		EncodeGC_LOGIN(CMessage msg,ref CNetData poNetData)
{
	GC_LOGIN pstIn = (GC_LOGIN)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ErrMsg))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EncryptType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Len))
		return -1;

   if ((int)pstIn.m_Len > (int)MAX_DYNCODE_LEN)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_DynCode,(int)pstIn.m_Len))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_QueueToken))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LastEnterQueueToken))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_LOGIN(ref CNetData poNetData)
{
	GC_LOGIN pstOut = new GC_LOGIN();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ErrMsg))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EncryptType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Len))
		return null;

   if ((int)pstOut.m_Len > (int)MAX_DYNCODE_LEN)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_DynCode,(int)pstOut.m_Len))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_QueueToken))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LastEnterQueueToken))
		return null;

	return pstOut;
}

public static	int		EncodeCG_LOGOUT(CMessage msg,ref CNetData poNetData)
{
	CG_LOGOUT pstIn = (CG_LOGOUT)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_LOGOUT(ref CNetData poNetData)
{
	CG_LOGOUT pstOut = new CG_LOGOUT();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	return pstOut;
}

public static	int		EncodeGC_LOGOUT(CMessage msg,ref CNetData poNetData)
{
	GC_LOGOUT pstIn = (GC_LOGOUT)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_LOGOUT(ref CNetData poNetData)
{
	GC_LOGOUT pstOut = new GC_LOGOUT();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	return pstOut;
}

public static	int		EncodeGC_QUEUE_INFO(CMessage msg,ref CNetData poNetData)
{
	GC_QUEUE_INFO pstIn = (GC_QUEUE_INFO)msg;

	if(-1 == poNetData.Add(pstIn.m_LastPassToken))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Priority))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_QUEUE_INFO(ref CNetData poNetData)
{
	GC_QUEUE_INFO pstOut = new GC_QUEUE_INFO();

	if(-1 == poNetData.Del(ref pstOut.m_LastPassToken))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Priority))
		return null;

	return pstOut;
}

public static	int		EncodeGC_ROLELIST(CMessage msg,ref CNetData poNetData)
{
	GC_ROLELIST pstIn = (GC_ROLELIST)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LastSelect))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.ACTIVE_ROLE_NUM || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.ACTIVE_ROLE_NUM; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		HeaderProto.EncodeSRoleInfoWithEquip(pstIn.m_RoleInfoList[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_ROLELIST(ref CNetData poNetData)
{
	GC_ROLELIST pstOut = new GC_ROLELIST();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LastSelect))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.ACTIVE_ROLE_NUM || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.ACTIVE_ROLE_NUM; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_RoleInfoList[i]	= (HeaderProto.SRoleInfoWithEquip)HeaderProto.DecodeSRoleInfoWithEquip( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCG_CREATEROLE(CMessage msg,ref CNetData poNetData)
{
	CG_CREATEROLE pstIn = (CG_CREATEROLE)msg;

		HeaderProto.EncodeSRoleInfo(pstIn.m_RoleInfo,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_ServerUniqueID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_CREATEROLE(ref CNetData poNetData)
{
	CG_CREATEROLE pstOut = new CG_CREATEROLE();

	pstOut.m_RoleInfo	= (HeaderProto.SRoleInfo)HeaderProto.DecodeSRoleInfo(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_ServerUniqueID))
		return null;

	return pstOut;
}

public static	int		EncodeGC_CREATEROLE(CMessage msg,ref CNetData poNetData)
{
	GC_CREATEROLE pstIn = (GC_CREATEROLE)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

		HeaderProto.EncodeSRoleInfo(pstIn.m_RoleInfo,ref poNetData);
	if(-1 == poNetData.AddString(pstIn.m_ErrString))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_CREATEROLE(ref CNetData poNetData)
{
	GC_CREATEROLE pstOut = new GC_CREATEROLE();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	pstOut.m_RoleInfo	= (HeaderProto.SRoleInfo)HeaderProto.DecodeSRoleInfo(ref poNetData);
	if(-1 == poNetData.DelString(ref pstOut.m_ErrString))
		return null;

	return pstOut;
}

public static	int		EncodeCG_DELROLE(CMessage msg,ref CNetData poNetData)
{
	CG_DELROLE pstIn = (CG_DELROLE)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_DELROLE(ref CNetData poNetData)
{
	CG_DELROLE pstOut = new CG_DELROLE();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeGC_DELROLE(CMessage msg,ref CNetData poNetData)
{
	GC_DELROLE pstIn = (GC_DELROLE)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_DELROLE(ref CNetData poNetData)
{
	GC_DELROLE pstOut = new GC_DELROLE();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	return pstOut;
}

public static	int		EncodeCG_SELECTROLE(CMessage msg,ref CNetData poNetData)
{
	CG_SELECTROLE pstIn = (CG_SELECTROLE)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_SELECTROLE(ref CNetData poNetData)
{
	CG_SELECTROLE pstOut = new CG_SELECTROLE();

	if(-1 == poNetData.Del(ref pstOut.m_RoleDBID))
		return null;

	return pstOut;
}

public static	int		EncodeGC_SELECTROLE(CMessage msg,ref CNetData poNetData)
{
	GC_SELECTROLE pstIn = (GC_SELECTROLE)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ErrString))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_SELECTROLE(ref CNetData poNetData)
{
	GC_SELECTROLE pstOut = new GC_SELECTROLE();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ErrString))
		return null;

	return pstOut;
}

public static	int		EncodeGC_GPK_CLT_DYN_CODE(CMessage msg,ref CNetData poNetData)
{
	GC_GPK_CLT_DYN_CODE pstIn = (GC_GPK_CLT_DYN_CODE)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

   if ((int)pstIn.m_Count > (int)12288)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_Code,(int)pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_GPK_CLT_DYN_CODE(ref CNetData poNetData)
{
	GC_GPK_CLT_DYN_CODE pstOut = new GC_GPK_CLT_DYN_CODE();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

   if ((int)pstOut.m_Count > (int)12288)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_Code,(int)pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeCG_GPK_CLT_NEGLECT(CMessage msg,ref CNetData poNetData)
{
	CG_GPK_CLT_NEGLECT pstIn = (CG_GPK_CLT_NEGLECT)msg;

	if(-1 == poNetData.Add(pstIn.m_Nothing,(int)196))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_GPK_CLT_NEGLECT(ref CNetData poNetData)
{
	CG_GPK_CLT_NEGLECT pstOut = new CG_GPK_CLT_NEGLECT();

	if(-1 == poNetData.Del(ref pstOut.m_Nothing,(int)196))
		return null;

	return pstOut;
}

public static	int		EncodeGC_GPK_CAL_ORIGIN(CMessage msg,ref CNetData poNetData)
{
	GC_GPK_CAL_ORIGIN pstIn = (GC_GPK_CAL_ORIGIN)msg;

	if(-1 == poNetData.Add(pstIn.m_Original,(int)24))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGC_GPK_CAL_ORIGIN(ref CNetData poNetData)
{
	GC_GPK_CAL_ORIGIN pstOut = new GC_GPK_CAL_ORIGIN();

	if(-1 == poNetData.Del(ref pstOut.m_Original,(int)24))
		return null;

	return pstOut;
}

public static	int		EncodeCG_GPK_CAL_RESULT(CMessage msg,ref CNetData poNetData)
{
	CG_GPK_CAL_RESULT pstIn = (CG_GPK_CAL_RESULT)msg;

	if(-1 == poNetData.Add(pstIn.m_Result,(int)24))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCG_GPK_CAL_RESULT(ref CNetData poNetData)
{
	CG_GPK_CAL_RESULT pstOut = new CG_GPK_CAL_RESULT();

	if(-1 == poNetData.Del(ref pstOut.m_Result,(int)24))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_SERVERINFO_RPT(CMessage msg,ref CNetData poNetData)
{
	GSPKG_SERVERINFO_RPT pstIn = (GSPKG_SERVERINFO_RPT)msg;

	if(-1 == poNetData.Add(pstIn.m_ServerID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_SERVERINFO_RPT(ref CNetData poNetData)
{
	GSPKG_SERVERINFO_RPT pstOut = new GSPKG_SERVERINFO_RPT();

	if(-1 == poNetData.Del(ref pstOut.m_ServerID))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_SERVER_GROUP_DESC_RPT(CMessage msg,ref CNetData poNetData)
{
	GSPKG_SERVER_GROUP_DESC_RPT pstIn = (GSPKG_SERVER_GROUP_DESC_RPT)msg;

	if(-1 == poNetData.AddString(pstIn.m_GroupName))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_SERVER_GROUP_DESC_RPT(ref CNetData poNetData)
{
	GSPKG_SERVER_GROUP_DESC_RPT pstOut = new GSPKG_SERVER_GROUP_DESC_RPT();

	if(-1 == poNetData.DelString(ref pstOut.m_GroupName))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_PLAYER_GPK_RPT(CMessage msg,ref CNetData poNetData)
{
	GSPKG_PLAYER_GPK_RPT pstIn = (GSPKG_PLAYER_GPK_RPT)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_PLAYER_GPK_RPT(ref CNetData poNetData)
{
	GSPKG_PLAYER_GPK_RPT pstOut = new GSPKG_PLAYER_GPK_RPT();

	return pstOut;
}

public static	int		EncodeGSPKG_ENTERGAME_REQ(CMessage msg,ref CNetData poNetData)
{
	GSPKG_ENTERGAME_REQ pstIn = (GSPKG_ENTERGAME_REQ)msg;

	if(-1 == poNetData.AddString(pstIn.m_PTID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DigitID))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_AccountName))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Flags))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GmLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SessionID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EnterType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GateSvrID))
		return -1;

		HeaderProto.EncodeSLMAccountInfo(pstIn.m_AccountInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_ENTERGAME_REQ(ref CNetData poNetData)
{
	GSPKG_ENTERGAME_REQ pstOut = new GSPKG_ENTERGAME_REQ();

	if(-1 == poNetData.DelString(ref pstOut.m_PTID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DigitID))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_AccountName))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Flags))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GmLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SessionID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EnterType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GateSvrID))
		return null;

	pstOut.m_AccountInfo	= (HeaderProto.SLMAccountInfo)HeaderProto.DecodeSLMAccountInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeGSPKG_ENTERGAME_ACK(CMessage msg,ref CNetData poNetData)
{
	GSPKG_ENTERGAME_ACK pstIn = (GSPKG_ENTERGAME_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Flags))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_ENTERGAME_ACK(ref CNetData poNetData)
{
	GSPKG_ENTERGAME_ACK pstOut = new GSPKG_ENTERGAME_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Flags))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_LEAVEGAME_REQ(CMessage msg,ref CNetData poNetData)
{
	GSPKG_LEAVEGAME_REQ pstIn = (GSPKG_LEAVEGAME_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_LEAVEGAME_REQ(ref CNetData poNetData)
{
	GSPKG_LEAVEGAME_REQ pstOut = new GSPKG_LEAVEGAME_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_LEAVEGAME_ACK(CMessage msg,ref CNetData poNetData)
{
	GSPKG_LEAVEGAME_ACK pstIn = (GSPKG_LEAVEGAME_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_LEAVEGAME_ACK(ref CNetData poNetData)
{
	GSPKG_LEAVEGAME_ACK pstOut = new GSPKG_LEAVEGAME_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_SWITCHSCENE_REQ(CMessage msg,ref CNetData poNetData)
{
	GSPKG_SWITCHSCENE_REQ pstIn = (GSPKG_SWITCHSCENE_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_SceneTemplateID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_SWITCHSCENE_REQ(ref CNetData poNetData)
{
	GSPKG_SWITCHSCENE_REQ pstOut = new GSPKG_SWITCHSCENE_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_SceneTemplateID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneID))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_SWITCHSCENE_ACK(CMessage msg,ref CNetData poNetData)
{
	GSPKG_SWITCHSCENE_ACK pstIn = (GSPKG_SWITCHSCENE_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneTemplateID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_SWITCHSCENE_ACK(ref CNetData poNetData)
{
	GSPKG_SWITCHSCENE_ACK pstOut = new GSPKG_SWITCHSCENE_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneTemplateID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneID))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_KICKOUT_NTF(CMessage msg,ref CNetData poNetData)
{
	GSPKG_KICKOUT_NTF pstIn = (GSPKG_KICKOUT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ErrCode))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_ErrString))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_KICKOUT_NTF(ref CNetData poNetData)
{
	GSPKG_KICKOUT_NTF pstOut = new GSPKG_KICKOUT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ErrCode))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_ErrString))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_MSG_BROADCAST(CMessage msg,ref CNetData poNetData)
{
	GSPKG_MSG_BROADCAST pstIn = (GSPKG_MSG_BROADCAST)msg;

	if(-1 == poNetData.Add(pstIn.m_IDCount))
		return -1;

	int i;
	if((int)pstIn.m_IDCount > HeaderProto.TS_BROADCAST_ID_COUNT || pstIn.m_IDCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.TS_BROADCAST_ID_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_IDCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_ID[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_MsgBufLen))
		return -1;

   if ((int)pstIn.m_MsgBufLen > (int)HeaderProto.TS_BROADCAST_MSG_SIZE)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_MsgBuf,(int)pstIn.m_MsgBufLen))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_MSG_BROADCAST(ref CNetData poNetData)
{
	GSPKG_MSG_BROADCAST pstOut = new GSPKG_MSG_BROADCAST();

	if(-1 == poNetData.Del(ref pstOut.m_IDCount))
		return null;

	int i;
	if ((int)pstOut.m_IDCount > (int)HeaderProto.TS_BROADCAST_ID_COUNT || (int)pstOut.m_IDCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.TS_BROADCAST_ID_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_IDCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_ID[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_MsgBufLen))
		return null;

   if ((int)pstOut.m_MsgBufLen > (int)HeaderProto.TS_BROADCAST_MSG_SIZE)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_MsgBuf,(int)pstOut.m_MsgBufLen))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_LOAD_GSDATA_NTF(CMessage msg,ref CNetData poNetData)
{
	GSPKG_LOAD_GSDATA_NTF pstIn = (GSPKG_LOAD_GSDATA_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

   if ((int)pstIn.m_Count > (int)HeaderProto.MAX_XMLDATA_COUNT)
		return -1;
	if(-1 == poNetData.Add(pstIn.m_XmlData,(int)pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_LOAD_GSDATA_NTF(ref CNetData poNetData)
{
	GSPKG_LOAD_GSDATA_NTF pstOut = new GSPKG_LOAD_GSDATA_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

   if ((int)pstOut.m_Count > (int)HeaderProto.MAX_XMLDATA_COUNT)
		return null;
	if(-1 == poNetData.Del(ref pstOut.m_XmlData,(int)pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeGSPKG_UPDATEGMLEVEL_NTF(CMessage msg,ref CNetData poNetData)
{
	GSPKG_UPDATEGMLEVEL_NTF pstIn = (GSPKG_UPDATEGMLEVEL_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_DigitID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GMLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeGSPKG_UPDATEGMLEVEL_NTF(ref CNetData poNetData)
{
	GSPKG_UPDATEGMLEVEL_NTF pstOut = new GSPKG_UPDATEGMLEVEL_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_DigitID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GMLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ERROR_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ERROR_NTF pstIn = (SC_ERROR_NTF)msg;

		HeaderProto.EncodeSError(pstIn.m_Data,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ERROR_NTF(ref CNetData poNetData)
{
	SC_ERROR_NTF pstOut = new SC_ERROR_NTF();

	pstOut.m_Data	= (HeaderProto.SError)HeaderProto.DecodeSError(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_LEVEL_INFO(CMessage msg,ref CNetData poNetData)
{
	SC_LEVEL_INFO pstIn = (SC_LEVEL_INFO)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_LEVEL_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		HeaderProto.EncodeSLevelInfo(pstIn.m_Info[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_BattleCount))
		return -1;

	if ((int)pstIn.m_BattleCount > (int)HeaderProto.MAX_BATTLE_COUNT || (int)pstIn.m_BattleCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_BATTLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_BattleCount)
			break;
		HeaderProto.EncodeSBattleInfo(pstIn.m_BtInfo[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_BoxCount))
		return -1;

	if((int)pstIn.m_BoxCount > HeaderProto.MAX_BATTLE_BOX_COUNT || pstIn.m_BoxCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_BATTLE_BOX_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_BoxCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_GotBattleBox[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_LEVEL_INFO(ref CNetData poNetData)
{
	SC_LEVEL_INFO pstOut = new SC_LEVEL_INFO();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_LEVEL_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Info[i]	= (HeaderProto.SLevelInfo)HeaderProto.DecodeSLevelInfo( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_BattleCount))
		return null;

	if ((int)pstOut.m_BattleCount > (int)HeaderProto.MAX_BATTLE_COUNT || (int)pstOut.m_BattleCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_BATTLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_BattleCount)
			break;
		pstOut.m_BtInfo[i]	= (HeaderProto.SBattleInfo)HeaderProto.DecodeSBattleInfo( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_BoxCount))
		return null;

	if ((int)pstOut.m_BoxCount > (int)HeaderProto.MAX_BATTLE_BOX_COUNT || (int)pstOut.m_BoxCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_BATTLE_BOX_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_BoxCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_GotBattleBox[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_LEVEL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_LEVEL_REQ pstIn = (CS_LEVEL_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_LevelBattleType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_LevelID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuffID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyssDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FriendPetID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_QuickEnd))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_LEVEL_REQ(ref CNetData poNetData)
{
	CS_LEVEL_REQ pstOut = new CS_LEVEL_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_LevelBattleType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_LevelID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuffID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyssDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FriendPetID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_QuickEnd))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_BATTLE_BOX_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_BATTLE_BOX_REQ pstIn = (CS_GET_BATTLE_BOX_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BattleboxID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_BATTLE_BOX_REQ(ref CNetData poNetData)
{
	CS_GET_BATTLE_BOX_REQ pstOut = new CS_GET_BATTLE_BOX_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BattleboxID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GET_BATTLE_BOX_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_BATTLE_BOX_ACK pstIn = (SC_GET_BATTLE_BOX_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_BattleboxID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_BATTLE_BOX_ACK(ref CNetData poNetData)
{
	SC_GET_BATTLE_BOX_ACK pstOut = new SC_GET_BATTLE_BOX_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_BattleboxID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSC_BATTLE_GOT_STAR_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_BATTLE_GOT_STAR_NTF pstIn = (SC_BATTLE_GOT_STAR_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_BattleID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GotStar))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_BATTLE_GOT_STAR_NTF(ref CNetData poNetData)
{
	SC_BATTLE_GOT_STAR_NTF pstOut = new SC_BATTLE_GOT_STAR_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_BattleID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GotStar))
		return null;

	return pstOut;
}

public static	int		EncodeSDropInfo(CMessage msg,ref CNetData poNetData)
{
	SDropInfo pstIn = (SDropInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSDropInfo(ref CNetData poNetData)
{
	SDropInfo pstOut = new SDropInfo();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeSDropInfoList(CMessage msg,ref CNetData poNetData)
{
	SDropInfoList pstIn = (SDropInfoList)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_ITEM_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSDropInfo(pstIn.m_List[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSDropInfoList(ref CNetData poNetData)
{
	SDropInfoList pstOut = new SDropInfoList();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_ITEM_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_List[i]	= (SDropInfo)DecodeSDropInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSDropInfos(CMessage msg,ref CNetData poNetData)
{
	SDropInfos pstIn = (SDropInfos)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_MONSTER_DROP || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_MONSTER_DROP; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSDropInfo(pstIn.m_List[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSDropInfos(ref CNetData poNetData)
{
	SDropInfos pstOut = new SDropInfos();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_MONSTER_DROP || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_MONSTER_DROP; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_List[i]	= (SDropInfo)DecodeSDropInfo( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	return pstOut;
}

public static	int		EncodeSMonsterInfo(CMessage msg,ref CNetData poNetData)
{
	SMonsterInfo pstIn = (SMonsterInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_Index))
		return -1;

		EncodeSDropInfos(pstIn.m_Drop,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMonsterInfo(ref CNetData poNetData)
{
	SMonsterInfo pstOut = new SMonsterInfo();

	if(-1 == poNetData.Del(ref pstOut.m_Index))
		return null;

	pstOut.m_Drop	= (SDropInfos)DecodeSDropInfos(ref poNetData);
	return pstOut;
}

public static	int		EncodeSMonsterInfos(CMessage msg,ref CNetData poNetData)
{
	SMonsterInfos pstIn = (SMonsterInfos)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_MONSTER_IN_LEVEL || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_MONSTER_IN_LEVEL; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSMonsterInfo(pstIn.m_List[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMonsterInfos(ref CNetData poNetData)
{
	SMonsterInfos pstOut = new SMonsterInfos();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_MONSTER_IN_LEVEL || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_MONSTER_IN_LEVEL; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_List[i]	= (SMonsterInfo)DecodeSMonsterInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_LEVEL_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_LEVEL_ACK pstIn = (SC_LEVEL_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_LevelBattleType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AbyssLevelID))
		return -1;

		EncodeSMonsterInfos(pstIn.m_Monster,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_FreeReliveCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RelivePrice))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_InitialBuffCount))
		return -1;

	int i;
	for(i = 0; i < (int)MAX_INITIAL_BUFF_COUNT; ++i)
	{
		if(-1 == poNetData.Add(pstIn.m_InitialBuffID[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_QuickEnd))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_LEVEL_ACK(ref CNetData poNetData)
{
	SC_LEVEL_ACK pstOut = new SC_LEVEL_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_LevelBattleType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AbyssLevelID))
		return null;

	pstOut.m_Monster	= (SMonsterInfos)DecodeSMonsterInfos(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_FreeReliveCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RelivePrice))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_InitialBuffCount))
		return null;

	int i;
	for(i = 0; i < (int)MAX_INITIAL_BUFF_COUNT; ++i)
	{
		if(-1 == poNetData.Del(ref pstOut.m_InitialBuffID[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_QuickEnd))
		return null;

	return pstOut;
}

public static	int		EncodeSSkillCount(CMessage msg,ref CNetData poNetData)
{
	SSkillCount pstIn = (SSkillCount)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSSkillCount(ref CNetData poNetData)
{
	SSkillCount pstOut = new SSkillCount();

	if(-1 == poNetData.Del(ref pstOut.m_SkillID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeCS_LEVEL_RESULT_NTF(CMessage msg,ref CNetData poNetData)
{
	CS_LEVEL_RESULT_NTF pstIn = (CS_LEVEL_RESULT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_LevelBattleType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if ((int)pstIn.m_ItemCount > (int)MAX_LEVEL_DROP || (int)pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_LEVEL_DROP; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		EncodeSDropInfo(pstIn.m_Item[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_ReliveCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_KillCount))
		return -1;

	if((int)pstIn.m_KillCount > MAX_MONSTER_IN_LEVEL || pstIn.m_KillCount < 0)
       return -1;
	for(i = 0; i < (int)MAX_MONSTER_IN_LEVEL; ++i)
	{
		if(i >= (int)pstIn.m_KillCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_Kills[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_MaxCombo))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Critical))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SkillCount))
		return -1;

	if ((int)pstIn.m_SkillCount > (int)100 || (int)pstIn.m_SkillCount < 0)
       return -1;
	for(i = 0; i < (int)100; ++i)
	{
		if(i >= (int)pstIn.m_SkillCount)
			break;
		EncodeSSkillCount(pstIn.m_Skills[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_CompleteResult))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ActivityAbyssTotalDamage))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CompleteSec))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PetChangeTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_LEVEL_RESULT_NTF(ref CNetData poNetData)
{
	CS_LEVEL_RESULT_NTF pstOut = new CS_LEVEL_RESULT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_LevelBattleType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)MAX_LEVEL_DROP || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)MAX_LEVEL_DROP; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		pstOut.m_Item[i]	= (SDropInfo)DecodeSDropInfo( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_ReliveCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_KillCount))
		return null;

	if ((int)pstOut.m_KillCount > (int)MAX_MONSTER_IN_LEVEL || (int)pstOut.m_KillCount < 0)
       return null;
	for(i = 0; i < (int)MAX_MONSTER_IN_LEVEL; ++i)
	{
		if(i >= (int)pstOut.m_KillCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_Kills[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_MaxCombo))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Critical))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SkillCount))
		return null;

	if ((int)pstOut.m_SkillCount > (int)100 || (int)pstOut.m_SkillCount < 0)
       return null;
	for(i = 0; i < (int)100; ++i)
	{
		if(i >= (int)pstOut.m_SkillCount)
			break;
		pstOut.m_Skills[i]	= (SSkillCount)DecodeSSkillCount( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_CompleteResult))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ActivityAbyssTotalDamage))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CompleteSec))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PetChangeTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSLevelSelectCardInfo(CMessage msg,ref CNetData poNetData)
{
	SLevelSelectCardInfo pstIn = (SLevelSelectCardInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_IndexID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSLevelSelectCardInfo(ref CNetData poNetData)
{
	SLevelSelectCardInfo pstOut = new SLevelSelectCardInfo();

	if(-1 == poNetData.Del(ref pstOut.m_IndexID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeSC_TREASURE_CHEST_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_TREASURE_CHEST_NTF pstIn = (SC_TREASURE_CHEST_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_LevelID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CurGetStar))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_QuickEnd))
		return -1;

		HeaderProto.EncodeSXITEMS(pstIn.m_Items,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Money))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Experience))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PetAboutCount))
		return -1;

	int i;
	if((int)pstIn.m_PetAboutCount > HeaderProto.MAX_PET_COUNT || pstIn.m_PetAboutCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_PetAboutCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_PetAbout[i]))
			return -1;
	}

	if(-1 == poNetData.Add(pstIn.m_GetCount))
		return -1;

	if ((int)pstIn.m_GetCount > (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT || (int)pstIn.m_GetCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_GetCount)
			break;
		EncodeSLevelSelectCardInfo(pstIn.m_CardInfos[i],ref poNetData);
	}

	if(-1 == poNetData.Add(pstIn.m_TempCount))
		return -1;

	if ((int)pstIn.m_TempCount > (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT || (int)pstIn.m_TempCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_TempCount)
			break;
		EncodeSLevelSelectCardInfo(pstIn.m_TempCardInfos[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_TREASURE_CHEST_NTF(ref CNetData poNetData)
{
	SC_TREASURE_CHEST_NTF pstOut = new SC_TREASURE_CHEST_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_LevelID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CurGetStar))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_QuickEnd))
		return null;

	pstOut.m_Items	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Money))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Experience))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PetAboutCount))
		return null;

	int i;
	if ((int)pstOut.m_PetAboutCount > (int)HeaderProto.MAX_PET_COUNT || (int)pstOut.m_PetAboutCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_PetAboutCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_PetAbout[i]))
			return null;
	}

	if(-1 == poNetData.Del(ref pstOut.m_GetCount))
		return null;

	if ((int)pstOut.m_GetCount > (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT || (int)pstOut.m_GetCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_GetCount)
			break;
		pstOut.m_CardInfos[i]	= (SLevelSelectCardInfo)DecodeSLevelSelectCardInfo( ref poNetData);
	}

	if(-1 == poNetData.Del(ref pstOut.m_TempCount))
		return null;

	if ((int)pstOut.m_TempCount > (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT || (int)pstOut.m_TempCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_LEVEL_SELECT_CARD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_TempCount)
			break;
		pstOut.m_TempCardInfos[i]	= (SLevelSelectCardInfo)DecodeSLevelSelectCardInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_OPEN_TREASURE_CHEST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_OPEN_TREASURE_CHEST_REQ pstIn = (CS_OPEN_TREASURE_CHEST_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemIndexID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_OPEN_TREASURE_CHEST_REQ(ref CNetData poNetData)
{
	CS_OPEN_TREASURE_CHEST_REQ pstOut = new CS_OPEN_TREASURE_CHEST_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_ItemIndexID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_OPEN_TREASURE_CHEST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_OPEN_TREASURE_CHEST_ACK pstIn = (SC_OPEN_TREASURE_CHEST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemIndexID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_OPEN_TREASURE_CHEST_ACK(ref CNetData poNetData)
{
	SC_OPEN_TREASURE_CHEST_ACK pstOut = new SC_OPEN_TREASURE_CHEST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_ItemIndexID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SELF_LOADSCENE(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_LOADSCENE pstIn = (SC_SELF_LOADSCENE)msg;

	if(-1 == poNetData.Add(pstIn.m_SceneId))
		return -1;

		HeaderProto.EncodeSPosInfo(pstIn.m_EnterPos,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneType))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_LOADSCENE(ref CNetData poNetData)
{
	SC_SELF_LOADSCENE pstOut = new SC_SELF_LOADSCENE();

	if(-1 == poNetData.Del(ref pstOut.m_SceneId))
		return null;

	pstOut.m_EnterPos	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneType))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ENTERSCENE(CMessage msg,ref CNetData poNetData)
{
	CS_ENTERSCENE pstIn = (CS_ENTERSCENE)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SceneId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ENTERSCENE(ref CNetData poNetData)
{
	CS_ENTERSCENE pstOut = new CS_ENTERSCENE();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SceneId))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SELF_ENTERSCENE(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_ENTERSCENE pstIn = (SC_SELF_ENTERSCENE)msg;

	if(-1 == poNetData.Add(pstIn.m_Flag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ServerTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_ENTERSCENE(ref CNetData poNetData)
{
	SC_SELF_ENTERSCENE pstOut = new SC_SELF_ENTERSCENE();

	if(-1 == poNetData.Del(ref pstOut.m_Flag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ServerTime))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SELF_DATA_END(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_DATA_END pstIn = (SC_SELF_DATA_END)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_DATA_END(ref CNetData poNetData)
{
	SC_SELF_DATA_END pstOut = new SC_SELF_DATA_END();

	return pstOut;
}

public static	int		EncodeSC_SELF_APPEAR(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_APPEAR pstIn = (SC_SELF_APPEAR)msg;

		HeaderProto.EncodeSPosInfo(pstIn.m_EnterPos,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_APPEAR(ref CNetData poNetData)
{
	SC_SELF_APPEAR pstOut = new SC_SELF_APPEAR();

	pstOut.m_EnterPos	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_SELF_VOLATILE_PRO(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_VOLATILE_PRO pstIn = (SC_SELF_VOLATILE_PRO)msg;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonMoney))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonCash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Experience))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AP))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_VOLATILE_PRO(ref CNetData poNetData)
{
	SC_SELF_VOLATILE_PRO pstOut = new SC_SELF_VOLATILE_PRO();

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonMoney))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonCash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Experience))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AP))
		return null;

	return pstOut;
}

public static	int		EncodeSC_CLIENT_CONFIG_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_CLIENT_CONFIG_NTF pstIn = (SC_CLIENT_CONFIG_NTF)msg;

	if(-1 == poNetData.AddString(pstIn.m_ConfigInfo))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_CLIENT_CONFIG_NTF(ref CNetData poNetData)
{
	SC_CLIENT_CONFIG_NTF pstOut = new SC_CLIENT_CONFIG_NTF();

	if(-1 == poNetData.DelString(ref pstOut.m_ConfigInfo))
		return null;

	return pstOut;
}

public static	int		EncodeCS_CLIENT_CONFIG_UPDATE(CMessage msg,ref CNetData poNetData)
{
	CS_CLIENT_CONFIG_UPDATE pstIn = (CS_CLIENT_CONFIG_UPDATE)msg;

	if(-1 == poNetData.AddString(pstIn.m_ConfigInfo))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_CLIENT_CONFIG_UPDATE(ref CNetData poNetData)
{
	CS_CLIENT_CONFIG_UPDATE pstOut = new CS_CLIENT_CONFIG_UPDATE();

	if(-1 == poNetData.DelString(ref pstOut.m_ConfigInfo))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SELF_BASE_PRO(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_BASE_PRO pstIn = (SC_SELF_BASE_PRO)msg;

	if(-1 == poNetData.Add(pstIn.m_ObjID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RoleIndex))
		return -1;

	if(-1 == poNetData.AddString(pstIn.m_Name))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BaseJob))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Job))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Experience))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonMoney))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_NonCash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cash))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MoveSpeed))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSpeed))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Str))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Int))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Dex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sta))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Fai))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxHP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxSP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HPTick))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SPTick))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AtkDmgMin))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AtkDmgMax))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Def))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IceAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FireAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PoisonAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ThunderAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IceDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FireDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PoisonDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ThunderDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Pierce))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Hit))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Dodge))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cri))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Flex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriDmg))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriDmgDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BodySize))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSize))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSpeedModPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MoveSpeedModPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PiercePer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HitPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DodgePer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FlexPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxEP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxAP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ExpPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MoneyPer))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_BASE_PRO(ref CNetData poNetData)
{
	SC_SELF_BASE_PRO pstOut = new SC_SELF_BASE_PRO();

	if(-1 == poNetData.Del(ref pstOut.m_ObjID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RoleIndex))
		return null;

	if(-1 == poNetData.DelString(ref pstOut.m_Name))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BaseJob))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Job))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Experience))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonMoney))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_NonCash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cash))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MoveSpeed))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSpeed))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Str))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Int))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Dex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sta))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Fai))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxHP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxSP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HPTick))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SPTick))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AtkDmgMin))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AtkDmgMax))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Def))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IceAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FireAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PoisonAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ThunderAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IceDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FireDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PoisonDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ThunderDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Pierce))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Hit))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Dodge))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cri))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Flex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriDmg))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriDmgDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BodySize))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSize))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSpeedModPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MoveSpeedModPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PiercePer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HitPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DodgePer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FlexPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxEP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxAP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ExpPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MoneyPer))
		return null;

	return pstOut;
}

public static	int		EncodeSPetInfo(CMessage msg,ref CNetData poNetData)
{
	SPetInfo pstIn = (SPetInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BattlePos))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TemplateID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Experience))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Up))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Lock))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Str))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Int))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Dex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Sta))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Fai))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxHP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxSP))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HPTick))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SPTick))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AtkDmgMin))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AtkDmgMax))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Def))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IceAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FireAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PoisonAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ThunderAtt))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IceDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FireDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PoisonDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ThunderDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Pierce))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Hit))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Dodge))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Cri))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Flex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriDmg))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriDmgDef))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BodySize))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSize))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSpeedModPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MoveSpeedModPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PiercePer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_HitPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DodgePer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CriPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FlexPer))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AttSpeed))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MoveSpeed))
		return -1;

		HeaderProto.EncodeSXITEMS(pstIn.m_Equip,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSPetInfo(ref CNetData poNetData)
{
	SPetInfo pstOut = new SPetInfo();

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BattlePos))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TemplateID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Experience))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Up))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Lock))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Str))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Int))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Dex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Sta))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Fai))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxHP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxSP))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HPTick))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SPTick))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AtkDmgMin))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AtkDmgMax))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Def))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IceAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FireAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PoisonAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ThunderAtt))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IceDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FireDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PoisonDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ThunderDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Pierce))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Hit))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Dodge))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Cri))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Flex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriDmg))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriDmgDef))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BodySize))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSize))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSpeedModPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MoveSpeedModPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PiercePer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_HitPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DodgePer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CriPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FlexPer))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AttSpeed))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MoveSpeed))
		return null;

	pstOut.m_Equip	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodePET_RECORD(CMessage msg,ref CNetData poNetData)
{
	PET_RECORD pstIn = (PET_RECORD)msg;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodePET_RECORD(ref CNetData poNetData)
{
	PET_RECORD pstOut = new PET_RECORD();

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PETS_RECORD_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PETS_RECORD_NTF pstIn = (SC_PETS_RECORD_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_PetRecordCount))
		return -1;

	int i;
	if ((int)pstIn.m_PetRecordCount > (int)HeaderProto.MAX_PET_RECORD_COUNT || (int)pstIn.m_PetRecordCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_PET_RECORD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_PetRecordCount)
			break;
		EncodePET_RECORD(pstIn.m_Record[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PETS_RECORD_NTF(ref CNetData poNetData)
{
	SC_PETS_RECORD_NTF pstOut = new SC_PETS_RECORD_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_PetRecordCount))
		return null;

	int i;
	if ((int)pstOut.m_PetRecordCount > (int)HeaderProto.MAX_PET_RECORD_COUNT || (int)pstOut.m_PetRecordCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_PET_RECORD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_PetRecordCount)
			break;
		pstOut.m_Record[i]	= (PET_RECORD)DecodePET_RECORD( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_USER_PETS_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_USER_PETS_NTF pstIn = (SC_USER_PETS_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_MaxPetCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PetCount))
		return -1;

	int i;
	if ((int)pstIn.m_PetCount > (int)HeaderProto.MAX_PET_COUNT || (int)pstIn.m_PetCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_PetCount)
			break;
		EncodeSPetInfo(pstIn.m_PetsInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_USER_PETS_NTF(ref CNetData poNetData)
{
	SC_USER_PETS_NTF pstOut = new SC_USER_PETS_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_MaxPetCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PetCount))
		return null;

	int i;
	if ((int)pstOut.m_PetCount > (int)HeaderProto.MAX_PET_COUNT || (int)pstOut.m_PetCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_PetCount)
			break;
		pstOut.m_PetsInfo[i]	= (SPetInfo)DecodeSPetInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_USER_JOIN_PETS(CMessage msg,ref CNetData poNetData)
{
	SC_USER_JOIN_PETS pstIn = (SC_USER_JOIN_PETS)msg;

	if(-1 == poNetData.Add(pstIn.m_PetCount))
		return -1;

	int i;
	if ((int)pstIn.m_PetCount > (int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM || (int)pstIn.m_PetCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM; ++i)
	{
		if(i >= (int)pstIn.m_PetCount)
			break;
		EncodeSPetInfo(pstIn.m_PetsInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_USER_JOIN_PETS(ref CNetData poNetData)
{
	SC_USER_JOIN_PETS pstOut = new SC_USER_JOIN_PETS();

	if(-1 == poNetData.Del(ref pstOut.m_PetCount))
		return null;

	int i;
	if ((int)pstOut.m_PetCount > (int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM || (int)pstOut.m_PetCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.EPetBattlePos.PET_BTPOS_TEAM; ++i)
	{
		if(i >= (int)pstOut.m_PetCount)
			break;
		pstOut.m_PetsInfo[i]	= (SPetInfo)DecodeSPetInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_PET_INFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_INFO_NTF pstIn = (SC_PET_INFO_NTF)msg;

		EncodeSPetInfo(pstIn.m_PetInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_INFO_NTF(ref CNetData poNetData)
{
	SC_PET_INFO_NTF pstOut = new SC_PET_INFO_NTF();

	pstOut.m_PetInfo	= (SPetInfo)DecodeSPetInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_PET_SINGLE_ENTER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_SINGLE_ENTER_NTF pstIn = (SC_PET_SINGLE_ENTER_NTF)msg;

		EncodeSPetInfo(pstIn.m_PetInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_SINGLE_ENTER_NTF(ref CNetData poNetData)
{
	SC_PET_SINGLE_ENTER_NTF pstOut = new SC_PET_SINGLE_ENTER_NTF();

	pstOut.m_PetInfo	= (SPetInfo)DecodeSPetInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_PET_SINGLE_LEAVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_SINGLE_LEAVE_NTF pstIn = (SC_PET_SINGLE_LEAVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_SINGLE_LEAVE_NTF(ref CNetData poNetData)
{
	SC_PET_SINGLE_LEAVE_NTF pstOut = new SC_PET_SINGLE_LEAVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GATHER_ITEM(CMessage msg,ref CNetData poNetData)
{
	SC_GATHER_ITEM pstIn = (SC_GATHER_ITEM)msg;

	if(-1 == poNetData.Add(pstIn.m_ID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CT))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GATHER_ITEM(ref CNetData poNetData)
{
	SC_GATHER_ITEM pstOut = new SC_GATHER_ITEM();

	if(-1 == poNetData.Del(ref pstOut.m_ID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CT))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GATHER_INFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GATHER_INFO_NTF pstIn = (SC_GATHER_INFO_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)MAX_GATHER_ITEM_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)MAX_GATHER_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSC_GATHER_ITEM(pstIn.m_Gathers[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GATHER_INFO_NTF(ref CNetData poNetData)
{
	SC_GATHER_INFO_NTF pstOut = new SC_GATHER_INFO_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)MAX_GATHER_ITEM_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)MAX_GATHER_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Gathers[i]	= (SC_GATHER_ITEM)DecodeSC_GATHER_ITEM( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_GATHER_SINGLE_ENTER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GATHER_SINGLE_ENTER_NTF pstIn = (SC_GATHER_SINGLE_ENTER_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CT))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GATHER_SINGLE_ENTER_NTF(ref CNetData poNetData)
{
	SC_GATHER_SINGLE_ENTER_NTF pstOut = new SC_GATHER_SINGLE_ENTER_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CT))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GATHER_SINGLE_LEAVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GATHER_SINGLE_LEAVE_NTF pstIn = (SC_GATHER_SINGLE_LEAVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GATHER_SINGLE_LEAVE_NTF(ref CNetData poNetData)
{
	SC_GATHER_SINGLE_LEAVE_NTF pstOut = new SC_GATHER_SINGLE_LEAVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GATHER_ITEM_MERGE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GATHER_ITEM_MERGE_REQ pstIn = (CS_GATHER_ITEM_MERGE_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GATHER_ITEM_MERGE_REQ(ref CNetData poNetData)
{
	CS_GATHER_ITEM_MERGE_REQ pstOut = new CS_GATHER_ITEM_MERGE_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GATHER_ITEM_MERGE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GATHER_ITEM_MERGE_ACK pstIn = (SC_GATHER_ITEM_MERGE_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_TID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GATHER_ITEM_MERGE_ACK(ref CNetData poNetData)
{
	SC_GATHER_ITEM_MERGE_ACK pstOut = new SC_GATHER_ITEM_MERGE_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_TID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_PROD_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_PROD_REQ pstIn = (CS_ROLE_PROD_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_PROD_ITEM_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_PROD_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_OtherDBID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_PROD_REQ(ref CNetData poNetData)
{
	CS_ROLE_PROD_REQ pstOut = new CS_ROLE_PROD_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_PROD_ITEM_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_PROD_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_OtherDBID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeSC_ROLE_PROD_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_PROD_ACK pstIn = (SC_ROLE_PROD_ACK)msg;

		HeaderProto.EncodeSXITEMS(pstIn.m_Items,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_PROD_ACK(ref CNetData poNetData)
{
	SC_ROLE_PROD_ACK pstOut = new SC_ROLE_PROD_ACK();

	pstOut.m_Items	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_PET_GET_EXP_RPT(CMessage msg,ref CNetData poNetData)
{
	CS_PET_GET_EXP_RPT pstIn = (CS_PET_GET_EXP_RPT)msg;

	if(-1 == poNetData.Add(pstIn.m_OwnerDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OtherCount))
		return -1;

	int i;
	if((int)pstIn.m_OtherCount > HeaderProto.MAX_PET_COUNT || pstIn.m_OtherCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_OtherCount)
			break;
		if(-1 == poNetData.Add(pstIn.m_OtherDBID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_GET_EXP_RPT(ref CNetData poNetData)
{
	CS_PET_GET_EXP_RPT pstOut = new CS_PET_GET_EXP_RPT();

	if(-1 == poNetData.Del(ref pstOut.m_OwnerDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OtherCount))
		return null;

	int i;
	if ((int)pstOut.m_OtherCount > (int)HeaderProto.MAX_PET_COUNT || (int)pstOut.m_OtherCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_PET_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_OtherCount)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_OtherDBID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeSC_PET_GET_EXP_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PET_GET_EXP_ACK pstIn = (SC_PET_GET_EXP_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_OwnerDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_GET_EXP_ACK(ref CNetData poNetData)
{
	SC_PET_GET_EXP_ACK pstOut = new SC_PET_GET_EXP_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_OwnerDBID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_LEVEL_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_LEVEL_NTF pstIn = (SC_PET_LEVEL_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_LEVEL_NTF(ref CNetData poNetData)
{
	SC_PET_LEVEL_NTF pstOut = new SC_PET_LEVEL_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	return pstOut;
}

public static	int		EncodeCS_PET_UP_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PET_UP_REQ pstIn = (CS_PET_UP_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_OwnerDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OtherDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_UP_REQ(ref CNetData poNetData)
{
	CS_PET_UP_REQ pstOut = new CS_PET_UP_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_OwnerDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OtherDBID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_UP_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PET_UP_ACK pstIn = (SC_PET_UP_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Up))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Ok))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_UP_ACK(ref CNetData poNetData)
{
	SC_PET_UP_ACK pstOut = new SC_PET_UP_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Up))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Ok))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SET_BATTLE_PET_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SET_BATTLE_PET_REQ pstIn = (CS_SET_BATTLE_PET_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BattlePos))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Ok))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SET_BATTLE_PET_REQ(ref CNetData poNetData)
{
	CS_SET_BATTLE_PET_REQ pstOut = new CS_SET_BATTLE_PET_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BattlePos))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Ok))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SET_BATTLE_PET_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SET_BATTLE_PET_NTF pstIn = (SC_SET_BATTLE_PET_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_BattlePos))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SET_BATTLE_PET_NTF(ref CNetData poNetData)
{
	SC_SET_BATTLE_PET_NTF pstOut = new SC_SET_BATTLE_PET_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_BattlePos))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DBID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_PET_MERGE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PET_MERGE_REQ pstIn = (CS_PET_MERGE_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_OwnerDBID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OtherDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_MERGE_REQ(ref CNetData poNetData)
{
	CS_PET_MERGE_REQ pstOut = new CS_PET_MERGE_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_OwnerDBID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OtherDBID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_ADD_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_ADD_NTF pstIn = (SC_PET_ADD_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_TemplateID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_ADD_NTF(ref CNetData poNetData)
{
	SC_PET_ADD_NTF pstOut = new SC_PET_ADD_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_TemplateID))
		return null;

	return pstOut;
}

public static	int		EncodeSOtherInfo(CMessage msg,ref CNetData poNetData)
{
	SOtherInfo pstIn = (SOtherInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_ObjID))
		return -1;

		HeaderProto.EncodeSRoleInfoWithEquip(pstIn.m_RoleInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSOtherInfo(ref CNetData poNetData)
{
	SOtherInfo pstOut = new SOtherInfo();

	if(-1 == poNetData.Del(ref pstOut.m_ObjID))
		return null;

	pstOut.m_RoleInfo	= (HeaderProto.SRoleInfoWithEquip)HeaderProto.DecodeSRoleInfoWithEquip(ref poNetData);
	return pstOut;
}

public static	int		EncodeSOtherInfoWithPos(CMessage msg,ref CNetData poNetData)
{
	SOtherInfoWithPos pstIn = (SOtherInfoWithPos)msg;

		EncodeSOtherInfo(pstIn.m_RoleInfo,ref poNetData);
		HeaderProto.EncodeSPosInfo(pstIn.m_Pos,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSOtherInfoWithPos(ref CNetData poNetData)
{
	SOtherInfoWithPos pstOut = new SOtherInfoWithPos();

	pstOut.m_RoleInfo	= (SOtherInfo)DecodeSOtherInfo(ref poNetData);
	pstOut.m_Pos	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_OTHER_APPEAR(CMessage msg,ref CNetData poNetData)
{
	SC_OTHER_APPEAR pstIn = (SC_OTHER_APPEAR)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSOtherInfoWithPos(pstIn.m_RoleInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_OTHER_APPEAR(ref CNetData poNetData)
{
	SC_OTHER_APPEAR pstOut = new SC_OTHER_APPEAR();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_RoleInfo[i]	= (SOtherInfoWithPos)DecodeSOtherInfoWithPos( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_OTHER_UPDATE(CMessage msg,ref CNetData poNetData)
{
	SC_OTHER_UPDATE pstIn = (SC_OTHER_UPDATE)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSOtherInfo(pstIn.m_RoleInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_OTHER_UPDATE(ref CNetData poNetData)
{
	SC_OTHER_UPDATE pstOut = new SC_OTHER_UPDATE();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_RoleInfo[i]	= (SOtherInfo)DecodeSOtherInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_OTHER_DISAPPEAR(CMessage msg,ref CNetData poNetData)
{
	SC_OTHER_DISAPPEAR pstIn = (SC_OTHER_DISAPPEAR)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > GS_MAX_SYNC_ROLE_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_ObjID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_OTHER_DISAPPEAR(ref CNetData poNetData)
{
	SC_OTHER_DISAPPEAR pstOut = new SC_OTHER_DISAPPEAR();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_ObjID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_MOVE_SYNC_MODE(CMessage msg,ref CNetData poNetData)
{
	CS_MOVE_SYNC_MODE pstIn = (CS_MOVE_SYNC_MODE)msg;

	if(-1 == poNetData.Add(pstIn.m_Mode))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_MOVE_SYNC_MODE(ref CNetData poNetData)
{
	CS_MOVE_SYNC_MODE pstOut = new CS_MOVE_SYNC_MODE();

	if(-1 == poNetData.Del(ref pstOut.m_Mode))
		return null;

	return pstOut;
}

public static	int		EncodeCS_MOVE_BEGIN(CMessage msg,ref CNetData poNetData)
{
	CS_MOVE_BEGIN pstIn = (CS_MOVE_BEGIN)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

		HeaderProto.EncodeSPosInfo(pstIn.m_Position,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_MOVE_BEGIN(ref CNetData poNetData)
{
	CS_MOVE_BEGIN pstOut = new CS_MOVE_BEGIN();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	pstOut.m_Position	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_MOVE_STOP(CMessage msg,ref CNetData poNetData)
{
	CS_MOVE_STOP pstIn = (CS_MOVE_STOP)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

		HeaderProto.EncodeSPosInfo(pstIn.m_Position,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_MOVE_STOP(ref CNetData poNetData)
{
	CS_MOVE_STOP pstOut = new CS_MOVE_STOP();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	pstOut.m_Position	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSMoveObj(CMessage msg,ref CNetData poNetData)
{
	SMoveObj pstIn = (SMoveObj)msg;

	if(-1 == poNetData.Add(pstIn.m_ObjectID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

		HeaderProto.EncodeSPosInfo(pstIn.m_Position,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSMoveObj(ref CNetData poNetData)
{
	SMoveObj pstOut = new SMoveObj();

	if(-1 == poNetData.Del(ref pstOut.m_ObjectID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	pstOut.m_Position	= (HeaderProto.SPosInfo)HeaderProto.DecodeSPosInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_MOVE_BEGIN(CMessage msg,ref CNetData poNetData)
{
	SC_MOVE_BEGIN pstIn = (SC_MOVE_BEGIN)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSMoveObj(pstIn.m_Objs[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_MOVE_BEGIN(ref CNetData poNetData)
{
	SC_MOVE_BEGIN pstOut = new SC_MOVE_BEGIN();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Objs[i]	= (SMoveObj)DecodeSMoveObj( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_MOVE_STOP(CMessage msg,ref CNetData poNetData)
{
	SC_MOVE_STOP pstIn = (SC_MOVE_STOP)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSMoveObj(pstIn.m_Objs[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_MOVE_STOP(ref CNetData poNetData)
{
	SC_MOVE_STOP pstOut = new SC_MOVE_STOP();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)GS_MAX_SYNC_ROLE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)GS_MAX_SYNC_ROLE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Objs[i]	= (SMoveObj)DecodeSMoveObj( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSSkillInfo(CMessage msg,ref CNetData poNetData)
{
	SSkillInfo pstIn = (SSkillInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CooldownTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSSkillInfo(ref CNetData poNetData)
{
	SSkillInfo pstOut = new SSkillInfo();

	if(-1 == poNetData.Del(ref pstOut.m_SkillID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CooldownTime))
		return null;

	return pstOut;
}

public static	int		EncodeSC_USER_SKILLS_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_USER_SKILLS_NTF pstIn = (SC_USER_SKILLS_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillPoint))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TotalSkillTreePoint))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_TRANS_SKILL_NUM || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_TRANS_SKILL_NUM; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSSkillInfo(pstIn.m_SkillsInfo[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_USER_SKILLS_NTF(ref CNetData poNetData)
{
	SC_USER_SKILLS_NTF pstOut = new SC_USER_SKILLS_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_SkillPoint))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TotalSkillTreePoint))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_TRANS_SKILL_NUM || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_TRANS_SKILL_NUM; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_SkillsInfo[i]	= (SSkillInfo)DecodeSSkillInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_LEARN_SKILL(CMessage msg,ref CNetData poNetData)
{
	CS_LEARN_SKILL pstIn = (CS_LEARN_SKILL)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_LEARN_SKILL(ref CNetData poNetData)
{
	CS_LEARN_SKILL pstOut = new CS_LEARN_SKILL();

	if(-1 == poNetData.Del(ref pstOut.m_SkillID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SKILL_INFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SKILL_INFO_NTF pstIn = (SC_SKILL_INFO_NTF)msg;

		EncodeSSkillInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SKILL_INFO_NTF(ref CNetData poNetData)
{
	SC_SKILL_INFO_NTF pstOut = new SC_SKILL_INFO_NTF();

	pstOut.m_Info	= (SSkillInfo)DecodeSSkillInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_REMOVE_SKILL_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_REMOVE_SKILL_NTF pstIn = (SC_REMOVE_SKILL_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_REMOVE_SKILL_NTF(ref CNetData poNetData)
{
	SC_REMOVE_SKILL_NTF pstOut = new SC_REMOVE_SKILL_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_SkillID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SKILL_POINT_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SKILL_POINT_NTF pstIn = (SC_SKILL_POINT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_SkillPoint))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_TotalSkillTreePoint))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SKILL_POINT_NTF(ref CNetData poNetData)
{
	SC_SKILL_POINT_NTF pstOut = new SC_SKILL_POINT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_SkillPoint))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_TotalSkillTreePoint))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SKILL_RESET_ALL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SKILL_RESET_ALL_REQ pstIn = (CS_SKILL_RESET_ALL_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SKILL_RESET_ALL_REQ(ref CNetData poNetData)
{
	CS_SKILL_RESET_ALL_REQ pstOut = new CS_SKILL_RESET_ALL_REQ();

	return pstOut;
}

public static	int		EncodeSC_SELF_ITEM_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_ITEM_NTF pstIn = (SC_SELF_ITEM_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Pos))
		return -1;

		HeaderProto.EncodeSItemList(pstIn.m_ItemList,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_ITEM_NTF(ref CNetData poNetData)
{
	SC_SELF_ITEM_NTF pstOut = new SC_SELF_ITEM_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Pos))
		return null;

	pstOut.m_ItemList	= (HeaderProto.SItemList)HeaderProto.DecodeSItemList(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_MOVE_ITEM_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_MOVE_ITEM_REQ pstIn = (CS_MOVE_ITEM_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_FromPos))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ToPos))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_AnotherUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_MOVE_ITEM_REQ(ref CNetData poNetData)
{
	CS_MOVE_ITEM_REQ pstOut = new CS_MOVE_ITEM_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_FromPos))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ToPos))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_AnotherUUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_MOVE_ITEM_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_MOVE_ITEM_ACK pstIn = (SC_MOVE_ITEM_ACK)msg;

		EncodeCS_MOVE_ITEM_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_MOVE_ITEM_ACK(ref CNetData poNetData)
{
	SC_MOVE_ITEM_ACK pstOut = new SC_MOVE_ITEM_ACK();

	pstOut.m_Info	= (CS_MOVE_ITEM_REQ)DecodeCS_MOVE_ITEM_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_BAG_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_BAG_NTF pstIn = (SC_ROLE_BAG_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_MaxRoleBagCount))
		return -1;

		HeaderProto.EncodeSXITEMS(pstIn.m_Items,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_BAG_NTF(ref CNetData poNetData)
{
	SC_ROLE_BAG_NTF pstOut = new SC_ROLE_BAG_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_MaxRoleBagCount))
		return null;

	pstOut.m_Items	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_LIMIT_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_LIMIT_NTF pstIn = (SC_ROLE_LIMIT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_MaxRoleBagCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxPetBagCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MaxPetCount))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_LIMIT_NTF(ref CNetData poNetData)
{
	SC_ROLE_LIMIT_NTF pstOut = new SC_ROLE_LIMIT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_MaxRoleBagCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxPetBagCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MaxPetCount))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_EQUIP_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_EQUIP_NTF pstIn = (SC_ROLE_EQUIP_NTF)msg;

		HeaderProto.EncodeSXITEMS(pstIn.m_Items,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_EQUIP_NTF(ref CNetData poNetData)
{
	SC_ROLE_EQUIP_NTF pstOut = new SC_ROLE_EQUIP_NTF();

	pstOut.m_Items	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_BAG_SINGLE_LEAVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_BAG_SINGLE_LEAVE_NTF pstIn = (SC_ROLE_BAG_SINGLE_LEAVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_BAG_SINGLE_LEAVE_NTF(ref CNetData poNetData)
{
	SC_ROLE_BAG_SINGLE_LEAVE_NTF pstOut = new SC_ROLE_BAG_SINGLE_LEAVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_BAG_SINGLE_ENTER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_BAG_SINGLE_ENTER_NTF pstIn = (SC_ROLE_BAG_SINGLE_ENTER_NTF)msg;

		HeaderProto.EncodeSXITEM(pstIn.m_Item,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_EnterType))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_BAG_SINGLE_ENTER_NTF(ref CNetData poNetData)
{
	SC_ROLE_BAG_SINGLE_ENTER_NTF pstOut = new SC_ROLE_BAG_SINGLE_ENTER_NTF();

	pstOut.m_Item	= (HeaderProto.SXITEM)HeaderProto.DecodeSXITEM(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_EnterType))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_EQUIP_SINGLE_LEAVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_EQUIP_SINGLE_LEAVE_NTF pstIn = (SC_ROLE_EQUIP_SINGLE_LEAVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_EQUIP_SINGLE_LEAVE_NTF(ref CNetData poNetData)
{
	SC_ROLE_EQUIP_SINGLE_LEAVE_NTF pstOut = new SC_ROLE_EQUIP_SINGLE_LEAVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_EQUIP_SINGLE_ENTER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_EQUIP_SINGLE_ENTER_NTF pstIn = (SC_ROLE_EQUIP_SINGLE_ENTER_NTF)msg;

		HeaderProto.EncodeSXITEM(pstIn.m_Item,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_EQUIP_SINGLE_ENTER_NTF(ref CNetData poNetData)
{
	SC_ROLE_EQUIP_SINGLE_ENTER_NTF pstOut = new SC_ROLE_EQUIP_SINGLE_ENTER_NTF();

	pstOut.m_Item	= (HeaderProto.SXITEM)HeaderProto.DecodeSXITEM(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_ROLE_EQUIP_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_EQUIP_REQ pstIn = (CS_ROLE_EQUIP_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IsEquip))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_EQUIP_REQ(ref CNetData poNetData)
{
	CS_ROLE_EQUIP_REQ pstOut = new CS_ROLE_EQUIP_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IsEquip))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_EVENT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_EVENT_REQ pstIn = (CS_ROLE_EVENT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_EventType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_EVENT_REQ(ref CNetData poNetData)
{
	CS_ROLE_EVENT_REQ pstOut = new CS_ROLE_EVENT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_EventType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_ITEMUP_RPT(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_ITEMUP_RPT pstIn = (CS_ROLE_ITEMUP_RPT)msg;

	if(-1 == poNetData.Add(pstIn.m_UpType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_ITEM_UP_USE_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_UP_USE_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_OtherItemUUID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_ITEMUP_RPT(ref CNetData poNetData)
{
	CS_ROLE_ITEMUP_RPT pstOut = new CS_ROLE_ITEMUP_RPT();

	if(-1 == poNetData.Del(ref pstOut.m_UpType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_ITEM_UP_USE_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_UP_USE_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_OtherItemUUID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeSC_ROLE_ITEMUP_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_ITEMUP_ACK pstIn = (SC_ROLE_ITEMUP_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_UpType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_ITEMUP_ACK(ref CNetData poNetData)
{
	SC_ROLE_ITEMUP_ACK pstOut = new SC_ROLE_ITEMUP_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_UpType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_ITEMUP_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_ITEMUP_NTF pstIn = (SC_ROLE_ITEMUP_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Up))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CurUp))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OK))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_ITEMUP_NTF(ref CNetData poNetData)
{
	SC_ROLE_ITEMUP_NTF pstOut = new SC_ROLE_ITEMUP_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Up))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CurUp))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OK))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_SUIT_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_SUIT_NTF pstIn = (SC_ROLE_SUIT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_ITEM_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		HeaderProto.EncodeSROLE_SUIT(pstIn.m_Suit[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_SUIT_NTF(ref CNetData poNetData)
{
	SC_ROLE_SUIT_NTF pstOut = new SC_ROLE_SUIT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_ITEM_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Suit[i]	= (HeaderProto.SROLE_SUIT)HeaderProto.DecodeSROLE_SUIT( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSROLE_HOLE_ON(CMessage msg,ref CNetData poNetData)
{
	SROLE_HOLE_ON pstIn = (SROLE_HOLE_ON)msg;

	if(-1 == poNetData.Add(pstIn.m_HoleIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_OtherUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_HOLE_ON(ref CNetData poNetData)
{
	SROLE_HOLE_ON pstOut = new SROLE_HOLE_ON();

	if(-1 == poNetData.Del(ref pstOut.m_HoleIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_OtherUUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_HOLE_ON_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_HOLE_ON_REQ pstIn = (CS_ROLE_HOLE_ON_REQ)msg;

		EncodeSROLE_HOLE_ON(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_HOLE_ON_REQ(ref CNetData poNetData)
{
	CS_ROLE_HOLE_ON_REQ pstOut = new CS_ROLE_HOLE_ON_REQ();

	pstOut.m_Hole	= (SROLE_HOLE_ON)DecodeSROLE_HOLE_ON(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_HOLE_ON_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_HOLE_ON_ACK pstIn = (SC_ROLE_HOLE_ON_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

		EncodeSROLE_HOLE_ON(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_HOLE_ON_ACK(ref CNetData poNetData)
{
	SC_ROLE_HOLE_ON_ACK pstOut = new SC_ROLE_HOLE_ON_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	pstOut.m_Hole	= (SROLE_HOLE_ON)DecodeSROLE_HOLE_ON(ref poNetData);
	return pstOut;
}

public static	int		EncodeSROLE_HOLE_OFF(CMessage msg,ref CNetData poNetData)
{
	SROLE_HOLE_OFF pstIn = (SROLE_HOLE_OFF)msg;

	if(-1 == poNetData.Add(pstIn.m_HoleIndex))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_HOLE_OFF(ref CNetData poNetData)
{
	SROLE_HOLE_OFF pstOut = new SROLE_HOLE_OFF();

	if(-1 == poNetData.Del(ref pstOut.m_HoleIndex))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_HOLE_OFF_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_HOLE_OFF_REQ pstIn = (CS_ROLE_HOLE_OFF_REQ)msg;

		EncodeSROLE_HOLE_OFF(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_HOLE_OFF_REQ(ref CNetData poNetData)
{
	CS_ROLE_HOLE_OFF_REQ pstOut = new CS_ROLE_HOLE_OFF_REQ();

	pstOut.m_Hole	= (SROLE_HOLE_OFF)DecodeSROLE_HOLE_OFF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_HOLE_OFF_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_HOLE_OFF_ACK pstIn = (SC_ROLE_HOLE_OFF_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

		EncodeSROLE_HOLE_OFF(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_HOLE_OFF_ACK(ref CNetData poNetData)
{
	SC_ROLE_HOLE_OFF_ACK pstOut = new SC_ROLE_HOLE_OFF_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	pstOut.m_Hole	= (SROLE_HOLE_OFF)DecodeSROLE_HOLE_OFF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSROLE_HOLE_REFRESH(CMessage msg,ref CNetData poNetData)
{
	SROLE_HOLE_REFRESH pstIn = (SROLE_HOLE_REFRESH)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Item2UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_HOLE_REFRESH(ref CNetData poNetData)
{
	SROLE_HOLE_REFRESH pstOut = new SROLE_HOLE_REFRESH();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Item2UUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_HOLE_REFRESH_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_HOLE_REFRESH_REQ pstIn = (CS_ROLE_HOLE_REFRESH_REQ)msg;

		EncodeSROLE_HOLE_REFRESH(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_HOLE_REFRESH_REQ(ref CNetData poNetData)
{
	CS_ROLE_HOLE_REFRESH_REQ pstOut = new CS_ROLE_HOLE_REFRESH_REQ();

	pstOut.m_Hole	= (SROLE_HOLE_REFRESH)DecodeSROLE_HOLE_REFRESH(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_HOLE_REFRESH_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_HOLE_REFRESH_ACK pstIn = (SC_ROLE_HOLE_REFRESH_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

		EncodeSDropInfoList(pstIn.m_Drops,ref poNetData);
		EncodeSROLE_HOLE_REFRESH(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_HOLE_REFRESH_ACK(ref CNetData poNetData)
{
	SC_ROLE_HOLE_REFRESH_ACK pstOut = new SC_ROLE_HOLE_REFRESH_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	pstOut.m_Drops	= (SDropInfoList)DecodeSDropInfoList(ref poNetData);
	pstOut.m_Hole	= (SROLE_HOLE_REFRESH)DecodeSROLE_HOLE_REFRESH(ref poNetData);
	return pstOut;
}

public static	int		EncodeSROLE_HOLE_MERGE(CMessage msg,ref CNetData poNetData)
{
	SROLE_HOLE_MERGE pstIn = (SROLE_HOLE_MERGE)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Item2UUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Item3UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_HOLE_MERGE(ref CNetData poNetData)
{
	SROLE_HOLE_MERGE pstOut = new SROLE_HOLE_MERGE();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Item2UUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Item3UUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_HOLE_MERGE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_HOLE_MERGE_REQ pstIn = (CS_ROLE_HOLE_MERGE_REQ)msg;

		EncodeSROLE_HOLE_MERGE(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_HOLE_MERGE_REQ(ref CNetData poNetData)
{
	CS_ROLE_HOLE_MERGE_REQ pstOut = new CS_ROLE_HOLE_MERGE_REQ();

	pstOut.m_Hole	= (SROLE_HOLE_MERGE)DecodeSROLE_HOLE_MERGE(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_HOLE_MERGE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_HOLE_MERGE_ACK pstIn = (SC_ROLE_HOLE_MERGE_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

		EncodeSDropInfoList(pstIn.m_Drops,ref poNetData);
		EncodeSROLE_HOLE_MERGE(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_HOLE_MERGE_ACK(ref CNetData poNetData)
{
	SC_ROLE_HOLE_MERGE_ACK pstOut = new SC_ROLE_HOLE_MERGE_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	pstOut.m_Drops	= (SDropInfoList)DecodeSDropInfoList(ref poNetData);
	pstOut.m_Hole	= (SROLE_HOLE_MERGE)DecodeSROLE_HOLE_MERGE(ref poNetData);
	return pstOut;
}

public static	int		EncodeSROLE_HOLE_MERGE2(CMessage msg,ref CNetData poNetData)
{
	SROLE_HOLE_MERGE2 pstIn = (SROLE_HOLE_MERGE2)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSROLE_HOLE_MERGE2(ref CNetData poNetData)
{
	SROLE_HOLE_MERGE2 pstOut = new SROLE_HOLE_MERGE2();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_HOLE_MERGE2_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_HOLE_MERGE2_REQ pstIn = (CS_ROLE_HOLE_MERGE2_REQ)msg;

		EncodeSROLE_HOLE_MERGE2(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_HOLE_MERGE2_REQ(ref CNetData poNetData)
{
	CS_ROLE_HOLE_MERGE2_REQ pstOut = new CS_ROLE_HOLE_MERGE2_REQ();

	pstOut.m_Hole	= (SROLE_HOLE_MERGE2)DecodeSROLE_HOLE_MERGE2(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_HOLE_MERGE2_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_HOLE_MERGE2_ACK pstIn = (SC_ROLE_HOLE_MERGE2_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

		EncodeSDropInfoList(pstIn.m_Drops,ref poNetData);
		EncodeSROLE_HOLE_MERGE2(pstIn.m_Hole,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_HOLE_MERGE2_ACK(ref CNetData poNetData)
{
	SC_ROLE_HOLE_MERGE2_ACK pstOut = new SC_ROLE_HOLE_MERGE2_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	pstOut.m_Drops	= (SDropInfoList)DecodeSDropInfoList(ref poNetData);
	pstOut.m_Hole	= (SROLE_HOLE_MERGE2)DecodeSROLE_HOLE_MERGE2(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_LOCK_RPT(CMessage msg,ref CNetData poNetData)
{
	CS_LOCK_RPT pstIn = (CS_LOCK_RPT)msg;

	if(-1 == poNetData.Add(pstIn.m_LockType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Lock))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_LOCK_RPT(ref CNetData poNetData)
{
	CS_LOCK_RPT pstOut = new CS_LOCK_RPT();

	if(-1 == poNetData.Del(ref pstOut.m_LockType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Lock))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_ROLE_USEITEM_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_USEITEM_REQ pstIn = (CS_ROLE_USEITEM_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_USEITEM_REQ(ref CNetData poNetData)
{
	CS_ROLE_USEITEM_REQ pstOut = new CS_ROLE_USEITEM_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_USEITEM_START_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_USEITEM_START_ACK pstIn = (SC_ROLE_USEITEM_START_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_USEITEM_START_ACK(ref CNetData poNetData)
{
	SC_ROLE_USEITEM_START_ACK pstOut = new SC_ROLE_USEITEM_START_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ROLE_USEITEM_END_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_USEITEM_END_ACK pstIn = (SC_ROLE_USEITEM_END_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_USEITEM_END_ACK(ref CNetData poNetData)
{
	SC_ROLE_USEITEM_END_ACK pstOut = new SC_ROLE_USEITEM_END_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	return pstOut;
}

public static	int		EncodeCS_PET_USEITEM_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PET_USEITEM_REQ pstIn = (CS_PET_USEITEM_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_USEITEM_REQ(ref CNetData poNetData)
{
	CS_PET_USEITEM_REQ pstOut = new CS_PET_USEITEM_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_USEITEM_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PET_USEITEM_ACK pstIn = (SC_PET_USEITEM_ACK)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_USEITEM_ACK(ref CNetData poNetData)
{
	SC_PET_USEITEM_ACK pstOut = new SC_PET_USEITEM_ACK();

	return pstOut;
}

public static	int		EncodeSC_PET_BAG_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_BAG_NTF pstIn = (SC_PET_BAG_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_MaxPetBagCount))
		return -1;

		HeaderProto.EncodeSXITEMS(pstIn.m_Items,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_BAG_NTF(ref CNetData poNetData)
{
	SC_PET_BAG_NTF pstOut = new SC_PET_BAG_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_MaxPetBagCount))
		return null;

	pstOut.m_Items	= (HeaderProto.SXITEMS)HeaderProto.DecodeSXITEMS(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_PET_BAG_SINGLE_LEAVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_BAG_SINGLE_LEAVE_NTF pstIn = (SC_PET_BAG_SINGLE_LEAVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_BAG_SINGLE_LEAVE_NTF(ref CNetData poNetData)
{
	SC_PET_BAG_SINGLE_LEAVE_NTF pstOut = new SC_PET_BAG_SINGLE_LEAVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_BAG_SINGLE_ENTER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_BAG_SINGLE_ENTER_NTF pstIn = (SC_PET_BAG_SINGLE_ENTER_NTF)msg;

		HeaderProto.EncodeSXITEM(pstIn.m_Item,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_BAG_SINGLE_ENTER_NTF(ref CNetData poNetData)
{
	SC_PET_BAG_SINGLE_ENTER_NTF pstOut = new SC_PET_BAG_SINGLE_ENTER_NTF();

	pstOut.m_Item	= (HeaderProto.SXITEM)HeaderProto.DecodeSXITEM(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_PET_EQUIP_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PET_EQUIP_REQ pstIn = (CS_PET_EQUIP_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_ItemUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_PetUUID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_IsEquip))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_EQUIP_REQ(ref CNetData poNetData)
{
	CS_PET_EQUIP_REQ pstOut = new CS_PET_EQUIP_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_ItemUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_PetUUID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_IsEquip))
		return null;

	return pstOut;
}

public static	int		EncodeCS_PET_EVENT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PET_EVENT_REQ pstIn = (CS_PET_EVENT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_EventType))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UUID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_EVENT_REQ(ref CNetData poNetData)
{
	CS_PET_EVENT_REQ pstOut = new CS_PET_EVENT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_EventType))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UUID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_EVENT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PET_EVENT_ACK pstIn = (SC_PET_EVENT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_EventType))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_EVENT_ACK(ref CNetData poNetData)
{
	SC_PET_EVENT_ACK pstOut = new SC_PET_EVENT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_EventType))
		return null;

	return pstOut;
}

public static	int		EncodeCS_PET_TEAM_RPT(CMessage msg,ref CNetData poNetData)
{
	CS_PET_TEAM_RPT pstIn = (CS_PET_TEAM_RPT)msg;

	if(-1 == poNetData.Add(pstIn.m_Team))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PET_TEAM_RPT(ref CNetData poNetData)
{
	CS_PET_TEAM_RPT pstOut = new CS_PET_TEAM_RPT();

	if(-1 == poNetData.Del(ref pstOut.m_Team))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PET_TEAM_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PET_TEAM_NTF pstIn = (SC_PET_TEAM_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Team))
		return -1;

	int i;
	for(i = 0; i < (int)HeaderProto.EPetBattlePos.PET_BTPOS_MAX; ++i)
	{
		if(-1 == poNetData.Add(pstIn.m_PetUUID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PET_TEAM_NTF(ref CNetData poNetData)
{
	SC_PET_TEAM_NTF pstOut = new SC_PET_TEAM_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Team))
		return null;

	int i;
	for(i = 0; i < (int)HeaderProto.EPetBattlePos.PET_BTPOS_MAX; ++i)
	{
		if(-1 == poNetData.Del(ref pstOut.m_PetUUID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_CHAT(CMessage msg,ref CNetData poNetData)
{
	CS_CHAT pstIn = (CS_CHAT)msg;

		HeaderProto.EncodeSCHAT(pstIn.m_Chat,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_CHAT(ref CNetData poNetData)
{
	CS_CHAT pstOut = new CS_CHAT();

	pstOut.m_Chat	= (HeaderProto.SCHAT)HeaderProto.DecodeSCHAT(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_CHAT(CMessage msg,ref CNetData poNetData)
{
	SC_CHAT pstIn = (SC_CHAT)msg;

		HeaderProto.EncodeSCHAT(pstIn.m_Chat,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_CHAT(ref CNetData poNetData)
{
	SC_CHAT pstOut = new SC_CHAT();

	pstOut.m_Chat	= (HeaderProto.SCHAT)HeaderProto.DecodeSCHAT(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_CHAT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_CHAT_ACK pstIn = (SC_CHAT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Error))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_CooldownTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_CHAT_ACK(ref CNetData poNetData)
{
	SC_CHAT_ACK pstOut = new SC_CHAT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Error))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_CooldownTime))
		return null;

	return pstOut;
}

public static	int		EncodeCS_FRIENDS_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_FRIENDS_LIST_REQ pstIn = (CS_FRIENDS_LIST_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_FRIENDS_LIST_REQ(ref CNetData poNetData)
{
	CS_FRIENDS_LIST_REQ pstOut = new CS_FRIENDS_LIST_REQ();

	return pstOut;
}

public static	int		EncodeSC_FRIENDS_LIST_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_FRIENDS_LIST_NTF pstIn = (SC_FRIENDS_LIST_NTF)msg;

		HeaderProto.EncodeFRIENDS_LIST_NTF(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_FRIENDS_LIST_NTF(ref CNetData poNetData)
{
	SC_FRIENDS_LIST_NTF pstOut = new SC_FRIENDS_LIST_NTF();

	pstOut.m_Info	= (HeaderProto.FRIENDS_LIST_NTF)HeaderProto.DecodeFRIENDS_LIST_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_FRIEND_EPINFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_FRIEND_EPINFO_NTF pstIn = (SC_FRIEND_EPINFO_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_EPSent))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EPSendMax))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EPRecv))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_EPRecvMax))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_FRIEND_EPINFO_NTF(ref CNetData poNetData)
{
	SC_FRIEND_EPINFO_NTF pstOut = new SC_FRIEND_EPINFO_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_EPSent))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EPSendMax))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EPRecv))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_EPRecvMax))
		return null;

	return pstOut;
}

public static	int		EncodeCS_QUERY_ROLE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_QUERY_ROLE_REQ pstIn = (CS_QUERY_ROLE_REQ)msg;

		HeaderProto.EncodeQUERY_ROLE_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_QUERY_ROLE_REQ(ref CNetData poNetData)
{
	CS_QUERY_ROLE_REQ pstOut = new CS_QUERY_ROLE_REQ();

	pstOut.m_Info	= (HeaderProto.QUERY_ROLE_REQ)HeaderProto.DecodeQUERY_ROLE_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_QUERY_ROLE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_QUERY_ROLE_ACK pstIn = (SC_QUERY_ROLE_ACK)msg;

		HeaderProto.EncodeQUERY_ROLE_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_QUERY_ROLE_ACK(ref CNetData poNetData)
{
	SC_QUERY_ROLE_ACK pstOut = new SC_QUERY_ROLE_ACK();

	pstOut.m_Info	= (HeaderProto.QUERY_ROLE_ACK)HeaderProto.DecodeQUERY_ROLE_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_ADD_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ADD_FRIEND_REQ pstIn = (CS_ADD_FRIEND_REQ)msg;

		HeaderProto.EncodeADD_FRIEND_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ADD_FRIEND_REQ(ref CNetData poNetData)
{
	CS_ADD_FRIEND_REQ pstOut = new CS_ADD_FRIEND_REQ();

	pstOut.m_Info	= (HeaderProto.ADD_FRIEND_REQ)HeaderProto.DecodeADD_FRIEND_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ADD_FRIEND_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ADD_FRIEND_ACK pstIn = (SC_ADD_FRIEND_ACK)msg;

		HeaderProto.EncodeADD_FRIEND_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ADD_FRIEND_ACK(ref CNetData poNetData)
{
	SC_ADD_FRIEND_ACK pstOut = new SC_ADD_FRIEND_ACK();

	pstOut.m_Info	= (HeaderProto.ADD_FRIEND_ACK)HeaderProto.DecodeADD_FRIEND_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_REMOVE_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_REMOVE_FRIEND_REQ pstIn = (CS_REMOVE_FRIEND_REQ)msg;

		HeaderProto.EncodeREMOVE_FRIEND_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_REMOVE_FRIEND_REQ(ref CNetData poNetData)
{
	CS_REMOVE_FRIEND_REQ pstOut = new CS_REMOVE_FRIEND_REQ();

	pstOut.m_Info	= (HeaderProto.REMOVE_FRIEND_REQ)HeaderProto.DecodeREMOVE_FRIEND_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_REMOVE_FRIEND_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_REMOVE_FRIEND_ACK pstIn = (SC_REMOVE_FRIEND_ACK)msg;

		HeaderProto.EncodeREMOVE_FRIEND_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_REMOVE_FRIEND_ACK(ref CNetData poNetData)
{
	SC_REMOVE_FRIEND_ACK pstOut = new SC_REMOVE_FRIEND_ACK();

	pstOut.m_Info	= (HeaderProto.REMOVE_FRIEND_ACK)HeaderProto.DecodeREMOVE_FRIEND_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_DONATE_ACTION_POINT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_DONATE_ACTION_POINT_REQ pstIn = (CS_DONATE_ACTION_POINT_REQ)msg;

		HeaderProto.EncodeDONATE_ACTION_POINT_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_DONATE_ACTION_POINT_REQ(ref CNetData poNetData)
{
	CS_DONATE_ACTION_POINT_REQ pstOut = new CS_DONATE_ACTION_POINT_REQ();

	pstOut.m_Info	= (HeaderProto.DONATE_ACTION_POINT_REQ)HeaderProto.DecodeDONATE_ACTION_POINT_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_DONATE_ACTION_POINT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_DONATE_ACTION_POINT_ACK pstIn = (SC_DONATE_ACTION_POINT_ACK)msg;

		HeaderProto.EncodeDONATE_ACTION_POINT_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_DONATE_ACTION_POINT_ACK(ref CNetData poNetData)
{
	SC_DONATE_ACTION_POINT_ACK pstOut = new SC_DONATE_ACTION_POINT_ACK();

	pstOut.m_Info	= (HeaderProto.DONATE_ACTION_POINT_ACK)HeaderProto.DecodeDONATE_ACTION_POINT_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_DONATE_ACTION_POINT_OTHER_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_DONATE_ACTION_POINT_OTHER_NTF pstIn = (SC_DONATE_ACTION_POINT_OTHER_NTF)msg;

		HeaderProto.EncodeDONATE_ACTION_POINT_OTHER_NTF(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_DONATE_ACTION_POINT_OTHER_NTF(ref CNetData poNetData)
{
	SC_DONATE_ACTION_POINT_OTHER_NTF pstOut = new SC_DONATE_ACTION_POINT_OTHER_NTF();

	pstOut.m_Info	= (HeaderProto.DONATE_ACTION_POINT_OTHER_NTF)HeaderProto.DecodeDONATE_ACTION_POINT_OTHER_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_RECEIVE_ACTION_POINT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_RECEIVE_ACTION_POINT_REQ pstIn = (CS_RECEIVE_ACTION_POINT_REQ)msg;

		HeaderProto.EncodeRECEIVE_ACTION_POINT_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_RECEIVE_ACTION_POINT_REQ(ref CNetData poNetData)
{
	CS_RECEIVE_ACTION_POINT_REQ pstOut = new CS_RECEIVE_ACTION_POINT_REQ();

	pstOut.m_Info	= (HeaderProto.RECEIVE_ACTION_POINT_REQ)HeaderProto.DecodeRECEIVE_ACTION_POINT_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_RECEIVE_ACTION_POINT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_RECEIVE_ACTION_POINT_ACK pstIn = (SC_RECEIVE_ACTION_POINT_ACK)msg;

		HeaderProto.EncodeRECEIVE_ACTION_POINT_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_RECEIVE_ACTION_POINT_ACK(ref CNetData poNetData)
{
	SC_RECEIVE_ACTION_POINT_ACK pstOut = new SC_RECEIVE_ACTION_POINT_ACK();

	pstOut.m_Info	= (HeaderProto.RECEIVE_ACTION_POINT_ACK)HeaderProto.DecodeRECEIVE_ACTION_POINT_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_RESPONSE_ADD_FRIEND_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_RESPONSE_ADD_FRIEND_REQ pstIn = (CS_RESPONSE_ADD_FRIEND_REQ)msg;

		HeaderProto.EncodeRESPONSE_ADD_FRIEND_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_RESPONSE_ADD_FRIEND_REQ(ref CNetData poNetData)
{
	CS_RESPONSE_ADD_FRIEND_REQ pstOut = new CS_RESPONSE_ADD_FRIEND_REQ();

	pstOut.m_Info	= (HeaderProto.RESPONSE_ADD_FRIEND_REQ)HeaderProto.DecodeRESPONSE_ADD_FRIEND_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_REQUEST_ADD_FRIEND_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_REQUEST_ADD_FRIEND_NTF pstIn = (SC_REQUEST_ADD_FRIEND_NTF)msg;

		HeaderProto.EncodeREQUEST_ADD_FRIEND_NTF(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_REQUEST_ADD_FRIEND_NTF(ref CNetData poNetData)
{
	SC_REQUEST_ADD_FRIEND_NTF pstOut = new SC_REQUEST_ADD_FRIEND_NTF();

	pstOut.m_Info	= (HeaderProto.REQUEST_ADD_FRIEND_NTF)HeaderProto.DecodeREQUEST_ADD_FRIEND_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_FRIEND_INFO_CHANGE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_FRIEND_INFO_CHANGE_NTF pstIn = (SC_FRIEND_INFO_CHANGE_NTF)msg;

		HeaderProto.EncodeFRIEND_INFO_CHANGE_NTF(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_FRIEND_INFO_CHANGE_NTF(ref CNetData poNetData)
{
	SC_FRIEND_INFO_CHANGE_NTF pstOut = new SC_FRIEND_INFO_CHANGE_NTF();

	pstOut.m_Info	= (HeaderProto.FRIEND_INFO_CHANGE_NTF)HeaderProto.DecodeFRIEND_INFO_CHANGE_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_FRIEND_REMOVE_INVITE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_FRIEND_REMOVE_INVITE_NTF pstIn = (SC_FRIEND_REMOVE_INVITE_NTF)msg;

		HeaderProto.EncodeFRIEND_REMOVE_INVITE_NTF(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_FRIEND_REMOVE_INVITE_NTF(ref CNetData poNetData)
{
	SC_FRIEND_REMOVE_INVITE_NTF pstOut = new SC_FRIEND_REMOVE_INVITE_NTF();

	pstOut.m_Info	= (HeaderProto.FRIEND_REMOVE_INVITE_NTF)HeaderProto.DecodeFRIEND_REMOVE_INVITE_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_FRIEND_ASSIST_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_FRIEND_ASSIST_LIST_REQ pstIn = (CS_FRIEND_ASSIST_LIST_REQ)msg;

		HeaderProto.EncodeFRIEND_ASSIST_LIST_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_FRIEND_ASSIST_LIST_REQ(ref CNetData poNetData)
{
	CS_FRIEND_ASSIST_LIST_REQ pstOut = new CS_FRIEND_ASSIST_LIST_REQ();

	pstOut.m_Info	= (HeaderProto.FRIEND_ASSIST_LIST_REQ)HeaderProto.DecodeFRIEND_ASSIST_LIST_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSAssistBattleInfo(CMessage msg,ref CNetData poNetData)
{
	SAssistBattleInfo pstIn = (SAssistBattleInfo)msg;

		HeaderProto.EncodeSAssistBattlePersonInfo(pstIn.m_Assistor,ref poNetData);
		EncodeSPetInfo(pstIn.m_BattlePet,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSAssistBattleInfo(ref CNetData poNetData)
{
	SAssistBattleInfo pstOut = new SAssistBattleInfo();

	pstOut.m_Assistor	= (HeaderProto.SAssistBattlePersonInfo)HeaderProto.DecodeSAssistBattlePersonInfo(ref poNetData);
	pstOut.m_BattlePet	= (SPetInfo)DecodeSPetInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_FRIEND_ASSIST_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_FRIEND_ASSIST_LIST_ACK pstIn = (SC_FRIEND_ASSIST_LIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_FRIENDASSIST_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_FRIENDASSIST_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSAssistBattleInfo(pstIn.m_Assistors[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_FRIEND_ASSIST_LIST_ACK(ref CNetData poNetData)
{
	SC_FRIEND_ASSIST_LIST_ACK pstOut = new SC_FRIEND_ASSIST_LIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_FRIENDASSIST_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_FRIENDASSIST_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Assistors[i]	= (SAssistBattleInfo)DecodeSAssistBattleInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_SELECT_FRIEND_ASSIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SELECT_FRIEND_ASSIST_REQ pstIn = (CS_SELECT_FRIEND_ASSIST_REQ)msg;

		HeaderProto.EncodeSELECT_FRIEND_ASSIST_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SELECT_FRIEND_ASSIST_REQ(ref CNetData poNetData)
{
	CS_SELECT_FRIEND_ASSIST_REQ pstOut = new CS_SELECT_FRIEND_ASSIST_REQ();

	pstOut.m_Info	= (HeaderProto.SELECT_FRIEND_ASSIST_REQ)HeaderProto.DecodeSELECT_FRIEND_ASSIST_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_SELECT_FRIEND_ASSIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SELECT_FRIEND_ASSIST_ACK pstIn = (SC_SELECT_FRIEND_ASSIST_ACK)msg;

		HeaderProto.EncodeSELECT_FRIEND_ASSIST_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELECT_FRIEND_ASSIST_ACK(ref CNetData poNetData)
{
	SC_SELECT_FRIEND_ASSIST_ACK pstOut = new SC_SELECT_FRIEND_ASSIST_ACK();

	pstOut.m_Info	= (HeaderProto.SELECT_FRIEND_ASSIST_ACK)HeaderProto.DecodeSELECT_FRIEND_ASSIST_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ABYSS_TRIGGER_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ABYSS_TRIGGER_ACK pstIn = (SC_ABYSS_TRIGGER_ACK)msg;

		HeaderProto.EncodeABYSS_TRIGGER_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ABYSS_TRIGGER_ACK(ref CNetData poNetData)
{
	SC_ABYSS_TRIGGER_ACK pstOut = new SC_ABYSS_TRIGGER_ACK();

	pstOut.m_Info	= (HeaderProto.ABYSS_TRIGGER_ACK)HeaderProto.DecodeABYSS_TRIGGER_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_ABYSS_OPEN_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ABYSS_OPEN_REQ pstIn = (CS_ABYSS_OPEN_REQ)msg;

		HeaderProto.EncodeABYSS_OPEN_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ABYSS_OPEN_REQ(ref CNetData poNetData)
{
	CS_ABYSS_OPEN_REQ pstOut = new CS_ABYSS_OPEN_REQ();

	pstOut.m_Info	= (HeaderProto.ABYSS_OPEN_REQ)HeaderProto.DecodeABYSS_OPEN_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ABYSS_OPEN_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ABYSS_OPEN_ACK pstIn = (SC_ABYSS_OPEN_ACK)msg;

		HeaderProto.EncodeABYSS_OPEN_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ABYSS_OPEN_ACK(ref CNetData poNetData)
{
	SC_ABYSS_OPEN_ACK pstOut = new SC_ABYSS_OPEN_ACK();

	pstOut.m_Info	= (HeaderProto.ABYSS_OPEN_ACK)HeaderProto.DecodeABYSS_OPEN_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ABYSS_OPEN_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ABYSS_OPEN_NTF pstIn = (SC_ABYSS_OPEN_NTF)msg;

		HeaderProto.EncodeABYSS_OPEN_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ABYSS_OPEN_NTF(ref CNetData poNetData)
{
	SC_ABYSS_OPEN_NTF pstOut = new SC_ABYSS_OPEN_NTF();

	pstOut.m_Info	= (HeaderProto.ABYSS_OPEN_ACK)HeaderProto.DecodeABYSS_OPEN_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GET_ABYSS_TRIGGER_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_ABYSS_TRIGGER_LIST_REQ pstIn = (CS_GET_ABYSS_TRIGGER_LIST_REQ)msg;

		HeaderProto.EncodeGET_ABYSS_TRIGGER_LIST_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_ABYSS_TRIGGER_LIST_REQ(ref CNetData poNetData)
{
	CS_GET_ABYSS_TRIGGER_LIST_REQ pstOut = new CS_GET_ABYSS_TRIGGER_LIST_REQ();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_TRIGGER_LIST_REQ)HeaderProto.DecodeGET_ABYSS_TRIGGER_LIST_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GET_ABYSS_TRIGGER_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_ABYSS_TRIGGER_LIST_ACK pstIn = (SC_GET_ABYSS_TRIGGER_LIST_ACK)msg;

		HeaderProto.EncodeGET_ABYSS_TRIGGER_LIST_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_ABYSS_TRIGGER_LIST_ACK(ref CNetData poNetData)
{
	SC_GET_ABYSS_TRIGGER_LIST_ACK pstOut = new SC_GET_ABYSS_TRIGGER_LIST_ACK();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_TRIGGER_LIST_ACK)HeaderProto.DecodeGET_ABYSS_TRIGGER_LIST_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GET_ABYSS_OPEN_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_ABYSS_OPEN_LIST_REQ pstIn = (CS_GET_ABYSS_OPEN_LIST_REQ)msg;

		HeaderProto.EncodeGET_ABYSS_OPEN_LIST_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_ABYSS_OPEN_LIST_REQ(ref CNetData poNetData)
{
	CS_GET_ABYSS_OPEN_LIST_REQ pstOut = new CS_GET_ABYSS_OPEN_LIST_REQ();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_OPEN_LIST_REQ)HeaderProto.DecodeGET_ABYSS_OPEN_LIST_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GET_ABYSS_OPEN_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_ABYSS_OPEN_LIST_ACK pstIn = (SC_GET_ABYSS_OPEN_LIST_ACK)msg;

		HeaderProto.EncodeGET_ABYSS_OPEN_LIST_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_ABYSS_OPEN_LIST_ACK(ref CNetData poNetData)
{
	SC_GET_ABYSS_OPEN_LIST_ACK pstOut = new SC_GET_ABYSS_OPEN_LIST_ACK();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_OPEN_LIST_ACK)HeaderProto.DecodeGET_ABYSS_OPEN_LIST_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GET_ABYSS_OPEN_REC_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_ABYSS_OPEN_REC_REQ pstIn = (CS_GET_ABYSS_OPEN_REC_REQ)msg;

		HeaderProto.EncodeGET_ABYSS_OPEN_REC_REQ(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_ABYSS_OPEN_REC_REQ(ref CNetData poNetData)
{
	CS_GET_ABYSS_OPEN_REC_REQ pstOut = new CS_GET_ABYSS_OPEN_REC_REQ();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_OPEN_REC_REQ)HeaderProto.DecodeGET_ABYSS_OPEN_REC_REQ(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GET_ABYSS_OPEN_REC_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_ABYSS_OPEN_REC_ACK pstIn = (SC_GET_ABYSS_OPEN_REC_ACK)msg;

		HeaderProto.EncodeGET_ABYSS_OPEN_REC_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_ABYSS_OPEN_REC_ACK(ref CNetData poNetData)
{
	SC_GET_ABYSS_OPEN_REC_ACK pstOut = new SC_GET_ABYSS_OPEN_REC_ACK();

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_OPEN_REC_ACK)HeaderProto.DecodeGET_ABYSS_OPEN_REC_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ENTER_ABYSS_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ENTER_ABYSS_ACK pstIn = (SC_ENTER_ABYSS_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

		HeaderProto.EncodeGET_ABYSS_OPEN_LIST_ACK(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ENTER_ABYSS_ACK(ref CNetData poNetData)
{
	SC_ENTER_ABYSS_ACK pstOut = new SC_ENTER_ABYSS_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	pstOut.m_Info	= (HeaderProto.GET_ABYSS_OPEN_LIST_ACK)HeaderProto.DecodeGET_ABYSS_OPEN_LIST_ACK(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ABYSS_RUN_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_ABYSS_RUN_NTF pstIn = (SC_ABYSS_RUN_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_ActivityAbyssDBID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ABYSS_RUN_NTF(ref CNetData poNetData)
{
	SC_ABYSS_RUN_NTF pstOut = new SC_ABYSS_RUN_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_ActivityAbyssDBID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_MAIL_LIST_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_MAIL_LIST_NTF pstIn = (SC_MAIL_LIST_NTF)msg;

		HeaderProto.EncodeMAIL_LIST(pstIn.m_List,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_MAIL_LIST_NTF(ref CNetData poNetData)
{
	SC_MAIL_LIST_NTF pstOut = new SC_MAIL_LIST_NTF();

	pstOut.m_List	= (HeaderProto.MAIL_LIST)HeaderProto.DecodeMAIL_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_SEND_MAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SEND_MAIL_REQ pstIn = (CS_SEND_MAIL_REQ)msg;

		HeaderProto.EncodeSEND_MAIL(pstIn.m_MailInfo,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SEND_MAIL_REQ(ref CNetData poNetData)
{
	CS_SEND_MAIL_REQ pstOut = new CS_SEND_MAIL_REQ();

	pstOut.m_MailInfo	= (HeaderProto.SEND_MAIL)HeaderProto.DecodeSEND_MAIL(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_SEND_MAIL_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SEND_MAIL_ACK pstIn = (SC_SEND_MAIL_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SEND_MAIL_ACK(ref CNetData poNetData)
{
	SC_SEND_MAIL_ACK pstOut = new SC_SEND_MAIL_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_ITEM_FROM_MAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_ITEM_FROM_MAIL_REQ pstIn = (CS_GET_ITEM_FROM_MAIL_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_MailID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_ITEM_FROM_MAIL_REQ(ref CNetData poNetData)
{
	CS_GET_ITEM_FROM_MAIL_REQ pstOut = new CS_GET_ITEM_FROM_MAIL_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_MailID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_MONEY_FROM_MAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_MONEY_FROM_MAIL_REQ pstIn = (CS_GET_MONEY_FROM_MAIL_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_MailID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_MONEY_FROM_MAIL_REQ(ref CNetData poNetData)
{
	CS_GET_MONEY_FROM_MAIL_REQ pstOut = new CS_GET_MONEY_FROM_MAIL_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_MailID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_READ_MAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_READ_MAIL_REQ pstIn = (CS_READ_MAIL_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_MailID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_READ_MAIL_REQ(ref CNetData poNetData)
{
	CS_READ_MAIL_REQ pstOut = new CS_READ_MAIL_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_MailID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_READ_MAIL_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_READ_MAIL_ACK pstIn = (SC_READ_MAIL_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_MailID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ReadTime))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_READ_MAIL_ACK(ref CNetData poNetData)
{
	SC_READ_MAIL_ACK pstOut = new SC_READ_MAIL_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_MailID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ReadTime))
		return null;

	return pstOut;
}

public static	int		EncodeCS_DELETE_MAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_DELETE_MAIL_REQ pstIn = (CS_DELETE_MAIL_REQ)msg;

		HeaderProto.EncodeMAIL_ID_LIST(pstIn.m_Mails,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_DELETE_MAIL_REQ(ref CNetData poNetData)
{
	CS_DELETE_MAIL_REQ pstOut = new CS_DELETE_MAIL_REQ();

	pstOut.m_Mails	= (HeaderProto.MAIL_ID_LIST)HeaderProto.DecodeMAIL_ID_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_DELETE_MAIL_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_DELETE_MAIL_ACK pstIn = (SC_DELETE_MAIL_ACK)msg;

		HeaderProto.EncodeMAIL_ID_LIST(pstIn.m_Mails,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_DELETE_MAIL_ACK(ref CNetData poNetData)
{
	SC_DELETE_MAIL_ACK pstOut = new SC_DELETE_MAIL_ACK();

	pstOut.m_Mails	= (HeaderProto.MAIL_ID_LIST)HeaderProto.DecodeMAIL_ID_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_UPDATE_MAIL_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_UPDATE_MAIL_NTF pstIn = (SC_UPDATE_MAIL_NTF)msg;

		HeaderProto.EncodeMAIL_LIST(pstIn.m_List,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_UPDATE_MAIL_NTF(ref CNetData poNetData)
{
	SC_UPDATE_MAIL_NTF pstOut = new SC_UPDATE_MAIL_NTF();

	pstOut.m_List	= (HeaderProto.MAIL_LIST)HeaderProto.DecodeMAIL_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_SHOP_ACTION_POINT_BOUGHT_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_ACTION_POINT_BOUGHT_NTF pstIn = (SC_SHOP_ACTION_POINT_BOUGHT_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_BoughtCount))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_ACTION_POINT_BOUGHT_NTF(ref CNetData poNetData)
{
	SC_SHOP_ACTION_POINT_BOUGHT_NTF pstOut = new SC_SHOP_ACTION_POINT_BOUGHT_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_BoughtCount))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SHOP_BUY_ACTION_POINT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_BUY_ACTION_POINT_REQ pstIn = (CS_SHOP_BUY_ACTION_POINT_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_BUY_ACTION_POINT_REQ(ref CNetData poNetData)
{
	CS_SHOP_BUY_ACTION_POINT_REQ pstOut = new CS_SHOP_BUY_ACTION_POINT_REQ();

	return pstOut;
}

public static	int		EncodeSC_SHOP_BUY_ACTION_POINT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_BUY_ACTION_POINT_ACK pstIn = (SC_SHOP_BUY_ACTION_POINT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_BUY_ACTION_POINT_ACK(ref CNetData poNetData)
{
	SC_SHOP_BUY_ACTION_POINT_ACK pstOut = new SC_SHOP_BUY_ACTION_POINT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_BUY_PET_INFO_CHEAP_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_INFO_CHEAP_NTF pstIn = (SC_SHOP_BUY_PET_INFO_CHEAP_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_RemainTimeCheap))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_BUY_PET_INFO_CHEAP_NTF(ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_INFO_CHEAP_NTF pstOut = new SC_SHOP_BUY_PET_INFO_CHEAP_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_RemainTimeCheap))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF pstIn = (SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_RemainCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RemainTimeExpensive))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF(ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF pstOut = new SC_SHOP_BUY_PET_INFO_EXPENSIVE_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_RemainCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RemainTimeExpensive))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SHOP_BUY_PET_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_BUY_PET_REQ pstIn = (CS_SHOP_BUY_PET_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BuyType))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_BUY_PET_REQ(ref CNetData poNetData)
{
	CS_SHOP_BUY_PET_REQ pstOut = new CS_SHOP_BUY_PET_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BuyType))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_BUY_PET_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_ACK pstIn = (SC_SHOP_BUY_PET_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_TemplateID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_BUY_PET_ACK(ref CNetData poNetData)
{
	SC_SHOP_BUY_PET_ACK pstOut = new SC_SHOP_BUY_PET_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_BUY_PET_RESULT_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_TemplateID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_SHOP_GET_GOODSLIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_GET_GOODSLIST_REQ pstIn = (CS_SHOP_GET_GOODSLIST_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_GET_GOODSLIST_REQ(ref CNetData poNetData)
{
	CS_SHOP_GET_GOODSLIST_REQ pstOut = new CS_SHOP_GET_GOODSLIST_REQ();

	return pstOut;
}

public static	int		EncodeSGoodsInfo(CMessage msg,ref CNetData poNetData)
{
	SGoodsInfo pstIn = (SGoodsInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_GoodsId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Num))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSGoodsInfo(ref CNetData poNetData)
{
	SGoodsInfo pstOut = new SGoodsInfo();

	if(-1 == poNetData.Del(ref pstOut.m_GoodsId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Num))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_GET_GOODSLIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_GET_GOODSLIST_ACK pstIn = (SC_SHOP_GET_GOODSLIST_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_SHOP_GOODS_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_GOODS_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSGoodsInfo(pstIn.m_GOODSINFOS[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_GET_GOODSLIST_ACK(ref CNetData poNetData)
{
	SC_SHOP_GET_GOODSLIST_ACK pstOut = new SC_SHOP_GET_GOODSLIST_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_SHOP_GOODS_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_GOODS_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_GOODSINFOS[i]	= (SGoodsInfo)DecodeSGoodsInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_SHOP_BUY_GOODS_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_BUY_GOODS_REQ pstIn = (CS_SHOP_BUY_GOODS_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_GoodsId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GoodsCount))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_BUY_GOODS_REQ(ref CNetData poNetData)
{
	CS_SHOP_BUY_GOODS_REQ pstOut = new CS_SHOP_BUY_GOODS_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_GoodsId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GoodsCount))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_BUY_GOODS_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_BUY_GOODS_ACK pstIn = (SC_SHOP_BUY_GOODS_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if ((int)pstIn.m_ItemCount > (int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT || (int)pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		EncodeSGoodsInfo(pstIn.m_TemplateID[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_BUY_GOODS_ACK(ref CNetData poNetData)
{
	SC_SHOP_BUY_GOODS_ACK pstOut = new SC_SHOP_BUY_GOODS_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_BUY_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		pstOut.m_TemplateID[i]	= (SGoodsInfo)DecodeSGoodsInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_SHOP_SECRET_GOODS_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_SECRET_GOODS_REQ pstIn = (CS_SHOP_SECRET_GOODS_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_SECRET_GOODS_REQ(ref CNetData poNetData)
{
	CS_SHOP_SECRET_GOODS_REQ pstOut = new CS_SHOP_SECRET_GOODS_REQ();

	return pstOut;
}

public static	int		EncodeSSecretItemInfo(CMessage msg,ref CNetData poNetData)
{
	SSecretItemInfo pstIn = (SSecretItemInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Bought))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSSecretItemInfo(ref CNetData poNetData)
{
	SSecretItemInfo pstOut = new SSecretItemInfo();

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Bought))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_SECRET_GOODS_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_SECRET_GOODS_ACK pstIn = (SC_SHOP_SECRET_GOODS_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_RefreshRemainTimeMS))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FreeRefreshRemainCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ItemCount))
		return -1;

	int i;
	if ((int)pstIn.m_ItemCount > (int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT || (int)pstIn.m_ItemCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_ItemCount)
			break;
		EncodeSSecretItemInfo(pstIn.m_SecretItems[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_SECRET_GOODS_ACK(ref CNetData poNetData)
{
	SC_SHOP_SECRET_GOODS_ACK pstOut = new SC_SHOP_SECRET_GOODS_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_RefreshRemainTimeMS))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FreeRefreshRemainCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ItemCount))
		return null;

	int i;
	if ((int)pstOut.m_ItemCount > (int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT || (int)pstOut.m_ItemCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_SHOP_SECRET_ITEM_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_ItemCount)
			break;
		pstOut.m_SecretItems[i]	= (SSecretItemInfo)DecodeSSecretItemInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_SHOP_SECRET_BUY_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_SECRET_BUY_REQ pstIn = (CS_SHOP_SECRET_BUY_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_SECRET_BUY_REQ(ref CNetData poNetData)
{
	CS_SHOP_SECRET_BUY_REQ pstOut = new CS_SHOP_SECRET_BUY_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_SHOP_SECRET_BUY_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SHOP_SECRET_BUY_ACK pstIn = (SC_SHOP_SECRET_BUY_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SHOP_SECRET_BUY_ACK(ref CNetData poNetData)
{
	SC_SHOP_SECRET_BUY_ACK pstOut = new SC_SHOP_SECRET_BUY_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SHOP_SECRET_REFRESH_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SHOP_SECRET_REFRESH_REQ pstIn = (CS_SHOP_SECRET_REFRESH_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SHOP_SECRET_REFRESH_REQ(ref CNetData poNetData)
{
	CS_SHOP_SECRET_REFRESH_REQ pstOut = new CS_SHOP_SECRET_REFRESH_REQ();

	return pstOut;
}

public static	int		EncodeSC_VIP_INFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_VIP_INFO_NTF pstIn = (SC_VIP_INFO_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_VIPLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_GMLevel))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_VIP_INFO_NTF(ref CNetData poNetData)
{
	SC_VIP_INFO_NTF pstOut = new SC_VIP_INFO_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_VIPLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_GMLevel))
		return null;

	return pstOut;
}

public static	int		EncodeSC_VIP_RMBINFO_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_VIP_RMBINFO_NTF pstIn = (SC_VIP_RMBINFO_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_RMBCount))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_RMBSum))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_VIP_RMBINFO_NTF(ref CNetData poNetData)
{
	SC_VIP_RMBINFO_NTF pstOut = new SC_VIP_RMBINFO_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_RMBCount))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_RMBSum))
		return null;

	return pstOut;
}

public static	int		EncodeSC_VIP_MONTHCARD_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_VIP_MONTHCARD_NTF pstIn = (SC_VIP_MONTHCARD_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_MonthCardCount))
		return -1;

	int i;
	if ((int)pstIn.m_MonthCardCount > (int)HeaderProto.MAX_MONTHCARD_COUNT || (int)pstIn.m_MonthCardCount < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_MONTHCARD_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_MonthCardCount)
			break;
		HeaderProto.EncodeMonthCardInfo(pstIn.m_MonthCardInfos[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_VIP_MONTHCARD_NTF(ref CNetData poNetData)
{
	SC_VIP_MONTHCARD_NTF pstOut = new SC_VIP_MONTHCARD_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_MonthCardCount))
		return null;

	int i;
	if ((int)pstOut.m_MonthCardCount > (int)HeaderProto.MAX_MONTHCARD_COUNT || (int)pstOut.m_MonthCardCount < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_MONTHCARD_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_MonthCardCount)
			break;
		pstOut.m_MonthCardInfos[i]	= (HeaderProto.MonthCardInfo)HeaderProto.DecodeMonthCardInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeCS_VIP_MONTHCARD_GETITEM_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_VIP_MONTHCARD_GETITEM_REQ pstIn = (CS_VIP_MONTHCARD_GETITEM_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_UId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_VIP_MONTHCARD_GETITEM_REQ(ref CNetData poNetData)
{
	CS_VIP_MONTHCARD_GETITEM_REQ pstOut = new CS_VIP_MONTHCARD_GETITEM_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_UId))
		return null;

	return pstOut;
}

public static	int		EncodeSC_VIP_MONTHCARD_GETITEM_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_VIP_MONTHCARD_GETITEM_ACK pstIn = (SC_VIP_MONTHCARD_GETITEM_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_UId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_VIP_MONTHCARD_GETITEM_ACK(ref CNetData poNetData)
{
	SC_VIP_MONTHCARD_GETITEM_ACK pstOut = new SC_VIP_MONTHCARD_GETITEM_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_UId))
		return null;

	return pstOut;
}

public static	int		EncodeCS_SELF_PVP_PRO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_SELF_PVP_PRO_REQ pstIn = (CS_SELF_PVP_PRO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_SELF_PVP_PRO_REQ(ref CNetData poNetData)
{
	CS_SELF_PVP_PRO_REQ pstOut = new CS_SELF_PVP_PRO_REQ();

	return pstOut;
}

public static	int		EncodeSC_SELF_PVP_PRO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_SELF_PVP_PRO_ACK pstIn = (SC_SELF_PVP_PRO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_MilitaryLevel))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Repute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Times))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Wins))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Loses))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Ranks))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MiFlag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DayRepute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DayReFlag))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuyTimesLeft))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_SELF_PVP_PRO_ACK(ref CNetData poNetData)
{
	SC_SELF_PVP_PRO_ACK pstOut = new SC_SELF_PVP_PRO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_MilitaryLevel))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Repute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Times))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Wins))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Loses))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Ranks))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MiFlag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DayRepute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DayReFlag))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimesLeft))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_PVP_MATCH_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_PVP_MATCH_REQ pstIn = (CS_GET_PVP_MATCH_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_PVP_MATCH_REQ(ref CNetData poNetData)
{
	CS_GET_PVP_MATCH_REQ pstOut = new CS_GET_PVP_MATCH_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_PVP_MATCH_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_PVP_MATCH_ACK pstIn = (SC_GET_PVP_MATCH_ACK)msg;

		HeaderProto.EncodePVP_MATCH_LIST(pstIn.m_PVPMatchList,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_PVP_MATCH_ACK(ref CNetData poNetData)
{
	SC_GET_PVP_MATCH_ACK pstOut = new SC_GET_PVP_MATCH_ACK();

	pstOut.m_PVPMatchList	= (HeaderProto.PVP_MATCH_LIST)HeaderProto.DecodePVP_MATCH_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_ENTER_PVP_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ENTER_PVP_REQ pstIn = (CS_ENTER_PVP_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ENTER_PVP_REQ(ref CNetData poNetData)
{
	CS_ENTER_PVP_REQ pstOut = new CS_ENTER_PVP_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_ENTER_PVP_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ENTER_PVP_ACK pstIn = (SC_ENTER_PVP_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

		EncodeSC_SELF_BASE_PRO(pstIn.m_BasePro,ref poNetData);
		EncodeSC_USER_JOIN_PETS(pstIn.m_Pet,ref poNetData);
		EncodeSC_USER_SKILLS_NTF(pstIn.m_Skill,ref poNetData);
		EncodeSC_ROLE_EQUIP_NTF(pstIn.m_Equip,ref poNetData);
		HeaderProto.EncodeSRobotBuffs(pstIn.m_RobotBuff,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ENTER_PVP_ACK(ref CNetData poNetData)
{
	SC_ENTER_PVP_ACK pstOut = new SC_ENTER_PVP_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	pstOut.m_BasePro	= (SC_SELF_BASE_PRO)DecodeSC_SELF_BASE_PRO(ref poNetData);
	pstOut.m_Pet	= (SC_USER_JOIN_PETS)DecodeSC_USER_JOIN_PETS(ref poNetData);
	pstOut.m_Skill	= (SC_USER_SKILLS_NTF)DecodeSC_USER_SKILLS_NTF(ref poNetData);
	pstOut.m_Equip	= (SC_ROLE_EQUIP_NTF)DecodeSC_ROLE_EQUIP_NTF(ref poNetData);
	pstOut.m_RobotBuff	= (HeaderProto.SRobotBuffs)HeaderProto.DecodeSRobotBuffs(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_PVP_RETULT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PVP_RETULT_REQ pstIn = (CS_PVP_RETULT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Timeout))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PVP_RETULT_REQ(ref CNetData poNetData)
{
	CS_PVP_RETULT_REQ pstOut = new CS_PVP_RETULT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Timeout))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PVP_RETULT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PVP_RETULT_ACK pstIn = (SC_PVP_RETULT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

		EncodeSC_SELF_PVP_PRO_ACK(pstIn.m_Pro,ref poNetData);
	if(-1 == poNetData.Add(pstIn.m_Points))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Repute))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PVP_RETULT_ACK(ref CNetData poNetData)
{
	SC_PVP_RETULT_ACK pstOut = new SC_PVP_RETULT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	pstOut.m_Pro	= (SC_SELF_PVP_PRO_ACK)DecodeSC_SELF_PVP_PRO_ACK(ref poNetData);
	if(-1 == poNetData.Del(ref pstOut.m_Points))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Repute))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_PVP_MILITARY_REWARD_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_PVP_MILITARY_REWARD_REQ pstIn = (CS_GET_PVP_MILITARY_REWARD_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_PVP_MILITARY_REWARD_REQ(ref CNetData poNetData)
{
	CS_GET_PVP_MILITARY_REWARD_REQ pstOut = new CS_GET_PVP_MILITARY_REWARD_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_PVP_MILITARY_REWARD_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_PVP_MILITARY_REWARD_ACK pstIn = (SC_GET_PVP_MILITARY_REWARD_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_MiFlag))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_PVP_MILITARY_REWARD_ACK(ref CNetData poNetData)
{
	SC_GET_PVP_MILITARY_REWARD_ACK pstOut = new SC_GET_PVP_MILITARY_REWARD_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_MiFlag))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_PVP_REPUTE_REWARD_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_PVP_REPUTE_REWARD_REQ pstIn = (CS_GET_PVP_REPUTE_REWARD_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RewardId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_PVP_REPUTE_REWARD_REQ(ref CNetData poNetData)
{
	CS_GET_PVP_REPUTE_REWARD_REQ pstOut = new CS_GET_PVP_REPUTE_REWARD_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RewardId))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GET_PVP_REPUTE_REWARD_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_PVP_REPUTE_REWARD_ACK pstIn = (SC_GET_PVP_REPUTE_REWARD_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DayRepute))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DayReFlag))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_PVP_REPUTE_REWARD_ACK(ref CNetData poNetData)
{
	SC_GET_PVP_REPUTE_REWARD_ACK pstOut = new SC_GET_PVP_REPUTE_REWARD_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DayRepute))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DayReFlag))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_RANK_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_RANK_LIST_REQ pstIn = (CS_GET_RANK_LIST_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_RANK_LIST_REQ(ref CNetData poNetData)
{
	CS_GET_RANK_LIST_REQ pstOut = new CS_GET_RANK_LIST_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_RANK_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_RANK_LIST_ACK pstIn = (SC_GET_RANK_LIST_ACK)msg;

		HeaderProto.EncodePVP_RANK_LIST(pstIn.m_RankList,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_RANK_LIST_ACK(ref CNetData poNetData)
{
	SC_GET_RANK_LIST_ACK pstOut = new SC_GET_RANK_LIST_ACK();

	pstOut.m_RankList	= (HeaderProto.PVP_RANK_LIST)HeaderProto.DecodePVP_RANK_LIST(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_PVP_BUY_CHALLENGE_TIMES_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_PVP_BUY_CHALLENGE_TIMES_REQ pstIn = (CS_PVP_BUY_CHALLENGE_TIMES_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BuyTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_PVP_BUY_CHALLENGE_TIMES_REQ(ref CNetData poNetData)
{
	CS_PVP_BUY_CHALLENGE_TIMES_REQ pstOut = new CS_PVP_BUY_CHALLENGE_TIMES_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PVP_BUY_CHALLENGE_TIMES_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_PVP_BUY_CHALLENGE_TIMES_ACK pstIn = (SC_PVP_BUY_CHALLENGE_TIMES_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuyTimesLeft))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PVP_BUY_CHALLENGE_TIMES_ACK(ref CNetData poNetData)
{
	SC_PVP_BUY_CHALLENGE_TIMES_ACK pstOut = new SC_PVP_BUY_CHALLENGE_TIMES_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimesLeft))
		return null;

	return pstOut;
}

public static	int		EncodeSC_PVP_TIME_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_PVP_TIME_NTF pstIn = (SC_PVP_TIME_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Time))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_PVP_TIME_NTF(ref CNetData poNetData)
{
	SC_PVP_TIME_NTF pstOut = new SC_PVP_TIME_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Time))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_PVP_ROLE_INFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_PVP_ROLE_INFO_REQ pstIn = (CS_GET_PVP_ROLE_INFO_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_RoleID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_PVP_ROLE_INFO_REQ(ref CNetData poNetData)
{
	CS_GET_PVP_ROLE_INFO_REQ pstOut = new CS_GET_PVP_ROLE_INFO_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_RoleID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GET_PVP_ROLE_INFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_PVP_ROLE_INFO_ACK pstIn = (SC_GET_PVP_ROLE_INFO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Attack))
		return -1;

		EncodeSC_ROLE_EQUIP_NTF(pstIn.m_Equip,ref poNetData);
		EncodeSC_USER_JOIN_PETS(pstIn.m_Pet,ref poNetData);
		EncodeSC_USER_SKILLS_NTF(pstIn.m_Skill,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_PVP_ROLE_INFO_ACK(ref CNetData poNetData)
{
	SC_GET_PVP_ROLE_INFO_ACK pstOut = new SC_GET_PVP_ROLE_INFO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Attack))
		return null;

	pstOut.m_Equip	= (SC_ROLE_EQUIP_NTF)DecodeSC_ROLE_EQUIP_NTF(ref poNetData);
	pstOut.m_Pet	= (SC_USER_JOIN_PETS)DecodeSC_USER_JOIN_PETS(ref poNetData);
	pstOut.m_Skill	= (SC_USER_SKILLS_NTF)DecodeSC_USER_SKILLS_NTF(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GIFT_SIGN_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_SIGN_NTF pstIn = (SC_GIFT_SIGN_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_CanSign))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_SignCount))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_SIGN_NTF(ref CNetData poNetData)
{
	SC_GIFT_SIGN_NTF pstOut = new SC_GIFT_SIGN_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_CanSign))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_SignCount))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_SIGN_VIP_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_SIGN_VIP_NTF pstIn = (SC_GIFT_SIGN_VIP_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_GIFT_VIP_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_GIFT_VIP_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_Day[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_SIGN_VIP_NTF(ref CNetData poNetData)
{
	SC_GIFT_SIGN_VIP_NTF pstOut = new SC_GIFT_SIGN_VIP_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_GIFT_VIP_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_GIFT_VIP_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_Day[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_GIFT_SIGN(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_SIGN pstIn = (CS_GIFT_SIGN)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_SIGN(ref CNetData poNetData)
{
	CS_GIFT_SIGN pstOut = new CS_GIFT_SIGN();

	return pstOut;
}

public static	int		EncodeCS_GIFT_SIGN_VIP(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_SIGN_VIP pstIn = (CS_GIFT_SIGN_VIP)msg;

	if(-1 == poNetData.Add(pstIn.m_SignDate))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_SIGN_VIP(ref CNetData poNetData)
{
	CS_GIFT_SIGN_VIP pstOut = new CS_GIFT_SIGN_VIP();

	if(-1 == poNetData.Del(ref pstOut.m_SignDate))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_LEVEL_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_LEVEL_NTF pstIn = (SC_GIFT_LEVEL_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_GIFT_LEVEL_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_GIFT_LEVEL_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_Level[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_LEVEL_NTF(ref CNetData poNetData)
{
	SC_GIFT_LEVEL_NTF pstOut = new SC_GIFT_LEVEL_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_GIFT_LEVEL_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_GIFT_LEVEL_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_Level[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_GIFT_LEVEL(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_LEVEL pstIn = (CS_GIFT_LEVEL)msg;

	if(-1 == poNetData.Add(pstIn.m_Level))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_LEVEL(ref CNetData poNetData)
{
	CS_GIFT_LEVEL pstOut = new CS_GIFT_LEVEL();

	if(-1 == poNetData.Del(ref pstOut.m_Level))
		return null;

	return pstOut;
}

public static	int		EncodeSDayQuestInfo(CMessage msg,ref CNetData poNetData)
{
	SDayQuestInfo pstIn = (SDayQuestInfo)msg;

	if(-1 == poNetData.Add(pstIn.m_QuestId))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_FinishProgress))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_ScoreReceived))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSDayQuestInfo(ref CNetData poNetData)
{
	SDayQuestInfo pstOut = new SDayQuestInfo();

	if(-1 == poNetData.Del(ref pstOut.m_QuestId))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_FinishProgress))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_ScoreReceived))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_DAY_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_DAY_NTF pstIn = (SC_GIFT_DAY_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if ((int)pstIn.m_Count > (int)HeaderProto.MAX_DAY_QUEST_COUNT || (int)pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_DAY_QUEST_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		EncodeSDayQuestInfo(pstIn.m_Info[i],ref poNetData);
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_DAY_NTF(ref CNetData poNetData)
{
	SC_GIFT_DAY_NTF pstOut = new SC_GIFT_DAY_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_DAY_QUEST_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_DAY_QUEST_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		pstOut.m_Info[i]	= (SDayQuestInfo)DecodeSDayQuestInfo( ref poNetData);
	}

	return pstOut;
}

public static	int		EncodeSC_GIFT_DAY_UPD(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_DAY_UPD pstIn = (SC_GIFT_DAY_UPD)msg;

		EncodeSDayQuestInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_DAY_UPD(ref CNetData poNetData)
{
	SC_GIFT_DAY_UPD pstOut = new SC_GIFT_DAY_UPD();

	pstOut.m_Info	= (SDayQuestInfo)DecodeSDayQuestInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GIFT_DAY_SCORE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_DAY_SCORE_REQ pstIn = (CS_GIFT_DAY_SCORE_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_QuestId))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_DAY_SCORE_REQ(ref CNetData poNetData)
{
	CS_GIFT_DAY_SCORE_REQ pstOut = new CS_GIFT_DAY_SCORE_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_QuestId))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GIFT_DAY_BOX_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_DAY_BOX_REQ pstIn = (CS_GIFT_DAY_BOX_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BoxID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_DAY_BOX_REQ(ref CNetData poNetData)
{
	CS_GIFT_DAY_BOX_REQ pstOut = new CS_GIFT_DAY_BOX_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BoxID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_DAY_BOX_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_DAY_BOX_NTF pstIn = (SC_GIFT_DAY_BOX_NTF)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_DAY_QUEST_BOX_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_DAY_QUEST_BOX_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_BoxID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_DAY_BOX_NTF(ref CNetData poNetData)
{
	SC_GIFT_DAY_BOX_NTF pstOut = new SC_GIFT_DAY_BOX_NTF();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_DAY_QUEST_BOX_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_DAY_QUEST_BOX_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_BoxID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_GIFT_JIHUOMA_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_JIHUOMA_REQ pstIn = (CS_GIFT_JIHUOMA_REQ)msg;

	if(-1 == poNetData.AddString(pstIn.m_JiHuoMa))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_JIHUOMA_REQ(ref CNetData poNetData)
{
	CS_GIFT_JIHUOMA_REQ pstOut = new CS_GIFT_JIHUOMA_REQ();

	if(-1 == poNetData.DelString(ref pstOut.m_JiHuoMa))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_JIHUOMA_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_JIHUOMA_ACK pstIn = (SC_GIFT_JIHUOMA_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Ret))
		return -1;

		HeaderProto.EncodeSMultiItems(pstIn.m_Items,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_JIHUOMA_ACK(ref CNetData poNetData)
{
	SC_GIFT_JIHUOMA_ACK pstOut = new SC_GIFT_JIHUOMA_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Ret))
		return null;

	pstOut.m_Items	= (HeaderProto.SMultiItems)HeaderProto.DecodeSMultiItems(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GIFT_EP_INFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_EP_INFO_REQ pstIn = (CS_GIFT_EP_INFO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_EP_INFO_REQ(ref CNetData poNetData)
{
	CS_GIFT_EP_INFO_REQ pstOut = new CS_GIFT_EP_INFO_REQ();

	return pstOut;
}

public static	int		EncodeSC_GIFT_EP_INFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_EP_INFO_ACK pstIn = (SC_GIFT_EP_INFO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Count))
		return -1;

	int i;
	if((int)pstIn.m_Count > HeaderProto.MAX_FOODS_COUNT || pstIn.m_Count < 0)
       return -1;
	for(i = 0; i < (int)HeaderProto.MAX_FOODS_COUNT; ++i)
	{
		if(i >= (int)pstIn.m_Count)
			break;
		if(-1 == poNetData.Add(pstIn.m_DayFoodID[i]))
			return -1;
	}

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_EP_INFO_ACK(ref CNetData poNetData)
{
	SC_GIFT_EP_INFO_ACK pstOut = new SC_GIFT_EP_INFO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Count))
		return null;

	int i;
	if ((int)pstOut.m_Count > (int)HeaderProto.MAX_FOODS_COUNT || (int)pstOut.m_Count < 0)
       return null;
	for(i = 0; i < (int)HeaderProto.MAX_FOODS_COUNT; ++i)
	{
		if(i >= (int)pstOut.m_Count)
			break;
		if(-1 == poNetData.Del(ref pstOut.m_DayFoodID[i]))
			return null;
	}

	return pstOut;
}

public static	int		EncodeCS_GIFT_RECEIVE_EP_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GIFT_RECEIVE_EP_REQ pstIn = (CS_GIFT_RECEIVE_EP_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_DayFoodID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GIFT_RECEIVE_EP_REQ(ref CNetData poNetData)
{
	CS_GIFT_RECEIVE_EP_REQ pstOut = new CS_GIFT_RECEIVE_EP_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_DayFoodID))
		return null;

	return pstOut;
}

public static	int		EncodeSC_GIFT_RECEIVE_EP_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GIFT_RECEIVE_EP_ACK pstIn = (SC_GIFT_RECEIVE_EP_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_DayFoodID))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GIFT_RECEIVE_EP_ACK(ref CNetData poNetData)
{
	SC_GIFT_RECEIVE_EP_ACK pstOut = new SC_GIFT_RECEIVE_EP_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_DayFoodID))
		return null;

	return pstOut;
}

public static	int		EncodeCS_WB_INFO_REFRESH_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_WB_INFO_REFRESH_REQ pstIn = (CS_WB_INFO_REFRESH_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_WB_INFO_REFRESH_REQ(ref CNetData poNetData)
{
	CS_WB_INFO_REFRESH_REQ pstOut = new CS_WB_INFO_REFRESH_REQ();

	return pstOut;
}

public static	int		EncodeSC_WB_INFO_REFRESH_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_WB_INFO_REFRESH_ACK pstIn = (SC_WB_INFO_REFRESH_ACK)msg;

		HeaderProto.EncodeSWorldBossInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_INFO_REFRESH_ACK(ref CNetData poNetData)
{
	SC_WB_INFO_REFRESH_ACK pstOut = new SC_WB_INFO_REFRESH_ACK();

	pstOut.m_Info	= (HeaderProto.SWorldBossInfo)HeaderProto.DecodeSWorldBossInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_WB_ADD_BUF_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_WB_ADD_BUF_REQ pstIn = (CS_WB_ADD_BUF_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Type))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_WB_ADD_BUF_REQ(ref CNetData poNetData)
{
	CS_WB_ADD_BUF_REQ pstOut = new CS_WB_ADD_BUF_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Type))
		return null;

	return pstOut;
}

public static	int		EncodeSC_WB_ADD_BUF_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_WB_ADD_BUF_ACK pstIn = (SC_WB_ADD_BUF_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_ADD_BUF_ACK(ref CNetData poNetData)
{
	SC_WB_ADD_BUF_ACK pstOut = new SC_WB_ADD_BUF_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeCS_WB_RELIVE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_WB_RELIVE_REQ pstIn = (CS_WB_RELIVE_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_WB_RELIVE_REQ(ref CNetData poNetData)
{
	CS_WB_RELIVE_REQ pstOut = new CS_WB_RELIVE_REQ();

	return pstOut;
}

public static	int		EncodeSC_WB_RELIVE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_WB_RELIVE_ACK pstIn = (SC_WB_RELIVE_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_RELIVE_ACK(ref CNetData poNetData)
{
	SC_WB_RELIVE_ACK pstOut = new SC_WB_RELIVE_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	return pstOut;
}

public static	int		EncodeSC_WB_KILLED_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_WB_KILLED_NTF pstIn = (SC_WB_KILLED_NTF)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_KILLED_NTF(ref CNetData poNetData)
{
	SC_WB_KILLED_NTF pstOut = new SC_WB_KILLED_NTF();

	return pstOut;
}

public static	int		EncodeSC_WB_RUN_NTF(CMessage msg,ref CNetData poNetData)
{
	SC_WB_RUN_NTF pstIn = (SC_WB_RUN_NTF)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_RUN_NTF(ref CNetData poNetData)
{
	SC_WB_RUN_NTF pstOut = new SC_WB_RUN_NTF();

	return pstOut;
}

public static	int		EncodeCS_WB_ENTER_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_WB_ENTER_REQ pstIn = (CS_WB_ENTER_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_WB_ENTER_REQ(ref CNetData poNetData)
{
	CS_WB_ENTER_REQ pstOut = new CS_WB_ENTER_REQ();

	return pstOut;
}

public static	int		EncodeSC_WB_ENTER_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_WB_ENTER_ACK pstIn = (SC_WB_ENTER_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

		HeaderProto.EncodeSWorldBossInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_ENTER_ACK(ref CNetData poNetData)
{
	SC_WB_ENTER_ACK pstOut = new SC_WB_ENTER_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	pstOut.m_Info	= (HeaderProto.SWorldBossInfo)HeaderProto.DecodeSWorldBossInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_WB_RESULT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_WB_RESULT_REQ pstIn = (CS_WB_RESULT_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Damage))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_WB_RESULT_REQ(ref CNetData poNetData)
{
	CS_WB_RESULT_REQ pstOut = new CS_WB_RESULT_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Damage))
		return null;

	return pstOut;
}

public static	int		EncodeSC_WB_RESULT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_WB_RESULT_ACK pstIn = (SC_WB_RESULT_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

		HeaderProto.EncodeSWorldBossInfo(pstIn.m_Info,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_WB_RESULT_ACK(ref CNetData poNetData)
{
	SC_WB_RESULT_ACK pstOut = new SC_WB_RESULT_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	pstOut.m_Info	= (HeaderProto.SWorldBossInfo)HeaderProto.DecodeSWorldBossInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_ROLE_RANK_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_ROLE_RANK_REQ pstIn = (CS_ROLE_RANK_REQ)msg;

		HeaderProto.EncodeSRankRoleReq(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_ROLE_RANK_REQ(ref CNetData poNetData)
{
	CS_ROLE_RANK_REQ pstOut = new CS_ROLE_RANK_REQ();

	pstOut.m_Req	= (HeaderProto.SRankRoleReq)HeaderProto.DecodeSRankRoleReq(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_ROLE_RANK_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_ROLE_RANK_ACK pstIn = (SC_ROLE_RANK_ACK)msg;

		HeaderProto.EncodeSRankRoleAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_ROLE_RANK_ACK(ref CNetData poNetData)
{
	SC_ROLE_RANK_ACK pstOut = new SC_ROLE_RANK_ACK();

	pstOut.m_Ack	= (HeaderProto.SRankRoleAck)HeaderProto.DecodeSRankRoleAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GET_PT_BASEINFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_PT_BASEINFO_REQ pstIn = (CS_GET_PT_BASEINFO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_PT_BASEINFO_REQ(ref CNetData poNetData)
{
	CS_GET_PT_BASEINFO_REQ pstOut = new CS_GET_PT_BASEINFO_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_PT_BASEINFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_PT_BASEINFO_ACK pstIn = (SC_GET_PT_BASEINFO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Times))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Buys))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Pass))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_PT_BASEINFO_ACK(ref CNetData poNetData)
{
	SC_GET_PT_BASEINFO_ACK pstOut = new SC_GET_PT_BASEINFO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Times))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Buys))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Pass))
		return null;

	return pstOut;
}

public static	int		EncodeCS_BUY_PT_TIMES_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_BUY_PT_TIMES_REQ pstIn = (CS_BUY_PT_TIMES_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BuyTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_BUY_PT_TIMES_REQ(ref CNetData poNetData)
{
	CS_BUY_PT_TIMES_REQ pstOut = new CS_BUY_PT_TIMES_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSC_BUY_PT_TIMES_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_BUY_PT_TIMES_ACK pstIn = (SC_BUY_PT_TIMES_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuyTimesLeft))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_BUY_PT_TIMES_ACK(ref CNetData poNetData)
{
	SC_BUY_PT_TIMES_ACK pstOut = new SC_BUY_PT_TIMES_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimesLeft))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_MD_BASEINFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_MD_BASEINFO_REQ pstIn = (CS_GET_MD_BASEINFO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_MD_BASEINFO_REQ(ref CNetData poNetData)
{
	CS_GET_MD_BASEINFO_REQ pstOut = new CS_GET_MD_BASEINFO_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_MD_BASEINFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_MD_BASEINFO_ACK pstIn = (SC_GET_MD_BASEINFO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Times))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Buys))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Pass))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_MD_BASEINFO_ACK(ref CNetData poNetData)
{
	SC_GET_MD_BASEINFO_ACK pstOut = new SC_GET_MD_BASEINFO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Times))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Buys))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Pass))
		return null;

	return pstOut;
}

public static	int		EncodeCS_BUY_MD_TIMES_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_BUY_MD_TIMES_REQ pstIn = (CS_BUY_MD_TIMES_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BuyTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_BUY_MD_TIMES_REQ(ref CNetData poNetData)
{
	CS_BUY_MD_TIMES_REQ pstOut = new CS_BUY_MD_TIMES_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSC_BUY_MD_TIMES_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_BUY_MD_TIMES_ACK pstIn = (SC_BUY_MD_TIMES_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuyTimesLeft))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_BUY_MD_TIMES_ACK(ref CNetData poNetData)
{
	SC_BUY_MD_TIMES_ACK pstOut = new SC_BUY_MD_TIMES_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimesLeft))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GET_FD_BASEINFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GET_FD_BASEINFO_REQ pstIn = (CS_GET_FD_BASEINFO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GET_FD_BASEINFO_REQ(ref CNetData poNetData)
{
	CS_GET_FD_BASEINFO_REQ pstOut = new CS_GET_FD_BASEINFO_REQ();

	return pstOut;
}

public static	int		EncodeSC_GET_FD_BASEINFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GET_FD_BASEINFO_ACK pstIn = (SC_GET_FD_BASEINFO_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Times))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Buys))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_Pass))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GET_FD_BASEINFO_ACK(ref CNetData poNetData)
{
	SC_GET_FD_BASEINFO_ACK pstOut = new SC_GET_FD_BASEINFO_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Times))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Buys))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_Pass))
		return null;

	return pstOut;
}

public static	int		EncodeCS_BUY_FD_TIMES_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_BUY_FD_TIMES_REQ pstIn = (CS_BUY_FD_TIMES_REQ)msg;

	if(-1 == poNetData.Add(pstIn.m_BuyTimes))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_BUY_FD_TIMES_REQ(ref CNetData poNetData)
{
	CS_BUY_FD_TIMES_REQ pstOut = new CS_BUY_FD_TIMES_REQ();

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimes))
		return null;

	return pstOut;
}

public static	int		EncodeSC_BUY_FD_TIMES_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_BUY_FD_TIMES_ACK pstIn = (SC_BUY_FD_TIMES_ACK)msg;

	if(-1 == poNetData.Add(pstIn.m_Result))
		return -1;

	if(-1 == poNetData.Add(pstIn.m_BuyTimesLeft))
		return -1;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_BUY_FD_TIMES_ACK(ref CNetData poNetData)
{
	SC_BUY_FD_TIMES_ACK pstOut = new SC_BUY_FD_TIMES_ACK();

	if(-1 == poNetData.Del(ref pstOut.m_Result))
		return null;

	if(-1 == poNetData.Del(ref pstOut.m_BuyTimesLeft))
		return null;

	return pstOut;
}

public static	int		EncodeCS_GUILD_INFO_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_INFO_REQ pstIn = (CS_GUILD_INFO_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_INFO_REQ(ref CNetData poNetData)
{
	CS_GUILD_INFO_REQ pstOut = new CS_GUILD_INFO_REQ();

	return pstOut;
}

public static	int		EncodeCS_GUILD_LIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_LIST_REQ pstIn = (CS_GUILD_LIST_REQ)msg;

		HeaderProto.EncodeSGuildListReq(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_LIST_REQ(ref CNetData poNetData)
{
	CS_GUILD_LIST_REQ pstOut = new CS_GUILD_LIST_REQ();

	pstOut.m_Req	= (HeaderProto.SGuildListReq)HeaderProto.DecodeSGuildListReq(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_LIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_LIST_ACK pstIn = (SC_GUILD_LIST_ACK)msg;

		HeaderProto.EncodeSGuildListAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_LIST_ACK(ref CNetData poNetData)
{
	SC_GUILD_LIST_ACK pstOut = new SC_GUILD_LIST_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildListAck)HeaderProto.DecodeSGuildListAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_ENTER_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_ENTER_REQ pstIn = (CS_GUILD_ENTER_REQ)msg;

		HeaderProto.EncodeSGuildEnterReq(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_ENTER_REQ(ref CNetData poNetData)
{
	CS_GUILD_ENTER_REQ pstOut = new CS_GUILD_ENTER_REQ();

	pstOut.m_Req	= (HeaderProto.SGuildEnterReq)HeaderProto.DecodeSGuildEnterReq(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_ENTER_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_ENTER_ACK pstIn = (SC_GUILD_ENTER_ACK)msg;

		HeaderProto.EncodeSGuildEnterAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_ENTER_ACK(ref CNetData poNetData)
{
	SC_GUILD_ENTER_ACK pstOut = new SC_GUILD_ENTER_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildEnterAck)HeaderProto.DecodeSGuildEnterAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_CREATE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_CREATE_REQ pstIn = (CS_GUILD_CREATE_REQ)msg;

		HeaderProto.EncodeSGuildEnterAck(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_CREATE_REQ(ref CNetData poNetData)
{
	CS_GUILD_CREATE_REQ pstOut = new CS_GUILD_CREATE_REQ();

	pstOut.m_Req	= (HeaderProto.SGuildEnterAck)HeaderProto.DecodeSGuildEnterAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_CREATE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_CREATE_ACK pstIn = (SC_GUILD_CREATE_ACK)msg;

		HeaderProto.EncodeSGuildEnterAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_CREATE_ACK(ref CNetData poNetData)
{
	SC_GUILD_CREATE_ACK pstOut = new SC_GUILD_CREATE_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildEnterAck)HeaderProto.DecodeSGuildEnterAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_LEAVE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_LEAVE_REQ pstIn = (CS_GUILD_LEAVE_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_LEAVE_REQ(ref CNetData poNetData)
{
	CS_GUILD_LEAVE_REQ pstOut = new CS_GUILD_LEAVE_REQ();

	return pstOut;
}

public static	int		EncodeSC_GUILD_LEAVE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_LEAVE_ACK pstIn = (SC_GUILD_LEAVE_ACK)msg;

		HeaderProto.EncodeSGuildLeaveAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_LEAVE_ACK(ref CNetData poNetData)
{
	SC_GUILD_LEAVE_ACK pstOut = new SC_GUILD_LEAVE_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildLeaveAck)HeaderProto.DecodeSGuildLeaveAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_BASEINFO_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_BASEINFO_ACK pstIn = (SC_GUILD_BASEINFO_ACK)msg;

		HeaderProto.EncodeSGuildBaseInfo(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_BASEINFO_ACK(ref CNetData poNetData)
{
	SC_GUILD_BASEINFO_ACK pstOut = new SC_GUILD_BASEINFO_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildBaseInfo)HeaderProto.DecodeSGuildBaseInfo(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_LOG_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_LOG_REQ pstIn = (CS_GUILD_LOG_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_LOG_REQ(ref CNetData poNetData)
{
	CS_GUILD_LOG_REQ pstOut = new CS_GUILD_LOG_REQ();

	return pstOut;
}

public static	int		EncodeSC_GUILD_LOG_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_LOG_ACK pstIn = (SC_GUILD_LOG_ACK)msg;

		HeaderProto.EncodeSGuildLogAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_LOG_ACK(ref CNetData poNetData)
{
	SC_GUILD_LOG_ACK pstOut = new SC_GUILD_LOG_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildLogAck)HeaderProto.DecodeSGuildLogAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_CHANGE_NOTICE_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_CHANGE_NOTICE_REQ pstIn = (CS_GUILD_CHANGE_NOTICE_REQ)msg;

		HeaderProto.EncodeSGuildChangeNoticeReq(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_CHANGE_NOTICE_REQ(ref CNetData poNetData)
{
	CS_GUILD_CHANGE_NOTICE_REQ pstOut = new CS_GUILD_CHANGE_NOTICE_REQ();

	pstOut.m_Req	= (HeaderProto.SGuildChangeNoticeReq)HeaderProto.DecodeSGuildChangeNoticeReq(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_CHANGE_NOTICE_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_CHANGE_NOTICE_ACK pstIn = (SC_GUILD_CHANGE_NOTICE_ACK)msg;

		HeaderProto.EncodeSGuildChangeNoticeAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_CHANGE_NOTICE_ACK(ref CNetData poNetData)
{
	SC_GUILD_CHANGE_NOTICE_ACK pstOut = new SC_GUILD_CHANGE_NOTICE_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildChangeNoticeAck)HeaderProto.DecodeSGuildChangeNoticeAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_DETAIL_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_DETAIL_REQ pstIn = (CS_GUILD_DETAIL_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_DETAIL_REQ(ref CNetData poNetData)
{
	CS_GUILD_DETAIL_REQ pstOut = new CS_GUILD_DETAIL_REQ();

	return pstOut;
}

public static	int		EncodeSC_GUILD_DETAIL_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_DETAIL_ACK pstIn = (SC_GUILD_DETAIL_ACK)msg;

		HeaderProto.EncodeSGuildDetailAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_DETAIL_ACK(ref CNetData poNetData)
{
	SC_GUILD_DETAIL_ACK pstOut = new SC_GUILD_DETAIL_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildDetailAck)HeaderProto.DecodeSGuildDetailAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_ENTERLIST_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_ENTERLIST_REQ pstIn = (CS_GUILD_ENTERLIST_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_ENTERLIST_REQ(ref CNetData poNetData)
{
	CS_GUILD_ENTERLIST_REQ pstOut = new CS_GUILD_ENTERLIST_REQ();

	return pstOut;
}

public static	int		EncodeSC_GUILD_ENTERLIST_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_ENTERLIST_ACK pstIn = (SC_GUILD_ENTERLIST_ACK)msg;

		HeaderProto.EncodeSGuildEnterListAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_ENTERLIST_ACK(ref CNetData poNetData)
{
	SC_GUILD_ENTERLIST_ACK pstOut = new SC_GUILD_ENTERLIST_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildEnterListAck)HeaderProto.DecodeSGuildEnterListAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_ENTER_ACCEPT_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_ENTER_ACCEPT_REQ pstIn = (CS_GUILD_ENTER_ACCEPT_REQ)msg;

		HeaderProto.EncodeSGuildEnterAcceptReq(pstIn.m_Req,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_ENTER_ACCEPT_REQ(ref CNetData poNetData)
{
	CS_GUILD_ENTER_ACCEPT_REQ pstOut = new CS_GUILD_ENTER_ACCEPT_REQ();

	pstOut.m_Req	= (HeaderProto.SGuildEnterAcceptReq)HeaderProto.DecodeSGuildEnterAcceptReq(ref poNetData);
	return pstOut;
}

public static	int		EncodeSC_GUILD_ENTER_ACCEPT_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_ENTER_ACCEPT_ACK pstIn = (SC_GUILD_ENTER_ACCEPT_ACK)msg;

		HeaderProto.EncodeSGuildEnterAcceptAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_ENTER_ACCEPT_ACK(ref CNetData poNetData)
{
	SC_GUILD_ENTER_ACCEPT_ACK pstOut = new SC_GUILD_ENTER_ACCEPT_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildEnterAcceptAck)HeaderProto.DecodeSGuildEnterAcceptAck(ref poNetData);
	return pstOut;
}

public static	int		EncodeCS_GUILD_ENTER_CLEAR_REQ(CMessage msg,ref CNetData poNetData)
{
	CS_GUILD_ENTER_CLEAR_REQ pstIn = (CS_GUILD_ENTER_CLEAR_REQ)msg;

	return poNetData.GetDataLen();
}

public	static	CMessage DecodeCS_GUILD_ENTER_CLEAR_REQ(ref CNetData poNetData)
{
	CS_GUILD_ENTER_CLEAR_REQ pstOut = new CS_GUILD_ENTER_CLEAR_REQ();

	return pstOut;
}

public static	int		EncodeSC_GUILD_ENTER_CLEAR_ACK(CMessage msg,ref CNetData poNetData)
{
	SC_GUILD_ENTER_CLEAR_ACK pstIn = (SC_GUILD_ENTER_CLEAR_ACK)msg;

		HeaderProto.EncodeSGuildEnterClearAck(pstIn.m_Ack,ref poNetData);
	return poNetData.GetDataLen();
}

public	static	CMessage DecodeSC_GUILD_ENTER_CLEAR_ACK(ref CNetData poNetData)
{
	SC_GUILD_ENTER_CLEAR_ACK pstOut = new SC_GUILD_ENTER_CLEAR_ACK();

	pstOut.m_Ack	= (HeaderProto.SGuildEnterClearAck)HeaderProto.DecodeSGuildEnterClearAck(ref poNetData);
	return pstOut;
}

}
