using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class QuantizationTableTests
    {
        [Test()]
        public void QuantizationTableTest_Zig_Zag_Entries()
        {
            QuantizationTable defaultYQuantizationTable = QuantizationTable.JpegDefaultYTable;
            byte[] zigZagEntries = defaultYQuantizationTable.ZigzagEntries;
            byte[] expectedTable =
            {
                16, 11, 12, 14, 12, 10, 16, 14,
                13, 14, 18, 17, 16, 19, 24, 40,
                26, 24, 22, 22, 24, 49, 35, 37,
                29, 40, 58, 51, 61, 60, 57, 51,
                56, 55, 64, 72, 92, 78, 64, 68,
                87, 69, 55, 56, 80, 109, 81, 87,
                95, 98, 103, 104, 103, 62, 77, 113,
                121, 112, 100, 120, 92, 101, 103, 99
            };

            NUnit.Framework.Assert.AreEqual(expectedTable, zigZagEntries);

        }

        [Test()]
        public void Scale_MultiplierInRange_ScalesTable()
        {
            QuantizationTable defaultYQuantizationTable = QuantizationTable.JpegDefaultYTable;
            QuantizationTable scaledDefaultYQuantizationTable = defaultYQuantizationTable.Scale(100);

            byte[] expectedTable = {
                2,   1,   1,   2,   3,   5,   6,   7,
                1,   1,   1,   2,   3,   7,   7,   6,
                1,   1,   2,   3,   5,   7,   8,   7,
                1,   2,   2,   3,   6,   10,  10,  7,
                2,   2,   4,   7,   8,   13,  12,  9,
                3,   4,   6,   8,   10,  13,  14,  11,
                6,   8,   9,   10,  12,  15,  15,  12,
                9,   11,  11,  12,  14,  12,  12,  12
            };

            NUnit.Framework.Assert.AreEqual(expectedTable, scaledDefaultYQuantizationTable.Entries);
        }

        [Test()]
        public void Scale_NegativeMultiplier_ThrowsException() {
            QuantizationTable defaultYQuantizationTable = QuantizationTable.JpegDefaultYTable;

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => defaultYQuantizationTable.Scale(-4));
            StringAssert.Contains("Quality must be in the range [0,100]", ex.Message);
        }

        [Test()]
        public void Scale_MultiplierAbove100_ThrowsException() {
            QuantizationTable defaultYQuantizationTable = QuantizationTable.JpegDefaultYTable;

            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(() => defaultYQuantizationTable.Scale(101));
            StringAssert.Contains("Quality must be in the range [0,100]", ex.Message);
        }

        [Test()]
        public void QuantizationToStringTest()
        {
            QuantizationTable defaultYQuantizationTable = QuantizationTable.JpegDefaultYTable;
            string expectedString = "16;11;10;16;24;40;51;61;" +
                                    "12;12;14;19;26;58;60;55;" +
                                    "14;13;16;24;40;57;69;56;" +
                                    "14;17;22;29;51;87;80;62;" +
                                    "18;22;37;56;68;109;103;77;" +
                                    "24;35;55;64;81;104;113;92;" +
                                    "49;64;78;87;103;121;120;101;" +
                                    "72;92;95;98;112;100;103;99;";

            NUnit.Framework.Assert.AreEqual(expectedString, defaultYQuantizationTable.ToString());
        }
    }
}