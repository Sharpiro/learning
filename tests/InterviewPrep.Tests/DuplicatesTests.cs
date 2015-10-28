using System.Collections.Generic;
using InterviewPrep.Core;
using Xunit;

namespace InterviewPrep.Tests
{
    public class DuplicatesTests
    {
        private readonly List<int> _simpleDupes;
        private readonly List<int> _simpleNoDupes;
        private readonly List<int> _largeListNoDupes;

        public DuplicatesTests()
        {
            _simpleDupes = new List<int> { 1, 9, 7, 6, 99, 3, 4, 6, 22, 34, 55, 22 };
            _simpleNoDupes = new List<int> { 1, 2, 99, 34, 55, 3, 9, 100 };
            _largeListNoDupes = Duplicates.CreateLargeList(1000);
        }

        [Fact]
        public void HasDupesIndexTest()
        {
            var hasDupes = Duplicates.HasDupesIndex(_simpleDupes);
            var noDupes = Duplicates.HasDupesIndex(_simpleNoDupes);
            Assert.True(hasDupes);
            Assert.False(noDupes);
        }

        [Fact]
        public void HasDupesHashSetTest()
        {
            var hasDupes = Duplicates.HasDupesHashSet(_simpleDupes);
            var noDupes = Duplicates.HasDupesHashSet(_simpleNoDupes);
            Assert.True(hasDupes);
            Assert.False(noDupes);
        }

        [Fact]
        public void HasDupesSortFirstTest()
        {
            var hasDupes = Duplicates.HasDupesSortFirst(_simpleDupes);
            var noDupes = Duplicates.HasDupesSortFirst(_simpleNoDupes);
            Assert.True(hasDupes);
            Assert.False(noDupes);
        }

        [Fact]
        public void HasDupesDistinctTest()
        {
            var hasDupes = Duplicates.HasDupesDistinct(_simpleDupes);
            var noDupes = Duplicates.HasDupesDistinct(_simpleNoDupes);
            Assert.True(hasDupes);
            Assert.False(noDupes);
        }

        [Fact]
        public void HasDupesGroupByTest()
        {
            var hasDupes = Duplicates.HasDupesGroupBy(_simpleDupes);
            var noDupes = Duplicates.HasDupesGroupBy(_simpleNoDupes);
            Assert.True(hasDupes);
            Assert.False(noDupes);
        }
    }
}
