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
    public class EdgeTests
    {
        [Test()]
        public void Edge_ToStringTest()
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

            NUnit.Framework.Assert.AreEqual("((5,6) <-> (7,8))", edge1.ToString());
        }
    }
}