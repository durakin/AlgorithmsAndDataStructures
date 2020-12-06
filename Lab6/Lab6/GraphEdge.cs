namespace Lab6
{
    public class GraphEdge
    {
        public GraphVertex Tail { get; }

        public GraphVertex Head { get; }
        
        public int Weight { get; set; }

        public GraphEdge(GraphVertex tail, GraphVertex head)
        {
            Tail = tail;
            Head = head;
            Weight = 0;
        }

        public GraphEdge(GraphVertex tail, GraphVertex head, int weight)
        {
            Tail = tail;
            Head = head;
            Weight = weight;

        }

    }
}