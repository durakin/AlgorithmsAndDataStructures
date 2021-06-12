using System;

namespace Lab4
{
    public static class TreeDrawer<T> where T : IComparable
    {
        private const string Cross = " ├─";
        private const string Corner = " └─";
        private const string Vertical = " │ ";
        private const string Space = "   ";

        public static void PrintNode(BTreeNode<T> bTreeNode, string indent)
        {
            if (bTreeNode == null)
            {
                return;
            }
            Console.WriteLine(bTreeNode.Data);
            if (bTreeNode.Left != null) PrintChildNode(bTreeNode.Left, indent, bTreeNode.Right == null);
            if (bTreeNode.Right != null) PrintChildNode(bTreeNode.Right, indent, true);

            static void PrintChildNode(BTreeNode<T> node, string indent, bool isLast)
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
