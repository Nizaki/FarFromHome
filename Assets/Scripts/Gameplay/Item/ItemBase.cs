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
public class ItemBase : ScriptableObject
{
    public Sprite itemPic;
    public ItemType itemType;
    public int amount;
}