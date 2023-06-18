using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public static class TabuSearch
    {
        public static List<Node> UpdateTabuList(int tabuTenure, Node nodeForTabu, List<Node> tabuList)
        {
            /* add the node for tabu in the tabu list; if the list has less items than the tabu tenure, just add
             * otherwise (number of items in the tabu list = tabu tenure => tabu list full) remove the first item in the tabu list 
             * (short memory) and add the node for tabu - works if tabuTenure not equal 0; if tabuTenure=0 => no action
             */
            if (tabuList.Count < tabuTenure)
                tabuList.Add(nodeForTabu);
            else
            {
                if (tabuTenure != 0)
                {
                    tabuList.RemoveAt(0);
                    tabuList.Add(nodeForTabu);
                }
                else { };       //if tabuTenure = 0 => don't do anything (always an empty tabu list)
            }

            return tabuList;
        }

        public static ResultingTour TabuSearchAlgorithm(int tabuTenure, SortedList<int, Node> nodesList, int maxItinerations , int maxIterationsWithoutImprovement)
        {
            //create and initialize an initial solution using the nearest neighbour algorithm, starting with node with id=3
            ResultingTour incumbentTour = InitialSolution.NearestNeighbourTour(2, nodesList);

            /* create ResultingTour as output of the TabuSearch and initialize it at the beginning with the initial tour
             * because it is the best known tour so far
             */
            ResultingTour bestTourAfterTabuSearch = incumbentTour;

            //create a tabu list and let it be empty while initializing it
            List<Node> tabuList = new List<Node>();

            //to track the nodes that are swapped by the operator
            Node node1 = null;
            Node node2 = null;

            //stopping criterion: itinerations
            int currentItineration = 1;
            int counterNoImprovements = 0;

            while (currentItineration <= maxItinerations && counterNoImprovements <= maxIterationsWithoutImprovement)
            {
               /* //FOR TESTING (SOLUTION TRACKING: tabu list)
                if (tabuList.Count == 0)
                    Console.Write("TL: empty");
                else
                    Console.Write("TL: ");
                for (int i = 0; i < tabuList.Count; i++)
                {
                    if (i < (tabuList.Count - 1))
                        Console.Write($"{tabuList[i].Id},");
                    else
                        Console.Write($"{tabuList[i].Id}");     //if last entry => no comma
                }
                Console.WriteLine("");
                //END TESTING */

                /* run the operator "node exchange best improvement"
                 * nodes 1 and 2 should be the first and second results from the operator (the two nodes that should be swapped)
                 */
                Tuple<Node, Node> operatorResult = Operators.NodeExchangeBestImprovement(incumbentTour, tabuList);
                node1 = operatorResult.Item1;
                node2 = operatorResult.Item2;

                //update tabu list - node1 should be added in the tabu list
                tabuList = UpdateTabuList(tabuTenure, node1, tabuList);

                /* perform the swapping of nodes on the incumbent tour (current tour in the current itineration)
                 * NOTE: in the method the tour distance is updated as well
                 */
                incumbentTour = Operators.SwapNodes(incumbentTour, node1, node2);

                /*  //FOR TESTING (SOLUTION TRACKING: nodes to be swapped)
                  Console.Write("Nodes for swap: ");
                  Console.WriteLine($"{node1.Id} -> {node2.Id}");

                  //FOR TESTING (SOLUTION TRACKING: tour after operator)
                  foreach (Node n in incumbentTour.Tour)
                      Console.Write($"{n.Id}->");
                  Console.WriteLine($"\n{Math.Round(incumbentTour.TourDistance, 2)}");
                  Console.WriteLine("");
                  //END TESTING */

                /* if the tour manipulated by the applied operator has a better fitness value than the best known tour so far, 
                 * this tour should become the best known tour so far
                 */
                if (incumbentTour.TourDistance < bestTourAfterTabuSearch.TourDistance)
                { bestTourAfterTabuSearch = incumbentTour;
                    counterNoImprovements = 0;
                }               
                else
                {
                    counterNoImprovements++;
                }

                //increase the itineration by one to move to the next itineration
                currentItineration++;
            }

            //return the best found tour
            return bestTourAfterTabuSearch;
        }
    }
}