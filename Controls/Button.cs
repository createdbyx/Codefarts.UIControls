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
    /// A button control.
    /// </summary>
    public class Button : Control
    {
        /// <summary>
        /// The text for the <see cref="Text"/> property.
        /// </summary>
        protected string text;

        /// <summary>
        /// The image for the <see cref="Image"/> property.
        /// </summary>
        protected ImageSource image;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="text">The text for the button.</param>
        public Button(string text)
            : this()
        {
            this.text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        public Button()
        {
        }

        #region Public Events

        /// <summary>
        /// Occurs when the button is clicked.
        /// </summary>
        public event EventHandler Click;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>               
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                var changed = this.text != value;
                this.text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Text");
                }
            }
        }

        /// <summary>
        /// Gets or sets button icon image.
        /// </summary>
        public virtual ImageSource Image
        {
            get
            {
                return this.image;
            }

            set
            {
                var changed = this.image != value;
                this.image = value;
                if (changed)
                {
                    this.OnPropertyChanged("Image");
                }
            }
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(75, 23);
            }
        }

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