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
    public class BitListTests
    {
        [Test()]
        public void BitListTest()
        {
            BitList bl = new BitList();

            Assert.AreEqual(0, bl.Count);
        }

        [Test()]
        public void BitListTest1()
        {
            BitList bl = new BitList(1);

            Assert.AreEqual(1, bl.Count);
        }

        [Test()]
        public void GetEnumeratorTest() //TODO: make test which checks if yield return works, so bl should return with 8 zeros?
        {
            BitList bl = new BitList(8);

            bl.GetEnumerator();
            Assert.Fail();
        }

        [Test()]
        public void InsertTest()
        {
            BitList bl = new BitList(8);

            bl.Insert(3, true);
            
            Assert.AreEqual(true, bl[3]);
        }

        [Test()]
        public void AddTest() //TODO: Seems like Add() changes the last value in bitlist and does not add a bit to the bitlist. Don't know if this is intended
        {
            BitList bl = new BitList(8);

            bl.Add(true);

            Assert.AreEqual(true, bl[9]);
        }

        [Test()]
        public void AddTest1()
        {
            BitList bl = new BitList(8);

            bl.Add(1);

            Assert.AreEqual(true, bl[9]);
        }
    }
}