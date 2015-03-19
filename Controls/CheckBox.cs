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
         
    public class CheckBox : Control
    {
        public event EventHandler Checked;

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
                if (this.text != value)
                {
                    return;
                }

                this.text = value;
                this.OnPropertyChanged("Text");
            }
        }
        
        private bool isChecked;

        private ImageSource image;
                                                                             
        /// <summary>
        /// Holds the value of the text
        /// </summary>
        protected string text;

        protected CheckBox(ImageSource image)
            : this()
        {
            this.image = image;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class.
        /// </summary>
        public CheckBox()
            : base()
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
        /// Gets or sets Texture.
        /// </summary>
        public virtual ImageSource Image
        {
            get
            {
                return this.image;
            }

            set
            {
                if (this.image == value)
                {
                    return;
                }

                this.image = value;
                this.OnPropertyChanged("Image");
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
                if (this.isChecked == value)
                {
                    return;
                }

                this.isChecked = value;
                this.OnChecked(EventArgs.Empty);
                this.OnPropertyChanged("IsChecked");
            }
        }

        public void OnChecked(EventArgs e)
        {
            var handler = this.Checked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}