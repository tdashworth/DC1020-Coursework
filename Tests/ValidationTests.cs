using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scientific_Calculator;

namespace Tests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        public void TestEmptyString()
        {
            Assert.AreEqual(0, CalculatorParser.Resolve(""));
        }

        [TestMethod]
        public void TestMissingOperand()
        {
            try
            {
                CalculatorParser.Resolve("1+");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid expression", ex.Message);
            }
        }

        [TestMethod]
        public void TestInvalidCharacter()
        {
            try
            {
                CalculatorParser.Resolve("1+O");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid expression", ex.Message);
            }
        }

        [TestMethod]
        public void TestSimpleExpression()
        {
            try
            {
                CalculatorParser.Resolve("1+1");
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TestAdvancedExpression()
        {
            try
            {
                CalculatorParser.Resolve("5+4-3*2/1");
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

    }
}
