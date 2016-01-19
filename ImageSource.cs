namespace Codefarts.UIControls
{
    using System.ComponentModel;

    /// <summary>
    /// Represents a object type that has a width, height, and <see cref="ImageMetadata" /> such as a <see cref="BitmapSource" />.
    /// This is an abstract class.
    /// </summary>
    public abstract class ImageSource : INotifyPropertyChanged
    {
        /// <summary>
        /// The value for the <see cref="Metadata"/> property.
        /// </summary>
        protected ImageMetadata metadata;

        /// <summary>
        /// The value for the <see cref="Height"/> property.
        /// </summary>
        protected int height;

        /// <summary>
        /// The value for the <see cref="Width"/> property.
        /// </summary>
        protected int width;

        /// <summary>
        /// Gets the image width.
        /// </summary>      
        public virtual int Width
        {
            get
            {
                return this.width;
            }

            private set
            {
                var changed = this.width != value;
                this.width = value;
                if (changed)
                {
                    this.OnPropertyChanged("Width");
                }
            }
        }

        /// <summary>
        /// Gets the image height.
        /// </summary>
        public virtual int Height
        {
            get
            {
                return this.height;
            }

            private set
            {
                var changed = this.height != value;
                this.height = value;
                if (changed)
                {
                    this.OnPropertyChanged("Height");
                }
            }
        }

        /// <summary>
        /// Gets the metadata for the image source.
        /// </summary>    
        public virtual ImageMetadata Metadata
        {
            get
            {
                return this.metadata;
            }

            private set
            {
                var changed = this.metadata != value;
                this.metadata = value;
                if (changed)
                {
                    this.OnPropertyChanged("Metadata");
                }
            }
        }

        /// <summary>
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var onPropertyChanged = this.PropertyChanged;
            if (onPropertyChanged != null)
            {
                onPropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
