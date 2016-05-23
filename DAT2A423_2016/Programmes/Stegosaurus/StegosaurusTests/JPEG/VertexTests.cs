using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class VertexTests
    {
        [Test()]
        public void Vertex_ToStringTest()
        {
            short SampleInput1 = 5;
            short SampleInput2 = 6;
            byte messageInput = 11;
            byte modulo = 4;

            Vertex vertex1 = new Vertex(SampleInput1, SampleInput2, messageInput, modulo);

            Assert.AreEqual("(5,6)", vertex1.ToString());
        }
    }
}