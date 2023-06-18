using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TabuSearch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //inputPath has to be changed with the own path of the input file!!!
            string inputPath = "C:\\Users\\Lukas\\Downloads\\TabuSearch\\bier127.tsp";
            FileHandler data1 = new FileHandler(inputPath);

            //create a sortednodesList and fill it with all nodes from the imported file
            SortedList<int, Node> sortedNodesList = new SortedList<int, Node>();
            sortedNodesList = data1.ReadInAllNodes();

          /*  //user should select a starting point (program will ask till a valid input is done)
            int startingIndex = 0;
            bool validInput = false;
            while (validInput == false)
            {
                try
                {
                    Console.Write($"Select a starting node (from 1 to {sortedPointsList.Count}):\t");   //user types in point ID
                    string userInput = Console.ReadLine();
                    startingIndex = int.Parse(userInput);
                    if (((startingIndex - 1) >= sortedPointsList.Count) || (startingIndex - 1) < 0)     //user has typed point ID so I adjust this input to the indices of the list
                    {
                        Console.WriteLine("The node ID you've selected is not available (out of range)! Try again.");
                        validInput = false;
                    }
                    else
                        validInput = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Your input is not valid (integer is required)! Try again.");
                }
            } */
            
            //FOR TESTING: CREATE AND PRINT THE INITIAL TOUR USED IN THE TABU SEARCH ALGORITHM - CAN BE DELETED
            ResultingTour initialTour = InitialSolution.ShortestNearestNeighbour(sortedNodesList);
            initialTour.PrintTourAndDistance();
            Console.WriteLine("");
            

            //set the tabu tenure
            int tabuTenure = 3;
            //test
            List < Node > adfa = new List<Node>();
            Operators.NodeShiftTwo(initialTour, TabuSearch.UpdateTabuList(tabuTenure, initialTour.Tour[0], adfa));
            //set maximal itinerations as a stopping criterion
            int maxItinerations = 100;
            int maxItinerationsWithoutImprovement = 20;

            //perform the tabu search, print out the tour after finishing the algorithm and print it out 
            ResultingTour newTour = TabuSearch.TabuSearchAlgorithm(tabuTenure, sortedNodesList, maxItinerations, maxItinerationsWithoutImprovement, 2);
            newTour.PrintTourAndDistance();            

            Console.ReadKey();
        }
    }
}
