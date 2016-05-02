using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class QuantizationTableTests
    {
        [Test()]
        public void QuantizationTableTest()
        {
            int imageWidth = 200;
            int imageHeight = 100;

            Bitmap bm = new Bitmap(imageWidth, imageHeight);
            JpegImage ji = new JpegImage(bm, -100, 4);
        }

        [Test()]
        public void ScaleTest()
        {
            Assert.Fail();
        }

        [Test()]
        public void ToStringTest()
        {
            Assert.Fail();
        }
    }
}