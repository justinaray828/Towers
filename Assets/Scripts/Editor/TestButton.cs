using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Bullets))]
public class TestButton : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Bullets myScript = (Bullets)target;
        if (GUILayout.Button("Test"))
        {
            myScript.Test();
        }
    }
}
