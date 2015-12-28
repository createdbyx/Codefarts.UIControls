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
    /// Label control for presenting a simple text.
    /// </summary>
    public class Label : Control
    {
        protected string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        public Label()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="text">The text for the label.</param>
        public Label(string text)
            : this()
        {
            this.text = text;
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>    
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }
    }
}