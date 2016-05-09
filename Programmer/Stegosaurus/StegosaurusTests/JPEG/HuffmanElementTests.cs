using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class HuffmanElementTests
    {
        /* TODO: Find out how to test huffman elements correctly
        public HuffmanElement huffmanTestElement1;
        public HuffmanElement huffmanTestElement2;

        [SetUp()]
        public void Huffelements()
        {
            byte runSizeInput1 = (byte)00000000;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = (byte)8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            byte runSizeInput2 = Byte.Parse("11111111");
            ushort codeWordInput2 = UInt16.Parse("00000111");
            byte lengthInput2 = Byte.Parse("0000100");

            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);
        }
        */

        [Test()]
        public void CompareToTest1()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = 8;

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3;
            byte lengthInput2 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            var check = huffmanTestElement1.CompareTo(huffmanTestElement2);
            Assert.AreEqual(-1, check);
        }

        [Test()]
        public void CompareToTest2()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = 7;

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3;
            byte lengthInput2 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            var check = huffmanTestElement1.CompareTo(huffmanTestElement2);
            Assert.AreEqual(-1, check);
        }

        [Test()]
        public void ToStringTest()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            Assert.AreEqual("0 = 10, 1000", huffmanTestElement1.ToString());
        }
    }
}