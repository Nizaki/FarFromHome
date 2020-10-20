using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new inventory" ,menuName = "Inventory Systems/New inventory")]
public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
        
    public InventorySlot(Item _item,int amount)
    {
        this.item = _item;
        this.amount = amount;
    }
}