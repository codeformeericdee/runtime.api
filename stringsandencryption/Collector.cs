using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractRuntimes
{
    /// <summary>
    /// Singleton class for passing readable strings to the Main() loop.
    /// </summary>
    public sealed class Collector
    {
        private static readonly object LockedState = new object();

        private static Collector instance;

        private string publicInfo;

        private bool isEditing;

        /// <summary>
        /// Empty singleton constructor.
        /// </summary>
        public Collector()
        {
            this.isEditing = false;
        }

        /// <summary>
        /// Gets the instance and locks and unlocks the instantiation for thread safety.
        /// </summary>
        public static Collector Instance
        {
            get
            {
                lock (LockedState)
                {
                    if (instance == null)
                    {
                        instance = new Collector();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Gets or sets the string allowed for viewing.
        /// </summary>
        public string PublicInfo
        {
            get
            {
                return this.publicInfo;
            }

            set
            {
                this.publicInfo = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an app has flagged all apps to wait on closing.
        /// </summary>
        public bool IsEditing
        {
            get
            {
                return this.isEditing;
            }

            set
            {
                this.isEditing = value;
            }
        }
    }
}