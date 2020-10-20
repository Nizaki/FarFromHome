using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    private BlockBase selectedBlock;
    public UnityAction<BlockBase> onBlockHover;
    public BlockBase currentTile;
    public InventoryObj inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = GameManager.Instance.mainTile.WorldToCell(worldPoint);

        BlockBase tile = GameManager.Instance.mainTile.GetTile<BlockBase>(position);
        if (tile != null)
        {
            BlockHover(tile);
        }
        else BlockHover(GameManager.Instance.air);

        if (Input.GetMouseButtonDown(1))
        {
            selectedBlock.OnActive();
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckPlaceAble(position))
                GameManager.Instance.mainTile.SetTile(position, currentTile);
            else
                Debug.Log("there was some block block the way");
        }
    }

    public void BlockHover(BlockBase block)
    {
        Debug.Log(block);
        selectedBlock = block;
        onBlockHover?.Invoke(selectedBlock);
    }

    public bool CheckPlaceAble(Vector3Int position)
    {
        if (GameManager.Instance.mainTile.GetTile<BlockBase>(position) != null)
            return false;
        return true;
    }
}
