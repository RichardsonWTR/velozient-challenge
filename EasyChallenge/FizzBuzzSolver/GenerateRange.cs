namespace FizzBuzzSolver;

public static class GenerateRange
{
    public static int[] GenerateRangeFromNumbers(int start, int end)
    {
        List<int> result = new List<int>();
        for (int i = start; i <= end; i++)
            result.Add(i);  

        return result.ToArray();
    }
}
