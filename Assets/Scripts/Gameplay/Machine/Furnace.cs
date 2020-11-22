using UnityEngine;

public class Furnace : Machine
{
  public override void OnUse(Player player)
  {
    Debug.Log($"player {player.name} use Furnace !!");
  }
}