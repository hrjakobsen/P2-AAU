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
        public void ToStringTest()
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
        public void GetSwitchesTest() //TODO: Get someone to validate the test...
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

            short weightinput = 3;
            bool vStartFirstInput = true;
            bool vEndFirstInput = false;

            Edge edge1 = new Edge(vertex1, vertex2, weightinput, vStartFirstInput, vEndFirstInput);
            //Edge edge2 = new Edge(vertex1, vertex2, weightinput, vStartFirstInput, vEndFirstInput);

            Graph graph1 = new Graph();
            graph1.Vertices.Add(vertex1);
            graph1.Vertices.Add(vertex2);
            graph1.Edges.Add(edge1);

            List<Edge> outputCheckEdges = new List<Edge>();
            outputCheckEdges.Add(edge1);

            NUnit.Framework.Assert.AreEqual(outputCheckEdges, graph1.GetSwitches());
        }
    }
}