using UnityEngine;
using System.Collections.Generic;
public abstract class Node: ScriptableObject
{
}
public abstract class Grid : ScriptableObject {

    public int Rows; 
    public int Cols;
    
    public List<Node> Nodes;
    public Node Destination;
    public abstract void Initialize(GameObject obj);
}

public abstract class GameState : ScriptableObject
{

}
