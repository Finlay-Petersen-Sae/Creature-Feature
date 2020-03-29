using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataNode
{
    //public enum NodeType
    //{
    //    HighPriority,
    //    LowPriority,
    //    UnPassable
    //};

    public bool HighPriority;
    public bool LowPriority;
    public bool Unpassable;

    public Vector3 worldLocation;
    public Vector2 gridLocation;
}
