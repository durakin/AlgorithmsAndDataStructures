using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> bTree = new();
 
            bTree.Insert(9); 
            bTree.Insert(5); 
            bTree.Insert(10); 
            bTree.Insert(0); 
            bTree.Insert(6); 
            bTree.Insert(11); 
            bTree.Insert(-1); 
            bTree.Insert(1); 
            bTree.Insert(2); 
 
            //var node = bTree.Find(5);
            //Console.WriteLine($"Found {node.Data}");
            //var depth = bTree.GetTreeDepth();
            //Console.WriteLine($"Tree's depth {depth}");
            //Console.WriteLine("PreOrder Traversal:");
            //bTree.PrefixTraverse(bTree.Head, Console.WriteLine);
            //Console.WriteLine();
 //
            //Console.WriteLine("InOrder Traversal:");
            //bTree.InfixTraverse(bTree.Head, Console.WriteLine);
            //Console.WriteLine();
 //
            //Console.WriteLine("PostOrder Traversal:");
            //bTree.PostfixTraverse(bTree.Head, Console.WriteLine);
            //Console.WriteLine();
            
            bTree.PrintTree();
 
           bTree.Remove(14);
           bTree.Remove(1);
           bTree.Remove(2);
           bTree.Remove(10);
           bTree.Remove(5);
           bTree.Remove(11);
           bTree.Remove(0);
           bTree.Remove(6);
           bTree.Remove(-1);
           bTree.Remove(-10);
           bTree.Remove(9);

            //bTree.Remove(5);
 
            //Console.WriteLine("PreOrder Traversal After Removing Operation:");
            //bTree.PrefixTraverse(bTree.Head, Console.WriteLine);
            Console.WriteLine();
            
            bTree.PrintTree();
        }
    }
}