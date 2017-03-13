using UnityEngine;
using System.Collections.Generic;
using AIE;
public class ScriptableNode : ScriptableObject
{
    public int Id;
    public int U;
    public int V;
    public int G;
    public int H;    
    public int F;
    public bool Walkable;

    public ScriptableNode Parent;

    public List<ScriptableNode> Neighbors;

    public void Initialize(AstarNode n)
    {
        
        Parent = null;
        Neighbors = new List<ScriptableNode>();
        Walkable = true;
        G = n.G;
        H = n.H;
        U = n.U;
        V = n.V;
        Id = n.Id;
        
    } 
}
