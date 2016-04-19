using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class JpegImageTests
    {
        [Test, ExpectedException()]
        public void JpegImageTest()
        {
            IImageEncoder ji = new JpegImage(null, 100, 4);

            Assert.Fail();
        }

        [Test()]
        public void JpegImageTest1()
        {
            Assert.Fail();
        }

        [Test()]
        public void JpegImageTest2()
        {
            Assert.Fail();
        }

        [Test()]
        public void JpegImageTest3()
        {
            Assert.Fail();
        }

        [Test()]
        public void EncodeTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void SaveTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void GetCapacityTest()
        {
            Assert.Fail();
        }
    }
}