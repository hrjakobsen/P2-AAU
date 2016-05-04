using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public List<Edge> Edges { get; set; } = new List<Edge>(); 

        public override string ToString() {
            return Vertices.Aggregate("These are my vertices: \n", (current, v) => current + (v + "\n"));
        }

        public List<Edge> GetSwitches() {
            List<Edge> sortedEdges = Edges.OrderBy(x => x.Weight).ToList();
            List<Edge> chosenEdges = new List<Edge>();
            while (sortedEdges.Any()) {
                chosenEdges.Add(sortedEdges[0]);
                _removeEdge(sortedEdges, sortedEdges[0]);
            }
            return chosenEdges;
        }
        
        public virtual void _removeEdge(List<Edge> list, Edge e) {
            list.RemoveAll(x => x.VStart == e.VStart || x.VEnd == e.VStart || x.VEnd == e.VEnd || x.VEnd == e.VStart);
        }
    }

    public class PGraph : Graph {
        public override void _removeEdge(List<Edge> list, Edge e) {
            int len = list.Count;

            Parallel.For(0, len, i => {
                if (list[i].VStart == e.VStart || list[i].VEnd == e.VStart || list[i].VEnd == e.VEnd || list[i].VEnd == e.VStart) {
                        list.RemoveAt(i);
                        len--;
                }
            });
        }
    }
}
