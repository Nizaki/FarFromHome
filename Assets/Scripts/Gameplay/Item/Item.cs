using UnityEngine;

namespace Assets.Scripts.Gameplay.Item
{
  [System.Serializable]
  public class Item
  {
    public string Id;

    public string Name;

    public itemType type;

    public Sprite Sprite;

    public Item(string id, string name, itemType type = itemType.none)
    {
      Id = id;
      Name = name;
      this.type = type;
      switch (type)
      {
        case itemType.none:
        case itemType.eatable:
        case itemType.equipment:
        case itemType.air:
          Sprite = GetSprite("item/" + id);
          break;

        case itemType.machine:
          Sprite = GetSprite("machine/" + id);
          break;

        case itemType.block:
          Sprite = GetSprite("block/" + id);
          break;

        default:
          Sprite = GetSprite("item/null");
          break;
      }
    }

    // public abstract void Use();
    private Sprite GetSprite(string id)
    {
      var temp = Resources.Load<Sprite>(id);
      if (temp != null)
        return temp;
      else
        return Resources.Load<Sprite>("Item/null");
    }

    public virtual void OnUse(Player player, Vector3Int position)
    {
    }
  }

  public enum itemType
  {
    none,
    eatable,
    block,
    equipment,
    air,
    machine
  }
}