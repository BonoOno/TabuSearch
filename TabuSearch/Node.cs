using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public class Node
    {
        public int Id { get; set; }
        public int X { get; set; }

        public int Y { get; set; }

        public Node(int id, int x, int y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        //method to calculate the distance between the point I will call this method for and the point "p" (input point for the method)
        public double DistanceToNode(Node p)
        {
            double distance = Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
            return distance;
        }
    }
}