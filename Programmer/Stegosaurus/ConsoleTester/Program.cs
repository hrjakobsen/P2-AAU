using System;
using System.Collections.Generic;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"tiger.jpg"), 100, 4);

            Console.WriteLine(ji.GetCapacity());

            int len = 280;
            byte[] msg = new byte[len];
            for (int i = 0; i < len; i++) {
                msg[i] = (byte)('A' + i % 26);
            }

            ji.Encode(msg);
            ji.Save(@"out.jpg");

            IImageDecoder jid = new Decoder("out.jpg");
            byte[] message = jid.Decode();
            Console.WriteLine(new string(message.Select(x => (char)x).ToArray()));
            Console.ReadKey();
        }
    }
}