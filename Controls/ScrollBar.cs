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
    /// Provides a scroll bar control.
    /// </summary>
    public class ScrollBar : RangeBase
    {
        /// <summary>
        /// The backing field for the <see cref="Orientation"/> property.
        /// </summary>
        protected Orientation orientation;

        /// <summary>
        /// Gets or sets a value that determines the orientation of the slider.  
        /// </summary>
        public virtual Orientation Orientation
        {
            get
            {
                return this.orientation;
            }

            set
            {
                var changed = this.orientation != value;
                this.orientation = value;
                if (changed)
                {
                    this.OnPropertyChanged("Orientation");
                }
            }
        }
    }
}