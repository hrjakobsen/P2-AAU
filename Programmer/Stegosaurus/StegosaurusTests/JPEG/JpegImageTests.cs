using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class JpegImageTests
    {
        #region JpegImage
        [Test]
        public void JpegImageTest() //Test if JpegImage constructor throws exception when cover image is null
        {
            try
            {
                IImageEncoder ji = new JpegImage(null, 100, 4);
            }
            catch (ArgumentNullException)
            {
                return;
            }
            
            Assert.Fail();
        }

        [Test()]
        public void JpegImageTest1() //Test if JpegImage constructor does not throw exception when cover image is not null
        {
            Bitmap testcover = new Bitmap(200, 100);
            try
            {
                IImageEncoder ji = new JpegImage(testcover, 100, 4);
            }
            catch (ArgumentNullException)
            {
                Assert.Fail();
            }

            Assert.Pass();
        }

        [Test()]
        public void JpegImageTest2()
        {
            Bitmap testcover = new Bitmap(200, 100);
            try
            {
                JpegImage testMod = new JpegImage(testcover, 100, 0);
            }
            catch (ArgumentException)
            {
                Assert.Pass();
            }

            Assert.Fail();
        }

        [Test()]
        public void JpegImageTest3()
        {
            Bitmap testcover = new Bitmap(200, 100);
            try
            {
                JpegImage testMod = new JpegImage(testcover, 100, 2);
            }
            catch (ArgumentException)
            {
                Assert.Fail();
            }

            Assert.Pass();
        }
        #endregion

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