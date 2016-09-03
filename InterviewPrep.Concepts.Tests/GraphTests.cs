using InterviewPrep.Concepts.VisualGraphs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace InterviewPrep.Concepts.Tests
{
    [TestClass]
    public class GraphTests
    {
        private readonly AdjacencyListGraph _graph;

        public GraphTests()
        {
            const string graphData = @"17
                directed
                A
                B
                C
                D
                E
                F
                G
				H
				I
				J
				K
				L
				M
				N
				O
				P
				Q
                A B 1
				B A 1
				B C 3
				C B 1
				B Q 3
				Q B 1
				C D 1
				D C 3
				C E 1
				E C 3
				D Q 3
				Q D 1
				E F 3
				F E 1
				F G 1
				G F 3
				F H 3
				H F 3
				H I 3
				I H 3
				I J 1
				J I 3
				J K 3
				K J 1
				K L 3
				L K 3
				K P 3
				P K 3
				L M 3
				M L 3
				M N 1
				N M 3
				M O 1
				O M 3
				O Q 3
				Q O 1
				Q P 3
				P Q 3";

            var reader = new StringReader(graphData);
            var parser = new GraphParser(reader);
            _graph = parser.CreateAdjacencyListGraph();
        }

        [TestMethod]
        public void GraphTest()
        {
            var gToNfringeData = _graph.FindShortestPath("G", "N");
            var gToN = new {
                Path = gToNfringeData.Select(f => f.Vertex.Name),
                Data = string.Join("\r\n", gToNfringeData.Select(f => f.Vertex.Name)),
                Distance = gToNfringeData.LastOrDefault().Distance
            };

            var jToNfringeData = _graph.FindShortestPath("J", "N");
            var jToN = new
            {
                Path = jToNfringeData.Select(f => f.Vertex.Name),
                Data = string.Join("\r\n", jToNfringeData.Select(f => f.Vertex.Name)),
                Distance = jToNfringeData.LastOrDefault().Distance
            };
        }
    }
}
