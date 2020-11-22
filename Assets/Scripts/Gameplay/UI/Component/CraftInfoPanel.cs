using Assets.Scripts.Gameplay.Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftInfoPanel : MonoBehaviour
{
  public ItemStack item;
  public Image image;
  public TextMeshProUGUI itemName;

  public GameObject[] slotList;

  public void SetItem(ItemStack item)
  {
    this.item = item;
    UpdateUi();
  }

  public void UpdateUi()
  {
    image.sprite = item.item.Sprite;
    itemName.text = item.item.Name;
  }

  public void ShowIngredient(List<Ingredient> item)
  {
    for (int i = 0; i < item.Count; i++)
    {
      slotList[i].GetComponent<ItemSlot>().SetItem(new ItemStack(Items.getItemByID(item[i].id), item.Count));
    }
  }
}