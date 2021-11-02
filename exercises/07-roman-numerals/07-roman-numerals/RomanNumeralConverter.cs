using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumerals
{
    public class RomanNumeralConverter : IRomanNumeralConverter
    {
        private Dictionary<char, int> _numbers = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000},
        };

        public int Convert(string roman)
        {
            var result = 0;
            for (var i = 0; i < roman.Length; ++i)
            {
                var currentDigit = _numbers[roman[i]];
                if (i != roman.Length - 1 && currentDigit < _numbers[roman[i + 1]])
                {
                    result -= currentDigit;
                }
                else
                {
                    result += currentDigit;
                }
            }

            return result;
        }
    }
}
