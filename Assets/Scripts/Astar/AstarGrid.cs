using System;
using System.Collections.Generic;

namespace AIE
{    
    [Serializable]
    public class AstarGrid 
    {
        public AstarGrid(int r, int c)
        {
            Rows = r;
            Cols = c;
            Nodes = new List<AstarNode>();
            Generate();
        }

        public void Generate()
        {
            var positions = Utilities.CreateGrid(Cols, Rows);

            foreach(var p in positions)
            {
                var node = new AstarNode(p.U, p.V, Nodes.Count);                                
                Nodes.Add(node);
            }

            var r = new Random();
            Destination = Nodes[r.Next(0, Nodes.Count)];
            Nodes.ForEach(n => n.H = Utilities.ManhattanDistance(new Point(n.U, n.V), new Point(Destination.U, Destination.V)));
        }        

        
        public List<AstarNode> Nodes;
        public AstarNode Destination;
        /// <summary>
        /// retrieve neighbors given a node in the graph
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<AstarNode> Neighbors(AstarNode node)
        {
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
            var neighbors = new List<AstarNode>();
            foreach(var dir in dirs)
            {
                var pos = new Point(node.U + dir.U, node.V + dir.V);
                var nay = GetNode(pos);
                if(nay != null)
                    neighbors.Add(nay);
            }
                
            return neighbors;
        }
        public AstarNode GetNode(AstarNode an)
        {
            AstarNode node = Nodes.Find(n => n.U == an.U && n.V == an.V);
            return node;
        }
        
        public AstarNode GetNode(Point p)
        {
            AstarNode node = Nodes.Find(n => n.U == p.U && n.V == p.V);
            return node;
        }
 
        public int Rows
        {
            get;set;
        }

        public int Cols
        {
            get;set;
        }
 
        public void Clear()
        {
            Nodes.Clear();
            Nodes = new List<AstarNode>();
            Destination = null;
        }
    }
}