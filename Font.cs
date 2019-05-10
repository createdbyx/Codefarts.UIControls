namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Defines an abstract font class.
    /// </summary>
    public class Font : IMarkup
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
        public Font(string fontName)
            : this(fontName, 12)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Font" /> is bold.
        /// </summary>
        /// <returns>
        /// true if this <see cref="Font" /> is bold; otherwise, false.
        /// </returns>
        public virtual bool Bold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this font has the italic style applied.
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

        /// <summary>
        /// Converts to markup.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relevant information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup["FontName"] = this.FontName;
            markup["FontSize"] = this.FontSize;
            markup["Bold"] = this.Bold;
            markup["Italic"] = this.Italic;
            return markup;
        }
    }
}