namespace Codefarts.UIControls
{
    /// <summary>
    /// Provides data about a change in value to a dependency property as reported by particular routed events, including the previous and current value of the property that changed.
    /// </summary>
    /// <typeparam name="T">The type of the dependency property that has changed.</typeparam>
    public class PropertyChangedEventArgs<T> : RoutedEventArgs
    {
        private T oldValue;
        private T newValue;
        private string propertyName;

        /// <summary>
        /// Gets the property name that raised the event.
        /// </summary>
        /// <returns>
        /// The the property name that was responcible for changing the event.
        /// </returns>
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        /// <summary>
        /// Gets the previous value of the property as reported by a property changed event.
        /// </summary>
        /// <returns>
        /// The generic value. In a practical implementation of the <see cref="PropertyChangedEventArgs{T}"/>, the generic type of this property is replaced with the constrained type of the implementation.
        /// </returns>
        public T OldValue
        {
            get
            {
                return this.oldValue;
            }
        }

        /// <summary>
        /// Gets the new value of a property as reported by a property changed event.
        /// </summary>
        /// <returns>
        /// The generic value. In a practical implementation of the <see cref="PropertyChangedEventArgs{T}"/>, the generic type of this property is replaced with the constrained type of the implementation.
        /// </returns>
        public T NewValue
        {
            get
            {
                return this.newValue;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class, with provided old and new values.
        /// </summary>
        /// <param name="oldValue">Previous value of the property, prior to the event being raised.</param><param name="newValue">Current value of the property at the time of the event.</param>
        public PropertyChangedEventArgs(T oldValue, T newValue)
            : this()
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class.
        /// </summary>
        public PropertyChangedEventArgs()
        {
        }
    }
}