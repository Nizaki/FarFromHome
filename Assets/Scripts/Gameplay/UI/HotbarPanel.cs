using System.Collections.Generic;
using UnityEngine;

public class HotbarPanel : MonoBehaviour
{
  [SerializeField]
  private Inventory inventory;

  [SerializeField]
  private GameObject ItemSlot;

  [SerializeField]
  private Player player;

  private List<GameObject> slotList;

  // Start is called before the first frame update
  private void Start()
  {
    inventory = GameManager.Instance.player.inventory;
    slotList = new List<GameObject>();
    player.onHotbarSelect += selectHotbar;
    CreateHotbar();
  }

  private void OnDestroy()
  {
    player.onHotbarSelect -= selectHotbar;
  }

  private void CreateHotbar()
  {
    for (int i = 0; i < 9; i++)
    {
      var obj = Instantiate(ItemSlot, this.transform).GetComponent<HotbarSlot>();
      slotList.Add(obj.gameObject);
      if (i <= inventory.itemList.Count - 1)
      {
        if (inventory.itemList[i].item.type != itemType.air)
        {
          obj.itemPic.sprite = inventory.itemList[i].item.Sprite;
          obj.count.text = inventory.itemList[i].amount.ToString();
        }
        else
        {
          Texture2D tex = new Texture2D(16, 16);
          obj.itemPic.sprite = Sprite.Create(tex, new Rect(0, 0, 16, 16), new Vector2(8, 8));
          obj.count.text = "";
        }
      }
      else
      {
        Texture2D tex = new Texture2D(16, 16);

        obj.itemPic.sprite = Sprite.Create(tex, new Rect(0, 0, 16, 16), new Vector2(8, 8));
        obj.count.text = "";
        continue;
      }
    }
  }

  private void UpdateHotbar()
  {
    for (int i = 0; i < 9; i++)
    {
      var obj = slotList[i].GetComponent<HotbarSlot>();
      if (i <= inventory.itemList.Count - 1)
      {
        if (inventory.itemList[i].item.type != itemType.air)
        {
          obj.itemPic.sprite = inventory.itemList[i].item.Sprite;
          obj.count.text = inventory.itemList[i].amount.ToString();
        }
        else
        {
          Texture2D tex = new Texture2D(16, 16);
          obj.itemPic.sprite = Sprite.Create(tex, new Rect(0, 0, 16, 16), new Vector2(8, 8));
          obj.count.text = "";
        }
      }
      else
      {
        Texture2D tex = new Texture2D(16, 16);

        obj.itemPic.sprite = Sprite.Create(tex, new Rect(0, 0, 16, 16), new Vector2(8, 8));
        obj.count.text = "";
        continue;
      }
    }
  }

  private void selectHotbar(int index)
  {
    slotList[0].GetComponent<HotbarSlot>().hightlight.SetActive(0 == index);
    slotList[1].GetComponent<HotbarSlot>().hightlight.SetActive(1 == index);
    slotList[2].GetComponent<HotbarSlot>().hightlight.SetActive(2 == index);
    slotList[3].GetComponent<HotbarSlot>().hightlight.SetActive(3 == index);
    slotList[4].GetComponent<HotbarSlot>().hightlight.SetActive(4 == index);
    slotList[5].GetComponent<HotbarSlot>().hightlight.SetActive(5 == index);
    slotList[6].GetComponent<HotbarSlot>().hightlight.SetActive(6 == index);
    slotList[7].GetComponent<HotbarSlot>().hightlight.SetActive(7 == index);
    slotList[8].GetComponent<HotbarSlot>().hightlight.SetActive(8 == index);
  }

  private void LateUpdate()
  {
    UpdateHotbar();
  }
}