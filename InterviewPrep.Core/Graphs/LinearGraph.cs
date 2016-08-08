﻿using System;
using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class LinearGraph : IGraph
    {
        private readonly IList<string> _vertexList;
        private readonly IDictionary<string, int> _vertexDictionary;
        private readonly List<Edge> _edgeList;


        public LinearGraph(IList<string> vertexList, List<Edge> edgeList)
        {
            _vertexList = vertexList;
            _edgeList = edgeList;
            _vertexDictionary = new Dictionary<string, int>();
            InitializeDictionary();
        }

        public LinearGraph(IList<string> vertexList, IDictionary<string, int> vertexDictionary, List<Edge> edgeList)
        {
            _vertexList = vertexList;
            _edgeList = edgeList;
            _vertexDictionary = vertexDictionary;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            var positionOne = _vertexDictionary[nodeOne];
            var positionTwo = _vertexDictionary[nodeTwo];
            foreach (var edge in _edgeList)
            {
                if (edge.FirstNode == positionOne && edge.SecondNode == positionTwo)
                {
                    return true;
                }
                else if (edge.FirstNode == positionTwo && edge.SecondNode == positionOne)
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerable<string> FindAdjacentNodes(string node)
        {
            var nodePosition = _vertexDictionary[node];
            var adjacentNodes = new List<string>();
            foreach (var edge in _edgeList)
            {
                if (edge.FirstNode == nodePosition)
                {
                    adjacentNodes.Add(_vertexList[edge.SecondNode]);
                }
                if (edge.SecondNode == nodePosition)
                {
                    adjacentNodes.Add(_vertexList[edge.FirstNode]);
                }
            }
            return adjacentNodes;
        }

        public IEnumerable<string> FindBestPath(string nodeOne, string nodeTwo)
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
    }

    public class Edge : IEquatable<Edge>
    {
        public int FirstNode { get; set; }
        public int SecondNode { get; set; }
        public int Weight { get; set; }

        public bool Equals(Edge other)
        {
            return this.FirstNode == other.FirstNode && this.SecondNode == other.SecondNode;
        }
    }
}