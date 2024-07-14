using FizzBuzzSolver;

var tokensGenerator = new GenerateAllFizzBuzzTokens(-20, 127);

var customRules = new Dictionary<int, string> {
    { 5, "Fizz" },
    { 9, "Buzz" },
    { 27, "Bar" },
};
tokensGenerator.SetTokenPairs(customRules);

printAllTokens(tokensGenerator.GetAllTokens());


static void printAllTokens(List<string> tokens)
{
    foreach (var token in tokens)
    {
        Console.WriteLine(token);
    }
}