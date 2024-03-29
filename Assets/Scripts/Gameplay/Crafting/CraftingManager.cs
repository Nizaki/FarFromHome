﻿using UnityEngine;

public class CraftingManager : MonoBehaviour
{
  public static CraftingManager Instance { get; private set; }

  public CraftDB InventoryCraftDB;
  public CraftDB CraftingTableDB;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else if (Instance != this)
    {
      Destroy(this);
    }
  }

  public CraftDB getRecipe()
  {
    return InventoryCraftDB;
  }
}