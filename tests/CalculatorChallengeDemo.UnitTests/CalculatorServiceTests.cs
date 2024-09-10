using CalculatorChallengeDemo.Console.Calculator;

using FluentAssertions;

using NSubstitute;

namespace CalculatorChallengeDemo.UnitTests;

public class CalculatorServiceTests
{
#pragma warning disable CA1859
    private readonly ICalculatorService _calculatorService;
#pragma warning restore CA1859
    public CalculatorServiceTests()
    {
        _calculatorService = new CalculatorService();
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("1,", 1)]
    [InlineData(",", 0)]
    public void Add_ShouldReturnSummationOfTwoNumbers_WhenInputIsEmptyOrHasMissingNumbers(string input, double expectedResult)
    {
        // when
        IEnumerable<double> numbers = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(numbers);

        // then
        expectedResult.Should().Be(actualResult);
    }

    [Fact]
    public void Add_ShouldReturnListOfNumbers_WhenInputHasOnlyNumbers()
    {
        // given
        string input = "1,2,3,4,5,6,7,8,9,10,11,12";
        double expectedResult = 78;

        // when
        IEnumerable<double> numbers = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(numbers);

        // then
        actualResult.Should().Be(expectedResult);
    }
}