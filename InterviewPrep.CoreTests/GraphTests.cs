using InterviewPrep.Core.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class GraphTests
    {
        private readonly IGraph _graph;
        private readonly List<string> _vertexList;
        private readonly List<Edge> _edgeList;

        public GraphTests()
        {
            _vertexList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            _edgeList = new List<Edge>
            {
                new Edge { FirstNode = 1, SecondNode = 0, Weight = 5 },
                new Edge { FirstNode = 0, SecondNode = 2, Weight = 7 },
                new Edge { FirstNode = 0, SecondNode = 3, Weight = 3 },
                new Edge { FirstNode = 1, SecondNode = 4, Weight = 2 },
                new Edge { FirstNode = 1, SecondNode = 5, Weight = 10 },
                new Edge { FirstNode = 2, SecondNode = 6, Weight = 1 },
                new Edge { FirstNode = 3, SecondNode = 7, Weight = 11 },
                new Edge { FirstNode = 4, SecondNode = 7, Weight = 9 },
                new Edge { FirstNode = 5, SecondNode = 7, Weight = 4 },
                new Edge { FirstNode = 6, SecondNode = 7, Weight = 6 },
            };
            _graph = new LinearGraph(_vertexList, _edgeList);
        }

        /// <summary>
        /// find all nodes adjacent to a node
        /// </summary>
        [TestMethod]
        public void FindAdjacentNodes()
        {
            var node = _vertexList[0];
            foreach (var vertex in _vertexList)
            {
                var adjacentNodes = _graph.FindAdjacentNodes(vertex);
                foreach (var adjacentNode in adjacentNodes)
                {
                    Assert.IsTrue(_graph.AreNodesAdjacent(vertex, adjacentNode));
                }
            }
            var actual = _graph.FindAdjacentNodes(node);
            Assert.AreEqual(_vertexList[1], actual[0]);
            Assert.AreEqual(_vertexList[2], actual[1]);
            Assert.AreEqual(_vertexList[3], actual[2]);
        }

        /// <summary>
        /// determine if 2 nodes are connected
        /// </summary>
        [TestMethod]
        public void AreNodesAdjacent()
        {
            var nodeA = _vertexList[0];
            var nodeB = _vertexList[1];
            var nodeC = _vertexList[2];
            var nodeD = _vertexList[3];
            var nodeE = _vertexList[4];
            var nodeF = _vertexList[5];
            Assert.IsTrue(_graph.AreNodesAdjacent(nodeA, nodeB));
            Assert.IsTrue(_graph.AreNodesAdjacent(nodeA, nodeC));
            Assert.IsFalse(_graph.AreNodesAdjacent(nodeB, nodeC));

            Assert.IsTrue(_graph.AreNodesAdjacent(nodeB, nodeE));
            Assert.IsTrue(_graph.AreNodesAdjacent(nodeB, nodeF));
            Assert.IsFalse(_graph.AreNodesAdjacent(nodeE, nodeF));
        }
    }
}