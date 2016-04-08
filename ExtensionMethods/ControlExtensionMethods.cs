namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// The Control extension methods.
    /// </summary>
    public static class ControlExtensionMethods
    {
        public static T GetProperty<T>(this Control control, string name)
        {
            return GetProperty<T>(control, name, default(T));
        }

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

        public static Point GetGridPosition(this Control control)
        {
            var childRow = control.GetProperty(Grid.Row, 0);
            var childColumn = control.GetProperty(Grid.Column, 0);
            return new Point(childColumn, childRow);
        }
    }
}