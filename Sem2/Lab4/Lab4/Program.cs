using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> bTree = new();
 
            bTree.Add(1);
            bTree.Add(2);
            bTree.Add(7);
            bTree.Add(3);
            bTree.Add(10);
            bTree.Add(5);
            bTree.Add(8);
            bTree.Add(6);
            bTree.Add(4);
            bTree.Add(9);
            bTree.Add(14);
 
            //var node = bTree.Find(5);
            //Console.WriteLine($"Found {node.Data}");
            //var depth = bTree.GetTreeDepth();
            //Console.WriteLine($"Tree's depth {depth}");
            Console.WriteLine("PreOrder Traversal:");
            bTree.PrefixTraverse(bTree.Head, Console.WriteLine);
            Console.WriteLine();
 
            Console.WriteLine("InOrder Traversal:");
            bTree.InfixTraverse(bTree.Head, Console.WriteLine);
            Console.WriteLine();
 
            Console.WriteLine("PostOrder Traversal:");
            bTree.PostfixTraverse(bTree.Head, Console.WriteLine);
            Console.WriteLine();
            
            bTree.PrintTree();
 
           bTree.Remove(14);
           bTree.Remove(1);
           bTree.Remove(2);
           bTree.Remove(10);
            //bTree.Remove(5);
 
            Console.WriteLine("PreOrder Traversal After Removing Operation:");
            bTree.PrefixTraverse(bTree.Head, Console.WriteLine);
            Console.WriteLine();
            
            bTree.PrintTree();
        }
    }
}