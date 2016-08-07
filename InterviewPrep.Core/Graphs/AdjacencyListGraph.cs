using System;
using System.Linq;
using System.Collections.Generic;

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
            var vertexOne = _adjacencyList[nodeOneIndex];
            var vertexTwo = _adjacencyList[nodeTwoIndex];
            return vertexOne.IsAdjacent(vertexTwo);
        }

        public IEnumerable<string> FindBestPath(string nodeOne, string nodeTwo)
        {
            var start = _vertexDictionary[nodeOne];
            var startVertex = _adjacencyList[start];
            var fringe = new List<FringeItem>();
            var tracker = new Dictionary<string, FringeItem>
            {
                ["A"] = new FringeItem { Vertex = startVertex, Distance = 0 }
            };
            //initial: add all children of start to first and set distance
            foreach (var neighbor in startVertex.FindNeighbors())
            {
                var fringeItem = new FringeItem
                {
                    PreviousItem = new FringeItem { Vertex = startVertex, Distance = 0 },
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
                fringe.Remove(minimumFringeItem);
                tracker.Add(minimumFringeItem.Vertex.Name, minimumFringeItem);
                var neighbors = minimumFringeItem.Vertex.FindNeighbors();
                foreach (var neighbor in neighbors)
                {
                    var neighborFringeItem = fringe.FirstOrDefault(fi => fi.Vertex == neighbor.Vertex);
                    //add
                    if (tracker.ContainsKey(neighbor.Vertex.Name))
                        continue;
                    if (neighborFringeItem == null)// || neighborFringeItem.Distance < 0)
                    {
                        var fringeItem = new FringeItem
                        {
                            PreviousItem = minimumFringeItem,
                            Distance = minimumFringeItem.Distance + neighbor.Weight,
                            Vertex = neighbor.Vertex
                        };
                        fringe.Add(fringeItem);
                    }
                    //update
                    else
                    {
                        var newDistance = minimumFringeItem.Distance + neighbor.Weight;
                        if (newDistance < neighborFringeItem.Distance)
                        {
                            neighborFringeItem.PreviousItem = minimumFringeItem;
                            neighborFringeItem.Distance = newDistance;
                        }
                    }
                }
            }
            var path = tracker[nodeTwo].GetPrevious().Select(fi => fi.Vertex.Name).Reverse();
            return path;
        }
    }

    public class FringeItem : IComparable<FringeItem>
    {
        public Vertex Vertex { get; set; }
        public FringeItem PreviousItem { get; set; }
        public int Distance { get; set; }

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
        public Vertex Vertex { get; set; }
        public Neighbor Next { get; set; }
        public int Weight { get; set; }
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

        public bool IsAdjacent(Vertex other)
        {
            var isAdjacent = IsAdjacent(Neighbors, other);
            return isAdjacent;
        }
        private bool IsAdjacent(Neighbor current, Vertex other)
        {
            if (current == null)
                return false;
            if (current.Vertex.Name == other.Name)
                return true;
            return IsAdjacent(current.Next, other);
        }
    }
}