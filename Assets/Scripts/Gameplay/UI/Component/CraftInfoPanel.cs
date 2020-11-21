using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftInfoPanel : MonoBehaviour
{
  public ItemStack item;
  public Image image;
  public TextMeshProUGUI itemName;

  public GameObject slotList;

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
}