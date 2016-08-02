using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public interface IGraph
    {
        bool AreNodesAdjacent(string nodeOne, string nodeTwo);
        List<string> FindAdjacentNodes(string node);
    }
}