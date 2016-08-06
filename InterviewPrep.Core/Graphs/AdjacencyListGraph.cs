using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyListGraph : IGraph
    {
        private readonly IDictionary<string, int> _vertexDictionary;
        private readonly List<Vertex> _adjacencyList;

        public int this[string index] => _vertexDictionary[index];

        public AdjacencyListGraph(IDictionary<string, int> vertexDictionary, List<Vertex> adjacencyList)
        {
            _adjacencyList = adjacencyList;
            _vertexDictionary = vertexDictionary;
        }

        public IEnumerable<string> FindAdjacentNodes(string node)
        {
            var nodeIndex = _vertexDictionary[node];
            var vertex = _adjacencyList[nodeIndex];
            var adjacentNodes = vertex.FindNeighbors();
            var namedList = adjacentNodes.Select(an => an.Vertex.Name);
            return namedList;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var nodeOneIndex = _vertexDictionary[nodeOne];
            var nodeTwoIndex = _vertexDictionary[nodeTwo];
            var vertex = _adjacencyList[nodeOneIndex];
            return vertex.IsAdjacent(nodeTwoIndex);
        }

        public IEnumerable<string> FindBestPath(string nodeOne, string nodeTwo)
        {
            var fringe = new List<FringeItem>();
            var start = _vertexDictionary[nodeOne];
            var end = _vertexDictionary[nodeTwo];
            var tracker = new List<FringeItem>();
            //initial: add all children of start to first and set distance
            foreach (var neighbor in _adjacencyList[start].FindNeighbors())
            {
                //var vertex = _adjacencyList[start];
                var fringeItem = new FringeItem
                {
                    PreviousItem = new FringeItem { Vertex = _adjacencyList[start], Distance = 0 },
                    Distance = neighbor.Weight,
                    Vertex = neighbor.Vertex
                };
                fringe.Add(fringeItem);
            }
            //while fringe is not empty
            //remove minimum distance vertex from the fringe
            //once a vertex is removed from the fringe, the shortest path to it has been found
            while (fringe.Any())
            {
                var minimumFringeItem = fringe.Min();
                tracker.Add(minimumFringeItem);
                fringe.Remove(minimumFringeItem);
                foreach (var neighbor in minimumFringeItem.Vertex.FindNeighbors())
                {
                    //if (neighbor.Vertex == minimumFringeItem.PreviousItem)
                    //    continue;
                    var temp = neighbor.Vertex.Name;
                    var temp2 = fringe.FirstOrDefault(fi => fi.Vertex.Name == temp);
                    if (temp2 == null || temp2.Distance < 0)
                    {
                        neighbor.Distance = minimumFringeItem.Distance + neighbor.Weight;
                        var fringeItem = new FringeItem
                        {
                            PreviousItem = minimumFringeItem,
                            Distance = minimumFringeItem.Distance + neighbor.Weight,
                            Vertex = neighbor.Vertex
                        };
                        fringe.Add(fringeItem);
                    }
                    else
                    {
                        var newDistance = minimumFringeItem.Distance + neighbor.Weight;
                        var minDistance = Math.Min(temp2.Distance, newDistance);
                        neighbor.Distance = minDistance;
                        var fringeItem = fringe.FirstOrDefault(fi => fi.Vertex == neighbor.Vertex);
                        fringeItem.PreviousItem = minimumFringeItem;
                        fringeItem.Distance = minDistance;
                        fringeItem.Vertex = neighbor.Vertex;
                    }
                }
            }
            var path = tracker.LastOrDefault().GetPrevious().Select(fi => fi.Vertex.Name).Reverse();
            return path;
        }
    }

    public class FringeItem : IComparable<FringeItem>
    {
        public Vertex Vertex { get; set; }
        public FringeItem PreviousItem { get; set; }
        public int Distance { get; set; } = -1;

        public ICollection<FringeItem> GetPrevious()
        {
            var list = new List<FringeItem>();
            GetPrevious(list, this);
            return list;
        }

        public void GetPrevious(ICollection<FringeItem> list, FringeItem current)
        {
            if (current == null)
                return;
            list.Add(current);
            GetPrevious(list, current.PreviousItem);
        }


        public int CompareTo(FringeItem other)
        {
            return this.Distance.CompareTo(other.Distance);
        }
    }

    public class Neighbor
    {
        //public int Index { get; set; }
        public Vertex Vertex { get; set; }
        public int Weight { get; set; }
        public Neighbor Next { get; set; }
        public int Distance { get; set; } = -1;
    }

    public class Vertex
    {
        public string Name { get; set; }
        public Neighbor Neighbors { get; set; }

        public void AddNeighbor(Neighbor newNeighbor)
        {
            AddNeighbor(Neighbors, newNeighbor);
        }
        private Neighbor AddNeighbor(Neighbor current, Neighbor newNeighbor)
        {
            if (current == null)
            {
                current = newNeighbor;
                if (Neighbors == null)
                    Neighbors = current;
            }
            else
                current.Next = AddNeighbor(current.Next, newNeighbor);
            return current;
        }

        public IList<Neighbor> FindNeighbors()
        {
            var list = new List<Neighbor>();
            FindNeighbors(list, Neighbors);
            return list;
        }
        private void FindNeighbors(ICollection<Neighbor> list, Neighbor current)
        {
            if (current == null)
                return;
            list.Add(current);
            FindNeighbors(list, current.Next);
        }

        public bool IsAdjacent(int otherNodeIndex)
        {
            var isAdjacent = IsAdjacent(Neighbors, otherNodeIndex);
            return isAdjacent;
        }
        private bool IsAdjacent(Neighbor current, int otherNodeIndex)
        {
            //if (current == null)
            //    return false;
            //if (current.Index == otherNodeIndex)
            //    return true;
            //return IsAdjacent(current.Next, otherNodeIndex);
            throw new NotImplementedException();
        }
    }
}