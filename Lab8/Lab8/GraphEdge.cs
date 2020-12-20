namespace Lab8
{
    public class GraphEdge
    {
        public GraphVertex Tail { get; }

        public GraphVertex Head { get; }

        public double Weight { get; set; }

        public GraphEdge(GraphVertex tail, GraphVertex head, double weight)
        {
            Tail = tail;
            Head = head;
            Weight = weight;
        }
        
        public override string ToString()
        {
            return $"{{{Tail.Name}, {Head.Name}}}, ({Weight});";
        }
    }
}