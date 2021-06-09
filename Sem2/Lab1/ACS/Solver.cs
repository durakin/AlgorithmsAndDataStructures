using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab1
{
    public class Solver
    {
        private Parameters Parameters { get; set; }
        private Ant GlobalBestAnt { get; set; }
        private List<double> Results { get; set; }
        private Graph Graph { get; set; }
        private Stopwatch Stopwatch { get; set; }

        public Solver(Parameters parameters, Graph graph)
        {
            Parameters = parameters;
            graph.MinimumPheromone = parameters.T0;
            Graph = graph;
            Results = new List<double>();
            Stopwatch = new Stopwatch();
        }

        /// <summary>
        /// Main loop of ACS algorithm
        /// </summary>
        public List<double> RunAcs()
        {
            Stopwatch.Start();
            Graph.ResetPheromone(Parameters.T0);
            for (var i = 0; i < Parameters.Iterations; i++)
            {
                var antColony = CreateAnts();
                GlobalBestAnt ??= antColony[0];

                var localBestAnt = BuildTours(antColony);
                if (Math.Round(localBestAnt.Distance, 2) < Math.Round(GlobalBestAnt.Distance, 2))
                {
                    GlobalBestAnt = localBestAnt;
                    Console.WriteLine(
                        "Current Global Best: " + GlobalBestAnt.Distance + " found in " + i + " iteration");
                }
                Results.Add(localBestAnt.Distance);
            }
            Stopwatch.Stop();
            return Results;
        }

        /// <summary>
        /// Create ants and place every ant in random point on graph (warning AntCount < Dimensions)
        /// </summary>
        private List<Ant> CreateAnts()
        {
            var antColony = new List<Ant>();
            var randomPoints = RandomGenerator.GenerateRandom(Parameters.AntCount, 0, Graph.Vertices.Count - 1);
            foreach (var random in randomPoints)
            {
                var ant = new Ant(Graph, Parameters.Beta, Parameters.Q0);
                ant.Init(random.ToString());
                antColony.Add(ant);
            }

            return antColony;
        }

        /// <summary>
        /// This method builds solution for every ant in AntColony and return the best ant (with shortest distance tour)
        /// </summary>
        private Ant BuildTours(List<Ant> antColony)
        {
            foreach (var ant in antColony)
            {
                Edge edge = ant.Move();
                LocalUpdate(edge);
            }


            GlobalUpdate();

            return antColony.OrderBy(x => x.Distance).FirstOrDefault(); // find shortest ant tour (path)
        }

        /// <summary>
        /// Update pheromone level on edge passed in parameter
        /// </summary>
        private void LocalUpdate(Edge edge)
        {
            var evaporate = 1 - Parameters.LocalEvaporationRate;
            Graph.EvaporatePheromone(edge, evaporate);

            var deposit = Parameters.LocalEvaporationRate * Parameters.T0;
            Graph.DepositPheromone(edge, deposit);
        }

        /// <summary>
        /// Update pheromone level on path for best ant
        /// </summary>
        private void GlobalUpdate()
        {
            var deltaR = 1 / GlobalBestAnt.Distance;
            foreach (var edge in GlobalBestAnt.Path)
            {
                var evaporate = (1 - Parameters.GlobalEvaporationRate);
                Graph.EvaporatePheromone(edge, evaporate);

                var deposit = Parameters.GlobalEvaporationRate * deltaR;
                Graph.DepositPheromone(edge, deposit);
            }
        }

        public TimeSpan GetExecutionTime()
        {
            return Stopwatch.Elapsed;
        }
    }
}