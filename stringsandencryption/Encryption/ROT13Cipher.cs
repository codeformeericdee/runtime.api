using System;
using System.Collections.Generic;
using AbstractInformation;
using AbstractRuntimes;
using Utility;

namespace Encryption
{
    /// <summary>
    /// ROT13 encryptor.
    /// </summary>
    public class ROT13Cipher : Runtime
    {
        private List<byte> cipherValue;

        /// <summary>
        /// Initialization settings for the ROT13 cipher.
        /// </summary>
        /// <param name="isRunning">Should the runtime be started with the application.</param>
        /// <param name="name">What is the name of the runtime.</param>
        public ROT13Cipher(bool isRunning, string name)
            : base(isRunning, name)
        {
            this.cipherValue = new List<byte>();
        }

        /// <summary>
        /// The definition of how the cipher should act during the Main() loop.
        /// </summary>
        /// <param name="collector">A framework dedicated singleton that acts as a pseudo namespace.</param>
        /// <returns>A true upon succes.</returns>
        public override bool Run(Collector collector)
        {
            try
            {
                return collector.PublicInfo == null ? false
                    : this.SetAsByteList(collector.PublicInfo) ? this.ROT13CipherApplication() : false;
            }
            catch (Exception ex)
            {
                Input.Out("Error encoding to ROT13.\n" + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Overrides the exit function to check if the collector is being edited.
        /// </summary>
        /// <param name="collector">The singleton storing state.</param>
        /// <returns>A value indicating the result of the exit.</returns>
        public override bool OnExit(Collector collector)
        {
            return this.FollowEdits(collector);
        }

        private bool ROT13CipherApplication()
        {
            bool result = false;

            if (this.cipherValue.Count > 0)
            {
                if (result = this.EncodeRotate13())
                {
                    Input.Out("The new collective string:\n");
                }
            }

            if (result)
            {
                this.DisplayByteList();
                this.DisplayStringList();
            }

            // Should always close after running
            return false;
        }

        private bool SetAsByteList(string arrayToAdd)
        {
            if (arrayToAdd != null)
            {
                this.cipherValue.Clear();
                char[] tempArray = arrayToAdd.ToCharArray();

                for (int i = 0; i < tempArray.Length; i++)
                {
                    this.cipherValue.Add((byte)tempArray[i]);
                }
            }

            return this.cipherValue.Count > 0 ? true : false;
        }

        private bool EncodeRotate13()
        {
            Input.Out("Rotating the string....\n");

            int first = this.cipherValue[0];

            for (int i = 0; i < this.cipherValue.Count; i++)
            {
                byte n = this.cipherValue[i];
                if (n < 91 && n >= 65)
                {
                    n = n >= 78 ? n -= 13 : n += 13;
                }
                else if (n < 123 && n >= 97)
                {
                    n = n >= 109 ? n -= 13 : n += 13;
                }

                this.cipherValue[i] = n;
            }

            return first == this.cipherValue[0] ? false : true;
        }

        private void DisplayByteList()
        {
            this.cipherValue.ForEach(n =>
            {
                Console.Write(n.ToString() + ", ");
            });
            Input.OutBlankLine();
        }

        private void DisplayStringList()
        {
            Input.Out(System.Text.Encoding.UTF8.GetString(this.cipherValue.ToArray()));
        }
    }
}