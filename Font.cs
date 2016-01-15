namespace Codefarts.UIControls
{
    /// <summary>
    /// Defines an abstract font class.
    /// </summary>
    public class Font
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        public Font()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="fontSize">Size of the font.</param>
        public Font(string fontName, float fontSize)
        {
            this.FontName = fontName;
            this.FontSize = fontSize;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        public Font(string fontName) : this(fontName, 12)
        {
        }

        /// <summary>
        /// Gets a value that indicates whether this <see cref="Font" /> is bold.
        /// </summary>
        /// <returns>
        /// true if this <see cref="Font" /> is bold; otherwise, false.
        /// </returns>
        public virtual bool Bold { get; set; }

        /// <summary>
        /// Gets a value that indicates whether this font has the italic style applied.
        /// </summary>
        /// <returns>
        /// true to indicate this font has the italic style applied; otherwise, false.
        /// </returns>
        public virtual bool Italic { get; set; }

        /// <summary>
        /// Gets or sets the name of the font family.
        /// </summary>
        public virtual string FontName { get; set; }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        public virtual float FontSize { get; set; }
    }
}