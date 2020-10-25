using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemBase> itemList;
    public Inventory()
    {
        itemList = new List<ItemBase>();
    }
}
