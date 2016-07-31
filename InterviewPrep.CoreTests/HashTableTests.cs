using InterviewPrep.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterviewPrep.CoreTests
{
    [TestClass]
    public class HashTableTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddFailTest()
        {
            var hasher = new CustomHashTable();
            hasher.Add(1, 1);
            hasher.Add(1, 2);
        }

        [TestMethod]
        public void AddOrUpdateTest()
        {
            var hasher = new CustomHashTable();
            hasher.AddOrUpdate(1, 1);
            hasher.AddOrUpdate(1, 2);
        }

        [TestMethod]
        public void GetHashTest()
        {
            var hasher = new CustomHashTable();
            var key = 1;
            var hash = hasher.GetHash(key);
            Assert.AreEqual(1, hash);
            key = 127;
            hash = hasher.GetHash(key);
            hasher.Add(key, 1);
            Assert.AreEqual(127, hash);
            key = 1023;
            hash = hasher.GetHash(key);
            Assert.AreEqual(0, hash);
        }

        [TestMethod]
        public void GetTest()
        {
            var hasher = new CustomHashTable();
            const int key = 128;
            const int value = 12;
            hasher.Add(key, value);
            var result = hasher.Get(key);
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RangeTest()
        {
            var hasher = new CustomHashTable();
            for (var i = 0; i < 128; i++)
            {
                hasher.Add(i, i);
            }
            hasher.Add(129, 129);
        }

        [TestMethod]
        public void LengthTest()
        {
            var hasher = new CustomHashTable();
            Assert.AreEqual(0, hasher.Length);
            hasher.Add(1, 1);
            Assert.AreEqual(1, hasher.Length);
        }

        [TestMethod]
        public void RemoveTest()
        {
            var hasher = new CustomHashTable();
            hasher.Add(1, 1);
            hasher.Add(2, 1);
            hasher.Add(3, 1);
            hasher.Remove(2);
            Assert.AreEqual(2, hasher.Length);
        }

        [TestMethod]
        public void IndexerTest()
        {
            var hasher = new CustomHashTable();
            hasher.Add(1, 1);
            hasher.Add(127, 3);
            hasher.Add(1023, 5);
            var one = hasher[1];
            var two = hasher[1023];
            var three = hasher[127];
            Assert.AreEqual(1, one);
            Assert.AreEqual(5, two);
            Assert.AreEqual(3, three);
        }
    }
}