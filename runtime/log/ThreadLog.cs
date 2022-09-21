using System.Diagnostics;
using System.Text;

namespace Applications
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
    }
}
