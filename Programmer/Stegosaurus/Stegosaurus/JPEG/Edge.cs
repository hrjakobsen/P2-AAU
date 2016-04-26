using System;

namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; set; }
        public Vertex VEnd { get; set; }

        public int Weight => Math.Abs((vStartFirst ? VStart.SampleValue1 : VStart.SampleValue2) -
                                      (vEndFirst ? VEnd.SampleValue1 : VEnd.SampleValue2));

        public readonly bool vStartFirst;
        public readonly bool vEndFirst;

        public Edge(Vertex vStart, Vertex vEnd, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            vStartFirst = vStartFirstItem;
            vEndFirst = vEndFirstItem;
        }

        public override bool Equals(object other) {
            return other is Edge && (VStart == ((Edge)other).VStart && VEnd == ((Edge)other).VEnd) || (VStart == ((Edge)other).VEnd && VStart == ((Edge)other).VEnd);
        }

        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}
