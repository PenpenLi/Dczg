using UnityEngine;
using System.Collections;

public class DropoutButton : MonoBehaviour 
{
	public string	m_ItemID;
	public int		m_ItemType;		// 0普通装备 1宠物 2宠物装备..

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
		if( m_ItemID == "" ) return;
		
		if( m_ItemType == 0 )
		{
			sdUICharacter.Instance.ShowTip(TipType.TempItem, m_ItemID);
		}
		else if( m_ItemType == 1 )
		{
			Hashtable itemInfo = sdConfDataMgr.Instance().GetItemById(m_ItemID);
			if (itemInfo != null)
				sdUIPetControl.Instance.ActivePetSmallTip(null, int.Parse(itemInfo["Expend"].ToString()), 0, 1);
		}
		else if( m_ItemType == 2 )
		{
			sdUIPetControl.Instance.ActivePetEquipTip(null, int.Parse(m_ItemID));
		}
	}
}
