using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private bool generateMap = true;

    public Player player;

    public Terrain terrain;

    public Tilemap mainTile;
    public Tilemap midleTile;
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
            spawnPoint = terrain.PickSpawnPoint() / 2.0f;
            Debug.Log(spawnPoint); ;
            player.gameObject.transform.position = spawnPoint;
        }
    }

    public void SpawnItemByID(Vector2 position, string id, int amount)
    {
        var item = ItemDB.Instance.ItemList.Find((i) => i.Id == id);
        Debug.Log("spawn Item at : " + item);
        if (item != null)
        {
            item.Count = amount;
            SpawnItem(position, item);
        }
        else
            Debug.LogError("Item not fond");
    }

    public void SpawnItem(Vector2 position, Item item)
    {
        var go = Instantiate(dropItemPrefab);
        go.transform.position = position;
        var dropCom = go.GetComponent<DItem>();
        dropCom.itemDate = item;
        dropCom.itemRender.sprite = item.Sprite;
    }
}