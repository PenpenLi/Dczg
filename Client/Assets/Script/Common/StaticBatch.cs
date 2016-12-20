using UnityEngine;
using System.Collections;

public class StaticBatch : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StaticBatchingUtility.Combine(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
