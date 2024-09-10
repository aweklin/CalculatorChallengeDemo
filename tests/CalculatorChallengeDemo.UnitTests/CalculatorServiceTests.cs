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

    [Fact]
    public void ConvertToDoubles_ShouldThrowException_WhenInputIsMoreThan2Numbers()
    {
        // given
        string input = "1,2,3";

        // when
        (double Operator1, double Operator2) ConvertToDoublesAction() =>
            _calculatorService.ConvertToDoubles(input);

        // then
        FluentActions
            .Invoking(ConvertToDoublesAction)
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData("1,", 1)]
    [InlineData(",", 0)]
    public void Add_ShouldReturnSummationOfTwoNumbers_WhenInputIsEmptyOrHasMissingNumbers(string input, double expectedResult)
    {
        // when
        (double operator1, double operator2) = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(operator1, operator2);

        // then
        expectedResult.Should().Be(actualResult);
    }
}