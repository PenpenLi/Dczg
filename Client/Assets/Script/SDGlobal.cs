using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Diagnostics;

public class SDMonsterDrop
{
	public int[] items;
	public int[] itemCount;
	public int money;
}

//临时背包...

public class TempItem
{
	public int itemId;
	public int itemCount;
}

public class TempBag
{
	public int money = 0;
	public List<TempItem> itemList = new List<TempItem>();
	
	public void Clear()
	{
		money = 0;
		itemList.Clear();
	}
	
	public void AddItem(int itemId)
	{
		bool have = false;
		for(int i = 0; i < itemList.Count; i++)
		{
			if(itemList[i].itemId == itemId)
			{
				itemList[i].itemCount = itemList[i].itemCount + 1;
				have = true;
				break;
			}
		}
		
		if(!have)
		{
			TempItem item = new TempItem();
			item.itemId = itemId;
			item.itemCount = 1;
			itemList.Add(item);
			sdUICharacter.Instance.AddLootItem(itemId);
		}
	}
}

public enum SCREEN_ASPECT_RATIO
{
	UNKNOW = 0,
	r16_9,
	r16_10,
	r3_2,
	r4_3,
}

public class SDGlobal : MonoBehaviour 
{
	// 品阶颜色：白、绿、蓝、蓝金、紫、紫金、橙...
	public static Color[] QualityColor = 
	{
		new Color(0.0f,0.0f,0.0f),		
		new Color(1.0f,1.0f,1.0f),		// 1:白..
		new Color(0.0f,1.0f,0.0f),		// 2:绿..
		new Color(0.0f,0.3f,1.0f),		// 3:蓝..
		new Color(1.0f,0.0f,1.0f),		// 5:紫..
		new Color(1.0f,0.5f,0.0f),		// 7:金..
	};

	public static bool apkUpdateEnable = true;
	public static bool gHomeLoginActivate = false;
	public static bool gHomeInitialize = false;
	public static string phoneNumber = "";
	public static string ptUserName = "";
	public static string ticket = "";

	// 屏幕分辨率比例...
	public static SCREEN_ASPECT_RATIO screenAspectRatio = SCREEN_ASPECT_RATIO.UNKNOW;
	
	// 初始化是否已经完成...
	public static bool initFinished = false;
		
	//等待被加载的场景名字，加载场景时都先进loading场景，再根据这个名字异步加载...
	public static string loadLevelName = "login";
	
	//是否开启手机调试模式...
	public static bool mobileDebug = false;
	//新的bundle下载测试开关...
	public static bool newBundleTech = false;
	
	//是否处于编辑模式，从login进游戏的话会被设成false
	//为true的话运行时会有给策划用的一些UI
	public static bool editorMode = true;
	
	//被击退时的摩擦力加速度...
	public static float beatBackA = 100.0f;
	
	public static int mainCharSelection = 0;	// 0 : warrior  1 : wizard
	
	public static bool CheckRoundHit(Vector3 sourcePos,float sourceRadius,
			Vector3 targetPos,float targetRadius)
	{
		Vector3 vec = targetPos - sourcePos;
		vec.y = 0.0f;
		
		float disSquare = vec.x*vec.x + vec.z*vec.z;
		if(disSquare > (sourceRadius+targetRadius)*(sourceRadius+targetRadius))
			return false;
		
		return true;
	}
	public static bool CheckBoxHit(Vector3 sourcePos,Vector3 dir,float distance,float fhalfwidth,
			Vector3 targetPos,float targetRadius)
	{
		Vector3 vUp		=	new Vector3(0,1,0);
		Vector3	vRight	=	Vector3.Cross(vUp,dir);
		
		Vector3	v	=	targetPos	-	sourcePos;
		
		float	x	=	Vector3.Dot(dir,v);
		float	y	=	Vector3.Dot(vUp,v);
		float	z	=	Vector3.Dot(vRight,v);
		
		if(x-targetRadius > distance || x+targetRadius < 0.0f)
		{
			return false;	
		}
		if(y-targetRadius > 2.0f || y+targetRadius < -0.5f)
		{
			return false;	
		}
		if(z-targetRadius > fhalfwidth || z+targetRadius < -fhalfwidth)
		{
			return false;	
		}
		
		return true;
	}
	
