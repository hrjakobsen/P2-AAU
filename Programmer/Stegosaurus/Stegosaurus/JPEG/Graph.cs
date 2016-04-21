using System;
using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class Graph {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();

        public override string ToString() {
            return Vertices.Aggregate("These are my vertices: \n", (current, v) => current + (v + "\n"));
        }

        public void DoSwitches() {
            foreach (Vertex vertex in Vertices) {
                Edge chosen;
                try {
                    chosen = vertex.Neighbours.OrderBy(x => x.Weight).First();
                    /*BUG doesnt remove the ones pointing on the vertex*/
                    if (chosen.VStart == vertex) {
                        chosen.VStart.Neighbours.RemoveAll(x => x != chosen);
                        chosen.VEnd.Neighbours.Clear();
                    }
                    else {
                        chosen.VEnd.Neighbours.RemoveAll(x => x != chosen);
                        chosen.VStart.Neighbours.Clear();
                    }
                }
                catch (InvalidOperationException) {
                    continue;
                }

            }
            Console.WriteLine(Vertices.Sum(vertex1 => vertex1.Neighbours.Count));
            
        } 
    }
}
