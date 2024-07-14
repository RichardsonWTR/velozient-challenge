using EasyChallenge;

GenerateFizzBuzzToken fizzBuzzGenerator = new();

for (int i = 1; i <= 100; i++)
{
    var token = fizzBuzzGenerator.GetToken(i);

    if (token == null)
        Console.WriteLine(i);
    else 
        Console.WriteLine(token);
}