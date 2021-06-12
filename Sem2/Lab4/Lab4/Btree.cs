using System;

namespace Lab4
{
    public class BTree<T> where T : IComparable
    {
        public BTreeNode<T> Head { get; internal set; }


        public void Add(T input)
        {
            AddTo(input, Head);
        }

        private void AddTo(T input, BTreeNode<T> current)
        {
            if (Head == null)
            {
                Head = new BTreeNode<T>(input, null, this);
                return;
            }

            if (input.CompareTo(current.Data) < 0)
            {
                if (current.Left == null)
                {
                    current.Left = new BTreeNode<T>(input, current, this);
                }
                else
                {
                    AddTo(input, current.Left);
                }
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new BTreeNode<T>(input, current, this);
                }
                else
                {
                    AddTo(input, current.Right);
                }
            }

            var parent = current;
            while (parent != null)
            {
                if (parent.State != TreeState.Balanced)
                {
                    parent.Balance();
                }

                parent = parent.Parent; //keep going up
            }
        }
        
        private T MinValue(BTreeNode<T> node)
        {
            var minValue = node.Data;

            while (node.Left != null)
            {
                minValue = node.Left.Data;
                node = node.Left;
            }

            return minValue;
        }


       // public bool Remove(T input)
       // {
       //     var current = FindWithParent(input, out var parent);
//
       //     var removeSuccessful = RemoveNode(current, input);
//
       //     if (removeSuccessful == null) return true;
//
       //     while (parent != null)
       //     {
       //         if (parent.State != TreeState.Balanced)
       //         {
       //             parent.Balance();
       //         }
//
       //         parent = parent.Parent; //keep going up
       //     }
//
       //     return true;
       // }

        
        public void Remove(T value)
        {
            Head = Remove(Head, value);
        }

        private BTreeNode<T> Remove(BTreeNode<T> parent, T key)
        {
            if (parent == null) return null;

            switch (key.CompareTo(parent.Data))
            {
                case < 0:
                    parent.Left = Remove(parent.Right, key);
                    break;
                case > 0:
                    parent.Right= Remove(parent.Left, key);
                    break;
                // if value is same as parent's value, then this is the node to be deleted  
                default:
                {
                    // node with only one child or no child  
                    if (parent.Left == null)
                        return parent.Right;
                    if (parent.Right == null)
                        return parent.Left;

                    // node with two children: Get the inorder successor (smallest in the right subtree)  
                    parent.Data = MinValue(parent.Right);

                    // Delete the inorder successor  
                    parent.Right = Remove(parent.Right, parent.Data);
                    break;
                }
            }

            return parent;
        }
        
        //public BTreeNode<T> RemoveNode(BTreeNode<T> current, T key)
        //{
            
            
            //// STEP 1: PERFORM STANDARD BST DELETE  
            //if (current == null)
            //    return null;
//
            //// If the key to be deleted is smaller  
            //// than the Head's key, then it lies 
            //// in left subtree  
            //if (key.CompareTo(current.Data) < 0)
            //    RemoveNode(current.Left, key);
//
            //// If the key to be deleted is greater  
            //// than the Head's key, then it lies  
            //// in right subtree  
            //else if (key.CompareTo(current.Data) > 0)
            //    RemoveNode(current.Right, key);
//
            //// if key is same as Head's key, then  
            //// This is the node to be deleted  
            //else
            //{
            //    // node with only one child or no child  
            //    if ((current.Left == null) ||
            //        (current.Right == null))
            //    {
            //        var temp = current.Left ?? current.Right;
//
            //        // No child case  
            //        if (temp == null)
            //        {
            //            if (current.Parent.Left == current)
            //            {
            //                current.Parent.Left = null;
            //                current = null;
            //            }
//
            //            if (current != null && current.Parent.Right == current)
            //            {
            //                current.Parent.Right = null;
            //                current = null;
            //            }
            //        }
            //        else // One child case  
            //            current = temp; // Copy the contents of  
//
            //        // the non-empty child  
            //    }
            //    else
            //    {
            //        // node with two children: Get the inorder  
            //        // successor (smallest in the right subtree)  
            //        var temp = MinValue(current.Right);
//
            //        // Copy the inorder successor's  
            //        // data to this node  
            //        current.Data = temp;
//
            //        // Delete the inorder successor  
            //        current.Right = RemoveNode(current.Right,
            //            temp);
            //    }
            //}
//
            //// If the tree had only one node 
            //// then return  
            //if (current == null)
            //    return null;
//
            //// STEP 2: UPDATE HEIGHT OF THE CURRENT NODE  
//
//
            //// STEP 3: GET THE BALANCE FACTOR OF  
            //// THIS NODE (to check whether this  
            //// node became unbalanced)  
            //int balance = current.BalanceFactor;
//
            //// If this node becomes unbalanced,  
            //// then there are 4 cases  
//
            //// Left Left Case  
            //if (balance > 1 &&
            //    current.Left.BalanceFactor >= 0)
            //{
            //    current.RightRotation();
            //    return current;
            //}
//
            //// Left Right Case  
            //if (balance > 1 &&
            //    current.Left.BalanceFactor < 0)
            //{
            //    current.RightLeftRotation();
            //    return current;
            //}
//
            //// Right Right Case  
            //if (balance < -1 &&
            //    current.Right.BalanceFactor <= 0)
            //{
            //    current.LeftRotation();
            //    return current;
            //}
//
            //// Right Left Case  
            //if (balance < -1 &&
            //    current.Right.BalanceFactor > 0)
            //{
            //    current.LeftRightRotation();
            //    return current;
            //}
//
            //return current;
        //}


        public bool Search(T input)
        {
            return SearchNode(input, Head);
        }

        private bool SearchNode(T input, BTreeNode<T> current)
        {
            if (current == null)
            {
                return false;
            }

            if (input.CompareTo(current.Data) == 0)
            {
                return true;
            }

            return SearchNode(input,
                input.CompareTo(current.Data) < 0
                    ? current.Left
                    : current.Right);
        }

        private BTreeNode<T> FindWithParent(T input, out BTreeNode<T> parent)
        {
            var current = Head;
            parent = null;

            while (current != null)
            {
                var compare = current.Data.CompareTo(input);

                switch (compare)
                {
                    case > 0:
                        parent = current;
                        current = current.Left;
                        break;
                    case < 0:
                        parent = current;
                        current = current.Right;
                        break;
                    default:
                        return current;
                }
            }

            return null;
        }


        public void PrefixTraverse(BTreeNode<T> node, Action<BTreeNode<T>> action)
        {
            if (node == null) return;
            action(node);
            PrefixTraverse(node.Left, action);
            PrefixTraverse(node.Right, action);
        }

        public void InfixTraverse(BTreeNode<T> node, Action<BTreeNode<T>> action)
        {
            if (node == null) return;
            InfixTraverse(node.Left, action);
            action(node);
            InfixTraverse(node.Right, action);
        }

        public void PostfixTraverse(BTreeNode<T> node, Action<BTreeNode<T>> action)
        {
            if (node == null) return;
            PostfixTraverse(node.Left, action);
            PostfixTraverse(node.Right, action);
            action(node);
        }

        public void PrintTree()
        {
            TreeDrawer<T>.PrintNode(Head, "");
        }
    }
}