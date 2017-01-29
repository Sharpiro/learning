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
        public int Property { get; set; }

        public ReferencesClass(int dependency)
        {
            DoStuff();

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