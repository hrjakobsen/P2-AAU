using System;
using Stegosaurus;
using System.Drawing;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100, 4);
            //IImageDecoder jid = new Decoder();
            //jid.Decode(@"output.jpg");

            ji.Encode(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
           ji.Save(@"output.jpg");
            Console.ReadKey();
        }
    }
}
