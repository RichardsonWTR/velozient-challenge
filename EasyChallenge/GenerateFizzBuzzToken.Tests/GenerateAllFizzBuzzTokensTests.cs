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

    
}
