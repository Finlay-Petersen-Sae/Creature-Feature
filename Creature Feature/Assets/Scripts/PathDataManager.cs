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

        if(index < 0 || index > 1442)
        {
            Debug.LogError("index is outside of array" + index);
            return null;
        }

        if (_checkLoc.x < 0 || _checkLoc.y < 0 || _checkLoc.x >= WorldSize.x || _checkLoc.y >= WorldSize.y)
        {
            return null;
        }
        return pathDataList[index];
    }
}
