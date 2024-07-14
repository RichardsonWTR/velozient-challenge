for (int i = 1; i <= 100; i++)
{
    var token = "";
    if (i % 3 == 0)
        token += "Fizz";
    if (i % 5 == 0)
        token += "Buzz";

    if (token.Length == 0)
        Console.WriteLine(i);
    else 
        Console.WriteLine(token);
}