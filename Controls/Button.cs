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
    using Codefarts.UIControls.Models;

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
            this.canFocus = true;
            this.isTabStop = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="text">The text for the button.</param>
        public Button(string name, string text)
            : this(text)
        {
            this.name = name;
        }

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

        #region Overrides of Control

        /// <summary>
        /// Builds a <see cref="Markup" /> object that represent the state of the implementor.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relevant information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
        public override Markup ToMarkup()
        {
            var markup = base.ToMarkup();
            var value = this.Text;
            markup.SetProperty("Text", value != null, value);

            var imageSource = this.Image;
            markup.SetProperty("Image", imageSource != null, imageSource.ToMarkup());

            return markup;
        }

        #endregion

        #endregion
    }
}