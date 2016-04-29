using System;
using System.Collections.Generic;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100, 4);
            //IImageEncoder ji = new JpegImage(new Bitmap(@"loladele.jpg"), 100, 4);

            ji.Encode(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 21, 54, 12, 56, 12, 12, 12, 12, 45, 76, 23, 54 });
            /*22 = 1011001*/
            // ji.Save(@"output.jpg");
            //  Console.ReadKey();
            //IImageDecoder jid = new Decoder("output.jpg");
            //jid.Decode();
            Console.ReadKey();
            
            
            //ji.Encode(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 21, 54, 12, 56, 12, 12, 12, 12, 45, 76, 23, 54});
            //ji.Save(@"output.jpg");

            debugGraph();
        }

        private static void debugGraph() {
                Graph g = new Graph();
                List<Vertex> samples = new List<Vertex> {
                    new Vertex(48, 50, 2),
                    new Vertex(49, 48, 2),
                    new Vertex(54, 53, 2),
                    new Vertex(50, 51, 3),
                    new Vertex(50, 49, 2),
                    new Vertex(65, 66, 2),
                    new Vertex(97, 98, 3),
                    new Vertex(96, 35, 2),
                    new Vertex(95, 26, 2)
                };

                foreach (Vertex vertex in samples) {
                    if ((vertex.SampleValue1 + vertex.SampleValue2).Mod(4) != vertex.Message) {
                        g.Vertices.Add(vertex);
                    }
                }

                foreach (Vertex currentVertex in g.Vertices) {
                    foreach (Vertex otherVertex in g.Vertices.Where(v => v != currentVertex)) {
                        if ((currentVertex.SampleValue2 + otherVertex.SampleValue1).Mod(4) == currentVertex.Message &&
                            (currentVertex.SampleValue1 + otherVertex.SampleValue2).Mod(4) == otherVertex.Message) {
                            Edge e = new Edge(currentVertex, otherVertex, true, true);
                            g.Edges.Add(e);
                        }
                        if ((currentVertex.SampleValue2 + otherVertex.SampleValue2).Mod(4) == currentVertex.Message &&
                            (currentVertex.SampleValue1 + otherVertex.SampleValue1).Mod(4) == otherVertex.Message) {
                            Edge e = new Edge(currentVertex, otherVertex, true, false);
                            g.Edges.Add(e);
                        }
                        if ((currentVertex.SampleValue1 + otherVertex.SampleValue2).Mod(4) == currentVertex.Message &&
                            (currentVertex.SampleValue2 + otherVertex.SampleValue1).Mod(4) == otherVertex.Message) {
                            Edge e = new Edge(currentVertex, otherVertex, false, false);
                            g.Edges.Add(e);
                        }
                        if ((currentVertex.SampleValue1 + otherVertex.SampleValue1).Mod(4) == currentVertex.Message &&
                            (currentVertex.SampleValue2 + otherVertex.SampleValue2).Mod(4) == otherVertex.Message) {
                            Edge e = new Edge(currentVertex, otherVertex, false, true);
                            g.Edges.Add(e);
                        }
                    }
                }

                Console.WriteLine("Nothing done yet!");
                int a = 0;
                foreach (Vertex sample in samples) {
                    a++;
                    Console.WriteLine($"Pair {a}: ({sample.SampleValue1};{sample.SampleValue2}) with message {sample.Message}. Does it fit? {(sample.SampleValue1 + sample.SampleValue2).Mod(4) == sample.Message}");
                }

                List<Edge> chosen = g.DoSwitches();
                foreach (Edge edge in chosen) {
                    swap(edge);
                }

                Console.WriteLine("\nSwapping done!");
                a = 0;
                foreach (Vertex sample in samples) {
                    a++;
                    Console.WriteLine($"Pair {a}: ({sample.SampleValue1};{sample.SampleValue2}) with message {sample.Message}. Does it fit? {(sample.SampleValue1 + sample.SampleValue2).Mod(4) == sample.Message}");
                }

                foreach (Vertex vertex in g.Vertices.Where(x => x.HasMessage)) {
                    if ((vertex.SampleValue1 + vertex.SampleValue2).Mod(4) != vertex.Message) {
                        _forceSampleChange(vertex);
                    }
                }

                Console.WriteLine("\nForcing done!");
                a = 0;
                foreach (Vertex sample in samples) {
                    a++;
                    Console.WriteLine($"Pair {a}: ({sample.SampleValue1};{sample.SampleValue2}) with message {sample.Message}. Does it fit? {(sample.SampleValue1 + sample.SampleValue2).Mod(4) == sample.Message}");
                }
            }


            private static void _forceSampleChange(Vertex vertex) {
                int M = 4;
                int error = (vertex.SampleValue1 + vertex.SampleValue2).Mod(M) - vertex.Message;

                if (vertex.SampleValue1 - error <= 127 && vertex.SampleValue1 - error >= -128) {
                    vertex.SampleValue1 -= error;
                } else if (vertex.SampleValue1 - error <= 127 && vertex.SampleValue1 - error >= -128) {
                    vertex.SampleValue2 -= error;
                } else {
                    vertex.SampleValue1 += 4 - error;
                }
            }

        private static void swap(Edge e) {
            int temp;
            if (e.vStartFirst) {
                if (e.vEndFirst) {
                    temp = e.VStart.SampleValue1;
                    e.VStart.SampleValue1 = e.VEnd.SampleValue1;
                    e.VEnd.SampleValue1 = temp;
                } else {
                    temp = e.VStart.SampleValue1;
                    e.VStart.SampleValue1 = e.VEnd.SampleValue2;
                    e.VEnd.SampleValue2 = temp;
                }
            } else {
                if (e.vEndFirst) {
                    temp = e.VStart.SampleValue2;
                    e.VStart.SampleValue2 = e.VEnd.SampleValue1;
                    e.VEnd.SampleValue1 = temp;
                } else {
                    temp = e.VStart.SampleValue2;
                    e.VStart.SampleValue2 = e.VEnd.SampleValue2;
                    e.VEnd.SampleValue2 = temp;
                }
            }
        }
    }
}