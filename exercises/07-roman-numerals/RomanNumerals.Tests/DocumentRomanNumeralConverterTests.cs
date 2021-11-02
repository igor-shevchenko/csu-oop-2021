using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace RomanNumerals.Tests
{
    [TestClass]
    public class DocumentRomanNumeralConverterTests
    {
        [TestMethod]
        public void ShouldConvertNumbersFromFile()
        {
            // Arrange
            var filename = "file.txt";
            var document = "I VI";
            var result = new List<int> {1, 6};

            var documentReader = new Mock<IDocumentReader>();
            var romanConverter = new Mock<IRomanNumeralConverter>();

            documentReader.Setup(d => d.ReadDocument(It.Is<string>(s => s == filename))).Returns(document);
            romanConverter.Setup(r => r.Convert(It.Is<string>(s => s == "I"))).Returns(1);
            romanConverter.Setup(r => r.Convert(It.Is<string>(s => s == "VI"))).Returns(6);

            // Act
            var documentConverter = new DocumentRomanNumeralConverter(romanConverter.Object, documentReader.Object);
            var actualResult = documentConverter.ConvertDocument(filename);

            // Assert
            CollectionAssert.AreEqual(result, actualResult);
            documentReader.VerifyAll();
            romanConverter.VerifyAll();

        }
    }
}