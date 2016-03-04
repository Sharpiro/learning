using System;
using System.Diagnostics;

namespace InterviewPrep.Core
{
    public delegate void Getter();

    public class DelegatesAndEvents
    {
        public event Getter MyEvent;

        public void CallEvent()
        {
            OnMyEvent();
            MyEvent?.Invoke();
        }

        public static void Call()
        {
            WriteToDebug(DoNothingActionDelegate);
            WriteToDebugFunc(GetData);
            WriteToDebug(() => Debug.WriteLine(1));
            WriteToDebugFunc(() => 1);
        }

        private static void WriteToDebug(Getter getter)
        {
            getter();
        }

        private static void WriteToDebugFunc(Func<int> getter)
        {
            getter();
        }

        private static int GetData()
        {
            return 1;
        }

        private static void DoNothingActionDelegate()
        {
            //Do nothing
        }

        protected virtual void OnMyEvent()
        {
            MyEvent?.Invoke();
        }
    }
}