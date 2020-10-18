using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class BlockWindows : EditorWindow
{
    [MenuItem("Window/BlockList")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BlockWindows));
    }

    private void OnGUI()
    {

    }
}
