using System;
using System.Collections.Generic;
using System.Linq;
using AbstractInformation;
using StringManipulation;

namespace AbstractRuntimes
{
    /// <summary>
    /// The entry point to the application.
    /// </summary>
    internal class Program
    {
        private static bool hasWork = true;
        private static List<Runtime> runtimes = new List<Runtime>();

        /// <summary>
        /// Substantial loop of the application.
        /// </summary>
        /// <param name="args">Default args unused.</param>
        private static void Main(string[] args)
        {
            PigLatinConverter pigLatinConverter = new PigLatinConverter(true, "Pig Latin Converter");
            runtimes.Add(pigLatinConverter);

            while (hasWork)
            {
                runtimes.ForEach(runtime =>
                {
                    runtime.RuntimeMain();
                });

                hasWork = runtimes.Any(runtime => runtime.GetStatus() == 1);
            }

            Console.WriteLine("\n\n\nPress the enter key to terminate the application.");
            Console.Read();
        }
    }
}
