using Assets.Scripts.Gameplay.Item;
using UnityEngine;

public class MachineItem : Item
{
  public GameObject prefab;

  public MachineItem(string id, string name) : base(id, name, itemType.machine)
  {
    this.prefab = Resources.Load<GameObject>("prefab/" + id);
    //Debug.Log($"{this.Id}, {this.Name}");
  }

  public override void OnUse(Player player, Vector3Int position)
  {
    if (TryPlace(position))
      player.RemoveItem(this, 1);
  }

  private bool TryPlace(Vector3Int position)
  {
    Vector2 pos;
    //TODO : Add algorithm to check floor
    pos.x = Mathf.Floor(position.x) + .5f;
    pos.y = Mathf.Floor(position.y) + .5f;
    if (GameManager.Instance.mainTile.GetTile(position) != null ||
      GameManager.Instance.mainTile.GetTile(new Vector3Int(position.x,position.y-1,0)) == null)
      return false;

    var temp = GameObject.Instantiate(prefab);
    temp.transform.SetParent(GameManager.Instance.machineHolder.transform);
    temp.transform.position = pos;
    return true;
  }
}