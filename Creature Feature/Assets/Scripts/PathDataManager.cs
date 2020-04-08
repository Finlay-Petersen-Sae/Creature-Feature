using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataManager : MonoBehaviour
{
    public List<PathDataNode> pathDataList = new List<PathDataNode>();
    public Vector2Int WorldSize;

    public PathDataNode GetNode(Vector2Int _checkLoc)
    {
        int index = _checkLoc.y * WorldSize.x + _checkLoc.x;

        if (_checkLoc.x < 0 || _checkLoc.y < 0 || _checkLoc.x >= WorldSize.x || _checkLoc.y >= WorldSize.y)
        {
            return null;
        }

        if (index < 0 || index > pathDataList.Count)
        Debug.Log(index + " " + _checkLoc + " " + WorldSize);

        return pathDataList[index];
    }

    public PathFindingNode CreatePathNode(Vector3 _checkLoc)
    {
        Vector3Int.FloorToInt(_checkLoc);
        foreach (var node in pathDataList)
        {
            if (node.worldLocation == _checkLoc)
            {
                var pathNode = new PathFindingNode(node, 0, 0);
                pathNode.Node = node;
                return pathNode;
            }
        }
        return null;
    }

}
