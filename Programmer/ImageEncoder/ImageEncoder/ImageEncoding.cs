using System;
using System.Drawing;
using System.IO;

namespace ImageEncoding
{
    public class ImageEncoder
    {
        private Bitmap ImageToEncode;

        public ImageEncoder(string FilePath) {
            if (!string.IsNullOrEmpty(FilePath)) {
                try {
                    ImageToEncode = new Bitmap(FilePath);
                } catch (FileNotFoundException e) {
                    Console.WriteLine($"{FilePath} not found. Make sure it is a valid file path. {e.Message}");
                    throw;
                }
            } else {
                throw new System.ArgumentException("String cannot be null or empty");
            }
        }

        public ImageEncoder(Bitmap ImageToBeEncoded) {
            if (ImageToBeEncoded != null) {
                ImageToEncode = ImageToBeEncoded;
            } else {
                throw new ArgumentException("Image cannot be null");
            }
        }

        public Bitmap EncodeToImage(string FilePath) {
            Bitmap ImageToEncodeIn;
            if (!string.IsNullOrEmpty(FilePath)) {
                try {
                    ImageToEncodeIn = new Bitmap(FilePath);
                } catch (FileNotFoundException e) {
                    Console.WriteLine($"{FilePath} not found. Make sure it is a valid file path. {e.Message}");
                    throw;
                }
            } else {
                throw new System.ArgumentException("String cannot be null or empty");
            }

            return EncodeToImage(ImageToEncodeIn);
        }

        public Bitmap EncodeToImage(Bitmap ImageToEncodeIn) {
            if (ImageToEncode.Width * 2 != ImageToEncodeIn.Width || ImageToEncode.Height * 2 != ImageToEncodeIn.Height) {
                throw new ArgumentException("The width and height of the image to encode in must be double the size of the image to encode, in both dimensions");
            }

            Color[] ImageToEncodeArr = ImageTo1DArr(ImageToEncode);
            Color[] ImageToEncodeInArr = ImageTo1DArr(ImageToEncodeIn);
            Color[] ResultImageArr = new Color[ImageToEncodeIn.Width * ImageToEncodeIn.Height];


            int EncoderCounter = 0;
            byte OriginalMask = 0xFC; /* Grabs 1111 1100 */
            byte[] ExtractMasks = new byte[] { 0xC0, 0x30, 0xC, 0x3}; /* 1100 0000, 0011 0000, 0000 1100, 0000 0011 */
            byte R, G, B;
            
            for (int j = 0; j < ImageToEncodeInArr.Length; j += 4) {
                for (int i = 0; i < 4; i++) {
                    R = (byte)((byte)(ImageToEncodeInArr[j + i].R & OriginalMask) + ((byte)(ImageToEncodeArr[EncoderCounter].R & ExtractMasks[i]) >> 2 * (3 - i)));
                    G = (byte)((byte)(ImageToEncodeInArr[j + i].G & OriginalMask) + ((byte)(ImageToEncodeArr[EncoderCounter].G & ExtractMasks[i]) >> 2 * (3 - i)));
                    B = (byte)((byte)(ImageToEncodeInArr[j + i].B & OriginalMask) + ((byte)(ImageToEncodeArr[EncoderCounter].B & ExtractMasks[i]) >> 2 * (3 - i)));
                   
                    ResultImageArr[j + i] = Color.FromArgb(R, G, B);
                }
                EncoderCounter++;
            }

            Bitmap ResultImage = ArrToImage(ImageToEncodeIn.Width, ImageToEncodeIn.Height, ResultImageArr);

            return ResultImage;
        }

        private static Color[] ImageTo1DArr(Bitmap ImageToConvert) {
            Color[] Arr = new Color[ImageToConvert.Width * ImageToConvert.Height];

            for (int i = 0; i < ImageToConvert.Height; i++) {
                for (int j = 0; j < ImageToConvert.Width; j++) {
                    Arr[i * ImageToConvert.Width + j] = ImageToConvert.GetPixel(j, i);
                }
            }

            return Arr;
        }

        private static Bitmap ArrToImage(int Width, int Height, Color[] Arr) {
            Bitmap ResultImage = new Bitmap(Width, Height);

            int ArrCounter = 0;
            for (int i = 0; i < Height; i++) {
                for (int j = 0; j < Width; j++) {
                    ResultImage.SetPixel(j, i, Arr[ArrCounter]);
                    ArrCounter++;
                }
            }

            return ResultImage;
        }

        public static Bitmap ExtractImageFromImage(string FilePath) {
            if (string.IsNullOrEmpty(FilePath)) {
                throw new ArgumentException("String cannot be null or empty.");
            }
            Bitmap ImageToExtractFrom = new Bitmap(FilePath);

            return ExtractImageFromImage(ImageToExtractFrom);
        }

        public static Bitmap ExtractImageFromImage(Bitmap OriginalImage) {
            if (OriginalImage == null) {
                throw new ArgumentNullException();
            }
            

            Color[] OriginalImageArr = ImageTo1DArr(OriginalImage);
            Color[] ResultImageArr = new Color[OriginalImage.Width / 2 * OriginalImage.Height / 2];

            byte ExtractMask = 0x3;

            for (int i = 0; i < ResultImageArr.Length; i++) {
                byte R = 0, G = 0, B = 0;
                for (int j = 0; j < 4; j++) {
                    R += (byte)((byte)(OriginalImageArr[i * 4 + j].R & ExtractMask) << ((3 - j) * 2));
                    G += (byte)((byte)(OriginalImageArr[i * 4 + j].G & ExtractMask) << ((3 - j) * 2));
                    B += (byte)((byte)(OriginalImageArr[i * 4 + j].B & ExtractMask) << ((3 - j) * 2));
                }
                ResultImageArr[i] = Color.FromArgb(R, G, B);
            }

            Bitmap ResultImage = ArrToImage(OriginalImage.Width / 2, OriginalImage.Height / 2, ResultImageArr);

            return ResultImage;
        }

    }
}