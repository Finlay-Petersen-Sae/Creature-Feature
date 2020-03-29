using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256, height = 256;
    public float scale = 20, offsetX = 100f, offsetY = 100f, xLoc, zLoc;
    int newNoise;
    public GameObject[] SpawnableTiles;
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
        //CoalatePerlinNoise();
    }

    private void Update()
    {


        
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x += 5)
        {
            for (int y = 0; y < height; y += 5)
            {
                xLoc = x;
                zLoc = y;
                Color colour = CalcColour(x, y);
                texture.SetPixel(x, y, colour);
            }
        }

        texture.Apply();
        return texture;
    }

    Color CalcColour(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        Generation(sample, Random.Range(0,4));
        return new Color(sample, sample, sample);
    }

    void CoalatePerlinNoise()
    {
        for (int x = 0; x < width; x+=5)
        {
            for (int y = 0; y < height; y+=5)
            {
                newNoise = Random.Range(0, 10000);
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;

                float sample = Mathf.PerlinNoise(xCoord + newNoise, yCoord + newNoise);
                Debug.Log("The perlin noise value in this area is " + sample);
            }
        }
    }

    void Generation(float noiseSample, float rNumber)
    {
        int caseSwitch = 0;
        Debug.Log("rnumber = " + rNumber);
        if (noiseSample <= 0.25)
        {
            Debug.Log("Should move to case 1 for generation");
            caseSwitch = 1;
        }
        else if(noiseSample > 0.25 && noiseSample <= 0.75)
        {
            Debug.Log("Should move to case 2 for generation");
            caseSwitch = 2;
        }
        else if(noiseSample > 0.75)
        {
            Debug.Log("Should move to case 3 for generation");
            caseSwitch = 3;
        }
        else
        {
            Debug.LogError("This should return a value, something is wrong");
        }

        
        switch (caseSwitch)
        {
            case 1:
                if(rNumber <= 1)
                {
                    Debug.Log("house case 1");
                    Instantiate(SpawnableTiles[0], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if (rNumber > 1 && rNumber <= 2)
                {
                    Debug.Log("high rise case 1");
                    Instantiate(SpawnableTiles[1], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if(rNumber > 2)
                {
                    Instantiate(SpawnableTiles[2], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                break;
            case 2:
                if (rNumber <= 0.25)
                {
                    Debug.Log("house case 2");
                    Instantiate(SpawnableTiles[0], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if (rNumber > 0.25 && rNumber <= 0.85)
                {
                    Debug.Log("high rise case 2");
                    Instantiate(SpawnableTiles[1], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if (rNumber > 0.85)
                {
                    Debug.Log("resturant case 2");
                    Instantiate(SpawnableTiles[2], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                break;
            case 3:
                if (rNumber <= 0.25)
                {
                    Debug.Log("house case 3");
                    Instantiate(SpawnableTiles[0], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if (rNumber > 0.25 && rNumber <= 0.85)
                {
                    Debug.Log("high rise case 3");
                    Instantiate(SpawnableTiles[1], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                else if (rNumber > 0.85)
                {
                    Debug.Log("resturant case 3");
                    Instantiate(SpawnableTiles[2], new Vector3(xLoc, 0, zLoc), Quaternion.identity);
                }
                break;
        }
    }

}
