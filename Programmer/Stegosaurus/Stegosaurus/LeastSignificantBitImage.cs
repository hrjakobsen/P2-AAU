using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    class LeastSignificantBitImage : StegoImageBase {
        private Bitmap _messageImage;
        private Bitmap _stegoImage;
        
        public Bitmap MessageImage {
            get { return _messageImage; }
            set { _messageImage = value; }
        }

        public Bitmap StegoImage {
            get { return _stegoImage; }
            set { _stegoImage = value; }
        }

        public override void Encode() {
            const byte coverMask = 0xFC;
            byte[] messageMasks = { 0xC0, 0x30, 0xC, 0x3 };
            int messageArrIndex = 0;

            /* Flatten cover image */
            Color[] coverArr = ImageToArray(CoverImage);

            /* Flatten message image */
            Color[] messageArr = ImageToArray(MessageImage);

            /* Array for holding the flattened decoded image */
            Color[] decodedArr = new Color[CoverImage.Width * CoverImage.Height];

            for (int coverArrIndex = 0; coverArrIndex < coverArr.Length; coverArrIndex += 4) {
                for (int messageBytePos = 0; messageBytePos < 4; messageBytePos++) {
                    byte r = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].R & coverMask) + ((byte)(messageArr[messageArrIndex].R & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
                    byte g = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].G & coverMask) + ((byte)(messageArr[messageArrIndex].G & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
                    byte b = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].B & coverMask) + ((byte)(messageArr[messageArrIndex].B & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));

                    decodedArr[coverArrIndex + messageBytePos] = Color.FromArgb(r, g, b);
                }
                messageArrIndex++;
            }

            StegoImage = ArrayToImage(CoverImage.Width, CoverImage.Height, decodedArr);
        }

        public override void Decode() {
            /* Flatten stego image */
            Color[] stegoArr = ImageToArray(CoverImage);

            /* Array for holding flattened plain image */
            Color[] plainArr = new Color[CoverImage.Width / 2 * CoverImage.Height / 2];
            const byte maskPlain = 0x3;

            for (int plainArrIndex = 0; plainArrIndex < plainArr.Length; plainArrIndex++) {
                byte r = 0, g = 0, b = 0;
                for (int stegoBitPos = 0; stegoBitPos < 4; stegoBitPos++) {
                    r += (byte)((byte)(stegoArr[plainArrIndex * 4 + stegoBitPos].R & maskPlain) << ((3 - stegoBitPos) * 2));
                    g += (byte)((byte)(stegoArr[plainArrIndex * 4 + stegoBitPos].G & maskPlain) << ((3 - stegoBitPos) * 2));
                    b += (byte)((byte)(stegoArr[plainArrIndex * 4 + stegoBitPos].B & maskPlain) << ((3 - stegoBitPos) * 2));
                }
                plainArr[plainArrIndex] = Color.FromArgb(r, g, b);
            }

            MessageImage = ArrayToImage(CoverImage.Width / 2, CoverImage.Height / 2, plainArr);
        }
    }
}
