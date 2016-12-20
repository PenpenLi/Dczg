using UnityEngine;
using System.Collections.Generic;

public class sdLevelItem : MonoBehaviour
{
	public int BattleID = 0;
	
	private GameObject dragPanel = null;
	
	void Start()
	{
		dragPanel = GameObject.Find("dragpanel");
	}

	public List<EventDelegate> onClick = new List<EventDelegate>();

	void OnClick()
	{
		if( GameObject.Find("$TownUi") == null ) return;	// 如果找不到TownUi说明在播放开启战役动画，则不可用点击战役.
        if (GameObject.Find("moviedialogue(Clone)") != null) return;

		// 播放点击音效。
		GameObject btn = GameObject.Find("btn_townset");
		if( btn )
		{
			AudioClip au = btn.GetComponent<UIPlaySound>().audioClip;
			if( au )
				NGUITools.PlaySound(au);
		}
		
		int LevelID = BattleID * 1000 + 11;
		bool LevelValid = false;
		if( BattleID == 21 )
		{
			// 等待主城解锁，不可进入下个推图关卡..
			if( sdGlobalDatabase.Instance.globalData.ContainsKey("OpenLevel_MainCity") 
			   && (int)sdGlobalDatabase.Instance.globalData["OpenLevel_MainCity"]==1 )							
				LevelID = 99999;
		}
		if( BattleID == 0 ) LevelID = 21011;	// 主城的解锁看第二章第一个战役第一个关卡是否解锁.
		for(int i=0;i<sdLevelInfo.levelInfos.Length;i++)
		{
			if( sdLevelInfo.levelInfos[i].levelID == LevelID )
			{
				LevelValid = sdLevelInfo.levelInfos[i].valid;
				break;							
			}
		}
		
		if( BattleID == 0 )
		{
			if( LevelValid )
			{
				sdUICharacter.JumpToMainCity();
			}
			else
			{
				sdUICharacter.Instance.ShowMsgLine( sdConfDataMgr.Instance().GetShowStr("MainCityError") , MSGCOLOR.Yellow );
			}
		}
		else
		{
            if (LevelValid)
            {
                if (BattleID >= 31)	// 临时处理..
                    sdUICharacter.Instance.ShowMsgLine("抱歉，本次测试暂不开放此战役。", MSGCOLOR.Yellow);
                else
                {
                    Vector3 sp = sdGameLevel.instance.mainCamera.GetComponent<Camera>().WorldToScreenPoint(gameObject.transform.localPosition);
                    float a = 1280f / Screen.width;
                    float x = (sp.x - Screen.width / 2f) * a;
                    float y = (sp.y - Screen.height / 2f) * a;

                    sdUICharacter.Instance.ShowTuitu(BattleID, false, x, y);
                }
            }
            else
            {
                if (BattleID == 21 && sdLevelInfo.GetLevelValid(14061))
                {
                    sdUICharacter.Instance.ShowMsgLine(sdConfDataMgr.Instance().GetShowStr("NeedInMainCity"), MSGCOLOR.Yellow);
                }
                else
                {
                    sdUICharacter.Instance.ShowMsgLine("通关前一战役普通模式所有关卡，就可解锁此战役。", MSGCOLOR.Yellow);
                }
            }
				
		}

		EventDelegate.Execute(onClick);
		onClick.Clear();
	}
	
	void Update()
	{
		if (UICamera.hoveredObject != null && UICamera.hoveredObject != dragPanel) return;
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = sdGameLevel.instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == gameObject)
				{	
					sdUICharacter.Instance.SetNeedShowLevel(gameObject);
				}
			}
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Ray ray = sdGameLevel.instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == gameObject && 
					sdUICharacter.Instance.GetNeedShowLevel() == gameObject)
				{	
					OnClick();
				}
			}
		}
	}
}