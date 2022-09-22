using Abstract;
using Application;
using Singular;

public class Link : Runtime
{
    private List<char[]> possibleCommands;
    private Chain chain;
    private int flagCount;

    public Link(string name, bool isRunning)
        : base(name, isRunning)
    {
        this.possibleCommands = new List<char[]>();
        this.chain = new Chain();
        this.possibleCommands.Add(new char[4] { 'e', 'x', 'i', 't' });
        this.possibleCommands.Add(new char[3] { 'e', 'n', 'd' });
        this.AssignKeys();
        this.flagCount = 0;
    }

    public List<char[]> PossibleCommands
    {
        get { return possibleCommands; }
    }

    public override bool Run(CommunalMemory communalMemory)
    {
        this.flagCount = 0;
        try
        {
            List<byte>? leftHandSide = null;
            bool flagged = false;

            if (communalMemory.HasContent)
            {
                flagged = this.DetermineFlags(out leftHandSide, communalMemory.PublicContent);
                if (flagged)
                {
                    this.flagCount++;
                    ThreadLog.ClearWhiteSpace(communalMemory.PublicContent);
                    while (this.flagCount < 2)
                    {
                        this.CheckForDefaults(communalMemory.PublicContent);

                    }
                    string replacement;
                    Log.SupplyByteListAsString(out replacement, leftHandSide);
                    communalMemory.ModifyContent(replacement, this);
                }
            }

            return flagged;
        }
        catch (Exception ex)
        {
            Log.Out("An error occurred while parsing the recent command: \n" + ex.Message);
            return false;
        }
    }

    public void AssignKeys()
    {
        this.possibleCommands.ForEach((possibility) =>
        {
            uint sum = 0;
            for (int i = 0; i < possibility.Length; i++)
            {
                sum += possibility[i];
            }
            this.chain.SetAction(sum, this.ActionFactory(sum));
        });
    }

    public void AddKey(char[] possibility, Action? newAction)
    {
        uint sum = 0;
        for (int i = 0; i < possibility.Length; i++)
        {
            sum += possibility[i];
        }
        this.chain.SetAction(sum, newAction);
        this.possibleCommands.Add(possibility);
    }

    public bool CheckForDefaults(List<byte>? inputToCheck)
    {
        List<byte>? leftHandSide = null;
        bool flagged = false;

        if (flagged = this.DetermineFlags(out leftHandSide, inputToCheck))
        {
            this.flagCount++;
            if (ThreadLog.CheckForCharacters(inputToCheck, new char[] { '!' }))
            {
                throw new Exception("Too many flags have been entered. The limit is two (2).");
            }
            Log.DisplayByteListAsString(leftHandSide);
            Log.DisplayByteListAsString(inputToCheck);
            int? sum = leftHandSide?.Sum(n => Convert.ToInt32(n));
            uint usom = Convert.ToUInt32(sum);
            this.chain.CallAction(usom);
            sum = inputToCheck?.Sum(n => Convert.ToInt32(n));
            usom = Convert.ToUInt32(sum);
            this.chain.CallAction(usom);
        }

        return false;
    }

    public bool ListDefaults()
    {
        this.possibleCommands.ForEach(nOfString => {
            Console.WriteLine(nOfString);
        });
        return true;
    }

    public bool DetermineFlags(out List<byte>?leftHandSide, List<byte>? stringToScan)
    {
        leftHandSide = null;
        if (stringToScan != null)
        {
            return Log.DecoupleByteList(out leftHandSide, stringToScan, 33);
        }
        else return false;
    }

    public Action ActionFactory(uint theDefault)
    {
        switch (theDefault)
        {

            case 442: // exit
                return this.chain.EndTheApplication;

            case 311: // end
                return this.chain.EndTheApplication;
            default:
                return () => { throw new Exception($"This flag sum of {theDefault} is not valid."); };
        }
    }

    public void DisplaySums()
    {
        this.possibleCommands.ForEach((possibility) =>
        {
            uint sum = 0;
            for (int i = 0; i < possibility.Length; i++)
            {
                sum += possibility[i];
            }
            Console.WriteLine(sum);
        });
    }
}