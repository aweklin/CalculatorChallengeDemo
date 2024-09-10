using System.Buffers;
using System.Globalization;

namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    public IEnumerable<double> ConvertToDoubles(string value)
    {
        string[] valuesArray = value.Split(",");

        if (valuesArray.Length == 0)
        {
            return Enumerable.Empty<double>();
        }

        List<double> result = new();
        foreach (var item in valuesArray)
        {
            if (string.IsNullOrWhiteSpace(item))
            {
                result.Add(0);
                continue;
            }

            if (double.TryParse(item, out double number))
            {
                result.Add(number);
            }
        }

        return result;
    }

    public double Add(IEnumerable<double> numbers)
    {
        return numbers.Sum();
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