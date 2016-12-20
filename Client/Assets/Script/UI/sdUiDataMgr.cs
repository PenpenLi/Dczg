using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class sdUidataMgr
{
	Hashtable roleInfo = new Hashtable();
	Hashtable itemInfo = new Hashtable();
	Hashtable skillInfo = new Hashtable();
	
	public void SetRoleInfo(string key, Hashtable info)
	{
		roleInfo[key] = info.Clone();
	}
	
	public void SetItemInfo()
	{
		
	}
	
	public void SetSkillInfo()
	{
		
	}
	
	public void RefreshRoleInfo()
	{
		foreach(DictionaryEntry item in roleInfo)
		{
			sdUICharacter.Instance.ShowProperty(item.Key.ToString(), item.Value as Hashtable);	
		}
	}
	
	public void RefreshItem()
	{
		
	}
	
	public void RefreshSkill()
	{
		
	}
}