using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scientific_Calculator;

namespace Tests
{
    [TestClass]
    public class BasicCalculationTests
    {
        [TestMethod]
        public void TestSimpleAddition()
        {
            Assert.AreEqual(5, CalculatorParser.Resolve("3+2"));
        }

        [TestMethod]
        public void TestSimpleSubtraction()
        {
            Assert.AreEqual(5, CalculatorParser.Resolve("15-10"));
        }

        [TestMethod]
        public void TestSimpleMultiplcation()
        {
            Assert.AreEqual(50, CalculatorParser.Resolve("10*5"));
        }

        [TestMethod]
        public void TestSimpleDivision()
        {
            Assert.AreEqual(5, CalculatorParser.Resolve("10/2"));
        }
        [TestMethod]
        public void TestAdvancedAddition()
        {
            Assert.AreEqual(15, CalculatorParser.Resolve("10+3+2"));
        }

        [TestMethod]
        public void TestAdvancedSubtraction()
        {
            Assert.AreEqual(5, CalculatorParser.Resolve("30-15-10"));
        }

        [TestMethod]
        public void TestAdvancedMultiplcation()
        {
            Assert.AreEqual(50, CalculatorParser.Resolve("2*5*5"));
        }

        [TestMethod]
        public void TestAdvancedDivision()
        {
            Assert.AreEqual(5, CalculatorParser.Resolve("50/5/2"));
        }

        [TestMethod]
        public void TestExpression()
        {
            Assert.AreEqual(25, CalculatorParser.Resolve("50/5*2+5"));
        }
    }
}
