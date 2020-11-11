using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DItem : MonoBehaviour
{
    public SpriteRenderer itemRender;
    public Item itemDate;
    public LayerMask player;

    private void Start()
    {
        Invoke(nameof(DestroySelf), 600);
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1.5f, Vector2.up, 20, player);

        if (hit.collider != null)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position = Vector2.MoveTowards(transform.position, hit.transform.position, 4 * Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().inventory.AddItem(itemDate, itemDate.Count);
            DestroySelf();
        }
    }
}