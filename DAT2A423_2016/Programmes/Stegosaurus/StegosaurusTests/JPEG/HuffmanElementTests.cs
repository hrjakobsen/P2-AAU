﻿using NUnit.Framework;
using Stegosaurus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class HuffmanElementTests
    {
        [Test()]
        public void Comparison_HuffmanElementWithDifferentRunSizes_SmallerRunSizeBeforeLarger()
        {
            byte runSizeInput1 = 0x00;
            ushort codeWordInput1 = 0x02;
            byte lengthInput1 = 8;

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3;
            byte lengthInput2 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);
            
            Assert.AreEqual(-1, huffmanTestElement1.CompareTo(huffmanTestElement2));
        }

        [Test()]
        public void Comparison_HuffmanElementWithDifferentLengths_ShorterLengthBeforeLonger()
        {
            byte runSizeInput1 = 0x00;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = 7;

            byte runSizeInput2 = 0x01;
            ushort codeWordInput2 = 3;
            byte lengthInput2 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            var check = huffmanTestElement1.CompareTo(huffmanTestElement2);
            Assert.AreEqual(-1, check);
        }

        [Test()]
        public void HuffmanElementToString_Test()
        {
            byte runSizeInput1 = 0x00;
            ushort codeWordInput1 = 2;
            byte lengthInput1 = 8;

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);

            Assert.AreEqual("0 = 10, 1000", huffmanTestElement1.ToString());
        }

        [Test]
        public void EqualsTest()
        {
            HuffmanElement ele1 = new HuffmanElement(0x00, 2, 8);
            HuffmanElement ele2 = new HuffmanElement(0x01, 5, 3);
            Assert.IsFalse(ele1.Equals(ele2));

            HuffmanElement ele3 = new HuffmanElement(0x00, 2, 8);
            HuffmanElement ele4 = new HuffmanElement(0x00, 2, 8);
            Assert.IsTrue(ele3.Equals(ele4));
        }
    }
}