using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 显示角色周围的Hexagon网格aa
/// </summary>
public class HexagonDebugger : MonoBehaviour
{
	protected sdGameActor mActor = null;
	protected List<GameObject> mElementObjectList = new List<GameObject>();
	
	// 显示权重调试信息aa
//	public bool showStaticWeight = false;
	public bool showDynamicWeight = false;

	// 虚函数(继承自MonoBehaviour)
	void Start()
	{
		mActor = gameObject.GetComponent<sdGameActor>();
		if (mActor == null)
			mActor = gameObject.GetComponent<sdGameMonster>();

		Hexagon.Coord kCoord = new Hexagon.Coord();
		for (int i = 0; i < 37; ++i )
			mElementObjectList.Add(HexagonElement.NewElement(kCoord, 0, 0.0f, 3));
	}

	// 虚函数(继承自MonoBehaviour)
	void Update()
	{
		if (mActor == null)
			return;

		List<Hexagon.Coord> kCoordList = new List<Hexagon.Coord>();
		ushort usValue = 0;
		if (showDynamicWeight)
		{
			Hexagon.Coord kCoord = new Hexagon.Coord();
			if (Hexagon.Manager.GetSingleton().Position_Coord(this.transform.position, ref kCoord, ref usValue))
            {
				Hexagon.Manager.GetSingleton().GetNeighbourCoord(kCoord, 0, ref kCoordList);
				Hexagon.Manager.GetSingleton().GetNeighbourCoord(kCoord, 1, ref kCoordList);
				Hexagon.Manager.GetSingleton().GetNeighbourCoord(kCoord, 2, ref kCoordList);
				Hexagon.Manager.GetSingleton().GetNeighbourCoord(kCoord, 3, ref kCoordList);
            }
		}

		if (showDynamicWeight)
		{
			for (int i = 0; i < kCoordList.Count; ++i)
			{
				if (i > mElementObjectList.Count)
					break;

				Hexagon.Coord kCoord = kCoordList[i]
;				Vector3 kPosition = Hexagon.Manager.GetSingleton().Coord_Position(kCoord, usValue);

				mElementObjectList[i].active = true;
				mElementObjectList[i].transform.position = kPosition;
				mElementObjectList[i].GetComponent<HexagonElement>().hexagonCoord = kCoord;
			}
		}
		else
		{
			foreach (GameObject kElementObject in mElementObjectList)
			{
				kElementObject.active = false;
			}
		}
	}
}