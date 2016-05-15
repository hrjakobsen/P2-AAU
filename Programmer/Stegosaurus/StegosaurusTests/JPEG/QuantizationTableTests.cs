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
            string expectedString = "16\t11\t10\t16\t24\t40\t51\t61\t\n" +
                                    "12\t12\t14\t19\t26\t58\t60\t55\t\n" +
                                    "14\t13\t16\t24\t40\t57\t69\t56\t\n" +
                                    "14\t17\t22\t29\t51\t87\t80\t62\t\n" +
                                    "18\t22\t37\t56\t68\t109\t103\t77\t\n" +
                                    "24\t35\t55\t64\t81\t104\t113\t92\t\n" +
                                    "49\t64\t78\t87\t103\t121\t120\t101\t\n" +
                                    "72\t92\t95\t98\t112\t100\t103\t99\t\n";

            NUnit.Framework.Assert.AreEqual(expectedString, defaultYQuantizationTable.ToString());
        }
    }
}