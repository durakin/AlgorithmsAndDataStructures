using System;

namespace Lab4
{
    public class Laundry
    {
        private int _nextId = 1;

        private BinarySearchTree<LaundryOrder> Orders { get; }

        public void AddOrder(string name, string clotheType)
        {
            Orders.Insert(new LaundryOrder(_nextId++, name, clotheType));
        }

        public bool DeleteOrderByName(string name)
        {
            return Orders.Remove(name);
        }

        public string OrderToStringByName(string name)
        {
            return Orders.FindValue(name, out var found) ? found.ToString() : null;
        }

        public void PrintAllOrders()
        {
            Orders.TraverseTree(x => Console.WriteLine(x.ToString()));
        }

        public void PrintAsTree()
        {
            Orders.PrintTree();
        }

        public Laundry()
        {
            Orders = new BinarySearchTree<LaundryOrder>();
        }
    }
}