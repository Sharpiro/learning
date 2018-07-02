using System;

namespace InterviewPrep.Core.Values_and_References
{
    public interface IType
    {
        int ValueProperty { get; set; }
        string ReferenceProperty { get; set; }
    }

    public class TestDto : IType
    {
        public int ValueProperty { get; set; }
        public string ReferenceProperty { get; set; }
    }

    public struct TestDtoStruct : IEquatable<TestDtoStruct>, IType
    {
        public int ValueProperty { get; set; }
        public string ReferenceProperty { get; set; }

        public override bool Equals(object obj)
        {
            var other = (IType)obj;
            return ValueProperty == other.ValueProperty && ReferenceProperty == other.ReferenceProperty;
        }

        public bool Equals(TestDtoStruct other)
        {
            return ValueProperty == other.ValueProperty && ReferenceProperty == other.ReferenceProperty;
        }

        public static bool operator ==(TestDtoStruct left, TestDtoStruct right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(TestDtoStruct left, TestDtoStruct right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}