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

    public class ScrollViewer : Control
    {
        protected ScrollBarVisibility verticalScrollBarVisibility;

        protected ScrollBarVisibility horizontialScrollBarVisibility;

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

        public float HorizontialOffset { get; set; }
        public float VerticalOffset { get; set; }

        /// <summary>
        /// Scroll content by one line to the top.
        /// </summary>
        public void LineUp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one line to the bottom. 
        /// </summary>                                                                                                             
        public void LineDown()
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Scroll content by one line to the left.
        /// </summary>
        public void LineLeft()
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Scroll content by one line to the right.
        /// </summary> 
        public void LineRight()
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Scroll content by one page to the top.
        /// </summary>
        public void PageUp()
        {
            throw new NotImplementedException();
        }

        /// <summary> 
        /// Scroll content by one page to the bottom.
        /// </summary> 
        public void PageDown()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the left. 
        /// </summary>
        public void PageLeft()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Scroll content by one page to the right. 
        /// </summary>
        public void PageRight()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Horizontally scroll to the beginning of the content. 
        /// </summary>
        public void ScrollToLeftEnd()
        {
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary>
        /// Horizontally scroll to the end of the content. 
        /// </summary>
        public void ScrollToRightEnd()
        {
            this.HorizontialOffset = int.MaxValue;
        }

        /// <summary>
        /// Scroll to Top-Left of the content. 
        /// </summary>
        public void ScrollToHome()
        {
            this.VerticalOffset = int.MinValue;
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary> 
        /// Scroll to Bottom-Left of the content.
        /// </summary> 
        public void ScrollToEnd()
        {
            this.VerticalOffset = int.MaxValue;
            this.HorizontialOffset = int.MinValue;
        }

        /// <summary> 
        /// Vertically scroll to the beginning of the content.
        /// </summary> 
        public void ScrollToTop()
        {
            this.VerticalOffset = int.MinValue;
        }

        /// <summary>
        /// Vertically scroll to the end of the content.
        /// </summary> 
        public void ScrollToBottom()
        {
            this.VerticalOffset = int.MaxValue;
        }

        /// <summary>
        /// Scroll horizontally to specified offset. Not guaranteed to end up at the specified offset though.
        /// </summary>
        /// <param name="offset">The offset amount to try and scroll.</param>
        public void ScrollToHorizontalOffset(float offset)
        {
            this.HorizontialOffset += offset;
        }

        /// <summary>
        /// Scroll vertically to specified offset. Not guaranteed to end up at the specified offset though.
        /// </summary>
        /// <param name="offset">The offset amount to try and scroll.</param>
        public void ScrollToVerticalOffset(float offset)
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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollViewer"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public ScrollViewer(string name)
        {
            this.name = name;
        }
    }
}