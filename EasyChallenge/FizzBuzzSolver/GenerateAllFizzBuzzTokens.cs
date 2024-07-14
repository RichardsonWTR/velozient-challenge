
using EasyChallenge;

namespace FizzBuzzSolver;

public class GenerateAllFizzBuzzTokens
{
    private readonly int _startNumber;
    private readonly int _finalNumber;

    public GenerateAllFizzBuzzTokens()
    {
        _startNumber = 1;
        _finalNumber = 100;
    }

    public GenerateAllFizzBuzzTokens(int startNumber, int finalNumber)
    {
        _startNumber = startNumber;
        _finalNumber = finalNumber;
    }

    public List<string> GetAllTokens()
    {
        var allTokens = new List<string>();
        var tokenGenerator = new GenerateFizzBuzzToken();

        for (int i = _startNumber; i <= _finalNumber; i++)
        {
            var tokenOfNumber = tokenGenerator.GetToken(i);
            if (tokenOfNumber == null)
                allTokens.Add(i.ToString());
            else
                allTokens.Add(tokenOfNumber);
        }
        return allTokens;
    }
}
