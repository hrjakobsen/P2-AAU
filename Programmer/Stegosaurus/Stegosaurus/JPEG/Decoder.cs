using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus {
    public class Decoder : IDecoder {

        /// <summary>
        /// Used to decode the hidden message in given file
        /// </summary>
        /// <param name="path"></param>
        public byte[] Decode(string path) {
            StreamReader sr = new StreamReader(path);
            BinaryReader file = new BinaryReader(sr.BaseStream);
            byte[] byteArr = findScanData(file);
            return GetMessage(byteArr);
        }

        private byte[] findScanData(BinaryReader file) {
            byte a;
            bool foundMarker = false;
            while (!foundMarker) {
                a = file.ReadByte();
                if (a == 0xff) {
                    a = file.ReadByte();
                    if (a == 0xda) {
                        foundMarker = true;
                    }
                }
            }
            foundMarker = false;
            List<byte> listOfBytes = new List<byte>();
            while (!foundMarker) {
                a = file.ReadByte();

                if (a == 0xff) {
                    byte b = file.ReadByte();
                    if (b != 0) {
                        break;
                    }
                    listOfBytes.Add(a);
                    listOfBytes.Add(b);
                } else {
                    listOfBytes.Add(a);
                }
            }
            return listOfBytes.ToArray();
        }

        private byte[] GetMessage(byte[] scanData) {
            // int m = scanData[0];
            // m is the modulo operator saved somewhere in scanData
            int m = 4;
            List<byte> byteList = new List<byte>();
            int i;
            for (i = 1; i < scanData.Length; i++) {
                while (scanData[i] == 0) {
                    i++;
                }
                byte temp = scanData[i++];
                while (scanData[i] == 0) {
                    i++;
                }
                byteList.Add((byte)((scanData[i] + temp) % m));
            }

            int iterations = (int)(8 / Math.Log(m, 2));
            int logM = (int)Math.Log(m, 2);
            List<byte> message = new List<byte>();
            i = 1;
            while (i < byteList.Count) {
                byte byteToAdd = 0;
                for (int j = 0; j < iterations; j++) {
                    if (i < byteList.Count) {
                        // extracts the message from the bytelist and bitshifts the appropriate amount of times.
                        // LogM describes how many bits we've got the information in
                        // iterations - j + 1 times LogM ensures we've added the bits in the right spots
                        byteToAdd += (byte)(byteList[i] << (logM * (iterations - j + 1)));
                        i++;
                    }
                }
                message.Add(byteToAdd);
            }
            List<byte> actualMessage = new List<byte>();

            //byteList[4] is the position where the length of the message is saved
            int read = byteList[4];
            for (i = 0; i < read; i++) {
                actualMessage.Add(message[i]);
            }
            return actualMessage.ToArray();
        }
    }
}
