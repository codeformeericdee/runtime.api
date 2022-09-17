using System;

namespace AbstractInformation
{
    /// <summary>
    /// Class for dynamically coded runtimes.
    /// </summary>
    public abstract class Runtime
    {
        private bool isRunning;
        private string name;

        /// <summary>
        /// The constructor for the abstract runtime framework.
        /// </summary>
        /// <param name="isRunning">Contains whether the class is set to repeat its calls during the Main() loop.</param>
        /// <param name="name">The name of the plugin. Should always be set to contain confusion.</param>
        public Runtime(bool isRunning, string name)
        {
            this.isRunning = isRunning;
            this.name = name;
        }

        /// <summary>
        /// The abstract and virtual framework method to define what the class should function as during the main loop.
        /// </summary>
        /// <returns>A true if successful. False if not.</returns>
        public abstract bool Run();

        /// <summary>
        /// Virtual method for what this class should do upon exiting.
        /// </summary>
        /// <returns>A true on completion.</returns>
        public virtual bool OnExit()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"================ {this.name} is exiting ================");
            Console.ResetColor();
            return true;
        }

        /// <summary>
        /// Enumerates the status into status codes.
        /// </summary>
        /// <returns>The status code.</returns>
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

        /// <summary>
        /// This method should be untouched, as it is the hardcoded function run by the Main() loop.
        /// Any changes should be made to the virtual run method.
        /// </summary>
        /// <returns>A true if successful, or a false if failure occurs. If an exception is thrown it is printed and caught.</returns>
        public bool RuntimeMain()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"================ {this.name} is running ================");
            Console.ResetColor();

            try
            {
                switch (this.Run())
                {
                    case true:
                        return true;
                    default:
                        this.isRunning = false;
                        this.OnExit();
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
