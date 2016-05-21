using System;

namespace Stegosaurus {
    public class Edge : IComparable<Edge> {
        public Vertex VStart { get; }
        public Vertex VEnd { get; }
        public int Weight { get; }
        public bool VStartFirst { get; }
        public bool VEndFirst { get; }
        
        public Edge(Vertex vStart, Vertex vEnd, int weight, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            Weight = weight;
            VStartFirst = vStartFirstItem;
            VEndFirst = vEndFirstItem;
        }

        public int CompareTo(Edge other) {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}