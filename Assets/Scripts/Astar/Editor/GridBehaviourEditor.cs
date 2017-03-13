using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridBehaviour))]
public class GridBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var myTarget = target as GridBehaviour;
        if(GUILayout.Button("Create Nodes",GUILayout.ExpandWidth(false)))
        {
            myTarget.CreateNodes(); 
        }
        if(GUILayout.Button("Create Children", GUILayout.ExpandWidth(false)))
        {
            myTarget.CreateChildren();
        }
        if(GUILayout.Button("Clear", GUILayout.ExpandWidth(false)))
        {
            myTarget.ClearNodes();
        }
    }
}
