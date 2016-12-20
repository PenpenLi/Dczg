using UnityEngine;
using System.Collections;

public class sdArrowBar : UIScrollBar
{
	public UISprite	m_arrow1;
	public UISprite	m_arrow2;
	public float	m_showRange	= 0.1f;		// 滑动超过此范围后才会显示箭头，缺省为0.1
	float m_fBarSize	= 0;
	float m_fValue		= 0;

	// 控制一次显示的对象数，保证效率..
	public UIDraggablePanel m_DraggablePanel = null;	// 可拖动Panel
	public int m_ItemsPerLine	= 0;		// 每行的对象数.
	public int m_LinesPerScreen	= 0;		// 单次激活的总行数.
	public int m_ItemHeight		= 0;		// 对象的高度，如果为0，则取对象碰撞盒的高度.

	GameObject[] m_items		= null;
	int m_itmesShowBegin		= -1;
	int m_itmesShowEnd			= -1;


	void Start ()
	{
		// 箭头1的动画..
		if( m_arrow1 != null )
		{
			TweenPosition tp = m_arrow1.GetComponent<TweenPosition>();
			tp.to = tp.from = m_arrow1.transform.localPosition;
			if( m_arrow1.spriteName == "arrow1" )	// v arror
				tp.to.y += 3.0f;
			else
				tp.to.x -= 3.0f;
			tp.duration = 0.5f;
			tp.style = UITweener.Style.PingPong;
			tp.PlayForward();
		}

		// 箭头2的动画..
		if( m_arrow2 != null )
		{
			TweenPosition tp = m_arrow2.GetComponent<TweenPosition>();
			tp.to = tp.from = m_arrow2.transform.localPosition;
			if( m_arrow1.spriteName == "arrow1" )	// v arror
				tp.to.y -= 3.0f;
			else
				tp.to.x += 3.0f;
			tp.duration = 0.5f;
			tp.style = UITweener.Style.PingPong;
			tp.PlayForward();
		}
	}

	public override float barSize
	{
		get
		{
			return m_fBarSize;
		}
		set
		{
			m_fBarSize = value;
		}
	}

	public override float value
	{
		get
		{
			return m_fValue;
		}
		set
		{
			m_fValue = value;
			if( m_fBarSize <= 1.0f )
			{
				if( m_arrow1 != null )
				{
					if( m_fValue > m_showRange ) 
						m_arrow1.gameObject.SetActive(true);
					else
						m_arrow1.gameObject.SetActive(false);
				}	  
				if( m_arrow2 != null )
				{
					if( m_fValue < (1.0f-m_showRange) ) 
						m_arrow2.gameObject.SetActive(true);
					else
						m_arrow2.gameObject.SetActive(false);
				}
			}

			// 控制显示个数.
			if( m_DraggablePanel!=null && m_itmesShowBegin>=0 )
			{
				Vector4 v = m_DraggablePanel.gameObject.GetComponent<UIPanel>().clipRange;
				int y1 = (int)(v.y+v.w/2);
				int y2 = (int)(v.y-v.w/2);

				if( (m_items[m_itmesShowEnd].transform.localPosition.y-m_ItemHeight/2) >= y2 )
				{
					for(int i=0;i<m_ItemsPerLine;i++)
					{
						if( (m_itmesShowEnd+1) >= m_items.Length ) break;
						if( m_items[m_itmesShowEnd+1] == null ) break;
						m_itmesShowEnd++;
						m_items[m_itmesShowEnd].SetActive(true);
						if( m_itmesShowBegin > 0 )
							m_items[m_itmesShowBegin].SetActive(false);
						m_itmesShowBegin++;
					}
				}
				else if( (m_items[m_itmesShowBegin].transform.localPosition.y+m_ItemHeight/2) <= y1 )
				{
					for(int i=0;i<m_ItemsPerLine;i++)
					{
						if( (m_itmesShowBegin-1) < 0 ) break;
						m_itmesShowBegin--;
						m_items[m_itmesShowBegin].SetActive(true);
						if( (m_itmesShowEnd+1) < m_items.Length )
							m_items[m_itmesShowEnd].SetActive(false);
						m_itmesShowEnd--;
					}
				}
			}
		}
	}

	// 设置滚动栏的项目列表，必须按照显示的前后次序提供..
	public void SetItems(GameObject[] items)
	{
		m_items = items;
		if( m_items==null || m_items.Length<=0 || m_DraggablePanel==null ) return;

		// 如果没有设置对象高度，则取碰撞盒的高度..
		if( m_ItemHeight == 0 )
		{
			BoxCollider box = m_items[0].GetComponent<BoxCollider>();
			if( box != null ) m_ItemHeight = (int)box.size.y;
		}

		Vector4 v = m_DraggablePanel.gameObject.GetComponent<UIPanel>().clipRange;
		int y1 = (int)(v.y+v.w/2);		// 屏幕上边缘..
		int y2 = (int)(v.y-v.w/2);		// 屏幕下边缘..
	
		int ia = 0;
		for(int i=0;i<items.Length;i++)
		{
			if( items[i] == null ) break;

			if( ia>0 )
			{
				items[i].SetActive(true);
				ia--;
				m_itmesShowEnd = i;
			}
			else if( ((int)items[i].transform.localPosition.y-m_ItemHeight/2) > y1 )
			{
				if( i > 0 )
					items[i].SetActive(false);
			}
			else if( ((int)items[i].transform.localPosition.y+m_ItemHeight/2) < y2 )
			{
				if( (i+1) < items.Length )
					items[i].SetActive(false);
			}
			else
			{
				m_itmesShowBegin = i;
				i--;
				ia = m_ItemsPerLine * m_LinesPerScreen;
			}
		}
	}
}
