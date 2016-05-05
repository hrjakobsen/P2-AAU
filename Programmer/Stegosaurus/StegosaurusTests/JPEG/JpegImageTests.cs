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
        [Test]
        public void JpegImage_Test_if_constructor_throws_exception_when_image_is_null() //Test if JpegImage constructor throws exception when cover image is null
        {
            Assert.Throws<ArgumentNullException>(() => new JpegImage(null, 100, 4));
        }

        [Test()]
        public void JpegImage_Test_If_image_width_Is_unchanged_by_constructor() //Test if JpegImage constructor does not throw exception when cover image is not null
        {
            int imageWidth = 200;
            int imageHeight = 100;

            Bitmap bm = new Bitmap(imageWidth, imageHeight);
            JpegImage ji = new JpegImage(bm, 100, 4);

            Assert.AreEqual(imageWidth, ji.CoverImage.Width);
        }

        [Test()]
        public void JpegImage_Test_If_image_height_Is_unchanged_by_constructor() //Test if JpegImage constructor does not throw exception when cover image is not null
        {
            int imageWidth = 200;
            int imageHeight = 100;

            Bitmap bm = new Bitmap(imageWidth, imageHeight);
            JpegImage ji = new JpegImage(bm, 100, 4);

            Assert.AreEqual(imageHeight, ji.CoverImage.Height);
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