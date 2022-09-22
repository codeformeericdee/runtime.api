using System.Diagnostics;
using System.Text;

namespace Application
{
    public static class Log
    {
        private static readonly bool consoleAvailability;

        static Log()
        {
            consoleAvailability = Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero;
        }

        public static void Out(string stringToDisplay)
        {
            switch (consoleAvailability)
            {
                case true:
                    Console.WriteLine(stringToDisplay);
                    return;
                default:
                    return;
            }
        }

        public static void OutNewLine()
        {
            switch (consoleAvailability)
            {
                case true:
                    Console.WriteLine();
                    return;
                default:
                    return;
            }
        }

        public static string? In()
        {
            switch (consoleAvailability)
            {
                case true:
                    return Console.ReadLine();
                default:
                    return null;
            }
        }

        public static void DisplayByteList(List<byte> listToDisplay)
        {
            switch (consoleAvailability)
            {
                case true:
                    listToDisplay.ForEach(n =>
                    {
                        Console.Write(n.ToString() + ", ");
                    });
                    return;
                default:
                    return;
            }
        }

        public static void DisplayByteListAsString(List<byte>? listToDisplay)
        {
            if (listToDisplay != null)
            {
                switch (consoleAvailability)
                {
                    case true:
                        Out(System.Text.Encoding.UTF8.GetString(listToDisplay.ToArray()));
                        return;
                    default:
                        return;
                }
            }
        }

        public static bool ValidateStringExists(string? stringToValidate)
        {
            return stringToValidate != null ? (stringToValidate.Length > 0 ? true : false) : false;
        }

        public static bool PassOrExceptString(bool result)
        {
            if (result == true)
            {
                return true;
            }
            else
            {
                throw new Exception("This string is invalid.");
            }
        }

        public static bool EnforceInputCharacters(string charactersNeeded, out string? resultingInput)
        {
            resultingInput = string.Empty;
            switch (consoleAvailability)
            {
                case true:
                    Console.WriteLine(charactersNeeded + "\n");
                    resultingInput = Console.ReadLine();
                    resultingInput = EnforceCharacters(resultingInput, charactersNeeded);
                    return resultingInput.ToLower().Contains(charactersNeeded) ? true : false;
                default:
                    return false;
            }
        }

        public static string EnforceCharacters(string? inputToEnforce, string charactersNeeded)
        {
            if (inputToEnforce != null)
            {
                if (inputToEnforce.Length > 0)
                {
                    return inputToEnforce;
                }
                else
                {
                    Console.WriteLine($"Please enter a character. {charactersNeeded}\n");
                    inputToEnforce = Console.ReadLine();
                }
            }
            return EnforceCharacters(inputToEnforce, charactersNeeded);
        }

        public static bool StartsWithAny(string? stringToCheck, string charactersToCheckFor)
        {
            char[] vowels = charactersToCheckFor.ToCharArray();
            int instructionPointer = 0;

            if (stringToCheck != null)
            {
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
            }

            return instructionPointer == 0 ? false : true;
        }

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

        public static bool DecoupleByteList(out List<byte>?outPutString, List<byte>? originalString, int separator)
        {
            outPutString = null;
            bool found = false;
            if (originalString != null)
            {
                List<byte> temporaryString = new List<byte>();
                int sourceIndex;
                for (sourceIndex = 0; sourceIndex < originalString.Count; sourceIndex++)
                {
                    if (originalString[sourceIndex] != separator)
                    {
                        temporaryString.Add(originalString[sourceIndex]);
                    }
                    else
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    sourceIndex++;
                    originalString.RemoveRange(0, sourceIndex);
                    outPutString = temporaryString;
                    return true;
                }
                else return true;
            }
            else return false;
        }

        public static string AppendToString(string originalString, string stringToAppend)
        {
            StringBuilder stringBuilder = new StringBuilder(originalString, originalString.Length);
            stringBuilder.Append(stringToAppend);
            return stringBuilder.ToString();
        }

        public static bool SupplyByteList(string stringToConvert, out List<byte> listToClear)
        {
            listToClear = new List<byte>();

            if (stringToConvert != null && listToClear != null)
            {
                char[] tempArray = stringToConvert.ToCharArray();

                for (int i = 0; i < tempArray.Length; i++)
                {
                    listToClear.Add((byte)tempArray[i]);
                }

                return listToClear.Count > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public static bool SupplyByteListAsString(out string stringToPlace, List<byte>? listToConvert)
        {
            stringToPlace = string.Empty;

            if (stringToPlace != null && listToConvert != null)
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                stringToPlace = encoding.GetString(listToConvert.ToArray());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] ConvertCharArrayToByteArray(char[] arrayToChange)
        {
            return new System.Text.UTF8Encoding(true).GetBytes(arrayToChange);
        }

        public static List<byte> ConvertCharArrayToByteList(char[] arrayToChange)
        {
            byte[] byteArray = ConvertCharArrayToByteArray(arrayToChange);
            return new List<byte>(byteArray);
        }

        public static void DisplayStartupString(List<char[]> defaults)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Log.Out("Enter a sentence and follow it with flags to translate or encode it.");
            Log.Out("These flags exist:");
            Console.ForegroundColor = ConsoleColor.Blue;
            defaults.ForEach(nOfString => {
                Log.Out($"!{new string(nOfString)}");
            });
            Console.ForegroundColor = ConsoleColor.Black;
            Log.Out("Example: My name is Eric !piglatin !rot13 translates and encrypts the sentence.");
            Console.ResetColor();
        }

        public static void DisplayExitString()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press the enter key to end the application.");
            Console.ResetColor();
            Console.Read();
        }
    }
}
