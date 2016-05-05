using NUnit.Framework;
using Stegosaurus;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stegosaurus.Tests
{
    [TestFixture()]
    public class BitListTests
    {
        [Test()]
        public void BitListTest_Construction_Of_Zero_Length_Bitlist()
        {
            BitList bl = new BitList();

            Assert.AreEqual(0, bl.Count);
        }

        [Test()]
        public void BitListTest_Construction_Of_One_Length_Bitlist()
        {
            BitList bl = new BitList(1);

            Assert.AreEqual(1, bl.Count);
        }

        [Test()]
        public void BitListTest_Construction_Of_negative_length_Bitlist() //TODO: do checks on this test
        {
            try
            {
                BitList bl = new BitList(-1);
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }

        }

        [Test()]
        public void GetEnumeratorTest() //TODO: make test which checks if yield return works, so bl should return with 8 zeros?
        {
            BitList bl = new BitList(8);

            bl.GetEnumerator();
            Assert.Fail();
        }

        [Test()]
        public void InsertTest_Insertion_On_Index()
        {
            BitList bl = new BitList(8);

            bl.Insert(3, true);
            
            Assert.AreEqual(true, bl[3]);
        }

        [Test()]
        public void AddTest_If_Add_Bool_Val_Adds_bit()
        {
            BitList bl = new BitList(8);

            bl.Add(true);

            Assert.AreEqual(true, bl[8]);
        }

        [Test()]
        public void AddTest_If_Add_Int_Val_Adds_bit()
        {
            BitList bl = new BitList(8);

            bl.Add(1);

            Assert.AreEqual(true, bl[8]);
        }
    }
}