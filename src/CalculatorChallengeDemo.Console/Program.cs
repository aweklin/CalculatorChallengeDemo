using CalculatorChallengeDemo.Console.Calculator;

using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<ICalculatorService, CalculatorService>();

string input = string.Empty;
#pragma warning disable CA1859
ICalculatorService calculatorService = new CalculatorService();
#pragma warning restore CA1859

Console.WriteLine("Welcome to Calculator Challenge!");
Console.WriteLine(
        """
        Kindly note the following: 
            1. No negative number is allowed
            2. Only numbers below 1001 is added
            3. You can enter numbers separated with comma or any delimiter of your choice
        """);

while (true)
{
    Console.Write("What numbers would you like to add: ");
    input = Console.ReadLine() ?? string.Empty;
    try
    {
        IEnumerable<double> numbersFoundInTheStringInput = calculatorService.ConvertToDoubles(input);
        double result = calculatorService.Add(numbersFoundInTheStringInput);
        Console.WriteLine($"The addition of numbers entered is {result}");
    }
    catch (ArithmeticException exception)
    {
        Console.WriteLine(exception.Message);
    }
}