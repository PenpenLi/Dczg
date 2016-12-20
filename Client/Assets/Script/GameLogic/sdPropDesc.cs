using UnityEngine;
using System.Collections;
using System;

public class sdPropDesc : object
{
	public const int TYPE_INT = 0;
	public const int TYPE_FLOAT = 1;
	public const int TYPE_STRING = 2;
	public const int TYPE_PERCENT = 3;
	
	public sdPropDesc(string name, int t, object min, object max,
							string desc, string other)
	{
		propName = name; propType = t; propMin = min; propMax = max;
		propDesc = desc; propOther = other;
	}
	
	public string 	propName;	//< 属性名
	public int		propType;	//< 属性类型 0: int 1: float 2: string 3: %
	public object	propMin;	//< 属性最小值
	public object	propMax;	//< 属性最大值
	public string	propDesc;	//< 属性描述
	public string	propOther;	//< 其他
}
