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
    public class ExtensionMethodsTests
    {
        [TestCase(-5, 3, ExpectedResult = 1)]
        [TestCase(5, -3, ExpectedResult = -1)]
        [TestCase(5, 3, ExpectedResult = 2)]
        [TestCase(3, 4, ExpectedResult = 3)]
        [TestCase(0, 3, ExpectedResult = 0)]
        [TestCase(-53, 3, ExpectedResult = 1)]
        [TestCase(-53, 53, ExpectedResult = 0)]
        [TestCase(53, -53, ExpectedResult = 0)] 
        public int ModTest(int dividend, int divisor)
        {
            return ExtensionMethods.Mod(dividend, divisor);
        }
    }
}