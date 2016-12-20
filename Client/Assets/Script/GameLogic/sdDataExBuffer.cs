using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public struct EquipInfo
{
	public string filename;
	public string partname;
	public string dummyname;
}
public class sdDataExBuffer : object
{
	private static sdDataExBuffer instance = null;
	public static sdDataExBuffer inst() 
	{
		if(instance == null)
		{
			instance = new sdDataExBuffer();
			bool result = instance.init();
			if(!result)
			{
				instance.finl();
				instance = null;
			}			
		}
		return instance;
	}
	public static CliProto.SC_SELF_BASE_PRO mainCharServerData = null;
	public static CliProto.SC_SELF_ITEM_NTF mainItemData = null;
	public static CliProto.SC_SELF_ITEM_NTF mainEquipData = null;
	public static CliProto.SC_SELF_ITEM_NTF mainWarehouseData = null;
	public static CliProto.SC_MOVE_ITEM_ACK mainMoveData = null;
	public static List<EquipInfo> mainEquipInfo = new List<EquipInfo>();
	public bool init() 
	{
		return true;
	}
	
	public void finl()
	{
	}
}
