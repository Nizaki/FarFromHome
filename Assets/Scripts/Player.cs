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
        Interact();
        Move();
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

        BlockBase tile = GameManager.Instance.mainTile.GetTile<BlockBase>(position);
        if (tile != null)
        {
            BlockHover(tile);
        }
        else BlockHover(GameManager.Instance.air);

        if (Input.GetMouseButton(1))
        {
            if (!selectedBlock.OnActive())
            {
                if (CheckPlaceAble(position))
                    GameManager.Instance.mainTile.SetTile(position, currentTile);
                else
                    Debug.Log("there was some block block the way");
            }
        }
        if (Input.GetMouseButton(0))
        {
            GameManager.Instance.mainTile.SetTile(position, null);
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
