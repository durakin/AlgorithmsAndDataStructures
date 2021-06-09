namespace Lab6
{
    public class Edge
    {
        public Vertex Tail { get; }
        public Vertex Head { get; }
        public Edge(Vertex tail, Vertex head)
        {
            Tail = tail;
            Head = head;
        }
    }
}
