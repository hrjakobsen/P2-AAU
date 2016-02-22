using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JpegFromBook {
    struct bit {
        byte b;
    }

    struct huffmanElement {
        public byte runSize;
        public ushort codeWord;
        public byte length;

        public huffmanElement(byte rS, ushort cW, byte len) {
            runSize = rS;
            codeWord = cW;
            length = len;
        }
    }


    class HuffmanTable {
        public byte[] bitLengths;
        public huffmanElement[] elements;

        public int combinationsInTable {
            get {
                int sum = 0;
                for (int i = 0; i < 16; i++) {
                    sum += bitLengths[i];
                }
                return sum; 
            }
        }
        

        public HuffmanTable(byte[] bitLengthArray, huffmanElement[] combinationsArray) {
            elements = combinationsArray;
            bitLengths = bitLengthArray;
        }

        public huffmanElement getHuffmanCode(byte zeroes, byte trailingBits) {
            byte SearchByte = (byte)((zeroes << 4) | trailingBits) ;

            for (int i = 0; i < combinationsInTable; i++) {
                if (elements[i].runSize == SearchByte) {
                    return elements[i];
                }
            }
            return new huffmanElement();
        }
    }
}
