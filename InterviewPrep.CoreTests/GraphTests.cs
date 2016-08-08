using InterviewPrep.Core.Graphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class GraphTests
    {
        private readonly IGraph _graph;
        private readonly List<string> _vertexList;
        private readonly List<Edge> _linearEdgeList;
        private readonly int[][] _adjacencyMatrix;
        private readonly Neighbor[] _adjacencyList;
        private readonly GraphHelper _graphHelper;

        public GraphTests()
        {
            //const string graphData = @"4
            //    undirected
            //    A
            //    B
            //    C
            //    D
            //    A B 5
            //    A C 4
            //    B D 6
            //    C D 8";
            //const string graphData = @"8
            //    undirected
            //    A
            //    B
            //    C
            //    D
            //    E
            //    F
            //    G
            //    H
            //    A B 5
            //    A C 7
            //    A D 3
            //    B E 2
            //    B F 10
            //    C G 1
            //    D H 11
            //    E H 9
            //    F H 4
            //    G H 6";
            const string graphData = @"7
                undirected
                A
                B
                C
                D
                E
                F
                G
                A B 5
                A C 9
                B D 6
                B E 3
                D F 6
                E C 2
                E D 2
                E G 2
                G F 2";
            _graphHelper = new GraphHelper(graphData);
            //_graph = _graphHelper.CreateEdgeListGraph();
            //_graph = _graphHelper.CreateAdjacencyMatrixGraph();
            _graph = _graphHelper.CreateAdjacencyListGraph();
        }

        [TestMethod]
        public void FindAdjacentNodesSimpleTest()
        {
            var actual = _graph.FindAdjacentNodes("A").ToList();
            Assert.AreEqual("B", actual[0]);
            Assert.AreEqual("C", actual[1]);
            actual = _graph.FindAdjacentNodes("E").ToList();
            Assert.AreEqual("B", actual[0]);
            Assert.AreEqual("C", actual[1]);
            Assert.AreEqual("D", actual[2]);
            Assert.AreEqual("G", actual[3]);
        }

        [TestMethod]
        public void FindShortestPathTest()
        {
            var actual = _graph.FindShortestPath("A", "C").ToList();
            Assert.AreEqual("A", actual[0]);
            Assert.AreEqual("C", actual[1]);
            actual = _graph.FindShortestPath("A", "D").ToList();
            Assert.AreEqual("A", actual[0]);
            Assert.AreEqual("B", actual[1]);
            Assert.AreEqual("E", actual[2]);
            Assert.AreEqual("D", actual[3]);
            actual = _graph.FindShortestPath("A", "F").ToList();
            Assert.AreEqual("A", actual[0]);
            Assert.AreEqual("B", actual[1]);
            Assert.AreEqual("E", actual[2]);
            Assert.AreEqual("G", actual[3]);
            Assert.AreEqual("F", actual[4]);
        }

        /// <summary>
        /// find all nodes adjacent to a node
        /// </summary>
        [TestMethod]
        public void FindAdjacentNodesTest()
        {
            foreach (var vertex in _graphHelper.VertexList)
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
            Assert.IsTrue(_graph.AreNodesAdjacent("A", "B"));
            Assert.IsTrue(_graph.AreNodesAdjacent("A", "C"));
            //Assert.IsFalse(_graph.AreNodesAdjacent("B", "C"));

            Assert.IsTrue(_graph.AreNodesAdjacent("B", "E"));
            //Assert.IsTrue(_graph.AreNodesAdjacent("B", "F"));
            Assert.IsTrue(_graph.AreNodesAdjacent("E", "G"));
        }
    }
}