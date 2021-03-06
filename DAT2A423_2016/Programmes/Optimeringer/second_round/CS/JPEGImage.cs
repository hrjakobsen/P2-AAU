﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace Stegosaurus {
    public class JpegImage:IImageEncoder {
        private JpegWriter _jw;
        private int _m;
        private Bitmap CoverImage { get; }
        private readonly List<byte> _message = new List<byte>();
        private readonly double[,] _cosinesCoefficients = new double[8, 8];
        private readonly int[] _lastDc = { 0, 0, 0 };
        private readonly List<Tuple<int[,], HuffmanTable, HuffmanTable, int>> _quantizedBlocks = new List<Tuple<int[,], HuffmanTable, HuffmanTable, int>>();
        private readonly List<int> _nonZeroValues = new List<int>();

        private readonly Stopwatch _s = new Stopwatch();
        private string _timings = "";
        private long _totalTime = 0;

        private void _setTimings(string method, long time) {
            Console.WriteLine($"{method} took {time} ms!");
            _timings += time + "; ";
            _totalTime += time;
        }

        public string getTimings() {
            _timings += _totalTime + "; ";
            return _timings;
        }

        /// <summary>
        /// Quantization table used for the Y component of the image.
        /// </summary>
        public QuantizationTable YQuantizationTable { get; set; }

        /// <summary>
        /// Quantization table used for the CrCb components of the image.
        /// </summary>
        public QuantizationTable ChrQuantizationTable { get; set; }

        /// <summary>
        /// Huffman table used for the DC coefficient of the Y component of the image.
        /// </summary>
        public HuffmanTable YDCHuffman { get; set; }

        /// <summary>
        /// Huffman table used for the AC coefficient of the Y component of the image.
        /// </summary>
        public HuffmanTable YACHuffman { get; set; }

        /// <summary>
        /// Huffman table used for the DC coefficient of the CrCb components of the image.
        /// </summary>
        public HuffmanTable ChrDCHuffman { get; set; }

        /// <summary>
        /// Huffman table used for the AC coefficient of the CrCb components of the image.
        /// </summary>
        public HuffmanTable ChrACHuffman { get; set; }

        public int M {
            get { return _m; }
            set {
                switch (value) {
                    case 2:
                    case 4:
                    case 16:
                    case 256:
                        _m = value;
                        break;
                    default:
                        throw new ArgumentException("M must 2, 4, 16 or 256!");
                }
            }
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        public JpegImage(Bitmap coverImage, int quality, int m) : this(coverImage, quality, m, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        /// <param name="yTable"></param>
        /// <param name="chrTable"></param>
        public JpegImage(Bitmap coverImage, int quality, int m, QuantizationTable yTable, QuantizationTable chrTable)
            : this(coverImage, quality, m, yTable, chrTable, HuffmanTable.JpegHuffmanTableYDC, HuffmanTable.JpegHuffmanTableYAC, HuffmanTable.JpegHuffmanTableChrDC, HuffmanTable.JpegHuffmanTableChrAC) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        /// <param name="huffmanYDC"></param>
        /// <param name="huffmanYAC"></param>
        /// <param name="huffmanChrDC"></param>
        /// <param name="huffmanChrAC"></param>
        public JpegImage(Bitmap coverImage, int quality, int m, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC)
            : this(coverImage, quality, m, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable, huffmanYDC, huffmanYAC, huffmanChrDC, huffmanChrAC) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        /// <param name="yTable"></param>
        /// <param name="chrTable"></param>
        /// <param name="huffmanYDC"></param>
        /// <param name="huffmanYAC"></param>
        /// <param name="huffmanChrDC"></param>
        /// <param name="huffmanChrAC"></param>
        public JpegImage(Bitmap coverImage, int quality, int m, QuantizationTable yTable, QuantizationTable chrTable, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC) {
            if (coverImage == null) {
                throw new ArgumentNullException();
            }

            CoverImage = coverImage;
            YQuantizationTable = yTable.Scale(quality);
            ChrQuantizationTable = chrTable.Scale(quality);
            M = m;
            YDCHuffman = huffmanYDC;
            YACHuffman = huffmanYAC;
            ChrDCHuffman = huffmanChrDC;
            ChrACHuffman = huffmanChrAC;

            _calculateCosineCoefficients();
        }

        private void _calculateCosineCoefficients() {
            for (int j = 0; j < 8; j++) {
                for (int i = 0; i < 8; i++) {
                    _cosinesCoefficients[j, i] = Math.Cos((2 * j + 1) * i * Math.PI / 16);
                }
            }
        }

        /// <summary>
        /// Used to encode a Bitmap as a JPEG along with a message
        /// </summary>
        public void Encode(byte[] message) {
            if (message.Length > 16884) {
                throw new ArgumentException("Message cannot be longer than 16884 bytes!");
            }
            _breakDownMessage(message);

            _jw = new JpegWriter();
            _writeHeaders();
            _writeScanData();
            _writeEndOfImage();
        }

        /// <summary>
        /// Saves an encoded jpeg image to a filesystem
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path) {
            if (_jw != null) {
                _jw.ToFile(path);
            } else {
                throw new Exception();
            }
        }

        private void _breakDownMessage(byte[] message) {
            List<byte> messageList = message.ToList();

            //Encode the message length in the 14 first bits and the value of M into the 15th and 16th bits
            //The M value is encoded using a temporary M-value of 4
            ushort len = (ushort)(message.Length << 2);
            switch (M) {
                case 2:
                    break;
                case 4:
                    len++;
                    break;
                case 16:
                    len += 2;
                    break;
                case 256:
                    len += 3;
                    break;
            }

            List<byte> metaDataList = new List<byte> {
                (byte) (len >> 8),
                (byte) (len & 0xFF)
            };

            //Split the metadata
            _splitMessageIntoSmallerComponents(metaDataList, 0x3, 2);

            //Split the message itself
            _splitMessageIntoSmallerComponents(messageList, (byte)(M - 1), (int)Math.Log(M, 2));
        }

        private void _splitMessageIntoSmallerComponents(List<byte> list, byte mask, int steps) {
            foreach (byte b in list) {
                //Each byte must be split into 8/log2(M) parts
                for (int i = (8 / steps) - 1; i >= 0; i--) {
                    //Save log2(M) bits at a time
                    byte toBeAdded = (byte)(b & (byte)(mask << (i * steps)));
                    toBeAdded >>= i * steps;
                    _message.Add(toBeAdded);
                }
            }
        }

        private void _writeHeaders() {
            _writeStartOfImage();
            _writeJFIFHeader();
            _writeQuantizationTables();
            _writeStartOfFrame();
            _writeHuffmanTables();
            _writeScanHeader();
        }

        private void _writeStartOfImage() {
            _jw.WriteBytes(0xff, 0xd8);
        }

        private void _writeJFIFHeader() {
            _jw.WriteBytes(0xff, 0xe0); //APP0 marker
            _jw.WriteBytes(0x00, 0x10); //Length of segment
            _jw.WriteBytes(0x4a, 0x46, 0x49, 0x46, 0x00); //Null terminated JFIF String
            _jw.WriteBytes(0x01, 0x02); //JFIF version 1.2
            _jw.WriteBytes(0x01); //Units (01 means dots/inch)
            _jw.WriteBytes(0x00, 0x60, 0x00, 0x60); //Xdensity, Ydensity (96x96 dots/inch) 2bytes pr density
            _jw.WriteBytes(0x00, 0x00); //No thumbnail
        }

        private void _writeQuantizationTables() {
            _writeQuantizationSegment(YQuantizationTable, 0x00);
            _writeQuantizationSegment(ChrQuantizationTable, 0x01);
        }

        private void _writeQuantizationSegment(QuantizationTable quantizationTable, byte id) {
            _jw.WriteBytes(0xff, 0xdb); //DQT header
            _jw.WriteBytes(0x00, 0x43); //Length of segment
            id = (byte)(id & 0x07);
            _jw.WriteBytes(id); //will output 0000 0xxx where the first nipple defines precission, and the second defines id
            _jw.WriteBytes(quantizationTable.ZigzagEntries);
        }

        private void _writeStartOfFrame() {
            _jw.WriteBytes(0xff, 0xc0); //SOF Marker
            _jw.WriteBytes(0x00, 0x11);
            _jw.WriteBytes(0x08); //Sample precision of 8 bit

            byte widthByteOne = (byte)(CoverImage.Width >> 8);
            byte widthByteTwo = (byte)(CoverImage.Width & 0xff);
            byte heightByteOne = (byte)(CoverImage.Height >> 8);
            byte heightByteTwo = (byte)(CoverImage.Height & 0xff);
            _jw.WriteBytes(heightByteOne, heightByteTwo, widthByteOne, widthByteTwo); //Width and height of image, each in two bytes
            _jw.WriteBytes(0x03); // number of components in image 

            _jw.WriteBytes(0x01); //Y-component
            _jw.WriteBytes(0x22); //2:1 sampling in both dimensions
            _jw.WriteBytes(0x00); //Uses qTable with id: 0

            _jw.WriteBytes(0x02); //Cb component
            _jw.WriteBytes(0x11);
            _jw.WriteBytes(0x01);

            _jw.WriteBytes(0x03); //Cr component
            _jw.WriteBytes(0x11);
            _jw.WriteBytes(0x01);
        }

        private void _writeHuffmanTables() {
            _writeHuffmanSegment(YDCHuffman, 0, true);
            _writeHuffmanSegment(YACHuffman, 0, false);
            _writeHuffmanSegment(ChrDCHuffman, 1, true);
            _writeHuffmanSegment(ChrACHuffman, 1, false);
        }

        private void _writeHuffmanSegment(HuffmanTable huffman, byte id, bool dc) {
            _jw.WriteBytes(0xff, 0xc4); //DHT marker

            ushort len = (ushort)(huffman.Elements.Count + huffman.Combinations().Length + 3);
            _jw.WriteBytes((byte)(len >> 8), (byte)(len & 0xff));

            byte combined;
            if (!dc) {
                combined = (byte)((1 << 4) + id);
            } else {
                combined = id;
            }

            _jw.WriteBytes(combined);

            _jw.WriteBytes(huffman.Combinations());


            HuffmanElement[] allElements = huffman.Elements.Values.OrderBy(x => x.Length).ThenBy(x => x.RunSize).ToArray();
            foreach (HuffmanElement huffmanElement in allElements) {
                _jw.WriteBytes(huffmanElement.RunSize);
            }
        }

        private void _writeScanHeader() {
            _jw.WriteBytes(0xff, 0xda); //SOS marker
            _jw.WriteBytes(0x00, 0x0c); //Length
            _jw.WriteBytes(0x03); //3 components
            _jw.WriteBytes(0x01, 0x00);
            _jw.WriteBytes(0x02, 0x11);
            _jw.WriteBytes(0x03, 0x11);
            _jw.WriteBytes(0x00, 0x3f, 0x00); //Used for progressive mode
        }

        private void _writeScanData() {
            _s.Restart();
            _padCoverImage();
            _s.Stop();
            _setTimings("_padCoImage", _s.ElapsedMilliseconds);
            _s.Restart();
            double[][,] channelValues = _splitToChannels(CoverImage);
            _s.Stop();
            _setTimings("_splitToChannels", _s.ElapsedMilliseconds);
            BitList bits = new BitList();

            _encodeMCU(bits, channelValues, CoverImage.Width, CoverImage.Height);
            _jw.WriteBytes(_flush(bits));
        }



        private static byte[] _flush(BitList bits) {
            while (bits.Count % 8 != 0) {
                bits.Add(false);
            }

            byte[] byteArray = new byte[(int)Math.Ceiling(bits.Count / 8.0)];
            int len = byteArray.Length;

            for (int i = 0; i < len; i++) {
                for (int j = 0; j < 8; j++) {
                    byteArray[i] = (byte)(byteArray[i] << 1);
                    if (i * 8 + j >= bits.Count) {
                        byteArray[i] = (byte)(byteArray[i] | 0x01);
                    } else {
                        byteArray[i] = (byte)(byteArray[i] | (bits[i * 8 + j] ? 1 : 0));
                    }
                }
            }
            return byteArray;
        }

        private void _encodeMCU(BitList bits, double[][,] YCbCrChannels, int imageWidth, int imageHeight) {
            if (_quantizedBlocks.Count == 0) {
                _quantizeValues(YCbCrChannels, imageWidth, imageHeight);
            }

            //Encode the secret message in the quantized blocks
            _encodeMessage();

            //This is where the data is huffman encoded and saved to the file
            _s.Restart();
            foreach (var quantizisedBlock in _quantizedBlocks) {
                HuffmanEncode(bits, quantizisedBlock.Item1, quantizisedBlock.Item2, quantizisedBlock.Item3, quantizisedBlock.Item4);
            }
            _s.Stop();
            _setTimings("_Huffmanencoding", _s.ElapsedMilliseconds);
        }

        private void _quantizeValues(double[][,] YCbCrChannels, int imageWidth, int imageHeight) {
            _s.Restart();

            double[][,] channels = new double[3][,];
            for (int i = 0; i < 3; i++) {
                channels[i] = new double[16, 16];
            }

            for (int MCUY = 0; MCUY < imageHeight; MCUY += 16) {
                for (int MCUX = 0; MCUX < imageWidth; MCUX += 16) {
                    for (int i = 0; i < 3; i++) {
                        for (int x = 0; x < 16; x++) {
                            for (int y = 0; y < 16; y++) {
                                channels[i][x, y] = YCbCrChannels[i][MCUX + x, MCUY + y];
                            }
                        }
                    }
                    _encodeBlocks(channels);
                }
            }

            int len = _quantizedBlocks.Count * 64;
            for (int i = 0; i < len; i++) {
                int array = i / 64;
                int xpos = i % 8;
                int ypos = (i % 64) / 8;

                if (xpos + ypos != 0 && _quantizedBlocks[array].Item1[xpos, ypos] != 0) {
                    _nonZeroValues.Add(_quantizedBlocks[array].Item1[xpos, ypos]);
                }
            }
            _s.Stop();
            _setTimings("_quantizeBlocks", _s.ElapsedMilliseconds);
        }

        private void _encodeBlocks(double[][,] MCU) {
            for (int i = 0; i < 4; i++) {
                double[,] YBlock = _block16ToBlock8(MCU[0], i);
                _encodeBlocksSubMethod(YBlock, YDCHuffman, YACHuffman, 0, YQuantizationTable);
            }

            double[,] CbDownSampled = _downSample(MCU[1]);
            _encodeBlocksSubMethod(CbDownSampled, ChrDCHuffman, ChrACHuffman, 1, ChrQuantizationTable);

            double[,] CrDownSampled = _downSample(MCU[2]);
            _encodeBlocksSubMethod(CrDownSampled, ChrDCHuffman, ChrACHuffman, 2, ChrQuantizationTable);
        }

        private void _encodeBlocksSubMethod(double[,] blocks, HuffmanTable DC, HuffmanTable AC, int index, QuantizationTable table) {
            blocks = _discreteCosineTransform(blocks);
            int[,] quantiziedBlock = _quantization(blocks, table);
            _quantizedBlocks.Add(new Tuple<int[,], HuffmanTable, HuffmanTable, int>(quantiziedBlock, DC, AC, index));
        }

        private int[,] _quantization(double[,] values, QuantizationTable qTable) {
            int[,] quantizedValues = new int[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    quantizedValues[i, j] = (int)(values[i, j] / qTable.Entries[j * 8 + i]);
                }
            }

            return quantizedValues;
        }

        private void _encodeMessage() {
            Graph graph = new Graph();

            //Add vertices for each part of the message
            _addVertices(graph);

            _s.Restart();
            List<Vertex> toBeChanged = graph.Vertices.Where(x => (x.SampleValue1 + x.SampleValue2).Mod(x.Modulo) != x.Message).ToList();
            int length = toBeChanged.Count;
            int threshold = 5;
            for (int i = 0; i < length; i++) {
                for (int j = i + 1; j < length; j++) {
                    _addEdge(true, true, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(true, false, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(false, true, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(false, false, toBeChanged[i], toBeChanged[j], threshold, graph);
                }
            }
            _s.Stop();
            _setTimings("Adding edges", _s.ElapsedMilliseconds);

            //Swap values and force the rest
            _refactorGraph(graph);

            //Put the changed values back into the QuantizedValues
            _mergeGraphAndQuantizedValues(graph);
        }

        private void _addVertices(Graph g) {
            for (int i = 0; i < 15; i += 2) {
                g.Vertices.Add(new Vertex(_nonZeroValues[i], _nonZeroValues[i + 1], _message[0], 4));
                _message.RemoveAt(0);
            }

            for (int i = 16; _message.Any(); i += 2) {
                g.Vertices.Add(new Vertex(_nonZeroValues[i], _nonZeroValues[i + 1], _message[0], M));
                _message.RemoveAt(0);
            }
        }
        
        private static void _addEdge(bool firstFirst, bool secondFirst, Vertex first, Vertex second, int threshold, Graph g) {
            int weight = Math.Abs((firstFirst ? first.SampleValue1 : first.SampleValue2) - (secondFirst ? second.SampleValue1 : second.SampleValue2));
            if (weight < threshold) {
                if (((firstFirst ? first.SampleValue2 : first.SampleValue1) + (secondFirst ? second.SampleValue1 : second.SampleValue2)).Mod(first.Modulo) == first.Message) {
                    if (((firstFirst ? first.SampleValue1 : first.SampleValue2) + (secondFirst ? second.SampleValue2 : second.SampleValue1)).Mod(second.Modulo) == second.Message) {
                        Edge e = new Edge(first, second, weight, firstFirst, secondFirst);
                        g.Edges.Add(e);
                    }
                }
            }
        }

        private void _refactorGraph(Graph graph) {
            _s.Restart();
            List<Edge> chosen = graph.GetSwitches();
            _s.Stop();
            _setTimings("GetSwitches", _s.ElapsedMilliseconds);

            foreach (Edge edge in chosen) {
                _swapVertexData(edge);
            }

            foreach (Vertex vertex in graph.Vertices) {
                if ((vertex.SampleValue1 + vertex.SampleValue2).Mod(vertex.Modulo) != vertex.Message) {
                    _forceSampleChange(vertex);
                }
            }
        }

        private static void _swapVertexData(Edge e) {
            int temp;
            if (e.VStartFirst) {
                if (e.VEndFirst) {
                    temp = e.VStart.SampleValue1;
                    e.VStart.SampleValue1 = e.VEnd.SampleValue1;
                    e.VEnd.SampleValue1 = temp;
                } else {
                    temp = e.VStart.SampleValue1;
                    e.VStart.SampleValue1 = e.VEnd.SampleValue2;
                    e.VEnd.SampleValue2 = temp;
                }
            } else {
                if (e.VEndFirst) {
                    temp = e.VStart.SampleValue2;
                    e.VStart.SampleValue2 = e.VEnd.SampleValue1;
                    e.VEnd.SampleValue1 = temp;
                } else {
                    temp = e.VStart.SampleValue2;
                    e.VStart.SampleValue2 = e.VEnd.SampleValue2;
                    e.VEnd.SampleValue2 = temp;
                }
            }
        }

        private static void _forceSampleChange(Vertex vertex) {
            int error = (vertex.SampleValue1 + vertex.SampleValue2).Mod(vertex.Modulo) - vertex.Message;

            if (vertex.SampleValue1 - error <= 127 && vertex.SampleValue1 - error >= -128 &&
                vertex.SampleValue1 - error != 0) {
                vertex.SampleValue1 -= error;
            } else if (vertex.SampleValue2 - error <= 127 && vertex.SampleValue2 - error >= -128 &&
                       vertex.SampleValue2 - error != 0) {
                vertex.SampleValue2 -= error;
            } else {
                vertex.SampleValue1 += vertex.Modulo - error;
            }
        }

        private void _mergeGraphAndQuantizedValues(Graph graph) {
            int vertexPos = 0;
            bool firstValue = true;
            int numberOfVertices = graph.Vertices.Count;
            int len = _quantizedBlocks.Count * 64;
            for (int i = 0; i < len; i++) {
                if (vertexPos >= numberOfVertices) {
                    break;
                }
                int array = i / 64;
                int xpos = i % 8;
                int ypos = (i % 64) / 8;

                if (xpos + ypos != 0 && _quantizedBlocks[array].Item1[xpos, ypos] != 0) {
                    if (firstValue) {
                        _quantizedBlocks[array].Item1[xpos, ypos] = graph.Vertices[vertexPos].SampleValue1;
                        firstValue = false;
                    } else {
                        _quantizedBlocks[array].Item1[xpos, ypos] = graph.Vertices[vertexPos].SampleValue2;
                        firstValue = true;
                        vertexPos++;
                    }
                }
            }
        }

        private double[,] _discreteCosineTransform(double[,] block8) {
            double[,] cosineValues = new double[8, 8];

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    double tempSum = 0.0;
                    double cCoefficient = _c(i, j);
                    for (int x = 0; x < 8; x++) {
                        for (int y = 0; y < 8; y++) {
                            tempSum += cCoefficient * block8[x, y] * _cosinesCoefficients[x, i] * _cosinesCoefficients[y, j];
                        }
                    }
                    cosineValues[i, j] = tempSum;
                }
            }
            return cosineValues;
        }

        private static double _c(int i, int j) {
            if (i == 0) {
                if (j == 0) {
                    return 0.125; // 1/8
                }
                return 0.17677; // 1/(4*sqrt(2))
            }
            if (j == 0) {
                return 0.17677; // 1/(4*sqrt(2))
            }
            return 0.25; // 1/4
        }

        private static double[,] _block16ToBlock8(double[,] block16, int index) {
            double[,] block8 = new double[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    block8[x, y] = block16[x + (index % 2) * 8, y + index / 2 * 8];
                }
            }
            return block8;
        }

        private static double[,] _downSample(double[,] values) {
            double[,] downSampled = new double[8, 8];
            for (int i = 0; i < 16; i += 2) {
                for (int j = 0; j < 16; j += 2) {
                    downSampled[j / 2, i / 2] = (values[j, i] + values[j + 1, i] + values[j, i + 1] + values[j + 1, i + 1]) / 4;
                }
            }
            return downSampled;
        }

        private static double[][,] _splitToChannels(Bitmap image) {
            double[][,] channels = new double[3][,];
            int imageWidth = image.Width, imageHeight = image.Height;
            for (int i = 0; i < 3; i++) {
                channels[i] = new double[image.Width, image.Height];
            }

            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.ReadWrite, image.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            int bytesPerPixel = bmpData.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;
            int imageSizeInBytes = bmpData.Stride * bmpData.Height;
            byte[] data = new byte[imageSizeInBytes];

            //Copy imageSizeInBytes bytes from bData into data
            System.Runtime.InteropServices.Marshal.Copy(ptr, data, 0, imageSizeInBytes);

            for (int i = 0; i < imageSizeInBytes - bytesPerPixel; i += bytesPerPixel) {
                int x = (i / bytesPerPixel) % imageWidth;
                int y = (i / bytesPerPixel) / imageWidth;

                byte r = data[i + 2];
                byte g = data[i + 1];
                byte b = data[i];

                channels[0][x, y] = 0.299 * r + 0.587 * g + 0.114 * b - 128;
                channels[1][x, y] = -0.168736 * r - 0.331264 * g + 0.5 * b;
                channels[2][x, y] = 0.5 * r - 0.418688 * g - 0.081312 * b;
            }

            image.UnlockBits(bmpData);

            return channels;
        }

        private void _padCoverImage() {
            int oldWidth = CoverImage.Width, oldHeight = CoverImage.Height;
            int newWidth = oldWidth, newHeight = oldHeight;

            if (oldWidth % 16 != 0) {
                newWidth = oldWidth + (16 - oldWidth % 16);
            }
            if (oldHeight % 16 != 0) {
                newHeight = oldHeight + (16 - oldHeight % 16);
            }

            if (newWidth == oldWidth && newHeight == oldHeight) {
                return;
            }

            Bitmap paddedCoverImage = _copyBitmap(CoverImage, newWidth, newHeight);

            for (int rows = 0; rows < oldHeight; rows++) {
                for (int extraPixelsX = oldWidth; extraPixelsX < newWidth; extraPixelsX++) {
                    paddedCoverImage.SetPixel(extraPixelsX, rows, CoverImage.GetPixel(oldWidth - 1, rows));
                }
            }

            for (int extraPixelsY = oldHeight; extraPixelsY < newHeight; extraPixelsY++) {
                for (int columns = 0; columns < oldWidth; columns++) {
                    paddedCoverImage.SetPixel(columns, extraPixelsY, CoverImage.GetPixel(columns, oldHeight - 1));
                }
            }

            for (int smallBoxY = oldHeight; smallBoxY < newHeight; smallBoxY++) {
                for (int smallBoxX = oldWidth; smallBoxX < newWidth; smallBoxX++) {
                    paddedCoverImage.SetPixel(smallBoxX, smallBoxY, CoverImage.GetPixel(oldWidth - 1, oldHeight - 1));
                }
            }

            CoverImage = paddedCoverImage;
        }

        private static Bitmap _copyBitmap(Bitmap bitmapIn, int width, int height) {
            Bitmap bitmapOut = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmapOut);
            Rectangle rect = new Rectangle(0, 0, bitmapIn.Width, bitmapIn.Height);
            g.DrawImage(bitmapIn, rect, rect, GraphicsUnit.Pixel);
            g.Dispose();

            return bitmapOut;
        }

        private void _writeEndOfImage() {
            _jw.WriteBytes(0xff, 0xd9);
        }

        private void HuffmanEncode(BitList bits, int[,] block8, HuffmanTable huffmanDC, HuffmanTable huffmanAC, int DCIndex) {
            short diff = (short)(block8[0, 0] - _lastDc[DCIndex]);
            _lastDc[DCIndex] += diff;

            if (diff != 0) {
                byte category = _bitCost(diff);
                HuffmanElement huffmanCode = huffmanDC.GetElementFromRunSize(0, category);
                _ushortToBits(bits, huffmanCode.CodeWord, huffmanCode.Length);

                _ushortToBits(bits, _numberEncoder(diff), category);
            } else {
                HuffmanElement EOB = huffmanDC.GetElementFromRunSize(0x00, 0x00);
                _ushortToBits(bits, EOB.CodeWord, EOB.Length);
            }

            int zeroesCounter = 0;
            for (int i = 1; i < 64; i++) {
                int x = QuantizationTable.RoadPoints[i, 0], y = QuantizationTable.RoadPoints[i, 1];
                if (block8[x, y] == 0) {
                    zeroesCounter++;
                    continue;
                }
                while (zeroesCounter >= 16) {
                    HuffmanElement ZRL = huffmanAC.GetElementFromRunSize(0x0F, 0x00);
                    _ushortToBits(bits, ZRL.CodeWord, ZRL.Length);
                    zeroesCounter -= 16;
                }

                byte cost = _bitCost((short)Math.Abs(block8[x, y]));
                HuffmanElement codeElement = huffmanAC.GetElementFromRunSize((byte)zeroesCounter, cost);
                zeroesCounter = 0;
                _ushortToBits(bits, codeElement.CodeWord, codeElement.Length);

                _ushortToBits(bits, _numberEncoder((short)block8[x, y]), cost);

            }

            if (zeroesCounter != 0) { //EOB
                HuffmanElement EOB = huffmanAC.GetElementFromRunSize(0x00, 0x00);
                _ushortToBits(bits, EOB.CodeWord, EOB.Length);
            }
        }

        private static void _ushortToBits(BitList bits, ushort number, byte length) {
            for (int i = 0; i < length; i++) {
                ushort dummy = 0x01;
                dummy = (ushort)(dummy << (length - i - 1));
                dummy = (ushort)(dummy & number);
                dummy = (ushort)(dummy >> (length - i - 1));
                bits.CheckedAdd((byte)dummy);
            }
        }

        private byte _bitCost(short number) {
            return (byte)Math.Ceiling(Math.Log(Math.Abs(number) + 1, 2));
        }


        private ushort _numberEncoder(short number) {
            return number < 0 ? (ushort)~Math.Abs(number) : (ushort)Math.Abs(number);
        }

        public int GetCapacity() {
            _padCoverImage();
            double[][,] YCbCrChannels = _splitToChannels(CoverImage);
            int imageHeight = CoverImage.Height;
            int imageWidth = CoverImage.Width;

            if (_quantizedBlocks.Count == 0) {
                _quantizeValues(YCbCrChannels, imageWidth, imageHeight);
            }

            //The amount of bytes can be calculated as follows:
            //Pairs available = nonZeroValues / 2
            //Bits per pair = Pairs / Math.Log(M, 2)
            //Total bytes available = bits per pair / 8
            return _nonZeroValues.Count / 2 / (int)Math.Log(M, 2) / 8;
        }
    }
}