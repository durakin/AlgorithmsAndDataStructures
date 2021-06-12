using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> binaryTree = new();
 
            binaryTree.Add(1);
            binaryTree.Add(2);
            binaryTree.Add(7);
            binaryTree.Add(3);
            binaryTree.Add(10);
            binaryTree.Add(5);
            binaryTree.Add(8);
            binaryTree.Add(6);
            binaryTree.Add(14);
            binaryTree.Add(4);
            binaryTree.Add(9);
            binaryTree.Add(14);
 
            var node = binaryTree.Find(5);
            Console.WriteLine($"Found {node.Data}");
            var depth = binaryTree.GetTreeDepth();
            Console.WriteLine($"Tree's depth {depth}");
            Console.WriteLine("PreOrder Traversal:");
            binaryTree.PrefixTraverse(binaryTree.Root, Console.WriteLine);
            Console.WriteLine();
 
            Console.WriteLine("InOrder Traversal:");
            binaryTree.InfixTraverse(binaryTree.Root, Console.WriteLine);
            Console.WriteLine();
 
            Console.WriteLine("PostOrder Traversal:");
            binaryTree.PostfixTraverse(binaryTree.Root, Console.WriteLine);
            Console.WriteLine();
            
            binaryTree.PrintTree();
 
            binaryTree.Remove(7);
            binaryTree.Remove(8);
 
            Console.WriteLine("PreOrder Traversal After Removing Operation:");
            binaryTree.PrefixTraverse(binaryTree.Root, Console.WriteLine);
            Console.WriteLine();
            
            binaryTree.PrintTree();
        }
    }
}