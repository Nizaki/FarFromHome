using Assets.Scripts.Gameplay.Item;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : PhysicsObject
{
  [SerializeField]
  private SpriteRenderer spriteRenderer;

  [SerializeField]
  private Animator animator;

  [Header("Player Stats")]
  public float hp;

  public float maxHp = 40;
  public float hunger;
  public float maxHunger = 20f;
  public float oxygen;
  public float maxOxygen = 10f;
  public float water;
  public float maxWater = 20f;
  public float temperature = 37f;

  public float maxSpeed = 7;
  public float jumpTakeOffSpeed = 7;

  public float minePower = 1f;
  [Header("Hotbar")]
  public List<ItemStack> inventory = new List<ItemStack>(36);

  [SerializeField]
  private int currestHotbarIndex = 0;

  private BlockBase hoverBlock;
  public UnityAction<BlockBase> onBlockHover;
  public UnityAction<int> onHotbarSelect;
  public Item selectedItem;

  [SerializeField]
  private float progress = 0;
  public bool canControl = true;
  // Start is called before the first frame update
  protected override void Start()
  {
    inventory = new List<ItemStack>(36);
    for (int i = 0; i < 36; i++)
    {
      inventory.Add(new ItemStack(Items.AIR, 0));
    }
    Debug.Log("player inventort init.");
    hp = maxHp;
    hunger = maxHunger;
    oxygen = maxOxygen;
    water = maxWater;
    GameObject.Find("Inventory")?.GetComponent<InvPanel>().CreateInv();
    AddItem(Items.FURNACE);
    AddItem(Items.getItemByID("sapling"), 99);
    SelectHotbarSlot(0);
    base.Start();
  }

  protected override void Update()
  {
    targetVelocity = Vector2.zero;
    if (canControl)
      ComputeVelocity();
  }

  private void LateUpdate()
  {
    if (canControl)
    {
      Interact();
      if (Input.GetAxis("Mouse ScrollWheel") > 0)
      {
        SelectHotbarSlot(currestHotbarIndex - 1);
      }
      else if (Input.GetAxis("Mouse ScrollWheel") < 0)
      {
        SelectHotbarSlot(currestHotbarIndex + 1);
      }
    }
    if (hunger <= 0)
    {
      hunger = 0;
      if (hp > 2)
        hp -= 0.1f;
    }
  }

  protected override void ComputeVelocity()
  {
    Vector2 move = Vector2.zero;

    move.x = Input.GetAxis("Horizontal");

    if (Input.GetButtonDown("Jump") && grounded)
    {
      hunger -= 0.15f;
      velocity.y = jumpTakeOffSpeed;
    }
    else if (Input.GetButtonUp("Jump"))
    {
      if (velocity.y > 0)
      {
        velocity.y = velocity.y * 0.5f;
      }
    }
    if (move.x > .05f)
    {
      spriteRenderer.flipX = false;
    }
    if (move.x < -.05f)
    {
      spriteRenderer.flipX = true;
    }
    if (move.x != 0)
    {
      hunger -= 0.1f * Time.deltaTime;
    }

    //animator.SetBool("grounded", grounded);
    animator.SetFloat("speed", Mathf.Abs(velocity.x) / maxSpeed);

    targetVelocity = move * maxSpeed;
  }

  private void Interact()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
      RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
      if (hit.collider != null)
      {
        if (hit.transform.tag == "Machine")
        {
          hit.transform.GetComponent<Machine>().OnUse(this);
          return;
        }
      }

      if (selectedItem.type == itemType.block)
      {
        if (CheckPlaceAble(position))
        {
          if (selectedItem is BlockItem tem)
            tem.OnUse(this, position);
          RemoveItem(selectedItem);
        }
        else
          Debug.Log("there was some block block the way");
      }
      else if (selectedItem.type == itemType.machine)
      {
        if (selectedItem is MachineItem tem)
          tem.OnUse(this, worldPoint.ToInt());
      }
    }
    if (Input.GetMouseButton(0))
    {
      RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
      if (hit.collider != null)
      {
        if (hit.transform.tag == "Machine")
        {
          hit.transform.GetComponent<Machine>().Break();
          return;
        }
      }
      if (hoverBlock != GameManager.Instance.air)
      {
        progress += Time.deltaTime * minePower;
        if (progress > hoverBlock.hardness)
        {
          switch (hoverBlock.blockType)
          {
            case BlockType.OPAQUE:
            case BlockType.SOLID:
              GameManager.Instance.mainTile.SetTile(position, null);
              break;

            default:
              break;
          }
          Debug.Log(hoverBlock.dropItemId);
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
    if (inventory.ElementAt(slot) != null)
    {
      selectedItem = inventory.ElementAt(slot).item;
    }
    else
    {
      selectedItem = null;
    }
    onHotbarSelect?.Invoke(slot);
  }

  public void RestroHunger(float value)
  {
    hunger += value;
    if (hunger > maxHunger)
      hunger = maxHunger;
  }

  public void SetSpeed(float speed)
  {
    maxSpeed = speed;
  }

  private void BlockHover(BlockBase block)
  {
    hoverBlock = block;
    onBlockHover?.Invoke(hoverBlock);
  }

  private bool CheckPlaceAble(Vector3Int position)
  {
    if (GameManager.Instance.mainTile.GetTile<BlockBase>(position) != null)
      return false;
    return true;
  }

  public void AddItemByID(string id, int amount = 1)
  {
    var item = Items.getItemByID(id);
    if (item != null)
      AddItem(Items.getItemByID(id), amount);
    else
      Debug.LogError($"Item {id} not found");
  }

  public bool AddItem(Item item, int amount = 1)
  {
    if (inventory.Contains(inventory.Where(i => i.item.Id == item.Id).FirstOrDefault()))
    {
      inventory.Find(i => i.item.Id == item.Id).amount += amount;
      Debug.Log("add item " + item.Id + " " + amount + "ea.");
      return true;
    }
    else
    {
      int index = inventory.IndexOf(inventory.Where(p => p.item == Items.getItemByID("air")).FirstOrDefault());
      if (index > -1)
      {
        Debug.Log($"add {amount} {item.Id} to inventory");
        inventory[index] = new ItemStack(Items.getItemByID(item.Id), amount);
        return true;
      }
      else
        Debug.Log("Inventory Full");
      return false;
    }
  }

  public bool RemoveItem(Item item, int amount = 1)
  {
    if (inventory.Contains(inventory.Where(i => i.item.Id == item.Id).FirstOrDefault()))
    {
      int index = inventory.IndexOf(inventory.Where(p => p.item.Id == item.Id).FirstOrDefault());
      if (inventory[index].amount < amount)
      {
        Debug.LogWarning("item is not enough to remove");
        return false;
      }
      inventory[index].amount -= amount;
      if (inventory[index].amount <= 0)
      {
        Debug.Log($"remove {amount} {inventory[index].item.Name}");
        inventory[index] = new ItemStack(Items.getItemByID("air"), 0);
        return true;
      }
      return true;
    }
    Debug.LogWarning("Item not found");
    return false;
  }
}