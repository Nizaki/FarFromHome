using Assets.Scripts.Gameplay.Item;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour

{
  public static GameManager Instance { get; private set; }

  [SerializeField]
  private bool generateMap = true;

  public Player player;

  public Terrain terrain;

  public Tilemap mainTile;
  public GameObject machineHolder;
  public Tilemap backTile;

  public BlockBase air;

  public Vector2 spawnPoint;
  public GameObject dropItemPrefab;

  // Start is called before the first frame update
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(this);
    }
  }

  private void Start()
  {
    if (generateMap)
    {
      terrain.Generate();
      spawnPoint = terrain.PickSpawnPoint();
      Debug.Log(spawnPoint); ;
      player.gameObject.transform.position = spawnPoint;
    }
  }

  public void SpawnItemByID(Vector2 position, string id, int amount)
  {
    var item = Items.getItemByID(id);
    Debug.Log("spawn Item at : " + item);
    if (item != null)
    {
      //item.Count = amount;
      SpawnItem(position, item, amount);
    }
    else
      Debug.LogError("Item not fond");
  }

  public void SpawnItem(Vector2 position, Item item, int amount = 1)
  {
    var go = Instantiate(dropItemPrefab);
    go.transform.position = position;
    var dropCom = go.GetComponent<DItem>();
    dropCom.itemDate = item;
    dropCom.amount = amount;
    dropCom.itemRender.sprite = item.Sprite;
  }

  public void addItem(Item item, int amount = 1)
  {
    player.AddItem(item, amount);
  }

  public void RemoveItem(Item item, int amount = 1)
  {
    player.RemoveItem(item, amount);
  }

  public void DisableControl()
  {
    player.canControl = false;
  }

  public void EnableControl()
  {
    player.canControl = true;
  }

  public bool InvContain(string itemId, int amount = 1)
  {
    return player.inventory.Any((itemstack) => itemstack.item.Id == itemId && itemstack.amount >= amount);
  }
}