using Singular;

namespace Abstract
{
    public abstract class Runtime
    {
        private string name;
        private bool isRunning;
        private char[]? hasCommand;
        private Action? hasMethod;

        public Runtime(string name, bool isRunning, char[]? command=null)
        {
            this.isRunning = isRunning;
            this.name = name;
            this.hasCommand = command;
            if (this.hasCommand != null) this.hasMethod = this.Start;
            else this.hasMethod = null;
        }

        protected void Start()
        {
            this.isRunning = true;
        }

        public abstract bool Run(CommunalMemory communalMemory);

        public virtual bool OnExit(bool invertBehavior=false)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n================ {this.name} is exiting ================");
            Console.ResetColor();
            this.isRunning = false;
            return invertBehavior ? false : true;
        }

        public virtual bool OnExit(CommunalMemory communalMemory, bool invertBehavior=false)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n================ {this.name} is done editing ================");
            Console.ResetColor();
            return invertBehavior ? this.OnExit() : true;
        }

        public Action? GetCommandCompatibility(out char[]? command)
        {
            command = this.hasCommand;
            return this.hasMethod;
        }

        public bool AdhereToCommunalStatus(CommunalMemory communalMemory)
        {
            bool result;
            if (communalMemory.IsBeingModified)
            {
                result = this.isRunning = true;
            }
            else
            {
                result = this.OnExit();
            }

            return result;
        }

        public virtual bool AutoRestart()
        {
            return true;
        }

        public int GetStatus()
        {
            switch (this.isRunning)
            {
                case true:
                    return 1;
                case false:
                    return 0;
            }
        }

        public bool RuntimeMain(CommunalMemory communalMemory)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n================ {this.name} is running ================");
            Console.ResetColor();

            try
            {
                switch (this.Run(communalMemory))
                {
                    case true:
                        return this.isRunning = this.AutoRestart() ?
                            true : this.OnExit(true);
                    default:
                        this.OnExit(communalMemory);
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
