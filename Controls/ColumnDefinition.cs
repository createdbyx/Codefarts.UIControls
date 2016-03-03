namespace Codefarts.UIControls
{
    using System;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

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
        protected float width;

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
                value = Math.Min(value, Math.Abs(this.MaxWidth) < float.Epsilon ? value : this.MaxWidth);
                var changed = Math.Abs(this.minWidth - value) < float.Epsilon;
                this.minWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinWidth");
                }

                this.Width = this.width;
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
                value = Math.Max(value, Math.Abs(this.MinWidth) < float.Epsilon ? value : this.MinWidth);
                var changed = Math.Abs(this.maxWidth - value) < float.Epsilon;
                this.maxWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxWidth");
                }

                this.Width = this.width;
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
                value = Math.Abs(this.MinWidth) > float.Epsilon && value < this.MinWidth ? this.MinWidth : value;
                value = Math.Abs(this.MaxWidth) > float.Epsilon && value > this.MaxWidth ? this.MaxWidth : value;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition" /> class.
        /// </summary>
        /// <param name="width">The width of the column.</param>
        public ColumnDefinition(float width) : this()
        {
            this.width = width;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition" /> class.
        /// </summary>
        /// <param name="width">The width of the column.</param>
        /// <param name="maxWidth">The maximum width of the column.</param>
        /// <param name="minWidth">The minimum width of the column.</param>
        public ColumnDefinition(float width, float maxWidth, float minWidth)
        {
            this.width = width;
            this.maxWidth = maxWidth;
            this.minWidth = minWidth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnDefinition" /> class.
        /// </summary>
        public ColumnDefinition()
        {
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
            markup.SetProperty("Width", Math.Abs(this.Width - 1) > float.Epsilon, this.Width);
            markup.SetProperty("MaxWidth", Math.Abs(this.MaxWidth) > float.Epsilon, this.MaxWidth);
            markup.SetProperty("MinWidth", Math.Abs(this.MinWidth) > float.Epsilon, this.MinWidth);
            return markup;
        }
    }
}