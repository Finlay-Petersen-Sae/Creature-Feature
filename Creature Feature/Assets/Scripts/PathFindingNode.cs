using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode : IEquatable<PathFindingNode>
{
    public PathDataNode Node;
    public float gCost, hCost;
    public PathFindingNode parentNode;

    public PathFindingNode(PathDataNode _node, float _gcost, float _hcost)
    {
        Node = _node;
        gCost = _gcost;
        hCost = _hcost; 
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as PathFindingNode);
    }

    public bool Equals(PathFindingNode other)
    {
        return other != null &&
               EqualityComparer<PathDataNode>.Default.Equals(Node, other.Node);
    }

    public override int GetHashCode()
    {
        return -56134859 + EqualityComparer<PathDataNode>.Default.GetHashCode(Node);
    }

    public static bool operator ==(PathFindingNode node1, PathFindingNode node2)
    {
        return EqualityComparer<PathFindingNode>.Default.Equals(node1, node2);
    }

    public static bool operator !=(PathFindingNode node1, PathFindingNode node2)
    {
        return !(node1 == node2);
    }
}
