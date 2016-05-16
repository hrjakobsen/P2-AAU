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

        private byte _encodeComponent(byte component, byte toEncode, int messageBytePos ) {
            /* Used to mask the correct bits of the RGB channels */
            const byte coverMask = 0xFC;
            byte[] messageMasks = { 0xC0, 0x30, 0xC, 0x3 };
            return (byte)((byte)(component & coverMask) + ((byte)(toEncode & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
        }

        

        private byte _toComponent(byte component, int stegoBitPos) {
            return (byte)((byte)(component & 0x3) << ((3 - stegoBitPos) * 2));
        }

        public void Encode(byte[] message) {

            List<byte> WholeMessage = message.ToList();
            uint len = (uint)message.Length;

            WholeMessage.Insert(0, (byte)(len >> 24));
            WholeMessage.Insert(1, (byte)((len & 0xff0000) >> 16));
            WholeMessage.Insert(2, (byte)((len & 0xff00) >> 8));
            WholeMessage.Insert(3, (byte)(len & 0xff));
            int messageArrIndex = 0;

            byte mask = 0x3;
            /* Flatten cover image */
            Color[] coverArr = ImageToArray(CoverImage);

            List<byte> hideData = new List<byte>();

            foreach (Color color in coverArr) {
                hideData.Add(color.R);
                hideData.Add(color.G);
                hideData.Add(color.B);
            }

            for (int i = 0; i < WholeMessage.Count; i++) {
                for (int j = 0; j < 4; j++) {
                    hideData[i * 4 + j] >>= 2;
                    hideData[i * 4 + j] <<= 2;
                    hideData[i * 4 + j] += (byte)((WholeMessage[i] & (0x3 << ((3 - j)*2))) >> ((3 - j)*2));
                }
            }

            /* Array for holding the flattened decoded image */
            Color[] decodedArr = new Color[CoverImage.Width * CoverImage.Height];

            int length = CoverImage.Width * CoverImage.Height;
            for (int i = 0; i < length; i ++) {
                decodedArr[i] = Color.FromArgb(hideData[i * 3], hideData[i * 3 + 1], hideData[i * 3 + 2]);
            }
            /* Convert the created array into a bitmap */
            StegoImage = ArrayToImage(CoverImage.Width, CoverImage.Height, decodedArr);
        }

        public int GetCapacity() {
            return CoverImage.Width * CoverImage.Height * 3 * 2 / 8;
        }

        public void Save(string path) {
            StegoImage.Save(path);
        }
    }
}
