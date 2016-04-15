using System;
using Stegosaurus;
using System.Drawing;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
           IImageEncoder ji = new JpegImage(new Bitmap(@"cat.jpg"), 100);
           // ji.Encode(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10});
          //  ji.Save(@"output.jpg");
            ji.Decode(@"output.jpg");
            Console.ReadKey();
        }
    }
}
