using System;
using System.Collections.Generic;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100, 4);
            IImageDecoder jid = new Decoder("output.jpg");
            Console.WindowWidth = Console.LargestWindowWidth - 10;
            Console.WindowHeight = Console.LargestWindowHeight - 20;
            jid.Decode();
            Console.ReadKey();
//            ji.Encode(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
//            ji.Save(@"output.jpg");

//#if DEBUG
//            Graph g = new Graph();
//            Vertex v1 = new Vertex(48, 50, 2);
//            Vertex v2 = new Vertex(49, 48, 2);
//            Vertex v3 = new Vertex(54, 53, 2);
//            Vertex v4 = new Vertex(50, 51, 3);
//            Vertex v5 = new Vertex(50, 49, 2);
//            Vertex v6 = new Vertex(65, 66, 2);
//            Vertex v7 = new Vertex(97, 98, 3);
//            Vertex v8 = new Vertex(96, 35, 2);
//            Vertex v9 = new Vertex(95, 26, 2);

//            g.Vertices.Add(v2);
//            g.Vertices.Add(v3);
//            g.Vertices.Add(v4);
//            g.Vertices.Add(v5);
//            g.Vertices.Add(v6);
//            g.Vertices.Add(v8);
//            g.Vertices.Add(v9);

//            foreach (Vertex currentVertex in g.Vertices) {
//                foreach (Vertex otherVertex in g.Vertices.Where(v => v != currentVertex)) {
//                    if ((currentVertex.SampleValue2 + otherVertex.SampleValue1).Mod(4) == currentVertex.Message &&
//                        (currentVertex.SampleValue1 + otherVertex.SampleValue2).Mod(4) == otherVertex.Message) {
//                        Edge e = new Edge(currentVertex, otherVertex, true, true);
//                        g.Edges.Add(e);
//                    }
//                    if ((currentVertex.SampleValue2 + otherVertex.SampleValue2).Mod(4) == currentVertex.Message &&
//                        (currentVertex.SampleValue1 + otherVertex.SampleValue1).Mod(4) == otherVertex.Message) {
//                        Edge e = new Edge(currentVertex, otherVertex, true, false);
//                        g.Edges.Add(e);
//                    }
//                    if ((currentVertex.SampleValue1 + otherVertex.SampleValue2).Mod(4) == currentVertex.Message &&
//                        (currentVertex.SampleValue2 + otherVertex.SampleValue1).Mod(4) == otherVertex.Message) {
//                        Edge e = new Edge(currentVertex, otherVertex, false, false);
//                        g.Edges.Add(e);
//                    }
//                    if ((currentVertex.SampleValue1 + otherVertex.SampleValue1).Mod(4) == currentVertex.Message &&
//                        (currentVertex.SampleValue2 + otherVertex.SampleValue2).Mod(4) == otherVertex.Message) {
//                        Edge e = new Edge(currentVertex, otherVertex, false, true);
//                        g.Edges.Add(e);
//                    }
//                }
//            }




//            List<Edge> chosen = g.DoSwitches();
//            foreach (Edge edge in chosen) {
//                swap(edge);
//            }

//            foreach (Vertex vertex in g.Vertices) {
//                Console.WriteLine((vertex.SampleValue1 + vertex.SampleValue2).Mod(4) == vertex.Message);
//            }

//            Console.ReadKey();
//        }
//        private static void swap(Edge e) {

//            int temp;
//            if (e.vStartFirst) {
//                if (e.vEndFirst) {
//                    temp = e.VStart.SampleValue1;
//                    e.VStart.SampleValue1 = e.VEnd.SampleValue1;
//                    e.VEnd.SampleValue1 = temp;
//                } else {
//                    temp = e.VStart.SampleValue1;
//                    e.VStart.SampleValue1 = e.VEnd.SampleValue2;
//                    e.VEnd.SampleValue2 = temp;
//                }
//            } else {
//                if (e.vEndFirst) {
//                    temp = e.VStart.SampleValue2;
//                    e.VStart.SampleValue2 = e.VEnd.SampleValue1;
//                    e.VEnd.SampleValue1 = temp;
//                } else {
//                    temp = e.VStart.SampleValue2;
//                    e.VStart.SampleValue2 = e.VEnd.SampleValue2;
//                    e.VEnd.SampleValue2 = temp;
//                }
//            }
//#endif
        }
    }
}

