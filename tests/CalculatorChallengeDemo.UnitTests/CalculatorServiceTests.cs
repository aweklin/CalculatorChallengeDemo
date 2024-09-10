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
    public void Add_ShouldReturnSummationOfTwoNumbers_WhenInputHasOnlyNumbers()
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

    [Fact]
    public void Add_ShouldReturnSummationOfTwoNumbers_WhenInputIncludesNewLineCharacterAsAnAlternativeDelimiter()
    {
        // given
        string input = "1\n2,3";
        double expectedResult = 6;

        // when
        IEnumerable<double> numbers = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(numbers);

        // then
        actualResult.Should().Be(expectedResult);
    }

    [Fact]
    public void ConvertToDoubles_ShouldThrowException_WhenInputContainsANegativeNumber()
    {
        // given
        string input = "4,-3";
        string expectedErrorMessage = "No negative number is allowed. Found the following negative numbers: -3";

        // when
        IEnumerable<double> ConvertToDoublesAction() => _calculatorService.ConvertToDoubles(input);

        // then
        FluentActions
            .Invoking(ConvertToDoublesAction)
            .Should()
            .Throw<ArithmeticException>()
            .WithMessage(expectedErrorMessage);
    }

    [Fact]
    public void Add_ShouldMakeAnyValueGreaterThan1000InvalidAndSumTheRest_WhenInputContainsANumberGreaterThan1000()
    {
        // given
        string input = "2,1001,6";
        double expectedResult = 8;

        // when
        IEnumerable<double> numbers = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(numbers);

        // then
        actualResult.Should().Be(expectedResult);
    }

    [Theory]
    [InlineData("//#\n2#5", 7)]
    [InlineData("//,\n2,ff,100", 102)]
    public void Add_ShouldSupport1CustomDelimiterOfASingleCharacter_WhenInputContainsACustomDelimiter(string input, double expectedResult)
    {
        // when
        IEnumerable<double> numbers = _calculatorService.ConvertToDoubles(input);
        double actualResult = _calculatorService.Add(numbers);

        // then
        actualResult.Should().Be(expectedResult);
    }
}