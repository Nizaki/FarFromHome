using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
  public bool Interact = true;
  public ItemStack item;
  public Image image;
  public TextMeshProUGUI text;

  private void Awake()
  {
    ResetSlot();
  }

  public void SetItem(ItemStack item)
  {
    this.item = item;
    image.sprite = item.item.Sprite;
    text.text = item.amount > 0 ? item.amount.ToString() : "";
  }

  public void UpdateUI()
  {
    image.sprite = item.item.Sprite;
    text.text = item.amount > 0 ? item.amount.ToString() : "";
    Debug.Log("Item slot update");
  }

  public void ResetSlot()
  {
    item = new ItemStack(ItemDB.getItemByID("air"), 0);
    UpdateUI();
  }
}