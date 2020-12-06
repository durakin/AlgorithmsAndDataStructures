using System;
using System.Linq;

namespace Lab7
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

            var floydResult = graph.Floyd();
            Console.WriteLine("FLOYD RESULTS\n");
            foreach (var i in graph.Vertices)
            {
                foreach (var j in graph.Vertices)
                {
                    Console.WriteLine($"\nRoute from {i.Name} to {j.Name}:");
                    GraphVertex x = i;
                    if (floydResult.Item2[(i, j)] == null)
                    {
                        Console.WriteLine("No route");
                        continue;
                    }

                    do
                    {
                        Console.Write($"{x.Name} -> ");
                        x = floydResult.Item2[(x, j)];
                    } while (x != j);

                    Console.WriteLine($"{j.Name}\nRoute weight: {floydResult.Item1[(i, j)]}");
                }
            }

            Console.ReadLine();
        }
    }
}