using FizzBuzzSolver;


Console.WriteLine("1. Solving values from -20 to 127 using the following rules:");
Console.WriteLine("-- For multiples of 5 add to the return \"Fizz\"");
Console.WriteLine("-- For multiples of 9 add to the return \"Buzz\"");
Console.WriteLine("-- For multiples of 27 add to the return \"Bar\"");

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