using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorageBoxPanel : MonoBehaviour
{
  [SerializeField]
  private GameObject storagePanel;

  [SerializeField]
  private GameObject playerPanel;

  [SerializeField]
  private GameObject itemSlotPrefab;

  public delegate void UiEventHandler();

  public event UiEventHandler UpdateUI;

  public void OpenPanel(Player player, Storage storage)
  {
    foreach (Transform child in storagePanel.transform)
    {
      Destroy(child.gameObject);
    }
    foreach (Transform child in playerPanel.transform)
    {
      Destroy(child.gameObject);
    }
    this.gameObject.SetActive(true);
    player.inventory.ForEach((stack) =>
    {
      var go = Instantiate(itemSlotPrefab, playerPanel.transform).GetComponent<ItemSlot>();
      go.SetItem(stack);
      go.gameObject.AddComponent<Button>().onClick.AddListener(() =>
      {
        if (storage.AddItem(stack.item, stack.amount))
        {
          player.RemoveItem(stack.item, stack.amount);
        }
        OpenPanel(player, storage);
      });
    });

    storage.container.ForEach((stack) =>
    {
      var go = Instantiate(itemSlotPrefab, storagePanel.transform).GetComponent<ItemSlot>();
      go.SetItem(stack);
      go.gameObject.AddComponent<Button>().onClick.AddListener(() =>
      {
        if (player.AddItem(stack.item, stack.amount))
        {
          storage.RemoveItem(stack.item, stack.amount);
        }
        OpenPanel(player, storage);
      });
    });
  }
}