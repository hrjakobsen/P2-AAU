using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class JpegImage:IImageEncoder {
        private JpegWriter _jw;
        private byte _m;
        public readonly Bitmap CoverImage;
        private readonly List<byte> _message = new List<byte>();
        private readonly int[] _lastDc = { 0, 0, 0 };
        private readonly List<Tuple<short[,], HuffmanTable, HuffmanTable, int>> _quantizedBlocks = new List<Tuple<short[,], HuffmanTable, HuffmanTable, int>>();
        private readonly List<short> _nonZeroValues = new List<short>();
        private static readonly float[,] CosinesCoefficients = new float[8, 8];

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

        /// <summary>
        /// The M value is used in the encoding of the message. It can be set at 2, 4 or 16.
        /// A higher number means that more data can be hidden in the image, but a lower one 
        /// means that the data is better hidden.
        /// </summary>
        public byte M {
            get { return _m; }
            set {
                switch (value) {
                    case 2:
                    case 4:
                    case 16:
                        _m = value;
                        break;
                    default:
                        throw new ArgumentException("M must 2, 4 or 16!");
                }
            }
        }

        /// <summary>
        /// A constructor
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        public JpegImage(Bitmap coverImage, int quality, byte m) : this(coverImage, quality, m, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coverImage"></param>
        /// <param name="quality"></param>
        /// <param name="m"></param>
        /// <param name="yTable"></param>
        /// <param name="chrTable"></param>
        public JpegImage(Bitmap coverImage, int quality, byte m, QuantizationTable yTable, QuantizationTable chrTable)
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
        public JpegImage(Bitmap coverImage, int quality, byte m, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC)
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
        public JpegImage(Bitmap coverImage, int quality, byte m, QuantizationTable yTable, QuantizationTable chrTable, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC) {
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

            // Calculate coefficients that are used in DCT
            _calculateCosineCoefficients();
        }

        /// <summary>
        /// Used to encode a Bitmap as a JPEG along with a message
        /// </summary>
        public void Encode(byte[] message) {
            //Instantiate a JpegWriter, which has an internal list of bytes 
            //and the logic required to write those to a file
            _jw = new JpegWriter();

            if (message.Length > 16884) {
                throw new ArgumentException("Message cannot be longer than 16884 bytes!");
            }
            _breakDownMessage(message);

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

        private static void _calculateCosineCoefficients() {
            for (int j = 0; j < 8; j++) {
                for (int i = 0; i < 8; i++) {
                    CosinesCoefficients[j, i] = (float)Math.Cos((2 * j + 1) * i * Math.PI / 16);
                }
            }
        }

        public int GetCapacity() {
            //Basically perform all the steps Encode does up until
            //actually encoding the secret message
            Bitmap paddedCoverImage = _padCoverImage();
            sbyte[][,] YCbCrChannels = _splitToChannels(paddedCoverImage);
            int imageHeight = paddedCoverImage.Height;
            int imageWidth = paddedCoverImage.Width;

            //If the image has already been quantized we do not want
            //to do it again
            if (_quantizedBlocks.Count == 0) {
                _encodeAndQuantizeValues(YCbCrChannels, imageWidth, imageHeight);
            }

            //The amount of bytes can be calculated as follows:
            //Pairs available = nonZeroValues / 2
            //Bits per pair = Pairs / 8 / Math.Log(M, 2)
            //Total bytes available = bits per pair / 8
            return _nonZeroValues.Count / 2 / (8 / (int)Math.Log(M, 2)) / 8;
        }

        private void _breakDownMessage(byte[] message) {
            List<byte> messageList = message.ToList();

            //Encode the message length in the 14 first bits and the
            //value of M into the 15th and 16th bits. These two bytes
            //of metadata are encoded using a temporary M -value of 4
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
            //Start of Image
            _jw.WriteBytes(0xff, 0xd8);
        }

        private void _writeJFIFHeader() {
            //APP0 marker
            _jw.WriteBytes(0xff, 0xe0);

            //Length of segment
            _jw.WriteBytes(0x00, 0x10);

            //String "JFIF\0"
            _jw.WriteBytes(0x4a, 0x46, 0x49, 0x46, 0x00);

            //JFIF version 1.2
            _jw.WriteBytes(0x01, 0x02);

            //Units (01 is dots/inch)
            _jw.WriteBytes(0x01);

            //Xdensity, Ydensity (96x96 dots/inch) 2bytes pr density
            _jw.WriteBytes(0x00, 0x60, 0x00, 0x60);

            //No Thumbnail
            _jw.WriteBytes(0x00, 0x00);
        }

        private void _writeQuantizationTables() {
            //Luminance QTable
            _writeQuantizationSegment(YQuantizationTable, 0x00);

            //Chroma QTable
            _writeQuantizationSegment(ChrQuantizationTable, 0x01);
        }

        private void _writeQuantizationSegment(QuantizationTable quantizationTable, byte id) {
            //DQT header
            _jw.WriteBytes(0xff, 0xdb);

            //Length of segment
            _jw.WriteBytes(0x00, 0x43);

            //0000 0xxx where the first nipple defines precision and the second defines ID
            id = (byte)(id & 0x07);
            _jw.WriteBytes(id);

            //The QTable itself. In zig-zag format
            _jw.WriteBytes(quantizationTable.ZigzagEntries);
        }

        private void _writeStartOfFrame() {
            //SOF Marker
            _jw.WriteBytes(0xff, 0xc0);

            //Segment length
            _jw.WriteBytes(0x00, 0x11);

            //Sample precision of 8 bit
            _jw.WriteBytes(0x08);

            //Width and height of image, each in two bytes
            byte widthByteOne = (byte)(CoverImage.Width >> 8);
            byte widthByteTwo = (byte)(CoverImage.Width & 0xff);
            byte heightByteOne = (byte)(CoverImage.Height >> 8);
            byte heightByteTwo = (byte)(CoverImage.Height & 0xff);
            _jw.WriteBytes(heightByteOne, heightByteTwo, widthByteOne, widthByteTwo);

            //Number of components in image 
            _jw.WriteBytes(0x03);

            //Y-component
            //2:1 sampling in both dimensions
            //Use QTable with ID 0
            _jw.WriteBytes(0x01);
            _jw.WriteBytes(0x22);
            _jw.WriteBytes(0x00);

            //Cb component
            //1:1 sampling in both dimensions
            //Use QTable with ID 1
            _jw.WriteBytes(0x02);
            _jw.WriteBytes(0x11);
            _jw.WriteBytes(0x01);

            //Cr component
            //1:1 sampling in both dimensions
            //Use QTable with ID 1
            _jw.WriteBytes(0x03);
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
            //DHT marker
            _jw.WriteBytes(0xff, 0xc4);

            //Size of Huffman table in bytes. Calculated as the number of elements +
            //the number of combinations + three bytes. Two of them for the length 
            //itself and the last is for whether it is a DC or not and the ID.
            ushort len = (ushort)(huffman.Elements.Count + huffman.Combinations().Length + 3);
            _jw.WriteBytes((byte)(len >> 8), (byte)(len & 0xff));

            byte combined;
            if (!dc) {
                combined = (byte)((1 << 4) + id);
            } else {
                combined = id;
            }

            //DC/AC and ID
            _jw.WriteBytes(combined);

            //The table combinations
            _jw.WriteBytes(huffman.Combinations());

            //Write the elements
            HuffmanElement[] allElements = huffman.Elements.Values.OrderBy(x => x.Length).ThenBy(x => x.RunSize).ToArray();
            foreach (HuffmanElement huffmanElement in allElements) {
                _jw.WriteBytes(huffmanElement.RunSize);
            }
        }

        private void _writeScanHeader() {
            //SOS marker
            _jw.WriteBytes(0xff, 0xda);

            //Length of scan (always 12)
            _jw.WriteBytes(0x00, 0x0c);

            //Number of components
            _jw.WriteBytes(0x03);

            //First byte: Component ID (Y)
            //Second byte: AC HTable ID and DC HTable ID
            _jw.WriteBytes(0x01, 0x00);

            //First byte: Component ID (Cb)
            //Second byte: AC HTable ID and DC HTable ID
            _jw.WriteBytes(0x02, 0x11);

            //First byte: Component ID (Cr)
            //Second byte: AC HTable ID and DC HTable ID
            _jw.WriteBytes(0x03, 0x11);

            //Used for progressive mode
            _jw.WriteBytes(0x00, 0x3f, 0x00);
        }

        private void _writeScanData() {
            _s.Restart();
            Bitmap paddedCoverImage = _padCoverImage();
            _s.Stop();
            _setTimings("_padCoverImage", _s.ElapsedMilliseconds);

            _s.Restart();
            sbyte[][,] channelValues = _splitToChannels(paddedCoverImage);
            _s.Stop();
            _setTimings("_splitToChannels", _s.ElapsedMilliseconds);

            BitList bits = new BitList();
            _encodeMCU(bits, channelValues, paddedCoverImage.Width, paddedCoverImage.Height);
            _jw.WriteBytes(_flush(bits));
        }

        private Bitmap _padCoverImage() {
            int oldWidth = CoverImage.Width, oldHeight = CoverImage.Height;
            int newWidth = oldWidth, newHeight = oldHeight;

            //Is the width or height of the image is not divisible by 16
            //calculate a new width or height
            if (oldWidth % 16 != 0) {
                newWidth = oldWidth + (16 - oldWidth % 16);
            }
            if (oldHeight % 16 != 0) {
                newHeight = oldHeight + (16 - oldHeight % 16);
            }

            if (newWidth == oldWidth && newHeight == oldHeight) {
                return CoverImage;
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

            //Clean up after ourselves
            CoverImage.Dispose();
            return paddedCoverImage;
        }

        private static Bitmap _copyBitmap(Bitmap bitmapIn, int width, int height) {
            Bitmap bitmapOut = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bitmapOut);
            Rectangle rect = new Rectangle(0, 0, bitmapIn.Width, bitmapIn.Height);
            g.DrawImage(bitmapIn, rect, rect, GraphicsUnit.Pixel);
            g.Dispose();

            return bitmapOut;
        }

        private static sbyte[][,] _splitToChannels(Bitmap image) {
            int imageWidth = image.Width, imageHeight = image.Height;
            sbyte[][,] channels = {
                new sbyte[imageWidth, imageHeight],
                new sbyte[imageWidth, imageHeight],
                new sbyte[imageWidth, imageHeight]
            };

            //Lock the bits of the bitmap into memory
            BitmapData bmpData = image.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.ReadWrite, image.PixelFormat);

            //Get the address of the first pixel
            IntPtr ptr = bmpData.Scan0;

            //Declare an array to hold the bitmap's bytes
            int imageSizeInBytes = bmpData.Stride * bmpData.Height;
            byte[] data = new byte[imageSizeInBytes];

            int bytesPerPixel = bmpData.PixelFormat == PixelFormat.Format24bppRgb ? 3 : 4;

            //Copy the bitmap's bytes into our array
            Marshal.Copy(ptr, data, 0, imageSizeInBytes);

            //We jump bytesPerPixel with every iteration
            int len = imageSizeInBytes / bytesPerPixel;
            Parallel.For(0, len, index => {
                int i = index * bytesPerPixel;
                int x = (i / bytesPerPixel) % imageWidth;
                int y = (i / bytesPerPixel) / imageWidth;

                byte r = data[i + 2];
                byte g = data[i + 1];
                byte b = data[i];

                channels[0][x, y] = (sbyte)(0.299 * r + 0.587 * g + 0.114 * b - 128);
                channels[1][x, y] = (sbyte)(-0.168736 * r - 0.331264 * g + 0.5 * b);
                channels[2][x, y] = (sbyte)(0.5 * r - 0.418688 * g - 0.081312 * b);
            });

            //Clean up after ourselves
            image.UnlockBits(bmpData);
            return channels;
        }

        private void _encodeMCU(BitList bits, sbyte[][,] YCbCrChannels, int imageWidth, int imageHeight) {
            if (_quantizedBlocks.Count == 0) {
                _encodeAndQuantizeValues(YCbCrChannels, imageWidth, imageHeight);
            }

            //Encode the secret message in the quantized blocks
            _encodeMessage();

            //Huffman encode the data and save it to the file
            _s.Restart();
            foreach (var quantizisedBlock in _quantizedBlocks) {
                HuffmanEncode(bits, quantizisedBlock.Item1, quantizisedBlock.Item2, quantizisedBlock.Item3, quantizisedBlock.Item4);
            }
            _s.Stop();
            _setTimings("_Huffmanencoding", _s.ElapsedMilliseconds);
        }

        private void _encodeAndQuantizeValues(sbyte[][,] YCbCrChannels, int imageWidth, int imageHeight) {
            _s.Restart();

            float[][,] channels = {
                new float[16, 16],
                new float[16, 16],
                new float[16, 16]
            };

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

        private void _encodeBlocks(float[][,] MCU) {
            Tuple<short[,], HuffmanTable, HuffmanTable, int>[] temp = new Tuple<short[,], HuffmanTable, HuffmanTable, int>[6];

            Parallel.For(0, 6, i => {
                switch (i) {
                    case 4:
                        float[,] CbDownSampled = _downSample(MCU[1]);
                        temp[i] = new Tuple<short[,], HuffmanTable, HuffmanTable, int>(_encodeBlocksSubMethod(CbDownSampled, ChrQuantizationTable), ChrDCHuffman, ChrACHuffman, 1);
                        break;
                    case 5:
                        float[,] CrDownSampled = _downSample(MCU[2]);
                        temp[i] = new Tuple<short[,], HuffmanTable, HuffmanTable, int>(_encodeBlocksSubMethod(CrDownSampled, ChrQuantizationTable), ChrDCHuffman, ChrACHuffman, 2);
                        break;
                    default:
                        float[,] YBlock = _block16ToBlock8(MCU[0], i);
                        temp[i] = new Tuple<short[,], HuffmanTable, HuffmanTable, int>(_encodeBlocksSubMethod(YBlock, YQuantizationTable), YDCHuffman, YACHuffman, 0);
                        break;
                }
            });

            for (int i = 0; i < 6; i++) {
                _quantizedBlocks.Add(temp[i]);
            }
        }

        private static float[,] _downSample(float[,] values) {
            float[,] downSampled = new float[8, 8];
            for (int i = 0; i < 16; i += 2) {
                for (int j = 0; j < 16; j += 2) {
                    downSampled[j / 2, i / 2] = (values[j, i] + values[j + 1, i] + values[j, i + 1] + values[j + 1, i + 1]) / 4;
                }
            }
            return downSampled;
        }

        private static float[,] _block16ToBlock8(float[,] block16, int index) {
            int indexMod = index % 2 * 8;
            int indexHalf = index / 2 * 8;
            float[,] block8 = new float[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    block8[x, y] = block16[x + indexMod, y + indexHalf];
                }
            }
            return block8;
        }

        private static short[,] _encodeBlocksSubMethod(float[,] blocks, QuantizationTable table) {
            blocks = _discreteCosineTransform(blocks);
            return _quantization(blocks, table);
        }

        private static float[,] _discreteCosineTransform(float[,] block8) {
            float[,] cosineValues = new float[8, 8];

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    float tempSum = 0.0f;
                    float cCoefficient = _c(i, j);
                    for (int x = 0; x < 8; x++) {
                        for (int y = 0; y < 8; y++) {
                            tempSum += cCoefficient * block8[x, y] * CosinesCoefficients[x, i] * CosinesCoefficients[y, j];
                        }
                    }
                    cosineValues[i, j] = tempSum;
                }
            }
            return cosineValues;
        }

        private static float _c(int i, int j) {
            if (i == 0) {
                if (j == 0) {
                    return 0.125f; // 1/8
                }
                return 0.17677f; // 1/(4*sqrt(2))
            }
            if (j == 0) {
                return 0.17677f; // 1/(4*sqrt(2))
            }
            return 0.25f; // 1/4
        }

        private static short[,] _quantization(float[,] values, QuantizationTable qTable) {
            short[,] quantizedValues = new short[8, 8];
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    quantizedValues[i, j] = (short)(values[i, j] / qTable.Entries[j * 8 + i]);
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
            Parallel.For(0, length, i => {
                for (int j = i + 1; j < length; j++) {
                    _addEdge(true, true, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(true, false, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(false, true, toBeChanged[i], toBeChanged[j], threshold, graph);
                    _addEdge(false, false, toBeChanged[i], toBeChanged[j], threshold, graph);
                }
            });
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

        private static void _addEdge(bool startFirst, bool endFirst, Vertex first, Vertex second, int threshold, Graph g) {
            short weight = (short)Math.Abs((startFirst ? first.SampleValue1 : first.SampleValue2) - (endFirst ? second.SampleValue1 : second.SampleValue2));
            if (weight < threshold) {
                if (((startFirst ? first.SampleValue2 : first.SampleValue1) + (endFirst ? second.SampleValue1 : second.SampleValue2)).Mod(first.Modulo) == first.Message) {
                    if (((startFirst ? first.SampleValue1 : first.SampleValue2) + (endFirst ? second.SampleValue2 : second.SampleValue1)).Mod(second.Modulo) == second.Message) {
                        lock (g) {
                            g.Edges.Add(new Edge(first, second, weight, startFirst, endFirst));
                        }
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
            short temp;
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
            short error = (short)((vertex.SampleValue1 + vertex.SampleValue2).Mod(vertex.Modulo) - vertex.Message);

            if (vertex.SampleValue1 - error <= 127 && vertex.SampleValue1 - error >= -128 &&
                vertex.SampleValue1 - error != 0) {
                vertex.SampleValue1 -= error;
            } else if (vertex.SampleValue2 - error <= 127 && vertex.SampleValue2 - error >= -128 &&
                       vertex.SampleValue2 - error != 0) {
                vertex.SampleValue2 -= error;
            } else {
                vertex.SampleValue1 += (short)(vertex.Modulo - error);
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

        private void HuffmanEncode(BitList bits, short[,] block8, HuffmanTable huffmanDC, HuffmanTable huffmanAC, int DCIndex) {
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

                byte cost = _bitCost(Math.Abs(block8[x, y]));
                HuffmanElement codeElement = huffmanAC.GetElementFromRunSize((byte)zeroesCounter, cost);
                zeroesCounter = 0;
                _ushortToBits(bits, codeElement.CodeWord, codeElement.Length);

                _ushortToBits(bits, _numberEncoder(block8[x, y]), cost);
            }

            if (zeroesCounter != 0) { //EOB
                HuffmanElement EOB = huffmanAC.GetElementFromRunSize(0x00, 0x00);
                _ushortToBits(bits, EOB.CodeWord, EOB.Length);
            }
        }

        private static byte _bitCost(short number) {
            return (byte)Math.Ceiling(Math.Log(Math.Abs(number) + 1, 2));
        }

        private static void _ushortToBits(BitList bits, ushort number, byte length) {
            for (int i = 0; i < length; i++) {
                ushort dummy = 0x01;
                dummy = (ushort)(dummy << (length - i - 1));
                dummy = (ushort)(dummy & number);
                dummy = (ushort)(dummy >> (length - i - 1));
                bits.CheckedAdd(dummy);
            }
        }

        private static ushort _numberEncoder(short number) {
            return number < 0 ? (ushort)~Math.Abs(number) : (ushort)Math.Abs(number);
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
        private void _writeEndOfImage() {
            _jw.WriteBytes(0xff, 0xd9);
        }
    }
}