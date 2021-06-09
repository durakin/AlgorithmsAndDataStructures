using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab8
{
    public class Graph
    {
        private readonly SortedSet<GraphEdge> _edges;

        public HashSet<GraphVertex> Vertices { get; }

        public Graph()
        {
            Vertices = new HashSet<GraphVertex>();
            _edges = new SortedSet<GraphEdge>(new EdgeComparer());
        }

        public int CountVertices()
        {
            return Vertices.Count;
        }

        private class EdgeComparer : IComparer<GraphEdge>
        {
            public int Compare(GraphEdge x, GraphEdge y)
            {
                var result = string.Compare(x.Head.Name, y.Head.Name, StringComparison.Ordinal);
                if (result == 0)
                {
                    result = string.Compare(x.Tail.Name, y.Tail.Name, StringComparison.Ordinal);
                }

                if (result != 0 && string.Compare(x.Head.Name, y.Tail.Name, StringComparison.Ordinal) == 0)
                {
                    result = string.Compare(x.Tail.Name, y.Head.Name, StringComparison.Ordinal);
                }

                if (result == 0)
                {
                    result = x.Weight.CompareTo(y.Weight);
                }

                return result;
            }
        }

        public override string ToString()
        {
            var result = Vertices.Aggregate("GRAPH\nVertices:\n",
                (current, vertex) => current + (vertex.ToString() + ' '));
            result += "\nEdges:\n";
            result = _edges.Aggregate(result, (current, edge) => current + edge + "  ");
            return result;
        }

        public bool AddVertex(GraphVertex vertex)
        {
            if (Vertices.Any(x => x.Name == vertex.Name))
            {
                return false;
            }

            Vertices.Add(vertex);
            return true;
        }

        public bool AddEdge(GraphEdge edge)
        {
            if (!(Vertices.Contains(edge.Head) && Vertices.Contains(edge.Tail)))
            {
                return false;
            }

            _edges.Add(edge);
            return true;
        }

        public GraphVertex VertexByName(string name)
        {
            return Vertices.FirstOrDefault(x => x.Name == name);
        }

        public Graph PrimsGraph()
        {
            if (CountVertices() <= 1)
            {
                return this;
            }

            var result = new Graph();

            result.AddVertex(Vertices.First());

            while (result.CountVertices() != CountVertices())
            {
                GraphVertex nextVertex = null;
                GraphEdge nextEdge = null;
                foreach (var vertex in result.Vertices)
                {
                    foreach (var edge in _edges.Where(x =>
                        x.Tail == vertex && !result.Vertices.Contains(x.Head) ||
                        x.Head == vertex && !result.Vertices.Contains(x.Tail)))
                    {
                        if (nextEdge == null || nextEdge.Weight > edge.Weight)
                        {
                            nextVertex = vertex;
                            nextEdge = edge;
                        }
                    }
                }

                if (nextVertex == null)
                {
                    throw new Exception("Graph connectivity is in doubt");
                }

                result.Vertices.Add(nextVertex);
                result.Vertices.Add(result.Vertices.Contains(nextEdge.Head) ? nextEdge.Tail : nextEdge.Head);
                result._edges.Add(nextEdge);
            }

            return result;
        }
    }
}