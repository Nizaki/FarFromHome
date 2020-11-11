using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Id;

    public string Name;

    public itemType type;

    public Sprite Sprite;

    public int Count;
    public BlockBase block;

    public Item(string id, string name, itemType type = itemType.none, Sprite sprite = null, int count = 0, BlockBase block = null)
    {
        this.Id = id;
        this.Name = name;
        this.type = type;
        this.Sprite = sprite;
        this.Count = count;
        this.block = block;
    }

    // public abstract void Use();
}

public enum itemType
{
    none,
    eatable,
    block,
    equipment
}