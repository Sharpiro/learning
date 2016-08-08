using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InterviewPrep.Core.Graphs
{
    public class GraphHelper
    {
        private readonly string _graphData;
        private readonly IList<string> _vertexList;
        private readonly IDictionary<string, int> _vertexDictionary;
        private readonly List<Edge> _edgeList;
        private string _graphType;

        public IList<string> VertexList => _vertexList;

        public GraphHelper(string graphData)
        {
            _graphData = graphData;
            _vertexList = new List<string>();
            _vertexDictionary = new Dictionary<string, int>();
            _edgeList = new List<Edge>();
            Parse();
        }

        private void Parse()
        {
            var reader = new StringReader(_graphData);
            int numberOfNodes;
            var parseResult = int.TryParse(reader.ReadLine()?.Trim(), out numberOfNodes);
            if (!parseResult)
                throw new ArgumentException("error parsing number of nodes from graph data", nameof(numberOfNodes));
            _graphType = reader.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(_graphType))
                throw new ArgumentException("error parsing graph type from graph data", nameof(_graphType));
            for (var i = 0; i < numberOfNodes; i++)
            {
                var line = reader.ReadLine()?.Trim();
                _vertexList.Add(line);
                _vertexDictionary.Add(line, i);
            }
            string data;
            while ((data = reader.ReadLine()) != null)
            {
                var edge = data.Trim().Split(' ');
                if (edge.Length != 3)
                    throw new ArgumentException($"error edges for line: {data}");
                var index1 = _vertexDictionary[edge[0]];
                var index2 = _vertexDictionary[edge[1]];
                int weight;
                parseResult = int.TryParse(edge[2], out weight);
                if (!parseResult)
                    throw new ArgumentException($"error parsing weight for edges {edge[0]} - {edge[1]}", nameof(numberOfNodes));
                _edgeList.Add(new Edge { FirstNode = index1, SecondNode = index2, Weight = weight });
            }
        }

        public IGraph CreateEdgeListGraph()
        {
            var linearGraph = new LinearGraph(_vertexList, _vertexDictionary, _edgeList);
            return linearGraph;
        }

        public IGraph CreateAdjacencyMatrixGraph()
        {
            var length = _vertexList.Count;
            var adjacencyMatrix = new int[length, length];
            foreach (var edge in _edgeList)
            {
                adjacencyMatrix[edge.FirstNode, edge.SecondNode] = edge.Weight;
                adjacencyMatrix[edge.SecondNode, edge.FirstNode] = edge.Weight;
            }
            var graph = new AdjacencyMatrixGraph(_vertexList, adjacencyMatrix);
            return graph;
        }

        public IGraph CreateAdjacencyListGraph()
        {
            var adjacencyList = _vertexList
                .Select(v => new KeyValuePair<string, Vertex>(v, new Vertex { Name = v }))
                .ToList();

            foreach (var edge in _edgeList)
            {
                var firstNode = edge.FirstNode;
                var secondNode = edge.SecondNode;
                var weight = edge.Weight;
                adjacencyList[firstNode].Value.AddNeighbor(new Neighbor { Vertex = adjacencyList[secondNode].Value, Weight = weight });
                if (_graphType.ToLower() == ("undirected"))
                    adjacencyList[secondNode].Value.AddNeighbor(new Neighbor { Vertex = adjacencyList[firstNode].Value, Weight = weight });
            }
            var adjacencyDictionary = adjacencyList.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var graph = new AdjacencyListGraph(adjacencyDictionary);
            return graph;
        }
    }
}
