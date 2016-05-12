using System;

namespace Stegosaurus {
    public class Edge : IComparable<Edge>{
        public Vertex VStart { get; }
        public Vertex VEnd { get; }
        public short Weight { get; }
        public bool VStartFirst { get; }
        public bool VEndFirst { get; }

        private static int id = 0;
        public int ID { get; }
        
        public Edge(Vertex vStart, Vertex vEnd, short weight, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            Weight = weight;
            VStartFirst = vStartFirstItem;
            VEndFirst = vEndFirstItem;
            ID = id++;
        }

        public int CompareTo(Edge other) {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}
