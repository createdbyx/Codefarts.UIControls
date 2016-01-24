namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Defines row-specific properties that apply to <see cref="Grid" /> elements.
    /// </summary>
    public class RowDefinition : DefinitionBase
    {
        /// <summary>
        /// The is offset value for the related property.
        /// </summary>
        protected float offset;

        /// <summary>
        /// The is height value for the related property.
        /// </summary>
        protected float height = 1;

        /// <summary>
        /// The is maxHeight value for the related property.
        /// </summary>
        protected float maxHeight;

        /// <summary>
        /// The is minHeight value for the related property.
        /// </summary>
        protected float minHeight;

        /// <summary>
        /// Gets or sets a value that represents the minimum allowable height of a <see cref="RowDefinition" />.  
        /// </summary>
        /// <returns>
        /// A <see cref="float" /> that represents the minimum allowable height. The default value is 0.
        /// </returns>
        public virtual float MinHeight
        {
            get
            {
                return this.minHeight;
            }

            set
            {
                var changed = Math.Abs(this.minHeight - value) < float.Epsilon;
                this.minHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinHeight");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the maximum height of a <see cref="RowDefinition" />.  
        /// </summary>
        /// <returns>
        /// A <see cref="float" /> that represents the maximum height. 
        /// </returns>
        public virtual float MaxHeight
        {
            get
            {
                return this.maxHeight;
            }

            set
            {
                var changed = Math.Abs(this.maxHeight - value) < float.Epsilon;
                this.maxHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxHeight");
                }
            }
        }

        /// <summary>
        /// Gets the height of a <see cref="RowDefinition" /> element.   
        /// </summary>
        /// <returns>
        /// The height of the row. The default value is 1.0.
        /// </returns>
        public virtual float Height
        {
            get
            {
                return this.height;
            }
            set
            {
                var changed = Math.Abs(this.height - value) < float.Epsilon;
                this.height = value;
                if (changed)
                {
                    this.OnPropertyChanged("Height");
                }
            }
        }

        /// <summary>
        /// Gets a value that represents the offset value of this <see cref="RowDefinition" />.
        /// </summary>
        /// <returns>
        /// A <see cref="float" /> that represents the offset of the row. The default value is 0.0.
        /// </returns>
        public virtual float Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                var changed = Math.Abs(this.offset - value) < float.Epsilon;
                this.offset = value;
                if (changed)
                {
                    this.OnPropertyChanged("Offset");
                }
            }
        }
    }
}