using System;
using System.Collections.Generic;
using UnityEngine;
namespace AIE
{
    [Serializable]
    public class Point
    {
        //coordinate system is -> u : right
        //                     ^  v : up 
        public int U;
        public int V;
        public Point(int u, int v)
        {
            U = u;
            V = v;
        }
        public override string ToString()
        {
            return string.Format("{0},{1}", U, V);
        }
    }

    public class Utilities
    {
        public static List<Point> CreateGrid(int rows, int cols)
        {
            var nodes = new List<Point>();
            for(int u = 0; u < cols; u++)
                for(int v = 0; v < rows; v++)
                    nodes.Add(new Point(u,v));
            return nodes;
        }

        /// <summary>
        /// Get the manhattan distance between two points
        /// </summary>
        /// <param name="src">the current node</param>
        /// <param name="dest">the destination node</param>
        /// <returns></returns>
        public static int ManhattanDistance(Point src, Point dest)
        {
            float x = Mathf.Abs(dest.U - src.U);
            float y = Mathf.Abs(dest.V - src.V);
            return ((int)x + (int)y) * 10;
        }

        public static int ManhattanDistance(int u1, int v1, int u2, int v2)
        {
            int x = Mathf.Abs(u2 - u1);
            int y = Mathf.Abs(v2 - v1);
            return (x + y) * 10;
        }
    }
}