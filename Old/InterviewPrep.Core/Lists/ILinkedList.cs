using System.Collections.Generic;

namespace InterviewPrep.Core.Lists
{
    public interface ISimpleLinkedList
    {
        int Count { get; }
        Node First { get; }
        Node Last { get; }
        Node AddFirst(int value);
        Node AddLast(int value);
        Node Find(int value);
        void RemoveFirst();
        void RemoveLast();
    }

    public interface IComplexLinkedList: ISimpleLinkedList, IEnumerable<int>
    {
        Node AddAfter(Node node, int value);
        Node AddBefore(Node node, int value);
        int FindPos(Node node);
        void Remove(Node node);
    }
}