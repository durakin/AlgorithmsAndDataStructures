using System;

namespace Lab4
{
    public class BTreeNode <T> where T : IComparable
    {
        public int Height; 
        public T Data { get; set; }
        internal BTreeNode <T> Left, Right; 
  
        public BTreeNode(T data) 
        {
            Data = data;
            Height = 1; 
        } 
    }
}