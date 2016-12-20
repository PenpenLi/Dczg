
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class sdFriend : IComparable
{
	public string id;
	public string name;
	public string job;
	public string level;
	public string power;
	public bool isOnline;
	public bool canSend;
    public int pvpwin;
    public int pvprepute;
	
	public byte gender;
	public byte hairStyle;
	public byte color;
	public int point;
	public int index = 0;
	
	public bool isFri = true;
	
	public SClientPetInfo petInfo = new SClientPetInfo();
	public List<uint> equipList = new List<uint>();
    public List<SClientPetInfo> petList = new List<SClientPetInfo>();
	
	public int CompareTo(object obj)
	{
		sdFriend fri = obj as sdFriend;
		if (fri.index > index)
		{
			return -1;	
		}
		else if (fri.index < index)
		{
			return 1;	
		}
		else
		{
			return 0;	
		}
	}
}