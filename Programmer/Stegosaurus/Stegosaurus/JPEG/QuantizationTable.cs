using System;
using System.Linq;

namespace Stegosaurus {
    [Serializable]
    public class QuantizationTable {
        public byte[] Entries { get; }
        public byte[] ZigzagEntries { get; }

        public QuantizationTable() : this (
            new byte[] {
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
                1,1,1,1,1,1,1,1,
            }) { }

        public static readonly int[,] RoadPoints = {
            {0, 0}, {1, 0}, {0, 1}, {0, 2}, {1, 1}, {2, 0}, {3, 0}, {2, 1},
            {1, 2}, {0, 3}, {0, 4}, {1, 3}, {2, 2}, {3, 1}, {4, 0}, {5, 0},
            {4, 1}, {3, 2}, {2, 3}, {1, 4}, {0, 5}, {0, 6}, {1, 5}, {2, 4},
            {3, 3}, {4, 2}, {5, 1}, {6, 0}, {7, 0}, {6, 1}, {5, 2}, {4, 3},
            {3, 4}, {2, 5}, {1, 6}, {0, 7}, {1, 7}, {2, 6}, {3, 5}, {4, 4},
            {5, 3}, {6, 2}, {7, 1}, {7, 2}, {6, 3}, {5, 4}, {4, 5}, {3, 6},
            {2, 7}, {3, 7}, {4, 6}, {5, 5}, {6, 4}, {7, 3}, {7, 4}, {6, 5},
            {5, 6}, {4, 7}, {5, 7}, {6, 6}, {7, 5}, {7, 6}, {6, 7}, {7, 7}
        };

        public QuantizationTable(byte[] entries) {
            Entries = entries;

            ZigzagEntries = new byte[64];

            for (int i = 0; i < 64; i++) {
                ZigzagEntries[i] = Entries[RoadPoints[i, 0] + RoadPoints[i, 1] * 8];
            }
        }

        public QuantizationTable Scale(int quality) {
            if (quality < 0 || quality > 100) {
                throw new ArgumentOutOfRangeException(nameof(quality), "Quality must be in the range [0,100]");
            }
            double scale = ((double)(100 - quality) / 53 + 0.125);
            byte[] scaledEntries = new byte[64];
            for (int entryIndex = 0; entryIndex < 64; entryIndex++) {
                scaledEntries[entryIndex] = (byte)(Entries[entryIndex] * scale);
            }
            return new QuantizationTable(scaledEntries);
        }

        public override string ToString() {
            string s = "";
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    s += Entries[i * 8 + j] + "\t";
                }
                s += "\n";
            }

            return s;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return !Entries.Where((t, i) => ((QuantizationTable)obj).Entries[i] != t).Any();
        }

        public static QuantizationTable JpegDefaultYTable = new QuantizationTable(new byte[] {
            0x10, 0x0b, 0x0a, 0x10, 0x18, 0x28, 0x33, 0x3d,
            0x0c, 0x0c, 0x0e, 0x13, 0x1a, 0x3a, 0x3c, 0x37,
            0x0e, 0x0d, 0x10, 0x18, 0x28, 0x39, 0x45, 0x38,
            0x0e, 0x11, 0x16, 0x1d, 0x33, 0x57, 0x50, 0x3e,
            0x12, 0x16, 0x25, 0x38, 0x44, 0x6d, 0x67, 0x4d,
            0x18, 0x23, 0x37, 0x40, 0x51, 0x68, 0x71, 0x5c,
            0x31, 0x40, 0x4e, 0x57, 0x67, 0x79, 0x78, 0x65,
            0x48, 0x5c, 0x5f, 0x62, 0x70, 0x64, 0x67, 0x63
        });

        public static QuantizationTable JpegDefaultChrTable = new QuantizationTable(new byte[] {
            0x11, 0x12, 0x18, 0x2f, 0x63, 0x63, 0x63, 0x63,
            0x12, 0x15, 0x1a, 0x42, 0x63, 0x63, 0x63, 0x63,
            0x18, 0x1a, 0x38, 0x63, 0x63, 0x63, 0x63, 0x63,
            0x2f, 0x42, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63,
            0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63,
            0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63,
            0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63,
            0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63, 0x63
        });
    }
}