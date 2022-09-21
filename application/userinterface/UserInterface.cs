using Abstract;
using Applications;
using Singular;

namespace Application
{
    public class UserInterface : Runtime
    {
        public UserInterface(string name, bool isRunning) : base(name, isRunning)
        {
        }

        public override bool Run(CommunalMemory communalMemory)
        {
            string? userInput = Log.In();
            return Log.PassOrExceptString(Log.ValidateStringExists(userInput)) ?
                communalMemory.ModifyContent(userInput, this) : false;
        }
    }
}
