using UnityEngine;

public class PerlinNoise : MonoBehaviour
{

    public int width = 256, height = 256;
    public float scale = 20, offsetX = 100f, offsetY = 100f;
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        renderer.material.mainTexture = GenerateTexture();
    }

    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
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
        return new Color(sample, sample, sample);
    }
}
