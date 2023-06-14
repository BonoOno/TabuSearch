using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public static class Operators
    {
        /* method for node exchange best improvement; output: node1 and node2 for the best swap
         * Node1 will be put in the tabu list in the TabuSearch class (this node cannot be exchanged while it is in the tabuList)
         */
        public static Tuple<Node, Node> NodeExchangeBestImprovement(ResultingTour tour, List<Node> tabuList)
        {
            //helping variables to find beneficial swaps
            double bestKnownImprovedDistance = 1000000;
            Node FirstNodeForBestSwap = null;
            Node SecondNodeForBestSwap = null;
            Node swappedPoint = null;

            /* itinerate over the tour (except the last node=starting node due to simplicity for swap) for two nodes and
             * find the best nodes exchange without considering nodes in the tabu list
             */
            for (int i = 0; i < tour.Tour.Count - 1; i++)
            {
                if (tabuList.Any(node => node == tour.Tour[i]))
                    continue;                                      //don't consider (skip) node "i" in the tour if it is in the tabu list!
                else
                {
                    for (int j = 0; j < tour.Tour.Count - 1; j++)
                    {
                        if (tabuList.Any(node => node == tour.Tour[j]))
                            continue;                             //don't consider (skip) node "j" in the tour if it is in the tabu list!
                        else
                        {
                            if (i != j)     //to not swap the same node
                            {
                                /* copy the tour and add all points to the copied list except the last node (=starting node) for 
                                 * simplicity for the swap (otherwise messy if the last point should be swapped)
                                 * the idea of creating a new list copying the original tour at each itineration is to have the
                                 * original tour unmanipulated and to start with a fresh copy of the original tour because
                                 * we want to determine the only one best swap for the original tour
                                 */
                                List<Node> tourCopy = new List<Node>();
                                for (int node = 0; node < tour.Tour.Count - 1; node++)
                                    tourCopy.Add(tour.Tour[node]);
                                double tourDistance = 0;    //to calculate the distance of the copied tour

                                //swap points at positions i and j in the tour, using the copy of the original initial tour
                                swappedPoint = tourCopy[i];
                                tourCopy[i] = tourCopy[j];
                                tourCopy[j] = swappedPoint;

                                //add the starting node to the tour copy to be also the last node in order to complete the tour
                                tourCopy.Add(tourCopy.First());

                                //recalculate tour length (with last node=starting node) after exchanging nodes
                                for (int k = 0; k < tourCopy.Count; k++)
                                {
                                    if ((k + 1) < tourCopy.Count)   //to not get out of range
                                        tourDistance += tourCopy[k].DistanceToNode(tourCopy[k + 1]);
                                }

                                if (tourDistance < bestKnownImprovedDistance)   //in case of improvement
                                {
                                    bestKnownImprovedDistance = tourDistance;   //best known improved distance is updated with the new one best solution so far
                                    FirstNodeForBestSwap = swappedPoint;        //new first node for swap, contains node "i" before swapping
                                    SecondNodeForBestSwap = tourCopy[i];        //new first node for swap, contains node "j" before swapping
                                }
                            }
                            else
                                continue;   //skip if i=j because swapping the same point with itself doesn't make sense
                        }
                    }
                }
            }

            //return the two nodes whose swap will give the best result
            return Tuple.Create(FirstNodeForBestSwap, SecondNodeForBestSwap);
        }

        //method to perform the swap of nodes on a tour
        public static ResultingTour SwapNodes(ResultingTour tour, Node node1, Node node2)
        {
            //add the tour to the new list without the last node (=starting node) for simplicity for the swapping
            List<Node> improvedTour = new List<Node>();
            for (int node = 0; node < tour.Tour.Count - 1; node++)
                improvedTour.Add(tour.Tour[node]);

            //determine the index of the nodes for swap in the tour and perform the swap
            int firstIndex = improvedTour.IndexOf(node1);
            int secondIndex = improvedTour.IndexOf(node2);
            improvedTour[firstIndex] = node2;               //first node for swap becomes the second one
            improvedTour[secondIndex] = node1;              //second node for swap becomes the first one
            improvedTour.Add(improvedTour.First());         //add the first node to be the last to close the tour for the TSP

            //calculate the total tour length of the improved tour
            double improvedTourDistance = 0;
            for (int k = 0; k < improvedTour.Count; k++)
            {
                if ((k + 1) < improvedTour.Count)   //to not get out of range
                    improvedTourDistance += improvedTour[k].DistanceToNode(improvedTour[k + 1]);
            }

            //create an object of ResultingTour which is the output of this method
            ResultingTour improvedTourObject = new ResultingTour(improvedTourDistance, improvedTour);

            return improvedTourObject;
        }
    }
}