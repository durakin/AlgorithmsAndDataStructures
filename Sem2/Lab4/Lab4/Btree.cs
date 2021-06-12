using System;

namespace Lab4
{
    class BinaryTree<T> where T : IComparable
    {
        public BtreeNode<T> Root { get; private set; }

        public bool Add(T value)
        {
            BtreeNode<T> before = null, after = Root;

            while (after != null)
            {
                before = after;
                switch (value.CompareTo(after.Data))
                {
                    //Is new node in left tree? 
                    case < 0:
                        after = after.LeftNode;
                        break;
                    //Is new node in right tree?
                    case > 0:
                        after = after.RightNode;
                        break;
                    default:
                        //Exist same value
                        return false;
                }
            }

            var newNode = new BtreeNode<T>(value);

            if (Root == null) //Tree ise empty
                Root = newNode;
            else
            {
                if (before != null && value.CompareTo(before.Data) < 0)
                    before.LeftNode = newNode;
                else if (before != null) before.RightNode = newNode;
            }

            return true;
        }

        public BtreeNode<T> Find(T value)
        {
            return Find(value, Root);
        }

        public void Remove(T value)
        {
            Root = Remove(Root, value);
        }

        private BtreeNode<T> Remove(BtreeNode<T> parent, T key)
        {
            if (parent == null) return null;

            switch (key.CompareTo(parent.Data))
            {
                case < 0:
                    parent.LeftNode = Remove(parent.LeftNode, key);
                    break;
                case > 0:
                    parent.RightNode = Remove(parent.RightNode, key);
                    break;
                // if value is same as parent's value, then this is the node to be deleted  
                default:
                {
                    // node with only one child or no child  
                    if (parent.LeftNode == null)
                        return parent.RightNode;
                    if (parent.RightNode == null)
                        return parent.LeftNode;

                    // node with two children: Get the inorder successor (smallest in the right subtree)  
                    parent.Data = MinValue(parent.RightNode);

                    // Delete the inorder successor  
                    parent.RightNode = Remove(parent.RightNode, parent.Data);
                    break;
                }
            }

            return parent;
        }

        private T MinValue(BtreeNode<T> node)
        {
            var minValue = node.Data;

            while (node.LeftNode != null)
            {
                minValue = node.LeftNode.Data;
                node = node.LeftNode;
            }

            return minValue;
        }

        private BtreeNode<T> Find(T value, BtreeNode<T> parent)
        {
            if (parent == null) return null;
            return value.CompareTo(parent.Data) switch
            {
                < 0 => Find(value, parent.LeftNode),
                > 0 => Find(value, parent.RightNode),
                0 => parent
            };
        }

        public int GetTreeDepth()
        {
            return GetTreeDepth(Root);
        }

        private int GetTreeDepth(BtreeNode<T> parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }

        public void PrefixTraverse(BtreeNode<T> parent, Action<BtreeNode<T>> action)
        {
            if (parent == null) return;
            action(parent);
            PrefixTraverse(parent.LeftNode, action);
            PrefixTraverse(parent.RightNode, action);
        }

        public void InfixTraverse(BtreeNode<T> parent, Action<BtreeNode<T>> action)
        {
            if (parent == null) return;
            InfixTraverse(parent.LeftNode, action);
            action(parent);
            InfixTraverse(parent.RightNode, action);
        }

        public void PostfixTraverse(BtreeNode<T> parent, Action<BtreeNode<T>> action)
        {
            if (parent == null) return;
            PostfixTraverse(parent.LeftNode, action);
            PostfixTraverse(parent.RightNode, action);
            action(parent);
        }

        public void BalanceTree()
        {
            
        }

        public void PrintTree()
        {
            TreeDrawer<T>.PrintNode(Root, "");
        }
    }
}