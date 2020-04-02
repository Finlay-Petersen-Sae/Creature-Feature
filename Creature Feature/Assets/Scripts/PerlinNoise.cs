using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256, height = 256;
    public float scale = 20, offsetX = 100f, offsetY = 100f, xLoc, zLoc, hypotenuse = 14.142f;
    int newNoise;
    public GameObject[] SpawnableTiles;
    Renderer renderer;

    private void Awake()
    {
        GenerateTexture();
    }

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
            }
        }
    }

    void Generation(float noiseSample, float rNumber)
    {
        float spawnLocx = xLoc - hypotenuse;
        float spawnLocz = zLoc - hypotenuse;
        int caseSwitch = 0;
        if (noiseSample <= 0.25)
        {
            caseSwitch = 1;
        }
        else if(noiseSample > 0.25 && noiseSample <= 0.75)
        {
            caseSwitch = 2;
        }
        else if(noiseSample > 0.75)
        {
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
                    Instantiate(SpawnableTiles[0], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if (rNumber > 1 && rNumber <= 2)
                {
                    Instantiate(SpawnableTiles[1], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if(rNumber > 2)
                {
                    Instantiate(SpawnableTiles[2], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                break;
            case 2:
                if (rNumber <= 0.25)
                {
                    Instantiate(SpawnableTiles[0], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if (rNumber > 0.25 && rNumber <= 0.85)
                {
                    Instantiate(SpawnableTiles[1], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if (rNumber > 0.85)
                {
                    Instantiate(SpawnableTiles[2], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                break;
            case 3:
                if (rNumber <= 0.25)
                {
                    Instantiate(SpawnableTiles[0], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if (rNumber > 0.25 && rNumber <= 0.85)
                {
                    Instantiate(SpawnableTiles[1], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                else if (rNumber > 0.85)
                {
                    Instantiate(SpawnableTiles[2], new Vector3(spawnLocx, 0, spawnLocz), Quaternion.identity);
                }
                break;
        }
    }

}
