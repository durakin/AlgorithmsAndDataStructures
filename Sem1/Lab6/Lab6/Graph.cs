using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Lab6
{
    public class Graph
    {
        private readonly HashSet<GraphVertex> _vertices;
        private readonly SortedSet<GraphEdge> _edges;

        public Graph()
        {
            _vertices = new HashSet<GraphVertex>();
            _edges = new SortedSet<GraphEdge>(new EdgeComparer());
        }

        public int CountVertices()
        {
            return _vertices.Count;
        }

        public int CountEdges()
        {
            return _edges.Count;
        }

        private class EdgeComparer : IComparer<GraphEdge>
        {
            public int Compare(GraphEdge x, GraphEdge y)
            {
                return (string.Compare(x.Head.Name, y.Head.Name, StringComparison.Ordinal));
            }
        }

        public bool AddVertex(GraphVertex vertex)
        {
            if (_vertices.Any(x => x.Name == vertex.Name))
            {
                return false;
            }

            _vertices.Add(vertex);
            return true;
        }

        public bool AddEdge(GraphEdge edge)
        {
            if (!(_vertices.Contains(edge.Head) && _vertices.Contains(edge.Tail)))
            {
                return false;
            }

            _edges.Add(edge);
            return true;
        }

        public GraphVertex VertexByName(string name)
        {
            return _vertices.FirstOrDefault(x => x.Name == name);
        }
        
        public void InitDfs(GraphVertex current, Action<GraphVertex> body,
            Dictionary<GraphVertex, bool> compliance)
        {
            foreach (var i in _vertices)
            {
                compliance.Add(i, false);
            }

            Dfs(current, body, compliance);
        }

        private void Dfs(GraphVertex current, Action<GraphVertex> body,
            IDictionary<GraphVertex, bool> compliance)
        {
            compliance[current] = true;
            foreach (var i in _edges.Where(x => x.Tail == current && !compliance[x.Head]))
            {
                Dfs(i.Head, body, compliance);
            }

            body(current);
        }
    }
}