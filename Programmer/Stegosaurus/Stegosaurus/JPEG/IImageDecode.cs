namespace Stegosaurus {
    public interface IImageDecoder {
        byte[] Decode(string path);
    }
}