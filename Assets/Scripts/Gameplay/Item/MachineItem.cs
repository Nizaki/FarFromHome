using UnityEngine;

public class MachineItem : Item
{
  private GameObject prefab;

  public MachineItem(string id, string name, itemType itemType = itemType.machine) : base(id, name, itemType)
  {
    this.prefab = Resources.Load<GameObject>("prefab/" + id);
    //Debug.Log($"{this.Id}, {this.Name}");
  }

  public override void OnUse(Player player, Vector2 position)
  {
    if (TryPlace(position))
      player.RemoveItem(this, 1);
  }

  private bool TryPlace(Vector2 position)
  {
    Vector3Int pos = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
    //TODO : Add algorithm to check floor
    position.x = Mathf.Floor(position.x) + .5f;
    position.y = Mathf.Floor(position.y) + .5f;
    if (GameManager.Instance.mainTile.GetTile(pos) != null ||
      GameManager.Instance.mainTile.GetTile(new Vector3Int(pos.x, pos.y - 1, 0)) == null)
      return false;

    var temp = GameObject.Instantiate(prefab);
    temp.transform.SetParent(GameManager.Instance.machineHolder.transform);
    temp.transform.position = position;
    return true;
  }
}