using System.Diagnostics;

namespace Application
{
    public static class ThreadLog
    {
        private static readonly bool consoleAvailability;

        static ThreadLog()
        {
            consoleAvailability = Process.GetCurrentProcess().MainWindowHandle == IntPtr.Zero;
        }

        public static bool SupplyByteList(string? stringToConvert, out List<byte> listToClear)
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

        public static bool ClearWhiteSpace(List<byte>? listToClear)
        {
            bool hadWhiteSpace = false;
            if (listToClear != null)
            {
                for (int i = 0; i < listToClear.Count; i++)
                {
                    if (listToClear[i] == 32)
                    {
                        listToClear.RemoveAt(i);
                        hadWhiteSpace = true;
                    }
                }
            }
            return hadWhiteSpace;
        }

        public static bool CheckForCharacters(List<byte>? listToCheck, char[] charactersToLookFor)
        {
            bool hasCharacters = false;
            if (listToCheck != null)
            {
                for (int i = 0; i < listToCheck.Count; i++)
                {
                    for (int n = 0; n < charactersToLookFor.Length; n++)
                    {
                        if (listToCheck[i] == charactersToLookFor[n])
                        {
                            hasCharacters = true;
                            return hasCharacters;
                        }
                    }
                }
            }
            return hasCharacters;
        }
    }
}
