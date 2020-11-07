using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    public BlockBase block = null;
}