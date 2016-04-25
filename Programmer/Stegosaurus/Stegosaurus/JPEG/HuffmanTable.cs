using System;
using System.Collections.Generic;
using System.Linq;

namespace Stegosaurus {
    public class HuffmanTable {

        public Dictionary<byte, HuffmanElement> Elements = new Dictionary<byte, HuffmanElement>();
        public HuffmanTable(params HuffmanElement[] elements) {
            foreach (HuffmanElement huffmanElement in elements) {
                Elements.Add(huffmanElement.RunSize, huffmanElement);
            }
        } 

        public byte[] Combinations() {
            byte[] numberOfCodes = new byte[16];
            foreach (KeyValuePair<byte, HuffmanElement> element in Elements) {
                numberOfCodes[element.Value.Length - 1]++;
            }

            return numberOfCodes;
        }

        public override string ToString() {
            return Elements.Aggregate("", (current, huffmanElement) => current + (huffmanElement + "\n"));
        }

        public HuffmanElement GetElementFromRunSize(byte run, byte size) {
            byte runSize = (byte)((run << 4) | size);
            return Elements[runSize];
        }
#region DefaultTables
        // ReSharper disable once InconsistentNaming
        public static HuffmanTable JpegHuffmanTableYDC = new HuffmanTable(
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
        );

        // ReSharper disable once InconsistentNaming
        public static HuffmanTable JpegHuffmanTableYAC = new HuffmanTable(
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
        );

        // ReSharper disable once InconsistentNaming
        public static HuffmanTable JpegHuffmanTableChrDC = new HuffmanTable(
            new HuffmanElement(0x00, 0x00, 2),
            new HuffmanElement(0x01, 0x01, 2),
            new HuffmanElement(0x02, 0x02, 2),
            new HuffmanElement(0x03, 0x06, 3),
            new HuffmanElement(0x04, 0x0e, 4),
            new HuffmanElement(0x05, 0x1e, 5),
            new HuffmanElement(0x06, 0x3e, 6),
            new HuffmanElement(0x07, 0x7e, 7),
            new HuffmanElement(0x08, 0xfe, 8),
            new HuffmanElement(0x09, 0x1fe, 9),
            new HuffmanElement(0x0a, 0x3fe, 10),
            new HuffmanElement(0x0b, 0x7fe, 11)
        );

