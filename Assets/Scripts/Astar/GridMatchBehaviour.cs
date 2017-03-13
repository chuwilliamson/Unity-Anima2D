using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIE;
using System;

public class GridMatchBehaviour : MonoBehaviour, IGridBehaviour
{
    public int Rows;
    public int Cols;
    public float Offset;
    public float Scale;
    public List<GameObject> Children;
    public List<ScriptableNode> Nodes;
    public GameObject prefab;
    public ScriptableNode Current;
    public ScriptableNode Goal;
    public void Awake()
    {
        Children.ForEach(child => SetColor(child, Color.white));
        Children.ForEach(child => child.GetComponent<NodeBehaviour>().gridBehaviour = this);
    }
    public GameObject GetChild(ScriptableNode s)
    {
        if(Children[s.Id].GetComponent<NodeBehaviour>().Node != s)
        {
            Debug.LogError("node behaviour node mismatch");
            return null;
        }
        return Children[s.Id];
    }

    public void Clear()
    {
        Children.ForEach(child => SetColor(child, Color.white));
        foreach(var n in Nodes)
        {
            if(!n.Walkable)
                SetColor(GetChild(n), Color.red);
        }

        Nodes.ForEach(node => node.Parent = null);
    }

    public void SetColor(GameObject go, Color c)
    {
        go.GetComponent<MeshRenderer>().material.color = c;
    }

    public void SetColor(ScriptableNode s, Color c)
    {
        GameObject go = GetChild(s);
        go.GetComponent<MeshRenderer>().material.color = c;
    }

    [ContextMenu("Clear")]
    public void ClearNodes()
    {
        if(Nodes.Count > 0)
            Nodes.ForEach(node => DestroyImmediate(node, true));
        Nodes.Clear();

        if(Children.Count > 0)
            Children.ForEach(child => DestroyImmediate(child));
        Children.Clear();
    }

    [ContextMenu("Create Nodes")]
    public void CreateNodes()
    {
        ClearNodes();
        Children = new List<GameObject>();
        Nodes = new List<ScriptableNode>();
        var positions = Utilities.CreateGrid(Rows, Cols);

        foreach(var p in positions)
        {
            var node = ScriptableObject.CreateInstance<ScriptableNode>();
            node.Initialize(new AstarNode(p.U, p.V, Nodes.Count));
            node.name = string.Format("Node {0}", Nodes.Count.ToString());
            Nodes.Add(node);
        }

        Nodes.ForEach(n => n.Neighbors = GetNeighbors(n));
        Nodes.ForEach(n => n.Walkable = true);
        Current = Nodes[0];
        Goal = Nodes[Nodes.Count - 1];
        CreateChildren();
    }

    public void CreateChildren()
    {
        foreach(var n in Nodes)
        {
            GameObject go = (prefab == null) ? GameObject.CreatePrimitive(PrimitiveType.Quad) : go = Instantiate(prefab);
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(n.U * Offset, n.V * Offset);
            go.transform.localScale *= Scale;
            go.name = string.Format("Node {0}", n.Id);

            var nb = go.GetComponent<NodeBehaviour>();
            if(nb == null)
                nb = go.AddComponent<NodeBehaviour>();

            nb.Node = n;
            nb.gridBehaviour = this;
            Children.Add(go);
        }
    }

    public List<ScriptableNode> GetNeighbors(ScriptableNode node)
    {
        var neighbors = new List<ScriptableNode>();
        var dirs = new List<Point>()
            {
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1),
                new Point(-1, 1),
                new Point(-1, 0),
                new Point(-1, -1),
                new Point(0, -1),
                new Point(1, -1),
            };

        foreach(var dir in dirs)
        {
            var nay = Nodes.Find(n => n.U == node.U + dir.U && n.V == node.V + dir.V);
            if(nay != null)
                neighbors.Add(nay);
        }

        return neighbors;
    }

    public void SetGoal(ScriptableNode s)
    {
        throw new NotImplementedException();
    }

    public void SetStart(ScriptableNode s)
    {
        throw new NotImplementedException();
    }
}
