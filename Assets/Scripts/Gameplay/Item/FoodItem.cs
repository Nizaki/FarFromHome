using Assets.Scripts.Gameplay.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item
{
  float amount;
  public FoodItem(string id, string name,float amount) : base(id, name, itemType.eatable)
  {
    this.amount = amount;
  }

  public override void OnUse(Player player, Vector3Int position)
  {
    player.RestroHunger(amount);
  }
}
