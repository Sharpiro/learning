using System;
using System.Linq;
using System.Collections.Generic;

namespace InterviewPrep.Concepts.VisualGraphs
{
    public class AdjacencyListGraph
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
            return adjacentNodes.Select(v => v.Vertex.Name);
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var vertexOne = _adjacencyDictionary[nodeOne];
            var vertexTwo = _adjacencyDictionary[nodeTwo];
            return vertexOne.IsAdjacent(vertexTwo);
        }

        public IEnumerable<FringeItem> FindShortestPath(string nodeOne, string nodeTwo)
        {
            var tracker = new Dictionary<string, FringeItem>();
            var fringe = new List<FringeItem>
            {
                new FringeItem {Vertex = _adjacencyDictionary[nodeOne] }
            };

            while (fringe.Any())
            {
                var minFringeItem = fringe.Min();
                fringe.Remove(minFringeItem);
                tracker.Add(minFringeItem.Vertex.Name, minFringeItem);

                foreach (var neighbor in minFringeItem.Vertex.FindNeighbors())
                {
                    if (tracker.ContainsKey(neighbor.Vertex.Name)) continue;
                    var neighborFringeItem = fringe.FirstOrDefault(f => f.Vertex == neighbor.Vertex);
                    var newDistance = minFringeItem.Distance + neighbor.Weight;
                    if (neighborFringeItem == null)
                    {
                        fringe.Add(new FringeItem
                        {
                            Vertex = neighbor.Vertex,
                            LastItem = minFringeItem,
                            Distance = newDistance
                        });
                    }
                    else
                    {
                        if (newDistance >= neighborFringeItem.Distance) continue;
                        neighborFringeItem.Distance = newDistance;
                        neighborFringeItem.LastItem = minFringeItem;
                    }
                }
            }

            var temp = tracker[nodeTwo].FindPath();
            return temp;
        }
    }

    public class Vertex
    {
        public string Name { get; set; }
        public Neighbor Neighbors { get; set; }

        public IList<Neighbor> FindNeighbors()
        {
            var list = new List<Neighbor>();
            FindNeighbors(Neighbors, list);
            return list;
        }

        public void FindNeighbors(Neighbor neighbor, IList<Neighbor> list)
        {
            if (neighbor == null)
                return;
            list.Add(neighbor);
            FindNeighbors(neighbor.Next, list);
        }

        public void AddNeighbor(Neighbor newNeighbor)
        {
            Neighbors = AddNeighbor(newNeighbor, Neighbors);
        }

        public Neighbor AddNeighbor(Neighbor newNeighbor, Neighbor current)
        {
            if (current == null)
                current = newNeighbor;
            else
                current.Next = AddNeighbor(newNeighbor, current.Next);
            return current;
        }

        public bool IsAdjacent(Vertex other)
        {
            if (other == null)
                return false;
            return IsAdjacent(Neighbors, other);
        }

        public bool IsAdjacent(Neighbor current, Vertex other)
        {
            if (current == null)
                return false;
            if (current.Vertex == other)
                return true;
            return IsAdjacent(current.Next, other);
        }
    }

    public class Neighbor
    {
        public Vertex Vertex { get; set; }
        public Neighbor Next { get; set; }
        public int Weight { get; set; }
    }

    public class FringeItem : IComparable<FringeItem>
    {
        public Vertex Vertex { get; set; }
        public int Distance { get; set; }
        public FringeItem LastItem { get; set; }

        public int CompareTo(FringeItem other)
        {
            return this.Distance.CompareTo(other.Distance);
        }

        public IList<FringeItem> FindPath()
        {
            var list = new List<FringeItem>();
            FindPath(this, list);
            list.Reverse();
            return list;
        }

        public void FindPath(FringeItem current, IList<FringeItem> list)
        {
            if (current == null)
                return;
            list.Add(current);
            FindPath(current.LastItem, list);
        }
    }
}