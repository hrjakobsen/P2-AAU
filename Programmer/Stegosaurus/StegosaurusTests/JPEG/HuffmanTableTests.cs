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
        public void HuffmanTable_Access_to_codeword_Test()
        {
            byte runSizeInput1 = 0x00;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 0x1;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2


            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);
            

            NUnit.Framework.Assert.AreEqual(2, huffTable1.Elements[0x00].CodeWord);
        }

        [Test()]
        public void Combinations_Test() //TODO: Maybe use a loop to check every element
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2

            byte runSizeInput3 = 2;
            ushort codeWordInput3 = 4; // 100 in base 2
            byte lengthInput3 = 8; // 1000 in base 2

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);
            HuffmanElement huffmanTestElement3 = new HuffmanElement(runSizeInput3, codeWordInput3, lengthInput3);
            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2, huffmanTestElement3);

            byte[] numberOfCodesOutput = huffTable1.Combinations();

            NUnit.Framework.Assert.AreEqual(3, numberOfCodesOutput[7]);
        }

        [Test()]
        public void HasCode_Test()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(0, huffTable1.HasCode(2, 8).RunSize);
        }

        [Test()]
        public void HasCodeTest_for_null_when_no_element_has_code()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(null, huffTable1.HasCode(1, 8));
        }

        [Test()]
        public void HuffmanTableToString_Test()
        {
            byte runSizeInput1 = 00000000;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 1;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual("0,2,8;1,3,8;", huffTable1.ToString());
        }

        [Test()]
        public void GetElementFromRunSize_Test()
        {
            byte runSizeInput1 = 0x00;
            ushort codeWordInput1 = 2; // 10 in base 2
            byte lengthInput1 = 8; // 1000 in base 2

            byte runSizeInput2 = 0x01;
            ushort codeWordInput2 = 3; // 11 in base 2
            byte lengthInput2 = 8; // 1000 in base 2

            HuffmanElement huffmanTestElement1 = new HuffmanElement(runSizeInput1, codeWordInput1, lengthInput1);
            HuffmanElement huffmanTestElement2 = new HuffmanElement(runSizeInput2, codeWordInput2, lengthInput2);

            HuffmanTable huffTable1 = new HuffmanTable(huffmanTestElement1, huffmanTestElement2);

            NUnit.Framework.Assert.AreEqual(huffmanTestElement2, huffTable1.GetElementFromRunSize(0x0, 0x1)); //combines to 0x01
        }
    }
}