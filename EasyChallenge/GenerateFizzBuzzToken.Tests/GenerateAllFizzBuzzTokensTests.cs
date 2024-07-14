using EasyChallenge;

namespace FizzBuzzSolver.Tests;

public class GenerateAllFizzBuzzTokensTests
{
    [Fact]
    public void GetAllTokens_DefaultConstructor_Returns100Items()
    {
        var actual = new GenerateAllFizzBuzzTokens().GetAllTokens();
        
        Assert.Equal(100, actual.Count);
    }

    [Theory]
    [InlineData("1", 1)]
    [InlineData("Fizz", 3)]
    [InlineData("Buzz", 100)]
    public void GetAllTokens_DefaultConstructor_ReturnsTheCorrectValueForDifferentInputs(string expectedValue, int listPosition)
    {
        var actual = new GenerateAllFizzBuzzTokens().GetAllTokens();

        Assert.Equal(expectedValue, actual[listPosition - 1]);
    }

    [Theory]
    [InlineData(1, 5, 5)]
    [InlineData(-2, 5, 8)]
    [InlineData(-10, -4, 7)]
    public void GetAllTokens_CustomSequenceConstructor_ReturnsTheCorrectLength(int startNumber, int finalNumber, int expectedResultLength)
    {
        var actual = new GenerateAllFizzBuzzTokens(startNumber, finalNumber).GetAllTokens();

        Assert.Equal(expectedResultLength, actual.Count);
    }

    [Fact]
    public void GetAllTokens_InvalidSequenceWithStartNumberBiggerThanFinalNumber_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new GenerateAllFizzBuzzTokens(1, 0).GetAllTokens());
    }

    [Fact]
    public void GetAllTokens_InvalidSequenceWithStartNumberBiggerThanFinalNumber_ThrowsExceptionWithTheExpectedMessage()
    {
        var exception = Assert.Throws<ArgumentException>(() => new GenerateAllFizzBuzzTokens(1, 0).GetAllTokens());
        Assert.Equal("The start number must be lesser than or equal to the final number", exception.Message);
    }

}
