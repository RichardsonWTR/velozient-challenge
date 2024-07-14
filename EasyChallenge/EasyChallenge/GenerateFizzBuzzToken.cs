namespace EasyChallenge;

public class GenerateFizzBuzzToken
{
    public string? GetToken(int number)
    {
        var token = "";
        if (number % 3 == 0)
            token += "Fizz";
        if (number % 5 == 0)
            token += "Buzz";

        if (token.Length == 0)
            return null;

        return token;
    }
}