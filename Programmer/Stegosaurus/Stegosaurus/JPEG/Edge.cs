using System;

namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; }
        public Vertex VEnd { get; }

        public int Weight { get; }

        public readonly bool vStartFirst;
        public readonly bool vEndFirst;

        public Edge(Vertex vStart, Vertex vEnd, int weight, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            vStartFirst = vStartFirstItem;
            vEndFirst = vEndFirstItem;
            Weight = weight;
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}
