using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public class AdjacencyMatrixGraph : IGraph
    {
        private readonly IDictionary<string, int> _vertexDictionary;
        private int[][] _adjacencyMatrix;

        public AdjacencyMatrixGraph(IDictionary<string, int> vertexDictionary, int[][] adjacencyMatrix)
        {
            _vertexDictionary = vertexDictionary;
            _adjacencyMatrix = adjacencyMatrix;
        }

        public bool AreNodesAdjacent(string nodeOne, string nodeTwo)
        {
            throw new System.NotImplementedException();
        }

        public List<string> FindAdjacentNodes(string node)
        {
            throw new System.NotImplementedException();
        }
    }
}