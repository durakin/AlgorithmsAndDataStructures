using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
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
                Console.WriteLine($"Edge {i + 1} of {n}. Edge from: ");
                from = Console.ReadLine();
                Console.WriteLine($"Edge {i + 1} of {n}. Edge from {from} to: ");
                to = Console.ReadLine();
                var vertexFrom = graph.VertexByName(from);
                var vertexTo = graph.VertexByName(to);
                if (graph.AddEdge(new GraphEdge(vertexFrom, vertexTo)))
                {
                    Console.WriteLine($"Added edge from {from} to {to}!");
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                    i--;
                }
            }

            do
            {
                Console.WriteLine("Name of start vertex of the DFS");
                s = Console.ReadLine();
            } while (graph.VertexByName(s) == null);

            Console.WriteLine($"Starting DFS from {s}, order of visiting:");
            var compliance = new Dictionary<GraphVertex, bool>();

            graph.InitDfs(graph.VertexByName(s), x => Console.WriteLine(x.Name), compliance);
            Console.WriteLine("Following vertices were never visited:");
            foreach (var i in compliance.Where(x => x.Value == false))
            {
                Console.WriteLine(i.Key.Name);
            }

            System.Console.ReadLine();
        }
    }
}