using Bogus;
using TextCopy;

while (GeneratePassword()) ;

Environment.Exit(-1);

static bool GeneratePassword()
{
    Console.Write("Specify number of characters: ");

    byte length;
    while (!byte.TryParse(Console.ReadLine() ?? String.Empty, out length) || length <= 0)
        Console.WriteLine($"Number of characters must be a valid number between 1 and {byte.MaxValue}.");

    var password = new Faker().Internet.Password(length, false);
    Console.WriteLine($"Password is: {password}");

    if (PromptForYOrN("Copy to clipboard?"))
    {
        ClipboardService.SetText(password);
        Console.WriteLine("Password copied.");
    }

    return PromptForYOrN("Generate another?");
}

static bool PromptForYOrN(string prompt)
{
    bool responseValue;

    var keyActions = new Dictionary<ConsoleKey, bool> {
        { ConsoleKey.Y, true },
        { ConsoleKey.N, false }
    };

    Console.Write($"{prompt} ({string.Join(", ", keyActions.Keys)}): ");

    var responseKey = Console.ReadKey().Key;

    while (!keyActions.TryGetValue(responseKey, out responseValue))
    {
        Console.WriteLine();
        Console.Write($"Must enter {string.Join(" or ", keyActions.Keys)}: ");
        responseKey = Console.ReadKey().Key;
    }

    Console.WriteLine();
    return responseValue;
}