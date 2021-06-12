using System;

namespace Lab4
{
    public class BTree
    {
        Node root;

        // A utility function to get height of the tree 
        int height(Node N)
        {
            if (N == null)
                return 0;
            return N.Height;
        }
        

        // A utility function to right 
        // rotate subtree rooted with y 
        // See the diagram given above. 
        Node rightRotate(Node y)
        {
            Node x = y.Left;
            Node T2 = x.Right;

            // Perform rotation 
            x.Right = y;
            y.Left = T2;

            // Update heights 
            y.Height = Math.Max(height(y.Left), height(y.Right)) + 1;
            x.Height = Math.Max(height(x.Left), height(x.Right)) + 1;

            // Return new root 
            return x;
        }

        // A utility function to left 
        // rotate subtree rooted with x 
        // See the diagram given above. 
        Node leftRotate(Node x)
        {
            Node y = x.Right;
            Node T2 = y.Left;

            // Perform rotation 
            y.Left = x;
            x.Right = T2;

            // Update heights 
            x.Height = Math.Max(height(x.Left), height(x.Right)) + 1;
            y.Height = Math.Max(height(y.Left), height(y.Right)) + 1;

            // Return new root 
            return y;
        }

        // Get Balance factor of node N 
        int getBalance(Node N)
        {
            if (N == null)
                return 0;
            return height(N.Left) - height(N.Right);
        }

        public void Insert(int key)
        {
            root = insert(root, key);
        }
        private Node insert(Node node, int key)
        {
            /* 1. Perform the normal BST rotation */
            if (node == null)
                return (new Node(key));

            if (key < node.Key)
                node.Left = insert(node.Left, key);
            else if (key > node.Key)
                node.Right = insert(node.Right, key);
            else // Equal keys not allowed 
                return node;

            /* 2. Update height of this ancestor node */
            node.Height = 1 + Math.Max(height(node.Left),
                height(node.Right));

            /* 3. Get the balance factor of this ancestor 
            node to check whether this node became 
            Wunbalanced */
            int balance = getBalance(node);

            // If this node becomes unbalanced, then 
            // there are 4 cases Left Left Case 
            if (balance > 1 && key < node.Left.Key)
                return rightRotate(node);

            // Right Right Case 
            if (balance < -1 && key > node.Right.Key)
                return leftRotate(node);

            // Left Right Case 
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = leftRotate(node.Left);
                return rightRotate(node);
            }

            // Right Left Case 
            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = rightRotate(node.Right);
                return leftRotate(node);
            }

            /* return the (unchanged) node pointer */
            return node;
        }

        /* Given a non-empty binary search tree, return the 
        node with minimum key value found in that tree. 
        Note that the entire tree does not need to be 
        searched. */
        Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the leftmost leaf */
            while (current.Left != null)
                current = current.Left;

            return current;
        }

        public void Remove(int key)
        {
            root = deleteNode(root, key);
        }
        Node deleteNode(Node root, int key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE 
            if (root == null)
                return root;

            // If the key to be deleted is smaller than 
            // the root's key, then it lies in left subtree 
            if (key < root.Key)
                root.Left = deleteNode(root.Left, key);

            // If the key to be deleted is greater than the 
            // root's key, then it lies in right subtree 
            else if (key > root.Key)
                root.Right = deleteNode(root.Right, key);

            // if key is same as root's key, then this is the node 
            // to be deleted 
            else
            {
                // node with only one child or no child 
                if ((root.Left == null) || (root.Right == null))
                {
                    Node temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;

                    // No child case 
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // One child case 
                        root = temp; // Copy the contents of 
                    // the non-empty child 
                }
                else
                {
                    // node with two children: Get the inorder 
                    // successor (smallest in the right subtree) 
                    Node temp = minValueNode(root.Right);

                    // Copy the inorder successor's data to this node 
                    root.Key = temp.Key;

                    // Delete the inorder successor 
                    root.Right = deleteNode(root.Right, temp.Key);
                }
            }

            // If the tree had only one node then return 
            if (root == null)
                return root;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE 
            root.Height = Math.Max(height(root.Left),
                height(root.Right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR
            // OF THIS NODE (to check whether 
            // this node became unbalanced) 
            int balance = getBalance(root);

            // If this node becomes unbalanced, 
            // then there are 4 cases 
            // Left Left Case 
            if (balance > 1 && getBalance(root.Left) >= 0)
                return rightRotate(root);

            // Left Right Case 
            if (balance > 1 && getBalance(root.Left) < 0)
            {
                root.Left = leftRotate(root.Left);
                return rightRotate(root);
            }

            // Right Right Case 
            if (balance < -1 && getBalance(root.Right) <= 0)
                return leftRotate(root);

            // Right Left Case 
            if (balance < -1 && getBalance(root.Right) > 0)
            {
                root.Right = rightRotate(root.Right);
                return leftRotate(root);
            }

            return root;
        }

        // A utility function to print preorder traversal of 
        // the tree. The function also prints height of every 
        // node 
        void preOrder(Node node)
        {
            if (node != null)
            {
                Console.Write(node.Key + " ");
                preOrder(node.Left);
                preOrder(node.Right);
            }
        }

        public void PrintTree()
        {
            TreeDrawer<int>.PrintNode(root, "");
        }
    }
}