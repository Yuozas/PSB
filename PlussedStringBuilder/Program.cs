using System.Diagnostics.CodeAnalysis;
using TextCopy;

var restartAppInSeconds = 10;

while (true)
{
    await NotifyAboutApp();
    if (TryStart())
    {
        if (!TryBuildTextFromClipboard(out var text))
        {
            Console.WriteLine($"Failed to copy from clipboard:\n{text}\n");
            Console.WriteLine();
            continue;
        }
        Console.Clear();
        await ClipboardService.SetTextAsync(text);
        await InformTextCopied(text);
        await RestartApp();
    }
    Console.Clear();
}


async Task LogInfo(string text)
{
    Console.WriteLine(text);
    // Delay to allow to read text.
    await Task.Delay(TimeSpan.FromSeconds(1));
}

async Task NotifyAboutApp()
{
    await LogInfo("Data from clipboard will be taken to build multiline plussed string.\n");
}

bool TryStart()
{
    // Notify how to start.
    Console.WriteLine("To start type - y\n");
    var userKeyPress = Console.ReadKey();

    // Clear pre start logs for clarity.
    Console.Clear();

    // Task succesfully started.
    return userKeyPress.KeyChar is 'y' or 'Y';
}

bool TryBuildTextFromClipboard([NotNullWhen(true)]out string? clipboardText)
{
    clipboardText = ClipboardService.GetText();
    if (clipboardText is null)
        return false;

    var builtLines = clipboardText.Split('\n').Select(line => $"\"{line.Trim('\r').Trim('\n')}\\n\"");

    clipboardText = "const string text = " +
            string.Join(" + \n", builtLines) +
            ";";
    return true;
}

async Task InformTextCopied(string text)
{
    await LogInfo("Text Copied...");
    Console.Clear();

    Console.WriteLine($"{text}");

    // New lines for clarity.
    Console.WriteLine();
    Console.WriteLine();
}

async Task RestartApp()
{
    await LogInfo($"Restarting app in");

    for (var i = 0; i < restartAppInSeconds; i++)
    {
        if (i % 4 is 0)
            Console.WriteLine($"{restartAppInSeconds - i} seconds.");
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}