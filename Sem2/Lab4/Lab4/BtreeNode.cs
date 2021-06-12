using System;

namespace Lab4
{
    public class BtreeNode<T> where T : IComparable
    {
        public BtreeNode<T> LeftNode { get; set; }
        public BtreeNode<T> RightNode { get; set; }
        public T Data { get; set; }

        public BtreeNode(T data)
        {
            Data = data;
        }
    }
}