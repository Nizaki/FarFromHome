using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum ItemType
    {
        Tool,
        Block,
        Consume,
        None
    }
public class Item : ScriptableObject
{
    public GameObject prefab;
    public ItemType itemType;
    public int amount;
}
