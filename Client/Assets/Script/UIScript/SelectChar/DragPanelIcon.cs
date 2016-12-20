using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 拖曳面板的图标(执行脚本)aa
/// </summary>
public class DragPanelIcon: MonoBehaviour
{
	public delegate void OnDragPanelIconSelected(DragPanelIcon kDragPanelIcon);
	public OnDragPanelIconSelected onDragFinished;	//< Icon被选中的订阅事件aa
	
	public int m_IconId = -1;	//< 图标IDaa
	
	private Vector3 mDefaultScale;
	private Vector3 mDefaultSize;
	
	protected bool mIsDragging = false;	//< 拖曳标记aa
	
	protected virtual void Start()
	{
		mDefaultScale = this.gameObject.transform.localScale;
		mDefaultSize = this.gameObject.GetComponent<BoxCollider>().size;
	}
	
	protected virtual void Update()
	{	
		Vector4 kPanelRange = this.transform.parent.GetComponent<UIPanel>().clipRange;
		float fPosX = this.gameObject.transform.localPosition.x;
		float fDelta = Mathf.Abs(fPosX - kPanelRange.x);
		
		float fTemp = fDelta / (float)(mDefaultSize.x * 1.5f);
		if (fTemp > 1.0f) 
			fTemp = 1.0f;
		
		float fNewScale = (float)((1.0f - fTemp)* 0.5f + 0.6f);
		this.gameObject.GetComponent<UISprite>().transform.localScale = mDefaultScale * fNewScale;
		this.gameObject.GetComponent<BoxCollider>().size =  mDefaultSize / fNewScale;
	}
	
	// 移动到当前图标并回调aa
	public void Select()
	{
		sdDragPanel kDragPanel = this.gameObject.transform.parent.GetComponent<sdDragPanel>();
		if (kDragPanel == null)
			return;
		
		kDragPanel.onDragFinished += new sdDragPanel.OnDragFinished(this.OnDragFinishNotify);
		kDragPanel.SetMove(this.transform.localPosition.x);	
	}
	
	// 拖曳完成回调aa
	protected void OnDragFinishNotify()
	{	
		if (onDragFinished != null)
			onDragFinished(this);
		
		sdDragPanel kDragPanel = this.gameObject.transform.parent.GetComponent<sdDragPanel>();
		kDragPanel.onDragFinished -= new sdDragPanel.OnDragFinished(OnDragFinishNotify);
	}
	
	// 处理拖曳消息aa
	void OnDrag(Vector2 delta)
	{
		mIsDragging = true;
	}
	
	// 处理按下与松开消息aa
	void OnPress(bool isPressed)
	{
		if (!isPressed)
		{
			if (mIsDragging)
			{
				DragPanelIcon kNearIcon = null;
				DragPanelIcon[] kIconList = this.transform.parent.GetComponentsInChildren<DragPanelIcon>();

				float fMinDistance = 1000.0f;
				Vector4 kRange = this.transform.parent.GetComponent<UIPanel>().clipRange;
				foreach(DragPanelIcon kIconItem in kIconList)
				{
					float kPosition = kIconItem.gameObject.transform.localPosition.x;
					float fDelta = Mathf.Abs(kPosition - kRange.x);	
					if (fDelta < fMinDistance)
					{
						fMinDistance = fDelta;
						kNearIcon = kIconItem;
					}
				}
				
				if (kNearIcon != null) 
				{		
					sdDragPanel kDragPanel = this.gameObject.transform.parent.GetComponent<sdDragPanel>();
					kDragPanel.onDragFinished += new sdDragPanel.OnDragFinished(kNearIcon.OnDragFinishNotify);	//< 注册回调aa
					kDragPanel.SetMove(kNearIcon.transform.localPosition.x);									//< 要求面板移动到指定位置aa
				}
				
				mIsDragging = false;
			}
			else
			{
				if (this.gameObject.transform.localPosition.x == this.transform.parent.GetComponent<UIPanel>().clipRange.x)
				{
					if (onDragFinished != null)		//< 已经在指定位置则立即回调aa
						onDragFinished(this);
				}
				else
				{
					sdDragPanel kDragPanel = this.gameObject.transform.parent.GetComponent<sdDragPanel>();		
					kDragPanel.onDragFinished += new sdDragPanel.OnDragFinished(this.OnDragFinishNotify);	//< 注册回调aa
					kDragPanel.SetMove(this.gameObject.transform.localPosition.x);							//< 要求面板移动到指定位置aa
				}
			}
		}
	}
}
