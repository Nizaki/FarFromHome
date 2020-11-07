using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarPanel : MonoBehaviour
{
    [SerializeField]
    private InventoryObj inventory;

    [SerializeField]
    private GameObject ItemSlot;

    [SerializeField]
    private Player player;

    private List<GameObject> slotList;

    // Start is called before the first frame update
    private void Start()
    {
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
            if (i <= inventory.Container.Count - 1)
            {
                obj.itemPic.sprite = inventory.Container[i].item.itemPic;
                obj.count.text = inventory.Container[i].amount.ToString();
            }
            else
            {
                continue;
            }
        }
    }

    private void UpdateHotbar()
    {
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

    // Update is called once per frame
    private void Update()
    {
    }
}