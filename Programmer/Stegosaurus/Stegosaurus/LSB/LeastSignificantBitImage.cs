using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Stegosaurus {
    public class LeastSignificantBitImage : StegoImageBase, IImageEncoder {
        private Bitmap _coverImage;
        private Bitmap _stegoImage;

        public LeastSignificantBitImage(Bitmap cover) {
            _coverImage = cover;
        }

        /* Setter for the CoverImage property checks the input */
        public Bitmap CoverImage {
            get { return _coverImage; }
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value));
                }
                _coverImage = value;
            }
        }


        /* Setter for the StegoImage property checks the input */
        public Bitmap StegoImage {
            get { return _stegoImage; }
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value));
                }
                _stegoImage = value;
            }
        }

        public void Encode(byte[] message) {

            List<byte> wholeMessage = message.ToList();

            //Use the first 4 bytes for saving the length
            uint len = (uint)message.Length;
            wholeMessage.Insert(0, (byte)(len >> 24));
            wholeMessage.Insert(1, (byte)((len & 0xff0000) >> 16));
            wholeMessage.Insert(2, (byte)((len & 0xff00) >> 8));
            wholeMessage.Insert(3, (byte)(len & 0xff));

            /* Flatten cover image */
            Color[] coverArr = ImageToArray(CoverImage);

            List<byte> hideData = new List<byte>();

            foreach (Color color in coverArr) {
                hideData.Add(color.R);
                hideData.Add(color.G);
                hideData.Add(color.B);
            }
            const byte mask = 0x3;
            const int bytesPerMessageElement = 4;

            for (int i = 0; i < wholeMessage.Count; i++) {
                for (int j = 0; j < bytesPerMessageElement; j++) {
                    //Remove the last two bits
                    hideData[i * bytesPerMessageElement + j] &= 0xFC;
                    
                    //Add the bits from the message
                    hideData[i * bytesPerMessageElement + j] += (byte)((wholeMessage[i] & (mask << ((bytesPerMessageElement - 1 - j) * 2))) >> ((bytesPerMessageElement - 1 - j) * 2));
                }
            }

            /* Array for holding the flattened decoded image */
            Color[] stegoArray = new Color[CoverImage.Width * CoverImage.Height];

            int length = CoverImage.Width * CoverImage.Height;
            for (int i = 0; i < length; i ++) {
                stegoArray[i] = Color.FromArgb(hideData[i * 3], hideData[i * 3 + 1], hideData[i * 3 + 2]);
            }
            /* Convert the created array into a bitmap */
            StegoImage = ArrayToImage(CoverImage.Width, CoverImage.Height, stegoArray);
        }

        public int GetCapacity() {
            int numberOfPixels = CoverImage.Width * CoverImage.Height;
            const int numberOfComponentsPerPixel = 3;
            const int bitsPerByte = 8;
            const int bitsSavedPerByte = 2;
            const int bytesUsedForLength = 4;

            return numberOfPixels * numberOfComponentsPerPixel * bitsSavedPerByte / bitsPerByte - bytesUsedForLength;
        }

        public void Save(string path) {
            StegoImage.Save(path);
        }
    }
}
