namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// The Control extension methods.
    /// </summary>
    public static class ControlExtensionMethods
    {
        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property being retrieved.</typeparam>
        /// <param name="control">The control to retrieve the property value from.</param>
        /// <param name="name">The name of the property.</param>
        public static T GetProperty<T>(this Control control, string name)
        {
            return GetProperty<T>(control, name, default(T));
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        /// <typeparam name="T">The type of the property being retrieved.</typeparam>
        /// <param name="control">The control to retrieve the property value from.</param>
        /// <param name="name">The name of the property.</param>
        /// <param name="defaultValue">The default value that will be returned if the property can not be found.</param>
        /// <returns>Returns the value of the property, otherwise returns the <see cref="defaultValue"/> that was specified.</returns>
        /// <exception cref="System.ArgumentNullException">control</exception>
        /// <seealso cref="GetProperty{T}(Codefarts.UIControls.Control,string)"/>
        public static T GetProperty<T>(this Control control, string name, T defaultValue)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            object value;
            if (control.Properties.TryGetValue(name, out value))
            {
                return (T)value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the grid position information (if any) from the controls <see cref="Control.Properties"/> property.
        /// </summary>
        /// <param name="control">The control to retrieve grid information from.</param>
        /// <returns>The grid position of the control within the parent grid.</returns>
        /// <remarks>If no grid position informaton exists the default value of zero will be ued for both Row & Column.</remarks>
        /// <seealso cref="Grid.Row"/>
        /// <seealso cref="Grid.Column"/>
        public static Point GetGridPosition(this Control control)
        {
            var childRow = control.GetProperty(Grid.Row, 0);
            var childColumn = control.GetProperty(Grid.Column, 0);
            return new Point(childColumn, childRow);
        }
    }
}