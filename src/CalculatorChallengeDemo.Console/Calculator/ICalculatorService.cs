namespace CalculatorChallengeDemo.Console.Calculator;

public interface ICalculatorService
{
    (double Operator1, double Operator2) ConvertToDoubles(string? value);
    double Add(double operator1, double operator2);
}