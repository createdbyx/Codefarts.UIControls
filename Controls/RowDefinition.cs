namespace Codefarts.UIControls
{
    using System;

    using Codefarts.UIControls.Models;

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
        protected float height;

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
                value = Math.Min(value, Math.Abs(this.MaxHeight) < float.Epsilon ? value : this.MaxHeight);
                var changed = Math.Abs(this.minHeight - value) < float.Epsilon;
                this.minHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinHeight");
                }


                this.Height = this.height;
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
                value = Math.Max(value, Math.Abs(this.MinHeight) < float.Epsilon ? value : this.MinHeight);
                var changed = Math.Abs(this.maxHeight - value) < float.Epsilon;
                this.maxHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxHeight");
                }

                this.Height = this.height;
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
                value = Math.Abs(this.MinHeight) > float.Epsilon && value < this.MinHeight ? this.MinHeight : value;
                value = Math.Abs(this.MaxHeight) > float.Epsilon && value > this.MaxHeight ? this.MaxHeight : value;
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

        /// <summary>
        /// Builds a <see cref="Markup" /> object that represent the state of the implementor.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relavent information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relavnet data stored by the implementor.</p>
        /// </remarks>
        public override Markup ToMarkup()
        {
            var markup = base.ToMarkup();
            markup.Name = this.GetType().FullName;
            markup.SetProperty("Offset", Math.Abs(this.Offset - 1) > float.Epsilon, this.Offset);
            markup.SetProperty("Height", Math.Abs(this.Height - 1) > float.Epsilon, this.Height);
            markup.SetProperty("MaxHeight", Math.Abs(this.MaxHeight) > float.Epsilon, this.MaxHeight);
            markup.SetProperty("MinHeight", Math.Abs(this.MinHeight) > float.Epsilon, this.MinHeight);
            return markup;
        }
    }
}