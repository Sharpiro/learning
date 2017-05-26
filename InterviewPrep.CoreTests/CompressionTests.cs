using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using InterviewPrep.Core.Compression.Huffman;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class HuffmanTests
    {
        [TestMethod]
        public void BuildTreeTest()
        {
            const string complexInput = "this is an example of a huffman tree";
            var tree = HuffmanTree.Create(complexInput);
            var huffmanList = tree.ToList().Select(n => n.Frequency).OrderBy(i => i).ToList();

            var expectedCollection = new List<int>
            {
                36, 16, 20, 8, 8, 8, 12, 4, 4, 4, 4, 4, 4, 5, 7, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 1, 1, 1, 1, 1, 1
            }.OrderBy(i => i).ToList();

            Assert.AreEqual(31, expectedCollection.Count);
            Assert.AreEqual(31, huffmanList.Count);
            Assert.IsTrue(huffmanList.SequenceEqual(expectedCollection));
        }

        [TestMethod]
        public void SimpleEncodeTest()
        {
            const string input = "input";
            var tree = HuffmanTree.Create(input);
            var uncompressedBits = new BitArray(Encoding.UTF8.GetBytes(input));

            var compressedBits = tree.Encode();
            var decodedText = tree.Decode(compressedBits);

            Assert.AreEqual(input, decodedText);
            Assert.IsTrue(uncompressedBits.Length > compressedBits.Length);
        }

        [TestMethod]
        public void ComplexEncodeTest()
        {
            const string input = "this is an example of a huffman tree";
            var tree = HuffmanTree.Create(input);
            var uncompressedBits = new BitArray(Encoding.UTF8.GetBytes(input));

            var compressedBits = tree.Encode();
            var bytesLength = (int)Math.Ceiling((double)compressedBits.Count / 8);
            var compressedBytes = new byte[bytesLength];
            compressedBits.CopyTo(compressedBytes, 0);
            var newBits = new BitArray(compressedBytes);
            var decodedText = tree.Decode(newBits);

            Assert.AreEqual(input, decodedText);
            Assert.IsTrue(uncompressedBits.Length > compressedBits.Length);
        }

        [TestMethod]
        public void BitsToBytesAndBackTest()
        {
            const string input = "this is an example of a huffman tree";

            var bytes = Encoding.UTF8.GetBytes(input);
            var bits = new BitArray(bytes);
            var bytesLength = (int)Math.Ceiling((double)bits.Count / 8);
            var newBytes = new byte[bytesLength];
            bits.CopyTo(newBytes, 0);
            var output = Encoding.UTF8.GetString(newBytes);

            Assert.AreEqual(input, output);
        }
    }
}