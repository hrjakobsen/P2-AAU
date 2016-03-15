using System;
using System.Collections.Generic;
using System.IO;

namespace Stegosaurus {
    class JpegWriter {
        private readonly List<byte> _bytes = new List<byte>(); 
        
        public void WriteBytes(params byte[] bytes) {
            foreach (byte b in bytes) {
                _bytes.Add(b);
            }
        }

        public byte[] ToArray() {
            return _bytes.ToArray();
        }
    }
}
