using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdFriDragPanel : MonoBehaviour
{
	void OnFinish()
	{
		if (GetComponent<UIDraggablePanel>().verticalScrollBar.value == 1 && sdFriendMgr.Instance.isReceive)
		{
			sdFriendMgr.Instance.curPage++;
			sdFriendMsg.notifyQueryRole(sdUICharacter.Instance.GetSearchFriName(), sdFriendMgr.Instance.curPage);
		}
	}
	
	void Start()
	{
		GetComponent<UIDraggablePanel>().onDragFinished += new UIDraggablePanel.OnDragFinished(OnFinish);
	}
}
