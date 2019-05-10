namespace Codefarts.UIControls
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Contains state information and event data associated with a event.
    /// </summary>
    public class RoutedEventArgs : EventArgs  , INotifyPropertyChanged
    {
        /// <summary>
        /// The backing field for the <see cref="Source"/> property.
        /// </summary>
        private object source;

        /// <summary>
        /// The backing field for the <see cref="Handled"/> property.
        /// </summary>
        private bool handled;

        /// <summary>Gets or sets a value that indicates the present state of the event handling for a routed event as it travels the route. </summary>
        /// <returns>If setting, set to true if the event is to be marked handled; otherwise false. If reading this value, true indicates that either a
        /// class handler, or some instance handler along the route, has already marked this event handled. false indicates that no such handler
        /// has marked the event handled. The default value is false.</returns>
        public bool Handled
        {
            get
            {
                return this.handled;
            }

            set
            {
                var changed = this.handled != value;
                this.handled = value;
                if (changed)
                {
                    this.OnPropertyChanged("Handled");
                }
            }
        }

        /// <summary>Gets or sets a reference to the object that raised the event. </summary>
        /// <returns>The object that raised the event.</returns>
        public object Source
        {
            get
            {
                return this.source;
            }

            set
            {
                var changed = this.source != value;
                this.source = value;
                if (changed)
                {
                    this.OnPropertyChanged("Source");
                }
            }
        }

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class InputEventArgs : RoutedEventArgs
    {

    }

    public class KeyboardEventArgs : InputEventArgs
    {

    }
}