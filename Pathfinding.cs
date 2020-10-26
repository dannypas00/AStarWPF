using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Star_pathfinding
{
    class Pathfinding
    {
        public static List<Node> FindPath(Node start, Node end)
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(start);

            while (openSet.Count > 0)
            {
                Node node = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].GetFCost() <= node.GetFCost())
                        if (openSet[i].HCost < node.HCost)
                            node = openSet[i];
                }

                openSet.Remove(node);
                closedSet.Add(node);

                if (node == end)
                {
                    return RetracePath(start, end);
                }

                foreach (Node neighbour in node.GetNeighbours())
                {
                    if (!neighbour.IsTraversible() || closedSet.Contains(neighbour)) continue;
                    int newCostToNeighbour = node.GCost + node.DistanceToNode(neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                    {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = neighbour.DistanceToNode(end);
                        neighbour.parent = node;
                        if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                    }
                }
            }

            return null;
        }

        private static List<Node> RetracePath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            Node currentNode = end;
            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();

            return path;
        }
    }
}
