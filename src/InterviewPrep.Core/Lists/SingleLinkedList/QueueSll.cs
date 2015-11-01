namespace InterviewPrep.Core.Lists.SingleLinkedList
{
    public class QueueSll<T> : SingleLinkedList<T>
    {
        public void Enqueue(T value)
        {
            Insert(value);
        }

        public T Dequeue()
        {
            return Delete(1).Value;
        }
    }
}
