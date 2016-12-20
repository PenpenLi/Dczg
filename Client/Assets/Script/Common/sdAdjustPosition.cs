using UnityEngine;

public class sdAdjustPosition : MonoBehaviour
{
	public Transform parent = null;
	public Vector3   posOffset = Vector3.zero;
	
	void Update () {
		if(parent != null && gameObject != null)
			gameObject.transform.position = parent.transform.position + posOffset;
	}
}