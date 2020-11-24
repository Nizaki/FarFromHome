using Assets.Scripts.Gameplay.Block;
using Assets.Scripts.Gameplay.Item;
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
    GenerateOre(Blocks.ORE_IRON);
    GenerateOre(Blocks.ORE_GOLD);
    GenerateOre(Blocks.ORE_COPPER);
    GenerateOre(Blocks.ORE_COAL);
    GenerateOre(Blocks.ORE_ALUMINIUM);
    GenerateOre(Blocks.ORE_TITANIUM);
    GenerateBackground();
    tilemap.RefreshAllTiles();
    for (int i = 0; i < size_x / 4; i++)
    {
      var gameObject = (Items.SAPLING as MachineItem).prefab;
      var instan = Instantiate(gameObject, GameManager.Instance.machineHolder.transform);
      Vector2 point = PickSpawnPoint();
      instan.transform.position = new Vector2(point.x, point.y - 0.5f);
    }
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
          backTile.SetTile(new Vector3Int(x, y, 0), Blocks.STONE);
      }
    }
    for (int y = size_y; y < size_y + 5; y++)
    {
      for (int x = 0; x < size_x; x++)
      {
        perlinValue = Mathf.PerlinNoise(seed / (seed.ToString().Length * 10f) + x / 10.0f, seed / (seed.ToString().Length * 10f) + y / 10.0f);
        bool isblock = perlinValue > 0.02f;
        if (isblock)
          backTile.SetTile(new Vector3Int(x, y, 0), Blocks.DIRT);
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
          tilemap.SetTile(new Vector3Int(x, y, 0), Blocks.STONE);
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
            tilemap.SetTile(new Vector3Int(x, y, 0), Blocks.GRASS);
          else
            tilemap.SetTile(new Vector3Int(x, y, 0), Blocks.DIRT);
      }
    }
  }

  private void GenerateOre(BlockBase block, float rate = 0.15f, int level = 0)
  {
    int count = 0;
    float oreSeed = seed * 0.35f * Random.Range(0.0f, seed);
    int size = 0;
    if (level == 0)
      size = size_y;
    else
      size = level;
    for (int y = 0; y < size; y++)
    {
      for (int x = 0; x < size_x; x++)
      {
        bool isOre = Mathf.PerlinNoise(oreSeed / (oreSeed.ToString().Length * 10f) + x /
          10.0f, oreSeed / (oreSeed.ToString().Length * 10f) + y / 10.0f) < rate;
        if (isOre)
        {
          tilemap.SetTile(new Vector3Int(x, y, 0), block);
          count++;
        }
      }
    }
    Debug.Log($"Generated {block.id} : {count}");
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
          return new Vector2(x, y + 1f);
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