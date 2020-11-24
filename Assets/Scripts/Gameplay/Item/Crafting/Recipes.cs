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
    recipes.Add(new Recipe(ItemS(Items.CHUNK_ALUMINIUM, 1), ItemS(Items.INGOT_ALUMINIUM, 1)));
    recipes.Add(new Recipe(ItemS(Items.CHUNK_COPPER, 1), ItemS(Items.INGOT_COPPER, 1)));
    recipes.Add(new Recipe(ItemS(Items.CHUNK_GOLD, 1), ItemS(Items.INGOT_GOLD, 1)));
    recipes.Add(new Recipe(ItemS(Items.CHUNK_IRON, 1), ItemS(Items.INGOT_IRON, 1)));
    recipes.Add(new Recipe(ItemS(Items.CHUNK_TITANIUM, 1), ItemS(Items.INGOT_TITANIUM, 1)));
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

  private ItemStack ItemS(Item item, int amount)
  {
    return new ItemStack(item, amount);
  }

  private ItemStack getItem(string id, int amout = 1)
  {
    return new ItemStack(Items.getItemByID(id), amout);
  }
}