	public static bool CheckHit(Vector3 sourcePos,Vector3 sourceVec,
		float sourceRadius,float angle,
		Vector3 targetPos,float targetRadius)
	{
		//扇形和圆的相交检测,目前只考虑水平位置...
		
		//首先判断两圆心距离...
		Vector3 vec = targetPos - sourcePos;
		vec.y = 0.0f;
		
		float disSquare = vec.x*vec.x + vec.z*vec.z;
		if(disSquare > (sourceRadius+targetRadius)*(sourceRadius+targetRadius))
			return false;
		
		if(disSquare < targetRadius*targetRadius)
		{
			return true;
		}
		
		//背对的直接打不中...
		float dirDot = Vector3.Dot(sourceVec,vec);
		if(dirDot < 0)
			return false;
		
		//180度夹角特殊处理...
		if(Mathf.Abs(angle - 90.0f) < 0.001f)
		{
			//简单一点只按位置来好了...
			return true;
		}
		
		//然后看两边是否分别在圆心两侧...
		Quaternion rot = Quaternion.Euler(0,-angle,0);
		Vector3 leftVec = rot * sourceVec;
		leftVec.y = 0.0f;
		
		rot = Quaternion.Euler(0,angle,0);
		Vector3 rightVec = rot *sourceVec;
		rightVec.y = 0.0f;
		
		float leftDot = Vector3.Dot(leftVec,vec);
		float rightDot = Vector3.Dot(rightVec,vec);
		Vector3 leftCross = Vector3.Cross(leftVec,vec);
		Vector3 rightCross = Vector3.Cross(rightVec,vec);
		
		if(angle < 90.0f)
		{
			if(leftDot < 0 && rightDot < 0)
				return false;
			if(leftCross.y * rightCross.y < 0)
			{
				return true;
			}
			else
			{
				//两点式算出直线方程...
				Vector2 p1 = new Vector2(sourcePos.x,sourcePos.z);
				Vector2 p2 = p1 + new Vector2(leftVec.x,leftVec.z);
				float A,B,C;
				
				if(leftDot >= 0)
				{
					if(Mathf.Abs(p2.x - p1.x) < 0.001f)
					{
						A = 1.0f;
						B = 0.0f;
						C = -p1.x;
					}
					else
					{
						float k = (p2.y - p1.y)/(p2.x-p1.x);
						A = -k;
						B = 1.0f;
						C = k*p1.x - p1.y;
					}
				
					//求距离...
					float tmp = (A*sourcePos.x + B*sourcePos.z + C);
					float disSqure = tmp*tmp/(A*A+B*B);
				
					if(disSqure < targetRadius*targetRadius)
						return true;
				}
				
				if(rightDot >= 0)
				{
					p2 = p1 + new Vector2(rightVec.x,rightVec.z);
					if(Mathf.Abs(p2.x - p1.x) < 0.001f)
					{
						A = 1.0f;
						B = 0.0f;
						C = -p1.x;
					}
					else
					{
						float k = (p2.y - p1.y)/(p2.x-p1.x);
						A = -k;
						B = 1.0f;
						C = k*p1.x - p1.y;
					}
				
					//求距离...
					float tmp = (A*sourcePos.x + B*sourcePos.z + C);
					float disSqure = tmp*tmp/(A*A+B*B);
				
					if(disSqure < targetRadius*targetRadius)
						return true;
				}
			}
		}
		else
		{
			//对于大于180度的扇形，在两圆相交的前提下，小于180一侧只要不完全包夹目标圆则肯定相交...
			if(leftCross.y * rightCross.y > 0)
				return true;
			
			Vector2 p1 = new Vector2(sourcePos.x,sourcePos.z);
			Vector2 p2 = p1 + new Vector2(leftVec.x,leftVec.z);
			float A,B,C;
			
			if(leftDot >= 0)
			{
				if(Mathf.Abs(p2.x - p1.x) < 0.001f)
				{
					A = 1.0f;
					B = 0.0f;
					C = -p1.x;
				}
				else
				{
					float k = (p2.y - p1.y)/(p2.x-p1.x);
					A = -k;
					B = 1.0f;
					C = k*p1.x - p1.y;
				}
				
			//求距离...
			float tmp = (A*sourcePos.x + B*sourcePos.z + C);
			float disSqure = tmp*tmp/(A*A+B*B);
				
			if(disSqure < sourceRadius*sourceRadius)
				return true;
			}
				
			if(rightDot >= 0)
			{
				p2 = p1 + new Vector2(rightVec.x,rightVec.z);
				if(Mathf.Abs(p2.x - p1.x) < 0.001f)
				{
					A = 1.0f;
					B = 0.0f;
					C = -p1.x;
				}
				else
				{
					float k = (p2.y - p1.y)/(p2.x-p1.x);
					A = -k;
					B = 1.0f;
					C = k*p1.x - p1.y;
				}
				
				//求距离...
				float tmp = (A*sourcePos.x + B*sourcePos.z + C);
				float disSqure = tmp*tmp/(A*A+B*B);
				
				if(disSqure < targetRadius*targetRadius)
					return true;
			}
			
			
		}
		
		return false;
	}
	
