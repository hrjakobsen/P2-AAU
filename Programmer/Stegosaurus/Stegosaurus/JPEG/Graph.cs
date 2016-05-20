using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; } = new List<Vertex>();
        public List<Edge> Edges { get; } = new List<Edge>();

        public override string ToString() {
            return Vertices.Aggregate("These are my vertices: \n", (current, v) => current + (v + "\n"));
        }

        /// <summary>
        /// Uses a greedy algorithm to choose what edges to keep. Always chooses the one with the lowest weight, then remove all other edges touching those vertices. Keeps doing this until no more edges can be chosen.
        /// </summary>
        /// <returns>A list of edges that describes switches that will make the message fit into the image</returns>
        public List<Edge> GetSwitches() {
            Edges.Sort();
            List<Edge> chosenEdges = new List<Edge>();
            
            while (Edges.Any()) {
                chosenEdges.Add(Edges[0]);
                _removeEdge(Edges, Edges[0]);
            }
            return chosenEdges;
        }

        private static void _removeEdge(List<Edge> list, Edge e) {
            list.RemoveAll(x => x.VStart == e.VStart || x.VStart == e.VEnd || x.VEnd == e.VStart || x.VEnd == e.VEnd);
        }
    }
}