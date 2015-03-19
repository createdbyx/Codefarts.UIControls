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
        /// Gets or sets the image source.
        /// </summary>              
        public ImageSource Source { get; set; }

        /// <summary>
        /// Gets or sets the stretch style.
        /// </summary>            
        /// <remarks>Default is <see cref="Stretch.None"/></remarks>         
        public Stretch Stretch { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.Stretch = Stretch.None;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="source">The image source.</param>
        public Image(ImageSource source)
        {
            this.Source = source;
        }
    }
}