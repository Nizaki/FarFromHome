using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftDB", menuName = "DB/craftDB", order = 1)]
public class CraftDB : ScriptableObject
{
    public List<CraftRecipe> recipes;
}

[System.Serializable]
public class CraftRecipe
{
    [Tooltip("Item result id")]
    public string result;

    public int amount;

    [Tooltip("Ingredient id")]
    public Ingredient[] ingredient;
}

[System.Serializable]
public class Ingredient
{
    public string id;
    public int amount;
}