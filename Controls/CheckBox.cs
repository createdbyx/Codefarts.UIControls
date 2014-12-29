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
        public virtual string Text { get; set; }

        private bool isChecked;

        protected CheckBox(ImageSource image)
            : this()
        {
            this.Image = image;
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
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets Texture.
        /// </summary>
        public virtual ImageSource Image { get; set; }

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
                var changed = this.isChecked != value;
                this.isChecked = value;
                if (changed)
                {
                    this.OnChecked(EventArgs.Empty);
                }
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