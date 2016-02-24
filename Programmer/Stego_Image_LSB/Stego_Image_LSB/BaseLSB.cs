using System;
using System.Drawing;
using System.IO;

namespace Stego_Image_LSB {
    public abstract class BaseLSB {
        protected Bitmap FullSizeImage;
        protected Bitmap MessageImage;

        protected BaseLSB(string filePath) {
            if (!string.IsNullOrEmpty(filePath)) {
                try {
                    FullSizeImage = new Bitmap(filePath);
                } catch (FileNotFoundException e) {
                    Console.WriteLine($"{filePath} not found. Make sure it is a valid file path. {e.Message}");
                    throw;
                }
            } else {
                throw new ArgumentException("String cannot be null or empty");
            }
        }

        protected BaseLSB(Bitmap fullSizeImage) {
            if (fullSizeImage != null) {
                FullSizeImage = fullSizeImage;
            } else {
                throw new ArgumentException("Image cannot be null");
            }
        }

        public abstract Bitmap Steganography();

        protected Color[] ImageToArray(Bitmap imgIn) {
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

        protected Bitmap ArrayToImage(int width, int height, Color[] arrIn) {
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
