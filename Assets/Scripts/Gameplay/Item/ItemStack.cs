using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public Item item;
    public int amount;

    public ItemStack(Item item, int amount = 1)
    {
        this.item = item;
        this.amount = amount;
    }
}