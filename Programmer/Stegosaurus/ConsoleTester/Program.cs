using System;
using System.Collections.Generic;
using Stegosaurus;
using System.Drawing;
using System.Linq;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100, 4);
            
            //Console.WriteLine(ji.GetCapacity());

            //byte[] msg = "Hej".Select(x => (byte)x).ToArray();

            int len = 400;
            byte[] msg = new byte[len];
            for (int i = 0; i < len; i++) {
                msg[i] = (byte)'a';
            }

            ji.Encode(msg);
            ji.Save(@"output.jpg");

            //IImageDecoder jid = new Decoder("output.jpg");
            //byte[] message = jid.Decode();
            //Console.WriteLine(new string(message.Select(x => (char)x).ToArray()));

            Console.ReadKey();
        }
    }
}