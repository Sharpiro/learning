using System;

namespace InterviewPrep.Core.Variance
{
    public class Contravariance
    {
        public void HandleBaseEvent(object sender, EventArgs args)
        {
            Console.WriteLine("hi");
        }

        public void Do()
        {
            EventHandler handler = HandleBaseEvent;
            handler(this, EventArgs.Empty);

            handler = new EventHandler(HandleBaseEvent);
            handler(this, EventArgs.Empty);

            handler = delegate { Console.WriteLine("hi"); };
            handler(this, EventArgs.Empty);

            handler = delegate (object sender, EventArgs args) { Console.WriteLine("hi"); };
            handler(this, EventArgs.Empty);

            handler = (s, a) => Console.WriteLine("hi");
            handler(this, EventArgs.Empty);

            //delegate contravariance
            UnhandledExceptionEventHandler unhandledHandler = HandleBaseEvent;
            unhandledHandler(this, null);

            //no contravariance for anonymous types
            unhandledHandler = (s, a) => Console.WriteLine("hi");
        }
    }
}