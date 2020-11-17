using CommandTerminal;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Terrain : MonoBehaviour
{
    public int size_x = 500;
    public int size_y = 1000;
    public float tileSize = 1.0f;

    public Tilemap tilemap;
    public Tilemap backTile;
    public int seed;
    public float perlinValue;

    // Start is called before the first frame update
    private void Start()
    {
        Terminal.Shell.AddCommand("Gen", CommandGen, 0, 0, "Generate terrain");
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Generate()
    {
        GenerateCave();
        GenerateSurface();
        GenerateOre();
        GenerateBackground();
        tilemap.RefreshAllTiles();
    }

    private void GenerateBackground()
    {
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.02f;
                var block = ItemDB.Instance.GetBlockById("stone");
                if (isblock)
                    backTile.SetTile(new Vector3Int(x, y, 0), block);
            }
        }
        for (int y = size_y; y < size_y + 5; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
                bool isblock = perlinValue > 0.02f;
                if (isblock)
                    backTile.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("dirt"));
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
                    tilemap.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("stone"));
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
                        tilemap.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("grass"));
                    else
                        tilemap.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("dirt"));
            }
        }
    }

    private void GenerateOre()
    {
        int goldOre = 0;
        int ironOre = 0;
        float ironSeed = seed * 0.35f * UnityEngine.Random.Range(0.0f, seed);
        float goldSeed = seed * 0.15f * UnityEngine.Random.Range(0.0f, seed);
        for (int y = 0; y < size_y; y++)
        {
            for (int x = 0; x < size_x; x++)
            {
                bool isIron = Mathf.PerlinNoise(ironSeed / (ironSeed.ToString().Length * 10f) + x / 10.0f, ironSeed / (ironSeed.ToString().Length * 10f) + y / 10.0f) < 0.15f;
                if (isIron)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("ore_iron"));
                    ironOre += 1;
                }
                if (y < 150)
                {
                    bool isGold = Mathf.PerlinNoise(goldSeed / (goldSeed.ToString().Length * 10f) + x / 10.0f, goldSeed / (goldSeed.ToString().Length * 10f) + y / 10.0f) < 0.13f;
                    if (isGold)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), ItemDB.Instance.GetBlockById("ore_gold"));
                        goldOre += 1;
                    }
                }
            }
        }
        Debug.Log($"Generated iron: {ironOre} gold: {goldOre} total: {ironOre + goldOre}");
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

    [RegisterCommand(Help = "Generate terrain")]
    private void CommandGen(CommandArg[] args)
    {
        if (Terminal.IssuedError) return; // Error will be handled by Terminal
        Generate();
    }
}