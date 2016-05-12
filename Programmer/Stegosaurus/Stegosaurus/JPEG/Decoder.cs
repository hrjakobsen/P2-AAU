using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Stegosaurus {
    public class Decoder:IImageDecoder {

        /// <summary>
        /// Huffman table used for the DC coefficient of the Y component of the image.
        /// </summary>
        public HuffmanTable YDCHuffman { get; private set; }

        /// <summary>
        /// Huffman table used for the AC coefficient of the Y component of the image.
        /// </summary>
        public HuffmanTable YACHuffman { get; private set; }

        /// <summary>
        /// Huffman table used for the DC coefficient of the CrCb components of the image.
        /// </summary>
        public HuffmanTable ChrDCHuffman { get; private set; }

        /// <summary>
        /// Huffman table used for the AC coefficient of the CrCb components of the image.
        /// </summary>
        public HuffmanTable ChrACHuffman { get; private set; }

        private BinaryReader file;
        List<HuffmanTable> huffmanTables = new List<HuffmanTable>();

        /// <summary>
        /// Takes a filepath to a JPEG file.
        /// </summary>
        public Decoder(string path) {
            StreamReader sr = new StreamReader(path);
            file = new BinaryReader(sr.BaseStream);
            for (int i = 0; i < 4; i++) {
                byte ClassAndID = 0;
                HuffmanTable temp = getHuffmanTable(ref ClassAndID);
                if ((byte)(ClassAndID & 0xf0) == 0) {
                    if ((byte)(ClassAndID & 0x0f) == 0) {
                        YDCHuffman = temp;
                    } else {
                        ChrDCHuffman = temp;
                    }
                } else if ((byte)(ClassAndID & 0x0f) == 0) {
                    YACHuffman = temp;
                } else {
                    ChrACHuffman = temp;
                }
            }

        }

        /// <summary>
        /// Used to decode the hidden message in given file
        /// </summary>
        /// <param name="path"></param>
        public byte[] Decode() {
            BitList bits = findScanData();
            return decodeScanData(bits);
        }

        private HuffmanTable getHuffmanTable(ref byte ClassAndID) {
            List<HuffmanElement> huffmanElements = new List<HuffmanElement>();
            /* length of the huffman table also contains the length of the elements, as well as the length itself
               subtracting the offset from the length, ensures we only read all the huffman elements, and not from outside the huffmantable */
            int offset = 19; // 16 (counter for each bitlength) + 2 (length bytes) + 1 (class byte)

            findMarker(0xc4);

            int length = ((file.ReadByte() << 8) + file.ReadByte());
            ClassAndID = file.ReadByte();

            byte[] byteArr = new byte[16];
            for (int i = 0; i < 16; i++) {
                byteArr[i] = file.ReadByte();
            }

            /* creates the huffman table from the file */
            length = length - offset;
            byte currentLength = 0;
            ushort code = 0;
            byte elementsOfLengthLeft = byteArr[currentLength];
            for (int i = 0; i < length; i++) {
                while (elementsOfLengthLeft <= 0 && currentLength != 15) {
                    currentLength++;
                    code <<= 1;
                    elementsOfLengthLeft = byteArr[currentLength];
                }
                huffmanElements.Add(new HuffmanElement(file.ReadByte(), code, (byte)(currentLength + 1)));
                elementsOfLengthLeft--;
                code++;
            }
            return new HuffmanTable(huffmanElements.ToArray());
        }

        private void findMarker(byte marker) {
            byte a = 0, b = 0;
            while (b != marker) {
                while (a != 0xFF) {
                    a = file.ReadByte();
                }
                a = 0;
                b = file.ReadByte();
            }
        }

        private BitList findScanData() {
            byte a;
            findMarker(0xda);
            for (int i = 0; i < 12; i++) {
                file.ReadByte();
            }

        //  Writes scandata to List
            List<byte> scanData = new List<byte>();
            int length = (int)file.BaseStream.Length;
            while (file.BaseStream.Position < length) {
                a = file.ReadByte();

                if (a == 0xff) {
                    byte b = file.ReadByte();
                    if (b != 0) { //If bytes are actually a marker
                        break;
                    }
                }
                scanData.Add(a);
            }
        
        //  Convert each byte to bits
            BitList bits = new BitList();
            foreach (byte current in scanData) {
                byte mask = 1;
                for (int i = 0; i < 8; i++) {
                    bits.Add((current & (mask << (7 - i))) >> (7 - i));
                }
            }
            return bits;
        }

        private byte[] decodeScanData(BitList bits) {
            List<int> validNumbers = new List<int>();
            int index = 0;
        
        //  16 values are needed in order to find the value of modulo and the length of the encoded message
            while (validNumbers.Count < 16) {
                _addNextMCU(validNumbers, bits, ref index);
            }
        
            int length = getLength(validNumbers);
            int modulo = getModulo(validNumbers);

            validNumbers.RemoveRange(0, 16);

            int elementsToRead = (int)(length * (8 / Math.Log(modulo, 2))) * 2; // what

        //  Only read in the values we need to decode in order to decode the message
            while (validNumbers.Count < elementsToRead) {
                _addNextMCU(validNumbers, bits, ref index);
            }

            List<byte> messageParts = new List<byte>();

            for (int i = 0; i < elementsToRead; i += 2) {
                messageParts.Add((byte)(validNumbers[i] + validNumbers[i + 1]).Mod(modulo));
            }

        //  Combines each part of each character together, to fully decode the message
            List<byte> message = new List<byte>();
            int steps = (int)(8 / Math.Log(modulo, 2));
            for (int i = 0; i < messageParts.Count - steps; i += steps) {
                byte toAdd = 0;
                for (int j = 0; j < steps; j++) {
                    toAdd <<= (int)(Math.Log(modulo, 2));
                    toAdd += messageParts[i + j];
                }
                message.Add(toAdd);
            }

            return message.ToArray();
        }

        private int getLength(List<int> values) {
            ushort length = 0;
            for (int i = 0; i < 14; i += 2) {
                int current = (values[i] + values[i + 1]).Mod(4);
                length = (ushort)((length << 2) + current);
            }
            return length;
        }

        private int getModulo(List<int> values) {
            switch ((values[14] + values[15]).Mod(4)) {
                case 0:
                    return 2;
                case 1:
                    return 4;
                case 2:
                    return 16;
                case 3:
                    return 256;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Read an MCU consisting of 4 Y blocks, 1 block for cb, and 1 block for cr
        private void _addNextMCU(List<int> validNumbers, BitList bits, ref int index) {
            for (int i = 0; i < 4; i++) {
                validNumbers.AddRange(getBlock(bits, ref index, YDCHuffman, YACHuffman));
            }
            for (int i = 0; i < 2; i++) {
                validNumbers.AddRange(getBlock(bits, ref index, ChrDCHuffman, ChrACHuffman));
            }
        }

        private List<int> getBlock(BitList bits, ref int index, HuffmanTable DC, HuffmanTable AC) {
            List<int> validNumbers = new List<int>();
            int[] values = new int[64];
            int[] zigzag = {
                0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26, 33, 40, 48, 41, 34, 27, 20, 13, 6, 7,
                14, 21, 28, 35, 42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52, 45, 38, 31, 39,
                46, 53, 60, 61, 54, 47, 55, 62, 63
            };
            int numberOfElements = 0;
            int zeroes = 0;
            values[zigzag[numberOfElements++]] = nextValue(bits, ref index, DC, out zeroes);
            while (numberOfElements < 64) {
                int value = nextValue(bits, ref index, AC, out zeroes);
                if (value == 0 && zeroes == 0) { //EOB
                    while (numberOfElements < 64) {
                        values[zigzag[numberOfElements++]] = 0;
                    }
                } else { //ZRL and normal
                    for (int i = 0; i < zeroes; i++) {
                        values[zigzag[numberOfElements++]] = 0;
                    }
                    values[zigzag[numberOfElements++]] = value;
                }
            }

            for (int i = 1; i < values.Length; i++) {
                int element = values[i];
                if (element != 0) {
                    validNumbers.Add(element);
                }
            }

            return validNumbers;

        }

        private int nextValue(BitList bits, ref int index, HuffmanTable huffmanTable, out int zeroes) {
            HuffmanElement e = null;
            int i;
            ushort code = 0;
            for (i = 1; i <= 16; i++) {
                code <<= 1;
                code += (ushort)(bits[index] ? 1 : 0);
                index++;
                e = huffmanTable.HasCode(code, i);
                if (e != null) {
                    break;
                }
            }
            if (e == null) {
                throw new ArgumentNullException();
            }
            zeroes = (e.RunSize & 0xF0) >> 4;

            int category = e.RunSize & 0xF;

            ushort value = 0;
            for (i = 0; i < category; i++) {
                value <<= 1;
                value += (ushort)(bits[index] ? 1 : 0);
                index++;
            }


            return lookupValue(value, category);
        }

        private short lookupValue(ushort value, int category) {
            //  Console.WriteLine($"{value}, {category}");
            short valueToReturn = 0;
            if (value >> (category - 1) == 1) {
                valueToReturn = (short)value;
            } else {
                valueToReturn = (short)(value - (Math.Pow(2, category)) + 1); //what?
            }
            return valueToReturn;
        }
    }
}