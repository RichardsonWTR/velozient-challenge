namespace FizzBuzzSolver.Tests;

public class GenerateRangeTests
{
    [Theory]
    [InlineData(1, 5, new[] {1, 2, 3, 4, 5})]
    [InlineData(3, 6, new[] {3, 4, 5, 6})]

    public void GenerateRangeFromNumbers_InputPositiveNumbers_GenerateTheCorrectValues(int start, int end, int[] expected)
    {
        var actual = GenerateRange.GenerateRangeFromNumbers(start, end);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(-1, 1, new[] { -1, 0, 1})]
    [InlineData(-6, -3, new[] { -6, -5, -4, -3 })]
    public void GenerateRangeFromNumbers_InputWithNegativeNumbers_GenerateTheCorrectValues(int start, int end, int[] expected)
    {
        var actual = GenerateRange.GenerateRangeFromNumbers(start, end);
        Assert.Equal(expected, actual);
    }
}
