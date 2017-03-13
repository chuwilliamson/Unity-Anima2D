using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateScriptableGrid
{   // Use this for initialization
    public ScriptableGrid Create(List<ScriptableNode> nodes)
    {
        var sg = ScriptableObject.CreateInstance<ScriptableGrid>();
        sg.Nodes = new List<ScriptableNode>();
        foreach(var s in nodes)
        {
            AssetDatabase.AddObjectToAsset(s, sg);
            sg.Nodes.Add(s);
        }

        AssetDatabase.SaveAssets();
        return sg;        
    }
}
