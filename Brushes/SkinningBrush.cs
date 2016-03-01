namespace Codefarts.UIControls
{
    using UnityEngine;

    /// <summary>
    /// Paints an area with an image. 
    /// </summary>
    public class SkinningBrush : GridImageBrush
    {
        /// <summary>
        /// Gets or sets the normal rect in pixels.
        /// </summary>
        public Rect Normal { get; set; }
        public Rect Active { get; set; }
        public Rect Focused { get; set; }
        public Rect Inactive { get; set; }
        public Rect Hover { get; set; }


    }
}