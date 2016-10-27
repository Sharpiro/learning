using System;

namespace InterviewPrep.Core.Values_and_References
{
    public class ValueTypes
    {
        public static void ChangeObject(TestDtoStruct testDto)
        {
            //testDto's key = memory address
            //testDto's value = data
            testDto.ReferenceProperty = "changed";
        }

        public static void NullifyObject(TestDtoStruct obj)
        {
            obj = default(TestDtoStruct);
        }

        public static void ChangeObjectByRef(ref TestDtoStruct obj)
        {
            obj.ReferenceProperty = "changedByRef";
        }

        public static void NullifyObjectByRef(ref TestDtoStruct obj)
        {
            obj = default(TestDtoStruct);
        }
    }

    public struct TestDtoStruct : IEquatable<TestDtoStruct>
    {
        public int ValueProperty { get; set; }
        public string ReferenceProperty { get; set; }

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
    }
}
