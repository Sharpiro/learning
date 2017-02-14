using System;
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

        internal static T WithParent<T>(this T node, Node parent) where T : Node
        {
            node.Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            return node;
        }

        internal static T BaseClone<T>(this T node) where T : Node
        {
            var currentType = typeof(T);
            var newNode = (T)Activator.CreateInstance(currentType, true);
            newNode.Annotation = node.Annotation;
            return newNode;
        }

        internal static T Clone<T>(this T node) where T : Node => node.CloneInternal<T>();
    }
}