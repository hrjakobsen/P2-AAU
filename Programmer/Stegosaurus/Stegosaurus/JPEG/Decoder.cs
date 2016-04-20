using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;

namespace Stegosaurus {
    public class Decoder : IImageDecoder {

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

        List<HuffmanTable> huffmanTables = new List<HuffmanTable>();
        /// <summary>
        /// Used to decode the hidden message in given file
        /// </summary>
        /// <param name="path"></param>
        public byte[] Decode(string path) {
            StreamReader sr = new StreamReader(path);
            BinaryReader file = new BinaryReader(sr.BaseStream);
            for (int i = 0; i < 4; i++) {
                byte ClassAndID = 0;
                HuffmanTable temp = getHuffmanTable(file, ref ClassAndID);
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
            BitArray bitArr = findScanData(file);
            byte[] byteArr = decodeHuffmanValues(bitArr);
            return GetMessage(byteArr);
        }

        private HuffmanTable getHuffmanTable(BinaryReader file, ref byte ClassAndID) {
            List<HuffmanElement> huffmanElements = new List<HuffmanElement>();
            bool foundMarker = false;
            byte a;
            /* length of the huffman table also contains the length of the elements, as well as the length itself
               subtracting the offset from the length, ensures we only read all the huffman elements, and not from outside the huffmantable */
            int offset = 19;
            while (!foundMarker) {
                a = file.ReadByte();
                if(a == 0xff) {
                    a = file.ReadByte();
                    if(a == 0xc4) {
                        foundMarker = true;
                    }
                }
            }
            int length = ((file.ReadByte() << 8) + file.ReadByte());
            ClassAndID = file.ReadByte();

            byte[] byteArr = new byte[16];
            for (int i = 0; i < 16; i++) {
                byteArr[i] = file.ReadByte();
            }

            /* creates the huffman table from the file */
            length = length - offset;
            int elementsOfLengthLeft, currentLength = 0;
            ushort code = 0;
            elementsOfLengthLeft = byteArr[currentLength];
            for (int i = 0; i < length; i++) {
                while(elementsOfLengthLeft <= 0 && currentLength != 16) {
                    currentLength++;
                    code <<= 1;
                    elementsOfLengthLeft = byteArr[currentLength];
                }
                huffmanElements.Add(new HuffmanElement(file.ReadByte(), code, byteArr[currentLength]));
                elementsOfLengthLeft--;
                code++;                
            }
            return new HuffmanTable(huffmanElements.ToArray());
        }

        private BitArray findScanData(BinaryReader file) {
            byte a;
            bool foundMarker = false;
            while (!foundMarker) {
                a = file.ReadByte();
                if (a == 0xff) {
                    a = file.ReadByte();
                    if (a == 0xda) {
                        foundMarker = true;
                    }
                }
            }
            foundMarker = false;
            List<byte> listOfBytes = new List<byte>();
            while (!foundMarker) {
                a = file.ReadByte();

                if (a == 0xff) {
                    byte b = file.ReadByte();
                    if (b != 0) {
                        break;
                    }
                    listOfBytes.Add(a);
                    listOfBytes.Add(b);
                } else {
                    listOfBytes.Add(a);
                }
            }
            BitArray bitArr = new BitArray(listOfBytes.ToArray());
            return bitArr;
        }

        private byte[] decodeHuffmanValues(BitArray bitArr) {
            byte[] byteArr = new byte[10];
            return byteArr;
        }

        private byte[] GetMessage(byte[] scanData) {
            // int m = scanData[0];
            // m is the modulo operator saved somewhere in scanData
            int m = 4;
            List<byte> byteList = new List<byte>();
            int i;
            for (i = 1; i < scanData.Length; i++) {
                while (scanData[i] == 0) {
                    i++;
                }
                byte temp = scanData[i++];
                while (scanData[i] == 0) {
                    i++;
                }
                byteList.Add((byte)((scanData[i] + temp) % m));
            }

            int iterations = (int)(8 / Math.Log(m, 2));
            int logM = (int)Math.Log(m, 2);
            List<byte> message = new List<byte>();
            i = 1;
            while (i < byteList.Count) {
                byte byteToAdd = 0;
                for (int j = 0; j < iterations; j++) {
                    if (i < byteList.Count) {
                        // extracts the message from the bytelist and bitshifts the appropriate amount of times.
                        // LogM describes how many bits we've got the information in
                        // iterations - j + 1 times LogM ensures we've added the bits in the right spots
                        byteToAdd += (byte)(byteList[i] << (logM * (iterations - j + 1)));
                        i++;
                    }
                }
                message.Add(byteToAdd);
            }
            List<byte> actualMessage = new List<byte>();

            //byteList[4] is the position where the length of the message is saved
            int read = byteList[4];
            for (i = 0; i < read; i++) {
                actualMessage.Add(message[i]);
            }
            return actualMessage.ToArray();
        }
    }
}
