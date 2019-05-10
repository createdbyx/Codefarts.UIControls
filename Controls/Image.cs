/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls.Controls
{
    /// <summary>
    /// Provides a image control.
    /// </summary>
    public class Image : Control
    {
        /// <summary>
        /// The backing field for the <see cref="Stretch"/> property.
        /// </summary>
        protected Stretch stretch;

        /// <summary>
        /// The backing field for the <see cref="Source"/> property.
        /// </summary>
        protected ImageSource source;

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public virtual ImageSource Source
        {
            get
            {
                return this.source;
            }

            set
            {
                var changed = this.source != value;
                this.source = value;
                if (changed)
                {
                    this.OnPropertyChanged("Source");
                }
            }
        }

        /// <summary>
        /// Gets or sets the stretch style.
        /// </summary>
        /// <remarks>Default is <see cref="UIControls.Stretch.None"/></remarks>
        public virtual Stretch Stretch
        {
            get
            {
                return this.stretch;
            }

            set
            {
                var changed = this.stretch != value;
                this.stretch = value;
                if (changed)
                {
                    this.OnPropertyChanged("Stretch");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.Stretch = Stretch.None;
            this.isTabStop = false;
            this.canFocus = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">The image source.</param>
        public Image(ImageSource source) : this()
        {
            this.Source = source;
        }
    }
}