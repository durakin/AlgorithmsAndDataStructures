using System;

namespace Lab4
{
    public static class TreeDrawer<T> where T : IComparable
    {
        private const string Cross = " ├─";
        private const string Corner = " └─";
        private const string Vertical = " │ ";
        private const string Space = "   ";

        public static void PrintNode(Node node, string indent)
        {
            Console.WriteLine(node.Key);
            if (node.Left != null) PrintChildNode(node.Left, indent, node.Right == null);
            if (node.Right != null) PrintChildNode(node.Right, indent, true);

            static void PrintChildNode(Node node, string indent, bool isLast)
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