using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public int size_x = 500;
    public int size_y = 1000;
    public float tileSize = 1.0f;

    public Tilemap tilemap;
    public Tilemap backTile;
    public BlockBase[] tiles;
    public int seed;
    public float perlinValue;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tilemap.ClearAllTiles();
            seed = UnityEngine.Random.Range(0, 1000);
            StartCoroutine("GenerateCave");
        }
    }

    public void Generate()
    {
        GenerateCave();
        GenerateSurface();
        GenerateOre();
        GenerateBackground();
    }

    private void GenerateBackground()
    {
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.02f;
                if (isblock)
                    backTile.SetTile(new Vector3Int(x, y, 0), tiles[0]);
            }
        }
        for (int y = size_y; y < size_y + 5; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.02f;
                if (isblock)
                    backTile.SetTile(new Vector3Int(x, y, 0), tiles[1]);
            }
        }
    }

    private void GenerateCave()
    {
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.3f;
                if (isblock)
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[0]);
            }
        }
    }

    private void GenerateSurface()
    {
        Debug.Log("generate surfaace");
        for (int y = size_y; y < size_y + 5; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.3f;
                if (isblock)
                    if (y > size_y + 3)
                        tilemap.SetTile(new Vector3Int(x, y, 0), tiles[2]);
                    else
                        tilemap.SetTile(new Vector3Int(x, y, 0), tiles[1]);
            }
        }
    }

    private void GenerateOre()
    {
        int totalOre = 0;
        float oreSeed = seed * 0.35f;
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(oreSeed / (oreSeed.ToString().Length * 10f) + x / 10.0f, oreSeed / (oreSeed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue < 0.15f;
                if (isblock)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tiles[4]);
                    totalOre += 1;
                }
            }
        }
        Debug.Log(totalOre);
    }

    public Vector2 PickSpawnPoint()
    {
        int x = UnityEngine.Random.Range(0, size_x);
        for (int y = size_y + 10; y > 0; y--)
        {
            if (tilemap.GetTile(new Vector3Int(x, y, 0)) == null)
            {
                if (tilemap.GetTile(new Vector3Int(x, y - 1, 0)) != null)
                {
                    return new Vector2(x, y + 2f);
                }
            }
        }
        return PickSpawnPoint();
    }
}