using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Storage : Machine
{
  public List<ItemStack> container = new List<ItemStack>(9);

  [SerializeField]
  private StorageBoxPanel panel;

  public override void Awake()
  {
    base.Awake();
    container = new List<ItemStack>(9);
    for (int i = 0; i <= 9; i++)
    {
      container.Add(new ItemStack(ItemDB.AIR, 99));
    }
    Debug.Log(container.Count);
  }

  public override void OnUse(Player player)
  {
    Debug.Log("use");
    panel.OpenPanel(player, this);
  }

  public bool AddItem(Item item, int amount = 1)
  {
    if (container.Contains(container.Where(i => i.item.Id == item.Id).FirstOrDefault()))
    {
      container.Find(i => i.item.Id == item.Id).amount += amount;
      Debug.Log("add item " + item.Id + " " + amount + "ea.");
      return true;
    }
    else
    {
      int index = container.IndexOf(container.Where(p => p.item == ItemDB.AIR).FirstOrDefault());
      if (index > -1)
      {
        Debug.Log($"add {amount} {item.Id} to inventory");
        container[index] = new ItemStack(ItemDB.getItemByID(item.Id), amount);
        return true;
      }
      else
        return false;
    }
  }

  public bool RemoveItem(Item item, int amount = 1)
  {
    if (container.Contains(container.Where(i => i.item.Id == item.Id).FirstOrDefault()))
    {
      int index = container.IndexOf(container.Where(p => p.item.Id == item.Id).FirstOrDefault());
      if (container[index].amount < amount)
      {
        Debug.LogWarning("item is not enough to remove");
        return false;
      }
      container[index].amount -= amount;
      if (container[index].amount <= 0)
      {
        Debug.Log($"set item to AIR");
        container[index] = new ItemStack(ItemDB.getItemByID("air"), 0);
        return true;
      }
      return true;
    }
    Debug.LogWarning("Item not found");
    return false;
  }
}