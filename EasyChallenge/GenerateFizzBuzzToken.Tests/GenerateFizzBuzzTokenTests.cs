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
}