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
            var obj = new TestDto { ReferenceProperty = orignal };

            ReferenceTypes.NullifyObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ReferenceTypes.ChangeObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == "changed");

            ReferenceTypes.ChangeObjectByRef(ref obj);
            Assert.IsTrue(obj.ReferenceProperty == "changedByRef");

            ReferenceTypes.NullifyObjectByRef(ref obj);
            Assert.IsTrue(obj == null);
        }

        [TestMethod()]
        public void ValueTest()
        {
            var orignal = "original";
            var obj = new TestDtoStruct { ReferenceProperty = orignal };

            ValueTypes.NullifyObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ValueTypes.ChangeObject(obj);
            Assert.IsTrue(obj.ReferenceProperty == orignal);

            ValueTypes.ChangeObjectByRef(ref obj);
            Assert.IsTrue(obj.ReferenceProperty == "changedByRef");

            ValueTypes.NullifyObjectByRef(ref obj);
            Assert.IsTrue(obj == default(TestDtoStruct));
        }
    }
}