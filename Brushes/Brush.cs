namespace Codefarts.UIControls
{
    using System;
    using System.ComponentModel;
    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Defines objects used to paint graphical objects. Classes that derive from <see cref="Brush" /> describe how the area is painted.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class Brush : INotifyPropertyChanged, IMarkup
    {
        /// <summary>
        /// The opacity field used by the <see cref="Opacity"/> property.
        /// </summary>
        private float opacity = 1;

        /// <summary>
        /// Raised when a brush property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the degree of opacity of a <see cref="Brush" />.
        /// </summary>
        /// <returns>
        /// The value of the <see cref="Brush.Opacity" /> property is expressed as a value between 0.0 and 1.0. The default value is 1.0.
        /// </returns>
        public virtual float Opacity
        {
            get
            {
                return this.opacity;
            }

            set
            {
                value = value < 0 ? 0 : value;
                value = value > 1 ? 1 : value;
                var changed = Math.Abs(this.opacity - value) > float.Epsilon;
                this.opacity = value;
                if (changed)
                {
                    this.OnPropertyChanged("Opacity");
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
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup["Opacity"] = this.Opacity;
            return markup;
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property hat changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
