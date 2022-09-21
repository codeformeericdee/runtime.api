using Abstract;
using Applications;
using Singular;

namespace Encryption
{
    public class ROT13Cipher : Runtime
    {
        private List<byte> cipherValue;

        public ROT13Cipher(string name, bool isRunning)
            : base(name, isRunning)
        {
            this.cipherValue = new List<byte>();
        }

        public override bool Run(CommunalMemory communalMemory)
        {
            try
            {
                return communalMemory.HasContent ? 
                    this.ROT13CipherApplication(communalMemory.PublicContent) : false;
            }
            catch (Exception ex)
            {
                Log.Out("Error encoding to ROT13.\n" + ex.Message.ToString());
                return false;
            }
        }

        public override bool AutoRestart()
        {
            return false;
        }

        public override bool OnExit(CommunalMemory communalMemory, bool invertBehavior=false)
        {
            return this.AdhereToCommunalStatus(communalMemory);
        }

        private bool EncodeRotate13(List<byte> listToEncode)
        {
            Log.Out("Rotating the string....\n");

            int first = listToEncode[0];

            for (int i = 0; i < listToEncode.Count; i++)
            {
                byte n = listToEncode[i];
                if (n < 91 && n >= 65)
                {
                    n = n >= 78 ? n -= 13 : n += 13;
                }
                else if (n < 123 && n >= 97)
                {
                    n = n >= 109 ? n -= 13 : n += 13;
                }

                listToEncode[i] = n;
            }

            return first == listToEncode[0] ? false : true;
        }

        private bool ROT13CipherApplication(List<byte>? listToEncode)
        {
            bool result = false;

            if (listToEncode != null)
            {
                if (listToEncode.Count > 0)
                {
                    if (result = this.EncodeRotate13(listToEncode))
                    {
                        Log.Out("The new community string:\n");
                    }
                }

                if (result)
                {
                    Log.DisplayByteList(listToEncode);
                    Log.OutNewLine();
                    Log.DisplayByteListAsString(listToEncode);
                    return true;
                }
            }

            return false;
        }
    }
}