using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedMap : MonoBehaviour
{
    FastNoiseLite noiseLib;

    public BiomeAttributes[] biomes;

    public int mapWidth;
    public int mapHeight;

    public float scale;

    public float offsetX;
    public float offsetY;

    private void Start()
    {
        noiseLib = new FastNoiseLite();
        noiseLib.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2S);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }


    private void Update()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = GenerateTexture();
    }


    Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(mapWidth, mapHeight);

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float xCoord = (float)x / mapWidth * scale + offsetX;
                float yCoord = (float)y / mapWidth * scale + offsetY;

                noiseLib.SetSeed(BiomeFunctions.tempSeed);
                float tempNoise = noiseLib.GetNoise(xCoord, yCoord);

                noiseLib.SetSeed(BiomeFunctions.heightSeed);
                float heightNoise = noiseLib.GetNoise(xCoord, yCoord);

                noiseLib.SetSeed(BiomeFunctions.humiditySeed);
                float humdityNoise = noiseLib.GetNoise(xCoord, yCoord);

                float[] noiseArray = new float[]
                {
                    tempNoise, heightNoise, humdityNoise
                };
                Color color = BiomeFunctions.GetBiomeFromAllNoise(noiseArray, this).color;

                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
}
