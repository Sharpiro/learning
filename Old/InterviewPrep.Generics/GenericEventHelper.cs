using System;
using System.Threading.Tasks;

namespace InterviewPrep.Generics
{
    public class GenericEventHelper<T> : IGenericHelper<T>
    {
        public T GenericProp1 { get; set; }

        public GenericEventHelper()
        {
            Task.Delay(50000).ContinueWith((task) => Task.Run((Action)TriggerEvent));
            //TriggerEvent();
        }

        public TOutput Map<TOutput>(T input, Func<T, TOutput> converter)
        {
            return converter(input);
        }

        private event Action _actionEvent;
        public event EventHandler<CustomEventArgs<int>> ThingHappened;

        private void TriggerEvent()
        {
            _actionEvent.Invoke();
            //OnThingHappened(12, 13);
        }

        public void Register(Action action)
        {
            _actionEvent += action.Invoke;
            Console.WriteLine("action registered...");
        }

        private void GenericEventHelper_ThingHappened(object sender, CustomEventArgs<int> e)
        {
            throw new NotImplementedException();
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
        //void TriggerEvent();
    }
}