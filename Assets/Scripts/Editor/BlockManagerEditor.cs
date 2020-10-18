using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(BlockManager)), CanEditMultipleObjects]
public class BlockManagerEditor : Editor
{
    private ReorderableList list;
    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty("blockList"), true, true, true, true);
        list.elementHeight = EditorGUIUtility.singleLineHeight * 2f;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            var obj = new SerializedObject(element.objectReferenceValue);
            rect.y += 2;
            EditorGUI.ObjectField(
                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                element, GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y + EditorGUIUtility.singleLineHeight, 60 , EditorGUIUtility.singleLineHeight),
                obj.FindProperty("blockType"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 30, rect.y + EditorGUIUtility.singleLineHeight, 30, EditorGUIUtility.singleLineHeight),
                obj.FindProperty("breakAble"), GUIContent.none);
        };
        list.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Block List");
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();
        //list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
