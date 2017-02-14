using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static System.Linq.LinqExtensions;

namespace RoslynCore
{
    public abstract class Node : IEquatable<Node>
    {
        public Node Parent { get; internal set; }
        public Annotation Annotation { get; internal set; }
        public abstract ImmutableList<Node> Children { get; }

        internal Node() { }

        public T WithAnnotation<T>(Annotation annotation) where T : Node
        {
            var newNode = CloneInternal<T>();
            newNode.Annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));
            return newNode;
        }

        public Node GetRootNode()
        {
            Node getRootNode(Node node)
            {
                if (node == null)
                    return null;
                if (node.Parent == null)
                    return node;
                return getRootNode(node.Parent);
            }
            return getRootNode(this);
        }

        public T FindDescendantByAnnotation<T>(Annotation annotation) where T : Node
        {
            return (T)GetDescendantNodes().SingleOrDefault(n => n.Annotation == annotation);
        }

        public IEnumerable<Node> GetDescendantNodes()
        {
            foreach (var child in Children)
            {
                if (child == null) continue;
                yield return child;
                foreach (var subchild in child.GetDescendantNodes())
                {
                    yield return subchild;
                }
            }
        }

        public IEnumerable<Node> GetDescendantNodesAndSelf() => SingleList.Select(i => this).Concat(GetDescendantNodes());

        public bool Equals(Node other) => this == other;

        public T Cast<T>() where T : Node => (T)this;

        internal abstract T CloneInternal<T>() where T : Node;
    }

    public class ChildOne : Node
    {
        public Node Type { get; private set; }
        public override ImmutableList<Node> Children => NotNullList(Type);

        internal ChildOne() { }

        public ChildOne WithType(Node type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var newNode = CloneInternal<ChildOne>();
            type = type.Clone().WithParent(newNode);
            newNode.Type = type;
            return newNode;
        }

        internal override T CloneInternal<T>()
        {
            var newClass = this.BaseClone();
            newClass.Type = Type?.Clone().WithParent(newClass);
            return newClass.Cast<T>();
        }
    }

    public class ParentOne : Node
    {
        public Node Identifier { get; private set; }
        public override ImmutableList<Node> Children => NotNullList(Identifier);

        internal ParentOne() { }

        public ParentOne WithIdentifier(Node identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));

            var newNode = this.Clone();
            identifier = identifier.Clone().WithParent(newNode);
            newNode.Identifier = identifier;
            return newNode;
        }

        internal override T CloneInternal<T>()
        {
            var newClass = this.BaseClone();
            newClass.Identifier = Identifier?.Clone().WithParent(newClass);
            return newClass.Cast<T>();
        }
    }

    public static class NodeFactory
    {
        public static ParentOne ParentOne(Node identifier = null)
        {
            var newClass = new ParentOne();
            if (identifier != null)
            {
                identifier = identifier.Clone().WithParent(newClass);
                newClass = newClass.WithIdentifier(identifier);
            }
            return newClass;
        }

        public static ChildOne ChildOne()
        {
            var newClass = new ChildOne();
            return newClass;
        }
    }

    public class Annotation : IEquatable<Annotation>
    {
        public string Text { get; }

        public Annotation(string text = null) => Text = text;

        public bool Equals(Annotation other) => this == other;
    }
}