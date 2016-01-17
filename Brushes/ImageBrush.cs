namespace Codefarts.UIControls
{
    /// <summary>
    /// Paints an area with an image. 
    /// </summary>
    public class ImageBrush : TileBrush
    {
        /// <summary>
        /// The image source field use by the <see cref="ImageSource"/> property.
        /// </summary>
        private ImageSource imageSource;

        /// <summary>Initializes a new instance of the <see cref="ImageBrush" /> class that paints an area with the specified image. </summary>
        /// <param name="source">The image to display.</param>
        public ImageBrush(ImageSource source)
        {
            this.imageSource = source;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageBrush" /> class with no content.
        /// </summary>
        public ImageBrush()
        {
        }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>   
        public virtual ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }

            set
            {
                this.imageSource = value;
            }
        }
    }
}