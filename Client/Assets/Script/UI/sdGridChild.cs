using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sdGridChild : MonoBehaviour, IComparable
{
    public int index = 0;
    public Vector3 firstPos = Vector3.zero;
    public Vector3 magrinPos = Vector3.zero;
    public int width = 0;
    public int height = 0;

    public int CompareTo(object item)
    {
        sdGridChild compare = item as sdGridChild;
        if (compare.index < index)
        {
            return 1;
        }
        else if (compare.index > index)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}