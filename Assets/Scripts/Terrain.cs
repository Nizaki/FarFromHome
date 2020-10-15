using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Terrain : MonoBehaviour
{
    public int size_x = 500;
    public int size_y = 1000;
    public float tileSize = 1.0f;

    public Tilemap tilemap;
    public Tile[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        GenerateTile();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateTile()
    {
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), tiles[Random.Range(0, tiles.Length)]);
            }
        }
    }

}
