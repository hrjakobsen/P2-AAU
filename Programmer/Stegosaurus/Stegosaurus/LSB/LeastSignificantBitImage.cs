using System;
using System.Drawing;

namespace Stegosaurus {
    public class LeastSignificantBitImage : StegoImageBase {
        private Bitmap _coverImage;
        private Bitmap _messageImage;
        private Bitmap _stegoImage;

        /* Setter for the CoverImage property checks the input */
        public Bitmap CoverImage {
            get { return _coverImage; }
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value));
                }
                if (MessageImage != null && (value.Width / 2 != MessageImage.Width || value.Height / 2 != MessageImage.Height)) {
                    throw new ArgumentException(nameof(value), "The width and height of the cover image must be exactly double those of the message image!");
                }
                _coverImage = value;
            }
        }

        /* Setter for the MessageImage property checks the input */
        public Bitmap MessageImage {
            get { return _messageImage; }
            set {
                if (value == null) {
                    throw new ArgumentNullException(nameof(value));
                }
                if (CoverImage != null && (value.Width * 2 != CoverImage.Width || value.Height * 2 != CoverImage.Height)) {
                    throw new ArgumentException(nameof(value), "The width and height of the message image must be exactly half those of the cover image!");
                }
                _messageImage = value;
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
        
        public override void Encode() {
            int messageArrIndex = 0;


            /* Flatten cover image */
            Color[] coverArr = ImageToArray(CoverImage);

            /* Flatten message image */
            Color[] messageArr = ImageToArray(MessageImage);

            /* Array for holding the flattened decoded image */
            Color[] decodedArr = new Color[CoverImage.Width * CoverImage.Height];

            for (int coverArrIndex = 0; coverArrIndex < coverArr.Length; coverArrIndex += 4) {
                for (int messageBytePos = 0; messageBytePos < 4; messageBytePos++) {
                    byte r = _encodeComponent(coverArr[coverArrIndex + messageBytePos].R, messageArr[messageArrIndex].R, messageBytePos);
                    byte g = _encodeComponent(coverArr[coverArrIndex + messageBytePos].G, messageArr[messageArrIndex].G, messageBytePos);
                    byte b = _encodeComponent(coverArr[coverArrIndex + messageBytePos].B, messageArr[messageArrIndex].B, messageBytePos);
                    decodedArr[coverArrIndex + messageBytePos] = Color.FromArgb(r, g, b);
                }
                messageArrIndex++;
            }

            /* Convert the created array into a bitmap */
            StegoImage = ArrayToImage(CoverImage.Width, CoverImage.Height, decodedArr);
        }

        private byte _encodeComponent(byte component, byte toEncode, int messageBytePos ) {
            /* Used to mask the correct bits of the RGB channels */
            const byte coverMask = 0xFC;
            byte[] messageMasks = { 0xC0, 0x30, 0xC, 0x3 };
            return (byte)((byte)(component & coverMask) + ((byte)(toEncode & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
        }

        public override void Decode() {
            /* Flatten stego image */
            Color[] stegoArr = ImageToArray(StegoImage);

            /* Array for holding flattened message image */
            Color[] plainArr = new Color[StegoImage.Width / 2 * StegoImage.Height / 2];
            
            for (int plainArrIndex = 0; plainArrIndex < plainArr.Length; plainArrIndex++) {
                byte r = 0, g = 0, b = 0;
                for (int stegoBitPos = 0; stegoBitPos < 4; stegoBitPos++) {
                    r += _toComponent(stegoArr[plainArrIndex * 4 + stegoBitPos].R, stegoBitPos);
                    g += _toComponent(stegoArr[plainArrIndex * 4 + stegoBitPos].G, stegoBitPos);
                    b += _toComponent(stegoArr[plainArrIndex * 4 + stegoBitPos].B, stegoBitPos);
                }
                plainArr[plainArrIndex] = Color.FromArgb(r, g, b);
            }

            /* Convert the created array into a bitmap */
            MessageImage = ArrayToImage(StegoImage.Width / 2, StegoImage.Height / 2, plainArr);
        }

        private byte _toComponent(byte component, int stegoBitPos) {
            return (byte)((byte)(component & 0x3) << ((3 - stegoBitPos) * 2));
        }
    }
}
