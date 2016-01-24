namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// Defines column-specific properties that apply to <see cref="Grid" /> elements. 
    /// </summary>
    public class ColumnDefinition : DefinitionBase
    {
        /// <summary>
        /// The is offset value for the related property.
        /// </summary>
        protected float offset;

        /// <summary>
        /// The is width value for the related property.
        /// </summary>
        protected float width = 1;

        /// <summary>
        /// The is maxWidth value for the related property.
        /// </summary>
        protected float maxWidth;

        /// <summary>
        /// The is minWidth value for the related property.
        /// </summary>
        protected float minWidth;

        /// <summary>
        /// Gets or sets a value that represents the minimum allowable width of a <see cref="ColumnDefinition" />.  
        /// </summary>
        /// <returns>
        /// A <see cref="float" /> that represents the minimum allowable width. The default value is 0.
        /// </returns>
        public virtual float MinWidth
        {
            get
            {
                return this.minWidth;
            }

            set
            {
                var changed = Math.Abs(this.minWidth - value) < float.Epsilon;
                this.minWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinWidth");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that represents the maximum width of a <see cref="ColumnDefinition" />.  
        /// </summary>
        /// <returns>
        /// A <see cref="float" /> that represents the maximum width. 
        /// </returns>
        public virtual float MaxWidth
        {
            get
            {
                return this.maxWidth;
            }

            set
            {
                var changed = Math.Abs(this.maxWidth - value) < float.Epsilon;
                this.maxWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxWidth");
                }
            }
        }

        /// <summary>
        /// Gets the width of a <see cref="ColumnDefinition" /> element.   
        /// </summary>
        /// <returns>
        /// The width of the row. The default value is 1.0.
        /// </returns>
        public virtual float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                var changed = Math.Abs(this.width - value) < float.Epsilon;
                this.width = value;
                if (changed)
                {
                    this.OnPropertyChanged("Width");
                }
            }
        }

        /// <summary>
        /// Gets a value that represents the offset value of this <see cref="ColumnDefinition" />.
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