using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMapController : MonoBehaviour
{
    //定义噪声数据
    public int width = 100;
    public int height = 100;
    public int octaves = 4;
    public float persistance = 9f;
    public float lacunarity = 0.3f;
    //定义地形tiles
    public Tarrain[] tarrains;
    [System.Serializable]
    public struct Tarrain
    {
        public String name;
        public float height;
        public TileBase tile;
    }
    GameObject tilemap;

    // Start is called before the first frame update
    void Start()
    {
        //新建Tilemap所在GameObject
        CreateGameObject();
        //为Tilemap添加Tile
        AddTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGameObject()
    {
        //新建Tilemap所在GameObject
        tilemap = new GameObject("Tilemap");
        tilemap.AddComponent<Tilemap>();
        tilemap.AddComponent<TilemapRenderer>();

        //设置Tilemap的位置
        tilemap.transform.position = new Vector3(-width / 2, -height / 2, 0);

        //设置本层为父类
        tilemap.transform.SetParent(transform);

        //为本层添加Grid
        this.gameObject.AddComponent<Grid>();
    }

    void AddTile()
    {
        //获取噪声数据
        float[,] noise = Noise.PerlinNoise(width, height, octaves, persistance, lacunarity);
        //赋值Tilemap
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int i = 0; i < tarrains.Length; i++)
                {
                    if (noise[x,y] < tarrains[i].height)
                    {
                        tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tarrains[i].tile);
                        break;
                    }
                    else
                    {
                        tilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tarrains[1].tile);
                    }    
                }
            }
        }
            
    }





}
