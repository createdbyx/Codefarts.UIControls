namespace Codefarts.UIControls
{
    using System.ComponentModel;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Represents a object type that has a width, height, and <see cref="ImageMetadata" /> such as a <see cref="BitmapSource" />.
    /// This is an abstract class.
    /// </summary>
    public abstract class ImageSource : INotifyPropertyChanged, IMarkup
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

        /// <summary>
        /// Used to raise the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Implementation of IMarkup

        /// <summary>
        /// Builds a <see cref="Markup"/> object that represent the state of the implementor.
        /// </summary>
        /// <returns>A <see cref="Markup"/> object containing the relavent information.</returns>
        /// <remarks>
        /// <p>The returned <see cref="Markup"/> object contains the relavnet data stored by the implementor.</p>
        /// </remarks>
        public Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Properties["Width"] = this.Width;
            markup.Properties["Height"] = this.Height;
            return markup;
        }

        #endregion
    }
}
