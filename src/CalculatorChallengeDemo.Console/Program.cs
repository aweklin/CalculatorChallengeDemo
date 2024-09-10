using CalculatorChallengeDemo.Console.Calculator;

using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<ICalculatorService, CalculatorService>();

string input = string.Empty;
ICalculatorService calculatorService = new CalculatorService();

while (true)
{
    Console.Write("Please enter 2 numbers separated with comma: ");
    input = Console.ReadLine() ?? string.Empty;
    Console.WriteLine($"You entered {input}");
}