using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class LeastSignificantBitDecoder : StegoImageBase, IImageDecoder {
        public string Filepath { get; set; }

        public LeastSignificantBitDecoder(string path) {
            Filepath = path;
        }

        public byte[] Decode() {
            /* Flatten stego image */
            Color[] stegoArr = ImageToArray(new Bitmap(Filepath));

            List<byte> hideData = new List<byte>();
            //Add all color components for saving information
            foreach (Color color in stegoArr) {
                hideData.Add(color.R);
                hideData.Add(color.G);
                hideData.Add(color.B);
            }

            List<byte> message = new List<byte>();
            //Get the length from the first 4 bytes (2 * 16 = 32 bits = sizeof(uint))
            uint length = 0;
            for (int i = 0; i < 16; i++) {
                length <<= 2;
                length += (byte)(hideData[i] & 0x3);
            }

            for (int i = 0; i < length; i++) {
                byte nextMessage = 0;
                for (int j = 0; j < 4; j++) {
                    nextMessage <<= 2;
                    nextMessage += (byte)(hideData[i * 4 + j + 16] & 0x3);
                }
                message.Add(nextMessage);
            }


            return message.ToArray();
        }
    }
}
