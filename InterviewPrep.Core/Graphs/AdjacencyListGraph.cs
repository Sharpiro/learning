using System;
using System.Linq;
using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyListGraph : IGraph
    {
        private readonly IDictionary<string, Vertex> _adjacencyDictionary;

        public Vertex this[string index] => _adjacencyDictionary[index];

        public AdjacencyListGraph(IDictionary<string, Vertex> adjacencyDictionary)
        {
            _adjacencyDictionary = adjacencyDictionary;
        }

        public IEnumerable<string> FindAdjacentNodes(string node)
        {
            var vertex = _adjacencyDictionary[node];
            var adjacentNodes = vertex.FindNeighbors();
            var namedList = adjacentNodes.Select(an => an.Vertex.Name);
            return namedList;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var vertexOne = _adjacencyDictionary[nodeOne];
            var vertexTwo = _adjacencyDictionary[nodeTwo];
            return vertexOne.IsAdjacent(vertexTwo);
        }

        public IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo)
        {
            var startVertex = _adjacencyDictionary[nodeOne];
            //initial: add start to fringe by default
            var fringe = new List<FringeItem>
            {
                new FringeItem {Distance = 0, Vertex = startVertex}
            };
            var tracker = new Dictionary<string, FringeItem>();

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
                    if (tracker.ContainsKey(neighbor.Vertex.Name)) continue;
                    //add
                    if (neighborFringeItem == null)//if a vertex has not been evaluated yet
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
                        if (newDistance >= neighborFringeItem.Distance) continue;
                        neighborFringeItem.PreviousItem = minimumFringeItem;
                        neighborFringeItem.Distance = newDistance;
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