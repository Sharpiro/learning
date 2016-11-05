using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterviewPrep.Core.Values_and_References.Tests
{
    [TestClass()]
    public class ReferenceTypesTests
    {
        [TestMethod()]
        public void ReferencesTest()
        {
            //x is an alias for a memory address
            //x's memory address hold a value
            //x's value = 5
            var x = 5;

            //obj is an alias for 0x01 (a memory address)
            //obj's memory address hold a value
            //obj's value = 0x02 (a different memory address)
            var orignal = "original";
            IType obj = new TestDto { ReferenceProperty = orignal };

            ReferenceChanger.NullifyObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ReferenceChanger.ChangeObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == "changed");

            ReferenceChanger.ChangeObjectByRef(ref obj);
            Assert.IsTrue(obj.ReferenceProperty == "changedByRef");

            ReferenceChanger.NullifyObjectByRef(ref obj);
            Assert.IsTrue(obj == null);
        }

        [TestMethod()]
        public void ValueTest()
        {
            var orignal = "original";
            var obj = new TestDtoStruct { ReferenceProperty = orignal };

            ValueChanger.NullifyObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ValueChanger.ChangeObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ValueChanger.ChangeObjectByRef(ref obj);
            Assert.IsTrue(obj.ReferenceProperty == "changedByRef");

            ValueChanger.NullifyObjectByRef(ref obj);
            Assert.IsTrue(obj.Equals(default(TestDtoStruct)));
        }
    }
}