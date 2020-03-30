using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataNode
{
    public enum NodeType
    {
        HighPriority,
        LowPriority,
        UnPassable
    };

    public NodeType type;
    // private List<PathDataEdges> edges = new List<PathDataEdges>();

    public bool HighPriority;
    public bool LowPriority;
    public bool Unpassable;

    public Vector3 worldLocation;
    public Vector2 gridLocation;

    public PathDataNode(Vector3 _worldLocation, Vector2 _gridLocation, NodeType _type)
    {
        worldLocation = _worldLocation;
        gridLocation = _gridLocation;

        type = _type;
    }


}
