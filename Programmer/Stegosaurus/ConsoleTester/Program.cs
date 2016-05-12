using System;
using System.Collections.Generic;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"testImage.jpg"), 100, 4);
            
            //Console.WriteLine(ji.GetCapacity());

            //byte[] msg = "Hej".Select(x => (byte)x).ToArray();

            int len = 320;
            byte[] msg = new byte[len];
            for (int i = 0; i < len; i++) {
                msg[i] = (byte)('a' + i % 26);
            }

            ji.Encode(msg);
            ji.Save(@"testOut.jpg");

            IImageDecoder jid = new Decoder("testOut.jpg");
            byte[] message = jid.Decode();
            Console.WriteLine(new string(message.Select(x => (char)x).ToArray()));
            Console.ReadKey();
        }
    }
}