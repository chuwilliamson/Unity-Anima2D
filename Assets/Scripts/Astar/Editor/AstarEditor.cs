using UnityEditor;
using UnityEngine;
using AIE;
using System.Collections.Generic;
using System.IO;

public class AstarEditor : EditorWindow
{
    Vector2 nPos;
    Vector2 dims;
    ScriptableGrid scriptableGrid;
    [MenuItem("Tools/Astar Editor %#e")]
    private static void Init()
    {
        var window = (AstarEditor)GetWindow(typeof(AstarEditor));
        window.ShowNotification(new GUIContent("Astar!", "get u some"));
    }

    private void OnGUI()
    {
        GUILayout.Space(50);
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Create Grid", GUILayout.ExpandWidth(false)))
            CreateGrid();

        if(GUILayout.Button("Clear Grid", GUILayout.ExpandWidth(false)))
            ClearGrid();
        GUILayout.EndHorizontal();

        GUILayout.Space(50);

        scriptableGrid = (ScriptableGrid)EditorGUILayout.ObjectField(scriptableGrid,typeof(ScriptableGrid),true, GUILayout.ExpandWidth(false));
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Get Neighbors", GUILayout.ExpandWidth(false)))        
            GetNeighbors();
        
        GUILayout.EndHorizontal();
        nPos = EditorGUILayout.Vector2Field("Position", nPos, GUILayout.ExpandWidth(false));
        GUILayout.BeginHorizontal();
        dims = EditorGUILayout.Vector2Field("Grid Dimensions", dims, GUILayout.ExpandWidth(false));
        GUILayout.EndHorizontal();
    }

    public void GetNeighbors()
    {
        //var node = scriptableGrid.GetNode(new Point((int)nPos.x, (int)nPos.y));
        //var nay = scriptableGrid.GetNeighbors(node.Id);
        //foreach(var n in nay)
        //    Debug.Log(n);
    }
    public static void ClearGrid()
    {
        if(!FindObjectOfType<GridBehaviour>())
            return;

        var parent = FindObjectOfType<GridBehaviour>().gameObject;
        DestroyImmediate(parent);
        DeleteScriptables();

    }

    public void CreateGrid()
    {
        ClearGrid();
        scriptableGrid = (ScriptableGrid)CreateInstance(typeof(ScriptableGrid));
    }
 
    public static void DeleteScriptables()
    {
       
        var absPath = Application.dataPath + "/Scripts/Utilities/ScriptableObjects/";
        //datapath includes the "Assets" string so take it off to get the relative path...
        var relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);

         

        foreach(var s in  Directory.GetFiles(relPath, "*.asset"))
        {
            AssetDatabase.DeleteAsset(s);
        }
    }
}
