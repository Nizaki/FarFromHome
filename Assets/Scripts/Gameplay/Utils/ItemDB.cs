using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB Instance;
    public List<Item> ItemList = new List<Item>();

    // Start is called before the first frame update

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        ItemList.Sort((block1, block2) => block1.Id.CompareTo(block2.Id));
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public Item getItemByID(string id)
    {
        return ItemList.Where((t) => t.Id == id).FirstOrDefault();
    }

    public Item GetItem(int index)
    {
        return ItemList[index];
    }
}