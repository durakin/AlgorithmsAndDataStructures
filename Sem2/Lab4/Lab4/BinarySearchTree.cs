using System;

namespace Lab4
{
    public class BinarySearchTree<T> where T : IComparable
    {
        private BTreeNode<T> _root;

        private static int Height(BTreeNode<T> node)
        {
            return node?.Height ?? 0;
        }
        
        private static BTreeNode<T> RightRotate(BTreeNode<T> y)
        {
            var x = y.Left;
            var t2 = x.Right;

            x.Right = y;
            y.Left = t2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }
        
        private static BTreeNode<T> LeftRotate(BTreeNode<T> x)
        {
            var y = x.Right;
            var t2 = y.Left;

            y.Left = x;
            x.Right = t2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        private static int GetBalance(BTreeNode<T> node)
        {
            return node != null ? Height(node.Left) - Height(node.Right) : 0;
        }

        public void Insert(T data)
        {
            _root = Insert(_root, data);
        }

        private static BTreeNode<T> Insert(BTreeNode<T> bTreeNode, T data)
        {
            if (bTreeNode == null)
                return new BTreeNode<T>(data);

            switch (data.CompareTo(bTreeNode.Data))
            {
                case < 0:
                    bTreeNode.Left = Insert(bTreeNode.Left, data);
                    break;
                case > 0:
                    bTreeNode.Right = Insert(bTreeNode.Right, data);
                    break;
                default:
                    return bTreeNode;
            }

            bTreeNode.Height = 1 + Math.Max(Height(bTreeNode.Left),
                Height(bTreeNode.Right));

            var balance = GetBalance(bTreeNode);

            switch (balance)
            {
                case > 1 when data.CompareTo(bTreeNode.Left.Data) < 0:
                    return RightRotate(bTreeNode);
                case < -1 when data.CompareTo(bTreeNode.Right.Data) > 0:
                    return LeftRotate(bTreeNode);
                case > 1 when data.CompareTo(bTreeNode.Left.Data) > 0:
                    bTreeNode.Left = LeftRotate(bTreeNode.Left);
                    return RightRotate(bTreeNode);
                case < -1 when data.CompareTo(bTreeNode.Right.Data) < 0:
                    bTreeNode.Right = RightRotate(bTreeNode.Right);
                    return LeftRotate(bTreeNode);
                default:
                    return bTreeNode;
            }
        }

        private static BTreeNode<T> MinValueNode(BTreeNode<T> bTreeNode)
        {
            var current = bTreeNode;

            while (current.Left != null)
                current = current.Left;

            return current;
        }

        private void Remove(T data)
        {
            _root = DeleteNode(_root, data);
        }

        public bool Remove(object value)
        {
            if (!FindValue(_root, value, out var data)) return false;
            Remove(data);
            return true;
        }

        public bool FindValue(object value, out T found)
        {
            if (FindValue(_root, value, out found)) return true;
            found = default;
            return false;
        }

        private bool FindValue(BTreeNode<T> node, object value, out T found)
        {
            if (node == null)
            {
                found = default;
                return false;
            }

            switch (node.Data.CompareTo(value))
            {
                case < 0: return FindValue(node.Right, value, out found);
                case > 0: return FindValue(node.Left, value, out found);
                default:
                    found = node.Data;
                    return true;
            }
        }

        private static BTreeNode<T> DeleteNode(BTreeNode<T> root, T data)
        {
            if (root == null)
                return null;

            switch (data.CompareTo(root.Data))
            {
                case < 0:
                    root.Left = DeleteNode(root.Left, data);
                    break;
                case > 0:
                    root.Right = DeleteNode(root.Right, data);
                    break;
                default:
                {
                    if (root.Left == null || root.Right == null)
                    {
                        var temp = root.Left ?? root.Right;

                        root = temp;
                    }
                    else
                    {

                        var temp = MinValueNode(root.Right);

                        root.Data = temp.Data;

                        root.Right = DeleteNode(root.Right, temp.Data);
                    }

                    break;
                }
            }

            if (root == null)
                return null;

            root.Height = Math.Max(Height(root.Left),
                Height(root.Right)) + 1;

            var balance = GetBalance(root);

            switch (balance)
            {
                case > 1 when GetBalance(root.Left) >= 0:
                    return RightRotate(root);
                case > 1 when GetBalance(root.Left) < 0:
                    root.Left = LeftRotate(root.Left);
                    return RightRotate(root);
                case < -1 when GetBalance(root.Right) <= 0:
                    return LeftRotate(root);
                case < -1 when GetBalance(root.Right) > 0:
                    root.Right = RightRotate(root.Right);
                    return LeftRotate(root);
                default:
                    return root;
            }
        }

        public enum TraverseOrder
        {
            Infix,
            Prefix,
            Postfix
        }

        public void TraverseTree(Action<T> action, TraverseOrder order = TraverseOrder.Prefix)
        {
            switch (order)
            {
                case TraverseOrder.Infix:
                    InfixTraverse(_root, action);
                    break;
                case TraverseOrder.Prefix:
                    PrefixTraverse(_root, action);
                    break;
                case TraverseOrder.Postfix:
                    PostfixTraverse(_root, action);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
        }

        private static void InfixTraverse(BTreeNode<T> node, Action<T> action)
        {
            if (node == null) return;
            if (node.Left != null) InfixTraverse(node.Left, action);
            action(node.Data);
            if (node.Right != null) InfixTraverse(node.Right, action);
        }

        private static void PrefixTraverse(BTreeNode<T> node, Action<T> action)
        {
            if (node == null) return;
            action(node.Data);
            if (node.Left != null) PrefixTraverse(node.Left, action);
            if (node.Right != null) PrefixTraverse(node.Right, action);
        }

        private static void PostfixTraverse(BTreeNode<T> node, Action<T> action)
        {
            if (node == null) return;
            if (node.Left != null) PostfixTraverse(node.Left, action);
            if (node.Right != null) PostfixTraverse(node.Right, action);
            action(node.Data);
        }

        public void PrintTree()
        {
            TreeDrawer<T>.PrintNode(_root, "");
        }
    }
}