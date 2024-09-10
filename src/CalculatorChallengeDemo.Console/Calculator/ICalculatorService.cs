namespace CalculatorChallengeDemo.Console.Calculator;

public interface ICalculatorService
{
    IEnumerable<double> ConvertToDoubles(string value);
    double Add(IEnumerable<double> numbers);
}