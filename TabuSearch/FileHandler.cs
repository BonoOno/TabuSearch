using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TabuSearch
{
    public class FileHandler
    {
        //properties for the file handler
        public string FilePath { get; set; }

        //constructor for the path of the file import
        public FileHandler(string filePath)    //filePath should be set in the main program; multiple classes file_handler can be created if multiple import files are available
        {
            FilePath = filePath;
        }

        //method to read all points from the import file and to create a sorted list with them (sorted by points' ids)
        public SortedList<int, Node> ReadInAllNodes()
        {
            //sorted list to store the nodes from the input file
            SortedList<int, Node> nodesList = new SortedList<int, Node>();

            //import data from a external file
            string[] importPoints = File.ReadAllLines(FilePath);

            //find the line where "NODE_COORD_SECTION" starts (header) and add 1 for the line after the header
            //benefit: code can be used for many different instances without having to adjust line indices etc.
            int startOfNodesSection = Array.FindIndex(importPoints, line => line.Contains("NODE_COORD_SECTION")) + 1;

            for (int i = startOfNodesSection; i < importPoints.GetLength(0); i++)
            {
                if (importPoints[i].StartsWith("EOF")) //if line in the input file starts with EOF, stop adding as the nodes are over
                    break;
                else
                {
                    string[] nodesArray = importPoints[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries); //split each line and have separately id, x and y for each node; RemoveEmptyEntires needed because some lines have 2 or 3 spaces between values
                    int id = int.Parse(nodesArray[0]);
                    int x = int.Parse(nodesArray[1]);
                    int y = int.Parse(nodesArray[2]);
                    Node node = new Node(id, x, y);
                    nodesList.Add(node.Id, node);
                }
            }

            //return the sorted list created from above by using the imported file
            return nodesList;
        }
    }
}