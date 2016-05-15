using System;

namespace Stegosaurus {
    public class Edge : IComparable<Edge> {
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

        public int CompareTo(Edge other) {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            Edge other = (Edge) obj;

            return VStart == other.VStart && VEnd == other.VEnd && VStartFirst == other.VStartFirst &&
                   VEndFirst == other.VEndFirst;
        }

        public override int GetHashCode() {
            return Weight.GetHashCode();
        }
    }
}