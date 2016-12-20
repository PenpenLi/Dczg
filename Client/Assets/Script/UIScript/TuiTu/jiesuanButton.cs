using UnityEngine;
using System.Collections;

public class jiesuanButton : MonoBehaviour {
	
	float lifetime = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifetime += Time.deltaTime;
		if (lifetime >= 30)
		{
			OnClick();
		}
	}
	
	void OnClick()
	{

	}
}
