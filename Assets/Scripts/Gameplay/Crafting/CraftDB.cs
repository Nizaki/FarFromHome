using System.Collections.Generic;
using UnityEngine;

//TODO:use hard code instead of scriptableObject
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
  public List<Ingredient> ingredient;
}

[System.Serializable]
public class Ingredient
{
  public string id;
  public int amount;
}