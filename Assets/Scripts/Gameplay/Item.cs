using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{ 
    public enum ItemType
    {
        Tool,
        Block,
        Consume
    }
    public ItemType itemType;
    public int amount;
}
