using System;
using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public List<Edge> Edges { get; set; } = new List<Edge>(); 

        public override string ToString() {
            return Vertices.Aggregate("These are my vertices: \n", (current, v) => current + (v + "\n"));
        }

        public List<Edge> DoSwitches() {
            List<Edge> sortedEdges = Edges.OrderBy(x => x.Weight).ToList();
            List<Edge> chosenEdges = new List<Edge>();
            while (sortedEdges.Any()) {
                chosenEdges.Add(sortedEdges[0]);
                removeEdge(sortedEdges, sortedEdges[0]);
            }
            return chosenEdges;
        }
        
        private void removeEdge(List<Edge> list, Edge e) {
            list.RemoveAll(x => x.VStart == e.VStart || x.VEnd == e.VStart || x.VEnd == e.VEnd || x.VEnd == e.VStart);
        }
    }
}
