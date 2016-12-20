using UnityEngine;
using System.Collections;

public class zhangjieClose : MonoBehaviour 
{	
	public GameObject	m_wndCampaign	= null;
	
	
	void OnClick()
	{
		if( gameObject.name == "btn_close" )
		{
			if( Application.loadedLevelName=="$worldmap_0" || Application.loadedLevelName=="$mainCity_1" )
			{	// 在城镇中...
				sdUICharacter.Instance.HideTuitu(false);

				//if( m_wndCampaign ) m_wndCampaign.SetActive(false);
				//sdUICharacter.Instance.ShowFullScreenUI(false);
			}
			else
			{	// 在关卡中...
				GameObject obj = GameObject.Find("Fx_lingjing01");
				if( obj != null ) obj.SetActive(false);
				obj = GameObject.Find("Fx_lingjing02");
				if( obj != null ) obj.SetActive(false);
				obj = GameObject.Find("Fx_lingjing03");
				if( obj != null ) obj.SetActive(false);
				
				sdUICharacter.Instance.TuiTu_To_WorldMap();
			}
		}
		else if( gameObject.name == "btn_rewardclose" )	
		{
			WndAni.HideWndAni(m_wndCampaign,false,"sp_grey");
			//m_wndCampaign.SetActive(false);
		}
		else if( gameObject.name == "btn_fpetclose" )	
		{
			WndAni.HideWndAni(m_wndCampaign,false,"sp_grey");
			//m_wndCampaign.SetActive(false);
		}
	}
}
