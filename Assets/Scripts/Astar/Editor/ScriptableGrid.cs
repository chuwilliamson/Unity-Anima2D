using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 
public class ScriptableGrid : ScriptableObject
{
    public List<ScriptableNode> Nodes;
    public ScriptableGrid Create(List<ScriptableNode> nodes)
    {
        Nodes.Clear();   
        Nodes = new List<ScriptableNode>();        
        foreach(ScriptableNode s in nodes)
        {
            AssetDatabase.AddObjectToAsset(s, this);
            Nodes.Add(s);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        return this;
    }
}
