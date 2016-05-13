using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class JpegImageTests
    {
        [Test]
        public void JpegImage_Test_if_constructor_throws_exception_when_image_is_null() //Test if JpegImage constructor throws exception when cover image is null
        {
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => new JpegImage(null, 100, 4));
        }

        [Test()]
        public void JpegImage_Test_If_image_width_Is_unchanged_by_constructor() //Test if JpegImage constructor does not throw exception when cover image is not null
        {
            int imageWidth = 200;
            int imageHeight = 100;

            Bitmap bm = new Bitmap(imageWidth, imageHeight);
            JpegImage ji = new JpegImage(bm, 100, 4);

            NUnit.Framework.Assert.AreEqual(imageWidth, ji.CoverImage.Width);
        }

        [Test()]
        public void JpegImage_Test_If_image_height_Is_unchanged_by_constructor() //Test if JpegImage constructor does not throw exception when cover image is not null
        {
            int imageWidth = 200;
            int imageHeight = 100;

            Bitmap bm = new Bitmap(imageWidth, imageHeight);
            JpegImage ji = new JpegImage(bm, 100, 4);

            NUnit.Framework.Assert.AreEqual(imageHeight, ji.CoverImage.Height);
        }
        
        [Test()]
        public void EncodeTest()
        {
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject obj = new PrivateObject(ji);
            NUnit.Framework.Assert.Fail();
            NUnit.Framework.Assert.Fail();
           
        }

        [Test()]
        public void SaveTest()
        {
            NUnit.Framework.Assert.Fail();
        }

        [Test()]
        public void GetCapacityTest()
        {
            NUnit.Framework.Assert.Fail();
        }
    }
}