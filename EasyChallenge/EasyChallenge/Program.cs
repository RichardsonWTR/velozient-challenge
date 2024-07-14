using EasyChallenge;
using FizzBuzzSolver;

var allTokens = new GenerateAllFizzBuzzTokens().GetAllTokens();

foreach (var token in allTokens)
{
    Console.WriteLine(token);
}