using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scientific_Calculator;

namespace Tests
{
    [TestClass]
    public class AdvancedCalculationTests
    {
        [TestMethod]
        public void TestSinFunction()
        {
            Assert.AreEqual(3 + Math.Sin(30) - 1, CalculatorParser.Resolve("3+sin(30)-1"));
        }

        [TestMethod]
        public void TestCosFunction()
        {
            Assert.AreEqual(3 + Math.Cos(30) - 1 * 5, CalculatorParser.Resolve("3+cos(30)-1*5"));
        }
    }
}
