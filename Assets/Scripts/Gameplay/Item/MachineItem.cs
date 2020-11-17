using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineItem : Item
{
    private GameObject prefab;

    public MachineItem(string id, string name, itemType itemType = itemType.machine) : base(id, name, itemType)
    {
        this.prefab = Resources.Load<GameObject>("prefab/" + id);
        Debug.Log($"{this.Id}, {this.Name}");
    }

    public override void OnUse(Player player, Vector2 position)
    {
        Debug.Log("On use");
        TryPlace(position);
        base.OnUse(player, position);
    }

    private void TryPlace(Vector2 position)
    {
        position.x = Mathf.Floor(position.x) + .5f;
        position.y = Mathf.Floor(position.y) + .5f;

        var temp = GameObject.Instantiate(prefab);
        temp.transform.SetParent(GameManager.Instance.machineHolder.transform);
        temp.transform.position = position;
    }
}