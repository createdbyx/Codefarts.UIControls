namespace Codefarts.UIControls
{
    /// <summary>
    /// Paints an area with an image.
    /// </summary>
    public class SkinningBrush : GridImageBrush
    {
        /// <summary>
        /// Gets or sets the normal rectangle in texture cordanants 0 to 1.
        /// </summary>
        public Rectangle Normal { get; set; }

        /// <summary>
        /// Gets or sets the Active rectangle in texture cordanants 0 to 1.
        /// </summary>
        public Rectangle Active { get; set; }

        /// <summary>
        /// Gets or sets the Focused rectangle in texture cordanants 0 to 1.
        /// </summary>
        public Rectangle Focused { get; set; }

        /// <summary>
        /// Gets or sets the Inactive rectangle in texture cordanants 0 to 1.
        /// </summary>
        public Rectangle Inactive { get; set; }

        /// <summary>
        /// Gets or sets the Hover rectangle in texture cordanants 0 to 1.
        /// </summary>
        public Rectangle Hover { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SkinningBrush" /> class that paints an area with the specified image. </summary>
        /// <param name="source">The image to display.</param>
        public SkinningBrush(ImageSource source)
            : base(source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkinningBrush" /> class with no content.
        /// </summary>
        public SkinningBrush()
        {
        }
    }
}