using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public interface IGraph
    {
        bool AreNodesAdjacent(string nodeOne, string nodeTwo);
        IEnumerable<string> FindAdjacentNodes(string node);
        IEnumerable<string> FindShortestPath(string nodeOne, string nodeTwo);
    }
}