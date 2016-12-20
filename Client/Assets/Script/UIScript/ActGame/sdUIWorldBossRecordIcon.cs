using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class sdUIWorldBossRecordIcon : MonoBehaviour
{
	int m_iRank = 0;
	string m_strName = "";
	int m_iDamage = 0;
	
	public GameObject bg = null;
	public GameObject lbRank = null;
	public GameObject lbName = null;
	public GameObject lbDamage = null;
	
	void Update()
	{
	}
	
	void OnClick()
	{
	}
	
	public void SetIdAndReflashUI(int rank, string strName, int damage)
	{
		if (rank==0) 
		{
			m_iRank = 0;
			m_strName = "";
			m_iDamage = 0;
			gameObject.SetActive(false);
			return;
		}
		
		gameObject.SetActive(true);
		m_iRank = rank;
		m_strName = strName;
		m_iDamage = damage;

		int iYS = m_iRank%2;
		if (iYS>0)
		{
			if (bg) bg.SetActive(false);
		}
		else
		{
			if (bg) bg.SetActive(true);
		}

		if (lbRank)
			lbRank.GetComponent<UILabel>().text = m_iRank.ToString();

		if (lbName)
			lbName.GetComponent<UILabel>().text = m_strName;

		if (lbDamage)
			lbDamage.GetComponent<UILabel>().text = m_iDamage.ToString();
	}
}
