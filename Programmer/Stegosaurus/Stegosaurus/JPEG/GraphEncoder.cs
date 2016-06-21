using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stegosaurus {
    public class GraphEncoder {
        private int[] _sampleValues;
        private ushort _mvalue;

        public GraphEncoder(int[] sampleValues, ushort mvalue) {
            _sampleValues = sampleValues;
            _mvalue = mvalue;
        }

        public int[] Encode(byte[] message) {
            Graph g = Graph.GraphFromSampleValuesAndMessage(_sampleValues, message, _mvalue);
            g.FindSwitches();
            g.PickEdges();
            g.DoSwitches();
            g.ForceChanges();

            int i = 0;
            foreach (Vertex vertex in g.Vertices) {
                _sampleValues[i++] = vertex.SampleValue1;
                _sampleValues[i++] = vertex.SampleValue2;
            }
            return _sampleValues;
        }

        private class Graph {
            public List<Vertex> Vertices { get; set; } = new List<Vertex>();
            private List<Edge> _allEdges = new List<Edge>(); 

            public static Graph GraphFromSampleValuesAndMessage(int[] SampleValues, byte[] Message, int modulo) {
                Graph g = new Graph();
                int currentSampleValue = 0;

                for (int i = 0; i < 8; i++) {
                    Vertex v = new Vertex(SampleValues[currentSampleValue++], SampleValues[currentSampleValue++], Message[i], 4);
                    g.Vertices.Add(v);
                }

                for (int i = 8; i < Message.Length; i++) {
                    Vertex v = new Vertex(SampleValues[currentSampleValue++], SampleValues[currentSampleValue++], Message[i], modulo);
                    g.Vertices.Add(v);
                }

                return g;
            }

            public void FindSwitches() {

                List<Vertex> toBeChanged = Vertices.Where(v => (v.SampleValue1 + v.SampleValue2).Mod(v.Modulo) != v.Message).ToList();
                int length = toBeChanged.Count;
                Parallel.For(0, length, i => {
                    for (int j = i + 1; j < length; j++) {
                        _addEdge(true, true, toBeChanged[i], toBeChanged[j], 4);
                        _addEdge(true, false, toBeChanged[i], toBeChanged[j], 4);
                        _addEdge(false, true, toBeChanged[i], toBeChanged[j], 4);
                        _addEdge(false, false, toBeChanged[i], toBeChanged[j], 4);
                    }
                });
            }

            public void PickEdges() {
                _allEdges = _allEdges.OrderBy(x => x.Weight).ToList();
                foreach (Edge currentEdge in _allEdges) {
                    Vertex startVertex = currentEdge.VStart;
                    Vertex endVertex = currentEdge.VEnd;

                    if (!startVertex.Neighbours.Contains(currentEdge)) continue;

                    startVertex.Choose(currentEdge);
                    endVertex.Choose(currentEdge);
                }
            }

            private void _addEdge(bool startFirst, bool endFirst, Vertex first, Vertex second, int threshold) {

                short weight = (short)Math.Abs((startFirst ? first.SampleValue1 : first.SampleValue2) - (endFirst ? second.SampleValue1 : second.SampleValue2));
                if (weight < threshold) {
                    if (((startFirst ? first.SampleValue2 : first.SampleValue1) + (endFirst ? second.SampleValue1 : second.SampleValue2)).Mod(first.Modulo) == first.Message) {
                        if (((startFirst ? first.SampleValue1 : first.SampleValue2) + (endFirst ? second.SampleValue2 : second.SampleValue1)).Mod(second.Modulo) == second.Message) {
                            lock (this) {
                                Edge e = new Edge(first, second, weight, startFirst, endFirst);
                                first.Neighbours.Add(e);
                                second.Neighbours.Add(e);
                                _allEdges.Add(e);
                            }
                        }
                    }
                }
            }

            public void DoSwitches() {
                foreach (Vertex vertex in Vertices) {
                    if (vertex.Neighbours.Any()) {
                        _swapVertexData(vertex.Neighbours[0]);
                    }
                }
            }

            public void ForceChanges() {
                List<Vertex> toBeChanged = Vertices.Where(v => (v.SampleValue1 + v.SampleValue2).Mod(v.Modulo) != v.Message).ToList();
                foreach (Vertex vertex in toBeChanged) {
                    _forceSampleChange(vertex);
                }
            }

            private static void _swapVertexData(Edge e) {
                int temp;
                if (e.VStartFirst) {
                    if (e.VEndFirst) {
                        temp = e.VStart.SampleValue1;
                        e.VStart.SampleValue1 = e.VEnd.SampleValue1;
                        e.VEnd.SampleValue1 = temp;
                    } else {
                        temp = e.VStart.SampleValue1;
                        e.VStart.SampleValue1 = e.VEnd.SampleValue2;
                        e.VEnd.SampleValue2 = temp;
                    }
                } else {
                    if (e.VEndFirst) {
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

            private static void _forceSampleChange(Vertex vertex) {
                short error = (short)((vertex.SampleValue1 + vertex.SampleValue2).Mod(vertex.Modulo) - vertex.Message);

                if (vertex.SampleValue1 - error <= 127 && vertex.SampleValue1 - error >= -128 &&
                    vertex.SampleValue1 - error != 0) {
                    vertex.SampleValue1 -= error;
                } else if (vertex.SampleValue2 - error <= 127 && vertex.SampleValue2 - error >= -128 &&
                           vertex.SampleValue2 - error != 0) {
                    vertex.SampleValue2 -= error;
                } else {
                    vertex.SampleValue1 += (short)(vertex.Modulo - error);
                }
            }
        }

        private class Vertex {
            public int SampleValue1 { get; set; }
            public int SampleValue2 { get; set; }
            public int Modulo { get; set; }
            public int Message { get; set; }
            public List<Edge> Neighbours { get; set; } = new List<Edge>();

            public Vertex(int sampleValue1, int sampleValue2, int message, int modulo) {
                SampleValue1 = sampleValue1;
                SampleValue2 = sampleValue2;
                Modulo = modulo;
                Message = message;
            }

            public void Choose(Edge e) {
                Vertex otherVertex = e.VStart == this ? e.VEnd : e.VStart;
                otherVertex.Neighbours.Clear();
                foreach (Edge edge in Neighbours) {
                    Vertex other = edge.VStart == this ? edge.VEnd : edge.VStart;
                    other.Neighbours.RemoveAll(x => x.VStart == this || x.VEnd == this);
                }
                Neighbours.Clear();
                Neighbours.Add(e);
            }
        }

        private class Edge {
            public Vertex VStart { get; }
            public Vertex VEnd { get; }
            public short Weight { get; }
            public bool VStartFirst { get; }
            public bool VEndFirst { get; }

            public Edge(Vertex vStart, Vertex vEnd, short weight, bool vStartFirst, bool vEndFirst) {
                VStart = vStart;
                VEnd = vEnd;
                Weight = weight;
                VStartFirst = vStartFirst;
                VEndFirst = vEndFirst;
            }
        }
    }
}
