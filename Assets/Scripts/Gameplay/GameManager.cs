using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player player;

    public Terrain terrain;

    public Tilemap mainTile;
    public Tilemap midleTile;
    public Tilemap backTile;

    public BlockBase air;

    public Vector2 spawnPoint;

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
        terrain.Generate();
        spawnPoint = terrain.PickSpawnPoint() / 2.0f;
        Debug.Log(spawnPoint); ;
        player.gameObject.transform.position = spawnPoint;
    }
}