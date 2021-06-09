namespace Lab1
{
    public class Edge
    {
        public Vertex Start { get; }
        public Vertex End { get; }
        public double Weight { get; set; }
        public double Length { get; }
        public double Pheromone { get; set; }

        public Edge(Vertex start, Vertex end, int length)
        {
            Start = start;
            End = end;
            Length = length;
        }
    }
}
