using Abstract;
using Application;

namespace Singular
{
    public sealed class CommunalMemory
    {
        private static CommunalMemory? instance;

        private static bool hasContent;

        private static bool isBeingModified;

        private static string? privateContent;

        private static List<byte>? publicBytes;

        private static readonly object instanceLock = new object();

        private static readonly object byteLock = new object();

        public CommunalMemory()
        {
            hasContent = false;
            isBeingModified = false;
        }

        public static CommunalMemory Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CommunalMemory();
                    }

                    return instance;
                }
            }
        }

        // Place a dictionary here with methods and give keys to certain runtimes
        // aka key 1 pertains to checking a variable for status changes

        public bool HasContent 
        {
            get
            {
                return hasContent;
            }
        }

        public List<byte>? PublicContent
        {
            get
            {
                return publicBytes;
            }
        }

        public bool IsBeingModified
        {
            get
            {
                return isBeingModified;
            }
        }

        public bool ModifyContent(string? newContent, Runtime runtime)
        {
            if (!isBeingModified)
            {
                lock (instanceLock)
                {
                    isBeingModified = true;
                    privateContent = newContent;
                    hasContent = true;
                    OnModificationEnd(runtime);

                    lock (byteLock)
                    {
                        Thread byteThread = new Thread(() =>
                            ThreadLog.SupplyByteList(privateContent, out publicBytes));
                        byteThread.Start();
                        byteThread.Join();
                    }
                }

                return true;
            }

            else
            {
                return false;
            }
        }

        public bool OnModificationEnd(Runtime runtime)
        {
            isBeingModified = false;
            return runtime.OnExit(this);
        }
    }
}