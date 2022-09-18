using System;
using System.Text;

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
        public static void OutBlankLine()
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
            Console.WriteLine("Translate again? (y/n)\n");
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
                Console.WriteLine("Please enter a character. (y or n)\n");
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
                throw new Exception("This string is invalid.\n");
            }
        }

        /// <summary>
        /// Checks if a string contains any of the leetters in the concurrent string.
        /// </summary>
        /// <param name="stringToCheck">The string to compare.</param>
        /// <param name="charactersToCheck">The letters to check for at the beginning.</param>
        /// <returns>True if the comparable string starts with one of the letters.</returns>
        public static bool StartsWithAny(string stringToCheck, string charactersToCheck)
        {
            char[] vowels = charactersToCheck.ToCharArray();
            int instructionPointer = 0;

            for (int sourceIndex = 0; sourceIndex < stringToCheck.Length; sourceIndex++)
            {
                if (sourceIndex - instructionPointer != 0)
                {
                    break;
                }

                for (int vowelIndex = 0; vowelIndex < vowels.Length; vowelIndex++)
                {
                    if (stringToCheck[sourceIndex] == vowels[vowelIndex])
                    {
                        instructionPointer++;
                        break;
                    }
                }
            }

            return instructionPointer == 0 ? false : true;
        }

        /// <summary>
        /// Removes the substring leading up to either of the trigger values.
        /// </summary>
        /// <param name="stringToCheck">The string to alter.</param>
        /// <param name="charactersToTrigger">The characters to trigger the cut.</param>
        /// <returns>The cut substring.</returns>
        public static string RemoveAtAny(ref string stringToCheck, string charactersToTrigger)
        {
            char[] vowels = charactersToTrigger.ToCharArray();
            int sourceIndex = 0;
            int instructionPointer = 0;

            for (sourceIndex = 0; sourceIndex < stringToCheck.Length; sourceIndex++)
            {
                for (int vowelIndex = 0; vowelIndex < vowels.Length; vowelIndex++)
                {
                    if (stringToCheck[sourceIndex] == vowels[vowelIndex])
                    {
                        instructionPointer++;
                        break;
                    }
                }

                if (instructionPointer != 0)
                {
                    break;
                }
            }

            char[] removal = new char[sourceIndex];

            for (int i = 0; i < sourceIndex; i++)
            {
                removal[i] = stringToCheck[i];
            }

            StringBuilder newString = new StringBuilder(stringToCheck, stringToCheck.Length);
            newString.Remove(0, sourceIndex);
            stringToCheck = newString.ToString();

            return string.Concat(removal);
        }

        /// <summary>
        /// Appends a string to another string safely.
        /// </summary>
        /// <param name="originalString">The string to add onto.</param>
        /// <param name="stringToAppend">The string to be added.</param>
        /// <returns>The new string with both items together.</returns>
        public static string AppendString(string originalString, string stringToAppend)
        {
            StringBuilder stringBuilder = new StringBuilder(originalString, originalString.Length);
            stringBuilder.Append(stringToAppend);
            return stringBuilder.ToString();
        }
    }
}
