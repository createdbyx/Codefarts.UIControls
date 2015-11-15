namespace Codefarts.UIControls
{
#if UNITY_5
    using UnityEngine;
#endif

    public class SolidColorBrush : Brush
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        /// <param name="color">The color of the brush.</param>
        public SolidColorBrush(Color color)
        {
            this.Color = color;
        }
                                     
        /// <summary>
        /// Gets or sets the color of the brush.
        /// </summary>
        public virtual Color Color { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        public SolidColorBrush()
        {   
        }
    }
}