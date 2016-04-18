﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Stegosaurus {
    public class JpegImage : IImageEncoder{
        private JpegWriter _jw;
        private int _m;
        private List<byte> _message = new List<byte>();
        private readonly double[,] _cosines = new double[8, 8];
        private readonly int[] _lastDc = { 0, 0, 0 };

        public Bitmap CoverImage { get; }

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
        public JpegImage(Bitmap coverImage, int quality, int m) :this(coverImage, quality, m, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        /// <param name="yTable"></param>
        /// <param name="chrTable"></param>
        public JpegImage(Bitmap coverImage, int quality, int m, QuantizationTable yTable, QuantizationTable chrTable) 
            : this (coverImage, quality, m, yTable, chrTable, HuffmanTable.JpegHuffmanTableYDC, HuffmanTable.JpegHuffmanTableYAC, HuffmanTable.JpegHuffmanTableChrDC, HuffmanTable.JpegHuffmanTableChrAC) { }

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
            :this(coverImage, quality, m, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable, huffmanYDC, huffmanYAC, huffmanChrDC, huffmanChrAC)
            {}

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
                    _cosines[j, i] = Math.Cos((2 * j + 1) * i * Math.PI / 16);
                }
            }
        }

        /// <summary>
        /// Used to encode a Bitmap as a JPEG along with a message
        /// </summary>
        public void Encode(byte[] message) {
            if (message.Length > 16884) {
                throw new ArgumentException("Message cannot be longer 16884 bytes!");
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
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            byte[] fileBytes = _jw.ToArray();

            foreach (byte fileByte in fileBytes) {
                fs.WriteByte(fileByte);
            }
        }

        private void _breakDownMessage(byte[] message) {

            //Encode the message length in the 14 first bits and the value of M into the 15th and 16th bits
            short len = (short)(message.Length << 2);
            switch (M) {
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
            
            _message.Add((byte)((len & 0xFF00) >> 8));
            _message.Add((byte)(len & 0xFF));

            byte mask = (byte) (M - 1);

            foreach (byte b in message) {
                //Each byte must be split into 8/log2(M) parts
                for (int i = 0; i < 8/Math.Log(M, 2); i++) {
                    //Save log2(M) bits at a time
                    _message.Add((byte)(b & (byte)(mask << i)));
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
            Bitmap paddedCoverImage = _padCoverImage();
            double[][,] channelValues = _splitToChannels(paddedCoverImage);
            List<byte> bits = new List<byte>();

            _encodeMCU(ref bits, channelValues, paddedCoverImage.Width, paddedCoverImage.Height);
            _jw.WriteBytes(_flush(bits));
            
        }

        private void _encodeMCU(ref List<byte> bits, double[][,] YCbCrChannels, int imageWidth, int imageHeight) {
            double[][,] channels = new double[3][,];
            for (int i = 0; i < 3; i++) {
                channels[i] = new double[16,16];
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
                    _encodeBlocks(ref bits, channels);
                }
            }
        }

        private void _encodeBlocks(ref List<byte> bits, double[][,] MCU) {
            for (int i = 0; i < 4; i++) {
                double[,] YBlock = _block16ToBlock8(MCU[0], i);
                _encodeBlocksSubMethod(ref bits, YBlock, YDCHuffman, YACHuffman, 0, YQuantizationTable);
            }

            double[,] CbDownSampled = _downSample(MCU[1]);
            _encodeBlocksSubMethod(ref bits, CbDownSampled, ChrDCHuffman, ChrACHuffman, 1, ChrQuantizationTable);

            double[,] CrDownSampled = _downSample(MCU[2]);
            _encodeBlocksSubMethod(ref bits, CrDownSampled, ChrDCHuffman, ChrACHuffman, 2, ChrQuantizationTable);
        }

        private void _encodeBlocksSubMethod(ref List<byte> bits, double[,] blocks, HuffmanTable DC, HuffmanTable AC, int index, QuantizationTable table) {
            blocks = _discreteCosineTransform(blocks);
            int[,] quantiziedBlock = _quantization(blocks, table);
            HuffmanEncode(ref bits, quantiziedBlock, DC, AC, index);
        }

        private byte[] _flush(List<byte> bits) {
            for (int i = 0; i < bits.Count / 8 - 1; i++) {
                if (bits[i * 8] == 1 && bits[i * 8 + 1] == 1 && bits[i * 8 + 2] == 1 && bits[i * 8 + 3] == 1 && bits[i * 8 + 4] == 1 && bits[i * 8 + 5] == 1 && bits[i * 8 + 6] == 1 && bits[i * 8 + 7] == 1) {
                    for (int j = 0; j < 8; j++) {
                        bits.Insert(i * 8 + 8, 0x00);
                    }
                }
            }

            byte[] byteArray = new byte[(int)Math.Ceiling(bits.Count / 8.0)];

            for (int i = 0; i < byteArray.Length; i++) {
                for (int j = 0; j < 8; j++) {
                    byteArray[i] = (byte)(byteArray[i] << 1);
                    if (i * 8 + j >= bits.Count) {
                        byteArray[i] = (byte)(byteArray[i] | 0x01);
                    } else {
                        byteArray[i] = (byte)(byteArray[i] | bits[i * 8 + j]);
                    }
                }
            }
            return byteArray;
        }


        private int[,] _quantization(double[,] values, QuantizationTable qTable) {
            int[,] quantizedValues = new int[8, 8];
            for (int i = 0; i < 8; i++) { 
                for (int j = 0; j < 8; j++) {
                    quantizedValues[i, j] = (int)(values[i, j] / qTable.Entries[j * 8 + i]);
                }
            }
            
            //Do graph things
            if (_message.Count != 0) {
                quantizedValues = _encodeData(quantizedValues);
            }

            return quantizedValues;
        }

        private int[,] _encodeData(int[,] qValues) {
            List<int> nonZeroValues = new List<int>();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (qValues[j, i] != 0 && j + i != 0) {
                        nonZeroValues.Add(qValues[j, i]);
                    }
                    Console.Write($"{qValues[j, i], 3}");
                }
                Console.WriteLine();
            }

            List<Tuple<int, int, byte>> pairs = new List<Tuple<int, int, byte>>();
            for (int i = 0; i < nonZeroValues.Count - 1; i += 2) {
                if (_message.Count != 0) {
                    pairs.Add(new Tuple<int, int, byte>(nonZeroValues[i], nonZeroValues[i + 1], _message[0]));
                    _message.RemoveAt(0);
                }
            }
        
            Graph graph = new Graph();

            foreach (Tuple<int, int, byte> pair in pairs) {
                if ((pair.Item1 + pair.Item2).Mod(M) != pair.Item3) {
                    graph.Vertices.Add(new Vertex(pair));
                } else {
                    Console.WriteLine("");
                }
            }
            Console.WriteLine(graph);

            //World's worst loops
            foreach (Vertex currentVertex in graph.Vertices) {
                foreach (Vertex otherVertex in graph.Vertices.Where(v => v != currentVertex)) {
                    if ((currentVertex.Value.Item2 + otherVertex.Value.Item1).Mod(M) == currentVertex.Value.Item3 &&
                        (currentVertex.Value.Item1 + otherVertex.Value.Item2).Mod(M) == otherVertex.Value.Item3) {
                        Edge e = new Edge(currentVertex, otherVertex, true, true);
                        currentVertex.Neighbours.Add(e);
                        otherVertex.Neighbours.Add(e);
                    }
                    if ((currentVertex.Value.Item2 + otherVertex.Value.Item2).Mod(M) == currentVertex.Value.Item3 &&
                        (currentVertex.Value.Item1 + otherVertex.Value.Item1).Mod(M) == otherVertex.Value.Item3) {
                        Edge e = new Edge(currentVertex, otherVertex, true, false);
                        currentVertex.Neighbours.Add(e);
                        otherVertex.Neighbours.Add(e);
                    }
                    if ((currentVertex.Value.Item1 + otherVertex.Value.Item2).Mod(M) == currentVertex.Value.Item3 &&
                        (currentVertex.Value.Item2 + otherVertex.Value.Item1).Mod(M) == otherVertex.Value.Item3) {
                        Edge e = new Edge(currentVertex, otherVertex, false, true);
                        currentVertex.Neighbours.Add(e);
                        otherVertex.Neighbours.Add(e);
                    }
                    if ((currentVertex.Value.Item1 + otherVertex.Value.Item1).Mod(M) == currentVertex.Value.Item3 &&
                        (currentVertex.Value.Item2 + otherVertex.Value.Item2).Mod(M) == otherVertex.Value.Item3) {
                        Edge e = new Edge(currentVertex, otherVertex, false, false);
                        currentVertex.Neighbours.Add(e);
                        otherVertex.Neighbours.Add(e);
                    }
                }
            }
            Console.ReadKey();

            return qValues;
        }

        private double[,] _discreteCosineTransform(double[,] block8) {
            double[,] cosineValues = new double[8, 8];

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    double tempSum = 0.0;
                    double cCoefficient = c(i, j);
                    for (int x = 0; x < 8; x++) {
                        for (int y = 0; y < 8; y++) {
                            tempSum += cCoefficient * block8[x, y] * _cosines[x, i] * _cosines[y, j];
                        }
                    }
                    cosineValues[i, j] = tempSum;
                }
            }
            return cosineValues;
        }

        private double c(int i, int j) {
            if (i == 0) {
                if (j == 0) {
                    return 0.125; // 1/8
                }
                return 0.17677; // 1/(4*sqrt(2))
            }
            if (j == 0) {
                return 0.17677;
            }
            return 0.25; // 1/4
        }

        private double[,] _block16ToBlock8(double[,] block16, int index) {
            double[,] block8 = new double[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    block8[x, y] = block16[x + (index % 2) * 8, y + index / 2 * 8];
                }
            }
            return block8;
        }

        private double[,] _downSample(double[,] values) {
            double[,] downSampled = new double[8,8];
            for (int i = 0; i < 16; i += 2) {
                for (int j = 0; j < 16; j += 2) {
                    downSampled[j / 2, i / 2] = (values[j, i] + values[j + 1, i] + values[j, i + 1] + values[j + 1, i + 1]) / 4;
                }
            }
            return downSampled;
        }

        private double[][,] _splitToChannels(Bitmap image) {
            double[][,] channels = new double[3][,];
            for (int i = 0; i < 3; i++) {
                channels[i] = new double[image.Width,image.Height];
            }
            for (int y = 0; y < image.Height; y++) {
                for (int x = 0; x < image.Width; x++) {
                    Color pixel = image.GetPixel(x, y);
                    channels[0][x, y] = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B - 128;
                    channels[1][x, y] = -0.168736 * pixel.R - 0.331264 * pixel.G + 0.5 * pixel.B;
                    channels[2][x, y] = 0.5 * pixel.R - 0.418688 * pixel.G - 0.081312 * pixel.B;
                }
            }

            return channels;
        }

        private Bitmap _padCoverImage() {
            int newWidth = CoverImage.Width, newHeight = CoverImage.Height;
            if (CoverImage.Width % 16 != 0) {
                newWidth = CoverImage.Width + (16 - CoverImage.Width % 16);
            }
            if (CoverImage.Height % 16 != 0) {
                newHeight = CoverImage.Height + (16 - CoverImage.Height % 16);
            }
            Bitmap paddedCoverImage = new Bitmap(newWidth, newHeight);
            for (int coverHeight = 0; coverHeight < CoverImage.Height; coverHeight++) { //Copy all of cover image to paddedimage
                for (int coverWidth = 0; coverWidth < CoverImage.Width; coverWidth++) {
                    paddedCoverImage.SetPixel(coverWidth, coverHeight, CoverImage.GetPixel(coverWidth, coverHeight));
                }
            }
            for (int rows = 0; rows < CoverImage.Height; rows++) {
                for (int extraPixelsX = CoverImage.Width; extraPixelsX < paddedCoverImage.Width; extraPixelsX++) {
                    paddedCoverImage.SetPixel(extraPixelsX, rows, CoverImage.GetPixel(CoverImage.Width - 1, rows));
                }
            }

            for (int extraPixelsY = CoverImage.Height; extraPixelsY < paddedCoverImage.Height; extraPixelsY++) {
                for (int columns = 0; columns < CoverImage.Width; columns++) {
                    paddedCoverImage.SetPixel(columns, extraPixelsY, CoverImage.GetPixel(columns, CoverImage.Height - 1));
                }
            }

            for (int smallBoxY = CoverImage.Height; smallBoxY < paddedCoverImage.Height; smallBoxY++) {
                for (int smallBoxX = CoverImage.Width; smallBoxX < paddedCoverImage.Width; smallBoxX++) {
                    paddedCoverImage.SetPixel(smallBoxX, smallBoxY, CoverImage.GetPixel(CoverImage.Width - 1, CoverImage.Height - 1));
                }
            }

            return paddedCoverImage;
        }

        private void _writeEndOfImage() {
            _jw.WriteBytes(0xff, 0xd9);
        }
        
        private void HuffmanEncode(ref List<byte> bits, int[,] block8, HuffmanTable huffmanDC, HuffmanTable huffmanAC, int DCIndex) {
            short diff = (short)(block8[0, 0] - _lastDc[DCIndex]);
            _lastDc[DCIndex] += diff;
            if (diff != 0) {
                byte category = _bitCost(diff);
                HuffmanElement huffmanCode = huffmanDC.GetElementFromRunSize(0, category);
                ushortToBits(ref bits, huffmanCode.CodeWord, huffmanCode.Length);

                ushortToBits(ref bits, _numberEncoder(diff), category);
            } else {
                HuffmanElement EOB = huffmanDC.GetElementFromRunSize(0x00, 0x00);
                ushortToBits(ref bits, EOB.CodeWord, EOB.Length);
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
                    ushortToBits(ref bits, ZRL.CodeWord, ZRL.Length);
                    zeroesCounter -= 16;
                }

                byte cost = _bitCost((short)Math.Abs(block8[x, y]));
                HuffmanElement codeElement = huffmanAC.GetElementFromRunSize((byte)zeroesCounter, cost);
                zeroesCounter = 0;
                ushortToBits(ref bits, codeElement.CodeWord, codeElement.Length);

                ushortToBits(ref bits, _numberEncoder((short)block8[x, y]), cost);

            }

            if (zeroesCounter != 0) { //EOB
                HuffmanElement EOB = huffmanAC.GetElementFromRunSize(0x00, 0x00);
                ushortToBits(ref bits, EOB.CodeWord, EOB.Length);
            }
        }

        private void ushortToBits(ref List<byte> bits, ushort number, byte length) {
            for (int i = 0; i < length; i++) {
                ushort dummy = 0x01;
                dummy = (ushort)(dummy << (length - i - 1));
                dummy = (ushort)(dummy & number);
                dummy = (ushort)(dummy >> (length - i - 1));
                bits.Add((byte)dummy);
            }
        }

        private byte _bitCost(short number) {
            return (byte)Math.Ceiling(Math.Log(Math.Abs(number) + 1, 2));
        }


        private ushort _numberEncoder(short number) {
            return (number < 0) ? (ushort)(~Math.Abs(number)) : (ushort)Math.Abs(number);
        }
        

        public int GetCapacity() {
            throw new NotImplementedException();
        }
    }
}