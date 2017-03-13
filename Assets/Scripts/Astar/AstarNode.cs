using System;
using AIE;

//Path Scoring

//The key to determining which squares to use when figuring out the path is the 
//following equation:

//F = G + H 
//where
//G = the movement cost to move from the starting point A to a given square on the grid, 
//following the path generated to get there. 
//H = the estimated movement cost to move from that given square on the grid 
//to the final destination,point B. 
//This is often referred to as the heuristic, which can be a bit confusing. 
//The reason why it is called that is because it is a guess.
//We really don’t know the actual distance until we find the path, 
//because all sorts of things can be in the way (walls, water, etc.). 
//You are given one way to calculate H in this tutorial, 
//but there are many others that you can find in other articles on the web.
/// <summary>
/// Node represenation of the search space
/// </summary>
namespace AIE
{
    public enum NodeState
    {
        None,
        Open,
        Closed,
    }
    [Serializable]
    public class AstarNode
    {
        
        public AstarNode(int u, int v, int id)
        {
            G = 0;
            H = 0;
            U = u;
            V = v;
            Id = id;
        }
        public int F
        {
            get { return G + H; }
        }

        public int G;
        public int H;
        public int Id;
        public int U;
        public int V;
        public AstarNode Parent;
        public override string ToString()
        {
            return string.Format("Node {0}", Id);
        }

    }

}