using System.Drawing;

namespace Stegosaurus {
    public abstract class StegoImageBase : Steganography {
        public abstract override void Encode();
        public abstract override void Decode();

        protected static Color[] ImageToArray(Bitmap imgIn) {
            int height = imgIn.Height;
            int width = imgIn.Width;
            Color[] arrOut = new Color[width * height];

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    arrOut[y * width + x] = imgIn.GetPixel(x, y);
                }
            }
            return arrOut;
        }

        protected static Bitmap ArrayToImage(int width, int height, Color[] arrIn) {
            Bitmap imgOut = new Bitmap(width, height);
            int arrIndex = 0;

            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    imgOut.SetPixel(x, y, arrIn[arrIndex]);
                    arrIndex++;
                }
            }
            return imgOut;
        }
    }
}
