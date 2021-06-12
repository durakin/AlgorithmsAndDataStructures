using System;

namespace Lab4
{
    public class BTree <T> where T : IComparable
    {
        private BTreeNode <T> _root;

        // A utility function to get height of the tree 
        private static int Height(BTreeNode<T> node)
        {
            return node?.Height ?? 0;
        }
        

        // A utility function to right 
        // rotate subtree rooted with y 
        // See the diagram given above. 
        private static BTreeNode<T> RightRotate(BTreeNode<T> y)
        {
            var x = y.Left;
            var t2 = x.Right;

            // Perform rotation 
            x.Right = y;
            y.Left = t2;

            // Update heights 
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            // Return new root 
            return x;
        }

        // A utility function to left 
        // rotate subtree rooted with x 
        // See the diagram given above. 
        private static BTreeNode<T> LeftRotate(BTreeNode<T> x)
        {
            var y = x.Right;
            var t2 = y.Left;

            // Perform rotation 
            y.Left = x;
            x.Right = t2;

            // Update heights 
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            // Return new root 
            return y;
        }

        // Get Balance factor of node N 
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
            /* 1. Perform the normal BST rotation */
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
                // Equal keys not allowed 
                default:
                    return bTreeNode;
            }

            /* 2. Update height of this ancestor node */
            bTreeNode.Height = 1 + Math.Max(Height(bTreeNode.Left),
                Height(bTreeNode.Right));

            /* 3. Get the balance factor of this ancestor 
            node to check whether this node became 
            Wunbalanced */
            var balance = GetBalance(bTreeNode);

            // If this node becomes unbalanced, then 
            // there are 4 cases Left Left Case 
            switch (balance)
            {
                case > 1 when data.CompareTo(bTreeNode.Left.Data) < 0:
                    return RightRotate(bTreeNode);
                // Right Right Case 
                case < -1 when data.CompareTo(bTreeNode.Right.Data) > 0:
                    return LeftRotate(bTreeNode);
                // Left Right Case 
                case > 1 when data.CompareTo(bTreeNode.Left.Data) > 0:
                    bTreeNode.Left = LeftRotate(bTreeNode.Left);
                    return RightRotate(bTreeNode);
                // Right Left Case 
                case < -1 when data.CompareTo(bTreeNode.Right.Data) < 0:
                    bTreeNode.Right = RightRotate(bTreeNode.Right);
                    return LeftRotate(bTreeNode);
                default:
                    /* return the (unchanged) node pointer */
                    return bTreeNode;
            }
        }

        /* Given a non-empty binary search tree, return the 
        node with minimum key value found in that tree. 
        Note that the entire tree does not need to be 
        searched. */
        private static BTreeNode<T> MinValueNode(BTreeNode<T> bTreeNode)
        {
            var current = bTreeNode;

            /* loop down to find the leftmost leaf */
            while (current.Left != null)
                current = current.Left;

            return current;
        }

        public void Remove(T data)
        {
            _root = DeleteNode(_root, data);
        }

        private static BTreeNode<T> DeleteNode(BTreeNode<T> root, T data)
        {
            // STEP 1: PERFORM STANDARD BST DELETE 
            if (root == null)
                return null;

            switch (data.CompareTo(root.Data))
            {
                // If the key to be deleted is smaller than 
                // the root's key, then it lies in left subtree 
                // If the key to be deleted is greater than the 
                // root's key, then it lies in right subtree 
                case < 0:
                    root.Left = DeleteNode(root.Left, data);
                    break;
                // if key is same as root's key, then this is the node 
                // to be deleted 
                case > 0:
                    root.Right = DeleteNode(root.Right, data);
                    break;
                default:
                {
                    // node with only one child or no child 
                    if (root.Left == null || root.Right == null)
                    {
                        var temp = root.Left ?? root.Right;

                        // No child case 
                        root = temp;
                        // the non-empty child 
                    }
                    else
                    {
                        // node with two children: Get the inorder 
                        // successor (smallest in the right subtree) 
                        var temp = MinValueNode(root.Right);

                        // Copy the inorder successor's data to this node 
                        root.Data = temp.Data;

                        // Delete the inorder successor 
                        root.Right = DeleteNode(root.Right, temp.Data);
                    }

                    break;
                }
            }

            // If the tree had only one node then return 
            if (root == null)
                return null;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE 
            root.Height = Math.Max(Height(root.Left),
                Height(root.Right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR
            // OF THIS NODE (to check whether 
            // this node became unbalanced) 
            var balance = GetBalance(root);

            switch (balance)
            {
                // If this node becomes unbalanced, 
                // then there are 4 cases 
                // Left Left Case 
                case > 1 when GetBalance(root.Left) >= 0:
                    return RightRotate(root);
                // Left Right Case 
                case > 1 when GetBalance(root.Left) < 0:
                    root.Left = LeftRotate(root.Left);
                    return RightRotate(root);
                // Right Right Case 
                case < -1 when GetBalance(root.Right) <= 0:
                    return LeftRotate(root);
                // Right Left Case 
                case < -1 when GetBalance(root.Right) > 0:
                    root.Right = RightRotate(root.Right);
                    return LeftRotate(root);
                default:
                    return root;
            }
        }

        // A utility function to print preorder traversal of 
        // the tree. The function also prints height of every 
        // node 
        /*void preOrder(BTreeNode bTreeNode)
        {
            if (bTreeNode != null)
            {
                Console.Write(bTreeNode.Key + " ");
                preOrder(bTreeNode.Left);
                preOrder(bTreeNode.Right);
            }
        }
        */

        public void PrintTree()
        {
            TreeDrawer<T>.PrintNode(_root, "");
        }
    }
}