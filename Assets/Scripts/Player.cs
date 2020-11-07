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

    private BlockBase currentBlock;
    public UnityAction<BlockBase> onBlockHover;
    public UnityAction<int> onHotbarSelect;
    public BlockBase selectBlock;
    public InventoryObj inventory;
    public ItemBase selectedItem;

    // Start is called before the first frame update
    private void Start()
    {
        hp = maxHp;
        hunger = maxHunger;
        oxygen = maxOxygen;
        water = maxWater;
        SelectHotbarSlot(currestHotbarIndex);
    }

    // Update is called once per frame
    private void Update()
    {
        Interact();
        Move();

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SelectHotbarSlot(currestHotbarIndex + 1);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SelectHotbarSlot(currestHotbarIndex - 1);
        }
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
            if (!currentBlock.OnActive() && selectBlock != null)
            {
                if (CheckPlaceAble(position))
                    GameManager.Instance.mainTile.SetTile(position, selectBlock);
                else
                    Debug.Log("there was some block block the way");
            }
        }
        if (Input.GetMouseButton(0))
        {
            GameManager.Instance.mainTile.SetTile(position, null);
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
        if (inventory.Container.Count - 1 >= slot)
        {
            selectedItem = inventory.Container[slot].item;
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
        currentBlock = block;
        onBlockHover?.Invoke(currentBlock);
    }

    public bool CheckPlaceAble(Vector3Int position)
    {
        if (GameManager.Instance.mainTile.GetTile<BlockBase>(position) != null)
            return false;
        return true;
    }
}