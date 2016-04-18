using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();

        public override string ToString() {
            return Vertices.Aggregate("These are my vertices: \n", (current, v) => current + (v + "\n"));
        }
    }
}
