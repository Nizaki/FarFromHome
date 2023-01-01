using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Item
{
  public static class Items
  {
    public static List<Item> ItemList = new List<Item>();
    public static Item AIR = AddItem(new Item("air", "air", itemType.air));

    //common item
    public static Item RICE = AddItem(new Item("rice", "Rice"));
    public static Item COAL = AddItem(new Item("coal", "Coal"));
    //block
    public static Item STONE = AddItem(new BlockItem("stone", "stone"));
    public static Item DIRT = AddItem(new BlockItem("dirt", "dirt"));
    public static Item GRASS = AddItem(new BlockItem("grass", "grass"));
    public static Item SAND = AddItem(new BlockItem("sand", "sand"));
    public static Item LOG = AddItem(new BlockItem("log", "Wooden Log"));
    public static Item WOOD_PLANK = AddItem(new BlockItem("wood_plank", "Wooden Plank"));
    //ore
    public static Item ORE_ALUMINIUM = AddItem(new BlockItem("ore_aluminium", "aluminium ore"));
    public static Item ORE_COAL = AddItem(new BlockItem("ore_coal", "coal ore"));
    public static Item ORE_COPPER = AddItem(new BlockItem("ore_copper", "copper ore"));
    public static Item ORE_GOLD = AddItem(new BlockItem("ore_gold", "gold ore"));
    public static Item ORE_IRON = AddItem(new BlockItem("ore_iron", "iron ore"));
    public static Item ORE_TITANIUM = AddItem(new BlockItem("ore_titanium", "titanium ore"));
    //chunk
    public static Item CHUNK_ALUMINIUM = AddItem(new Item("chunk_aluminium", "Aluminium Chunk"));
    public static Item CHUNK_COPPER = AddItem(new Item("chunk_copper", "Copper Chunk"));
    public static Item CHUNK_GOLD = AddItem(new Item("chunk_gold", "Gold Chunk"));
    public static Item CHUNK_IRON = AddItem(new Item("chunk_iron", "Iron Chunk"));
    public static Item CHUNK_TITANIUM = AddItem(new Item("chunk_titanium", "Titanium Chunk"));
    //Ingot
    public static Item INGOT_ALUMINIUM = AddItem(new Item("ingot_aluminium", "Aluminium Ingot"));
    public static Item INGOT_COPPER = AddItem(new Item("ingot_copper", "Copper Ingot"));
    public static Item INGOT_GOLD = AddItem(new Item("ingot_gold", "Gold Ingot"));
    public static Item INGOT_IRON = AddItem(new Item("ingot_iron", "Iron Ingot"));
    public static Item INGOT_TITANIUM = AddItem(new Item("ingot_titanium", "Titanium Ingot"));
    //function
    public static Item FURNACE = AddItem(new MachineItem("furnace", "Furnace"));
    public static Item SAPLING = AddItem(new MachineItem("sapling", "Sapling"));
    public static Item CHEST = AddItem(new MachineItem("chest", "Chest"));
    public static Item CRAFT_TABLE = AddItem(new MachineItem("crafting_table", "Crafting Table"));
    //Food
    public static Item POTATO = AddItem(new FoodItem("potato", "Potato", 1.5f));
    public static Item CARROT = AddItem(new FoodItem("carrot", "Carrot", 1.5f));
    public static Item CABBAGE = AddItem(new FoodItem("cabbage", "Cabbage", 1.5f));
    public static Item RAW_CHICKEN = AddItem(new FoodItem("raw_chicken", "Raw chicken", 1.5f));
    public static Item EGG = AddItem(new FoodItem("egg", "Egg", 1.5f));
    //Cooked food
    public static Item COOKED_POTATO = AddItem(new FoodItem("cooked_potato", "Baked Potato", 3f));
    public static Item COOKED_CARROT = AddItem(new FoodItem("cooked_carrot", "Baked Carrot", 3.5f));
    public static Item COOKED_CABBAGE = AddItem(new FoodItem("cooked_cabbage", "ผักกาดดอง", 2f));
    public static Item COOKED_RAW_CHICKEN = AddItem(new FoodItem("cooked_chicken", "Grilled Chicken", 6f));
    public static Item COOKED_EGG = AddItem(new FoodItem("cooked_egg", "Fired Egg", 10f));
    public static void init()
    {

    }

    private static Item AddItem(Item item)
    {
      ItemList.Add(item);
      return item;
    }

    public static void printAllItem()
    {
      ItemList.ForEach((item) => { Debug.Log(item.Name); });
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
}