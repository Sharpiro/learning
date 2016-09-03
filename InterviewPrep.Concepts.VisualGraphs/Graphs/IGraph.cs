using System.Collections.Generic;

namespace InterviewPrep.Concepts.VisualGraphs
{
    public interface IGraph
    {
        bool AreNodesAdjacent(string nodeOne, string nodeTwo);
        IEnumerable<string> FindAdjacentNodes(string node);
        IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo);
    }
}