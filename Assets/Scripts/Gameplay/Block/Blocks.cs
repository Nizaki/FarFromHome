using Assets.Scripts.Gameplay.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Block
{
  public static class Blocks
  {
    public static List<BlockBase> BlockList = new List<BlockBase>();
    public static BlockBase STONE = NewBlock("stone", "Stone",1f);
    public static BlockBase DIRT = NewBlock("dirt", "dirt");
    public static BlockBase GRASS = NewBlock("grass", "Grass");
    public static BlockBase ORE_ALUMINIUM = NewBlock("ore_aluminium", "aluminium",2f);
    public static BlockBase SAND = NewBlock("sand", "sand");

    private static BlockBase NewBlock(string id, string name,float hardness = 0f, string dropItem = "")
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