using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stegosaurus;
using System.Drawing;

namespace ConsoleTester {
    class Program {
        static void Main(string[] args) {
            JpegImage ji = new JpegImage(new Bitmap(@"cat.jpg"), @"output.jpg", 100);
            ji.Encode();
            //     ji.Decode();
        }
    }
}
