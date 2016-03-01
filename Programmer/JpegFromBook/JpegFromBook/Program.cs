using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JpegFromBook {
    class Program {
        static void Main(string[] args) {
            Bitmap b = new Bitmap("lena_color.jpg");
            
            JPEG jpg = new JPEG(b);
            jpg.encode(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\out.jpg", 100.0);

        }
    }
}
