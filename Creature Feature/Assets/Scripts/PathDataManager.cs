using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataManager : MonoBehaviour
{
    public List<PathDataNode> pathDataList = new List<PathDataNode>();

    public PathDataNode GetNode(Vector2 _checkLoc)
    {
        PathDataNode desiredNode = null;
        foreach (var Node in pathDataList)
        {
            if (Node.gridLocation == _checkLoc)
                desiredNode = Node;
        }
        return desiredNode;
    }
}
