using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Stegosaurus {
    public class Decoder : IImageDecoder {

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

        public Decoder(string path) {
            StreamReader sr = new StreamReader(path);
            file = new BinaryReader(sr.BaseStream);
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
        }

        /// <summary>
        /// Used to decode the hidden message in given file
        /// </summary>
        /// <param name="path"></param>
        public byte[] Decode() {
            int modulo = 0;
            BitArray bitArr = findScanData(file);
            short[] byteArr = decodeHuffmanValues(bitArr, ref modulo);
            return GetMessage(byteArr, modulo);
        }

        private HuffmanTable getHuffmanTable(BinaryReader file, ref byte ClassAndID) {
            List<HuffmanElement> huffmanElements = new List<HuffmanElement>();
            bool foundMarker = false;
            byte a;
            /* length of the huffman table also contains the length of the elements, as well as the length itself
               subtracting the offset from the length, ensures we only read all the huffman elements, and not from outside the huffmantable */
            int offset = 19; //Where the fuck does 19 come from?? 16 (counter for each bitlength) + 2 (length bytes) + 1 (class byte)?
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
            byte elementsOfLengthLeft, currentLength = 0;
            ushort code = 0;
            elementsOfLengthLeft = byteArr[currentLength];
            for (int i = 0; i < length; i++) {
                while(elementsOfLengthLeft <= 0 && currentLength != 15) {
                    currentLength++;
                    code <<= 1;
                    elementsOfLengthLeft = byteArr[currentLength];
                }
                huffmanElements.Add(new HuffmanElement(file.ReadByte(), code, (byte)(currentLength+1)));
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

        private short[] decodeHuffmanValues(BitArray bitArr, ref int modulo) {
            int arrPos, iterations, length;
            arrPos = iterations = 0;
            length = 1;
            List<short> listOfShorts = new List<short>();
            while (bitArr.Count > arrPos && listOfShorts.Count < length) {
                for (int i = 0; i < 4; i++) {
                    decodeYDC(bitArr, ref arrPos);
                    decodeYAC(ref listOfShorts, bitArr, ref arrPos);
                }
                decodeChrDC(bitArr, ref arrPos);
                decodeChrAC(ref listOfShorts, bitArr, ref arrPos);
                iterations++;
                if (iterations == 1) {
                    length = getMessageLength(listOfShorts, ref modulo);
                    for (int i = 0; i < 16; i++) {
                        listOfShorts.RemoveAt(0);
                    }
                }
               // Console.WriteLine(listOfShorts.Count + " " + arrPos);

            }
            //foreach (var item in listOfShorts) {
            //    Console.Write($"{item} ");
            //};
            int additionalElements = listOfShorts.Count - length;
            for (int i = 0; i < additionalElements; i++) {
                listOfShorts.RemoveAt(length);
            }
            //Console.WriteLine(listOfShorts.Count);
            return listOfShorts.ToArray();
        }

        private void decodeYDC(BitArray bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0;
            int length = 0;
            while (i < 16) {
                code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                i++;
                foreach (var item in YDCHuffman.Elements) {
                    if (i == item.Value.Length && code == item.Value.CodeWord) {
                        length = item.Value.RunSize;
                        break;
                    }
                }
                code <<= 1;
            }
            arrPos += length;
        }

        private void decodeYAC(ref List<short> listOfShorts, BitArray bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0, counter = 1;
            byte zeroes = 0;
            int category = 0;
            bool EOB, ZRL;
            EOB = false;
            while (!EOB && counter < 64) {
                code = 0;
                i = 0;
                bool valueNotFound = true;
                ZRL = false;
                while (i < 16 && valueNotFound) {
                    code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                    i++;
                    foreach (var item in YACHuffman.Elements) {
                        if (i == item.Value.Length && code == item.Value.CodeWord) {
                            if (item.Value.RunSize == 0xf0) {
                                ZRL = true;
                            } else if (item.Value.RunSize == 0) {
                            EOB = true;
                        }
                        zeroes = (byte)((item.Value.RunSize & 0xf0) >> 4);
                            category = (item.Value.RunSize & 0x0f) + 1;
                            valueNotFound = false;
                            break;
                        }
                    }
                    code <<= 1;
                }
                counter += (zeroes + 1);

                if (!EOB && !ZRL) {
                    int length = category + (arrPos - 1);
                    code = 0;
                    for (; arrPos < length; arrPos++) {
                        code += (ushort)(bitArr[arrPos] ? 1 : 0);
                        code <<= 1;
                    }
                    code += (ushort)(bitArr[arrPos++] ? 1 : 0);

                    listOfShorts.Add(lookupValue(code, category));
                }
            }
        }

        private void decodeChrDC(BitArray bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0;
            int length = 0;
            while (i < 16) {
                code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                i++;
                foreach (var item in ChrDCHuffman.Elements) {
                    if (i == item.Value.Length && code == item.Value.CodeWord) {
                        length = item.Value.RunSize;
                        break;
                    }
                }
                code <<= 1;
            }

            arrPos += length;
        }

        private void decodeChrAC(ref List<short> listOfShorts, BitArray bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0, counter = 1;
            byte zeroes = 0;
            int category = 0;
            bool EOB, ZRL;
            EOB = false;
            while (!EOB && counter < 64) {
                bool valueNotFound = true;
                ZRL = false;
                code = 0;
                i = 0;
                while (i < 16 && valueNotFound) {
                    code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                    i++;
                    foreach (var item in YACHuffman.Elements) {
                        if (i == item.Value.Length && code == item.Value.CodeWord) {
                            if (item.Value.RunSize == 0xf0) {
                                ZRL = true;
                            } else if (item.Value.RunSize == 0) {
                                EOB = true;
                            }
                            zeroes = (byte)((item.Value.RunSize & 0xf0) >> 4);
                            category = (item.Value.RunSize & 0x0f) + 1;
                            valueNotFound = false;
                            break;
                        }
                    }
                    code <<= 1;
                }
                counter += (zeroes + 1);
                if (!EOB && !ZRL) {
                    int length = category + (arrPos - 1);
                    code = 0;
                    for (; arrPos < length; arrPos++) {
                        code += (ushort)(bitArr[arrPos] ? 1 : 0);
                        code <<= 1;
                    }
                    code += (ushort)(bitArr[arrPos++] ? 1 : 0);

                    listOfShorts.Add(lookupValue(code, category));
                }
            }
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

        private int getMessageLength(List<short> listOfShorts, ref int modulo) {
            short[] shortArr = new short[7];
            int k = 0;
            int length = 7;
            modulo = listOfShorts[14] + listOfShorts[15];
            switch (modulo) {
                case 0:
                    modulo = 4;
                    break;
                case 1:
                    modulo = 8;
                    break;
                case 2:
                    modulo = 16;
                    break;
                case 3:
                    modulo = 256;
                    break;
                default:
                    Console.WriteLine($"no such modulo. Modulo was {modulo}");
                    throw new Exception();
            }
            int logM = (int)Math.Log(modulo, 2);
            for (int i = 0, j = 1; i < 14; i+=2) {
                shortArr[k++] = (short)((listOfShorts[i] + listOfShorts[j]).Mod(modulo));
                j += 2;
            }

            length -= 1;
            short lengthOfMessage = 0;
            for (int i = 0; i < length; i++) {
                lengthOfMessage += shortArr[i];
                lengthOfMessage <<= logM;
            }
            //lengthOfMessage += shortArr[length];
            //foreach (var item in shortArr) {
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine($"length: {lengthOfMessage}");
            //Console.ReadKey();
            return lengthOfMessage % 2 == 0 ? lengthOfMessage : lengthOfMessage + 1;
        }

        private byte[] GetMessage(short[] scanData, int m) {
            List<byte> byteList = new List<byte>();
            int i;
            for (i = 1; i < scanData.Length - 1; i+=2) {
                if (i < scanData.Length) {
                    byteList.Add((byte)((scanData[i] + scanData[i+1]).Mod(m)));
                }
                
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
