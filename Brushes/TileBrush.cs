namespace Codefarts.UIControls
{
    /// <summary>
    /// Describes a way to paint a region by using one or more tiles. 
    /// </summary>
    public abstract class TileBrush : Brush
    {
        /// <summary>
        /// Gets or sets the horizontal alignment of content in the <see cref="TileBrush" /> base tile. This is a dependency property. 
        /// </summary>
        /// <returns>
        /// A value that specifies the horizontal position of <see cref="TileBrush" /> content in its base tile. The default value is <see cref="F:HorizontalAlignment.Center" />.
        /// </returns>
        public AlignmentX AlignmentX { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of content in the <see cref="TileBrush" /> base tile. This is a dependency property. 
        /// </summary>
        /// <returns>
        /// A value that specifies the vertical position of <see cref="TileBrush" /> content in its base tile. The default value is <see cref="F:AlignmentY.Center" />.
        /// </returns>
        public AlignmentY AlignmentY { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies how the content of this <see cref="TileBrush" /> stretches to fit its tiles. This is a dependency property. 
        /// </summary>
        /// <returns>
        /// A value that specifies how this <see cref="TileBrush" /> content is projected onto its base tile. The default value is <see cref="F:Stretch.Fill" />.
        /// </returns>
        public Stretch Stretch { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies how a <see cref="TileBrush" /> fills the area that you are painting if the base tile is smaller than the output area. This is a dependency property. 
        /// </summary>
        /// <returns>
        /// A value that specifies how the <see cref="TileBrush" /> tiles fill the output area. The default value is <see cref="F:TileMode.None" />.
        /// </returns>
        public TileMode TileMode { get; set; }
    }
}