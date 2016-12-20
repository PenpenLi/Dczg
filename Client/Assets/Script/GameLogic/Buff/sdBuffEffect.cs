using UnityEngine;
using System.Collections;
/*
 *   struct SBuffEffect ///byEffectType,byEffectExplain,dwEffectID,dwEffectData,dwPeriodicData
{
	UINT8   byEffectType;        ///[0]效果生效类型,见EBuffEffectType
	UINT8   byEffectExplain;     ///[1]
	///效果附加数据
	///周期性效果,周期性效果类型EPeriodicDamage
	///结束性效果,表示结束类型,见EBuffEndType 
	///触发性效果,表示作用与自己(0) 还是触发者(1) 还是作用buff释放者(2)  还是buff 的aoe对象(3)
	///层数叠满时效果,无意义
	UINT32  dwOperationID;         ///[2]BUFF效果ID
	UINT32  dwOperationData;       ///[3]BUFF效果参数
	UINT32  dwOperationData1;      ///[4]BUFF效果参数
	UINT32  dwPeriodicData;     ///[5]周期生效参数（固定值或者万分比）,触发性效果表示该效果触发的概率为%  
	///结束生效：被驱散生效：0代表对自己的效果，1代表对驱散者的效果2代表对释放者的效果，释放者可能找不到
	///          其他生效：0代表对自己的效果，1代表对释放者的效果，释放者可能找不到
	UINT32  dwEXData; //额外数据，在修正技能时使用
};


BUFF_EFFECT_NONE          = 0,
	BUFF_EFFECT_START,
	BUFF_EFFECT_PERIODIC,
	BUFF_EFFECT_END,
	BUFF_EFFECT_SPRING,
	BUFF_EFFECT_FULL_LAYER,
	BUFF_EFFECT_MOD_PRO,
	BUFF_EFFECT_MOD_SKILL_PRO,
	
*/
public	class sdBuffEffect
{
	public	int	effectID;
	public	int	byEffectType;
	public	int	byEffectExplain;
	public	int	dwOperationID;
	public	int	dwOperationData;
	public	int	dwOperationData1;
	public	int	dwPeriodicData;
	public	int	dwEXData;
	
	public static	bool	Add(sdActorInterface castActor,sdActorInterface target,Hashtable buffeffect,HeaderProto.EDoOperationType doType,int layer)
	{

		sdEffectOperation.OpParameter param = new sdEffectOperation.OpParameter();
		param.id			=	(int)buffeffect["dwOperationID"];
		param.data			=	(int)buffeffect["dwOperationData"];
		param.data1			=	(int)buffeffect["dwOperationData1"];
		param.data2         =   0;
		param.layer			=	layer;
		param.doType		=	(int)doType;
		param.attackActor	=	castActor;
		param.targetActor	=	target;
		param.trans			=	target.transform;

		return sdEffectOperation.Do(target,param);
	}
}