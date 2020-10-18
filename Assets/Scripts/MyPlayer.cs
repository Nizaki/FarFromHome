using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MyPlayer : MonoBehaviour
{
    public Tilemap mainTile;
    public Tile newTile;
    public Grid grid;
    float runSpeed = 20f;
    Vector2 horizontalMove;
    public Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, 0);
        horizontalMove = horizontalMove.normalized;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector2.up * 30;
        }

    }

    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + horizontalMove * Time.fixedDeltaTime);
    }
}
