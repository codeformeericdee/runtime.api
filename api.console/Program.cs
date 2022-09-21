using Abstract;
using Application;
using Applications;
using Encryption;
using Singular;

if (OperatingSystem.IsWindows())
{
    Console.SetWindowSize(100, 44);
}

ConsoleApplication.ApplicationEntryLocation(new string[] {"Startup"});

internal class ConsoleApplication
{
    private static bool takingInput = true;
    private static bool hasWork = true;
    private static List<Runtime> runtimes = new List<Runtime>();
    private static CommunalMemory communalMemory = new CommunalMemory();

    public static void ApplicationEntryLocation(string[] args)
    {
        UserInterface userInterface = new UserInterface("Console input", true);
        PigLatin pigLatin = new PigLatin("Pig latin converter", true);
        ROT13Cipher rot13Cipher = new ROT13Cipher("ROT13 cipher", true);
        runtimes.Add(userInterface);
        runtimes.Add(pigLatin);
        runtimes.Add(rot13Cipher);

        while (hasWork)
        {
            runtimes.ForEach(runtime =>
            {
                bool complete = runtime.GetStatus() == 1 ? runtime.RuntimeMain(communalMemory) : true;
            });

            hasWork = runtimes.Any(runtime => runtime.GetStatus() == 1);
        }

        DisplayExitString();
    }

    public static void DisplayStartupString()
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Log.Out("Enter a sentence and follow it with flags to translate or encode it.");
        Console.ForegroundColor = ConsoleColor.Blue;
        Log.Out("!p will translate to pig latin.");
        Log.Out("!rot13 will encrypt in ROT13 formation.");
        Console.ForegroundColor = ConsoleColor.Black;
        Log.Out("Example: My name is Eric Dee !p !rot13 translates and encrypts the sentence.");
        Console.ResetColor();
    }

    public static void DisplayExitString()
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press the enter key to end the application.");
        Console.ResetColor();
        Console.Read();
    }
}