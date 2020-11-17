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

    public Item(string id, string name, itemType type = itemType.none)
    {
        this.Id = id;
        this.Name = name;
        this.type = type;
        switch (type)
        {
            case itemType.none:
            case itemType.eatable:
            case itemType.equipment:
                this.Sprite = GetSprite("item/" + id);
                break;

            case itemType.machine:
                this.Sprite = GetSprite("machine/" + id);
                break;

            case itemType.block:
                this.Sprite = GetSprite("block/" + id);
                break;

            default:
                this.Sprite = GetSprite("item/null");
                break;
        }
    }

    // public abstract void Use();
    private Sprite GetSprite(string id)
    {
        var temp = Resources.Load<Sprite>(id);
        if (temp != null)
            return temp;
        else
            return Resources.Load<Sprite>("Item/null");
    }

    public virtual void OnUse(Player player, Vector2 position)
    {
    }
}

public enum itemType
{
    none,
    eatable,
    block,
    equipment,
    air,
    machine
}