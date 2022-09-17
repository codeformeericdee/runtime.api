using System;
using AbstractRuntimes;

namespace AbstractInformation
{
    /// <summary>
    /// Class for dynamically coded runtimes.
    /// </summary>
    public abstract class Runtime
    {
        /// <summary>
        /// Protected means of persisting this variable into subclasses and ensuring that it is used.
        /// </summary>
#pragma warning disable SA1401 // Fields should be private
        protected bool isRunning;
#pragma warning restore SA1401 // Fields should be private

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
        /// <param name="collector">A framework dedicated singleton that acts as a pseudo namespace.</param>
        /// <returns>A true if successful. False if not.</returns>
        public abstract bool Run(Collector collector);

        /// <summary>
        /// Virtual method for what this class should do upon exiting.
        /// </summary>
        /// <returns>A true on completion.</returns>
        public virtual bool OnExit()
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n================ {this.name} is exiting ================");
            Console.ResetColor();
            return true;
        }

        /// <summary>
        /// Virtual method for what this class should do upon exiting that uses a collector.
        /// </summary>
        /// <param name="collector">A framework dedicated singleton that acts as a pseudo namespace.</param>
        /// <returns>A true on completion.</returns>
        public virtual bool OnExit(Collector collector)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n================ {this.name} is exiting ================");
            Console.ResetColor();
            collector.IsEditing = false;
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
        /// <param name="collector">A framework dedicated singleton that acts as a pseudo namespace.</param>
        /// <returns>A true if successful, or a false if failure occurs. If an exception is thrown it is printed and caught.</returns>
        public bool RuntimeMain(Collector collector)
        {
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n================ {this.name} is running ================");
            Console.ResetColor();

            try
            {
                switch (this.Run(collector))
                {
                    case true:
                        collector.IsEditing = true;
                        return true;
                    default:
                        this.isRunning = false;
                        this.OnExit(collector);
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
