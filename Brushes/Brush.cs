namespace Codefarts.UIControls
{
    /// <summary>
    /// Defines objects used to paint graphical objects. Classes that derive from <see cref="Brush" /> describe how the area is painted.       
    /// </summary>
    public abstract class Brush
    {
        /// <summary>
        /// The opacity field used by the <see cref="Opacity"/> property.
        /// </summary>
        protected float opacity = 1;

        /// <summary>
        /// Gets or sets the degree of opacity of a <see cref="Brush" />. 
        ///  </summary>
        /// <returns>
        /// The value of the <see cref="Brush.Opacity" /> property is expressed as a value between 0.0 and 1.0. The default value is 1.0. 
        /// </returns>
        public virtual float Opacity
        {
            get
            {
                return this.opacity;
            }

            set
            {
                this.opacity = value;
            }
        }

    }
}
