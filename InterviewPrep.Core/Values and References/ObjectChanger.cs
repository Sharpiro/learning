namespace InterviewPrep.Core.Values_and_References
{
    public class ReferenceChanger
    {
        public static void ChangeObject(IType testDto)
        {
            //testDto's key
            //testDto's value = obj's value = memory address
            testDto.ReferenceProperty = "changed";
        }

        public static void NullifyObject(IType obj)
        {
            obj = null;
        }

        public static void ChangeObjectByRef(ref IType obj)
        {
            obj.ReferenceProperty = "changedByRef";
        }

        public static void NullifyObjectByRef(ref IType obj)
        {
            obj = null;
        }
    }

    public class ValueChanger
    {
        public static void ChangeObject(TestDtoStruct testDto)
        {
            //testDto's key
            //testDto's value = obj's value = memory address
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
}