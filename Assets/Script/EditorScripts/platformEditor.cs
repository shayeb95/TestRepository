using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatForm))]

public class platformEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //DrawDefaultInspector();

        PlatForm myScript = (PlatForm)target;

        if (GUILayout.Button("Destroy platform"))
        {
            myScript.DestroyExistingTiles();
        }

        if (GUILayout.Button("Build platform"))
        {
            myScript.GeneratePlatform();
        }
    }

}

