using System;
using System.Runtime.CompilerServices;

namespace Parallels.CustomTask
{
    public class CustomAwaitable
    {
        public CustomAwaiter GetAwaiter()
        {
            return new CustomAwaiter();
        }
    }

    public class CustomAwaiter : INotifyCompletion
    {
        public const string Message = "This is the result";
        public bool IsCompleted => true;

        public void OnCompleted(Action continuation)
        {
            Console.WriteLine("This won't get called");
        }

        public string GetResult()
        {
            return Message;
        }
    }
}