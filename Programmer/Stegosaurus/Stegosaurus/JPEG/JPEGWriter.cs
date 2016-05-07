using System.Collections.Generic;
using System.IO;

namespace Stegosaurus {
    class JpegWriter {
        private readonly List<byte> _bytes = new List<byte>(); 

        public void WriteBytes(params byte[] bytes) {
            _bytes.AddRange(bytes);
        }

        public void ToFile(string path) {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            foreach (byte fileByte in _bytes) {
                fs.WriteByte(fileByte);
            }
            fs.Close();
        }
    }
}
