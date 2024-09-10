using System.Buffers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    private const char _comma = ',';
    private const char _newLine = '\n';
    private const double _validNumberThreashold = 1000;
    private static readonly char[] _allowedAlternativeDelimiters = new char[] { _newLine };
    private static readonly char[] _invalidDelimiters = new char[] { ' ', '/', '[', ']', _comma };

    public IEnumerable<double> ConvertToDoubles(string value)
    {
        string[] valuesArray = SplitInput(value);

        if (valuesArray.Length == 0)
        {
            return Enumerable.Empty<double>();
        }

        List<double> result = new();
        List<double> badNumbers = new();
        foreach (var item in valuesArray)
        {
            if (double.TryParse(item, out double number) && number > 0)
            {
                result.Add(number);
                continue;
            }

            if (number < 0)
            {
                badNumbers.Add(number);
            }
        }

        if (badNumbers.Count > 0)
        {
            throw new ArithmeticException("No negative number is allowed. Found the following negative numbers: " + string.Join(',', badNumbers));
        }

        return result;
    }

    public double Add(IEnumerable<double> numbers)
    {
        return numbers.Where(x => x < _validNumberThreashold).Sum();
    }

    private static string[] SplitInput(string input)
    {
        string result = input;

        // allowed alternative delimiters
        foreach (var allowedCharacter in _allowedAlternativeDelimiters)
        {
            if (input.Contains(allowedCharacter, StringComparison.OrdinalIgnoreCase))
            {
                result = input.Replace(allowedCharacter, _comma);
            }
        }

        // custom delimiter character
        if (result.AsSpan().Trim().StartsWith("//"))
        {
            // eliminate numbers surounded by alphabets
            string pattern = @"(?<=[a-zA-Z])\d(?=[a-zA-Z])";
            result = Regex.Replace(input, pattern, ",").Replace(_newLine, _comma);

            IEnumerable<char> customDelimiterCharacters = result
                .Where(x => !char.IsDigit(x))
                .Where(x => !((int)x >= 65 && (int)x <= 90))
                .Where(x => !((int)x >= 97 && (int)x <= 112))
                .Where(x => !_invalidDelimiters.Contains(x))
                .Where(x => !_allowedAlternativeDelimiters.Contains(x))
                .Distinct()
                .ToArray();
            if (customDelimiterCharacters.Any())
            {
                foreach (char customDelimiterCharacter in customDelimiterCharacters)
                {
                    result = result.Replace(customDelimiterCharacter, _comma);
                }
            }
        }

        return result.Split(_comma);
    }

    private static double ParseDouble(string input)
    {
        if (double.TryParse(input, out double result))
        {
            return result;
        }

        return 0;
    }
}