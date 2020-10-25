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
    public ItemBase item;
    public int amount;
        
    public InventorySlot(ItemBase _item,int amount)
    {
        this.item = _item;
        this.amount = amount;
    }
}