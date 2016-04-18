using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; set; }

        public Vertex GetVertexByID(int id) {
            return Vertices.Single(v => v.ID == id);
        }

        public Vertex AddVertex(Vertex vertex) {
            Vertices.Add(vertex);
            return vertex;
        }
    }
}
