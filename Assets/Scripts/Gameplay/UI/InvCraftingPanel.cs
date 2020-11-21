using UnityEngine;
using UnityEngine.UI;

public class InvCraftingPanel : MonoBehaviour
{
  public GameObject container;
  public GameObject craftItemPrefab;
  public CraftInfoPanel craftInfo;
  public CraftRecipe cRecipe;

  private void Start()
  {
    updateUI();
  }

  public void updateUI()
  {
    CraftingManager.Instance.InventoryCraftDB.recipes.ForEach((recipe) =>
    {
      var resultItem = ItemDB.getItemByID(recipe.result);
      var go = Instantiate(craftItemPrefab, container.transform);
      var comp = go.GetComponent<CraftItems>();

      comp.image.sprite = resultItem.Sprite;
      comp.itemName.text = resultItem.Name;
      go.GetComponent<Button>().onClick.AddListener(() =>
          {
            craftInfo.SetItem(new ItemStack(resultItem));
            cRecipe = recipe;
            craftInfo.ShowIngredient(recipe.ingredient);
          });
    });
  }

  public void Craft()
  {
    if (cRecipe.ingredient.TrueForAll((ingredient) =>
    GameManager.Instance.InvContain(ingredient.id, ingredient.amount)))
    {
      cRecipe.ingredient.ForEach((ingredient) =>
      {
        GameManager.Instance.RemoveItem(ItemDB.getItemByID(ingredient.id), ingredient.amount);
      });
      GameManager.Instance.addItem(ItemDB.getItemByID(cRecipe.result), cRecipe.amount);
    }
    else
      Debug.Log("No item");
  }
}