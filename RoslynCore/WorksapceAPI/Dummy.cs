using System;

namespace RoslynCore
{
    class Dummy{
        public void Do() {
            Console.WriteLine("Hello guvna");
                return; }
    }
    public class ReferencesClass
    {
        private readonly string _dependency;

        public int Property { get; set; }

        public ReferencesClass(string dependency)
        {
            DoStuff();
            _dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
        }

        public void DoStuff()
        {
            Property = 2;
        }

        public void DoOtherStuff()
        {
            DoStuff();
        }
    }
}