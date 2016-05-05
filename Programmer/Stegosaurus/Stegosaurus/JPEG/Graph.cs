using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Diagnostics;
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
            List<Edge> sortedEdges = new List<Edge>(Edges.OrderBy(x => x.Weight).ToList());
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
            BlockingCollection<Edge> tempList = new BlockingCollection<Edge>(new ConcurrentBag<Edge>(list));

            Parallel.ForEach(tempList.GetConsumingEnumerable(), x => {
                if (x.VStart == e.VStart || x.VEnd == e.VStart || x.VEnd == e.VEnd || x.VEnd == e.VStart) {
                    tempList.Take();
                }
            });

            list = tempList.ToList();
        }
    }
}
