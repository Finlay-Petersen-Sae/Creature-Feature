using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataNode : IEquatable<PathDataNode>
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
    public Vector2Int gridLocation;

    public PathDataNode(Vector3 _worldLocation, Vector2Int _gridLocation, NodeType _type)
    {
        worldLocation = _worldLocation;
        gridLocation = _gridLocation;

        type = _type;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as PathDataNode);
    }

    public bool Equals(PathDataNode other)
    {
        return other != null &&
               gridLocation.Equals(other.gridLocation);
    }

    public override int GetHashCode()
    {
        return 1117241146 + EqualityComparer<Vector2>.Default.GetHashCode(gridLocation);
    }

    public static bool operator ==(PathDataNode node1, PathDataNode node2)
    {
        return EqualityComparer<PathDataNode>.Default.Equals(node1, node2);
    }

    public static bool operator !=(PathDataNode node1, PathDataNode node2)
    {
        return !(node1 == node2);
    }
}
