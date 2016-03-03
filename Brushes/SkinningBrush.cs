namespace Codefarts.UIControls
{
    using UnityEngine;

    /// <summary>
    /// Paints an area with an image. 
    /// </summary>
    public class SkinningBrush : GridImageBrush
    {
        /// <summary>
        /// Gets or sets the normal rect in texture cordanants 0 to 1.
        /// </summary>
        public Rect Normal { get; set; }

        /// <summary>
        /// Gets or sets the Active rect in texture cordanants 0 to 1.
        /// </summary>
        public Rect Active { get; set; }

        /// <summary>
        /// Gets or sets the Focused rect in texture cordanants 0 to 1.
        /// </summary>
        public Rect Focused { get; set; }

        /// <summary>
        /// Gets or sets the Inactive rect in texture cordanants 0 to 1.
        /// </summary>
        public Rect Inactive { get; set; }

        /// <summary>
        /// Gets or sets the Hover rect in texture cordanants 0 to 1.
        /// </summary>
        public Rect Hover { get; set; }

        /// <summary>Initializes a new instance of the <see cref="SkinningBrush" /> class that paints an area with the specified image. </summary>
        /// <param name="source">The image to display.</param>
        public SkinningBrush(ImageSource source) : base(source)
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