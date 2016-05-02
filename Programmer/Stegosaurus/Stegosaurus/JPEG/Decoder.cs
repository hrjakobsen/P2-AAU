using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Xml.Schema;

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
            BitList bits = findScanData(file);
            decodeScanData(bits, ref modulo);
            //short[] byteArr = decodeHuffmanValues(bits, ref modulo);
            //return GetMessage(byteArr, modulo);
            return null;
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

        private BitList findScanData(BinaryReader file) {
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
            for (int i = 0; i < 12; i++) {
                file.ReadByte();
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
                }
                listOfBytes.Add(a);
                
            }

            //foreach (byte b in listOfBytes) {
            //    Console.WriteLine(Convert.ToString(b, 16));
            //}

            
            BitList bits = new BitList();
            foreach (byte current in listOfBytes) {
                byte mask = 1;
                for (int i = 0; i < 8; i++) {
                    bits.Add((current & (mask << (7 - i))) >> (7 - i));
                }
            }
            return bits;
        }

        private void decodeScanData(BitList bits, ref int mod) {
            List<int> validNumbers = new List<int>();
            int index = 0;

            while (validNumbers.Count < 16) {
                _addNextMCU(validNumbers, bits, ref index);
            }
            

            int length = getLength(validNumbers);
            int mvalue = getModulo(validNumbers);

            validNumbers.RemoveRange(0, 16);
            

            int elementsToRead = (int)(length * (8 / Math.Log(mvalue, 2))) * 2;

            while (validNumbers.Count < elementsToRead) {
                _addNextMCU(validNumbers, bits, ref index);
            }

            List<byte> messageParts = new List<byte>();

            for (int i = 0; i <= elementsToRead; i += 2) {
                messageParts.Add((byte)(validNumbers[i] + validNumbers[i + 1]).Mod(mvalue));
            }

            List<byte> message = new List<byte>();
            int steps = (int)(8 / Math.Log(mvalue, 2));
            for (int i = 0; i < messageParts.Count - steps; i += steps) {
                byte toAdd = 0;
                for (int j = 0; j < steps; j++) {
                    toAdd <<= (int)(Math.Log(mvalue, 2));
                    toAdd += messageParts[i + j];
                }
                message.Add(toAdd);
            }

            string s = new string(message.Select(x => (char)x).ToArray());
            Console.WriteLine(s);


            Console.ReadKey();
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

        private short[] decodeHuffmanValues(BitList bitArr, ref int modulo) {
            int arrPos, iterations, length;
            arrPos = iterations = 0;
            length = 1;
            bool foundLength = false;
            List<short> listOfShorts = new List<short>();
            //foreach (bool o in bitArr) {
            //    Console.Write(o ? 1 : 0);
            //}
            while (bitArr.Count > arrPos && listOfShorts.Count < length) {
                for (int i = 0; i < 4; i++) {
                    decodeYDC(listOfShorts, bitArr, ref arrPos);
                    Console.WriteLine(listOfShorts.Aggregate("List: ", (current, listOfShort) => current + (listOfShort + ";")));
                }
                for (int i = 0; i < 2; i++) {   
                    decodeChrDC(listOfShorts, bitArr, ref arrPos);
                }
                Console.ReadKey();
                iterations++;
                if (!foundLength && listOfShorts.Count >= 16) {
                    foundLength = true;
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

        private void decodeYDC(List<short> listOfShorts, BitList bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0;
            int length = 0;
            while (i < 16) {
                code <<= 1;
                code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                i++;
                foreach (var item in YDCHuffman.Elements) {
                    if (i == item.Value.Length && code == item.Value.CodeWord) {
                        length = item.Value.RunSize & 0xf;
                        arrPos += length;
                        decodeYAC(ref listOfShorts, bitArr, ref arrPos);
                        
                        break;
                    }
                }
            }
        }

        private void decodeYAC(ref List<short> listOfShorts, BitList bitArr, ref int arrPos) {
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
                    code <<= 1;
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
                            category = (item.Value.RunSize & 0xf) + 1;
                            valueNotFound = false;
                            break;
                        }
                    }
                }
                counter += (zeroes + 1);

                if (!EOB && !ZRL) {
                    int length = category + arrPos;
                    code = 0;
                    for (; arrPos < length; arrPos++) {
                        code <<= 1;
                        code += (ushort)(bitArr[arrPos] ? 1 : 0);
                    }
                    short s = lookupValue(code, category);
                    listOfShorts.Add(lookupValue(code, category));
                }
            }
        }

        private void decodeChrDC(List<short> listOfShorts, BitList bitArr, ref int arrPos) {
            ushort code = 0;
            int i = 0;
            int length = 0;
            while (i < 16) {
                code += (ushort)(bitArr[arrPos++] ? 1 : 0);
                i++;
                foreach (var item in ChrDCHuffman.Elements) {
                    if (i == item.Value.Length && code == item.Value.CodeWord) {
                        length = item.Value.RunSize & 0xF;
                        arrPos += length;
                        decodeChrAC(ref listOfShorts, bitArr, ref arrPos);
                        break;
                    }
                }
                code <<= 1;
            }

        }

        private void decodeChrAC(ref List<short> listOfShorts, BitList bitArr, ref int arrPos) {
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
                    code <<= 1;
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
            ushort[] pairs = new ushort[7];
            int k = 0;
            int length = 7;
            Console.WriteLine(listOfShorts[14] + "-" + listOfShorts[15]);
            modulo = (listOfShorts[14] + listOfShorts[15]).Mod(4);
            switch (modulo) {
                case 0:
                    modulo = 2;
                    break;
                case 1:
                    modulo = 4;
                    break;
                case 2:
                    modulo = 16;
                    break;
                default:
                    Console.WriteLine($"no such modulo. Modulo was {modulo}");
                    throw new Exception();
            }
            
            int logM = (int)Math.Log(modulo, 2);
            for (int i = 0; i < 14; i += 2) {
                pairs[k++] = (ushort)((listOfShorts[i] + listOfShorts[i + 1]).Mod(modulo));
            }
            
            ushort lengthOfMessage = 0;
            for (int i = 0; i < length; i++) {
                lengthOfMessage <<= 2;
                lengthOfMessage += pairs[i];
            }
            foreach (var item in pairs) {
                Console.WriteLine(item);
            }
            Console.WriteLine($"length: {lengthOfMessage}");
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
