using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    [SerializeReference]
    public List<Item> itemList = new List<Item>(36);

    public Item noneItem;

    public void Init()
    {
        itemList = new List<Item>(36);
        for (int i = 0; i < 36; i++)
        {
            itemList.Add(ItemDB.Instance.GetItem(0));
        }
    }

    public void AddItem(Item item, int amount = 1)
    {
        if (itemList.Contains(itemList.Where(i => i.Id == item.Id).FirstOrDefault()))
        {
            //itemList.Find(i => i.Id == item.Id).Count += amount;
            Debug.Log("add item " + item.Id + " " + amount + "ea.");
        }
        else
        {
            int index = itemList.IndexOf(itemList.Where(p => p.Id == "none").FirstOrDefault());
            if (index > -1)
                itemList[index] = new Item(item.Id, item.Name, item.type);
            else
                Debug.Log("Inventory Full");
        }
    }

    public bool RemoveItem(Item item, int amount = 1)
    {
        if (itemList.Contains(itemList.Where(i => i.Id == item.Id).FirstOrDefault()))
        {
            //int index = itemList.IndexOf(itemList.Where(p => p.Id == item.Id).FirstOrDefault());
            //if (itemList[index].Count < amount)
            //{
            //    Debug.LogWarning("item is not enough to remove");
            //    return false;
            //}
            //itemList[index].Count -= amount;
            //if (itemList[index].Count <= 0)
            //{
            //    Debug.Log("remove item " + itemList[index].Name);
            //    itemList[index] = new Item("none", "none");
            //    return true;
            //}
            //return true;
        }
        Debug.LogWarning("Item not found");
        return false;
    }
}