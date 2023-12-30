using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Noise
{
    public static float[,] PerlinNoise(int width, int height, int octaves, float persistance, float lacunarity)
    {
        float[,] noise = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float frequency = 1;
                float amplitude = 1;

                for (int i = 0; i < octaves; i++)
                {
                    noise[x, y] += Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;
//                    Debug.Log(noise[x, y]);
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

            }

        }

        float max = noise.Cast<float>().Max();
        float min = noise.Cast<float>().Min();
        //Debug.Log("max:" + max+" min:"+min);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noise[x, y] = (noise[x, y] - min) / (max - min);
                //Debug.Log(noise[x, y]);
            }
        }

        return noise;
    }

}