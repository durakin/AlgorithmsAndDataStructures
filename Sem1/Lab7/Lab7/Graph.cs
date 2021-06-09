using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Lab7
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

        public int CountEdges()
        {
            return _edges.Count;
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

                if (result == 0)
                {
                    result = x.Weight.CompareTo(y.Weight);
                }

                return result;
            }
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
        
        private double ShortestEdgeWeight(GraphVertex a, GraphVertex b)
        {
            if (!(Vertices.Contains(a) && Vertices.Contains(b)))
            {
                throw new Exception("Wrong vertices");
            }

            if (a == b)
            {
                return 0;
            }

            var result = double.PositiveInfinity;
            foreach (var i in _edges.Where(x => x.Tail == a && x.Head == b))
            {
                if (i.Weight < result)
                {
                    result = i.Weight;
                }
            }

            return result;
        }
        
        public GraphVertex VertexByName(string name)
        {
            return Vertices.FirstOrDefault(x => x.Name == name);
        }

        public (Dictionary<(GraphVertex, GraphVertex), double>, Dictionary<(GraphVertex, GraphVertex), GraphVertex>)
            Floyd()
        {
            var floydMatrix = new Dictionary<(GraphVertex, GraphVertex), double>();
            var routeMatrix = new Dictionary<(GraphVertex, GraphVertex), GraphVertex>();
            foreach (var i in Vertices)
            {
                foreach (var j in Vertices)
                {
                    floydMatrix.Add((i, j), ShortestEdgeWeight(i, j));
                    routeMatrix.Add((i, j), double.IsPositiveInfinity(floydMatrix[(i, j)]) ? null : j);
                }
            }

            foreach (var k in Vertices)
            {
                foreach (var i in Vertices)
                {
                    foreach (var j in Vertices)
                    {
                        if (floydMatrix[(i, k)] + floydMatrix[(k, j)] < floydMatrix[(i, j)])
                        {
                            floydMatrix[(i, j)] = floydMatrix[(i, k)] + floydMatrix[(k, j)];
                            routeMatrix[(i, j)] = k;
                        }
                    }
                }
            }

            return (floydMatrix, routeMatrix);
        }
    }
}