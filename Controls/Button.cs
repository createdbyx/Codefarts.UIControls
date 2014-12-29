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
    using System;

    /// <summary>
    /// The button.
    /// </summary>
    public class Button : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="text">The text for the button.</param>
        public Button(string text)
            : this()
        {
            this.Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
            : base()
        {
        }

        #region Public Events

        public event EventHandler Click;

        #endregion

        #region Public Properties

        public virtual string Text { get; set; }
                                                                                   
        /// <summary>
        /// Gets or sets button icon image.
        /// </summary>
        public virtual ImageSource Image { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on click.
        /// </summary>
        public void OnClick()
        {
            var handler = this.Click;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}