using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDB
{
  public static List<Item> ItemList = new List<Item>();
  public static List<BlockBase> blockList = new List<BlockBase>();
  public static Item AIR = AddItem(new Item("air", "air", itemType.air));

  //common item
  public static Item STONE = AddItem(new Item("stone", "stone", itemType.block));
  public static Item DIRT = AddItem(new Item("dirt", "dirt", itemType.block));
  public static Item GRASS = AddItem(new Item("grass", "grass", itemType.block));

  //ore
  public static Item ORE_ALUMINIUM = AddItem(new Item("ore_aluminium", "aluminium ore", itemType.block));
  public static Item ORE_COAL = AddItem(new Item("ore_coal", "coal ore", itemType.block));
  public static Item ORE_COPPER = AddItem(new Item("ore_copper", "copper ore", itemType.block));
  public static Item ORE_GOLD = AddItem(new Item("ore_gold", "gold ore", itemType.block));
  public static Item ORE_IRON = AddItem(new Item("ore_iron", "iron ore", itemType.block));
  public static Item ORE_TITANIUM = AddItem(new Item("ore_titanium", "titanium ore", itemType.block));
  public static Item SAND = AddItem(new Item("sand", "sand", itemType.block));

  //function
  public static Item FURNACE = AddItem(new MachineItem("furnace", "Furnace"));
  public static Item SAPLING = AddItem(new MachineItem("sapling", "Sapling"));

  public static void init()
  {
    BuildBlock();
  }

  private static Item AddItem(Item item)
  {
    ItemList.Add(item);
    return item;
  }

  private void printAllItem()
  {
    ItemList.ForEach((item) => { Debug.Log(item.Name); });
  }

  private static void BuildBlock()
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

  public static Item getItemByID(string id)
  {
    return ItemList.Where((t) => t.Id == id).FirstOrDefault();
  }

  public static Item GetItem(int index)
  {
    return ItemList[index];
  }
}