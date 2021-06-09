using System;

namespace Lab6
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            bool[,] lengthMatrix =
            {
                {false,true,false,true,false,false,false},
                {true,false,true,true,true,false,false},
                {false,true,false,false,true,false,false},
                {true,true,false,false,true,true,false},
                {false,true,true,true,false,true,true},
                {false,false,false,true,true,false,true},
                {false,false,false,false,true,true,false}
            };
            var graph = new Graph(lengthMatrix);  // Create Graph
            var result = graph.GreedyColor();
            foreach (var (key, value) in result)
            {
                Console.WriteLine($"Вершина {key.Name} - цвет {value}");
            }
        }
    }
}