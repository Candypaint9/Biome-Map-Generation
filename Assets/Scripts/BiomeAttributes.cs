using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject")]
public class BiomeAttributes : ScriptableObject
{
    public string biomeName;

    public Color color;

    public int closenessPoints = 0;

    public float minTemp;
    public float maxTemp;

    public float minHumidity;
    public float maxHumidity;

    public float minHeight;
    public float maxHeight;
}


public static class BiomeFunctions
{
    public static int heightSeed = 0;
    public static int tempSeed = heightSeed + 1;
    public static int humiditySeed = heightSeed + 2;


    public static BiomeAttributes GetBiomeFromHumidityNoise(float noise, HumidityMap humidityMap)
    {
        int maxPoints = 0;
        BiomeAttributes mostPointsBiome = null;


        for (int i = 0; i < humidityMap.biomes.Length; i++)
        {
            if (noise >= humidityMap.biomes[i].minTemp && noise <= humidityMap.biomes[i].maxTemp)
            {
                humidityMap.biomes[i].closenessPoints += 1;
            }
        }

        for (int i = 0; i < humidityMap.biomes.Length; i++)
        {
            if (humidityMap.biomes[i].closenessPoints > maxPoints)
            {
                maxPoints = humidityMap.biomes[i].closenessPoints;
                mostPointsBiome = humidityMap.biomes[i];
            }
        }

        for (int i = 0; i < humidityMap.biomes.Length; i++)
        {
            humidityMap.biomes[i].closenessPoints = 0;
        }

        maxPoints = 0;

        return mostPointsBiome;
    }
   
    public static BiomeAttributes GetBiomeFromTempNoise(float noise, TempMap tempMap)
    {
        for (int i = 0; i < tempMap.biomes.Length; i++)
        {
            if (noise >= tempMap.biomes[i].minTemp && noise <= tempMap.biomes[i].maxTemp)
            {
                return tempMap.biomes[i];
            }
        }

        return tempMap.biomes[0];
    }  
    
    
    public static BiomeAttributes GetBiomeFromHeightNoise(float noise, HeightMap heightMap)
    {
        int maxPoints = 0;
        BiomeAttributes mostPointsBiome = null;


        for (int i = 0; i < heightMap.biomes.Length; i++)
        {
            if (noise >= heightMap.biomes[i].minTemp && noise <= heightMap.biomes[i].maxTemp)
            {
                heightMap.biomes[i].closenessPoints += 1;
            }
        }

        for (int i = 0; i < heightMap.biomes.Length; i++)
        {
            if (heightMap.biomes[i].closenessPoints > maxPoints)
            {
                maxPoints = heightMap.biomes[i].closenessPoints;
                mostPointsBiome = heightMap.biomes[i];
            }
        }

        for (int i = 0; i < heightMap.biomes.Length; i++)
        {
            heightMap.biomes[i].closenessPoints = 0;
        }

        maxPoints = 0;

        return mostPointsBiome;
    }


    public static BiomeAttributes GetBiomeFromAllNoise(float[] noiseArray, CombinedMap combinedMap)
    {
        int maxPoints = 0;
        BiomeAttributes mostPointsBiome = null;

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < combinedMap.biomes.Length; i++)
            {
                if (noiseArray[j] >= combinedMap.biomes[i].minTemp && noiseArray[j] <= combinedMap.biomes[i].maxTemp)
                {
                    combinedMap.biomes[i].closenessPoints += 1;
                }
            }
        }

        for (int i = 0; i < combinedMap.biomes.Length; i++)
        {
            if (combinedMap.biomes[i].closenessPoints > maxPoints)
            {
                maxPoints = combinedMap.biomes[i].closenessPoints;
                mostPointsBiome = combinedMap.biomes[i];
            }
        }

        for (int i = 0; i < combinedMap.biomes.Length; i++)
        {
            combinedMap.biomes[i].closenessPoints = 0;
        }
        maxPoints = 0;
        return mostPointsBiome;
    }

}