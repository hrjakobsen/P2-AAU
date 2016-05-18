using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class GraphTests //TODO: make a setup fixture
    {
        [Test()]
        public void GraphToString_Test()
        {
            short SampleInput1 = 5;
            short SampleInput2 = 6;
            byte messageInput1 = 11;
            byte modulo1 = 100;

            Vertex vertex1 = new Vertex(SampleInput1, SampleInput2, messageInput1, modulo1);

            short SampleInput3 = 7;
            short SampleInput4 = 8;
            byte messageInput2 = 100;
            byte modulo2 = 100;

            Vertex vertex2 = new Vertex(SampleInput3, SampleInput4, messageInput2, modulo2);

            Graph graph1 = new Graph();

            graph1.Vertices.Add(vertex1);
            graph1.Vertices.Add(vertex2);

            NUnit.Framework.Assert.AreEqual("These are my vertices: \n(5,6)\n(7,8)\n", graph1.ToString());
        }

        [Test()]
        public void GetSwitches_Test() {
            Vertex
                v1 = new Vertex(48, 50, 2, 4),
                v2 = new Vertex(49, 48, 2, 4),
                v3 = new Vertex(54, 53, 2, 4),
                v4 = new Vertex(50, 51, 3, 4),
                v5 = new Vertex(50, 49, 2, 4),
                v6 = new Vertex(65, 66, 2, 4),
                v7 = new Vertex(97, 98, 3, 4),
                v8 = new Vertex(96, 35, 2, 4),
                v9 = new Vertex(95, 26, 2, 4);

            Edge
                e1 = new Edge(v2, v3, 5, true, true),
                e2 = new Edge(v2, v5, 1, true, true),
                e3 = new Edge(v2, v6, 17, true, false),
                e4 = new Edge(v2, v3, 5, false, false),
                e5 = new Edge(v2, v5, 1, false, false),
                e7 = new Edge(v8, v9, 1, true, true),
                e6 = new Edge(v2, v6, 17, false, true),
                e8 = new Edge(v8, v9, 9, false, false);

            Graph g = new Graph() {
                Vertices = {v1, v2, v3, v4, v5, v6, v7, v8, v9},
                Edges = {e1, e2, e3, e4, e5, e6, e7, e8}
            };

            NUnit.Framework.Assert.AreEqual(new List<Edge> {e5,e7}, g.GetSwitches() );
        }
    }
}