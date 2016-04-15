namespace Stegosaurus {
    public interface IImageEncoder {
        void Encode(byte[] message);
        int GetCapacity();
        void Save(string path);
    }
}
