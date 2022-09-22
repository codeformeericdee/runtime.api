using Abstract;
using Singular;

namespace Application
{
    public class UserInterface : Runtime
    {
        public UserInterface(string name, bool isRunning) : base(name, isRunning)
        {}

        public override bool Run(CommunalMemory communalMemory)
        {
            try
            {
                string? userInput = Log.In();
                return Log.PassOrExceptString(Log.ValidateStringExists(userInput)) ?
                    communalMemory.ModifyContent(userInput, this) : false;
            }
            catch (Exception ex)
            {
                Log.Out("Error taking input.\n" + ex.Message.ToString());
                return false;
            }
        }
    }
}
