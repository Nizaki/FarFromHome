using UnityEngine;
using UnityEngine.UI;

public class FurnacePanel : MonoBehaviour
{
    public ItemSlot inputSlot;
    public ItemSlot fuel;
    public ItemSlot resultSlot;
    public GameObject slotPrefab;
    public GameObject Parent;
    public Item air;
    private bool haveItem = false;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        updateInventory();
    }

    public void Init()
    {
        Debug.Log("init Furnace");
        air = ItemDB.Instance.getItemByID("air");
        updateInventory();
        resultSlot.gameObject.AddComponent<Button>().onClick.AddListener(() =>
        {
            if (resultSlot.item.item != ItemDB.Instance.getItemByID("air"))
            {
                GameManager.Instance.player.inventory.AddItem(resultSlot.item.item, resultSlot.item.amount);
                resultSlot.ResetSlot();
                updateInventory();
            }
        });
        inputSlot.gameObject.AddComponent<Button>().onClick.AddListener(() =>
        {
            if (inputSlot.item.item != ItemDB.Instance.getItemByID("air"))
            {
                GameManager.Instance.player.inventory.AddItem(inputSlot.item.item, inputSlot.item.amount);
                inputSlot.ResetSlot();
                updateInventory();
            }
        });
    }

    private void updateInventory()
    {
        foreach (Transform child in Parent.transform)
        {
            Destroy(child.gameObject);
        }
        GameManager.Instance.player.inventory.itemList.ForEach((item) =>
        {
            var go = Instantiate(slotPrefab);
            go.transform.SetParent(Parent.transform);
            var button = go.AddComponent<Button>();
            var slot = go.GetComponent<ItemSlot>();
            slot.SetItem(item);

            button.onClick.AddListener(() =>
            {
                if (inputSlot.item.item != ItemDB.Instance.getItemByID("air"))
                {
                    GameManager.Instance.player.inventory.AddItem(inputSlot.item.item, inputSlot.item.amount);
                }
                inputSlot.SetItem(new ItemStack(item.item, item.amount));
                GameManager.Instance.player.inventory.RemoveItem(item.item, item.amount);
                slot.UpdateUI();
                updateInventory();
                SlotClick();
                haveItem = true;
            });
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Init();
        }
        if (haveItem)
        {
            if (inputSlot.item.item == ItemDB.Instance.getItemByID("air"))
                return;
            var iSlot = inputSlot.item;
            var oSlot = resultSlot.item;
            if (resultSlot.item.item != air)
            {
                if (Recipes.get.Match(iSlot.item, oSlot.item))
                {
                    var result = Recipes.get.Find(iSlot.item);

                    iSlot.amount -= 1;
                    if (iSlot.amount <= 0)
                    {
                        inputSlot.ResetSlot();
                        haveItem = false;
                    }
                    resultSlot.item.amount += result.amount;
                    inputSlot.UpdateUI();
                    resultSlot.UpdateUI();
                }
            }
            else if (Recipes.get.match(iSlot.item))
            {
                var result = Recipes.get.Find(iSlot.item);

                iSlot.amount -= 1;
                if (iSlot.amount <= 0)
                {
                    inputSlot.ResetSlot();
                    haveItem = false;
                }
                resultSlot.SetItem(new ItemStack(result.item, result.amount));
                inputSlot.UpdateUI();
                resultSlot.UpdateUI();
            }
        }
    }

    public void SlotClick()
    {
        //TODO:add logic to remove item from inventory
    }
}