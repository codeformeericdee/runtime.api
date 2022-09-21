using System.Text;
using Abstract;
using Singular;

namespace Applications
{
    public class PigLatin : Runtime
    {
        private StringBuilder stringBuilder;

        public PigLatin(string name, bool isRunning)
            : base(name, isRunning)
        {
            this.stringBuilder = new StringBuilder();
        }

        public override bool Run(CommunalMemory communalMemory)
        {
            try
            {
                this.PigLatinApplication(communalMemory);
                communalMemory.ModifyContent(this.stringBuilder.ToString(), this);
                return true;
            }
            catch (Exception ex)
            {
                Log.Out("Error converting string to pig latin.\n" + ex.Message.ToString());
                Log.Out("Translate another sentence?");
                return Log.RequestInputCharacters("y/n");
            }
        }

        public override bool AutoRestart()
        {
            return false;
        }

        private StringBuilder ReplaceSubstringWithPigLatin(string? originalString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (originalString != null)
            {
                string removal = Log.RemoveAtAny(ref originalString, "aeiouAEIOU");
                stringBuilder = new StringBuilder(originalString, originalString.Length);
                stringBuilder.Append(removal.ToLower() + "ay");
            }
            return stringBuilder;
        }

        private StringBuilder AppendString(string? originalString, string stringToAppend)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (originalString != null)
            {
                stringBuilder = new StringBuilder(originalString, originalString.Length);
                stringBuilder.Append(stringToAppend);
            }
            return stringBuilder;
        }

        private bool AssignStringBuilder(string? sentenceToTranslate)
        {
            switch (Log.StartsWithAny(sentenceToTranslate, "aeiouAEIOU"))
            {
                case true:
                    this.stringBuilder = this.AppendString(sentenceToTranslate, "way");
                    break;
                default:
                    this.stringBuilder = this.ReplaceSubstringWithPigLatin(sentenceToTranslate);
                    break;
            }

            Log.Out("The sentence has been translated to the following.\n");
            Log.Out(this.stringBuilder.ToString());
            return true;
        }

        private bool PigLatinApplication(CommunalMemory communalMemory)
        {
            string sentenceToTranslate;
            Log.SupplyByteListAsString(out sentenceToTranslate, communalMemory.PublicContent);
            Log.OutNewLine();
            return Log.PassOrExceptString(Log.ValidateStringExists(sentenceToTranslate)) ? this.AssignStringBuilder(sentenceToTranslate) : false;
        }
    }
}