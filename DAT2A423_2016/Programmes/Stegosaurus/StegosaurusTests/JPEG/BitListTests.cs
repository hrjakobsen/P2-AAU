using NUnit.Framework;
using Stegosaurus;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

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
        public void BitListTest_Construction_Of_negative_length_Bitlist()
        {
            Assert.Catch<ArgumentOutOfRangeException>(() => new BitList(-1));
        }

        [Test()]
        public void Enumerable_MultipleValues_LoopsThroughAllAddedValues()
        {
            BitList bl = new BitList(8) {true};
            //Assert that the first 8 bits are false, and the last value is true
            Assert.True(!bl.Take(8).Any(bit => bit) && bl.Last()); 
        }

        [Test()]
        public void InsertTest_IndexInMiddle_InsertionOnIndex()
        {
            BitList bl = new BitList(8);

            bl.InsertAt(3, true);
            
            Assert.AreEqual(true, bl[3]);
        }

        [Test()]
        public void AddTest_IfAddBoolVal_AddsBit()
        {
            BitList bl = new BitList(8);
            
            bl.Add(true);

            Assert.AreEqual(true, bl[8]);
        }

        [Test()]
        public void AddTest_IfAddIntVal_AddsBit()
        {
            BitList bl = new BitList(8);

            bl.Add(1);

            Assert.AreEqual(true, bl[8]);
        }

        [Test()]
        public void CheckedAdd_AddedEigthTrueInARow_AddsEightZeroes() {
            const int numberOfStartElements = 5;
            BitList bl = new BitList(numberOfStartElements);

            for (int i = 0; i < 8; i++) {
                bl.CheckedAdd(1);
            }

            Assert.AreEqual(16 + numberOfStartElements, bl.Count);
            for (int i = 8 + numberOfStartElements; i < 16 + numberOfStartElements; i++) {
                Assert.False(bl[i]);
            }
        }

        [Test()]
        public void CheckedAdd_Added7TrueInARowAndOneFalse_DoesNotAddEightZeroes() {
            const int numberOfStartElements = 5;
            BitList bl = new BitList(numberOfStartElements);

            for (int i = 0; i < 7; i++) {
                bl.CheckedAdd(1);
            }
            bl.CheckedAdd(0);

            Assert.AreEqual(8 + numberOfStartElements, bl.Count);
            for (int i = 8 + numberOfStartElements; i < 8 + numberOfStartElements; i++) {
                Assert.False(bl[i]);
            }
        }

    }
}