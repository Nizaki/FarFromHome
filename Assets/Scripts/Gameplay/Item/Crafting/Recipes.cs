using Assets.Scripts.Gameplay.Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Recipes : MonoBehaviour
{
  public static Recipes get;

  public bool init = false;

  public List<Recipe> recipes = new List<Recipe>();

  private void Awake()
  {
    if (get == null)
    {
      get = this;
      Init();
    }
    else if (get != this)
    {
      Destroy(this);
    }
  }

  private void Init()
  {
    recipes.Add(new Recipe(getItem("stone"), getItem("furnace")));
    init = true;
  }

  public bool Match(Item input, Item output)
  {
    return recipes.Any((recipe) => recipe.itemIn.item == input && recipe.itemOut.item == output);
  }

  public bool match(Item ingredient)
  {
    return recipes.Any((recipe) => recipe.itemIn.item == ingredient);
  }

  public bool ResultMatch(Item resultItem)
  {
    return recipes.Any((recipe) => recipe.itemOut.item == resultItem);
  }

  public ItemStack Find(Item ingredient)
  {
    return recipes.Find((item) => item.itemIn.item == ingredient).itemOut;
  }

  private ItemStack getItem(string id, int amout = 1)
  {
    return new ItemStack(Items.getItemByID(id), amout);
  }
}