using System.Collections.Generic;
using UnityEngine;

public interface IGridBehaviour
{ 
    void ClearNodes();
    void CreateChildren();
    void CreateNodes();
    GameObject GetChild(ScriptableNode s);
    List<ScriptableNode> GetNeighbors(ScriptableNode node);
    void SetColor(ScriptableNode s, Color c);
    void SetColor(GameObject go, Color c);
    void SetGoal(ScriptableNode s);
    void SetStart(ScriptableNode s);
}