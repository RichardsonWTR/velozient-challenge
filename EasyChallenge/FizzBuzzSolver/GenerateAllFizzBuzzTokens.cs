
using EasyChallenge;

namespace FizzBuzzSolver;

public class GenerateAllFizzBuzzTokens
{
    public List<string> GetAllTokens()
    {
        var allTokens = new List<string>();
        var tokenGenerator = new GenerateFizzBuzzToken();

        for (int i = 1; i <= 100; i++)
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
