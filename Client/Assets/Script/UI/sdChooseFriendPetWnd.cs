
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

class sdChooseFriendPetWnd : MonoBehaviour
{
	public GameObject copyItem;
	public GameObject dragPanel = null;
	
	
	void Refresh()
	{
		Hashtable table = sdFriendMgr.Instance.GetFightList();
		int num = table.Count;
		List<sdFriPetChooseBtn> btnList = new List<sdFriPetChooseBtn>();
		sdFriPetChooseBtn[] list = GetComponentsInChildren<sdFriPetChooseBtn>();
		foreach(sdFriPetChooseBtn btn in list)
		{
			btnList.Add(btn);
		}
		if (list.Length < 15)
		{
			for (int i = list.Length; i < num; ++i)
			{
				GameObject tempItem = GameObject.Instantiate(copyItem) as GameObject;
				tempItem.transform.parent = dragPanel.transform;
				tempItem.transform.localPosition = copyItem.transform.localPosition;
				tempItem.transform.localScale = copyItem.transform.localScale;
				Vector3 pos = tempItem.transform.localPosition;
				pos.y = pos.y - (120 * i);
				tempItem.transform.localPosition = pos;
				btnList.Add(tempItem.GetComponent<sdFriPetChooseBtn>());
			}
		}
		
		IEnumerator iter = btnList.GetEnumerator();
		foreach(DictionaryEntry item in table)
		{
			sdFriend info = item.Value as sdFriend;
			if (info == null) continue;
			if (iter.MoveNext())
			{
				sdFriPetChooseBtn btn = iter.Current as sdFriPetChooseBtn;
				btn.SetInfo(info);
				btn.gameObject.SetActive(true);
			}
		}
		
		while (iter.MoveNext())
		{
			sdFriPetChooseBtn btn = iter.Current as sdFriPetChooseBtn;
			btn.SetInfo(null);	
		}	
	}
	
	void Start()
	{
		Refresh();
	}
}