using System;
using System.Collections.Immutable;
using System.Linq;

namespace RoslynCore
{
    public static class NodeExtensions
    {
        public static T ReplaceNode<T>(this T oldParentNode, Node oldNode, Node newNode) where T : Node
        {
            if (oldNode == null) throw new ArgumentNullException(nameof(oldNode));
            if (newNode == null) throw new ArgumentNullException(nameof(newNode));

            var newParentNode = getParentNodeFromNewTree();
            var newNodeClone = newNode.Clone().WithParent(newParentNode);
            var nodeType = newParentNode.GetType();
            var properties = nodeType.GetProperties();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(oldParentNode);
                if (oldNode == propertyValue)
                    property.SetValue(newParentNode, newNodeClone);
            }

            Node getParentNodeFromNewTree()
            {
                var oldRoot = oldParentNode.GetRootNode();
                var newRoot = oldParentNode.GetRootNode().Clone();
                var oldNodes = oldRoot.GetDescendantNodesAndSelf();
                var newNodes = newRoot.GetDescendantNodesAndSelf();
                var index = oldNodes.IndexOf(oldParentNode);
                return newNodes.ElementAt(index);
            }
            return (T)newParentNode;
        }

        public static T WithAnnotation<T>(this T node, Annotation annotation) where T : Node
        {
            var newNode = node.Clone<T>();
            newNode.Annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));
            return newNode;
        }

        public static ImmutableList<Node> NodeList(params Node[] parameters) => parameters.Where(p => p != null).ToImmutableList();
        public static T Clone<T>(this T node) where T : Node => node.Clone<T>();

        internal static T WithParent<T>(this T node, Node parent) where T : Node
        {
            node.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            return node;
        }
    }
}