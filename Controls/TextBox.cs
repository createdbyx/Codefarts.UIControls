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
    /// Implements the basic functionality required by text controls.
    /// </summary>
    public class TextBox : Control
    {
        public const string TextBoxSelectionStartChanged = "4BB16D2B-113E-42BD-8339-2E7EEC0B1C08";
        public const string TextBoxSelectionLengthChanged = "B1B9862F-7C70-4959-A208-157311FB475F";
      
        /// <summary>
        /// The backing field for the <see cref="Text"/> property.
        /// </summary>
        protected string text = string.Empty;

        /// <summary>
        /// The backing field for the <see cref="SelectionLength"/> property.
        /// </summary>
        protected int selectionLength;

        /// <summary>
        /// The backing field for the <see cref="SelectionStart"/> property.
        /// </summary>
        protected int selectionStart;

        /// <summary>
        /// The backing field for the <see cref="MaxLength"/> property.
        /// </summary>
        protected int maxLength;

        /// <summary>
        /// The backing field for the <see cref="VerticalOffset"/> property.
        /// </summary>
        protected float verticalOffset;

        /// <summary>
        /// The backing field for the <see cref="HorizontalOffset"/> property.
        /// </summary>
        protected float horizontalOffset;

        /// <summary>
        /// The backing field for the <see cref="VerticalScrollBarVisibility"/> property.
        /// </summary>
        protected ScrollBarVisibility verticalScrollBarVisibility;

        /// <summary>
        /// The backing field for the <see cref="HorizontalScrollBarVisibility"/> property.
        /// </summary>
        protected ScrollBarVisibility horizontalScrollBarVisibility;

        /// <summary>
        /// The backing field for the <see cref="AcceptsReturn"/> property.
        /// </summary>
        private bool acceptsReturn;

        /// <summary>
        /// The backing field for the <see cref="AcceptsTab"/> property.
        /// </summary>
        private bool acceptsTab;


        /// <summary>
        /// Gets or sets a value that indicates whether a horizontal scroll bar is shown. 
        /// </summary>
        /// <returns>
        /// A value that is defined by the <see cref="ScrollBarVisibility" /> enumeration. The default value is <see cref="Visibility.Hidden" />.
        /// </returns>
        public virtual ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get
            {
                return this.horizontalScrollBarVisibility;
            }

            set
            {
                var changed = this.horizontalScrollBarVisibility != value;
                this.horizontalScrollBarVisibility = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontalScrollBarVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether a vertical scroll bar is shown. 
        /// </summary>
        /// <returns>
        /// A value that is defined by the <see cref="ScrollBarVisibility" /> enumeration. The default value is <see cref="Visibility.Hidden" />.
        /// </returns>
        public virtual ScrollBarVisibility VerticalScrollBarVisibility
        {
            get
            {
                return this.verticalScrollBarVisibility;
            }

            set
            {
                var changed = this.verticalScrollBarVisibility != value;
                this.verticalScrollBarVisibility = value;
                if (changed)
                {
                    this.OnPropertyChanged("VerticalScrollBarVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets the horizontal scroll position.
        /// </summary>
        /// <returns>
        /// A floating-point value that specifies the horizontal scroll position. 
        /// Setting this property causes the text editing control to scroll to the specified horizontal offset. 
        /// Reading this property returns the current horizontal offset.                                                                             
        /// </returns>
        public virtual float HorizontalOffset
        {
            get
            {
                return this.horizontalOffset;
            }

            set
            {
                var changed = Math.Abs(this.horizontalOffset - value) > float.Epsilon;
                this.horizontalOffset = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontalOffset");
                }
            }
        }

        /// <summary>
        /// Gets or sets the vertical scroll position.
        /// </summary>
        /// <returns>
        /// A floating-point value that specifies the vertical scroll position.
        /// Setting this property causes the text editing control to scroll to the specified vertical offset. 
        /// Reading this property returns the current vertical offset.
        /// </returns>
        public virtual float VerticalOffset
        {
            get
            {
                return this.verticalOffset;
            }

            set
            {
                var changed = Math.Abs(this.verticalOffset - value) > float.Epsilon;
                this.verticalOffset = value;
                if (changed)
                {
                    this.OnPropertyChanged("VerticalOffset");
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters that can be manually entered into the text box.
        /// </summary>
        /// <returns>
        /// The maximum number of characters that can be manually entered into the text box. The default is 0, which indicates no limit.
        /// </returns>
        public virtual int MaxLength
        {
            get
            {
                return this.maxLength;
            }
            set
            {
                var changed = this.maxLength != value;
                this.maxLength = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxLength");
                }
            }
        }

        /// <summary>
        /// Gets or sets the text contents of the text box.
        /// </summary>
        /// <returns>
        /// A string containing the text contents of the text box. The default is an empty string ("").
        /// </returns>
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                value = !this.AcceptsReturn ? value.Replace("\r\n", string.Empty) : value;
                value = value == null ? string.Empty : value;
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
                return new Size(100, 32);
            }
        }

        /// <summary> 
        /// Character number of the selected text
        /// </summary>
        /// <remarks>
        /// Length is calculated as unicode count, so it counts 
        /// eacn \r\n combination as 2 - even though it is actially
        /// one caret position, and it would be illegal to insert 
        /// any characters between them or expect selection ends 
        /// to stay between them.
        /// Because of that after setting SelectionLength to some value 
        /// it can be automatically corrected (by adding 1)
        /// if selection end happens to be between \r and \n.
        /// </remarks>              
        public virtual int SelectionLength
        {
            get
            {
                return this.selectionLength;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Parameter must be greater than or equal to zero.");
                }

                // Identify new position for selection end
                var stringValue = this.text == null ? string.Empty : this.text;
                value = value > stringValue.Length ? stringValue.Length : value;
                value = this.selectionStart + value > stringValue.Length ? stringValue.Length - this.selectionStart : value;
                var changed = this.selectionLength != value;
                this.selectionLength = value;
                this.Properties[TextBox.TextBoxSelectionLengthChanged] = changed;
                if (changed)
                {
                    this.OnPropertyChanged("SelectionLength");
                }
            }
        }

        /// <summary>
        /// The start position of the selection. 
        /// </summary>
        /// <remarks>
        /// Index is calculated as unicode offset, so it counts
        /// eacn \r\n combination as 2 - even though it is actially 
        /// one caret position, and it would be illegal to insert
        /// any characters between them or expect selection ends 
        /// to stay between them. 
        /// Because of that after setting SelectionStart to some value
        /// it can be automatically corrected (by adding 1) 
        /// if it happens to be between \r and \n.
        /// </remarks>
        public virtual int SelectionStart
        {
            get
            {
                return this.selectionStart;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Parameter must be greater than or equal to zero.");
                }

                var stringValue = this.text == null ? string.Empty : this.text;
                value = value > stringValue.Length ? stringValue.Length : value;
                var changed = this.selectionStart != value;
                this.selectionStart = value;
                this.selectionLength = value + this.selectionLength > stringValue.Length ? stringValue.Length - this.selectionStart : this.selectionLength;
                this.Properties[TextBox.TextBoxSelectionStartChanged] = changed;
                if (changed)
                {
                    this.OnPropertyChanged("SelectionStart");
                }
            }
        }

        /// <summary> 
        /// Position of the caret.
        /// </summary> 
        public virtual int CaretIndex
        {
            get
            {
                return this.selectionStart;
            }

            set
            {
                this.Select(value, 0);
            }
        }

        /// <summary>
        /// Select the text in the given position and length.
        /// </summary>
        public virtual void Select(int start, int length)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "Parameter must be greater than or equal to zero.");
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", "Parameter must be greater than or equal to zero.");
            }

            start = start > this.text.Length ? this.text.Length : start;
            this.selectionStart = start;
            this.selectionLength = start + length > this.text.Length ? this.text.Length - this.selectionStart : length;
        }

        /// <summary>
        /// Number of lines in the TextBox. 
        /// </summary>
        public virtual int LineCount
        {
            get
            {
                return this.text.Split(new[] { "\n" }, StringSplitOptions.None).Length;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts return keys.
        /// </summary> 
        public virtual bool AcceptsReturn
        {
            get
            {
                return this.acceptsReturn;
            }

            set
            {
                var changed = this.acceptsReturn != value;
                this.acceptsReturn = value;
                if (changed)
                {
                    this.OnPropertyChanged("AcceptsReturn");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts tab keys.
        /// </summary> 
        public virtual bool AcceptsTab
        {
            get
            {
                return this.acceptsTab;
            }

            set
            {
                var changed = this.acceptsTab != value;
                this.acceptsTab = value;
                if (changed)
                {
                    this.OnPropertyChanged("AcceptsTab");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public TextBox()
        {
            this.horizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.verticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.acceptsReturn = true;
        }
    }
}