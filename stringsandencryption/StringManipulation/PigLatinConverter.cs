using System;
using System.Text;
using AbstractInformation;
using Utility;

namespace StringManipulation
{
    /// <summary>
    /// A converter which implements a continuous runtime abstract base class that will display user inputs in pig latin.
    /// </summary>
    public class PigLatinConverter : Runtime
    {
        /// <summary>
        /// A generic string manipulation class.
        /// </summary>
        private StringBuilder stringBuilder;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="isRunning">Whether the converter should start with the application or not.</param>
        /// <param name="name">The name for the runtime (converter) for logging.</param>
        public PigLatinConverter(bool isRunning, string name)
            : base(isRunning, name)
        {
        }

        /// <summary>
        /// Defines how the application should act during its focus in Main().
        /// </summary>
        /// <returns>A true if successful. False if not. Or an Exception if thrown.</returns>
        public override bool Run()
        {
            try
            {
                this.PigLatinApplication();
                return Input.PromptRepeat();
            }
            catch (Exception ex)
            {
                Input.Out("Error converting string to pig latin.\n" + ex.Message.ToString());
                return Input.PromptRepeat();
            }
        }

        private StringBuilder ConvertStringToPigLatin(string stringToConvert)
        {
            char first = stringToConvert[0];
            StringBuilder stringBuilder = new StringBuilder(stringToConvert, stringToConvert.Length);
            stringBuilder.Remove(0, 1);
            stringBuilder.Append(char.ToLower(first) + "ay");
            return stringBuilder;
        }

        private bool AssignString(string sentenceToTranslate)
        {
            this.stringBuilder = this.ConvertStringToPigLatin(sentenceToTranslate);
            Input.Out("\nYour sentence has been translated: ");
            Input.Out(this.stringBuilder + "\n");
            return true;
        }

        private bool PigLatinApplication()
        {
            Input.Out("Welcome to pig latin maker. Please type a sentence and it will become translated.\n");
            Input.Out("================");
            string sentenceToTranslate = Input.In();
            return Input.PassOrFailString(Input.ValidateStringLength(sentenceToTranslate)) ? this.AssignString(sentenceToTranslate) : false;
        }
    }
}
