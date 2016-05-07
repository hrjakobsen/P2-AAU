using System;

namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; }
        public Vertex VEnd { get; }
        public short Weight { get; }
        public bool VStartFirst { get; }
        public bool VEndFirst { get; }
        
        public Edge(Vertex vStart, Vertex vEnd, short weight, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            Weight = weight;
            VStartFirst = vStartFirstItem;
            VEndFirst = vEndFirstItem;
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}
