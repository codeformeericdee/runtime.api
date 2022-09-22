using Application;

public class Link
{
    public List<char[]> defaults;

    public Link()
    {
        this.defaults = new List<char[]>();
        this.defaults.Add(new char[4] { 'e', 'x', 'i', 't' });
        this.CheckDefaults();
    }

    public bool CheckDefaults()
    {
        this.defaults.ForEach(nOfString => {
            Console.WriteLine(nOfString);
        });
        return true;
    }

    public bool DetermineFlags(out List<byte>?leftHandSide, List<byte>? stringToScan)
    {
        return Log.DecoupleByteList(out leftHandSide, stringToScan);
    }

    public void Decouple()
    {
        List<byte>? leftHandSide = null;

        if (communalMemory.HasContent)
        {
            bool flagged = link.DetermineFlags(out leftHandSide, communalMemory.PublicContent);
            communalMemory.ModifyContent(leftHandSide, runtime);
        }
    }
}