using InterviewPrep.Core.Security;
using System;

namespace InterviewPrep.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var saltedKey = AesFacade.GetSaltedKey("password", 16);
            var isMatch = AesFacade.CheckSaltedKey("password", saltedKey, 16);
            var isMatch2 = AesFacade.CheckSaltedKey("passwordXXX", saltedKey);
            Console.ReadLine();
        }
    }
}