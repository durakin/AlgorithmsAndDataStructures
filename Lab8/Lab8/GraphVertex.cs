namespace Lab8
{
    public class GraphVertex
    {
        public string Name { get; }

        public GraphVertex(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return "Vertex " + Name + ";";
        }
    }
}