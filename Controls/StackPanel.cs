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
    /// Arranges child elements into a single line that can be oriented horizontally or vertically.
    /// </summary>
    public class StackPanel : Control
    {
        /// <summary>
        /// The orientation value used by the <see cref="Orientation"/> property.
        /// </summary>
        protected Orientation orientation;

        /// <summary>
        /// Initializes a new instance of the <see cref="StackPanel" /> class.
        /// </summary>
        /// <param name="orientation">The orientation value that indicates the dimension by which child elements are stacked.</param>
        public StackPanel(Orientation orientation)
                    : this()
        {
            this.orientation = orientation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StackPanel" /> class.
        /// </summary>
        public StackPanel()
        {
        }

        /// <summary>
        /// Gets or sets a value that indicates the dimension by which child elements are stacked.  
        /// </summary>
        /// <returns>
        /// The <see cref="Orientation" /> of child content.
        /// </returns>
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
    }
}