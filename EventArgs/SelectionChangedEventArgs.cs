/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    /// <summary>
    /// Provides data for the selection changed" /> events.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SelectionChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets the old value.
        /// </summary>
        public int OldValue { get; set; }

        /// <summary>
        /// Gets or sets the new value.
        /// </summary>
        public int NewValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        public SelectionChangedEventArgs(int oldValue, int newValue) : this()
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionChangedEventArgs"/> class.
        /// </summary>
        public SelectionChangedEventArgs()
        {
        }
    }
}