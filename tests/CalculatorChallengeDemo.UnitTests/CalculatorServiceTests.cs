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
}