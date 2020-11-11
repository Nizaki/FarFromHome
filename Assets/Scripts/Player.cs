﻿using System;
using System.CodeDom;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public int hp;

    public int maxHp = 40;
    public float hunger;
    public float maxHunger = 20f;
    public float oxygen;
    public float maxOxygen = 10f;
    public float water;
    public float maxWater = 20f;
    public float temperature = 37f;

    [Header("Hotbar")]
    [SerializeField]
    private int currestHotbarIndex = 0;

    private BlockBase hoverBlock;
    public UnityAction<BlockBase> onBlockHover;
    public UnityAction<int> onHotbarSelect;
    public BlockBase selectBlock;
    public Inventory inventory;
    public Item selectedItem;

    [SerializeField]
    private float progress = 0;

    // Start is called before the first frame update
    private void Start()
    {
        hp = maxHp;
        hunger = maxHunger;
        oxygen = maxOxygen;
        water = maxWater;
        inventory.Init();
        Debug.Log(ItemDB.Instance.ItemList[0]);
        SelectHotbarSlot(0);
    }

    // Update is called once per frame
    private void Update()
    {
        Interact();
        Move();

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SelectHotbarSlot(currestHotbarIndex - 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SelectHotbarSlot(currestHotbarIndex + 1);
        }
    }

    private void LateUpdate()
    {
        SelectHotbarSlot(currestHotbarIndex);
        selectBlock = selectedItem.block;
    }

    private void Move()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        this.transform.Translate(move * Time.deltaTime * 5);
    }

    private void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = GameManager.Instance.mainTile.WorldToCell(worldPoint);

        BlockBase tile = GameManager.Instance.midleTile.GetTile<BlockBase>(position);
        if (tile == null)
            tile = GameManager.Instance.mainTile.GetTile<BlockBase>(position);
        if (tile != null)
        {
            BlockHover(tile);
        }
        else BlockHover(GameManager.Instance.air);

        if (Input.GetMouseButton(1))
        {
            if (hoverBlock.blockType != BlockType.FUNTIONAL && selectBlock != null)
            {
                if (CheckPlaceAble(position))
                {
                    switch (selectBlock.blockType)
                    {
                        case BlockType.OPAQUE:
                        case BlockType.SOLID:
                            GameManager.Instance.mainTile.SetTile(position, selectBlock);
                            inventory.RemoveItem(selectedItem);
                            break;

                        case BlockType.FUNTIONAL:
                            GameManager.Instance.midleTile.SetTile(position, selectBlock);
                            inventory.RemoveItem(selectedItem);
                            break;

                        default:
                            break;
                    }
                }
                else
                    Debug.Log("there was some block block the way");
            }
            else if (hoverBlock.blockType == BlockType.FUNTIONAL)
            {
                var roundPos = new Vector2(Mathf.Round(worldPoint.x), Mathf.Round(worldPoint.y));
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (hoverBlock != GameManager.Instance.air)
            {
                progress += Time.deltaTime;
                if (progress > hoverBlock.hardness)
                {
                    switch (hoverBlock.blockType)
                    {
                        case BlockType.OPAQUE:
                        case BlockType.SOLID:
                            GameManager.Instance.mainTile.SetTile(position, null);
                            break;

                        case BlockType.FUNTIONAL:
                            GameManager.Instance.midleTile.SetTile(position, null);
                            break;

                        default:
                            break;
                    }
                    GameManager.Instance.SpawnItemByID(worldPoint, hoverBlock.dropItemId, 1);
                    progress = 0;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            progress = 0;
        }
    }

    private void SelectHotbarSlot(int slot)
    {
        if (slot > 8)
        {
            SelectHotbarSlot(0);
            return;
        }
        else if (slot < 0)
        {
            SelectHotbarSlot(8);
            return;
        }
        currestHotbarIndex = slot;
        if (inventory.itemList.ElementAt(slot) != null)
        {
            selectedItem = inventory.itemList.ElementAt(slot);
            selectBlock = selectedItem.block;
        }
        else
        {
            selectedItem = null;
            selectBlock = null;
        }
        onHotbarSelect?.Invoke(slot);
    }

    public void BlockHover(BlockBase block)
    {
        hoverBlock = block;
        onBlockHover?.Invoke(hoverBlock);
    }

    public bool CheckPlaceAble(Vector3Int position)
    {
        if (GameManager.Instance.mainTile.GetTile<BlockBase>(position) != null)
            return false;
        return true;
    }
}