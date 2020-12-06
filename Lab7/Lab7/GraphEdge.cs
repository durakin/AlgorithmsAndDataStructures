namespace Lab7
{
    public class GraphEdge
    {
        public GraphVertex Tail { get; }

        public GraphVertex Head { get; }

        public double Weight { get; set; }

        public GraphEdge(GraphVertex tail, GraphVertex head)
        {
            Tail = tail;
            Head = head;
            Weight = 0;
        }

        public GraphEdge(GraphVertex tail, GraphVertex head, double weight)
        {
            Tail = tail;
            Head = head;
            Weight = weight;
        }
    }
}