using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new block",menuName ="Inventory System/Items/Block")]
public class ItemBlock : ItemBase
{
    private void Awake()
    {
        itemType = ItemType.Block;
    }
}
