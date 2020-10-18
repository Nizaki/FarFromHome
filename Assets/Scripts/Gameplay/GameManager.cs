using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;

    public Tilemap mainTile;
    public Tilemap midleTile;
    public Tilemap backTile;

    public BlockBase air;
    // Start is called before the first frame update
    void Awake()
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
}
