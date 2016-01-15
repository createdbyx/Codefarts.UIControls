namespace Codefarts.UIControls.Factories
{
    /// <summary>
    /// Provides the base factory implementation.
    /// </summary>
    public abstract class UIControlsFactory
    {
        /// <summary>
        /// Gets or sets a singleton instance of a <see cref="UIControlsFactory"/>.
        /// </summary>
        public static UIControlsFactory Instance { get; set; }

        /// <summary>
        /// Creates a font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="size">The size of the font text.</param>
        /// <returns>A reference to a font class.</returns>
        public abstract Font CreateFont(string fontName, float size);
    }
}
