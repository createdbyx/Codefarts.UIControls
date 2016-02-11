namespace Codefarts.UIControls
{
    using System.IO;

    /// <summary>
    /// Provides the base factory implementation.
    /// </summary>
    public interface IUIControlsManager
    {
        /// <summary>
        /// Gets the control renderer.
        /// </summary>
        /// <returns>A reference to a <see cref="IControlRendererManager"/> implementation.</returns>
        IControlRendererManager Renderer { get; }

        /// <summary>
        /// Creates a font.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <param name="size">The size of the font text.</param>
        /// <returns>A reference to a font class.</returns>
        Font CreateFont(string fontName, float size);

        /// <summary>
        /// Creates a bitmap source.
        /// </summary>
        /// <returns>A reference to a <see cref="BitmapSource"/> type.</returns>
        ImageSource CreateImageSource();

        /// <summary>
        /// Creates a bitmap source.
        /// </summary>
        /// <param name="width">The bitmap width.</param>
        /// <param name="height">The bitmap height.</param>
        /// <returns>A reference to a <see cref="BitmapSource"/> type.</returns>
        ImageSource CreateImageSource(int width, int height);

        /// <summary>
        /// Creates a bitmap source.
        /// </summary>
        /// <param name="stream">The stream where the bitmap will be read from.</param>
        /// <returns>A reference to a <see cref="BitmapSource"/> type.</returns>
        ImageSource CreateImageSource(Stream stream);
    }
}