using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    [Header("PathData Inputs")]
    private PathDataManager PDM;

    public List<PathFindingNode> Path = new List<PathFindingNode>();
    public List<PathFindingNode> openList = new List<PathFindingNode>();
    public List<PathFindingNode> closedList = new List<PathFindingNode>();
    public PathFindingNode startNode;
    public PathFindingNode endNode;
    public Vector3 start, end;

    public int iterationCount;

    // Start is called before the first frame update
    private void Start()
    {
        PDM = FindObjectOfType<PathDataManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (openList.Count != 0)
        {
            DrawPathFindData(Path);
            DrawPathFindData(openList);
            Debug.DrawRay(start, Vector3.up * 100, Color.cyan);
            Debug.DrawRay(end, Vector3.up * 100, Color.green);
        }
    }

    public void PathMake()
    {
        var pathNode1 = new PathFindingNode(PDM.pathDataList[Random.Range(0, PDM.pathDataList.Count)], 0, 0);
        var pathNode2 = new PathFindingNode(PDM.pathDataList[Random.Range(0, PDM.pathDataList.Count)], 0, 0);
      
        Path = PathFind(pathNode1, pathNode2);
    }

    public float calculateHCost(Vector3 _Start, Vector3 _End)
    {
        var hcost = Vector3.Distance(_Start, _End);
        return hcost;
    }

    public List<PathFindingNode> PathFind(PathFindingNode startNode, PathFindingNode endNode)
    {
        start = startNode.Node.worldLocation;
        end = endNode.Node.worldLocation;
        startNode.gCost = 0;
        startNode.hCost = calculateHCost(startNode.Node.worldLocation, endNode.Node.worldLocation);
        endNode.gCost = 0;

        if (startNode != null)
        {
            openList.Add(startNode);
        }

        iterationCount = 0;
        var thresholdLimit = 1600;
        iterationCount = 0;
        List<PathFindingNode> Path = new List<PathFindingNode>();
        while (openList.Count > 0)
        {
            iterationCount++;
            if (iterationCount > thresholdLimit)
            {
                break;
            }
            
            PathFindingNode bestNode = openList[0];
            foreach (var pathNode in openList)
            {
                if (pathNode.gCost + pathNode.hCost < bestNode.gCost + bestNode.hCost)
                {
                    bestNode = pathNode;
                }
            }
            if (bestNode != null)
            {
                openList.Remove(bestNode);
                closedList.Add(bestNode);
            }
            if (bestNode == endNode)
            {
                var currentNode = bestNode;
                while (currentNode.parentNode != null)
                {
                    Path.Add(currentNode);
                    currentNode = currentNode.parentNode;
                }
                Debug.Log("Iterations was " + iterationCount);
                Debug.Log("Path was" + Path.Count);

                Path.Reverse();

                return Path;
            }
            var neighbourList = CheckNeighbours(openList, closedList, bestNode);
            foreach (var neighbour in neighbourList)
            {
                if (!openList.Contains(neighbour))
                {
                    neighbour.gCost = bestNode.gCost + calculateHCost(bestNode.Node.worldLocation, neighbour.Node.worldLocation);
                    neighbour.hCost = calculateHCost(neighbour.Node.worldLocation, endNode.Node.worldLocation);
                    neighbour.parentNode = bestNode;
                    openList.Add(neighbour);
                }
                else
                {
                    var newGCost = bestNode.gCost + calculateHCost(bestNode.Node.worldLocation, neighbour.Node.worldLocation);
                    if (newGCost < neighbour.gCost)
                    {
                        neighbour.parentNode = bestNode;
                        neighbour.gCost = newGCost;
                    }
                }
            }
        }

        return null;
    }

    public List<PathFindingNode> CheckNeighbours(List<PathFindingNode> _openList, List<PathFindingNode> _closedList, PathFindingNode bestNode)
    {
        var west = new Vector2Int(-1, 0);
        var east = new Vector2Int(1, 0);
        var north = new Vector2Int(0, -1);
        var south = new Vector2Int(0, 1);
        var northwest = new Vector2Int(-1, 1);
        var northeast = new Vector2Int(1, 1);
        var southwest = new Vector2Int(-1, -1);
        var southeast = new Vector2Int(1, -1);
        List<PathFindingNode> neighbourList = new List<PathFindingNode>();

        var pathdata = PDM;

        var nodeWest = PDM.GetNode(bestNode.Node.gridLocation + west);
        if (nodeWest != null)
        {
            if (!ClosedListCheck(closedList, nodeWest))
            {
                if (!OpenListCheck(openList, nodeWest, neighbourList))
                {
                    var Node = new PathFindingNode(nodeWest, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeEast = PDM.GetNode(bestNode.Node.gridLocation + east);
        if (nodeEast != null)
        {
            if (!ClosedListCheck(closedList, nodeEast))
            {
                if (!OpenListCheck(openList, nodeEast, neighbourList))
                {
                    var Node = new PathFindingNode(nodeEast, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeNorth = PDM.GetNode(bestNode.Node.gridLocation + north);
        if (nodeNorth != null)
        {
            if (!ClosedListCheck(closedList, nodeNorth))
            {
                if (!OpenListCheck(openList, nodeNorth, neighbourList))
                {
                    var Node = new PathFindingNode(nodeNorth, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeSouth = PDM.GetNode(bestNode.Node.gridLocation + south);
        if (nodeSouth != null)
        {
            if (!ClosedListCheck(closedList, nodeSouth))
            {
                if (!OpenListCheck(openList, nodeSouth, neighbourList))
                {
                    var Node = new PathFindingNode(nodeSouth, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeNorthWest = PDM.GetNode(bestNode.Node.gridLocation + northwest);
        if (nodeNorthWest != null)
        {
            if (!ClosedListCheck(closedList, nodeNorthWest))
            {
                if (!OpenListCheck(openList, nodeNorthWest, neighbourList))
                {
                    var Node = new PathFindingNode(nodeNorthWest, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeNorthEast = PDM.GetNode(bestNode.Node.gridLocation + northeast);
        if (nodeNorthEast != null)
        {
            if (!ClosedListCheck(closedList, nodeNorthEast))
            {
                if (!OpenListCheck(openList, nodeNorthEast, neighbourList))
                {
                    var Node = new PathFindingNode(nodeNorthEast, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeSouthWest = PDM.GetNode(bestNode.Node.gridLocation + southwest);
        if (nodeSouthWest != null)
        {
            if (!ClosedListCheck(closedList, nodeSouthWest))
            {
                if (!OpenListCheck(openList, nodeSouthWest, neighbourList))
                {
                    var Node = new PathFindingNode(nodeSouthWest, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }
        var nodeSouthEast = PDM.GetNode(bestNode.Node.gridLocation + southeast);
        if (nodeSouthEast != null)
        {
            if (!ClosedListCheck(closedList, nodeSouthEast))
            {
                if (!OpenListCheck(openList, nodeSouthEast, neighbourList))
                {
                    var Node = new PathFindingNode(nodeSouthEast, 0, 0);
                    neighbourList.Add(Node);
                }
            }
        }

        return neighbourList;
    }

    public bool OpenListCheck(List<PathFindingNode> openlist, PathDataNode checkNode, List<PathFindingNode> Neighbours)
    {
        foreach (var node in openlist)
        {
            if (checkNode == node.Node)
            {
                Neighbours.Add(node);
                return true;
            }
        }
        return false;
    }

    public bool ClosedListCheck(List<PathFindingNode> closedlist, PathDataNode checkNode)
    {
        foreach (var node in closedlist)
        {
            if (checkNode == node.Node)
            {
                return true;
            }
        }
        return false;
    }

    private void DrawPathFindData(List<PathFindingNode> Pathdata)
    {
        foreach (var Node in Pathdata)
        {
            Debug.DrawRay(Node.Node.worldLocation, Vector3.up * 5, Color.magenta);
        }
    }
}