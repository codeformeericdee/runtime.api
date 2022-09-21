﻿using System;
using System.Linq;
using System.Text;
using Abstraction;
using AbstractRuntimes;
using Implementation;
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
        /// <param name="collector">A framework dedicated singleton that acts as a pseudo namespace.</param>
        /// <returns>A true if successful. False if not. Or an Exception if thrown.</returns>
        public override bool Run(Collector collector)
        {
            try
            {
                collector.IsEditing = true;
                this.PigLatinApplication();
                collector.PublicInfo = this.stringBuilder.ToString();
                return Input.PromptRepeat();
            }
            catch (Exception ex)
            {
                Input.Out("Error converting string to pig latin.\n" + ex.Message.ToString());
                return Input.PromptRepeat();
            }
        }

        private StringBuilder ReplaceSubstringWithPigLatin(string originalString)
        {
            string removal = Input.RemoveAtAny(ref originalString, "aeiouAEIOU");
            StringBuilder stringBuilder = new StringBuilder(originalString, originalString.Length);
            stringBuilder.Append(removal.ToLower() + "ay");
            return stringBuilder;
        }

        private StringBuilder AppendString(string originalString, string stringToAppend)
        {
            StringBuilder stringBuilder = new StringBuilder(originalString, originalString.Length);
            stringBuilder.Append(stringToAppend);
            return stringBuilder;
        }

        private bool AssignString(string sentenceToTranslate)
        {
            switch (Input.StartsWithAny(sentenceToTranslate, "aeiouAEIOU"))
            {
                case true:
                    this.stringBuilder = this.AppendString(sentenceToTranslate, "way");
                    break;
                default:
                    this.stringBuilder = this.ReplaceSubstringWithPigLatin(sentenceToTranslate);
                    break;
            }

            Input.Out("Your sentence has been translated: \n");
            Input.Out(this.stringBuilder + "\n");
            return true;
        }

        private bool PigLatinApplication()
        {
            Input.Out("Welcome to pig latin maker. Please type a sentence and it will become translated.\n");
            Input.Out("================");
            string sentenceToTranslate = Input.In();
            Input.OutBlankLine();
            return Input.PassOrFailString(Input.ValidateStringLength(sentenceToTranslate)) ? this.AssignString(sentenceToTranslate) : false;
        }
    }
}