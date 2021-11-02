using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RomanNumerals
{
    public class DocumentReader : IDocumentReader
    {
        public string ReadDocument(string filename)
        {
            return File.ReadAllText(filename);
        }
    }

    public class DocumentRomanNumeralConverter
    {
        private readonly IRomanNumeralConverter _converter;
        private readonly IDocumentReader _reader;

        public DocumentRomanNumeralConverter(IRomanNumeralConverter converter, IDocumentReader reader)
        {
            _converter = converter;
            _reader = reader;
        }

        public List<int> ConvertDocument(string filename)
        {
            var fileContents = _reader.ReadDocument(filename);
            var numbers = fileContents.Split('\n', ' ');
            return numbers.Select(_converter.Convert).ToList();
        }
    }
}