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

    // private List<PathDataEdges> edges = new List<PathDataEdges>();

    public bool HighPriority;
    public bool LowPriority;
    public bool Unpassable;

    public Vector3 worldLocation;
    public Vector2 gridLocation;

    public PathDataNode(Vector3 _worldLocation, Vector2 _gridLocation, bool _highPriority, bool _lowPriority, bool _unpassable)
    {
        worldLocation = _worldLocation;
        gridLocation = _gridLocation;

        HighPriority = _highPriority;
        LowPriority = _lowPriority;
        Unpassable = _unpassable;
    }


}
