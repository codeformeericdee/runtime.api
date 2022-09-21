using System;
using Implementation;

namespace Abstraction
{
    /// <summary>
    /// Class for dynamically coded runtimes.
    /// </summary>
    public abstract class Runtime
    {
        /// <summary>
        /// The status of the runtime which the Main() loop uses to determine whether to call the RuntimeMain method.
        /// </summary>
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
            this.isRunning = false;
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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n================ {this.name} is done editing ================");
            Console.ResetColor();
            collector.IsEditing = false;
            return this.OnExit();
        }

        /// <summary>
        /// Allows the plugin to always exit after use.
        /// Useful for plugins that only act if editing is occurring (Responder vs Updater).
        /// It also allows the privatization of the isRunning member.
        /// </summary>
        /// <param name="collector">The psuedo namespace for collective data.</param>
        /// <returns>A true or false depending on the exit functions override status and implementation.</returns>
        public bool FollowEdits(Collector collector)
        {
            bool result;
            if (collector.IsEditing)
            {
                result = this.isRunning = true;
            }
            else
            {
                result = this.OnExit();
            }

            return result;
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
