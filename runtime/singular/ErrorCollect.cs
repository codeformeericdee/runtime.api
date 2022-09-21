namespace Singular
{
    public sealed class ErrorCollect
    {
        private static ErrorCollect? instance;

        private static List<string> publicErrorLog = new List<string>();

        private static readonly object objectLock = new object();

        public static ErrorCollect Instance
        {
            get
            {
                lock (objectLock)
                {
                    if (instance == null)
                    {
                        instance = new ErrorCollect();
                    }

                    return instance;
                }
            }
        }

        public List<string>? AllPublicErrors
        {
            get
            {
                return publicErrorLog;
            }
        }

        public string MostRecentError
        {
            get
            {
                return publicErrorLog.Last();
            }
        }

        public bool AddError(string newError)
        {
            publicErrorLog.Add(newError);
            return true;
        }
    }
}