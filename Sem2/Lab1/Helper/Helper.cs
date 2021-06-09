using System.Collections.Generic;
using System.Linq;
using Lab1;

namespace Lab1
{
    public static class Helper
    {
        public static IEnumerable<Edge> EdgeCumulativeSum(IEnumerable<Edge> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item.Weight;
                item.Weight = sum;
            }

            return sequence;
        }

        public static Vertex GetRandomVertex(IEnumerable<Edge> cumulativeSum)
        {
            var random = RandomGenerator.Instance.Random.NextDouble();
            return cumulativeSum.First(j => j.Weight >= random).End;
        }



    }
}
