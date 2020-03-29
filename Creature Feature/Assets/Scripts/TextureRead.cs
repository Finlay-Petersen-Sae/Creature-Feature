using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextureRead : MonoBehaviour
{
    public RenderTexture targetTexture;
    public Texture2D targetTexture2D;
    public Vector2 startPoint;
    public Vector2 endPoint;
    public Rect sourceRect;

    public Color[] pix;
    public List<PathDataNode> pathDataNodeList = new List<PathDataNode>();

    public Color startNodeColour;
    public Color endNodeColour;
    public Color highPriorityPassableColour;
    public Color lowPriorityPassableColour;
    public Color unpassableColour;
    
    int x;
    int y;
    int width;
    int height;

    // Start is called before the first frame update
    void Start()
    {
       
        sourceRect.width = targetTexture.width;
        sourceRect.height = targetTexture.height;
        x = Mathf.FloorToInt(sourceRect.x);
        y = Mathf.FloorToInt(sourceRect.y);
        width = Mathf.FloorToInt(sourceRect.width);
        height = Mathf.FloorToInt(sourceRect.height);
        targetTexture2D = new Texture2D(width, height);
    }

    // Update is called once per frame
    void Update()
    {
       
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
                    var pixelColour = pix[index];
                    if(pixelColour == startNodeColour)
                    {
                        startPoint = new Vector2(xIndex, yIndex);
                    }
                    else if(pixelColour == endNodeColour)
                    {
                        endPoint = new Vector2(xIndex, yIndex);
                    }
                    else if(pixelColour == highPriorityPassableColour)
                    {
                        PathDataNode Node = new PathDataNode(transform.position, new Vector2(xIndex, yIndex), true, false, false);
                        pathDataNodeList.Add(Node);
                    }
                    else if(pixelColour == lowPriorityPassableColour)
                    {
                        PathDataNode Node = new PathDataNode(transform.position, new Vector2(xIndex, yIndex), false, true, false);
                        pathDataNodeList.Add(Node);
                    }
                    else if(pixelColour == unpassableColour)
                    {
                        PathDataNode Node = new PathDataNode(transform.position, new Vector2(xIndex, yIndex), false, false, true);
                        pathDataNodeList.Add(Node); 
                    }
                    //if(pixelColour = start)
                    //create node objects inbetween the start and end vector 2 and fill out the world map
                    //create it based on colours
                    //TODO make a variable for colours that make different nodes, blockable, unprioritised that sort of stuff

                    // logic then goes here for creating different nodes based on the colour
                }
            }
        
        }
    }

    private void OnPostRender()
    {
      
        targetTexture2D.ReadPixels(new Rect(0, 0, targetTexture.width, targetTexture.height), 0, 0);
        targetTexture2D.SetPixels(pix);
        targetTexture2D.Apply();
        pix = targetTexture2D.GetPixels(x, y, width, height);

    }
}
