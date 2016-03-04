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
            JpegImage ji = new JpegImage(new Bitmap(@"C:\Users\musik\Desktop\Avatar_cat.jpg"), @"C:\Users\musik\Desktop\hej.hex");
            ji.Encode();

        }
    }
}
