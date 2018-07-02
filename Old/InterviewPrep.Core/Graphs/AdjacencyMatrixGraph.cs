using System;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyMatrixGraph : IGraph
    {
        private readonly IList<string> _vertexList;
        private readonly IDictionary<string, SimpleVertex> _vertexDictionary;
        private readonly int[,] _adjacencyMatrix;

        public AdjacencyMatrixGraph(IDictionary<string, SimpleVertex> vertexDictionary, int[,] adjacencyMatrix)
        {
            _vertexDictionary = vertexDictionary;
            _vertexList = _vertexDictionary.Select(kvp => kvp.Key).ToList();
            _adjacencyMatrix = adjacencyMatrix;
        }

        public bool AreNodesAdjacent(string nodeOneName, string nodeTwoName)
        {
            var nodeOne = _vertexDictionary[nodeOneName].Index;
            var nodeTwo = _vertexDictionary[nodeTwoName].Index;
            var temp = _adjacencyMatrix[nodeOne, nodeTwo];
            return temp > 0;
        }

        public IEnumerable<string> FindAdjacentNodes(string nodeName)
        {
            var nodeIndex = _vertexDictionary[nodeName].Index;
            var adjacentNodes = new List<string>();
            var length = Math.Sqrt(_adjacencyMatrix.Length);
            for (var i = 0; i < length; i++)
            {
                var currentNode = _adjacencyMatrix[nodeIndex, i];
                if (currentNode > 0)
                {
                    adjacentNodes.Add(_vertexList[i]);
                }
            }
            return adjacentNodes;
        }

        public IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo)
        {
            throw new NotImplementedException();
        }
    }
}