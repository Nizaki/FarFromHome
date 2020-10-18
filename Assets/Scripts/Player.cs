using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    private BlockBase selectedBlock;
    public UnityAction<BlockBase> onBlockHover;
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
    }

    public void BlockHover(BlockBase block)
    {
        Debug.Log(block);
        selectedBlock = block;
        onBlockHover?.Invoke(selectedBlock);
    }
}
