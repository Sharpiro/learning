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
            var adjacentNodes = vertex.Neighbors;
            return adjacentNodes.Select(v => v.Vertex.Name);
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var vertexOne = _adjacencyDictionary[nodeOne];
            var vertexTwo = _adjacencyDictionary[nodeTwo];
            return vertexOne.IsAdjacent(vertexTwo);
        }

        public IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo)
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

                foreach (var neighbor in minFringeItem.Vertex.Neighbors)
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

            var temp = tracker[nodeTwo].FindPath().Select(f => f.Vertex.Name).Reverse();
            return temp;
        }
    }

    public class Vertex
    {
        public string Name { get; set; }
        public List<Neighbor> Neighbors { get; set; } = new List<Neighbor>();

        public bool IsAdjacent(Vertex other) => Neighbors.Any(n => n.Vertex == other);
    }

    public class Neighbor
    {
        public Vertex Vertex { get; set; }
        public int Weight { get; set; }
    }

    public class FringeItem : IComparable<FringeItem>
    {
        public Vertex Vertex { get; set; }
        public int Distance { get; set; }
        public FringeItem LastItem { get; set; }

        public int CompareTo(FringeItem other) => Distance.CompareTo(other.Distance);

        public IList<FringeItem> FindPath()
        {
            var list = new List<FringeItem>();
            FindPath(this, list);
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