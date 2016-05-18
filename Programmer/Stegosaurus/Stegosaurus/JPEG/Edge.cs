using System;

namespace Stegosaurus {
    public class Edge : IComparable<Edge> {
        public Vertex VStart { get; }
        public Vertex VEnd { get; }
        public short Weight { get; }
        public bool VStartFirst { get; }
        public bool VEndFirst { get; }

        /// <summary>
        /// Create edge between two vertices and describes what values from the vertices are switches
        /// </summary>
        /// <param name="vStart">Start vertex of the edge</param>
        /// <param name="vEnd">End vertex of the edge</param>
        /// <param name="weight">Weight of the edge (how expensive the switch is)</param>
        /// <param name="vStartFirstItem">True if the edge describes that the value switched in the first vertex is the first samplevalue, otherwise false</param>
        /// <param name="vEndFirstItem">True if the edge describes that the value switched in the second vertex is the first samplevalue, otherwise false</param>
        public Edge(Vertex vStart, Vertex vEnd, short weight, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            Weight = weight;
            VStartFirst = vStartFirstItem;
            VEndFirst = vEndFirstItem;
        }

        /// <summary>
        /// Used for sorting edges by weight
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Edge other) {
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }

        /// <summary>
        /// Tests if two edges are equal.
        /// </summary>
        /// <param name="obj">Other edge</param>
        /// <returns>True if they are equal, otherwise returns false</returns>
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