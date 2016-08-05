using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyMatrixGraph : IGraph
    {
        private readonly List<string> _vertexList;
        private readonly IDictionary<string, int> _vertexDictionary;
        private int[][] _adjacencyMatrix;

        public AdjacencyMatrixGraph(List<string> vertexList, int[][] adjacencyMatrix)
        {
            _vertexList = vertexList;
            _adjacencyMatrix = adjacencyMatrix;
            _vertexDictionary = new Dictionary<string, int>();
            InitializeDictionary();
        }

        public bool AreNodesAdjacent(string nodeOneName, string nodeTwoName)
        {
            var nodeOne = _vertexDictionary[nodeOneName];
            var nodeTwo = _vertexDictionary[nodeTwoName];
            var temp = _adjacencyMatrix[nodeOne][nodeTwo];
            var temp2 = _adjacencyMatrix[nodeTwo][nodeOne];
            return temp > 0;
        }

        public IEnumerable<string> FindAdjacentNodes(string nodeName)
        {
            var nodeIndex = _vertexDictionary[nodeName];
            var adjacentNodes = new List<string>();
            for (var i = 0; i < _adjacencyMatrix[nodeIndex].Length; i++)
            {
                var currentNode = _adjacencyMatrix[nodeIndex][i];
                if (currentNode > 0)
                {
                    adjacentNodes.Add(_vertexList[i]);
                }
            }
            return adjacentNodes;
        }

        public IList<string> FindBestPath(string nodeOne, string nodeTwo)
        {
            throw new System.NotImplementedException();
        }

        private void InitializeDictionary()
        {
            for (var i = 0; i < _vertexList.Count; i++)
            {
                _vertexDictionary.Add(_vertexList[i], i);
            }
        }
    }
}