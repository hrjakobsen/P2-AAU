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
    public class HuffmanTableTests
    {
        [Test()]
        public void HuffmanTableTest_Access_to_codeword()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);
            

            NUnit.Framework.Assert.AreEqual(2, huffTable1.Elements[00000000].CodeWord);
        }

        [Test()]
        public void CombinationsTest() //TODO: Maybe use a loop to check every element
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            byte[] numberOfCodesOutput = huffTable1.Combinations();

            NUnit.Framework.Assert.AreEqual(2, numberOfCodesOutput[7]);
        }

        [Test()]
        public void HasCodeTest1()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(0, huffTable1.HasCode(2, 8).RunSize);
        }

        [Test()]
        public void HasCodeTest_for_null_when_no_element_has_code()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(null, huffTable1.HasCode(1, 8));
        }

        [Test()]
        public void ToStringTest()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual("[0, 0 = 10, 1000]\n[1, 1 = 11, 1000]\n", huffTable1.ToString());
        }

        [Test()]
        public void GetElementFromRunSizeTest() //TODO: Check if GetElementFromRunSize is meant to work like this
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 as byte
            byte lengthInput1 = 8; // 1000 as byte

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 as byte
            byte lengthInput2 = 8; // 1000 as byte

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(huffmanTestElement2, huffTable1.GetElementFromRunSize(0, 1));
        }
    }
}