using Abstract;
using Application;
using Encryption;
using Singular;

if (OperatingSystem.IsWindows())
{
    Console.SetWindowSize(100, 44);
}

ConsoleApplication.ApplicationEntryLocation(new string[] {"Startup"});

internal class ConsoleApplication
{
    private static bool hasWork = true;
    private static List<Runtime> runtimes = new List<Runtime>();
    private static CommunalMemory communalMemory = new CommunalMemory();

    public static void ApplicationEntryLocation(string[] args)
    {
        char[] pigLatinCommand = { 'p', 'i', 'g', 'l', 'a', 't', 'i', 'n' };
        char[] rot13Command = { 'r', 'o', 't', '1', '3' };

        UserInterface userInterface = new UserInterface("Console input", true);
        Link link = new Link("Instruction parser", true);
        PigLatin pigLatin = new PigLatin("Pig latin converter", false, pigLatinCommand);
        ROT13Cipher rot13Cipher = new ROT13Cipher("ROT13 cipher", false, rot13Command);
        runtimes.Add(userInterface);
        runtimes.Add(link);
        runtimes.Add(pigLatin);
        runtimes.Add(rot13Cipher);

        pullClassCommands(link);

        Log.DisplayStartupString(link.PossibleCommands);

        while (hasWork)
        {
            runtimes.ForEach(runtime =>
            {
                _ = runtime.GetStatus() == 1 ? runtime.RuntimeMain(communalMemory) : true;
            });

            hasWork = runtimes.Any(runtime => runtime.GetStatus() == 1);
        }

        Log.DisplayExitString();
    }

    private static void pullClassCommands(Link link)
    {
        runtimes.ForEach(runtime =>
        {
            char[]? addedCommand;
            Action? compatibility = runtime.GetCommandCompatibility(out addedCommand);
            if (addedCommand != null)
            {
                link.AddKey(addedCommand, compatibility);
            }
        });
    }
}