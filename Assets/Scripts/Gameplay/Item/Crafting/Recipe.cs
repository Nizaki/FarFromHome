using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    public ItemStack itemIn;
    public ItemStack itemOut;

    public Recipe(ItemStack itemIn, ItemStack itemOut)
    {
        this.itemIn = itemIn;
        this.itemOut = itemOut;
    }
}