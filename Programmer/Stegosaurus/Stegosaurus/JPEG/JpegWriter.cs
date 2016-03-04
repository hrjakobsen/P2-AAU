using System;
using System.IO;

namespace Stegosaurus {
    class JpegWriter : FileStream{
        public JpegWriter(string path):base(path, FileMode.Create, FileAccess.Write) {
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException();
            }
        }

        public void WriteBytes(params byte[] bytes) {
            foreach (byte b in bytes) {
                WriteByte(b);
            }
        }
    }
}
