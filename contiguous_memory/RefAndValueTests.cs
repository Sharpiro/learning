using System;
using System.Diagnostics;
using System.Linq;
using Xunit;
using static System.Console;

namespace contiguous_memory
{
    public class RefAndValueTests
    {
        [Fact]
        // creates a new array pointer to an existing array on the heap but does not allocate a new array
        public void CreateNewArrayVariableTest()
        {
            var array1 = new[] { 1, 2, 3, 4, 5 };
            var array2 = array1;
            array1[0] = 99;

            Assert.Equal(99, array1[0]);
            Assert.Equal(99, array2[0]);
        }

        [Fact]
        // create a new array pointer to a new array allocated on the heap
        // all items in array1 are copied by value to array2
        public void LinqCreatesNewArrayAndCopiesAllItemsByValueTest()
        {
            var array1 = new[] { 1, 2, 3, 4, 5 };
            var array2 = array1.ToArray();
            array1[0] = 99;

            Assert.Equal(99, array1[0]);
            Assert.Equal(1, array2[0]);
        }

        [Fact]
        // create a new array pointer to a new array allocated on the heap
        // all items in array1 are copied by value to array2
        public void LinqCreatesNewArrayAndCopiesAllItemsByValueTest2()
        {
            var array1 = new[] { new Ref(1), new Ref(2), new Ref(3), new Ref(4), new Ref(5) };
            var array2 = array1.ToArray();
            array1[0].Prop = 99;
            array1[1] = new Ref(77);
            ref var temp = ref array1[2];
            temp = new Ref(12);

            Assert.Equal(99, array1[0].Prop);
            Assert.Equal(99, array2[0].Prop);

            Assert.Equal(77, array1[1].Prop);
            Assert.Equal(2, array2[1].Prop);

            // ref does nothing here since .ToArray() created new pointers in each array index
            Assert.Equal(12, array1[2].Prop);
            Assert.Equal(3, array2[2].Prop);
        }

        [Fact]
        public void LinqSliceWithAllocation()
        {
            var array1 = new[] { 1, 2, 3, 4, 5 };
            var array2 = array1.Skip(1).Take(3).ToArray();
            array1[1] = 99;

            Assert.Equal(99, array1[1]);
            Assert.Equal(2, array2[0]);
        }

        [Fact]
        public void SliceWithSpan()
        {
            var array1 = new Span<int>(new[] { 1, 2, 3, 4, 5 });
            var array2 = array1.Slice(1, 3);
            array1[1] = 99;

            Assert.Equal(99, array1[1]);
            Assert.Equal(99, array2[0]);
        }

        [Fact]
        // ref and in are essentially like creating new aliases for existing variables
        // and therefore you have 2 variables that are aliases for the same memory location
        public void InReadonlyRefTest()
        {
            var x = 1;
            var y = 2;
            TryIncrement(ref x, y);

            void TryIncrement(ref int xRef, in int yRef)
            {
                xRef++;
                // yCopy++; // illegal!
            }
            Assert.Equal(2, x);
            Assert.Equal(2, y);
        }

        [Fact]
        public void MutableRefTest()
        {
            var buffer = new char[] { 'a', 'b', 'c' };
            ref char firstChar = ref buffer[0];
            firstChar = 'z';
            Assert.Equal(buffer[0], firstChar);
            unsafe
            {
                fixed (char* firstCharAddress = &firstChar)
                fixed (char* bufferStartAddress = buffer)
                {
                    {
                        var firstCharPointer = new IntPtr(firstCharAddress).ToString();
                        var bufferStartPointer = new IntPtr(bufferStartAddress).ToString();
                        Assert.True(firstCharPointer == bufferStartPointer);
                    }
                }
            }
        }
    }

    [DebuggerDisplay("Ref = {Prop}")]
    class Ref
    {
        public int Prop { get; set; }

        public Ref(int prop)
        {
            Prop = prop;
        }
    }
}
