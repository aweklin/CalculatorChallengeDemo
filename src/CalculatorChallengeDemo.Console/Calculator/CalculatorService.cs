using System.Buffers;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    private const char _comma = ',';
    private const char _newLine = '\n';
    private const double _validNumberThreshold = 1001;
    private static readonly char[] _allowedAlternativeDelimiters = new char[] { _newLine };

    public double Add(IEnumerable<double> numbers)
    {
        return numbers.Where(x => x < _validNumberThreshold).Sum();
    }

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

        // ======== custom delimiter character ======

        // eliminate numbers surrounded by alphabets
        string pattern = @"(?<=[a-zA-Z])\d(?=[a-zA-Z])";
        result = Regex.Replace(input, pattern, ",").Replace(_newLine, _comma);

        // allow custom delimiters
        IEnumerable<char> customDelimiterCharacters = result
            .Where(x => !char.IsDigit(x))
            .Where(x => x != '-')
            .Where(x => x != ',')
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

        return result.Split(_comma);
    }
}