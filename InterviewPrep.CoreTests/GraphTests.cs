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
        private readonly List<Edge> _linearEdgeList;
        private readonly int[][] _adjacencyMatrix;

        public GraphTests()
        {
            _vertexList = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            _linearEdgeList = new List<Edge>
            {
                new Edge { FirstNode = 0, SecondNode = 1, Weight = 5 },
                new Edge { FirstNode = 0, SecondNode = 2, Weight = 7 },
                new Edge { FirstNode = 0, SecondNode = 3, Weight = 3 },
                new Edge { FirstNode = 1, SecondNode = 4, Weight = 2 },
                new Edge { FirstNode = 1, SecondNode = 5, Weight = 10 },
                new Edge { FirstNode = 2, SecondNode = 6, Weight = 1 },
                new Edge { FirstNode = 3, SecondNode = 7, Weight = 11 },
                new Edge { FirstNode = 4, SecondNode = 7, Weight = 9 },
                new Edge { FirstNode = 5, SecondNode = 7, Weight = 4 },
                new Edge { FirstNode = 6, SecondNode = 7, Weight = 6 }
            };
            _adjacencyMatrix = new int[][]
            {
               new int[] { 0, 5,  7, 3,  0, 0,  0, 0  },
               new int[] { 5, 0,  0, 0,  2, 10, 0, 0  },
               new int[] { 7, 0,  0, 0,  0, 0,  1, 0  },
               new int[] { 3, 0,  0, 0,  0, 0,  0, 11 },
               new int[] { 0, 2,  0, 0,  0, 0,  0, 9  },
               new int[] { 0, 10, 0, 0,  0, 0,  0, 4  },
               new int[] { 0, 0,  1, 0,  0, 0,  0, 6  },
               new int[] { 0, 0,  0, 11, 9, 4,  6, 0  }
            };
            _graph = new AdjacencyMatrixGraph(_vertexList, _adjacencyMatrix);
            //_graph = new LinearGraph(_vertexList, _linearEdgeList);
        }

        [TestMethod]
        public void FindAdjacentNodesSimpleTest()
        {
            var node = _vertexList[0];
            var actual = _graph.FindAdjacentNodes(node);
            Assert.AreEqual(_vertexList[1], actual[0]);
            Assert.AreEqual(_vertexList[2], actual[1]);
            Assert.AreEqual(_vertexList[3], actual[2]);
        }

        /// <summary>
        /// find all nodes adjacent to a node
        /// </summary>
        [TestMethod]
        public void FindAdjacentNodesTest()
        {
            foreach (var vertex in _vertexList)
            {
                var adjacentNodes = _graph.FindAdjacentNodes(vertex);
                foreach (var adjacentNode in adjacentNodes)
                {
                    Assert.IsTrue(_graph.AreNodesAdjacent(vertex, adjacentNode));
                }
            }
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