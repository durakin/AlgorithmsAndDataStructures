using System;

namespace Lab4
{
    public static class TreeDrawer<T> where T : IComparable
    {
        private const string Cross = " ├─";
        private const string Corner = " └─";
        private const string Vertical = " │ ";
        private const string Space = "   ";

        public static void PrintNode(BtreeNode<T> node, string indent)
        {
            Console.WriteLine(node.Data);
            if (node.LeftNode != null) PrintChildNode(node.LeftNode, indent, node.RightNode == null);
            if (node.RightNode != null) PrintChildNode(node.RightNode, indent, true);

            static void PrintChildNode(BtreeNode<T> node, string indent, bool isLast)
            {
                Console.Write(indent);

                if (isLast)
                {
                    Console.Write(Corner);
                    indent += Space;
                }
                else
                {
                    Console.Write(Cross);
                    indent += Vertical;
                }

                PrintNode(node, indent);
            }
        }
    }
}