namespace Codefarts.UIControls
{
    public abstract class ImageSource
    {
        /// <summary>
        /// Gets the image width.
        /// </summary>      
        public virtual int Width { get; private set; }

        /// <summary>
        /// Gets the image height.
        /// </summary>
        public virtual int Height { get; private set; }

        /// <summary>
        /// Gets the metadata for the image source.
        /// </summary>    
        public virtual ImageMetadata Metadata { get; private set; }
    }
}
