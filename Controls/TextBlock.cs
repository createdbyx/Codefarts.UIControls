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
    /// TextBlock control for presenting a simple text.
    /// </summary>
    public class TextBlock : Control
    {
        /// <summary>
        /// The text value for the <see cref="Text"/> property.
        /// </summary>
        protected string text;

        /// <summary>
        /// The text wrapping value for the <see cref="TextWrapping"/> property.
        /// </summary>
        protected TextWrapping textWrapping = TextWrapping.NoWrap;

        /// <summary>
        /// The text trimming value for the <see cref="TextTrimming"/> property.
        /// </summary>
        protected TextTrimming textTrimming = TextTrimming.None;

        /// <summary>
        /// The text alignment value for the <see cref="TextAlignment"/> property.
        /// </summary>
        protected TextAlignment textAlignment = TextAlignment.Left;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock()
        {
            this.canFocus = false;
            this.isTabStop = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        /// <param name="text">The text for the TextBlock.</param>
        public TextBlock(string text)
            : this()
        {
            this.text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        /// <param name="text">The text for the TextBlock.</param>
        /// <param name="name">The control name.</param>
        public TextBlock(string name, string text) : this(text)
        {
            this.name = name;
        }

        /// <summary>
        /// Gets or sets the text trimming behavior to employ when content overflows the content area.  
        /// </summary>
        /// <returns>
        /// One of the <see cref="TextTrimming" /> values that specifies the text trimming behavior to employ. The default is <see cref="TextTrimming.None" />.
        /// </returns>
        public TextTrimming TextTrimming
        {
            get
            {
                return this.textTrimming;
            }

            set
            {
                var changed = this.textTrimming != value;
                this.textTrimming = value;
                if (changed)
                {
                    this.OnPropertyChanged("TextTrimming");
                }
            }
        }

        /// <summary>
        /// Gets or sets how the <see cref="TextBlock" /> should wrap text. 
        /// </summary>
        /// <returns>
        /// One of the <see cref="TextWrapping" /> values. The default is <see cref="TextWrapping.NoWrap" />.
        /// </returns>
        public virtual TextWrapping TextWrapping
        {
            get
            {
                return this.textWrapping;
            }

            set
            {
                var changed = this.textWrapping != value;
                this.textWrapping = value;
                if (changed)
                {
                    this.OnPropertyChanged("TextWrapping");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates the horizontal alignment of text content.  
        /// </summary>
        /// <returns>
        /// One of the <see cref="TextAlignment" /> values that specifies the desired alignment. The default is <see cref="TextAlignment.Left" />.
        /// </returns>
        public TextAlignment TextAlignment
        {
            get
            {
                return this.textAlignment;
            }

            set
            {
                var changed = this.textAlignment != value;
                this.textAlignment = value;
                if (changed)
                {
                    this.OnPropertyChanged("TextAlignment");
                }
            }
        }

        /// <summary>
        /// Gets or sets the TextBlock text.
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

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 23);
            }
        }
    }
}