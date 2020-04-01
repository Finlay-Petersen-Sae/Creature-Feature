using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode
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
}
