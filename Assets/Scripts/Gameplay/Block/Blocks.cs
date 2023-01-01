using Assets.Scripts.Gameplay.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Block
{
  public static class Blocks
  {
    public static List<BlockBase> BlockList = new List<BlockBase>();
    public static BlockBase STONE = AddBlock("stone", "Stone", 1f);
    public static BlockBase DIRT = AddBlock("dirt", "dirt");
    public static BlockBase GRASS = AddBlock("grass", "Grass", 0f, "dirt");
    public static BlockBase SAND = AddBlock("sand", "sand");
    public static BlockBase LOG = AddBlock("log", "Log");
    public static BlockBase WOOD_PLANK = AddBlock("wood_plank", "Wooden plank");
    public static BlockBase ORE_ALUMINIUM = AddBlock("ore_aluminium", "aluminium", 2f, "chunk_aluminium");
    public static BlockBase ORE_COAL = AddBlock("ore_coal", "aluminium", 2f, "coal");
    public static BlockBase ORE_COPPER = AddBlock("ore_copper", "aluminium", 2f, "chunk_copper");
    public static BlockBase ORE_GOLD = AddBlock("ore_gold", "aluminium", 2f, "chunk_gold");
    public static BlockBase ORE_IRON = AddBlock("ore_iron", "aluminium", 2f, "chunk_iron");
    public static BlockBase ORE_TITANIUM = AddBlock("ore_titanium", "aluminium", 2f, "chunk_titanium");

    private static BlockBase AddBlock(string id, string name, float hardness = 0f, string dropItem = "")
    {
      BlockBase block = ScriptableObject.CreateInstance<BlockBase>();
      block.id = id.ToLower();
      if (dropItem != "")
        block.dropItemId = dropItem;
      else
        block.dropItemId = id;
      block.hardness = hardness;
      block.name = name;
      block.sprite = GetSprite("block/" + id);
      BlockList.Add(block);
      return block;
    }

    public static BlockBase GetBlockById(string id)
    {
      return BlockList.Find((block) => block.id == id.ToLower());
    }

    private static Sprite GetSprite(string id)
    {
      var temp = Resources.Load<Sprite>(id);
      if (temp != null)
        return temp;
      else
        return Resources.Load<Sprite>("Item/null");
    }
  }
}