using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.Concepts.VisualGraphs
{
    public class GraphParser
    {
        private readonly TextReader _reader;
        private string _graphType;
        private readonly IDictionary<string, Vertex> _simpleVertexDictionary;
        private readonly List<Edge> _edgeList;

        public GraphParser(TextReader reader)
        {
            _reader = reader;
            _simpleVertexDictionary = new Dictionary<string, Vertex>();
            _edgeList = new List<Edge>();
            Parse();
        }

        private void Parse()
        {
            int numberOfNodes;
            var parseResult = int.TryParse(_reader.ReadLine()?.Trim(), out numberOfNodes);
            if (!parseResult)
                throw new ArgumentException("error parsing number of nodes from graph data", nameof(numberOfNodes));
            _graphType = _reader.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(_graphType))
                throw new ArgumentException("error parsing graph type from graph data", nameof(_graphType));
            for (var i = 0; i < numberOfNodes; i++)
            {
                var line = _reader.ReadLine()?.Trim();
                _simpleVertexDictionary.Add(line, new Vertex { Name = line });
            }
            string data;
            while ((data = _reader.ReadLine()) != null)
            {
                var edge = data.Trim().Split(' ');
                if (edge.Length != 3)
                    throw new ArgumentException($"error edges for line: {data}");
                int weight;
                parseResult = int.TryParse(edge[2], out weight);
                if (!parseResult)
                    throw new ArgumentException($"error parsing weight for edges {edge[0]} - {edge[1]}", nameof(numberOfNodes));
                _edgeList.Add(new Edge { FirstNode = _simpleVertexDictionary[edge[0]], SecondNode = _simpleVertexDictionary[edge[1]], Weight = weight });
            }
        }

        public AdjacencyListGraph CreateAdjacencyListGraph()
        {
            var adjacencyDictionary = _simpleVertexDictionary
                .Select(kvp => new KeyValuePair<string, Vertex>(kvp.Key, new Vertex
                {
                    Name = kvp.Value.Name
                })).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            foreach (var edge in _edgeList)
            {
                adjacencyDictionary[edge.FirstNode.Name].Neighbors.Add(new Neighbor { Vertex = adjacencyDictionary[edge.SecondNode.Name], Weight = edge.Weight });
                if (_graphType.ToLower() == "undirected")
                    adjacencyDictionary[edge.SecondNode.Name].Neighbors.Add(new Neighbor { Vertex = adjacencyDictionary[edge.FirstNode.Name], Weight = edge.Weight });
            }
            var graph = new AdjacencyListGraph(adjacencyDictionary);
            return graph;
        }
    }

    public class Edge : IEquatable<Edge>
    {
        public Vertex FirstNode { get; set; }
        public Vertex SecondNode { get; set; }
        public int Weight { get; set; }

        public bool Equals(Edge other)
        {
            return this.FirstNode == other.FirstNode && this.SecondNode == other.SecondNode;
        }
    }
}