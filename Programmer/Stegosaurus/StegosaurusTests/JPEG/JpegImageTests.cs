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
            /*Bitmap b = new Bitmap(3, 3);
            b.SetPixel(0,0, Color.White);
            b.SetPixel(1,1,Color.DarkBlue);
            b.SetPixel(0,1,Color.Red);
            b.SetPixel(1,0,Color.Green);
            b.SetPixel(2,2,Color.Purple);
           

            var result = new Bitmap(b, 2000, 2000);
            var hello = new JpegImage(result, 100, 4);
            int das = hello.GetCapacity();
            int len = 40;
            byte[] msg = new byte[len];
            
            

            hello.Encode(msg);*/
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
        public void GetCapacity_Test() //TODO: Fix this test/remake it
        {
            var b = new Bitmap(1, 1);
            b.SetPixel(0, 0, Color.White);
            //b.SetPixel(1,0,Color.White);
            //b.SetPixel(0,1,Color.White);
            //b.SetPixel(1,1,Color.White);

            var scaledUnitBitmap = new Bitmap(b, 160, 160); //Scale the unit bitmap

            //scaledUnitBitmap.Save(@"C:\Users\LeoMohr\Desktop\out.png");

            JpegImage ji = new JpegImage(scaledUnitBitmap, 100, 4);

            int capacity = ji.GetCapacity();

            NUnit.Framework.Assert.AreEqual(34, capacity);
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
            //po.Invoke("_writeHuffmanSegment", new object[] {ji.YDCHuffman, 0, true});

            NUnit.Framework.Assert.Ignore();
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Divisible_by_16()
        {
            Bitmap coverImage = new Bitmap(16, 16);
            JpegImage ji = new JpegImage(coverImage, 100, 4);
            PrivateObject po = new PrivateObject(ji);

            po.Invoke("_padCoverImage");

            Bitmap returnedCoverImage = ji.CoverImage;

            NUnit.Framework.Assert.AreEqual(coverImage, returnedCoverImage);
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Not_Divisible_by_16_Test_Size()
        {
            Bitmap b = new Bitmap(1,1);
            b.SetPixel(0,0, Color.Black);
            Bitmap coverImage = new Bitmap(b, 15, 15);

            JpegImage ji = new JpegImage(coverImage, 100, 4);
            PrivateObject po = new PrivateObject(ji);

            po.Invoke("_padCoverImage");

            Bitmap returnedCoverImage = ji.CoverImage;

            Bitmap expectedCoverImage = new Bitmap(b, 16,16);

            NUnit.Framework.Assert.AreEqual(expectedCoverImage.Size, returnedCoverImage.Size);
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Not_Divisible_by_16_Test_Colour()
        {
            Bitmap b = new Bitmap(1, 1);
            b.SetPixel(0, 0, Color.Black);
            Bitmap coverImage = new Bitmap(b, 15, 15);
            JpegImage ji = new JpegImage(coverImage, 100, 4);
            PrivateObject po = new PrivateObject(ji);

            po.Invoke("_padCoverImage");

            Bitmap returnedCoverImage = ji.CoverImage;

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
        public void SplitToChannels_Test()
        {
            Bitmap b = new Bitmap(2,2);
            b.SetPixel(0,0, Color.Black);   // R = 0,   G = 0,   B = 0 
            b.SetPixel(1,0, Color.Green);   // R = 0,   G = 128, B = 0
            b.SetPixel(0,1, Color.Red);     // R = 128, G = 0,   B = 0
            b.SetPixel(1,1,Color.Blue);     // R = 0,   G = 0,   B= 128

            PrivateType pt = new PrivateType(typeof(JpegImage));


            sbyte[][,] returnedChannels = (sbyte[][,])pt.InvokeStatic("_splitToChannels", b);

            sbyte[,] ch1 = {
                {-128, -51}, {-52, -98}
            };
            sbyte[,] ch2 = {
                {0, -43}, {-42, 127}
            };
            sbyte[,] ch3 = {
                {0, 127}, {-53, -20}
            };

            sbyte[][,] expectedChannels = {ch1, ch2, ch3};

            NUnit.Framework.Assert.AreEqual(expectedChannels, returnedChannels);
        }

        [Test()]
        public void EncodeAndQuantizeValues_Test_() //TODO: "_encodeAndQuantizeValues" method can't be found
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject po = new PrivateObject(ji);

            sbyte[][,] inputValues = {
                new sbyte[16, 16], 
                new sbyte[16, 16],
                new sbyte[16, 16], 
            };

            ji.GetCapacity();
            //pt.InvokeStatic("_encodeAndQuantizeValues", new object[] {inputValues, 200, 100});
            po.Invoke("_encodeAndQuantizeValues", new object[] {inputValues, 200, 100});

            List<Tuple<short[,], HuffmanTable, HuffmanTable, int>> quan = (List<Tuple<short[,], HuffmanTable, HuffmanTable, int>>)po.GetField("_quantizedBlocks");
            List<short> nonzero = (List<short>)po.GetField("_nonZeroValues");
        }

        [Test()]
        public void DownSample_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            float[,] inputValues = new float[16, 16];
            int i = 0;
            for (int x = 0; x < 16; x++) //Fill input value with values from 1 to 255
            {
                for (int y = 0; y < 16; y++)
                {
                    inputValues[x, y] = i++;
                }
            }

            float[,] downSampleValues = (float[,])pt.InvokeStatic("_downSample", inputValues);

            float[,] expectedDownSampledValues = new float[8, 8]
            {
                {8.5f,  10.5f, 12.5f, 14.5f, 16.5f, 18.5f, 20.5f, 22.5f},
                {40.5f, 42.5f, 44.5f, 46.5f, 48.5f, 50.5f, 52.5f, 54.5f},
                {72.5f, 74.5f, 76.5f, 78.5f, 80.5f, 82.5f, 84.5f, 86.5f},
                {104.5f, 106.5f, 108.5f, 110.5f, 112.5f, 114.5f, 116.5f, 118.5f},
                {136.5f, 138.5f, 140.5f, 142.5f, 144.5f, 146.5f, 148.5f, 150.5f},
                {168.5f, 170.5f, 172.5f, 174.5f, 176.5f, 178.5f, 180.5f, 182.5f},
                {200.5f, 202.5f, 204.5f, 206.5f, 208.5f, 210.5f, 212.5f, 214.5f},
                {232.5f, 234.5f, 236.5f, 238.5f, 240.5f, 242.5f, 244.5f, 246.5f},
            };

            NUnit.Framework.Assert.AreEqual(expectedDownSampledValues, downSampleValues);
        }

        [Test()]
        public void Block16ToBlock8_Test() //TODO: might want to check this test
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            

            float[,] inputValues = new float[16, 16];
            int i = 0;
            for (int x = 0; x < 16; x++) //Fill input value with values from 0 to 255
            {
                for (int y = 0; y < 16; y++)
                {
                    inputValues[x, y] = i++;
                }
            }

            float[,] returnedBlock8 = (float[,]) pt.InvokeStatic("_block16ToBlock8", new object[] {inputValues, 1});

            float[,] expectedBlock8 = new float[8, 8]
            {
                {128,  129f, 130f, 131f, 132f, 133f, 134f, 135f},
                {144f, 145f, 146f, 147f, 148f, 149f, 150f, 151f},
                {160f, 161f, 162f, 163f, 164f, 165f, 166f, 167f},
                {176f, 177f, 178f, 179f, 180f, 181f, 182f, 183f},
                {192f, 193f, 194f, 195f, 196f, 197f, 198f, 199f},
                {208f, 209f, 210f, 211f, 212f, 213f, 214f, 215f},
                {224f, 225f, 226f, 227f, 228f, 229f, 230f, 231f},
                {240f, 241f, 242f, 243f, 244f, 245f, 246f, 247f},
            };

            NUnit.Framework.Assert.AreEqual(expectedBlock8, returnedBlock8);
        }

        [Test()]
        public void DiscreteCosineTransform_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);

            float[,] inputBlock8 = new float[8, 8];
            int i = 0;
            for (int x = 0; x < 8; x++) //Fill input value with values from 0 to 63
            {
                for (int y = 0; y < 8; y++)
                {
                    inputBlock8[x, y] = i++;
                }
            }

            float[,] returnedCosineValues = (float[,])pt.InvokeStatic("_discreteCosineTransform", inputBlock8);

            float[,] expectedCosineValues = new float[8, 8]
            {
                {252f, -18.220953f, -1.3045323E-07f, -1.90474725f, 1.78540418E-07f, -0.568218122f, -8.80875195E-09f, -0.143401206f},
                {-145.767609f, 2.41847101E-06f, 8.53143945E-07f, 1.44885166E-06f, 1.03676985E-06f, -1.5390215E-07f, -2.55802661E-07f, -9.47974854E-08f},
                {-2.99147609E-06f, 5.62151536E-06f, -1.10848976E-06f, -3.00187935E-07f, 2.10143571E-06f, -5.08922085E-07f, -6.90155488E-08f, 9.28238705E-07f},
                {-15.2379627f, 4.95177403E-07f, -1.25386225E-06f, -2.32657641E-07f, -7.20322817E-07f, 4.82752E-07f, -1.97521246E-07f, 6.68256234E-07f},
                {-1.72880823E-06f, -8.70578845E-07f, -7.59587181E-07f, 1.18702587E-06f, 2.07287385E-07f, 6.64636985E-08f, 1.24796173E-07f, -2.74013161E-07f},
                {-4.54574156f, -2.06125083E-06f, 1.39842655E-06f, 5.91484728E-09f, -8.87210604E-07f, -3.22430438E-09f, 1.63273896E-07f, -2.61182095E-07f},
                {5.71323699E-06f, 2.60522029E-06f, 4.07821602E-07f, 7.56153099E-07f, -8.28878115E-07f, 1.63273896E-07f, -2.87587607E-08f, 4.81403617E-07f},
                {-1.1472187f, -1.5253089E-06f, 1.64349444E-06f, 4.29837684E-07f, -5.12431711E-07f, 8.11701511E-07f, 1.23775763E-07f, 7.26117264E-07f},
            };
            GlobalSettings.DefaultFloatingPointTolerance = 0.00001;

            
            NUnit.Framework.Assert.That(expectedCosineValues, Is.EquivalentTo(returnedCosineValues));
        }

        [Test()]
        public void C_Test_i0_j0()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float) pt.InvokeStatic("_c", new object[] {0, 0});
            
            NUnit.Framework.Assert.AreEqual(0.125f, returnedC);
        }

        [Test()]
        public void C_Test_i0_j1()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 0, 1 });

            NUnit.Framework.Assert.AreEqual(0.17677f, returnedC);
        }

        [Test()]
        public void C_Test_i1_j0()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 1, 0 });

            NUnit.Framework.Assert.AreEqual(0.17677f, returnedC);
        }

        [Test()]
        public void C_Test_i1_j1()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 1, 1 });

            NUnit.Framework.Assert.AreEqual(0.25f, returnedC);
        }

        [Test()]
        public void Quantization_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);


            float[,] inputValues = new float[8, 8];
            int i = 0;
            for (int x = 0; x < 8; x++) //Fill input value with values from 0 to 63
            {
                for (int y = 0; y < 8; y++)
                {
                    inputValues[x, y] = i++;
                }
            }

            short[,] quantizedValues =
                (short[,]) pt.InvokeStatic("_quantization", new object[] {inputValues, ji.YQuantizationTable});

            short[,] expectedQuantizedValues = new short[8, 8]
            {
                {0, 1, 2, 3, 2, 1, 1, 0},
                {8, 9, 10, 5, 6, 3, 1, 1},
                {16, 17, 9, 9, 5, 3, 2, 2},
                {12, 12, 8, 9, 4, 3, 3, 2},
                {10, 11, 6, 5, 4, 3, 3, 2},
                {8, 5, 6, 4, 3, 3, 3, 3},
                {8, 7, 6, 5, 4, 3, 3, 4},
                {8, 9, 8, 8, 6, 5, 5, 5},
            };

            NUnit.Framework.Assert.AreEqual(expectedQuantizedValues, quantizedValues);
        }

        [Test()]
        public void EncodeMessage_Test()
        {
            //TODO: Ask if _encodeMessage needs test, because the only logic within is a for loop and a .Where
            NUnit.Framework.Assert.Ignore();
        }

        [Test()]
        public void AddVertices_Test()
        {
            PrivateObject po = new PrivateObject(new JpegImage(new Bitmap(200, 100), 100, 4));

            List<short> inputNonZeroValues = new List<short>();
            List<byte> inputMessage = new List<byte>();

            for (int i = 0; i < 20; i++)
            {
                inputNonZeroValues.Add((short)i);
            }
            for (int e = 0; e < 10; e++)
            {
                inputMessage.Add((byte)e);
            }

            po.SetField("_nonZeroValues", inputNonZeroValues);
            po.SetField("_message",inputMessage);

            Vertex
                v1 = new Vertex(0, 1, 0, 4),
                v2 = new Vertex(2, 3, 1, 4),
                v3 = new Vertex(4, 5, 2, 4),
                v4 = new Vertex(6, 7, 3, 4),
                v5 = new Vertex(8, 9, 4, 4),
                v6 = new Vertex(10, 11, 5, 4),
                v7 = new Vertex(12, 13, 6, 4),
                v8 = new Vertex(14, 15, 7, 4),
                v9 = new Vertex(16, 17, 8, 4),
                v10 = new Vertex(18, 19, 9, 4);

            Graph returnedGraph = new Graph();
            Graph expectedGraph = new Graph();

            expectedGraph.Vertices.AddRange(new List<Vertex>() {v1, v2, v3, v4, v5, v6, v7, v8, v9, v10});

            po.Invoke("_addVertices", returnedGraph);

            NUnit.Framework.Assert.AreEqual(expectedGraph.ToString(), returnedGraph.ToString());
        }

        [Test()]
        public void AddEdge_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            Vertex
                v1 = new Vertex(0, 1, 0, 4),
                v2 = new Vertex(2, 3, 2, 4),
                v3 = new Vertex(4, 5, 3, 4),
                v4 = new Vertex(6, 7, 3, 4),
                v5 = new Vertex(8, 9, 4, 4),
                v6 = new Vertex(10, 11, 5, 4);

            Graph inputGraph = new Graph();
            inputGraph.Vertices.AddRange(new List<Vertex>() { v1, v2, v3, v4, v5, v6 });

            int inputThreshold = 5;

            pt.InvokeStatic("_addEdge", new object[] {true, false, v1, v2, inputThreshold, inputGraph});    //pass
            pt.InvokeStatic("_addEdge", new object[] {true, true, v3, v4, inputThreshold, inputGraph});     //pass
            pt.InvokeStatic("_addEdge", new object[] {false, true, v5, v6, inputThreshold, inputGraph});    //fail

            Graph expectedGraph = new Graph();

            expectedGraph.Edges.Add(new Edge(v1, v2, 3, true, false));
            expectedGraph.Edges.Add(new Edge(v3, v4, 2, true, true));

            NUnit.Framework.Assert.AreEqual(expectedGraph.Edges, inputGraph.Edges);

        }
    }
}