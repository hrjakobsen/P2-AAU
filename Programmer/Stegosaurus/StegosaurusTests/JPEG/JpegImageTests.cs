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
            int exceptions = 0;
            try
            {
                IImageEncoder ji = new JpegImage(null, 100, 4);
            }
            catch (ArgumentNullException)
            {
                exceptions++;
            }
            
            Assert.AreEqual(1, exceptions);
        }

        [Test()]
        public void JpegImageTest1()
        {
            Assert.Fail();
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