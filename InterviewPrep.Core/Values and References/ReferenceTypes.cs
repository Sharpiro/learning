namespace InterviewPrep.Core.Values_and_References
{
    public class ReferenceTypes
    {
        public static void ChangeObject(TestDto testDto)
        {
            //testDto's key
            //testDto's value = obj's value = memory address
            testDto.ReferenceProperty = "changed";
        }

        public static void NullifyObject(TestDto obj)
        {
            obj = null;
        }

        public static void ChangeObjectByRef(ref TestDto obj)
        {
            obj.ReferenceProperty = "changedByRef";
        }

        public static void NullifyObjectByRef(ref TestDto obj)
        {
            obj = null;
        }
    }

    public class TestDto
    {
        public int ValueProperty { get; set; }
        public string ReferenceProperty { get; set; }
    }
}