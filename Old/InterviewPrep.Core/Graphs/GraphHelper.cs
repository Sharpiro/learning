using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.Core.Graphs
{
    public class GraphHelper
    {
        private readonly TextReader _reader;
        private string _graphType;
        private readonly IDictionary<string, SimpleVertex> _simpleVertexDictionary;
        private readonly List<Edge> _edgeList;

        public IEnumerable<string> VertexList => _simpleVertexDictionary.Select(kvp => kvp.Value.Name);

        public GraphHelper(TextReader reader)
        {
            _reader = reader;
            _simpleVertexDictionary = new Dictionary<string, SimpleVertex>();
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
                _simpleVertexDictionary.Add(line, new SimpleVertex { Name = line, Index = i });
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

        public IGraph CreateLinearGraph()
        {
            var linearGraph = new LinearGraph(_simpleVertexDictionary, _edgeList);
            return linearGraph;
        }

        public IGraph CreateAdjacencyMatrixGraph()
        {
            var listLength = _simpleVertexDictionary.Count;
            var adjacencyMatrix = new int[listLength, listLength];
            foreach (var edge in _edgeList)
            {
                var firstIndex = _simpleVertexDictionary[edge.FirstNode.Name].Index;
                var secondIndex = _simpleVertexDictionary[edge.SecondNode.Name].Index;
                adjacencyMatrix[firstIndex, secondIndex] = edge.Weight;
                adjacencyMatrix[secondIndex, firstIndex] = edge.Weight;
            }
            var graph = new AdjacencyMatrixGraph(_simpleVertexDictionary, adjacencyMatrix);
            return graph;
        }

        public IGraph CreateAdjacencyListGraph()
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
}
