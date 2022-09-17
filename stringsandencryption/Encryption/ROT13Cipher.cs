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
            Input.Out("The original string is as follows: " + collector.PublicInfo + "\n");
            this.SetAsByteList(collector.PublicInfo);
            this.ROT13CipherApplication();
            return false;
        }

        /// <summary>
        /// Overrides the exit function to check if the collector is being edited.
        /// </summary>
        /// <param name="collector">The singleton storing state.</param>
        /// <returns>A value indicating the result of the exit.</returns>
        public override bool OnExit(Collector collector)
        {
            bool result;
#pragma warning disable SA1100 // Do not prefix calls with base unless local implementation exists
            if (collector.IsEditing)
            {
                result = base.OnExit();
                this.isRunning = true;
            }
            else
            {
                result = base.OnExit();
            }

            return result;
#pragma warning restore SA1100 // Do not prefix calls with base unless local implementation exists
        }

        private bool ROT13CipherApplication()
        {
            this.DisplayByteList();
            this.EncodeRotate13();
            this.DisplayByteList();
            this.DisplayStringList();
            return true;
        }

        private void SetAsByteList(string arrayToAdd)
        {
            this.cipherValue.Clear();
            char[] tempArray = arrayToAdd.ToCharArray();

            for (int i = 0; i < tempArray.Length; i++)
            {
                this.cipherValue.Add((byte)tempArray[i]);
            }
        }

        private void DisplayByteList()
        {
            Input.Out("The string in decimal codes:");
            this.cipherValue.ForEach(n =>
            {
                Console.Write(n.ToString() + ", ");
            });
        }

        private void EncodeRotate13()
        {
            Input.Out("\n\nRotating the string....\n");

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
        }

        private void DisplayStringList()
        {
            Input.Out("\n\nThe string having been rotated by 13 places up or down:");
            Input.Out(System.Text.Encoding.UTF8.GetString(this.cipherValue.ToArray()));
        }
    }
}