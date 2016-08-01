using System.Collections.Generic;

namespace InterviewPrep.Core.Graphs
{
    public interface IGraph
    {
        List<int> FindAdjacentNodes(string node);
        bool AreNodesConnected(string nodeOne, string nodeTwo);
    }
}