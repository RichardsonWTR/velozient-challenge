using EasyChallenge;

namespace FizzBuzzTokenTests;

public class GenerateFizzBuzzTokenTests
{
    [Fact]
    public void GetToken_Input3_ReturnsFizz()
    {
        var actual = new GenerateFizzBuzzToken().GetToken(3);
        Assert.Equal("Fizz", actual);
    }

    [Fact]
    public void GetToken_InputMultipleOf3_ReturnsFizz()
    {
        var actual = new GenerateFizzBuzzToken().GetToken(6);
        Assert.Equal("Fizz", actual);
    }

    [Fact]
    public void GetToken_Input5_ReturnsBuzz()
    {
        var actual = new GenerateFizzBuzzToken().GetToken(5);
        Assert.Equal("Buzz", actual);
    }

    [Fact]
    public void GetToken_InputMultipleOf5_ReturnsBuzz()
    {
        var actual = new GenerateFizzBuzzToken().GetToken(10);
        Assert.Equal("Buzz", actual);
    }

    [Fact]
    public void GetToken_InputMultipleOf3And5_ReturnsFizzBuzz()
    {
        var actual = new GenerateFizzBuzzToken().GetToken(15);
        Assert.Equal("FizzBuzz", actual);
    }

    [Fact]
    public void GetToken_ForCustomTokenOfValue3_ReturnsTheExpectedStringPoem()
    {
        var customTokens = new Dictionary<int, string>();
        customTokens.Add(3, "Poem");

        var actual = new GenerateFizzBuzzToken(customTokens).GetToken(3);
        Assert.Equal("Poem", actual);
    }

    [Theory]
    [InlineData(119, "PoemWriter")]
    [InlineData(21, "PoemCollege")]
    [InlineData(357, "PoemWriterCollege")]
    public void GetToken_ForMultipleTokenOfValues_ReturnsTheExpectedConcatenatedValues(int inputNumber, string expectedValue)
    {
        var customTokens = new Dictionary<int, string>();
        customTokens.Add(7, "Poem");
        customTokens.Add(17, "Writer");
        customTokens.Add(3, "College");

        var actual = new GenerateFizzBuzzToken(customTokens).GetToken(inputNumber);
        Assert.Equal(expectedValue, actual);
    }
}