using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InterviewPrep.Core.Compression.Huffman
{
    public class HuffmanTree : IEnumerable<HuffmanNode>
    {
        private readonly string _input;

        public HuffmanNode Root { get; set; }

        private HuffmanTree(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            _input = input;
        }

        private void Build()
        {
            var frequencyList = _input.GroupBy(c => c).OrderBy(g => g.Count()).ThenBy(g => g.Key).Select(g => new HuffmanNode
            {
                Character = g.Key,
                Frequency = g.Count()
            }).ToList();

            while (frequencyList.Count > 1)
            {
                var topTwo = frequencyList.OrderBy(n => n.Frequency).ThenBy(g => g.Character).Take(2).ToList();
                var x = topTwo[0];
                var y = topTwo[1];
                var parentNode = new HuffmanNode
                {
                    Frequency = x.Frequency + y.Frequency,
                    Left = x,
                    Right = y
                };
                frequencyList.Remove(x);
                frequencyList.Remove(y);
                frequencyList.Add(parentNode);
            }
            Root = frequencyList.Single();
        }

        public BitArray Encode()
        {
            var encodedSource = new List<bool>();
            foreach (var character in _input)
            {
                var encodedSymbol = Traverse(Root, character);
                encodedSource.AddRange(encodedSymbol);
            }
            var bits = new BitArray(encodedSource.ToArray());
            return bits;
        }

        public string Decode(BitArray bits)
        {
            if (bits == null) throw new ArgumentNullException(nameof(bits));

            var current = Root;
            var decoded = string.Empty;

            foreach (bool bit in bits)
            {
                if (bit && current.Right != null)
                    current = current.Right;
                else if (current.Left != null)
                    current = current.Left;

                if (!IsLeafNode(current)) continue;
                decoded += current.Character;
                current = Root;
            }
            return decoded;
        }

        private static IEnumerable<bool> Traverse(HuffmanNode currentNodeX, char? charcterY)
        {
            var list = new List<bool>();

            return Traverse(currentNodeX, charcterY, list);

            List<bool> Traverse(HuffmanNode currentNode, char? compareChar, List<bool> data)
            {
                if (IsLeafNode(currentNode)) return compareChar.Equals(currentNode.Character) ? data : null;

                if (currentNode.Left != null)
                {
                    var leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);
                    var left = Traverse(currentNode.Left, compareChar, leftPath);
                    if (left != null) return left;
                }

                if (currentNode.Right == null) return null;
                var rightPath = new List<bool>();
                rightPath.AddRange(data);
                rightPath.Add(true);
                var right = Traverse(currentNode.Right, compareChar, rightPath);
                return right;
            }
        }

        public IEnumerator<HuffmanNode> GetEnumerator() => Root.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static bool IsLeafNode(HuffmanNode node) => node.Left == null && node.Right == null;

        public static HuffmanTree Create(string input)
        {
            if (string.IsNullOrEmpty(input)) throw new ArgumentNullException(nameof(input));

            var tree = new HuffmanTree(input);
            tree.Build();
            return tree;
        }
    }

    public class HuffmanNode : IEnumerable<HuffmanNode>
    {
        public char? Character { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }

        public IEnumerator<HuffmanNode> GetEnumerator()
        {
            if (Left != null)
            {
                foreach (var v in Left)
                {
                    yield return v;
                }
            }

            yield return this;

            if (Right == null) yield break;
            foreach (var v in Right)
            {
                yield return v;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}