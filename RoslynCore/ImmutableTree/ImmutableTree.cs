using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using static System.Linq.LinqExtensions;
using static RoslynCore.NodeExtensions;

namespace RoslynCore
{
    public abstract class Node : IEquatable<Node>
    {
        public Node Parent { get; internal set; }
        public Annotation Annotation { get; internal set; }
        public abstract ImmutableList<Node> Children { get; }

        internal Node() { }

        public Node GetRootNode()
        {
            Node getRootNode(Node node)
            {
                if (node == null) return null;
                if (node.Parent == null) return node;
                return getRootNode(node.Parent);
            }
            return getRootNode(this);
        }

        public Node FindDescendantByAnnotation(Annotation annotation)
        {
            if (annotation == null) throw new ArgumentNullException(nameof(annotation));

            return GetDescendantNodes().SingleOrDefault(n => n.Annotation.Equals(annotation));
        }

        public IEnumerable<Node> GetDescendantNodes()
        {
            foreach (var child in Children)
            {
                if (child == null) continue;
                yield return child;
                foreach (var subchild in child.GetDescendantNodes())
                    yield return subchild;
            }
        }

        public IEnumerable<Node> GetDescendantNodesAndSelf() => SingleList(this).Concat(GetDescendantNodes());

        public T Cast<T>() where T : Node => (T)this;
        public bool Equals(Node other) => this == other;

        internal virtual T Clone<T>() where T : Node
        {
            var currentType = typeof(T);
            var newNode = (T)Activator.CreateInstance(currentType, true);
            newNode.Annotation = Annotation;
            return newNode;
        }
    }

    public class ChildOne : Node
    {
        public Node Type { get; private set; }
        public override ImmutableList<Node> Children => NodeList(Type);

        internal ChildOne() { }

        public ChildOne WithType(Node type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var newNode = this.Clone();
            type = type.Clone().WithParent(newNode);
            newNode.Type = type;
            return newNode;
        }

        internal override T Clone<T>()
        {
            var newClass = base.Clone<ChildOne>();
            newClass.Type = Type?.Clone().WithParent(newClass);
            return newClass.Cast<T>();
        }
    }

    public class ParentOne : Node
    {
        public ChildOne Identifier { get; private set; }
        public override ImmutableList<Node> Children => NodeList(Identifier);

        internal ParentOne() { }

        public ParentOne WithIdentifier(ChildOne identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));

            var newNode = this.Clone();
            identifier = identifier.Clone().WithParent(newNode);
            newNode.Identifier = identifier;
            return newNode;
        }

        internal override T Clone<T>()
        {
            var newClass = base.Clone<ParentOne>();
            newClass.Identifier = Identifier?.Clone().WithParent(newClass);
            return newClass.Cast<T>();
        }
    }

    public static class NodeFactory
    {
        public static Annotation Annotation(string text = null) => new Annotation(text);

        public static ParentOne ParentOne(ChildOne identifier = null)
        {
            var newNode = new ParentOne();
            if (identifier != null)
            {
                identifier = identifier.Clone().WithParent(newNode);
                newNode = newNode.WithIdentifier(identifier);
            }
            return newNode;
        }

        public static ChildOne ChildOne(Node type = null)
        {
            var newNode = new ChildOne();
            if (type != null)
            {
                type = type.WithParent(newNode);
                newNode = newNode.WithType(type);
            }
            return newNode;
        }
    }

    public class Annotation : IEquatable<Annotation>
    {
        public string Text { get; }
        internal Annotation(string text = null) => Text = text;
        public bool Equals(Annotation other) => this == other;
    }
}