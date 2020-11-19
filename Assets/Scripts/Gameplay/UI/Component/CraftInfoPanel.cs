using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftInfoPanel : MonoBehaviour
{
    public ItemStack item;
    public Image image;
    public TextMeshProUGUI itemName;

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