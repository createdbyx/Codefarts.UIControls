namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Models;

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
                var changed = this.imageSource != value;
                this.imageSource = value;
                if (changed)
                {
                    this.OnPropertyChanged("ImageSource");
                }
            }
        }

        /// <summary>
        /// Converts to markup.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relevant information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
        public override Markup ToMarkup()
        {
            var markup = base.ToMarkup();
            markup.Name = this.GetType().FullName;
            var image = this.ImageSource;
            markup["ImageSource"] = image != null ? image.ToMarkup() : null;
            return markup;
        }
    }
}