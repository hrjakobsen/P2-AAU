using System;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100, 4);
            IImageDecoder jid = new Decoder();
            jid.Decode(@"output.jpg");

            //ji.Encode(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22,23,24,25 });
            //ji.Save(@"output.jpg");
            //Console.ReadKey();

            //Graph g = new Graph();
            //Vertex v1 = new Vertex(new Tuple<int, int, byte>(48, 50, 2));
            //Vertex v2 = new Vertex(new Tuple<int, int, byte>(49, 48, 2));
            //Vertex v3 = new Vertex(new Tuple<int, int, byte>(54, 53, 2));
            //Vertex v4 = new Vertex(new Tuple<int, int, byte>(50, 51, 3));
            //Vertex v5 = new Vertex(new Tuple<int, int, byte>(50, 49, 2));
            //Vertex v6 = new Vertex(new Tuple<int, int, byte>(65, 66, 2));
            //Vertex v7 = new Vertex(new Tuple<int, int, byte>(97, 98, 3));
            //Vertex v8 = new Vertex(new Tuple<int, int, byte>(96, 35, 2));
            //Vertex v9 = new Vertex(new Tuple<int, int, byte>(95, 26, 2));

            //g.Vertices.Add(v2);
            //g.Vertices.Add(v3);
            //g.Vertices.Add(v4);
            //g.Vertices.Add(v5);
            //g.Vertices.Add(v6);
            //g.Vertices.Add(v8);
            //g.Vertices.Add(v9);


            //foreach (Vertex currentVertex in g.Vertices) {
            //    foreach (Vertex otherVertex in g.Vertices.Where(v => v != currentVertex)) {
            //        if ((currentVertex.Value.Item2 + otherVertex.Value.Item1).Mod(4) == currentVertex.Value.Item3 &&
            //            (currentVertex.Value.Item1 + otherVertex.Value.Item2).Mod(4) == otherVertex.Value.Item3) {
            //            Edge e = new Edge(currentVertex, otherVertex, true, true);
            //            currentVertex.Neighbours.Add(e);
            //            otherVertex.Neighbours.Add(e);
            //        }
            //        if ((currentVertex.Value.Item2 + otherVertex.Value.Item2).Mod(4) == currentVertex.Value.Item3 &&
            //            (currentVertex.Value.Item1 + otherVertex.Value.Item1).Mod(4) == otherVertex.Value.Item3) {
            //            Edge e = new Edge(currentVertex, otherVertex, true, false);
            //            currentVertex.Neighbours.Add(e);
            //            otherVertex.Neighbours.Add(e);
            //        }
            //        if ((currentVertex.Value.Item1 + otherVertex.Value.Item2).Mod(4) == currentVertex.Value.Item3 &&
            //            (currentVertex.Value.Item2 + otherVertex.Value.Item1).Mod(4) == otherVertex.Value.Item3) {
            //            Edge e = new Edge(currentVertex, otherVertex, false, false);
            //            currentVertex.Neighbours.Add(e);
            //            otherVertex.Neighbours.Add(e);
            //        }
            //        if ((currentVertex.Value.Item1 + otherVertex.Value.Item1).Mod(4) == currentVertex.Value.Item3 &&
            //            (currentVertex.Value.Item2 + otherVertex.Value.Item2).Mod(4) == otherVertex.Value.Item3) {
            //            Edge e = new Edge(currentVertex, otherVertex, false, true);
            //            currentVertex.Neighbours.Add(e);
            //            otherVertex.Neighbours.Add(e);
            //        }
            //    }
            //}

            //g.DoSwitches();

            //foreach (var vertex in g.Vertices) {
            //    foreach (Edge neighbour in vertex.Neighbours) {
            //        Console.WriteLine($"{neighbour.VStart} ({neighbour.VStart.Value.Item3}), {neighbour.VEnd}({neighbour.VEnd.Value.Item3}), - {neighbour.Weight}");
            //    }
            //}

            Console.ReadKey();
        }
    }
}
