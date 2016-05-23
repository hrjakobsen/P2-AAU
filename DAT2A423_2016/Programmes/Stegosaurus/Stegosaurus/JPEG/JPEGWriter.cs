using System.Collections.Generic;
using System.IO;

namespace Stegosaurus {
    class JpegWriter {
        private readonly List<byte> _buffer = new List<byte>(); 

        /// <summary>
        /// Saves all bytes to a buffer than can be written to a file later.
        /// </summary>
        /// <param name="bytes"></param>
        public void WriteBytes(params byte[] bytes) {
            _buffer.AddRange(bytes);
        }

        /// <summary>
        /// Writes the buffer to the specified path
        /// </summary>
        /// <param name="path">Path to the file where the file will be saved</param>
        public void ToFile(string path) {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            foreach (byte fileByte in _buffer) {
                fs.WriteByte(fileByte);
            }
            fs.Close();
        }
    }
}
