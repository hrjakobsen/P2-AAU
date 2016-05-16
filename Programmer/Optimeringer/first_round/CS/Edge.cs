using System;

namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; }
        public Vertex VEnd { get; }
        public int Weight => Math.Abs((VStartFirst ? VStart.SampleValue1 : VStart.SampleValue2) - (VEndFirst ? VEnd.SampleValue1 : VEnd.SampleValue2));

        public readonly bool VStartFirst;
        public readonly bool VEndFirst;

        public Edge(Vertex vStart, Vertex vEnd, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            VStartFirst = vStartFirstItem;
            VEndFirst = vEndFirstItem;
        }
        
        public override string ToString() {
            return $"({VStart} <-> {VEnd})";
        }
    }
}
