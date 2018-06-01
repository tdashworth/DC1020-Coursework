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
            Assert.AreEqual(1.011968375907138, CalculatorParser.Resolve("3+sin(30)-1"));
        }
    }
}
