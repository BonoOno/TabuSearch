using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public class ResultingTour
    {
        public double TourDistance { get; set; }
        public List<Node> Tour { get; set; }    //store points (with id, x, y) in the tour

        public ResultingTour(double tourDistance, List<Node> tourIndices)
        {
            TourDistance = tourDistance;
            Tour = tourIndices;
        }

        //method to print out tours (the points' IDs), their insertion method and their distance
        public void PrintTourAndDistance()
        {
            Console.Write("Tour:\t\t\t");
            for (int i = 0; i < Tour.Count; i++)
            {
                if (i < (Tour.Count - 1))
                    Console.Write($"{Tour[i].Id}->");
                else
                    Console.Write($"{Tour[i].Id}");   //no "->" should be printed after the last point
            }
            Console.WriteLine($"\nDistance:\t\t{Math.Round(TourDistance, 2)}");
        }
    }
}
