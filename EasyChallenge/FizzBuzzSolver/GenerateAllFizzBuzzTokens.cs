
using EasyChallenge;

namespace FizzBuzzSolver;

public class GenerateAllFizzBuzzTokens
{
    private readonly int _startNumber;
    private readonly int _finalNumber;
    private readonly List<int>? _customSequence;
    private Dictionary<int, string>? tokenPairs;

    public GenerateAllFizzBuzzTokens()
    {
        _startNumber = 1;
        _finalNumber = 100;
    }

    public GenerateAllFizzBuzzTokens(int startNumber, int finalNumber)
    {
        if (startNumber > finalNumber)
            throw new ArgumentException("The start number must be lesser than or equal to the final number");

        _startNumber = startNumber;
        _finalNumber = finalNumber;
    }

    public GenerateAllFizzBuzzTokens(List<int> customSequence)
    {
        _customSequence = customSequence;
    }

    public void SetTokenPairs(Dictionary<int, string> tokenPairs)
    {
        this.tokenPairs = tokenPairs;
    }

    public List<string> GetAllTokens()
    {
        var sequenceToIterate = GetSequenceToIterate();
        var allTokens = new List<string>();
        
        var tokenGenerator = new GenerateFizzBuzzToken();
        if (this.tokenPairs != null)
        {
            tokenGenerator = new GenerateFizzBuzzToken(this.tokenPairs);
        }
        
        foreach (int i in sequenceToIterate)
        {
            var tokenOfNumber = tokenGenerator.GetToken(i);
            if (tokenOfNumber == null)
                allTokens.Add(i.ToString());
            else
                allTokens.Add(tokenOfNumber);
        }
        return allTokens;
    }

    private int[] GetSequenceToIterate()
    {
        if (_customSequence != null)
            return _customSequence.ToArray();

        return GenerateRange.GenerateRangeFromNumbers(_startNumber, _finalNumber);
    }
}
