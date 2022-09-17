using System;

namespace Utility
{
    /// <summary>
    /// An input utility class for reading and displaying lines.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Displays a string to the console.
        /// </summary>
        /// <param name="stringToDisplay">The string to display.</param>
        public static void Out(string stringToDisplay)
        {
            Console.WriteLine(stringToDisplay);
        }

        /// <summary>
        /// Displays a new line to the console.
        /// </summary>
        /// <param name="stringToDisplay">The string to display.</param>
        public static void OutBlank()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Reads a string from the console.
        /// </summary>
        /// <returns>The string which has been read from the user.</returns>
        public static string In()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// A default display for input from the user.
        /// </summary>
        /// <returns>A bool on success with a valid character.</returns>
        public static bool PromptRepeat()
        {
            Console.WriteLine("\nTranslate again? (y/n)");
            string restart = EnforceYesOrNoInput(Console.ReadLine());
            Console.WriteLine();
            return restart.ToLower() == "y" ? true : false;
        }

        /// <summary>
        /// Validates string is active by checking its length.
        /// </summary>
        /// <param name="stringToValidate">The string to check.</param>
        /// <returns>A true if the string has content, or a false if it does not.</returns>
        public static bool ValidateStringLength(string stringToValidate)
        {
            if (stringToValidate.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Enforces that an input has been given a correct yes or no character.
        /// </summary>
        /// <param name="inputToValidate">The string to validate.</param>
        /// <returns>The same string if it is valid, or repeats until the user enters a valid string and returns it.</returns>
        public static string EnforceYesOrNoInput(string inputToValidate)
        {
            if (inputToValidate.Length > 0)
            {
                return inputToValidate;
            }
            else
            {
                Console.WriteLine("Please enter a character. (y or n)");
                inputToValidate = Console.ReadLine();
                return EnforceYesOrNoInput(inputToValidate);
            }
        }

        /// <summary>
        /// A wrapper method for failing input via exception.
        /// </summary>
        /// <param name="item">The method to gather a response from.</param>
        /// <returns>A true if it passes, or throws an exception if it fails.</returns>
        /// <exception cref="Exception">Generic exception type with a basic failure message to display.</exception>
        public static bool PassOrFailString(bool item)
        {
            if (item == true)
            {
                return true;
            }
            else
            {
                throw new Exception("This string is invalid.");
            }
        }
    }
}
