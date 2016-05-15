using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Compatibility;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class JpegImageTests
    {
        [Test()]
        public void M_Test_if_modulo_field_gets_set()
        {
            PrivateObject po = new PrivateObject(new JpegImage(new Bitmap(200, 100), 100, 4));

            var modField = po.GetField("_m");

            NUnit.Framework.Assert.AreEqual(4, modField);
        }

        [Test()]
        public void JpegImage_Test_if_constructor_throws_exception_when_image_is_null() //Test if JpegImage constructor throws exception when cover image is null
        {
            NUnit.Framework.Assert.Throws<ArgumentNullException>(() => new JpegImage(null, 100, 4));
        }

        [Test()]
        public void Encode_Test_if_throws_Exception_When_Message_Length_Is_Over_Limit() //TODO: Some errors with _addVertices, _encodeMessage, _encodeMCU, _writeScanData, Encode
        {
            var b = new Bitmap(1, 1);
            b.SetPixel(0,0, Color.Black);
            var result = new Bitmap(b, 2000, 1000);
            int len = 20;
            byte[] msg = new byte[len];
            for (int i = 0; i < len; i++)
            {
                msg[i] = (byte)('A');
            }

            var hello = new JpegImage(b, 100, 4);

            hello.Encode(msg);
        }

        [Test()]
        public void Save_Test_if_when_jpeg_writer_is_null_throws_exception()
        {
            JpegImage ji = new JpegImage(new Bitmap(200,100), 100, 4);
            NUnit.Framework.Assert.Throws<Exception>(()=> ji.Save("test"));
        }

        [Test()]
        public void CalculateCosineCoefficients_Test() //TODO: Get formula for calc of CosCoef and fill out ExpectedCosCoef with real values
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4); //Constructor calls calcCosineCoef

            float[,] CosCoef = (float[,])pt.GetStaticField("CosinesCoefficients");

            float[,] ExpectedCosCoef = new float[8, 8]
            {
                {1f, 0.980785251f, 0.9238795f, 0.8314696f, 0.707106769f, 0.555570245f, 0.382683426f, 0.195090324f}, 
                {1f, 0.8314696f, 0.382683426f, -0.195090324f, -0.707106769f, -0.980785251f, -0.9238795f, -0.555570245f}, 
                {1f, 0.555570245f, -0.382683426f, -0.980785251f, -0.707106769f, 0.195090324f, 0.923879504f, 0.831469595f}, 
                {1f, 0.195090324f, -0.923879504f, -0.555570245f, 0.707106769f, 0.831469595f, -0.382683426f, -0.980785251f}, 
                {1f, -0.195090324f, -0.923879504f, 0.555570245f, 0.707106769f, -0.831469595f, -0.382683426f, 0.980785251f}, 
                {1f, -0.555570245f, -0.382683426f, 0.980785251f, -0.707106769f, -0.195090324f, 0.923879504f, -0.831469595f}, 
                {1f, -0.831469595f, 0.382683426f, 0.195090324f, -0.707106769f, 0.980785251f, -0.923879504f, 0.555570245f},
                {1f, -0.980785251f, 0.923879504f, -0.831469595f, 0.707106769f, -0.555570245f, 0.382683426f, -0.195090324f},
            };
           
            NUnit.Framework.Assert.AreEqual(ExpectedCosCoef, CosCoef);
        }

        [Test()]
        public void GetCapacity_Test()
        {
            var b = new Bitmap(2, 2);
            b.SetPixel(0, 0, Color.Black);
            b.SetPixel(1,0,Color.Blue);
            b.SetPixel(0,1,Color.Red);
            b.SetPixel(1,1,Color.White);

            var scaledUnitBitmap = new Bitmap(b, 200, 100); //Scale the unit bitmap

            JpegImage ji = new JpegImage(scaledUnitBitmap, 100, 4);

            int capacity = ji.GetCapacity();

            NUnit.Framework.Assert.AreEqual(35, capacity);
        }

        [Test()]
        public void BreakDownMessage_Test() //TODO: check this test. In order to test logic regarding length of message we needed to drill down into "_splitMessageIntoSmallerComponents"
        {
            PrivateObject po = new PrivateObject(new JpegImage(new Bitmap(200, 100), 100, 4));

            byte[] message = new byte[] {1,1,1};

            po.Invoke("_breakDownMessage", message);

            List<byte> messageList = new List<byte>();
            messageList = (List<byte>)po.GetField("_message"); //Get the broken down message from instance of JpegImage class

            List<byte> expectedList = new List<byte> {0, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1}; // What {1,1,1} corresponds to when broken down and has length encoded

            NUnit.Framework.Assert.AreEqual(expectedList, messageList);
        }

        [Test()]
        public void SplitMessageIntoSmallerComponents_Test()
        {
            //TODO: might need a test for this, but "BreakDownMessage_Test" might have it covered already
            NUnit.Framework.Assert.Ignore();
        }

        // Most methods in between here only call jpegWriter or another method which calls it, JpegWriter will be tested on it's own. TODO: ask about this and delete

        [Test()]
        public void WriteHuffmanSegment_Test_Combined() //TODO: Find out how to access JpegWriter even when it's internal
        {
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject po = new PrivateObject(ji);
            
            var joo = po.GetField("_jw");
            po.Invoke("_writeHuffmanSegment", new object[] {ji.YDCHuffman, 0, true});

            NUnit.Framework.Assert.Ignore();
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Divisible_by_16()
        {
            Bitmap coverImage = new Bitmap(16, 16);
            PrivateObject po = new PrivateObject(new JpegImage(coverImage, 100, 4));

            Bitmap returnedCoverImage = (Bitmap) po.Invoke("_padCoverImage");

            NUnit.Framework.Assert.AreEqual(coverImage, returnedCoverImage);
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Not_Divisible_by_16_Test_Size()
        {
            Bitmap b = new Bitmap(1,1);
            b.SetPixel(0,0, Color.Black);
            Bitmap coverImage = new Bitmap(b, 15, 15);
            PrivateObject po = new PrivateObject(new JpegImage(coverImage, 100, 4));

            Bitmap returnedCoverImage = (Bitmap) po.Invoke("_padCoverImage");

            Bitmap expectedCoverImage = new Bitmap(b, 16,16);

            NUnit.Framework.Assert.AreEqual(expectedCoverImage.Size, returnedCoverImage.Size);
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Not_Divisible_by_16_Test_Colour()
        {
            Bitmap b = new Bitmap(1, 1);
            b.SetPixel(0, 0, Color.Black);
            Bitmap coverImage = new Bitmap(b, 15, 15);
            PrivateObject po = new PrivateObject(new JpegImage(coverImage, 100, 4));

            Bitmap returnedCoverImage = (Bitmap)po.Invoke("_padCoverImage");

            Bitmap expectedCoverImage = new Bitmap(b, 16, 16);

            NUnit.Framework.Assert.AreEqual(expectedCoverImage.GetPixel(15, 15), returnedCoverImage.GetPixel(15, 15));
        } //TODO: maybe make more tests for padCoverImage

        [Test()]
        public void CopyBitmap_Test()
        {
            Bitmap b = new Bitmap(1, 1);
            b.SetPixel(0, 0, Color.Black);
            Bitmap testBitmapIn = new Bitmap(b, 200, 100);

            PrivateType pt = new PrivateType(typeof(JpegImage));

            Bitmap copiedBitmap = (Bitmap)pt.InvokeStatic("_copyBitmap", new object[] {testBitmapIn, 200, 100});
 
            NUnit.Framework.Assert.AreEqual(testBitmapIn.GetPixel(100, 50), copiedBitmap.GetPixel(100, 50));
        }

        [Test()]
        public void SplitToChannels_Test() //TODO: Find out why "_splitToChannels" can't be found, might need to go through "writeScanData" but channels don't pop up again
        {
            Bitmap b = new Bitmap(2,2);
            b.SetPixel(0,0, Color.Black);
            b.SetPixel(1,0, Color.Green);
            b.SetPixel(0,1, Color.Red);
            b.SetPixel(1,1,Color.Blue);
            JpegImage ji = new JpegImage(new Bitmap(b, 16, 16), 100, 4);

            PrivateType pt = new PrivateType(typeof(JpegImage));
            PrivateObject po = new PrivateObject(ji);


            sbyte[][,] returnedChannels = (sbyte[][,])pt.InvokeStatic("_splitToChannels", b);

            sbyte[,] ch1 = {
                {1, 2}, {3, 4}, {5, 6}, {7, 8},
            };
            sbyte[,] ch2 = {
                {1, 2}, {3, 4}, {5, 6}, {7, 8},
            };
            sbyte[,] ch3 = {
                {1, 2}, {3, 4}, {5, 6}, {7, 8},
            };

            sbyte[][,] s = {ch1, ch2, ch3};
            
            NUnit.Framework.Assert.AreEqual(s, returnedChannels);
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