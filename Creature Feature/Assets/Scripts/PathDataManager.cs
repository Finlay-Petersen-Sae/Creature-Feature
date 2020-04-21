using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDataManager : MonoBehaviour
{
    public List<PathDataNode> pathDataList = new List<PathDataNode>();
    public List<GameObject> FoodObj = new List<GameObject>();
    public List<GameObject> WaterObj = new List<GameObject>();
    public List<GameObject> HumanObj = new List<GameObject>();
    public List<GameObject> CatsObj = new List<GameObject>();
    public int CatLimit, curCatAmount;
    public List<string> catNames = new List<string>();
    public Vector2Int WorldSize;

    public void Start()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Food"))
        {
            FoodObj.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Water"))
        {
            WaterObj.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Human"))
        {
            HumanObj.Add(item);
        }
    }

    public void Update()
    {
        if(curCatAmount < CatLimit)
        {
            FindObjectOfType<PathFinding>().PathMake();
            curCatAmount++;
        }
    }

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
    // get node from location

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
    //get closest node from a location

}
