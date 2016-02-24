using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace JpegFromBook {
    class JPEG {
        private int[,] _roadPoints = {
            {0, 0}, {1, 0}, {0, 1}, {0, 2}, {1, 1}, {2, 0}, {3, 0}, {2, 1}, {1, 2}, {0, 3},
            {0, 4}, {1, 3}, {2, 2}, {3, 1}, {4, 0}, {5, 0}, {4, 1}, {3, 2}, {2, 3}, {1, 4}, {0, 5}, {0, 6}, {1, 5},
            {2, 4}, {3, 3}, {4, 2}, {5, 1}, {6, 0}, {7, 0}, {6, 1}, {5, 2}, {4, 3}, {3, 4}, {2, 5}, {1, 6}, {0, 7},
            {1, 7}, {2, 6}, {3, 5}, {4, 4}, {5, 3}, {6, 2}, {7, 1}, {7, 2}, {6, 3}, {5, 4}, {4, 5}, {3, 6}, {2, 7},
            {3, 7}, {4, 6}, {5, 5}, {6, 4}, {7, 3}, {7, 4}, {6, 5}, {5, 6}, {4, 7}, {5, 7}, {6, 6}, {7, 5}, {7, 6},
            {6, 7}, {7, 7}
        };

        private double[,] cosines = new double[8,8];

        private Bitmap _sourceBitmap;
        private FileStream _fileStream;
        private short[] LastDC = {0, 0, 0};

        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        private double[][,] channels = new double[3][,];

        public JPEG(Bitmap bmp) {
            _sourceBitmap = bmp;
            Width = (ushort)(_sourceBitmap.Width);
            Height = (ushort)(_sourceBitmap.Height);
            bmpToChannels();

        }


        private void calculateCosineCoefficients() {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    cosines[j, i] = Math.Cos((2*j + 1)*i*Math.PI/16);
                }
            }
        }
        
        public void encode(string target) {

            if (string.IsNullOrEmpty(target)) {
                throw new ArgumentNullException();
            }
            _fileStream = new FileStream(target, FileMode.Create, FileAccess.Write);
            
            calculateCosineCoefficients();
            
            addStartOfImage();
            addJFIFHeader();
            addQuantizationTable(DefaultTables.QuantizationTableY, 0x00);
            addQuantizationTable(DefaultTables.QuantizationTableChr, 0x01);
            addFrameHeader();
            addHuffmanTable(DefaultTables.HuffmanYDC, 0, true);
            addHuffmanTable(DefaultTables.HuffmanYAC, 0, false);
            addHuffmanTable(DefaultTables.HuffmanChrDC, 1, true);
            addHuffmanTable(DefaultTables.HuffmanChrAC, 1, false);
            addScanHeader();

            writeBytesToStream(encodeImage());

            addEndOfImage();


        }

        private void writeBytesToStream(params byte[] bytes) {
            foreach (byte _byte in bytes) {
                _fileStream.WriteByte(_byte);
            }
        }

        private void addStartOfImage() {
            writeBytesToStream(0xff, 0xd8);

        }

        private void addJFIFHeader() {
            writeBytesToStream(0xff, 0xe0); //APP0 marker
            writeBytesToStream(0x00, 0x10); //Length of segment
            writeBytesToStream(0x4a, 0x46, 0x49, 0x46, 0x00); //Null terminated JFIF String
            writeBytesToStream(0x01, 0x02); //JFIF version 1.2
            writeBytesToStream(0x01); //Units (01 means dots/inch)
            writeBytesToStream(0x00, 0x60, 0x00, 0x60); //Xdensity, Ydensity (96x96 dots/inch) 2bytes pr density
            writeBytesToStream(0x00, 0x00); //No thumbnail
        }

        private void addQuantizationTable(byte[] quantizationTable, byte id) {
            writeBytesToStream(0xff, 0xdb); //DQT header
            writeBytesToStream(0x00, 0x43); //Length of segment
            writeBytesToStream(id); //will output 0000 0xxx where the first nipple defines precission, and the second defines id
            writeBytesToStream(quantizationTable);
        }

        private void addFrameHeader() {
            writeBytesToStream(0xff, 0xc0); //SOF Marker
            writeBytesToStream(0x00, 0x11);
            writeBytesToStream(0x08); //Sample precision of 8 bit

            byte WidthByteOne = (byte)((Width / 16 * 16) >> 8);
            byte WidthByteTwo = (byte)((Width / 16 * 16) & 0xff);
            byte HeightByteOne = (byte)((Height / 16 * 16) >> 8);
            byte HeightByteTwo = (byte)((Height / 16 * 16) & 0xff);
            writeBytesToStream(HeightByteOne, HeightByteTwo, WidthByteOne, WidthByteTwo); //Width and height of image, each in two bytes
            writeBytesToStream(0x03); // number of components in image 

            writeBytesToStream(0x01); //Y-component
            writeBytesToStream(0x22); //2:1 sampling in both dimensions
            writeBytesToStream(0x00); //Uses qTable with id: 0

            writeBytesToStream(0x02); //Cb component
            writeBytesToStream(0x11);
            writeBytesToStream(0x01);

            writeBytesToStream(0x03); //Cr component
            writeBytesToStream(0x11);
            writeBytesToStream(0x01);
        }

        private void addHuffmanTable(HuffmanTable huffman, byte id, bool dc) {
            writeBytesToStream(0xff, 0xc4); //DHT marker
            ushort len = (ushort)(huffman.combinationsInTable + 19);
            writeBytesToStream((byte)(len >> 8), (byte)(len & 0xff));
            byte combined;
            if (!dc) {
                combined = (byte)((1 << 4) + id);
            } else {
                combined = id;
            }
            writeBytesToStream(combined);

            for (int i = 0; i < 16; i++) {
                writeBytesToStream(huffman.bitLengths[i]);
            }

            for (int i = 0; i < 16; i++) {
                for (int j = 0; j < huffman.combinationsInTable; j++) {
                    if (huffman.elements[j].length == i + 1) {
                        writeBytesToStream(huffman.elements[j].runSize);
                    }
                }
            }

        }

        private void addScanHeader() {
            writeBytesToStream(0xff, 0xda); //SOS marker
            writeBytesToStream(0x00, 0x0c); //Length
            writeBytesToStream(0x03); //3 components
            writeBytesToStream(0x01, 0x00);
            writeBytesToStream(0x02, 0x11);
            writeBytesToStream(0x03, 0x11);
            writeBytesToStream(0x00, 0x3f, 0x00); //Used for progressive mode
        }

        private void addEndOfImage() {
            writeBytesToStream(0xff, 0xd9);
            _fileStream.Close();
        }

        private byte[] encodeImage() {
            List<byte> bits = new List<byte>();

            for (int chunkY = 0; chunkY < Height / 16; chunkY++) {
                for (int chunkX = 0; chunkX < Width / 16; chunkX++) {
                    double[][,] chunkChannels = new double[3][,];
                    for (int i = 0; i < 3; i++) {
                        chunkChannels[i] = new double[16, 16];
                        for (int y = 0; y < 16; y++) {
                            for (int x = 0; x < 16; x++) {
                                chunkChannels[i][x, y] = channels[i][chunkX * 16 + x, chunkY * 16 + y];
                            }
                        }
                    }
                    encode16by16block(ref bits, chunkChannels);
                }
            }

            return flush(bits);

        }

        private byte[] flush(List<byte> bits) {

            for (int i = 0; i < bits.Count / 8 - 1; i ++) {
                if ((bits[i*8] == 1 && bits[i*8 + 1] == 1 && bits[i*8 + 2] == 1 && bits[i*8 + 3] == 1 && bits[i*8 + 4] == 1 && bits[i*8 + 5] == 1 && bits[i*8 + 6] == 1 && bits[i*8 + 7] == 1)) {
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

        private void bmpToChannels() {
            for (int i = 0; i < 3; i++) {
                channels[i] = new double[Width,Height];
            }
            for (int y = 0; y < _sourceBitmap.Height; y++) {
                for (int x = 0; x < _sourceBitmap.Width; x++) {
                    Color pix = _sourceBitmap.GetPixel(x, y);
                    channels[0][x, y] = 0.299 * pix.R + 0.587 * pix.G + 0.114 * pix.B - 128;
                    channels[1][x, y] = -0.168736 * pix.R - 0.331264 * pix.G + 0.5 * pix.B;
                    channels[2][x, y] = 0.5 * pix.R - 0.418688 * pix.G - 0.081312 * pix.B;
                }
            }

        }

        private void encode16by16block(ref List<byte> bits, double[][,] YCbCrChannels) {
            for (int i = 0; i < 4; i++) {
                double[,] block8 = Block16ToBlock8(YCbCrChannels[0], i);
                block8 = DCT(block8);

                int[,] intBlock8 = quantization(block8, DefaultTables.QuantizationTableY);
                
                HuffmanEncode(ref bits, intBlock8, DefaultTables.HuffmanYDC, DefaultTables.HuffmanYAC, 0);



            }
            double[,] block8Cb = downSample(YCbCrChannels[1]);
            block8Cb = DCT(block8Cb);
            int[,] intBlock8Cb = quantization(block8Cb, DefaultTables.QuantizationTableChr);
            HuffmanEncode(ref bits, intBlock8Cb, DefaultTables.HuffmanChrDC, DefaultTables.HuffmanChrAC, 1);

            double[,] block8Cr = downSample(YCbCrChannels[2]);
            block8Cr = DCT(block8Cr);
            int[,] intBlock8Cr = quantization(block8Cr, DefaultTables.QuantizationTableChr);
            HuffmanEncode(ref bits, intBlock8Cr, DefaultTables.HuffmanChrDC, DefaultTables.HuffmanChrAC, 2);


        }

        private double[,] downSample(double[,] block16) {
            double[,] block8 = new double[8, 8];

            for (int y = 0; y < 16; y += 2) {
                for (int x = 0; x < 16; x += 2) {
                    block8[x / 2, y / 2] = (block16[x, y] + block16[x + 1, y] + block16[x, y + 1] + block16[x + 1, y + 1]) / 4;
                }
            }
            return block8;
        }

        private double[,] Block16ToBlock8(double[,] block16, int index) {
            double[,] block8 = new double[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    block8[x, y] = block16[x + (index % 2) * 8, y + index / 2 * 8];
                }
            }
            
            return block8;
        }

        private double[,] DCT(double[,] block8) {
            double[,] intBlock8 = new double[8, 8];

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    double tempSum = 0.0;
                    double cij = c(i, j);
                    for (int y = 0; y < 8; y++) {
                        for (int x = 0; x < 8; x++) {
                            tempSum += cij * block8[x, y] * cosines[x,i] * cosines[y,j];
                        }
                    }
                    intBlock8[i, j] = tempSum;
                }
            }
            return intBlock8;
        }

        private int[,] quantization(double[,] block8, byte[] quantizationTable) {
            int[,] block8Quantizied = new int[8, 8];

            for (int y = 0; y < 8; y++) {
                for (int x = 0; x < 8; x++) {
                    block8Quantizied[x,y] = (int)(block8[x, y] / quantizationTable[y * 8 + x] + 0.5);
                }
            }

            return block8Quantizied;
        }

        private int count = -1;
        private void HuffmanEncode(ref List<byte> bits, int[,] block8, HuffmanTable huffmanDC, HuffmanTable huffmanAC, int DCIndex) {
            count++;
            if (count > 150000) { 
                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        Console.Write(block8[j, i] + "  ");
                    }
                    Console.Write("\n");
                }
                Console.WriteLine();
                Console.ReadKey();
            }
            short diff = (short)(block8[0, 0] - LastDC[DCIndex]);
            LastDC[DCIndex] += diff;
            if (diff != 0) {
                byte category = bitCost(diff);
                huffmanElement huffmanCode = huffmanDC.getHuffmanCode(0, category);
                ushortToBits(ref bits, huffmanCode.codeWord, huffmanCode.length);

                ushortToBits(ref bits, negativeNumberEncoder(diff), category);
            } else {
                huffmanElement EOB = huffmanDC.getHuffmanCode(0x00, 0x00);
                ushortToBits(ref bits, EOB.codeWord, EOB.length);
            }

            int zeroesCounter = 0;
            for (int i = 1; i < 64; i++) {
                int x = _roadPoints[i, 0], y = _roadPoints[i, 1];
                if (block8[x, y] == 0) {
                    zeroesCounter++;
                    continue;
                }
                while (zeroesCounter >= 16) {
                    huffmanElement ZRL = huffmanAC.getHuffmanCode(0x0F, 0x00);
                    ushortToBits(ref bits, ZRL.codeWord, ZRL.length);
                    zeroesCounter -= 16;
                }

                byte cost = bitCost((short)Math.Abs(block8[x, y]));
                huffmanElement codeElement = huffmanAC.getHuffmanCode((byte)zeroesCounter, cost);
                zeroesCounter = 0;
                ushortToBits(ref bits, codeElement.codeWord, codeElement.length);

                ushortToBits(ref bits, negativeNumberEncoder((short)block8[x, y]), cost);

            }

            if (zeroesCounter != 0) { //EOB
                huffmanElement EOB = huffmanAC.getHuffmanCode(0x00, 0x00);
                ushortToBits(ref bits, EOB.codeWord, EOB.length);
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

        private byte bitCost(short number) {
            return (byte)(Math.Ceiling(Math.Log(Math.Abs(number) + 1) / Math.Log(2)));
        }

        private double c(int i, int j) {
            if (i == 0) {
                if (j == 0) {
                    return 0.125;
                } 
                return 0.17677;
            }
            if (j == 0) {
                return 0.17677;
            }
            return 0.25;
        }

        private ushort negativeNumberEncoder(short number) {
            return (number < 0) ? (ushort)(~Math.Abs(number)) : (ushort)Math.Abs(number);
        }

    }
}
