using UnityEngine;
using System.Collections;
using System;

public class sdLoginMsg : MonoBehaviour
{
	public UILabel txt = null;

	void Start()
	{
		sdUICharacter.Instance.SetLoginMsg(this);
		gameObject.SetActive(false);
	}
}
