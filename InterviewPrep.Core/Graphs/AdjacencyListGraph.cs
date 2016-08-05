using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyListGraph : IGraph
    {
        private readonly IList<string> _vertexList;
        private readonly IDictionary<string, int> _vertexDictionary;
        private readonly List<Vertex> _adjacencyList;

        public int this[string index] => _vertexDictionary[index];

        public AdjacencyListGraph(IList<string> vertexList, List<Vertex> adjacencyList)
        {
            _vertexList = vertexList;
            _adjacencyList = adjacencyList;
            _vertexDictionary = new Dictionary<string, int>();
            InitializeDictionary();
        }

        public IEnumerable<string> FindAdjacentNodes(string node)
        {
            var nodeIndex = _vertexDictionary[node];
            var vertex = _adjacencyList[nodeIndex];
            var adjacentNodes = vertex.FindNeighbors();
            var namedList = adjacentNodes.Select(i => _vertexList[i]);
            return namedList;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var nodeOneIndex = _vertexDictionary[nodeOne];
            var nodeTwoIndex = _vertexDictionary[nodeTwo];
            var vertex = _adjacencyList[nodeOneIndex];
            return vertex.IsAdjacent(nodeTwoIndex);
        }

        public IList<string> FindBestPath(string nodeOne, string nodeTwo)
        {
            throw new NotImplementedException();
        }

        private void InitializeDictionary()
        {
            for (var i = 0; i < _vertexList.Count; i++)
            {
                _vertexDictionary.Add(_vertexList[i], i);
            }
        }

        public static AdjacencyListGraph CreateGraph(string graphData)
        {
            var reader = new StringReader(graphData);
            var adjacencyList = new List<Vertex>();
            int numberOfNodes;
            var parseResult = int.TryParse(reader.ReadLine()?.Trim(), out numberOfNodes);
            if (!parseResult)
                throw new ArgumentException("error parsing number of nodes from graph data", nameof(numberOfNodes));
            string data;
            var vertexList = new List<string>();
            for (var i = 0; i < numberOfNodes; i++)
            {
                var line = reader.ReadLine()?.Trim();
                vertexList.Add(line);
                adjacencyList.Add(new Vertex { Name = line });
            }
            var graph = new AdjacencyListGraph(vertexList, adjacencyList);
            while ((data = reader.ReadLine()) != null)
            {
                var edge = data.Trim().Split(' ');
                var index1 = graph[edge.FirstOrDefault()];
                var index2 = graph[edge.LastOrDefault()];
                adjacencyList[index1].AddNeighbor(new Neighbor { Index = index2 });
                adjacencyList[index2].AddNeighbor(new Neighbor { Index = index1 });
            }
            return graph;
        }
    }

    public class Neighbor
    {
        public int Index { get; set; }
        public int Weight { get; set; }
        public Neighbor Next { get; set; }
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

        public IList<int> FindNeighbors()
        {
            var list = new List<int>();
            FindNeighbors(list, Neighbors);
            return list;
        }
        private void FindNeighbors(ICollection<int> list, Neighbor current)
        {
            if (current == null)
                return;
            list.Add(current.Index);
            FindNeighbors(list, current.Next);
        }

        public bool IsAdjacent(int otherNodeIndex)
        {
            var isAdjacent = IsAdjacent(Neighbors, otherNodeIndex);
            return isAdjacent;
        }
        private bool IsAdjacent(Neighbor current, int otherNodeIndex)
        {
            if (current == null)
                return false;
            if (current.Index == otherNodeIndex)
                return true;
            return IsAdjacent(current.Next, otherNodeIndex);
        }
    }
}