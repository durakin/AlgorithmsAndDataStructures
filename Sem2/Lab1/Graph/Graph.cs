using System;
using System.Collections.Generic;

namespace Lab1
{
    public class Graph
    {
        public List<Vertex> Vertices { get; }
        private List<Edge> Edges { get; }
        public double MinimumPheromone { get; set; }

        public Graph(int[,] lengthMatrix)
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
            for (var i = lengthMatrix.GetLowerBound(0);
                i <= lengthMatrix.GetUpperBound(0);
                i++)
            {
                Vertices.Add(new Vertex(i.ToString()));
            }
            CreateEdges(lengthMatrix);
        }
        
        private void CreateEdges(int[,] weightMatrix)
        {
            for (var i = 0; i < Vertices.Count; i++)
            {
                for (var j = 0; j < Vertices.Count; j++)
                {
                    if (i == j) continue;
                    Edges.Add(new Edge(Vertices[i], Vertices[j], weightMatrix[i, j]));
                }
            }
        }

        public Edge GetEdge(string firstVertexName, string secondVertexName)
        {
            return Edges.Find(x => x.Start.Name == firstVertexName && x.End.Name == secondVertexName);
        }
        
        public void ResetPheromone(double pheromoneValue)
        {
            foreach (var edge in Edges)
            {
                edge.Pheromone = pheromoneValue;
            }
        }

        public void EvaporatePheromone(Edge edge, double value)
        {
            edge.Pheromone = Math.Max(MinimumPheromone, edge.Pheromone * value);
            var secondEdge = GetEdge(edge.End.Name, edge.Start.Name);
            secondEdge.Pheromone = Math.Max(MinimumPheromone, secondEdge.Pheromone * value);
        }

        public void DepositPheromone(Edge edge, double value)
        {
            edge.Pheromone += value;
            var secondEdge = GetEdge(edge.End.Name, edge.Start.Name);
            secondEdge.Pheromone += value;
        }
    }
}