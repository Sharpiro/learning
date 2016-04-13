using System;

namespace InterviewPrep.Generics
{
    public class GenericEventHelper<T> : IGenericHelper<T>
    {
        public T GenericProp1 { get; set; }

        public TOutput Map<TOutput>(T input, Func<T, TOutput> converter)
        {
            return converter(input);
        }

        public event EventHandler<CustomEventArgs<int>> ThingHappened;

        public void SayHello()
        {
            Console.WriteLine("Hello there");
            OnThingHappened(12, 13);
        }

        private void OnThingHappened(int thing1, int thing2)
        {
            var args = new CustomEventArgs<int>
            {
                Property1 = thing1,
                Property2 = thing2
            };
            ThingHappened?.Invoke(this, args);
        }
    }

    public class CustomEventArgs<T> : EventArgs
    {
        public T Property1 { get; set; }
        public T Property2 { get; set; }
    }

    public interface IGenericHelper<T>
    {
        T GenericProp1 { get; set; }
        TOutput Map<TOutput>(T input, Func<T, TOutput> converter);
        event EventHandler<CustomEventArgs<int>> ThingHappened;
        void SayHello();
    }
}