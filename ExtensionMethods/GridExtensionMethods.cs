namespace Codefarts.UIControls
{
    using System;

    /// <summary>
    /// The grid extension methods.
    /// </summary>
    public static class GridExtensionMethods
    {
        public static Control GetCell(this Grid control, int column, int row)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }

            foreach (var child in control.Controls)
            {
                var childRow = control.GetProperty(Grid.Row, 0);
                var childColumn = control.GetProperty(Grid.Column, 0);
                if (row == childRow && column == childColumn)
                {
                    return child;
                }
            }

            return null;
        }

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