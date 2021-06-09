using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab1
{
    public class Ant
    {
        #region Properties

        public Graph Graph { get; }
        public int Beta { get; }
        public double Q0 { get; }
        public double Distance { get; set; }
        public List<Vertex> VisitedVertices { get; set; }
        public List<Vertex> UnvisitedVertices { get; set; }
        public List<Edge> Path { get; set; }

        #endregion

        public Ant(Graph graph, int beta, double q0)
        {
            Graph = graph;
            Beta = beta;
            Q0 = q0;
            VisitedVertices = new List<Vertex>();
            UnvisitedVertices = new List<Vertex>();
            Path = new List<Edge>();
        }

        public void Init(string startVertexName)
        {
            Distance = 0;
            VisitedVertices.Add(Graph.Vertices.First(x => x.Name == startVertexName));
            UnvisitedVertices = Graph.Vertices.Where(x => x.Name != startVertexName).ToList();
            Path.Clear();
        }

        public Vertex CurrentVertex()
        {
            return VisitedVertices[VisitedVertices.Count - 1];
        }

        public bool CanMove()
        {
            return VisitedVertices.Count != Path.Count;
        }

        public Edge Move()
        {
            Vertex endPoint;
            var startPoint = CurrentVertex();

            if (UnvisitedVertices.Count == 0)
            {
                endPoint = VisitedVertices[0]; // if ant visited every node, just go back to start
            }
            else
            {
                endPoint = ChooseNextPoint();
                VisitedVertices.Add(endPoint);
                UnvisitedVertices.RemoveAt(UnvisitedVertices.FindIndex(x => x.Name == endPoint.Name));
            }

            var edge = Graph.GetEdge(startPoint.Name, endPoint.Name);
            Path.Add(edge);
            Distance += edge.Length;
            return edge;
        }

        private Vertex ChooseNextPoint()
        {
            var edgesWithWeight = new List<Edge>();
            Edge bestEdge = null;
            var currentVertexName = CurrentVertex().Name;

            foreach (var vertex in UnvisitedVertices)
            {
                var edge = Graph.GetEdge(currentVertexName, vertex.Name);
                edge.Weight = Weight(edge);
                if (bestEdge is null || edge.Weight > bestEdge.Weight)
                {
                    bestEdge = edge;
                }

                edgesWithWeight.Add(edge);
            }

            var random = RandomGenerator.Instance.Random.NextDouble();
            if (random < Q0)
            {
                return Exploitation(bestEdge);
            }
            else
            {
                return Exploration(edgesWithWeight);
            }
        }

        private double Weight(Edge edge)
        {
            double heuristic = 1 / edge.Length;
            return edge.Pheromone * Math.Pow(heuristic, Beta);
        }

        private static Vertex Exploitation(Edge bestEdge)
        {
            return bestEdge.End;
        }

        private static Vertex Exploration(List<Edge> edgesWithWeight)
        {
            var totalSum = edgesWithWeight.Sum(x => x.Weight);
            var edgeProbabilities = edgesWithWeight.Select(w =>
            {
                w.Weight /= totalSum;
                return w;
            }).ToList();
            var cumulativeSum = Helper.EdgeCumulativeSum(edgeProbabilities);
            var chosenPoint = Helper.GetRandomVertex(cumulativeSum);

            return chosenPoint;
        }
    }
}