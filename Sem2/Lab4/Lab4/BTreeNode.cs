using System;

namespace Lab4
{
    public class BTreeNode<T> where T : IComparable
    {
        private BTreeNode<T> _left;
        private BTreeNode<T> _right;
        internal readonly BTree<T> Tree;

        public T Data { get; set; }
        internal BTreeNode<T> Parent { get; set; }

        internal BTreeNode<T> Left
        {
            get => _left;
            set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }

        internal BTreeNode<T> Right
        {
            get => _right;
            set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }


        public BTreeNode(T data, BTreeNode<T> parent, BTree<T> tree)
        {
            Data = data;
            Parent = parent;
            Tree = tree;
        }


        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }

        public void Balance()
        {
            switch (State)
            {
                case TreeState.RightHeavy when Right is {BalanceFactor: < 0}:
                    LeftRightRotation();
                    break;
                case TreeState.RightHeavy:
                    LeftRotation();
                    break;
                case TreeState.LeftHeavy when Left is {BalanceFactor: > 0}:
                    RightLeftRotation();
                    break;
                case TreeState.LeftHeavy:
                    RightRotation();
                    break;
                case TreeState.Balanced:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal void LeftRotation()
        {
            var rootParent = Parent;
            var root = this;
            var pivot = Right;

            var isLeftChild = rootParent != null && rootParent.Left == root;
            root.Right = pivot.Left;
            pivot.Left = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.Right != null)
                root.Right.Parent = root;

            if (Tree.Head == root)
            {
                Tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.Left = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.Right = pivot;
            }
        }

        internal void RightRotation()
        {
            var rootParent = Parent;
            var root = this;
            var pivot = Left;
            var isLeftChild = (rootParent != null) && rootParent.Left == root;

            root.Left = pivot.Right;
            pivot.Right = root;

            root.Parent = pivot;
            pivot.Parent = rootParent;

            if (root.Left != null)
                root.Left.Parent = root;

            if (Tree.Head == root)
            {
                Tree.Head = pivot;
            }
            else if (isLeftChild)
            {
                rootParent.Left = pivot;
            }
            else if (rootParent != null)
            {
                rootParent.Right = pivot;
            }
        }

        internal void LeftRightRotation()
        {
            Right.RightRotation();
            LeftRotation();
        }

        internal void RightLeftRotation()
        {
            Left.LeftRotation();
            RightRotation();
        }

        private static int MaxChildrenHeight(BTreeNode<T> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildrenHeight(node.Left), MaxChildrenHeight(node.Right));
            }

            return 0;
        }

        private int LeftHeight => MaxChildrenHeight(Left);

        private int RightHeight => MaxChildrenHeight(Right);

        internal TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }

                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }

                return TreeState.Balanced;
            }
        }

        internal int BalanceFactor => RightHeight - LeftHeight;
    }
}