using UnityEngine;
using System.Collections.Generic;

public class sdMaskClick : MonoBehaviour
{
	public List<EventDelegate> onClick = new List<EventDelegate>();

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = sdGameLevel.instance.mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if(hit.collider.gameObject == gameObject)
				{	
					EventDelegate.Execute(onClick);
					onClick.Clear();
				}
			}
		}
//		else if (Input.GetMouseButtonUp(0))
//		{
//			Ray ray = sdGameLevel.instance.mainCamera.camera.ScreenPointToRay(Input.mousePosition);
//			RaycastHit hit;
//			if (Physics.Raycast(ray, out hit))
//			{
//				if(hit.collider.gameObject == gameObject)
//				{	
//					EventDelegate.Execute(onClick);
//					onClick.Clear();
//				}
//			}
//		}
	}
}
