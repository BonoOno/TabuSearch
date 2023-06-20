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
        public double X { get; set; }

        public double Y { get; set; }

        public Node(int id, double x, double y)
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

        public override string ToString()
        {
            return $"{Id}";
        }
    }
}