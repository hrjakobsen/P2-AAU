using System.Drawing;

namespace Stego_Image_LSB {
    public class DecodeLSB : BaseLSB {
        public DecodeLSB(string filePath) : base(filePath) { }
        public DecodeLSB(Bitmap fullSizeImage) : base(fullSizeImage) { }
        
        public override Bitmap Steganography() {
            /* Flatten stego image */
            Color[] stegoArr = ImageToArray(FullSizeImage);

            /* Array for holding flattened plain image */
            Color[] plainArr = new Color[FullSizeImage.Width / 2 * FullSizeImage.Height / 2];
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

            return ArrayToImage(FullSizeImage.Width / 2, FullSizeImage.Height / 2, plainArr);
        }
    }
}