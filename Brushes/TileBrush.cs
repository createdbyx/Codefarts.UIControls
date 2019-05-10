namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Describes a way to paint a region by using one or more tiles.
    /// </summary>
    public abstract class TileBrush : Brush
    {
        /// <summary>
        /// The alignment x value for the <see cref="AlignmentX"/> property.
        /// </summary>
        private AlignmentX alignmentX;

        /// <summary>
        /// The alignment y value for the <see cref="AlignmentY"/> property.
        /// </summary>
        private AlignmentY alignmentY;

        /// <summary>
        /// The alignment stretch value for the <see cref="Stretch"/> property.
        /// </summary>
        private Stretch stretch;

        /// <summary>
        /// The alignment tile mode value for the <see cref="TileMode"/> property.
        /// </summary>
        private TileMode tileMode;

        /// <summary>
        /// Gets or sets the horizontal alignment of content in the <see cref="TileBrush" /> base tile.
        /// </summary>
        /// <returns>
        /// A value that specifies the horizontal position of <see cref="TileBrush" /> content in its base tile. The default value is <see cref="F:HorizontalAlignment.Center" />.
        /// </returns>
        public AlignmentX AlignmentX
        {
            get
            {
                return this.alignmentX;
            }

            set
            {
                var changed = this.alignmentX != value;
                this.alignmentX = value;
                if (changed)
                {
                    this.OnPropertyChanged("AlignmentX");
                }
            }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of content in the <see cref="TileBrush" /> base tile.
        /// </summary>
        /// <returns>
        /// A value that specifies the vertical position of <see cref="TileBrush" /> content in its base tile. The default value is <see cref="F:AlignmentY.Center" />.
        /// </returns>
        public AlignmentY AlignmentY
        {
            get
            {
                return this.alignmentY;
            }

            set
            {
                var changed = this.alignmentY != value;
                this.alignmentY = value;
                if (changed)
                {
                    this.OnPropertyChanged("AlignmentY");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies how the content of this <see cref="TileBrush" /> stretches to fit its tiles.
        /// </summary>
        /// <returns>
        /// A value that specifies how this <see cref="TileBrush" /> content is projected onto its base tile. The default value is <see cref="F:Stretch.Fill" />.
        /// </returns>
        public Stretch Stretch
        {
            get
            {
                return this.stretch;
            }

            set
            {
                var changed = this.stretch != value;
                this.stretch = value;
                if (changed)
                {
                    this.OnPropertyChanged("Stretch");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that specifies how a <see cref="TileBrush" /> fills the area that you are painting if the base tile is smaller than the output area.
        /// </summary>
        /// <returns>
        /// A value that specifies how the <see cref="TileBrush" /> tiles fill the output area. The default value is <see cref="F:TileMode.None" />.
        /// </returns>
        public TileMode TileMode
        {
            get
            {
                return this.tileMode;
            }

            set
            {
                var changed = this.tileMode != value;
                this.tileMode = value;
                if (changed)
                {
                    this.OnPropertyChanged("TileMode");
                }
            }
        }

        /// <summary>
        /// Converts to markup.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relevant information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
        public override Markup ToMarkup()
        {
            var markup = base.ToMarkup();
            markup.Name = this.GetType().FullName;
            markup["AlignmentX"] = this.AlignmentX;
            markup["AlignmentY"] = this.AlignmentY;
            markup["Stretch"] = this.Stretch;
            markup["TileMode"] = this.TileMode;
            return markup;
        }
    }
}