	static public string macAddress="";

	public static void StartGetMacAddress()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
#if UNITY_ANDROID
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call("checkWifiMacAddress",new object[]{0});
#endif
		}
		else if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		
			foreach(NetworkInterface adapter in nics)
			{
				PhysicalAddress address = adapter.GetPhysicalAddress();
			
				if(address.ToString() != "")
				{
					macAddress = address.ToString();
					break;
				}
			}
			if(macAddress=="020000000000")
			{
				int randAddr	=	100000000+Random.Range(0,100)+(int)System.DateTime.Now.Ticks*100;
				macAddress	=	randAddr.ToString();
			}
		}
		else
		{
			NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
		
			foreach(NetworkInterface adapter in nics)
			{
				PhysicalAddress address = adapter.GetPhysicalAddress();
			
				if(address.ToString() != "")
				{
					macAddress = address.ToString();
					break;
				}
			}
		}
		
	}
	
	public static string GetMacAddress()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
#if UNITY_ANDROID
			/*AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			
			string ad = jo.Call<string>("getMacAddress",new object[]{0});
			Debug.Log(ad);
			ad = ad.Replace(":","");
			return ad;*/
			return macAddress;
#else
			return macAddress;
#endif
		}
		else
		{
			return macAddress;
		}
	}
	
	public static bool CheckMacCompelete()
	{
		if(Application.platform == RuntimePlatform.Android)
		{
#if UNITY_ANDROID
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			
			bool comp = jo.Call<bool>("getMacCompelete",new object[]{0});
			
			if(comp)
			{
				string ad = jo.Call<string>("getMacAddress",new object[]{0});
                UnityEngine.Debug.Log(ad);
				ad = ad.Replace(":","");
				macAddress = ad;
			}
			return comp;
#else
			return true;
#endif
		}
		else
		{
			return true;
		}
	}
	
	public static void Log(object message)
	{
		UnityEngine.Debug.Log(System.DateTime.Now.ToString() + ' ' + message.ToString());
	}
	
	// 掉落表aa
	public static Hashtable msMonsterDropTable;	
	
	//
	public static TempBag tmpBag = new TempBag();

    public static void PrintCallStack()
    {
        StackTrace ss = new StackTrace();
        StackFrame[] frames = ss.GetFrames();
        string str = "CallStack=\n";
        foreach (StackFrame sf in frames)
        {
            str += sf.GetMethod().Name +"  "+ sf.GetFileName() +"("+ sf.GetFileLineNumber()+")\n";
        }
        UnityEngine.Debug.Log(str);
    }
}
