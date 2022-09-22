using Application;

public class Chain
{
    private Dictionary<uint, Action> actions;

    public Chain()
    {
        this.actions = new Dictionary<uint, Action>();
    }

    public void SetAction(uint value, Action? method)
    {
        if (this.actions.ContainsKey(value))
        {
            Log.Out(
                $"This id of {value} has already been used for {this.actions[value].ToString()}. " +
                $"Replace it? (y/n)");

            string? resultingInput;
            bool overwrite = Log.EnforceInputCharacters("y/n", out resultingInput);

            switch (overwrite)
            {
                case true:
                    this.actions.Remove(value);
                    if (method != null)
                    this.actions.Add(value, method);
                    return;
                default:
                    break;
            }
        }
        this.actions.Add(value, method);
    }

    public bool CallAction(uint sum)
    {
        try
        {
            foreach (KeyValuePair<uint, Action> kvp in this.actions)
            {
                if (sum == kvp.Key)
                {
                    kvp.Value();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Log.Out($"The key of {sum} failed to complete its method" + ex.Message);
            return false;
        }
    }

    public void EndTheApplication()
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Press the enter key to end the application.");
        Console.ResetColor();
        Console.Read();
        System.Environment.Exit(0);
    }
}