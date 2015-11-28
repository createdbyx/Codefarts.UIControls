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

    public class TextBox : Control
    {
        public event System.EventHandler<RoutedPropertyChangedEventArgs<string>> TextChanged;


        protected string text;

        /// <summary>
        /// The selection lengthHolds the selection length value.
        /// </summary>
        private int selectionLength;

        private int selectionStart;

        public ScrollBarVisibility HorizontalScrollBarVisibility { get; set; }
        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; }
        public float HorizontalOffset { get; set; }
        public float VerticalOffset { get; set; }

        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                var changed = this.text != value;
                if (!changed)
                {
                    return;
                }

                value = !this.AcceptsReturn ? value.Replace("\r\n", string.Empty) : value;

                var oldValue = this.text;
                this.text = value;
                this.OnTextChanged(new RoutedPropertyChangedEventArgs<string>(oldValue, value));
            }
        }


        protected virtual void OnTextChanged(RoutedPropertyChangedEventArgs<string> e)
        {
            var handler = this.TextChanged;
            if (handler != null)
            {
                handler(this, e);
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
        public int SelectionLength
        {
            get
            {
                return this.selectionLength;
            }

            set
            {
                if (value == this.selectionLength)
                {
                    return;
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Parameter must be greater than or equal to zero.");
                }

                // Identify new position for selection end
                value = value > this.text.Length ? this.text.Length : value;
                value = this.selectionStart + value > this.text.Length ? this.text.Length - this.selectionStart : value;
                this.selectionLength = value;
                var props = this.ExtendedProperties;
                if (props == null)
                {
                    props = new PropertyCollection();
                    props = this.ExtendedProperties;
                }

                props["SelectionLengthChanged - B1B9862F-7C70-4959-A208-157311FB475F"] = true;
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
        public int SelectionStart
        {
            get
            {
                return this.selectionStart;
            }

            set
            {
                if (value == this.selectionStart)
                {
                    return;
                }

                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", "Parameter must be greater than or equal to zero.");
                }

                var stringValue = this.text == null ? string.Empty : this.text;
                value = value > stringValue.Length ? stringValue.Length : value;
                this.selectionStart = value;
                this.selectionLength = value + this.selectionLength > stringValue.Length ? stringValue.Length - this.selectionStart : this.selectionLength;

                var props = this.ExtendedProperties;
                if (props == null)
                {
                    props = new PropertyCollection();
                    this.ExtendedProperties = props;
                }

                props["SelectionStartChanged - 4BB16D2B-113E-42BD-8339-2E7EEC0B1C08"] = true;
            }
        }

        /// <summary> 
        /// Position of the caret.
        /// </summary> 
        public int CaretIndex
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
        public void Select(int start, int length)
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
        public int LineCount
        {
            get
            {
                return this.text.Split(new[] { "\n" }, StringSplitOptions.None).Length;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts return keys.
        /// </summary> 
        public virtual bool AcceptsReturn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the text box accepts tab keys.
        /// </summary> 
        public bool AcceptsTab { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public TextBox()
        {
            this.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.AcceptsReturn = true;
        }
    }
}