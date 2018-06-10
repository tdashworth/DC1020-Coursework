using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scientific_Calculator;

namespace Tests
{
    [TestClass]
    public class AdvancedCalculationTests
    {
        [TestMethod]
        public void TestTrigFunction()
        {
            Assert.AreEqual(3 + Math.Sin(30) - 1, CalculatorParser.Resolve("3+sin(30)-1"));
            Assert.AreEqual(Math.Sin(330), CalculatorParser.Resolve("sin(330)"));
            Assert.AreEqual(Math.Sin(-330), CalculatorParser.Resolve("sin(-330)"));

            Assert.AreEqual(3 + Math.Cos(30) - 1 * 5, CalculatorParser.Resolve("3+cos(30)-1*5"));
            Assert.AreEqual(Math.Cos(300), CalculatorParser.Resolve("cos(300)"));
            Assert.AreEqual(Math.Cos(-300), CalculatorParser.Resolve("cos(-300)"));
        }

        [TestMethod]
        public void TestOrderOfOperations()
        {
            Assert.AreEqual(5 + 3 * 6, CalculatorParser.Resolve("5+3*6"));
            Assert.AreEqual(7 * 3 - 10 / 2, CalculatorParser.Resolve("7*3-10/2"));
            Assert.AreEqual(4 * 6 / 3, CalculatorParser.Resolve("4*6/3")); // (4*6)/3
            Assert.AreEqual(4 * 5 + 4 * 3, CalculatorParser.Resolve("4*5+4*3")); // (4*5) + (4*3)
            Assert.AreEqual(Math.Pow(4, 2) / 2, CalculatorParser.Resolve("4^2/2"));
            Assert.AreEqual(7 - 4 + 6 - 2, CalculatorParser.Resolve("7-4+6-2"));
            Assert.AreEqual(5 + 10 * 5, CalculatorParser.Resolve("5+10*5")); // 5+(10*5)
            Assert.AreEqual(8 + Math.Pow(5, 2) - 9, CalculatorParser.Resolve("8+5^2-9"));
            Assert.AreEqual(6 * 5 / 10 - 7, CalculatorParser.Resolve("6*5/10-7")); // (6*5) / (10-7)
            Assert.AreEqual(8 - 5 * Math.Pow(2, 2), CalculatorParser.Resolve("8-5*2^2"));
            Assert.AreEqual(2 + 3 * 5 - 4, CalculatorParser.Resolve("2+3*5-4"));
        }

        [TestMethod]
        public void TestBrackets()
        {
            Assert.AreEqual((4 * 5) + (4 * Math.Sin(3)) / Math.Cos(2), CalculatorParser.Resolve("(4*5) + (4*sin(3)) / cos(2)"));
            Assert.AreEqual(1 + (2 - (Math.Pow(3, 2) * 4) / 10), CalculatorParser.Resolve("1+(2-((3^2)*4)/10)"));
        }
    }
}
