using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace RoslynCore
{
    public abstract class Node
    {
        public Node Parent { get; private set; }
        public Annotation Annotation { get; private set; }
        public abstract ImmutableList<Node> Children { get; }

        internal Node() { }

        public T WithAnnotation<T>(Annotation annotation) where T : Node
        {
            var newNode = Clone<T>();
            newNode.Annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));
            return newNode;
        }

        public T ReplaceNode<T>(Node oldNode, Node newNode) where T : Node
        {
            var parentNode = getParentNodeFromNewTree();
            var nodeType = parentNode.GetType();
            var properties = nodeType.GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(this);
                if (oldNode == propertyValue)
                    property.SetValue(parentNode, newNode.Clone().WithParent(parentNode));
            }

            Node getParentNodeFromNewTree()
            {
                var oldRoot = oldNode.GetRootNode();
                var newRoot = oldNode.GetRootNode().Clone();
                var oldNodes = oldRoot.GetDescendantNodes().ToList();
                var newNodes = newRoot.GetDescendantNodes().ToList();
                var index = oldNodes.IndexOf(oldNode);

                return newNodes[index].Parent;
            }

            return (T)this;
        }

        public Node GetRootNode() => GetRootNode(this);

        public Node GetRootNode(Node node)
        {
            if (node == null)
                return null;
            if (node.Parent == null)
                return node;
            return GetRootNode(node.Parent);
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

        internal T WithParent<T>(Node parent) where T : Node
        {
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            return Cast<T>();
        }

        internal Node WithParent(Node parent) => WithParent<Node>(parent);

        public T Cast<T>() where T : Node
        {
            return (T)this;
        }

        protected virtual T BaseClone<T>() where T : Node
        {
            var currentType = typeof(T);
            var node = (T)Activator.CreateInstance(currentType, true);
            node.Annotation = Annotation;
            return node;
        }

        internal virtual Node Clone() => Clone<Node>();

        internal abstract T Clone<T>() where T : Node;
    }

    public class ChildOne : Node
    {
        public Node Type { get; private set; }
        public override ImmutableList<Node> Children => ImmutableList.Create(Type).Where(n => n != null).ToImmutableList();

        internal ChildOne() { }

        public ChildOne WithType(Node type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var newNode = Clone<ChildOne>();
            type = type.Clone().WithParent(newNode);
            newNode.Type = type;
            return newNode;
        }

        internal override T Clone<T>()
        {
            var newClass = BaseClone<ChildOne>();
            newClass.Type = Type?.Clone().WithParent(newClass);
            return newClass.Cast<T>();
        }
    }

    public class ParentOne : Node
    {
        public Node Identifier { get; private set; }
        public override ImmutableList<Node> Children => ImmutableList.Create(Identifier).Where(n => n != null).ToImmutableList();

        internal ParentOne() { }

        public ParentOne WithIdentifier(Node identifier)
        {
            if (identifier == null) throw new ArgumentNullException(nameof(identifier));

            var newNode = Clone<ParentOne>();
            identifier = identifier.Clone().WithParent(newNode);
            newNode.Identifier = identifier;
            return newNode;
        }

        internal override T Clone<T>()
        {
            var newClass = BaseClone<ParentOne>();
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

    public class Annotation
    {
        public string Text { get; }

        public Annotation(string text = null)
        {
            Text = text;
        }
    }
}