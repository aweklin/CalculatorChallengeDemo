using System.Globalization;

namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    public (double Operator1, double Operator2) ConvertToDoubles(string value)
    {
        string[] valuesArray = value.Split(",");
        if (valuesArray.Length > 2)
        {
            throw new InvalidOperationException();
        }

        switch (valuesArray.Length)
        {
            case 0:
                return (Operator1: 0, Operator2: 0);
            case 1:
                return (Operator1: ParseDouble(valuesArray[0]), Operator2: 0);
            default:
                return (Operator1: ParseDouble(valuesArray[0]), Operator2: ParseDouble(valuesArray[1]));
        }
    }

    public double Add(double operator1, double operator2)
    {
        return operator1 + operator2;
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