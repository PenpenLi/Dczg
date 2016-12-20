using UnityEngine;
class sdDragPanel : MonoBehaviour
{
	private bool isMove = false;
	private float movePoint = 0;
	private float startPos = 0;
	
	public delegate void OnDragFinished();
	public OnDragFinished onDragFinished;
	
	void Start()
	{
		startPos = transform.localPosition.x;
	}
	
	
	void Update()
	{
		if (isMove)
		{
			Vector3 pos = transform.localPosition;
			Vector4 range = GetComponent<UIPanel>().clipRange;
			if (range.x > movePoint)
			{
				if (range.x - 20 < movePoint)
				{
					range.x = movePoint;	
					pos.x = -movePoint + startPos;
					isMove = false;
					movePoint = 0;
				}
				else
				{
					range.x -= 20;	
					pos.x += 20;
				}
			}
			else if (range.x < movePoint)
			{
				if (range.x + 20 > movePoint)
				{
					range.x = movePoint;	
					pos.x = -movePoint + startPos;
					isMove = false;
					movePoint = 0;
				}
				else
				{
					range.x += 20;	
					pos.x -= 20;
				}
			}
			else
			{
				movePoint = 0;
				isMove = false;	
			}
			
			transform.localPosition = pos;
			GetComponent<UIPanel>().clipRange = range;
			
			if (!isMove)
			{
				GetComponent<UIDraggablePanel>().currentMomentum = new Vector3(0,0,0);	
				if (onDragFinished != null) onDragFinished();
			}
		}
	}
	
	public void SetMove(float point)
	{
		if (GetComponent<UIPanel>().clipRange.x == point) 
		{
			return;
		}
		
		movePoint = point;
		isMove = true;
	}
	
	public void StopMove()
	{
		isMove = false;
	}
}