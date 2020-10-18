using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class BlockBase : Tile
{
    public Sprite sprite;
    public BlockType blockType;
    public float hardness = 1f;
    public bool breakAble = true;

    public void OnActive()
    {
        if (blockType == BlockType.SOLID)
        {
            Debug.Log("nothing to show");
        }
    }
    // Start is called before the first frame update
#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/Blocks/BlockBase")]
    public static void CreateRoadTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Block Tile", "New Block Tile", "Asset", "Save Block Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<BlockBase>(), path);
    }
#endif
}
