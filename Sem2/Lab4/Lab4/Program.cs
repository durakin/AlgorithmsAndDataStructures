using System;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<int> binarySearchTree = new();
            
            binarySearchTree.Insert(10);
            binarySearchTree.Insert(5);
            binarySearchTree.Insert(0);
            binarySearchTree.Insert(6);
            binarySearchTree.Insert(18);
            binarySearchTree.Insert(3);
            binarySearchTree.Insert(4);
            binarySearchTree.Insert(9);
            binarySearchTree.Insert(11);
            binarySearchTree.Insert(-1);
            binarySearchTree.Insert(1);
            binarySearchTree.Insert(2);

            binarySearchTree.PrintTree();

            Console.WriteLine("Infix traverse order:");
            binarySearchTree.TraverseTree(x => Console.Write($"{x} "), BinarySearchTree<int>.TraverseOrder.Infix);
            Console.WriteLine("\nPostfix traverse order:");
            binarySearchTree.TraverseTree(x => Console.Write($"{x} "), BinarySearchTree<int>.TraverseOrder.Postfix);
            Console.WriteLine("\nPrefix traverse order:");
            binarySearchTree.TraverseTree(x => Console.Write($"{x} "));
            Console.WriteLine("\nRemoving elements (true/false = found value in list");

            Console.WriteLine($"Removing {5} {binarySearchTree.Remove(5)}");
            Console.WriteLine($"Removing {14} {binarySearchTree.Remove(14)}");
            Console.WriteLine($"Removing {1} {binarySearchTree.Remove(1)}");
            Console.WriteLine($"Removing {2} {binarySearchTree.Remove(2)}");
            Console.WriteLine($"Removing {0} {binarySearchTree.Remove(0)}");
            Console.WriteLine($"Removing {-1} {binarySearchTree.Remove(-1)}");
            Console.WriteLine($"Removing {-10} {binarySearchTree.Remove(-10)}");
            Console.WriteLine($"Removing {9} {binarySearchTree.Remove(9)}");
            Console.WriteLine("After removing");

            binarySearchTree.PrintTree();

            var laundry = new Laundry();
            laundry.AddOrder("Vasiliy", "Suit");
            laundry.AddOrder("Ivan", "Black shirt");
            laundry.AddOrder("Rodion", "Jeans");
            laundry.AddOrder("Jhon", "Leather bag");
            laundry.AddOrder("Pyotr", "White shirt");
            laundry.AddOrder("Bojack", "Coat");
            laundry.AddOrder("Sergey", "Sneakers");
            Console.WriteLine("List as tree");
            laundry.PrintAsTree();
            
            Console.WriteLine($"Found by name Bojack {laundry.OrderToStringByName("Bojack")}");
            Console.WriteLine("Delete Vasiliy");
            laundry.DeleteOrderByName("Vasiliy");
            Console.WriteLine("List as tree");
            laundry.PrintAsTree();
            Console.WriteLine("List as raw list");
            laundry.PrintAllOrders();
        }
    }
}