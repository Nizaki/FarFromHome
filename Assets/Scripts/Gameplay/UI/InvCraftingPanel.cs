using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvCraftingPanel : MonoBehaviour
{
    public GameObject container;
    public GameObject craftItemPrefab;

    private void Start()
    {
        updateUI();
    }

    public void updateUI()
    {
        Debug.Log(CraftingManager.Instance.getRecipe());
        CraftingManager.Instance.InventoryCraftDB.recipes.ForEach((recipe) =>
        {
            var resultItem = ItemDB.Instance.getItemByID(recipe.result);
            var go = Instantiate(craftItemPrefab, container.transform);
            var comp = go.GetComponent<CraftItems>();

            comp.image.sprite = resultItem.Sprite;
            comp.itemName.text = resultItem.Name;
        });
    }
}