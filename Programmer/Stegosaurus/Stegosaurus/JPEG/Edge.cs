using System;

namespace Stegosaurus {
    public class Edge {
        public Vertex VStart { get; set; }
        public Vertex VEnd { get; set; }

        public int Weight => Math.Abs((_vStartFirst ? VStart.Value.Item1 : VStart.Value.Item2) -
                                      (_vEndFirst ? VEnd.Value.Item1 : VEnd.Value.Item2));

        public readonly bool _vStartFirst;
        public readonly bool _vEndFirst;

        public Edge(Vertex vStart, Vertex vEnd, bool vStartFirstItem, bool vEndFirstItem) {
            VStart = vStart;
            VEnd = vEnd;
            _vStartFirst = vStartFirstItem;
            _vEndFirst = vEndFirstItem;
        }
    }
}
