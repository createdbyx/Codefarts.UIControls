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
    /// Represents a <see cref="CheckBox" />.
    /// </summary>
    public class CheckBox : Control
    {
        /// <summary>
        /// Holds the value of the appearance
        /// </summary>
        protected Appearance appearance = Appearance.Normal;

        /// <summary>
        /// Holds the value of the image.
        /// </summary>
        protected ImageSource image;

        /// <summary>
        /// The is value for the <see cref="IsChecked"/> property.
        /// </summary>
        protected bool isChecked;

        /// <summary>
        /// Holds the value of the text
        /// </summary>
        protected string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox" /> class.
        /// </summary>
        /// <param name="image">The image.</param>
        protected CheckBox(ImageSource image)
            : this()
        {
            this.image = image;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        public CheckBox()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        /// <param name="text">The text to display.</param>
        public CheckBox(string text)
            : this()
        {
            this.text = text;
        }

        /// <summary>
        /// Gets or sets the value that determines the appearance of a <see cref="CheckBox" /> control.
        /// </summary>
        /// <returns>
        /// One of the <see cref="Appearance" /> values. The default value is <see cref="Appearance.Normal" />.
        /// </returns>
        public virtual Appearance Appearance
        {
            get
            {
                return this.appearance;
            }

            set
            {
                var changed = this.appearance != value;
                this.appearance = value;
                if (changed)
                {
                    this.OnPropertyChanged("Appearance");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text.
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
        /// Gets or sets image that is displayed in the checkbox.
        /// </summary>
        public virtual ImageSource Image
        {
            get
            {
                return this.image;
            }

            set
            {
                var changed = this.image == value;
                this.image = value;
                if (changed)
                {
                    this.OnPropertyChanged("Image");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the checkbox checked.
        /// </summary>   
        public virtual bool IsChecked
        {
            get
            {
                return this.isChecked;
            }

            set
            {
                var changed = this.isChecked == value;
                this.isChecked = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsChecked");
                }
            }
        }
         
        /// <summary>Gets the default size of the control.</summary>
        /// <returns>The default size.</returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(104, 24);
            }
        }
    }
}