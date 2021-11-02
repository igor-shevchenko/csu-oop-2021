using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RomanNumerals.Tests
{
    [TestClass]
    public class RomanNumeralConverterTests
    {
        [TestMethod]
        public void ShouldConvertSingleDigitNumber()
        {
            Assert.AreEqual(1, new RomanNumeralConverter().Convert("I"));
            Assert.AreEqual(5, new RomanNumeralConverter().Convert("V"));
            Assert.AreEqual(500, new RomanNumeralConverter().Convert("D"));
        }

        [TestMethod]
        public void ShouldConvertDoubleDigitNumber()
        {
            Assert.AreEqual(2, new RomanNumeralConverter().Convert("II"));
            Assert.AreEqual(6, new RomanNumeralConverter().Convert("VI"));
            Assert.AreEqual(110, new RomanNumeralConverter().Convert("CX"));
        }

        [TestMethod]
        public void ShouldConvertMultipleDigitNumber()
        {
            Assert.AreEqual(116, new RomanNumeralConverter().Convert("CXVI"));
        }

        [TestMethod]
        public void ShouldConvertComplexNumber()
        {
            Assert.AreEqual(1994, new RomanNumeralConverter().Convert("MCMXCIV"));
        }
    }
}
