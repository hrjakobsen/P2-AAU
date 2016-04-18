using System.Collections.Generic;

namespace Stegosaurus {
    public class Vertex {
        public int ID { get; }
        public List<Edge> Neighbours { get; set; }

        public Vertex(int id) {
            ID = id;
        }

        public void AddNeighbour(Edge neighbour) {
            Neighbours.Add(neighbour);
        }
    }
}