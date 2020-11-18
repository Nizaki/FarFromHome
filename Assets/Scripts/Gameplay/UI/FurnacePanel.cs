using UnityEngine;
using UnityEngine.UI;

public class FurnacePanel : MonoBehaviour
{
    public ItemSlot inputSlot;
    public ItemSlot fuel;
    public ItemSlot resultSlot;
    public GameObject slotPrefab;
    public GameObject Parent;

    private bool isInit;
    private bool haveItem = false;

    public void Init()
    {
        int i = 0;
        Debug.Log("init Furnace");
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
                inputSlot.SetItem(item);
                SlotClick();
                haveItem = true;
            });
            i++;
        });
        isInit = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Init();
        }
        if (isInit && haveItem)
        {
            if (inputSlot.item.item == ItemDB.Instance.getItemByID("air"))
                return;
            var slot = inputSlot.item;
            Debug.Log(slot.amount);
            if (Recipes.get.match(slot.item))
            {
                var result = Recipes.get.Find(slot.item);

                slot.amount -= 1;
                if (slot.amount <= 0)
                {
                    inputSlot.item = new ItemStack(ItemDB.Instance.getItemByID("air"));
                    haveItem = false;
                }
                if (resultSlot.item.item == result.item)
                {
                    Debug.Log("add item");
                    resultSlot.item.amount += result.amount;
                }
                else
                {
                    Debug.Log("new Item");
                    resultSlot.SetItem(new ItemStack(result.item, result.amount));
                }
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