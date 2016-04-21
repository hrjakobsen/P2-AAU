using System;
using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class Vertex {
        public Tuple<int, int, byte> Value { get; }
        public List<Edge> Neighbours { get; set; } = new List<Edge>();

        public Vertex(Tuple<int, int, byte> value) {
            Value = value;
        }

        public override string ToString() {
            return $"({Value.Item1},{Value.Item2})";
        }
    }
}