using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvPanel : MonoBehaviour
{
    private List<GameObject> gameObjects = new List<GameObject>();
    public GameObject invPrefab;

    public GameObject invContainer;
    public bool isInit = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    public void CreateInv()
    {
        if (!isInit)
        {
            GameManager.Instance.player.inventory.itemList.ForEach((item) =>
            {
                var go = Instantiate(invPrefab);
                go.transform.SetParent(invContainer.transform);
                var slot = go.GetComponent<InvSlot>();
                slot.image.sprite = item.item.Sprite.name != null ? item.item.Sprite : null;
                slot.text.text = item.amount.ToString();
                gameObjects.Add(go);
            });
            isInit = true;
        }
    }

    public void UpdateInv()
    {
        int i = 0;
        GameManager.Instance.player.inventory.itemList.ForEach((item) =>
        {
            var go = gameObjects[i];
            go.transform.SetParent(invContainer.transform);
            var slot = go.GetComponent<InvSlot>();
            Debug.Log(item.item.Sprite.name);
            Debug.Log(item.amount);
            slot.image.sprite = item.item.Sprite.name != null ? item.item.Sprite : null;
            slot.text.text = item.amount.ToString();
            gameObjects.Add(go);
            i++;
        });
    }

    // Update is called once per frame
    private void Update()
    {
    }
}