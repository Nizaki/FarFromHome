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
    // Start is called before the first frame update
    void Start()
    {
        CreateHotbar();
    }

    private void CreateHotbar()
    {
        for (int i = 0; i < 9; i++)
        {
            var obj = Instantiate(ItemSlot, this.transform).GetComponent<HotbarSlot>();
            if (i <= inventory.Container.Count-1)
            {
                obj.itemPic.sprite = inventory.Container[i].item.itemPic;
                obj.count.text = inventory.Container[i].amount.ToString() ;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
