using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public static class InitialSolution
    {
        //method to generate a tour considering the nearest neighbour with an output object from class ResultingTour
        public static ResultingTour NearestNeighbourTour(int startingIndex, SortedList<int, Node> pointsList)
        {
            //int list to store the tour created -> points will be stored (the IDs will be printed for the tour)
            List<Node> tour = new List<Node>();
            //variable to store the distance of the tour
            double tourDistance = 0;

            //first point of the tour should be the starting point
            tour.Add(pointsList.Values[startingIndex]);

            //create a list to store the non-visited points which will be considered for the next point (nearest neighbour)
            List<Node> nonVisitedPoints = new List<Node>();
            //add all points to the list
            foreach (Node p in pointsList.Values)
            {
                nonVisitedPoints.Add(p);
            }
            //remove the starting point from this list as this point is already visited
            nonVisitedPoints.Remove(tour[0]);

            //variables to determine the nearest neighbour (find the minimal distance and the nearest neighbour)
            double minimalDistance = 1000000000;
            double distanceToNeighbour = 0;
            Node nextNeighbour = null;

            /* loop to generate the tour - it works till the nonVisitedPoints list has no points anymore 
             * meaning: all points are visited
             * loop will not go to the first point after the last point, this will be coded after the while loop
             */
            while (nonVisitedPoints.Count > 0)
            {
                for (int i = 1; i < pointsList.Count; i++)  //starting to determine the tour from the second point as the first one (index 0) is defined already
                {
                    /* loop to determine the nearest neighbour (smallest distance) of the current point
                     * it goes through all points in the nonVisitedPoints-list and compares the distance between current point and
                     * each point in the nonVisitedPoints-list
                     */
                    for (int j = 0; j < nonVisitedPoints.Count; j++)
                    {
                        //determine the possible next point for the tour in each itineration and calculate the distance to it from the current point 
                        Node neighbour = nonVisitedPoints[j];
                        distanceToNeighbour = tour[i - 1].DistanceToNode(neighbour);   //previous point from the tour and the potential one for a following point

                        /* If the minimal distance we have found is higher/equal than the distance between the two checked
                         * points now, the distance between these two points should become the new minimal distance.
                         * The distanceToNeighbour should NOT be 0 because if it is 0, it will mean that we have calculated the 
                         * distance of a point with itself. However, a point cannot be a neighbour to itself so this case
                         * should be excluded (there is no logic to have 2 different points with the same coordinates).
                         */
                        if (minimalDistance >= distanceToNeighbour && distanceToNeighbour != 0)
                        {
                            minimalDistance = distanceToNeighbour;
                            //nearest neighbour that has to be the next point for the tour
                            nextNeighbour = neighbour;  //neighbour = nonVisitedPoints[j]
                        }
                    }

                    //add point ID of the nearest neighbour to our tour
                    tour.Add(nextNeighbour);

                    //remove next point in the tour (nearest neighbour) from the nonVisitedPoints-list as this point is already visited
                    nonVisitedPoints.Remove(nextNeighbour);

                    //reinitialize "minimalDistance" with high number to be able to find the nearest neighbour in the next itineration
                    minimalDistance = 1000000000;

                    //Point that stores the next neighbour is reinitialized to be empty again
                    nextNeighbour = null;
                }
            }

            //last point should be the first point of the tour, so add it again to the list (will be placed at the end as last item)
            tour.Add(tour[0]);

            //determine the total length of the tour
            for (int i = 0; i < tour.Count; i++)
            {
                if ((i + 1) < tour.Count)   //to not get out of range
                    tourDistance += tour[i].DistanceToNode(tour[i + 1]);
            }

            //return nearestNeighbourTour from ResultingTour where the tour distnace and the tour are stored
            ResultingTour nearestNeighbourTour = new ResultingTour(tourDistance, tour);
            return nearestNeighbourTour;
        }

        //method to generate a tour considering a random starting with an output object from class ResultingTour
        public static ResultingTour RandomTour(int startingIndex, SortedList<int, Node> pointsList)
        {
            //int list to store the tour created -> points will be stored (the IDs will be printed for the tour)
            List<Node> tour = new List<Node>();
            //variable to store the distance of the tour
            double tourDistance = 0;

            //first point of the tour should be the starting point
            tour.Add(pointsList.Values[startingIndex]);

            //create a list to store the non-visited points which will be considered for the next point
            List<Node> nonVisitedPoints = new List<Node>();
            //add all points to the list
            foreach (Node p in pointsList.Values)
            {
                nonVisitedPoints.Add(p);
            }
            //remove the starting point from this list as this point is already visited
            nonVisitedPoints.Remove(tour[0]);

            //next neighbour point will be stored
            Node nextNeighbour = null;

            /* loop to generate the tour - it works till the nonVisitedPoints list has no points anymore 
             * meaning: all points are visited
             * loop will not go to the first point after the last point, this will be coded after the while loop
             */
            while (nonVisitedPoints.Count > 0)
            {
                for (int i = 1; i < pointsList.Count; i++)  //starting to determine the tour from the second point as the first one (index 0) is defined already
                {
                    //determining the next point for the tour randomly from the list with non-visited points
                    Random rnd = new Random();
                    int nextRandomListIndex = rnd.Next(0, nonVisitedPoints.Count);
                    nextNeighbour = nonVisitedPoints[nextRandomListIndex];

                    //add point ID of the next neighbour to our tour
                    tour.Add(nextNeighbour);

                    //remove next point in the tour (next neighbour) from the nonVisitedPoints-list as this point is already visited
                    nonVisitedPoints.Remove(nextNeighbour);

                    //Point that stores the next neighbour is reinitialized to be empty again
                    nextNeighbour = null;
                }
            }

            //last point should be the first point of the tour, so add it again to the list (will be placed at the end as last item)
            tour.Add(tour[0]);

            //determine the total length of the tour
            for (int i = 0; i < tour.Count; i++)
            {
                if ((i + 1) < tour.Count)   //to not get out of range
                    tourDistance += tour[i].DistanceToNode(tour[i + 1]);
            }

            //return RandomTour from ResultingTour where the tour distance and the tour are stored
            ResultingTour randomTour = new ResultingTour(tourDistance, tour);
            return randomTour;
        }
    }
}