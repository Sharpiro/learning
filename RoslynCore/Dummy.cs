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
        public ReferencesClass(int dependency)
        {

        }

        public void DoStuff()
        {

        }

        public void DoOtherStuff()
        {
            DoStuff();
        }

    }
}