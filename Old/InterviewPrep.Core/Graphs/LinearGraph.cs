using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class LinearGraph : IGraph
    {
        private readonly IDictionary<string, SimpleVertex> _vertexDictionary;
        private readonly List<Edge> _edgeList;


        public LinearGraph(IDictionary<string, SimpleVertex> vertexDictionary, List<Edge> edgeList)
        {
            _edgeList = edgeList;
            _vertexDictionary = vertexDictionary;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            foreach (var edge in _edgeList)
            {
                if (edge.FirstNode == _vertexDictionary[nodeOne] && edge.SecondNode == _vertexDictionary[nodeTwo])
                {
                    return true;
                }
                else if (edge.FirstNode == _vertexDictionary[nodeTwo] && edge.SecondNode == _vertexDictionary[nodeOne])
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<string> FindAdjacentNodes(string node)
        {
            var adjacentNodes = new List<string>();
            foreach (var edge in _edgeList)
            {
                if (edge.FirstNode == _vertexDictionary[node])
                {
                    adjacentNodes.Add(edge.SecondNode.Name);
                }
                if (edge.SecondNode == _vertexDictionary[node])
                {
                    adjacentNodes.Add(edge.FirstNode.Name);
                }
            }
            return adjacentNodes;
        }

        public IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo)
        {
            throw new NotImplementedException();
        }
    }

    public class Edge : IEquatable<Edge>
    {
        public SimpleVertex FirstNode { get; set; }
        public SimpleVertex SecondNode { get; set; }
        public int Weight { get; set; }

        public bool Equals(Edge other)
        {
            return this.FirstNode == other.FirstNode && this.SecondNode == other.SecondNode;
        }
    }

    public class SimpleVertex
    {
        public int Index { get; set; }
        public string Name { get; set; }
    }
}