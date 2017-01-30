using System;

namespace RoslynTests
{
    public class DummyClass
    {
        private readonly object _dep2;

        public DummyClass(object dep2)
        {
            _dep2 = dep2 ?? throw new ArgumentNullException(nameof(dep2));
        }
    }
}