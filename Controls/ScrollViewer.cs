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
    /// Represents a scrollable area that can contain other controls.
    /// </summary>
    public class ScrollViewer : Control
    {
        /// <summary>
        /// The backing field for the <see cref="VerticalScrollBarVisibility"/> property.
        /// </summary>
        protected ScrollBarVisibility verticalScrollBarVisibility;

        /// <summary>
        /// The backing field for the <see cref="HorizontialScrollBarVisibility"/> property.
        /// </summary>
        protected ScrollBarVisibility horizontialScrollBarVisibility;

        /// <summary>
        /// The backing field for the <see cref="VerticalOffset"/> property.
        /// </summary>
        protected float verticalOffset;

        /// <summary>
        /// The backing field for the <see cref="HorizontialOffset"/> property.
        /// </summary>
        protected float horizontialOffset;

        #region Overrides of Control

        /// <summary>Gets the default size of the control.</summary>
        /// <returns>The default <see cref="Control.Size" /> of the control.</returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets a value that indicates whether a horizontal <see cref="T:ScrollBar" /> should be displayed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:ScrollBarVisibility" /> value that indicates whether a horizontal <see cref="T:ScrollBar" /> should be displayed.
        /// The default is <see cref="ScrollBarVisibility.Auto" />.
        /// </returns>
        public virtual ScrollBarVisibility HorizontialScrollBarVisibility
        {
            get
            {
                return this.horizontialScrollBarVisibility;
            }

            set
            {
                var changed = this.horizontialScrollBarVisibility != value;
                this.horizontialScrollBarVisibility = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontialScrollBarVisibility");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether a vertical <see cref="T:ScrollBar" /> should be displayed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:ScrollBarVisibility" /> value that indicates whether a vertical <see cref="T:ScrollBar" /> should be displayed.
        /// The default is <see cref="ScrollBarVisibility.Auto" />.
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
        /// Gets a value that contains the horizontal offset of the scrolled content.
        /// </summary>
        /// <returns>
        /// A <see cref="Single" /> that represents the horizontal offset of the scrolled content. The default is 0.0.
        /// </returns>
        public virtual float HorizontialOffset
        {
            get
            {
                return this.horizontialOffset;
            }

            set
            {
                var changed = Math.Abs(this.horizontialOffset - value) > float.Epsilon;
                this.horizontialOffset = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontialOffset");
                }
            }
        }

        /// <summary>
        /// Gets a value that contains the vertical offset of the scrolled content.
        /// </summary>
        /// <returns>
        /// A <see cref="Single" /> that represents the vertical offset of the scrolled content. The default is 0.0.
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
        /// Scroll content by one line to the top.
        /// </summary>
        public virtual void LineUp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one line to the bottom.
        /// </summary>
        public virtual void LineDown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one line to the left.
        /// </summary>
        public virtual void LineLeft()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one line to the right.
        /// </summary>
        public virtual void LineRight()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the top.
        /// </summary>
        public virtual void PageUp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the bottom.
        /// </summary>
        public virtual void PageDown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the left.
        /// </summary>
        public virtual void PageLeft()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the right.
        /// </summary>
        public virtual void PageRight()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Horizontally scroll to the beginning of the content.
        /// </summary>
        public virtual void ScrollToLeftEnd()
        {
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary>
        /// Horizontally scroll to the end of the content.
        /// </summary>
        public virtual void ScrollToRightEnd()
        {
            this.HorizontialOffset = int.MaxValue;
        }

        /// <summary>
        /// Scroll to Top-Left of the content.
        /// </summary>
        public virtual void ScrollToHome()
        {
            this.VerticalOffset = int.MinValue;
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary>
        /// Scroll to Bottom-Left of the content.
        /// </summary>
        public virtual void ScrollToEnd()
        {
            this.VerticalOffset = int.MaxValue;
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary>
        /// Vertically scroll to the beginning of the content.
        /// </summary>
        public virtual void ScrollToTop()
        {
            this.VerticalOffset = int.MinValue;
        }

        /// <summary>
        /// Vertically scroll to the end of the content.
        /// </summary>
        public virtual void ScrollToBottom()
        {
            this.VerticalOffset = int.MaxValue;
        }

        /// <summary>
        /// Scroll horizontally to specified offset. Not guaranteed to end up at the specified offset though.
        /// </summary>
        /// <param name="offset">The offset amount to try and scroll.</param>
        public virtual void ScrollToHorizontalOffset(float offset)
        {
            this.HorizontialOffset += offset;
        }

        /// <summary>
        /// Scroll vertically to specified offset. Not guaranteed to end up at the specified offset though.
        /// </summary>
        /// <param name="offset">The offset amount to try and scroll.</param>
        public virtual void ScrollToVerticalOffset(float offset)
        {
            this.VerticalOffset += offset;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollViewer"/> class.
        /// </summary>
        public ScrollViewer()
        {
            this.horizontialScrollBarVisibility = ScrollBarVisibility.Auto;
            this.verticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.canFocus = false;
            this.isTabStop = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollViewer"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ScrollViewer(string name) : this()
        {
            this.name = name;
        }
    }
}