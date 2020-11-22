using Assets.Scripts.Gameplay.Block;
using Assets.Scripts.Gameplay.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockItem : Item
{
  BlockBase Block;
  public BlockItem(string id, string name) : base(id, name, itemType.block)
  {
    Block = Blocks.GetBlockById(id);
  }

  public override void OnUse(Player player, Vector3Int position)
  {
    Debug.Log("use block");
    GameManager.Instance.mainTile.SetTile(position, Block);
  }
}
