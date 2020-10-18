using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BlockBase)), CanEditMultipleObjects]
public class BlockBaseEditor : Editor
{
    SerializedProperty sprite;
    SerializedProperty blocktype;
    SerializedProperty hardness;
    SerializedProperty breakable;
    Texture2D texture;
    // Start is called before the first frame update
    private void OnEnable()
    {
        sprite = serializedObject.FindProperty("sprite");
        blocktype = serializedObject.FindProperty("blockType");
        hardness = serializedObject.FindProperty("hardness");
        breakable = serializedObject.FindProperty("breakAble");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(sprite);
        EditorGUILayout.PropertyField(blocktype);
        EditorGUILayout.PropertyField(hardness);
        EditorGUILayout.PropertyField(breakable);
        serializedObject.ApplyModifiedProperties();
    }
}
