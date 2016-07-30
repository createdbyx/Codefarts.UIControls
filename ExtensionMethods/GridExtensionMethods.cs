namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// The grid extension methods.
    /// </summary>
    public static class GridExtensionMethods
    {
        /// <summary>
        /// Gets the control stored at a specified cell location.
        /// </summary>
        /// <param name="control">The grid control to search.</param>
        /// <param name="column">The grid column.</param>
        /// <param name="row">The grid row.</param>
        /// <returns>A reference to a control stored at the specified grid location.</returns>
        /// <remarks>Return the first control it finds that match the row & column.</remarks>
        /// <exception cref="System.ArgumentNullException">control</exception>
        public static Control GetCell(this Grid control, int column, int row)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            foreach (var child in control.Controls)
            {
                var childRow = child.GetProperty(Grid.Row, 0);
                var childColumn = child.GetProperty(Grid.Column, 0);
                if (row == childRow && column == childColumn)
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Stores a control at a specified cell location.
        /// </summary>
        /// <param name="control">The grid control reference.</param>
        /// <param name="column">The grid column.</param>
        /// <param name="row">The grid row.</param>
        /// <param name="value">The control to store at the specified grid cell location.</param>
        public static void SetCell(this Grid control, int column, int row, Control value)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            value.Properties[Grid.Row] = row;
            value.Properties[Grid.Column] = column;
            control.Controls.Add(value);
        }
    }
}