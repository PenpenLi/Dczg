using UnityEngine;
using System.Collections.Generic;

public enum NotifyType
{
	HpChanged = 0,
	MpChanged,
};

public class sdNotification
{
	public virtual void Notifi(NotifyType id, object data1, object data2)
	{
	}
}

public class sdNotifiCenter : MonoBehaviour
{
	private static sdNotifiCenter m_Center;
	public static sdNotifiCenter NotifiCenter()
	{
		if (!m_Center)
		{
			GameObject notifiObject = new GameObject("NotifiCenter");
			m_Center = notifiObject.AddComponent<sdNotifiCenter>();
			DontDestroyOnLoad(m_Center);
		}
		
		return m_Center;
	}
	
	Dictionary<NotifyType, List<sdNotification>> m_notifiList = new Dictionary<NotifyType, List<sdNotification>>();
	
	public void RegisterNotifi(sdNotification notification, NotifyType id)
	{
		if (notification != null && m_notifiList != null)
		{
			List<sdNotification> list = m_notifiList[id];
			if (list != null)
			{
				list.Add(notification);
			}
			else
			{
				list = new List<sdNotification>();	
			}
			
			m_notifiList[id] = list;
		}
	}
	
	public delegate void NotifiDelegate();
	public event NotifiDelegate Notify;
	
	public void SendMsg(NotifyType id, object data1, object data2)
	{
		List<sdNotification> list = m_notifiList[id];
		if (list == null) return;
		foreach(sdNotification com in list)
		{
			//Notify += new NotifiDelegate(com.Notifi);
			com.Notifi(id, data1, data2);
		}
	}
}


