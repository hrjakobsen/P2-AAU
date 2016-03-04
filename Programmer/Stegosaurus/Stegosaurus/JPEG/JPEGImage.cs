using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class JpegImage : StegoImageBase{
        public Bitmap CoverImage { get; private set; }
        public string Path { get; set; }
        private JpegWriter _jw;
        public QuantizationTable YQuantizationTable { get; set; }
        public QuantizationTable ChrQuantizationTable { get; set; }
        public HuffmanTable YDCHuffman { get; set; }
        public HuffmanTable YACHuffman { get; set; }
        public HuffmanTable ChrDCHuffman { get; set; }
        public HuffmanTable ChrACHuffman { get; set; }

        public JpegImage(Bitmap coverImage, string path) :this(coverImage, path, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable){}

        public JpegImage(Bitmap coverImage, string path, QuantizationTable yTable, QuantizationTable chrTable) 
            : this (coverImage, path, yTable, chrTable, HuffmanTable.JpegHuffmanTableYDC, HuffmanTable.JpegHuffmanTableYAC, HuffmanTable.JpegHuffmanTableChrDC, HuffmanTable.JpegHuffmanTableChrAC) { }

        public JpegImage(Bitmap coverImage, string path, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC) 
            :this(coverImage, path, QuantizationTable.JpegDefaultYTable, QuantizationTable.JpegDefaultChrTable, huffmanYDC, huffmanYAC, huffmanChrDC, huffmanChrAC)
            {}

        public JpegImage(Bitmap coverImage, string path, QuantizationTable yTable, QuantizationTable chrTable, HuffmanTable huffmanYDC, HuffmanTable huffmanYAC, HuffmanTable huffmanChrDC, HuffmanTable huffmanChrAC) {
            if (coverImage == null) { 
                throw new ArgumentNullException();
            }

            CoverImage = coverImage;
            Path = path;
            YQuantizationTable = yTable;
            ChrQuantizationTable = chrTable;
            YDCHuffman = huffmanYDC;
            YACHuffman = huffmanYAC;
            ChrDCHuffman = huffmanChrDC;
            ChrACHuffman = huffmanChrAC;
        }


        public override void Encode() {
            _jw = new JpegWriter(Path);
            _writeHeaders();
            _jw.Close();
            
            
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

            ushort len = (ushort)(huffman.Elements.Length + huffman.Combinations().Length + 3);
            _jw.WriteBytes((byte)(len >> 8), (byte)(len & 0xff));

            byte combined;
            if (!dc) {
                combined = (byte)((1 << 4) + id);
            } else {
                combined = id;
            }

            _jw.WriteBytes(combined);

            _jw.WriteBytes(huffman.Combinations());

            foreach (HuffmanElement huffmanElement in huffman.Elements) {
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
        private void _writeScanData() { }

        private void _writeEndOfImage() {
            _jw.WriteBytes(0xff, 0xd9);
        }
        public override void Decode() {
            throw new NotImplementedException();
        }
    }
}
