namespace Codefarts.UIControls
{
#if UNITY_5
    using UnityEngine;
#endif

    /// <summary>
    /// Paints an area with a solid color. 
    /// </summary>
    public class SolidColorBrush : Brush
    {
        /// <summary>
        /// The color for the <see cref="Color"/> property.
        /// </summary>
        private Color color;

        /// <summary>
        /// The is read only flag that determine weather or not hte <see cref="Color"/> property can be set.
        /// </summary>
        private bool isReadOnly;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        /// <param name="color">The color of the brush.</param>
        public SolidColorBrush(Color color)
        {
            this.color = color;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        /// <param name="color">The color of the brush.</param>
        /// <param name="readOnly">Sets the <see cref="isReadOnly"/> flag for the brush.</param>
        internal SolidColorBrush(Color color, bool readOnly)
        {
            this.color = color;
            this.isReadOnly = readOnly;
        }

        /// <summary>
        /// Gets or sets the color of the brush.
        /// </summary>
        public virtual Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                if (this.isReadOnly)
                {
                    return;
                }

                var changes = this.color != value;
                this.color = value;
                if (changes)
                {
                    this.OnPropertyChanged("Color");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidColorBrush"/> class.
        /// </summary>
        public SolidColorBrush()
        {   
        }
    }
}