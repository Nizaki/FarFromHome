using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR

#endif

[System.Serializable]
public class BlockBase : Tile
{
  public string id;
  public string dropItemId = "";
  public BlockType blockType;
  public float hardness = 1f;
  public bool breakAble = true;

  public BlockBase(string id, string dropItemId, Sprite sprite)
  {
    this.id = id;
    this.dropItemId = id;
    this.sprite = sprite;
  }

  public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
  {
    tileData.sprite = sprite;
    tileData.colliderType = ColliderType.Grid;
  }

  public bool OnActive()
  {
    if (blockType == BlockType.SOLID)
    {
      return false;
    }
    if (blockType == BlockType.FUNTIONAL)
    {
    }
    return true;
  }
}