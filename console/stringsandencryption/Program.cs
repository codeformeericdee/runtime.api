using System;
using System.Collections.Generic;
using System.Linq;
using Abstraction;
using Encryption;
using Implementation;
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
        private static Collector collector = new Collector();

        /// <summary>
        /// Substantial loop of the application.
        /// </summary>
        /// <param name="args">Default args unused.</param>
        private static void Main(string[] args)
        {
            Console.SetWindowSize(100, 42);
            PigLatinConverter pigLatinConverter = new PigLatinConverter(true, "Pig Latin Converter");
            ROT13Cipher rot13Cipher = new ROT13Cipher(true, "ROT13 Cipher");
            runtimes.Add(pigLatinConverter);
            runtimes.Add(rot13Cipher);

            while (hasWork)
            {
                runtimes.ForEach(runtime =>
                {
                    bool complete = runtime.GetStatus() == 1 ? runtime.RuntimeMain(collector) : true;
                });

                hasWork = runtimes.Any(runtime => runtime.GetStatus() == 1);
            }

            Console.WriteLine("\n\n\n\nPress the enter key to terminate the application.");
            Console.Read();
        }
    }
}