        // ReSharper disable once InconsistentNaming
        public static HuffmanTable JpegHuffmanTableChrAC = new HuffmanTable(
            new HuffmanElement(0x00, 0x0, 2),
            new HuffmanElement(0x01, 0x1, 2),
            new HuffmanElement(0x02, 0x4, 3),
            new HuffmanElement(0x03, 0xa, 4),
            new HuffmanElement(0x04, 0x18, 5),
            new HuffmanElement(0x05, 0x19, 5),
            new HuffmanElement(0x06, 0x38, 6),
            new HuffmanElement(0x07, 0x78, 7),
            new HuffmanElement(0x08, 0x1f4, 9),
            new HuffmanElement(0x09, 0x3f6, 10),
            new HuffmanElement(0x0a, 0xff4, 12),
            new HuffmanElement(0x11, 0xb, 4),
            new HuffmanElement(0x12, 0x39, 6),
            new HuffmanElement(0x13, 0xf6, 8),
            new HuffmanElement(0x14, 0x1f5, 9),
            new HuffmanElement(0x15, 0x7f6, 11),
            new HuffmanElement(0x16, 0xff5, 12),
            new HuffmanElement(0x17, 0xff88, 16),
            new HuffmanElement(0x18, 0xff89, 16),
            new HuffmanElement(0x19, 0xff8a, 16),
            new HuffmanElement(0x1a, 0xff8b, 16),
            new HuffmanElement(0x21, 0x1a, 5),
            new HuffmanElement(0x22, 0xf7, 8),
            new HuffmanElement(0x23, 0x3f7, 10),
            new HuffmanElement(0x24, 0xff6, 12),
            new HuffmanElement(0x25, 0x7fc2, 15),
            new HuffmanElement(0x26, 0xff8c, 16),
            new HuffmanElement(0x27, 0xff8d, 16),
            new HuffmanElement(0x28, 0xff8e, 16),
            new HuffmanElement(0x29, 0xff8f, 16),
            new HuffmanElement(0x2a, 0xff90, 16),
            new HuffmanElement(0x31, 0x1b, 5),
            new HuffmanElement(0x32, 0xf8, 8),
            new HuffmanElement(0x33, 0x3f8, 10),
            new HuffmanElement(0x34, 0xff7, 12),
            new HuffmanElement(0x35, 0xff91, 16),
            new HuffmanElement(0x36, 0xff92, 16),
            new HuffmanElement(0x37, 0xff93, 16),
            new HuffmanElement(0x38, 0xff94, 16),
            new HuffmanElement(0x39, 0xff95, 16),
            new HuffmanElement(0x3a, 0xff96, 16),
            new HuffmanElement(0x41, 0x3a, 6),
            new HuffmanElement(0x42, 0x1f6, 9),
            new HuffmanElement(0x43, 0xff97, 16),
            new HuffmanElement(0x44, 0xff98, 16),
            new HuffmanElement(0x45, 0xff99, 16),
            new HuffmanElement(0x46, 0xff9a, 16),
            new HuffmanElement(0x47, 0xff9b, 16),
            new HuffmanElement(0x48, 0xff9c, 16),
            new HuffmanElement(0x49, 0xff9d, 16),
            new HuffmanElement(0x4a, 0xff9e, 16),
            new HuffmanElement(0x51, 0x3b, 6),
            new HuffmanElement(0x52, 0x3f9, 10),
            new HuffmanElement(0x53, 0xff9f, 16),
            new HuffmanElement(0x54, 0xffa0, 16),
            new HuffmanElement(0x55, 0xffa1, 16),
            new HuffmanElement(0x56, 0xffa2, 16),
            new HuffmanElement(0x57, 0xffa3, 16),
            new HuffmanElement(0x58, 0xffa4, 16),
            new HuffmanElement(0x59, 0xffa5, 16),
            new HuffmanElement(0x5a, 0xffa6, 16),
            new HuffmanElement(0x61, 0x79, 7),
            new HuffmanElement(0x62, 0x7f7, 11),
            new HuffmanElement(0x63, 0xffa7, 16),
            new HuffmanElement(0x64, 0xffa8, 16),
            new HuffmanElement(0x65, 0xffa9, 16),
            new HuffmanElement(0x66, 0xffaa, 16),
            new HuffmanElement(0x67, 0xffab, 16),
            new HuffmanElement(0x68, 0xffac, 16),
            new HuffmanElement(0x69, 0xffad, 16),
            new HuffmanElement(0x6a, 0xffae, 16),
            new HuffmanElement(0x71, 0x7a, 7),
            new HuffmanElement(0x72, 0x7f8, 11),
            new HuffmanElement(0x73, 0xffaf, 16),
            new HuffmanElement(0x74, 0xffb0, 16),
            new HuffmanElement(0x75, 0xffb1, 16),
            new HuffmanElement(0x76, 0xffb2, 16),
            new HuffmanElement(0x77, 0xffb3, 16),
            new HuffmanElement(0x78, 0xffb4, 16),
            new HuffmanElement(0x79, 0xffb5, 16),
            new HuffmanElement(0x7a, 0xffb6, 16),
            new HuffmanElement(0x81, 0xf9, 8),
            new HuffmanElement(0x82, 0xffb7, 16),
            new HuffmanElement(0x83, 0xffb8, 16),
            new HuffmanElement(0x84, 0xffb9, 16),
            new HuffmanElement(0x85, 0xffba, 16),
            new HuffmanElement(0x86, 0xffbb, 16),
            new HuffmanElement(0x87, 0xffbc, 16),
            new HuffmanElement(0x88, 0xffbd, 16),
            new HuffmanElement(0x89, 0xffbe, 16),
            new HuffmanElement(0x8a, 0xffbf, 16),
            new HuffmanElement(0x91, 0x1f7, 9),
            new HuffmanElement(0x92, 0xffc0, 16),
            new HuffmanElement(0x93, 0xffc1, 16),
            new HuffmanElement(0x94, 0xffc2, 16),
            new HuffmanElement(0x95, 0xffc3, 16),
            new HuffmanElement(0x96, 0xffc4, 16),
            new HuffmanElement(0x97, 0xffc5, 16),
            new HuffmanElement(0x98, 0xffc6, 16),
            new HuffmanElement(0x99, 0xffc7, 16),
            new HuffmanElement(0x9a, 0xffc8, 16),
            new HuffmanElement(0xa1, 0x1f8, 9),
            new HuffmanElement(0xa2, 0xffc9, 16),
            new HuffmanElement(0xa3, 0xffca, 16),
            new HuffmanElement(0xa4, 0xffcb, 16),
            new HuffmanElement(0xa5, 0xffcc, 16),
            new HuffmanElement(0xa6, 0xffcd, 16),
            new HuffmanElement(0xa7, 0xffce, 16),
            new HuffmanElement(0xa8, 0xffcf, 16),
            new HuffmanElement(0xa9, 0xffd0, 16),
            new HuffmanElement(0xaa, 0xffd1, 16),
            new HuffmanElement(0xb1, 0x1f9, 9),
            new HuffmanElement(0xb2, 0xffd2, 16),
            new HuffmanElement(0xb3, 0xffd3, 16),
            new HuffmanElement(0xb4, 0xffd4, 16),
            new HuffmanElement(0xb5, 0xffd5, 16),
            new HuffmanElement(0xb6, 0xffd6, 16),
            new HuffmanElement(0xb7, 0xffd7, 16),
            new HuffmanElement(0xb8, 0xffd8, 16),
            new HuffmanElement(0xb9, 0xffd9, 16),
            new HuffmanElement(0xba, 0xffda, 16),
            new HuffmanElement(0xc1, 0x1fa, 9),
            new HuffmanElement(0xc2, 0xffdb, 16),
            new HuffmanElement(0xc3, 0xffdc, 16),
            new HuffmanElement(0xc4, 0xffdd, 16),
            new HuffmanElement(0xc5, 0xffde, 16),
            new HuffmanElement(0xc6, 0xffdf, 16),
            new HuffmanElement(0xc7, 0xffe0, 16),
            new HuffmanElement(0xc8, 0xffe1, 16),
            new HuffmanElement(0xc9, 0xffe2, 16),
            new HuffmanElement(0xca, 0xffe3, 16),
            new HuffmanElement(0xd1, 0x7f9, 11),
            new HuffmanElement(0xd2, 0xffe4, 16),
            new HuffmanElement(0xd3, 0xffe5, 16),
            new HuffmanElement(0xd4, 0xffe6, 16),
            new HuffmanElement(0xd5, 0xffe7, 16),
            new HuffmanElement(0xd6, 0xffe8, 16),
            new HuffmanElement(0xd7, 0xffe9, 16),
            new HuffmanElement(0xd8, 0xffea, 16),
            new HuffmanElement(0xd9, 0xffeb, 16),
            new HuffmanElement(0xda, 0xffec, 16),
            new HuffmanElement(0xe1, 0x3fe0, 14),
            new HuffmanElement(0xe2, 0xffed, 16),
            new HuffmanElement(0xe3, 0xffee, 16),
            new HuffmanElement(0xe4, 0xffef, 16),
            new HuffmanElement(0xe5, 0xfff0, 16),
            new HuffmanElement(0xe6, 0xfff1, 16),
            new HuffmanElement(0xe7, 0xfff2, 16),
            new HuffmanElement(0xe8, 0xfff3, 16),
            new HuffmanElement(0xe9, 0xfff4, 16),
            new HuffmanElement(0xea, 0xfff5, 16),
            new HuffmanElement(0xf0, 0x3fa, 10),
            new HuffmanElement(0xf1, 0x7fc3, 15),
            new HuffmanElement(0xf2, 0xfff6, 16),
            new HuffmanElement(0xf3, 0xfff7, 16),
            new HuffmanElement(0xf4, 0xfff8, 16),
            new HuffmanElement(0xf5, 0xfff9, 16),
            new HuffmanElement(0xf6, 0xfffa, 16),
            new HuffmanElement(0xf7, 0xfffb, 16),
            new HuffmanElement(0xf8, 0xfffc, 16),
            new HuffmanElement(0xf9, 0xfffd, 16),
            new HuffmanElement(0xfa, 0xfffe, 16)
        );
#endregion
    }


    public class HuffmanElement : IComparable<HuffmanElement> {
        public byte RunSize { get; }
        public byte Length { get; }
        public ushort CodeWord { get; }

        public HuffmanElement(byte runSize, ushort codeWord, byte length) {
            RunSize = runSize;
            CodeWord = codeWord;
            Length = length;
        }

        public int CompareTo(HuffmanElement other) {
            if (Length == other.Length) {
                return RunSize - other.RunSize;
            } else {
                return Length - other.Length;
            }
        }

        public byte[] CodeWordToBytes() {
            byte[] bytes = new byte[2];
            bytes[0] = (byte)(CodeWord >> 8);
            bytes[1] = (byte)CodeWord;

            return bytes;
        }

        public override string ToString() {
            return $"{Convert.ToString(RunSize, 2)} = {Convert.ToString(CodeWord, 2)}, {Convert.ToString(Length, 2)}";
        }
    }
}
