using UnityEngine;
using System.Collections;

/// <summary>
/// 旋转指定对象(目前只支持鼠标)
/// </summary>
public class SpinObject : MonoBehaviour 
{
	public sdLoginChar m_Target;	//< 变换目标对象aa
	public float m_Speed;		//< 变化速率aa
	
	private void Start()
	{

	}

	void Update () 
	{

	}

	// 需要注意OnDrag事件只有在挂有Box Collider的对象上才会被调用aa
	void OnDrag(Vector2 kDelta)
	{
		if( m_Target && m_Target.IsShowAnimPlaying()==false )
		{
			m_Target.gameObject.transform.Rotate(new Vector3(0, kDelta.x, 0) * m_Speed);
		}
	}
}
