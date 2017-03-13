using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIE;
public class GridBehaviour : MonoBehaviour, IGridBehaviour
{    
    private void Awake()
    {      
        Children.ForEach(child => child.GetComponent<NodeBehaviour>().gridBehaviour = this);
    }

    [HideInInspector]
    public GameObject prefab;
    public int Rows = 5;
    public int Cols = 5;
    public float Scale = 5;
    public float Offset = 5;
    public float Timer = 0;
    private void Update()
    {
        GameObject.Find("TimeText").GetComponent<UnityEngine.UI.Text>().text = "Time:" + Timer.ToString();
    }

    /*
        1. Find node closest to your position and declare it start node and put it on 
            the open list. 
        2. While there are nodes in the open list:
        3. Pick the node from the open list having the smallest F score. Put it on 
            the closed list (you don't want to consider it again).
        4. For each neighbor (adjacent cell) which isn't in the closed list:
        5. Set its parent to current node.
        6. Calculate G score (distance from starting node to this neighbor) and 
            add it to the open list
        7. Calculate F score by adding heuristics to the G value. 
     */

    private ScriptableNode Source;


    [SerializeField]
    [HideInInspector]
    private List<GameObject> Children = new List<GameObject>();
    [SerializeField]
    [HideInInspector]
    private List<ScriptableNode> Nodes = new List<ScriptableNode>();
    private List<ScriptableNode> Path = new List<ScriptableNode>();

    public void SetGoal(ScriptableNode goal)
    {
        if(!goal.Walkable || Source == null)
            return;
        Clear();
        SetColor(GetChild(goal), Color.green);
        StopAllCoroutines();
        Timer = 0;
        StartCoroutine(Astar(Source, goal, Path));
    }

    public void SetStart(ScriptableNode start)
    {
        Clear();
        start.Walkable = true;
        SetColor(GetChild(start), Color.green);
        Source = start;
    }
#region Astar
    public IEnumerator Astar(ScriptableNode start, ScriptableNode goal, List<ScriptableNode> cameFrom)
    {
        Timer = 0;
        cameFrom = new List<ScriptableNode>();
        var closedSet = new List<ScriptableNode>();
        var openSet = new List<ScriptableNode>();

        openSet.Add(start);
        SetColor(start, Color.cyan);
        start.H = heuristic_cost_estimate(start, goal);
        start.G = 0;
        start.F = start.G + start.H;
        while(openSet.Count > 0)
        {
            openSet.Sort((a, b) => a.F.CompareTo(b.F));
            var current = openSet[0]; //the node in openSet having the lowest fScore[] value
            if(current == goal)
            {
                cameFrom = reconstruct_path(goal);
                yield break;
            }

            openSet.Remove(current);
            closedSet.Add(current);
            SetColor(current, Color.grey);
            foreach(var neighbor in current.Neighbors)
            {
                //if in the closed list or not walkable ignore it 
                if(closedSet.Contains(neighbor) || !neighbor.Walkable)
                    continue; // Ignore the neighbor which is already evaluated.
                // The distance from start to a neighbor
                int tentative_gScore = current.G + dist_between(current, neighbor);
                //if it's not in the open list then add it 
                if(!openSet.Contains(neighbor)) // Discover a new node
                    openSet.Add(neighbor);
                else if(tentative_gScore >= neighbor.G)
                    continue; // This is not a better path.
                SetColor(neighbor, Color.cyan);
                neighbor.Parent = current; //This path is the best until now. Record it!
                neighbor.G = tentative_gScore;
                neighbor.F = neighbor.G + heuristic_cost_estimate(neighbor, goal);
            }

            Timer += Time.deltaTime;
            yield return null;
        }
    }

    public int heuristic_cost_estimate(ScriptableNode s, ScriptableNode g)
    {
        return Utilities.ManhattanDistance(s.U, s.V, g.U, g.V); ;
    }

    public List<ScriptableNode> reconstruct_path(ScriptableNode s)
    {
        var iterator = s;
        var p = new List<ScriptableNode>();
        while(iterator != Source)
        {
            p.Add(iterator);
            iterator = iterator.Parent;
        }
        p.Add(Source);
        p.ForEach(n => SetColor(GetChild(n), Color.yellow));
        return p;
    }

    public int dist_between(ScriptableNode current, ScriptableNode neighbor)
    {
        int cost = (current.U == neighbor.U || current.V == neighbor.V) ? 10 : 14;
        return cost;
    }
#endregion Astar

    public GameObject GetChild(ScriptableNode s)
    {     
        if(Children[s.Id].GetComponent<NodeBehaviour>().Node != s)
        {
            Debug.LogError("NodeBehaviour node mismatch");
            return null;
        }
        return Children[s.Id];
    }

    private void Clear()
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
        go.GetComponent<NodeBehaviour>().Tween();
    }

    #region Creation
    [ContextMenu("Clear")]
    public void ClearNodes()
    {
        Nodes.Clear();
        Nodes = new List<ScriptableNode>();

        if(Children != null)
        {
            if(Children.Count > 0)
                Children.ForEach(child => DestroyImmediate(child));
            Children.Clear();
        }
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
        CreateChildren();
    }

    [ContextMenu("Create Children")]
    public void CreateChildren()
    {
        foreach(var n in Nodes)
        {
            GameObject go = (prefab == null) ? GameObject.CreatePrimitive(PrimitiveType.Quad) : go = Instantiate(prefab);
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(n.U * Offset, n.V * Offset);
            go.transform.localScale *= Scale;
            go.transform.localRotation = Quaternion.identity;
            go.name = string.Format("Node {0}", n.Id);
            go.GetComponent<MeshRenderer>().receiveShadows = false;
            go.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            var nb = go.GetComponent<NodeBehaviour>();
            if(nb == null)
                nb = go.AddComponent<NodeBehaviour>();

            nb.Node = n;
            nb.gridBehaviour = this;
            Source = Nodes[0];
            Children.Add(go);
        }
    }

    public List<ScriptableNode> GetNeighbors(ScriptableNode node)
    {
        var neighbors = new List<ScriptableNode>();

        var dirs = new List<Point>()
        {
            new Point(1, 0),//right
            new Point(1, 1),//top right
            new Point(0, 1),//top
            new Point(-1, 1),//topleft
            new Point(-1, 0),//left
            new Point(-1, -1),//botleft
            new Point(0, -1),//bot
            new Point(1, -1),//botright
        };

        foreach(var dir in dirs)
        {
            var nay = Nodes.Find(n => n.U == node.U + dir.U && n.V == node.V + dir.V);
            if(nay == null)
                continue;
            neighbors.Add(nay);
        }

        return neighbors;
    }
    #endregion Creation
}
