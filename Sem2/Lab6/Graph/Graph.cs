using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6
{
    public class Graph
    {
        private List<Vertex> Vertices { get; }
        private List<Edge> Edges { get; }

        private class VertexComparer : IComparer<Vertex>
        {
            public int Compare(Vertex x, Vertex y)
            {
                return string.Compare(x.Name, y.Name);
            }
        }

        public Graph(bool[,] adjacencyMatrix)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            for (var i = adjacencyMatrix.GetLowerBound(0);
                i <= adjacencyMatrix.GetUpperBound(0);
                i++)
            {
                Vertices.Add(new Vertex((i+1).ToString()));
            }

            CreateEdges(adjacencyMatrix);
        }

        private void CreateEdges(bool[,] adjacencyMatrix)
        {
            for (var i = 0; i < Vertices.Count; i++)
            {
                for (var j = 0; j < Vertices.Count; j++)
                {
                    if (i == j || !adjacencyMatrix[i, j]) continue;
                    Edges.Add(new Edge(Vertices[i], Vertices[j]));
                }
            }
        }


        private List<Vertex> Neighbours(Vertex vertex)
        {
            return (from edge in Edges where edge.Tail == vertex select edge.Head).ToList();
        }

        public Dictionary<Vertex, int> GreedyColor()
        {
            Vertices.Sort(new VertexComparer());
            Dictionary<Vertex, int> colors = new();
            foreach (var vertex in Vertices)
            {
                List<int> neighbourColorUsage = new();
                for (var i = 0; Vertices[i] != vertex; i++)
                {
                    if (!Neighbours(vertex).Contains(Vertices[i]))
                    {
                        continue;
                    }

                    if (!colors.ContainsKey(Vertices[i]))
                    {
                        continue;
                    }

                    neighbourColorUsage.Add(colors[Vertices[i]]);
                }

                var newColor = 1;
                while (neighbourColorUsage.Contains(newColor))
                {
                    newColor++;
                }

                colors[vertex] = newColor;
                neighbourColorUsage.Clear();
            }

            return colors;
        }
    }
}