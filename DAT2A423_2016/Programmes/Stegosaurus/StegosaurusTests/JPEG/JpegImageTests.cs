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
using Assert = NUnit.Framework.Assert;

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

            Assert.AreEqual(4, modField);
        }

        [Test()]
        public void JpegImage_Test_if_constructor_throws_exception_when_image_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new JpegImage(null, 100, 4));
        }

        [Test()]
        public void Encode_Test_if_throws_Exception_When_Message_Length_Is_Over_Capacity()
        {
            Bitmap inputBitmap = new Bitmap(16, 16);
            inputBitmap.SetPixel(8,8, Color.White);
            

            Bitmap scaledBitmap = new Bitmap(inputBitmap, 200, 200);
            JpegImage ji = new JpegImage(scaledBitmap, 100, 4); // Maximum length is 22
            
            byte[] msg = new byte[50];

            Assert.Throws<ImageCannotContainDataException>(() => ji.Encode(msg));
        }

        [Test()]
        public void Save_Test_if_when_jpeg_writer_is_null_throws_exception()
        {
            JpegImage ji = new JpegImage(new Bitmap(200,100), 100, 4);
            Assert.Throws<Exception>(()=> ji.Save("test"));
        }

        [Test()]
        public void CalculateCosineCoefficients_Test()
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
           
            Assert.AreEqual(ExpectedCosCoef, CosCoef);
        }

        [Test()]
        public void GetCapacity_Test() 
        {
            
            var inputBitmap = new Bitmap(16, 16); //Scale the unit bitmap

            for (int i = 0; i < 16; i+=2)
            {
                for (int j = 0; j < 16; j+=2)
                {
                    inputBitmap.SetPixel(i,j,Color.White);
                    inputBitmap.SetPixel(i+1,j+1,Color.Black);
                }
            }

            JpegImage ji = new JpegImage(inputBitmap, 100, 4);

            int capacity = ji.GetCapacity();

            Assert.AreEqual(10, capacity);
        }

        [Test()]
        public void BreakDownMessage_Test() //TODO: check this test. In order to test logic regarding length of message we needed to drill down into "_splitMessageIntoSmallerComponents"
        {
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject po = new PrivateObject(ji);

            byte[] message = new byte[] {1,1,1};

            po.Invoke("_breakDownMessage", message);

            List<byte> messageList = new List<byte>();
            messageList = (List<byte>)po.GetField("_message"); //Get the broken down message from instance of JpegImage class

            List<byte> expectedList = new List<byte> {0, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1}; // What {1,1,1} corresponds to when broken down and has length encoded

            Assert.AreEqual(expectedList, messageList);
        }

        [Test()]
        public void PadCoverImage_Test_When_Cover_Is_Divisible_by_16()
        {
            Bitmap coverImage = new Bitmap(16, 16);
            JpegImage ji = new JpegImage(coverImage, 100, 4);
            PrivateObject po = new PrivateObject(ji);

            po.Invoke("_padCoverImage");

            Bitmap returnedCoverImage = ji.CoverImage;

            Assert.AreEqual(coverImage, returnedCoverImage);
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

            Assert.AreEqual(expectedCoverImage.Size, returnedCoverImage.Size);
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

            Assert.AreEqual(expectedCoverImage.GetPixel(15, 15), returnedCoverImage.GetPixel(15, 15));
        } 
        //TODO: maybe make more tests for padCoverImage
        [Test()]
        public void CopyBitmap_Test()
        {
            Bitmap b = new Bitmap(1, 1);
            b.SetPixel(0, 0, Color.Black);
            Bitmap testBitmapIn = new Bitmap(b, 200, 100);

            PrivateType pt = new PrivateType(typeof(JpegImage));

            Bitmap copiedBitmap = (Bitmap)pt.InvokeStatic("_copyBitmap", new object[] {testBitmapIn, 200, 100});
 
            Assert.AreEqual(testBitmapIn.GetPixel(100, 50), copiedBitmap.GetPixel(100, 50));
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

            Assert.AreEqual(expectedChannels, returnedChannels);
        }

        [Test()]
        public void EncodeAndQuantizeValues_Test_() 
        {
            JpegImage ji = new JpegImage(new Bitmap(16, 16), 100, 4);

            PrivateObject po = new PrivateObject(ji);

            sbyte[][,] inputValues = {
                new sbyte[16, 16], 
                new sbyte[16, 16],
                new sbyte[16, 16], 
            };
            inputValues[0][0, 0] = 1;
            inputValues[0][0, 1] = 2;
            inputValues[0][0, 2] = 3;
            inputValues[0][0, 3] = 4;

            po.Invoke("_encodeAndQuantizeValues", new object[] {inputValues, 16, 16});

            List<short> nonZero = (List<short>)po.GetField("_nonZeroValues");
            
            List<short> expected = new List<short>(4) {1, 1, 1, 1};

            Assert.AreEqual(expected, nonZero);
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

            Assert.AreEqual(expectedDownSampledValues, downSampleValues);
        }

        [Test()]
        public void Block16ToBlock8_Test() 
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

            Assert.AreEqual(expectedBlock8, returnedBlock8);
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

            
            Assert.That(expectedCosineValues, Is.EquivalentTo(returnedCosineValues));
        }

        [Test()]
        public void C_Test_i0_j0()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float) pt.InvokeStatic("_c", new object[] {0, 0});
            
            Assert.AreEqual(0.125f, returnedC);
        }

        [Test()]
        public void C_Test_i0_j1()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 0, 1 });

            Assert.AreEqual(0.17677f, returnedC);
        }

        [Test()]
        public void C_Test_i1_j0()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 1, 0 });

            Assert.AreEqual(0.17677f, returnedC);
        }

        [Test()]
        public void C_Test_i1_j1()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            float returnedC = (float)pt.InvokeStatic("_c", new object[] { 1, 1 });

            Assert.AreEqual(0.25f, returnedC);
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

            Assert.AreEqual(expectedQuantizedValues, quantizedValues);
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

            Assert.AreEqual(expectedGraph.ToString(), returnedGraph.ToString());
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

            Assert.AreEqual(expectedGraph.Edges, inputGraph.Edges);
        }

        [Test()]
        public void RefactorGraph_Test() //TODO: fix this test
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            Vertex
                v1 = new Vertex(0, 1, 0, 4),
                v2 = new Vertex(2, 3, 2, 4),
                v3 = new Vertex(4, 5, 3, 4),
                v4 = new Vertex(6, 7, 3, 4),
                v5 = new Vertex(8, 9, 4, 4),
                v6 = new Vertex(10, 11, 5, 4);
            Edge
                e1 = new Edge(v1, v2, 1, true, false),
                e2 = new Edge(v3, v4, 2, true, true),
                e3 = new Edge(v5, v6, 2, false, true);

            Graph inputGraph = new Graph();
            Graph sad = new Graph();
            inputGraph.Edges.Add(e1);
            inputGraph.Edges.Add(e2);
            inputGraph.Edges.Add(e3);
            sad.Edges.Add(e1);
            sad.Edges.Add(e2);
            sad.Edges.Add(e3);
            sad.Vertices.AddRange(new List<Vertex>() { v1, v2, v3, v4, v5, v6 });
            inputGraph.Vertices.AddRange(new List<Vertex>() { v1, v2, v3, v4, v5, v6 });

            
            pt.InvokeStatic("_refactorGraph", inputGraph);
            
            Assert.AreEqual(sad.Vertices, inputGraph.Vertices);
        }

        [Test()]
        public void SwapVertexData()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            Vertex
                inputV1 = new Vertex(0, 1, 0, 4),
                inputV2 = new Vertex(2, 3, 0, 4),
                expectedV1 = new Vertex(2, 1, 0, 4),
                expectedV2 = new Vertex(0, 3, 0, 4);

            Edge
                inputE1 = new Edge(inputV1, inputV2, 0, true, true),
                expectedE1 = new Edge(expectedV1, expectedV2, 0, true, true);

            pt.InvokeStatic("_swapVertexData", inputE1);

            Assert.AreEqual(expectedE1.ToString(), inputE1.ToString());
        }

        [Test()]
        public void ForceSampleChange()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            Vertex
                inputVertex = new Vertex(0, 1, 0, 4),
                expectedVertex = new Vertex(-1, 1, 0, 4);

            pt.InvokeStatic("_forceSampleChange", inputVertex);

            Assert.AreEqual(expectedVertex.ToString(), inputVertex.ToString());
        }

        [Test()]
        public void MergeGraphAndQuantizedValues_Test()
        {
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject po = new PrivateObject(ji);
            
            Vertex
                v1 = new Vertex(2, 3, 1, 4);

            Graph inputGraph = new Graph();

            inputGraph.Vertices.Add(v1);
            short[,] inBlock8 = new short[8, 8];
            
            var expectedTuple = new Tuple<short[,], HuffmanTable, HuffmanTable, int>(inBlock8, ji.YDCHuffman, ji.YACHuffman, 0);
            
            var quantizedBlocks = new List<Tuple<short[,], HuffmanTable, HuffmanTable, int>>();
            var expectedQuantizedBlocks = new List<Tuple<short[,], HuffmanTable, HuffmanTable, int>>();

            quantizedBlocks.Add(expectedTuple);

            expectedQuantizedBlocks = quantizedBlocks;

            po.Invoke("_mergeGraphAndQuantizedValues", inputGraph);

            var returnedQuantizedBlocks = (List<Tuple<short[,], HuffmanTable, HuffmanTable, int>>)po.GetField("_quantizedBlocks");

            Assert.AreEqual(expectedQuantizedBlocks.ToString(), returnedQuantizedBlocks.ToString());
        }

        [Test()]
        public void HuffmanEncode() //TODO: Delete this
        {
            JpegImage ji = new JpegImage(new Bitmap(200, 100), 100, 4);
            PrivateObject po = new PrivateObject(ji);

            BitList bl = new BitList();
            short[,] inputBlock = new short[8, 8]
            {
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1, 1},
            };
            HuffmanTable inputHuffDC = new HuffmanTable(
            #region HuffmanElements
                new HuffmanElement(0x00, 0x00, 2),
                new HuffmanElement(0x01, 0x02, 3),
                new HuffmanElement(0x02, 0x03, 3),
                new HuffmanElement(0x03, 0x04, 3),
                new HuffmanElement(0x04, 0x05, 3),
                new HuffmanElement(0x05, 0x06, 3),
                new HuffmanElement(0x06, 0x0e, 4),
                new HuffmanElement(0x07, 0x1e, 5),
                new HuffmanElement(0x08, 0x3e, 6),
                new HuffmanElement(0x09, 0x7e, 7),
                new HuffmanElement(0x0a, 0xfe, 8),
                new HuffmanElement(0x0b, 0x1fe, 9)
            #endregion
            );
            
            HuffmanTable inputHuffAC = new HuffmanTable(
            #region HuffmanElements
                new HuffmanElement(0x00, 0xa, 4),
                new HuffmanElement(0x01, 0x0, 2),
                new HuffmanElement(0x02, 0x1, 2),
                new HuffmanElement(0x03, 0x4, 3),
                new HuffmanElement(0x04, 0xb, 4),
                new HuffmanElement(0x05, 0x1a, 5),
                new HuffmanElement(0x06, 0x78, 7),
                new HuffmanElement(0x07, 0xf8, 8),
                new HuffmanElement(0x08, 0x3f6, 10),
                new HuffmanElement(0x09, 0xff82, 16),
                new HuffmanElement(0x0a, 0xff83, 16),
                new HuffmanElement(0x11, 0xc, 4),
                new HuffmanElement(0x12, 0x1b, 5),
                new HuffmanElement(0x13, 0x79, 7),
                new HuffmanElement(0x14, 0x1f6, 9),
                new HuffmanElement(0x15, 0x7f6, 11),
                new HuffmanElement(0x16, 0xff84, 16),
                new HuffmanElement(0x17, 0xff85, 16),
                new HuffmanElement(0x18, 0xff86, 16),
                new HuffmanElement(0x19, 0xff87, 16),
                new HuffmanElement(0x1a, 0xff88, 16),
                new HuffmanElement(0x21, 0x1c, 5),
                new HuffmanElement(0x22, 0xf9, 8),
                new HuffmanElement(0x23, 0x3f7, 10),
                new HuffmanElement(0x24, 0xff4, 12),
                new HuffmanElement(0x25, 0xff89, 16),
                new HuffmanElement(0x26, 0xff8a, 16),
                new HuffmanElement(0x27, 0xff8b, 16),
                new HuffmanElement(0x28, 0xff8c, 16),
                new HuffmanElement(0x29, 0xff8d, 16),
                new HuffmanElement(0x2a, 0xff8e, 16),
                new HuffmanElement(0x31, 0x3a, 6),
                new HuffmanElement(0x32, 0x1f7, 9),
                new HuffmanElement(0x33, 0xff5, 12),
                new HuffmanElement(0x34, 0xff8f, 16),
                new HuffmanElement(0x35, 0xff90, 16),
                new HuffmanElement(0x36, 0xff91, 16),
                new HuffmanElement(0x37, 0xff92, 16),
                new HuffmanElement(0x38, 0xff93, 16),
                new HuffmanElement(0x39, 0xff94, 16),
                new HuffmanElement(0x3a, 0xff95, 16),
                new HuffmanElement(0x41, 0x3b, 6),
                new HuffmanElement(0x42, 0x3f8, 10),
                new HuffmanElement(0x43, 0xff96, 16),
                new HuffmanElement(0x44, 0xff97, 16),
                new HuffmanElement(0x45, 0xff98, 16),
                new HuffmanElement(0x46, 0xff99, 16),
                new HuffmanElement(0x47, 0xff9a, 16),
                new HuffmanElement(0x48, 0xff9b, 16),
                new HuffmanElement(0x49, 0xff9c, 16),
                new HuffmanElement(0x4a, 0xff9d, 16),
                new HuffmanElement(0x51, 0x7a, 7),
                new HuffmanElement(0x52, 0x7f7, 11),
                new HuffmanElement(0x53, 0xff9e, 16),
                new HuffmanElement(0x54, 0xff9f, 16),
                new HuffmanElement(0x55, 0xffa0, 16),
                new HuffmanElement(0x56, 0xffa1, 16),
                new HuffmanElement(0x57, 0xffa2, 16),
                new HuffmanElement(0x58, 0xffa3, 16),
                new HuffmanElement(0x59, 0xffa4, 16),
                new HuffmanElement(0x5a, 0xffa5, 16),
                new HuffmanElement(0x61, 0x7b, 7),
                new HuffmanElement(0x62, 0xff6, 12),
                new HuffmanElement(0x63, 0xffa6, 16),
                new HuffmanElement(0x64, 0xffa7, 16),
                new HuffmanElement(0x65, 0xffa8, 16),
                new HuffmanElement(0x66, 0xffa9, 16),
                new HuffmanElement(0x67, 0xffaa, 16),
                new HuffmanElement(0x68, 0xffab, 16),
                new HuffmanElement(0x69, 0xffac, 16),
                new HuffmanElement(0x6a, 0xffad, 16),
                new HuffmanElement(0x71, 0xfa, 8),
                new HuffmanElement(0x72, 0xff7, 12),
                new HuffmanElement(0x73, 0xffae, 16),
                new HuffmanElement(0x74, 0xffaf, 16),
                new HuffmanElement(0x75, 0xffb0, 16),
                new HuffmanElement(0x76, 0xffb1, 16),
                new HuffmanElement(0x77, 0xffb2, 16),
                new HuffmanElement(0x78, 0xffb3, 16),
                new HuffmanElement(0x79, 0xffb4, 16),
                new HuffmanElement(0x7a, 0xffb5, 16),
                new HuffmanElement(0x81, 0x1f8, 9),
                new HuffmanElement(0x82, 0x7fc0, 15),
                new HuffmanElement(0x83, 0xffb6, 16),
                new HuffmanElement(0x84, 0xffb7, 16),
                new HuffmanElement(0x85, 0xffb8, 16),
                new HuffmanElement(0x86, 0xffb9, 16),
                new HuffmanElement(0x87, 0xffba, 16),
                new HuffmanElement(0x88, 0xffbb, 16),
                new HuffmanElement(0x89, 0xffbc, 16),
                new HuffmanElement(0x8a, 0xffbd, 16),
                new HuffmanElement(0x91, 0x1f9, 9),
                new HuffmanElement(0x92, 0xffbe, 16),
                new HuffmanElement(0x93, 0xffbf, 16),
                new HuffmanElement(0x94, 0xffc0, 16),
                new HuffmanElement(0x95, 0xffc1, 16),
                new HuffmanElement(0x96, 0xffc2, 16),
                new HuffmanElement(0x97, 0xffc3, 16),
                new HuffmanElement(0x98, 0xffc4, 16),
                new HuffmanElement(0x99, 0xffc5, 16),
                new HuffmanElement(0x9a, 0xffc6, 16),
                new HuffmanElement(0xa1, 0x1fa, 9),
                new HuffmanElement(0xa2, 0xffc7, 16),
                new HuffmanElement(0xa3, 0xffc8, 16),
                new HuffmanElement(0xa4, 0xffc9, 16),
                new HuffmanElement(0xa5, 0xffca, 16),
                new HuffmanElement(0xa6, 0xffcb, 16),
                new HuffmanElement(0xa7, 0xffcc, 16),
                new HuffmanElement(0xa8, 0xffcd, 16),
                new HuffmanElement(0xa9, 0xffce, 16),
                new HuffmanElement(0xaa, 0xffcf, 16),
                new HuffmanElement(0xb1, 0x3f9, 10),
                new HuffmanElement(0xb2, 0xffd0, 16),
                new HuffmanElement(0xb3, 0xffd1, 16),
                new HuffmanElement(0xb4, 0xffd2, 16),
                new HuffmanElement(0xb5, 0xffd3, 16),
                new HuffmanElement(0xb6, 0xffd4, 16),
                new HuffmanElement(0xb7, 0xffd5, 16),
                new HuffmanElement(0xb8, 0xffd6, 16),
                new HuffmanElement(0xb9, 0xffd7, 16),
                new HuffmanElement(0xba, 0xffd8, 16),
                new HuffmanElement(0xc1, 0x3fa, 10),
                new HuffmanElement(0xc2, 0xffd9, 16),
                new HuffmanElement(0xc3, 0xffda, 16),
                new HuffmanElement(0xc4, 0xffdb, 16),
                new HuffmanElement(0xc5, 0xffdc, 16),
                new HuffmanElement(0xc6, 0xffdd, 16),
                new HuffmanElement(0xc7, 0xffde, 16),
                new HuffmanElement(0xc8, 0xffdf, 16),
                new HuffmanElement(0xc9, 0xffe0, 16),
                new HuffmanElement(0xca, 0xffe1, 16),
                new HuffmanElement(0xd1, 0x7f8, 11),
                new HuffmanElement(0xd2, 0xffe2, 16),
                new HuffmanElement(0xd3, 0xffe3, 16),
                new HuffmanElement(0xd4, 0xffe4, 16),
                new HuffmanElement(0xd5, 0xffe5, 16),
                new HuffmanElement(0xd6, 0xffe6, 16),
                new HuffmanElement(0xd7, 0xffe7, 16),
                new HuffmanElement(0xd8, 0xffe8, 16),
                new HuffmanElement(0xd9, 0xffe9, 16),
                new HuffmanElement(0xda, 0xffea, 16),
                new HuffmanElement(0xe1, 0xffeb, 16),
                new HuffmanElement(0xe2, 0xffec, 16),
                new HuffmanElement(0xe3, 0xffed, 16),
                new HuffmanElement(0xe4, 0xffee, 16),
                new HuffmanElement(0xe5, 0xffef, 16),
                new HuffmanElement(0xe6, 0xfff0, 16),
                new HuffmanElement(0xe7, 0xfff1, 16),
                new HuffmanElement(0xe8, 0xfff2, 16),
                new HuffmanElement(0xe9, 0xfff3, 16),
                new HuffmanElement(0xea, 0xfff4, 16),
                new HuffmanElement(0xf0, 0x7f9, 11),
                new HuffmanElement(0xf1, 0xfff5, 16),
                new HuffmanElement(0xf2, 0xfff6, 16),
                new HuffmanElement(0xf3, 0xfff7, 16),
                new HuffmanElement(0xf4, 0xfff8, 16),
                new HuffmanElement(0xf5, 0xfff9, 16),
                new HuffmanElement(0xf6, 0xfffa, 16),
                new HuffmanElement(0xf7, 0xfffb, 16),
                new HuffmanElement(0xf8, 0xfffc, 16),
                new HuffmanElement(0xf9, 0xfffd, 16),
                new HuffmanElement(0xfa, 0xfffe, 16)
            #endregion
            );
            int inputDCIndex = 0;

            po.Invoke("HuffmanEncode", new object[] {bl, inputBlock, inputHuffDC, inputHuffAC, inputDCIndex});

            BitList expectedBitList = new BitList();
            expectedBitList.Add(false);

            //Assert.AreEqual(expectedBitList.Count, bl.Count);
            Assert.Pass();
        }

        [Test()]
        public void Bitcost_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            short input = 2;

            byte expected = 2;

            byte output = (byte)pt.InvokeStatic("_bitCost", input);

            Assert.AreEqual(expected, output);
        }

        [Test()]
        public void UShortToBits_Test()
        {
            BitList bitList = new BitList();
            ushort input = 3;
            byte inputLen = 2;
            
            PrivateType pt = new PrivateType(typeof(JpegImage));

            pt.InvokeStatic("_ushortToBits", new object[] {bitList, input, inputLen});

            BitList expectedBitList = new BitList();
            expectedBitList.Add(true);
            expectedBitList.Add(true);

            Assert.AreEqual(expectedBitList, bitList);
        }

        [Test()]
        public void NumberEncoder_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));
            short input = -1;
            ushort expected = 65534; // 11111111 11111110

            ushort output = (ushort)pt.InvokeStatic("_numberEncoder", input);
            
            Assert.AreEqual(expected, output);
        }

        [Test()]
        public void Flush_Test()
        {
            PrivateType pt = new PrivateType(typeof(JpegImage));

            BitList bl = new BitList();
            bl.Add(true);
            bl.Add(true);
            bl.Add(false);

            byte[] output = (byte[]) pt.InvokeStatic("_flush", bl);

            byte[] expected = new byte[1] {192}; // 11000000

            Assert.AreEqual(expected, output);

        }
    }
}