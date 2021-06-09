using System;
using System.Collections.Generic;

namespace Lab1
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            int[,] lengthMatrix =
            {
                {0, 10, 2, 4, 14},
                {10, 0, 6, 13, 8},
                {2, 6, 0, 2, 7},
                {4, 13, 2, 0, 20},
                {14, 8, 7, 20, 0}
            };
            var graph = new Graph(lengthMatrix);  // Create Graph
            var parameters = new Parameters();
            parameters.T0 = 0.004;
            parameters.Show();
            var solver = new Solver(parameters, graph);
            var results = solver.RunAcs(); // Run ACS
            Console.WriteLine("Time: " + solver.GetExecutionTime());
            Console.ReadLine();
        }
    }
}