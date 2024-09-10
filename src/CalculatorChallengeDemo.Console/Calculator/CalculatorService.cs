using System.Buffers;
using System.Globalization;

namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    private const char _comma = ',';
    private static readonly char[] _allowedAlternativeDelimiters = new char[] { '\n' };

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
        return numbers.Sum();
    }

    private static string[] SplitInput(string input)
    {
        string result = input;

        foreach (var allowedCharacter in _allowedAlternativeDelimiters)
        {
            if (input.Contains(allowedCharacter, StringComparison.OrdinalIgnoreCase))
            {
                result = input.Replace(allowedCharacter, _comma);
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