using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class LinearGraph : IGraph
    {
        private readonly List<string> _vertexList;
        private readonly List<Edge> _edgeList;

        public LinearGraph(List<string> vertexList, List<Edge> edgeList)
        {
            _vertexList = vertexList;
            _edgeList = edgeList;
        }

        public bool AreNodesConnected(string nodeOne, string nodeTwo)
        {
            throw new NotImplementedException();
        }

        public List<int> FindAdjacentNodes(string node)
        {
            var nodePosition = FindNodePosition(node);
            var adjacentNodes = new List<int>();
            for (var i = 0; i < _edgeList.Count; i++)
            {
                var edge = _edgeList[i];
                if (edge.FirstNode == nodePosition)
                {
                    adjacentNodes.Add(edge.SecondNode);
                }
                if (edge.SecondNode == nodePosition)
                {
                    adjacentNodes.Add(edge.FirstNode);
                }
            }
            return adjacentNodes;
        }

        public int FindNodePosition(string node)
        {
            for (int i = 0; i < _vertexList.Count; i++)
            {
                if (_vertexList[i] == node)
                    return i;
            }
            throw new ArgumentException("could not find the node specifid", nameof(node));
        }
    }

    public class Edge
    {
        public int FirstNode { get; set; }
        public int SecondNode { get; set; }
        public int Weight { get; set; }
    }
}