using UnityEngine;
using System.Collections;

public class ChooseBuff : MonoBehaviour 
{
	public ReadyButton			m_StartLevelButton;
	public GameObject			m_SelectPic;
	
	static uint					m_BuffCost1		= 1000;
	static uint					m_BuffCost2		= 1000;
	static uint					m_BuffCost3		= 10;
	static uint					m_BuffCost4		= 10;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnClick()
	{
		if( gameObject.name == "bt_cost1" || gameObject.name == "bt_buff1" )
		{
			if( (uint)(sdGameLevel.instance.mainChar.Property["NonMoney"]) < m_BuffCost1 )
				sdUICharacter.Instance.ShowOkMsg("您的游戏币不足。",null);
			else
				m_StartLevelButton.ChooseBuff(0);
		}
		else if( gameObject.name == "bt_cost2" || gameObject.name == "bt_buff2" )
		{
			if( (uint)sdGameLevel.instance.mainChar.Property["NonMoney"] < m_BuffCost2 )
				sdUICharacter.Instance.ShowOkMsg("您的游戏币不足。",null);
			else
				m_StartLevelButton.ChooseBuff(1);
		}
		else if( gameObject.name == "bt_cost3" || gameObject.name == "bt_buff3" )
		{
			if( ((uint)sdGameLevel.instance.mainChar.Property["NonCash"]+(uint)sdGameLevel.instance.mainChar.Property["Cash"]) < m_BuffCost3 )
				sdUICharacter.Instance.ShowOkMsg("您的勋章不足。",null);
			else
				m_StartLevelButton.ChooseBuff(2);
		}
		else if( gameObject.name == "bt_cost4" || gameObject.name == "bt_buff4" )
		{
			if( ((uint)sdGameLevel.instance.mainChar.Property["NonCash"]+(uint)sdGameLevel.instance.mainChar.Property["Cash"]) < m_BuffCost4 )
				sdUICharacter.Instance.ShowOkMsg("您的勋章不足。",null);
			else
				m_StartLevelButton.ChooseBuff(3);
		}
	}
}
