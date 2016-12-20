using UnityEngine;
using System.Collections;

public class sdGameSkill : object
{
	public int templateID = -1;
	public	int	coolDown	=	0;
	public 	int line = -1;
	public 	int index = -1;
	public int classId = -1;
	public int nextlv = -1;
	public bool isPassive = false;
	
	public int level = 1;
	
	public string icon
	{
		get{
			return (sdConfDataMgr.Instance().GetSkill(templateID.ToString()))["icon"].ToString();	
		}
	}
}

