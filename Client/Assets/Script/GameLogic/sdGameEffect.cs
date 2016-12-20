using UnityEngine;
using System.Collections;

public class sdGameEffect : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	public bool isAniFinished()
	{
		return gameObject.transform.childCount == 0;
	}
}
