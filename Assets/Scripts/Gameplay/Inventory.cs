using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeReference]
    public List<ItemStack> itemList = new List<ItemStack>(36);

    public Item noneItem;

    public void Init()
    {
        Debug.Log("player inventort init.");
        itemList = new List<ItemStack>(36);
        for (int i = 0; i < 36; i++)
        {
            itemList.Add(new ItemStack(ItemDB.Instance.getItemByID("air"), 0));
        }
        GameManager.Instance.addItem(ItemDB.Instance.getItemByID("furnace"));
        GameManager.Instance.addItem(ItemDB.Instance.getItemByID("stone"), 99);
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (itemList.Contains(itemList.Where(i => i.item.Id == item.Id).FirstOrDefault()))
        {
            itemList.Find(i => i.item.Id == item.Id).amount += amount;
            Debug.Log("add item " + item.Id + " " + amount + "ea.");
        }
        else
        {
            int index = itemList.IndexOf(itemList.Where(p => p.item == ItemDB.Instance.getItemByID("air")).FirstOrDefault());
            if (index > -1)
            {
                Debug.Log($"add item {item.Id} to inventory");
                itemList[index] = new ItemStack(ItemDB.Instance.getItemByID(item.Id), amount);
            }
            else
                Debug.Log("Inventory Full");
        }
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        if (itemList.Contains(itemList.Where(i => i.item.Id == item.Id).FirstOrDefault()))
        {
            int index = itemList.IndexOf(itemList.Where(p => p.item.Id == item.Id).FirstOrDefault());
            if (itemList[index].amount < amount)
            {
                Debug.LogWarning("item is not enough to remove");
                return false;
            }
            itemList[index].amount -= amount;
            if (itemList[index].amount <= 0)
            {
                Debug.Log("remove item " + itemList[index].item.Name);
                itemList[index] = new ItemStack(ItemDB.Instance.getItemByID("air"));
                return true;
            }
            return true;
        }
        Debug.LogWarning("Item not found");
        return false;
    }
}