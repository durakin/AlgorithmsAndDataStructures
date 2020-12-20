using System;
using System.Drawing;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph();
            string s;
            byte n;
            do
            {
                Console.WriteLine("Enter number of vertices");
                s = Console.ReadLine();
            } while (!byte.TryParse(s, out n));

            for (var i = 0; i < n; i++)
            {
                graph.AddVertex(new GraphVertex((i + 1).ToString()));
            }

            do
            {
                Console.WriteLine("Enter number of edges");
                s = Console.ReadLine();
            } while (!byte.TryParse(s, out n));

            if (graph.CountVertices() == 0)
            {
                Console.WriteLine("No vertices in graph");
                return;
            }

            for (var i = 0; i < n; i++)
            {
                string from;
                string to;
                double weight = -1;
                Console.WriteLine($"Edge {i + 1} of {n}. Edge from: ");
                from = Console.ReadLine();
                Console.WriteLine($"Edge {i + 1} of {n}. Edge from {from} to: ");
                to = Console.ReadLine();
                do
                {
                    Console.WriteLine($"Edge from {from} to {to}. Weight: ");
                    s = Console.ReadLine();
                } while (!(double.TryParse(s, out weight) && weight >= 0));

                var vertexFrom = graph.VertexByName(from);
                var vertexTo = graph.VertexByName(to);
                if (graph.AddEdge(new GraphEdge(vertexFrom, vertexTo, weight)))
                {
                    Console.WriteLine($"Added edge from {from} to {to}!");
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                    i--;
                }
            }

            Console.WriteLine(graph.ToString());
            
            var ostov = graph.PrimsGraph();
            
            Console.WriteLine(ostov.ToString());
        }
    }
}