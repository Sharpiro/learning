using InterviewPrep.Core.Lists;
using InterviewPrep.Core.Lists.SingleLinkedList;
using Xunit;

namespace InterviewPrep.Tests.Lists
{
    public class LinkedListTests
    {
        [Fact]
        public void InsertTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            Assert.Equal(list.Count, 15);
        }

        [Fact]
        public void GetTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var node = list.Get(5);
            Assert.NotNull(node);
            Assert.Equal(node.Value, 4);
        }

        [Fact]
        public void DeleteTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var node = list.Delete(2);
            Assert.Equal(list.Count, 14);
            Assert.Equal(node.Value, 1);

            node = list.Delete(5);
            Assert.Equal(list.Count, 13);
            Assert.Equal(node.Value, 5);
        }

        [Fact]
        public void ToListTest()
        {
            var list = new SingleLinkedList<int>();
            for (var i = 0; i < 15; i++)
            {
                list.Insert(i);
            }
            var standardList = list.ToList();
            Assert.Equal(standardList.Count, list.Count);
        }
    }
}
