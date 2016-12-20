using UnityEngine;
using System.Collections;

public class ScalePetView : MonoBehaviour
{
	public GameObject m_petGrey = null;
	public GameObject m_petIcon = null;
	public float m_beginTime = 0;	
	public float m_currScale = 1.0f;
	
	void Update()
	{
		if(Time.time >=m_beginTime + 1.5)
		{
			if(m_currScale<=0.001)
			{
				m_petIcon.SetActive(true);
				
				if(m_petIcon.name == "Item9")
					m_petGrey.SetActive(false);
				
				return;
			}
				
			m_currScale -= Time.deltaTime;
			this.transform.localScale *= m_currScale;
			
		}
	}
}
