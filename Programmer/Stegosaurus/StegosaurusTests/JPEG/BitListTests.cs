﻿using NUnit.Framework;
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

            NUnit.Framework.Assert.AreEqual(0, bl.Count);
        }

        [Test()]
        public void BitListTest_Construction_Of_One_Length_Bitlist()
        {
            BitList bl = new BitList(1);

            NUnit.Framework.Assert.AreEqual(1, bl.Count);
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
        public void InsertTest_Insertion_On_Index()
        {
            BitList bl = new BitList(8);

            bl.Insert(3, true);
            
            NUnit.Framework.Assert.AreEqual(true, bl[3]);
        }

        [Test()]
        public void AddTest_If_Add_Bool_Val_Adds_bit()
        {
            BitList bl = new BitList(8);

            bl.Add(true);

            NUnit.Framework.Assert.AreEqual(true, bl[8]);
        }

        [Test()]
        public void AddTest_If_Add_Int_Val_Adds_bit()
        {
            BitList bl = new BitList(8);

            bl.Add(1);

            NUnit.Framework.Assert.AreEqual(true, bl[8]);
        }
    }
}