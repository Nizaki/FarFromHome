using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
  public static ItemDB Instance;
  public static List<Item> ItemList = new List<Item>();
  public static List<BlockBase> blockList = new List<BlockBase>();
  // Start is called before the first frame update

  // Start is called before the first frame update
  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      init();
    }
    else if (Instance != this)
    {
      Destroy(this);
    }
  }

  private void init()
  {
    ItemList.Add(new Item("air", "air", itemType.air));
    ItemList.Add(new Item("stone", "stone", itemType.block));
    ItemList.Add(new Item("dirt", "dirt", itemType.block));
    ItemList.Add(new Item("grass", "grass", itemType.block));
    ItemList.Add(new Item("ore_aluminium", "aluminium ore", itemType.block));
    ItemList.Add(new Item("ore_coal", "coal ore", itemType.block));
    ItemList.Add(new Item("ore_copper", "copper ore", itemType.block));
    ItemList.Add(new Item("ore_gold", "gold ore", itemType.block));
    ItemList.Add(new Item("ore_iron", "iron ore", itemType.block));
    ItemList.Add(new Item("ore_titanium", "titanium ore", itemType.block));
    ItemList.Add(new Item("sand", "sand", itemType.block));
    ItemList.Add(new MachineItem("furnace", "Furnace"));
    BuildBlock();
  }

  private void printAllItem()
  {
    ItemList.ForEach((item) => { Debug.Log(item.Name); });
  }

  private void BuildBlock()
  {
    ItemList.FindAll((item) => item.type == itemType.block).ForEach((item) =>
    {
      //BlockBase block = new BlockBase(item.Id, item.Id, item.Sprite);
      BlockBase block = ScriptableObject.CreateInstance<BlockBase>();
      block.id = item.Id;
      block.dropItemId = item.Id;
      block.name = item.Name;
      block.sprite = item.Sprite;
      blockList.Add(block);
    });
  }

  public BlockBase GetBlockById(string id)
  {
    var block = blockList.Find((b) => b.id == id);
    return block;
  }

  private void Start()
  {
    ItemList.Sort((block1, block2) => block1.Id.CompareTo(block2.Id));
  }

  // Update is called once per frame
  private void Update()
  {
  }

  public static Item getItemByID(string id)
  {
    return ItemList.Where((t) => t.Id == id).FirstOrDefault();
  }

  public static Item GetItem(int index)
  {
    return ItemList[index];
  }
}