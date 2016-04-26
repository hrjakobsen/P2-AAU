namespace Stegosaurus {
    public interface IImageDecoder {
        HuffmanTable YDCHuffman { get; }
        HuffmanTable YACHuffman { get; }
        HuffmanTable ChrDCHuffman { get; }
        HuffmanTable ChrACHuffman { get; }
        byte[] Decode();
    }
}