namespace CalculatorChallengeDemo.Console.Calculator;

internal sealed class CalculatorService : ICalculatorService
{
    public (double Operator1, double Operator2) ConvertToDoubles(string? value)
    {
        string[] valuesArray = value!.Split(",");
        if (valuesArray.Length > 2)
        {
            throw new InvalidOperationException();
        }

        return (Operator1: 0, Operator2: 0);
    }

    public double Add(double operator1, double operator2)
    {
        throw new NotImplementedException();
    }
}