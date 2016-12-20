using UnityEngine;
using System.Collections;

public class DragCenter : MonoBehaviour 
{


	public int m_offsetX = 0;
	public int m_offsetY = 0;

	public float springStrength = 8f;
	public GameObject m_draggablePanel;
	public SpringPanel.OnFinished onFinished;

	GameObject _alignItem;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnEnable()
	{
		AlignItem();
	}


	void OnDragFinished()
	{
		if (enabled)	
			AlignItem();
	}

	public void AlignItem()
	{
		if (m_draggablePanel == null)
			return;

		UIDraggablePanel draggablePanel = m_draggablePanel.GetComponent<UIDraggablePanel> ();
		draggablePanel.onDragFinished = OnDragFinished;

		if(draggablePanel.horizontalScrollBar != null)						
			draggablePanel.horizontalScrollBar.onDragFinished = OnDragFinished;

		if (draggablePanel.verticalScrollBar != null)
			draggablePanel.verticalScrollBar.onDragFinished = OnDragFinished;

		if (draggablePanel.panel == null)
			return;

		// Calculate the panel's center in world coordinates
		Vector4 clip = draggablePanel.panel.clipRange;
		Transform dt = draggablePanel.panel.cachedTransform;
		Vector3 center = dt.localPosition;
		center.x += clip.x;
		center.y += clip.y;
		
		center.x -= m_offsetX;
		center.y -= m_offsetY;
		center = dt.parent.TransformPoint(center);

		// offset this value by the momentum
		Vector3 offsetCenter = center - draggablePanel.currentMomentum * 
			(draggablePanel.momentumAmount * 0.1f);
		draggablePanel.currentMomentum = Vector3.zero;

		float min = float.MaxValue;
		Transform closest = null;
		Transform trans = this.transform;

		// Determain the closest child
		for(int i=0; i<trans.childCount; i++)
		{
			Transform t = trans.GetChild(i);
			
			/*
			Vector3 pos = t.position;
			pos.x += m_offsetX;
			pos.y += m_offsetY;
			*/

			/*
			offsetCenter = dt.InverseTransformPoint(offsetCenter);
			offsetCenter.x += m_offsetX;
			offsetCenter.y += m_offsetY;
			offsetCenter = dt.parent.TransformPoint(offsetCenter);
			*/

			float sqrDist = Vector3.SqrMagnitude(t.position - offsetCenter);

			if(sqrDist < min)
			{
				min = sqrDist;
				closest = t;
			}
		}

		if (closest != null) 
		{
			_alignItem = closest.gameObject;

			// Figure out the difference between the chosen child and the panel's left top in local coordinates
			Vector3 offset = dt.InverseTransformPoint (closest.position) - dt.InverseTransformPoint (center);

			// offset shouldn't occur if blocked by a zeroed-out scale
			if (draggablePanel.scale.x == 0f)
				offset.x = 0f;
			if (draggablePanel.scale.y == 0f)
				offset.y = 0f;
			if (draggablePanel.scale.z == 0f)
				offset.z = 0f;

			offset.x += m_offsetX;
			offset.y += m_offsetY;

			// Spring the panel to this calculated position
			SpringPanel.Begin (m_draggablePanel, dt.localPosition - offset, springStrength).onFinished = onFinished;
		} 
		else 
		{
			_alignItem = null;		
		}

	}


}
