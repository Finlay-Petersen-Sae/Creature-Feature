using System.Collections.Generic;
using UnityEngine;

public class TextureRead : MonoBehaviour
{
    [Header("Debug")]
    public bool drawGizmos;

    [Header("Markers")]
    public Color startNodeColour;

    public GameObject startNode;
    public Color endNodeColour;
    public GameObject endNode;

    [Header("Camera Config")]
    public Camera pathDataCam;

    public RenderTexture targetTexture;
    public Texture2D targetTexture2D;

    [Header("Procgen")]
    public Color[] pix;

    public Vector2 startPoint;
    public Vector2 endPoint;
    public Vector3 pixelToWorldScale;
    public Vector3Int startNodeMarkerLoc;
    public Vector3Int endNodeMarkerLoc;
    public Rect sourceRect;
    public Color highPriorityPassableColour;
    public Color lowPriorityPassableColour;
    public Color unpassableColour;

    public List<PathDataNode> pathDataNodeList = new List<PathDataNode>();
    public PathDataManager pathDataManager;

    private int x;
    private int y;
    private int width;
    private int height;

 

    // Start is called before the first frame update
    private void Awake()
    {

    }
    private void Start()
    {
        sourceRect.width = targetTexture.width;
        sourceRect.height = targetTexture.height;
        x = Mathf.FloorToInt(sourceRect.x);
        y = Mathf.FloorToInt(sourceRect.y);
        width = Mathf.FloorToInt(sourceRect.width);
        height = Mathf.FloorToInt(sourceRect.height);
        targetTexture2D = new Texture2D(width, height);
        startNodeMarkerLoc = Vector3Int.zero;
        endNodeMarkerLoc = Vector3Int.zero;
        pathDataManager = FindObjectOfType<PathDataManager>();
        pathDataManager.WorldSize = new Vector2Int(width, height);
        Render();
        ProcGen();
    }

    private void Update()
    {


        NodeGizmos();
        
    }

    // Update is called once per frame
    private void ProcGen()
    {
        if (targetTexture2D != null)
        {
            for (int yIndex = 0; yIndex < targetTexture2D.height; ++yIndex)
            {
                for (int xIndex = 0; xIndex < targetTexture2D.width; ++xIndex)
                {
                    int index = yIndex * targetTexture2D.width + xIndex;
                    var pixelColour = pix[index];

                    //Make the nodes attached to a particular object
                    //spawn them
                    //If the Local vector2's are not null continue through the loop.
                    ////Make a pathdata manager start.
                    //if(pixelColour == startNodeColour)
                    //{
                    //    startPoint = new Vector2(xIndex, yIndex);
                    //}
                    //else if(pixelColour == endNodeColour)
                    //{
                    //    endPoint = new Vector2(xIndex, yIndex);
                    //}
                    Vector3 spawnPoint = startNode.transform.position;
                    spawnPoint.x += (xIndex - startNodeMarkerLoc.x) * pixelToWorldScale.x;
                    spawnPoint.z += (yIndex - startNodeMarkerLoc.z) * pixelToWorldScale.z;

                    if (pixelColour == highPriorityPassableColour)
                    {
                        PathDataNode Node = new PathDataNode(spawnPoint, new Vector2Int(xIndex, yIndex), PathDataNode.NodeType.HighPriority);
                        pathDataNodeList.Add(Node);
                    }
                    else if (pixelColour == lowPriorityPassableColour)
                    {
                        PathDataNode Node = new PathDataNode(spawnPoint, new Vector2Int(xIndex, yIndex), PathDataNode.NodeType.HighPriority);
                        pathDataNodeList.Add(Node);
                    }
                    else if (pixelColour == unpassableColour)
                    {
                        PathDataNode Node = new PathDataNode(spawnPoint, new Vector2Int(xIndex, yIndex), PathDataNode.NodeType.HighPriority);
                        pathDataNodeList.Add(Node);
                    }
                    else
                    {
                        PathDataNode Node = new PathDataNode(spawnPoint, new Vector2Int(xIndex, yIndex), PathDataNode.NodeType.UnPassable);
                        pathDataNodeList.Add(Node);

                    }
                }
            }
            pathDataManager.pathDataList = pathDataNodeList;
            Debug.Log("pathdata list" + pathDataNodeList.Count);
        }
    }

    public void Render()
    {
        RenderTexture.active = targetTexture;

        pathDataCam.Render();

        targetTexture2D.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
       
        targetTexture2D.Apply();
        pix = targetTexture2D.GetPixels(x, y, width, height);
        targetTexture2D.SetPixels(pix);



        Color[] pixelData = targetTexture2D.GetPixels(0);
        pix = pixelData;
        if (targetTexture2D != null)
        {
            for (int yIndex = 0; yIndex < targetTexture2D.height; ++yIndex)
            {
                for (int xIndex = 0; xIndex < targetTexture2D.width; ++xIndex)
                {
                    int index = yIndex * targetTexture2D.width + xIndex;

                    //Make the nodes attached to a particular object
                    //spawn them
                    //If the Local vector2's are not null continue through the loop.
                    //Make a pathdata manager start.
                    if (pixelData[index] == startNodeColour)
                    {
                        startNodeMarkerLoc.x = xIndex;
                        startNodeMarkerLoc.z = yIndex;
                    }
                    else if (pixelData[index] == endNodeColour)
                    {
                        endNodeMarkerLoc.x = xIndex;
                        endNodeMarkerLoc.z = yIndex;
                    }
                }
            }
        }
        pixelToWorldScale = endNode.transform.position - startNode.transform.position;

        pixelToWorldScale.x /= (endNodeMarkerLoc.x - startNodeMarkerLoc.x);
        pixelToWorldScale.z /= (endNodeMarkerLoc.z - startNodeMarkerLoc.z);
        pixelToWorldScale.y = 0;

        Debug.Log("pixel to world scale is " + pixelToWorldScale);

        //Vector3 spawnPoint = startNode.transform.position;
        //spawnPoint.x += (15 - pixelToWorldScale.x - startNodeMarkerLoc.x);
        //spawnPoint.z += (15 - pixelToWorldScale.z - startNodeMarkerLoc.z);
        //GameObject newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //newObject.transform.position = spawnPoint;


    }

    private void NodeGizmos()
    {
        foreach (var PathDataNode in pathDataNodeList)
        {
            Debug.DrawRay(PathDataNode.worldLocation, Vector3.up, Color.magenta);
        }
    }
}