using UnityEngine;
using System.Collections;

[AddComponentMenu("SNDA/GameLogic/TriggerReceiver/LevelDoor")]
public class sdLevelDoor : sdTriggerReceiver {
	
	public GameObject DoorObject;
	
	public GameObject[] Obstacles;
	
	public bool bornActive = false;
	
	// Use this for initialization
	void Start () {
		if(!bornActive)
		{
			DoorObject.SetActive(false);
			
			foreach(GameObject obs in Obstacles)
			{
				obs.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnTriggerHitted (GameObject obj,int[] param)
	{
		base.OnTriggerHitted (obj,param);
		
		//现在没资源，不确定出现消失是怎样的表现，先用马上出现，马上消失...
		
		if(param[3] == 0)
		{
			DoorObject.SetActive(false);
			foreach(GameObject obs in Obstacles)
			{
				obs.SetActive(false);
			}
		}
		else
		{
			DoorObject.SetActive(true);
			foreach(GameObject obs in Obstacles)
			{
				obs.SetActive(true);
			}
		}
	}
}
