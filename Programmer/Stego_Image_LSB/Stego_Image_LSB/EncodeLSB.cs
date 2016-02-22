using System;
using System.Drawing;
using System.IO;

namespace Stego_Image_LSB {
    public class EncodeLSB : BaseLSB {

        public EncodeLSB(string coverFilePath, string messageFilePath) : base(coverFilePath) {
            if (!string.IsNullOrEmpty(messageFilePath)) {
                try {
                    MessageImage = new Bitmap(messageFilePath);
                } catch (FileNotFoundException e) {
                    Console.WriteLine($"{messageFilePath} not found. Make sure it is a valid file path. {e.Message}");
                    throw;
                }
            } else {
                throw new ArgumentException("File path of the message image cannot be null or empty");
            }
        }

        public EncodeLSB(Bitmap coverImage, Bitmap messageImage) : base(coverImage) {
            if (messageImage != null) {
                MessageImage = messageImage;
            } else {
                throw new ArgumentException("The message image cannot be null");
            }
        }

        public override Bitmap Steganography() {
            const byte coverMask = 0xFC;
            byte[] messageMasks = { 0xC0, 0x30, 0xC, 0x3 };
            int messageArrIndex = 0;

            /* Flatten cover image */
            Color[] coverArr = ImageToArray(FullSizeImage);

            /* Flatten message image */
            Color[] messageArr = ImageToArray(MessageImage);

            /* Array for holding the flattened decoded image */
            Color[] decodedArr = new Color[FullSizeImage.Width * FullSizeImage.Height];

            for (int coverArrIndex = 0; coverArrIndex < coverArr.Length; coverArrIndex += 4) {
                for (int messageBytePos = 0; messageBytePos < 4; messageBytePos++) {
                    byte r = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].R & coverMask) + ((byte)(messageArr[messageArrIndex].R & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
                    byte g = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].G & coverMask) + ((byte)(messageArr[messageArrIndex].G & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));
                    byte b = (byte)((byte)(coverArr[coverArrIndex + messageBytePos].B & coverMask) + ((byte)(messageArr[messageArrIndex].B & messageMasks[messageBytePos]) >> 2 * (3 - messageBytePos)));

                    decodedArr[coverArrIndex + messageBytePos] = Color.FromArgb(r, g, b);
                }
                messageArrIndex++;
            }

            return ArrayToImage(FullSizeImage.Width, FullSizeImage.Height, decodedArr);
        }

    }
}
