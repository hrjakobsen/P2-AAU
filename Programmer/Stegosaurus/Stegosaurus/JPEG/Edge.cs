
namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; set; }
        public Vertex VEnd { get; set; }

        public int Weight { get; set; }

        public Edge(Vertex vStart, Vertex vEnd, int weight) {
            VStart = vStart;
            VEnd = vEnd;
            Weight = weight;
        }
    }
}
