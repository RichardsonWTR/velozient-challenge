namespace EasyChallenge;

public class GenerateFizzBuzzToken
{
    private readonly Dictionary<int, string> _tokenPairs;
    public GenerateFizzBuzzToken()
    {
        _tokenPairs = new Dictionary<int, string>() {
            {3, "Fizz"},
            {5, "Buzz"},
        };
    }
    
    public GenerateFizzBuzzToken(Dictionary<int, string> tokenPairs)
    {
        _tokenPairs = tokenPairs;
    }

    public string? GetToken(int number)
    {
        var token = "";

        foreach (var pair in _tokenPairs)
        {
            if (number % pair.Key == 0)
                token += pair.Value;
        }
        
        if (token.Length == 0)
            return null;

        return token;
    }
}