using System;

namespace Lab4
{
    public class Node 
    { 
        public int Key, Height; 
        public Node Left, Right; 
  
        public Node(int d) 
        { 
            Key = d; 
            Height = 1; 
        } 
    }
}