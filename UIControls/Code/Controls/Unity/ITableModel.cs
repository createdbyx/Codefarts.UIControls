/*
<copyright>
  Codefarts.UnityTesting spawned from SharpUnit
  contact@codefarts.com
  http://www.codefarts.com

  SharpUnit was written by:
  Mark Gants | mark@markgants.com
  May 20, 2010

  This software is provided "as is" for free.
  You may do anything you like with this software as long as you leave this notice in place.

  See original LICENCE file for full license details.
</copyright>
*/

namespace Codefarts.UIControls.Code
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides an interface for working table model data.
    /// </summary>
    /// <typeparam name="T">The type of data model that the table displays.</typeparam>
    public interface ITableModel<T>
    {
        /// <summary>
        /// Gets the number of columns.
        /// </summary>             
        int ColumnCount { get; }

        /// <summary>
        /// Gets an <see cref="IList{T}"/> of model data.
        /// </summary>
        IList<T> Elements { get; }

        /// <summary>
        /// Gets the number of rows.
        /// </summary>             
        int RowCount { get; }

        /// <summary>
        /// Gets a value indicating whether or not headers shown be shown.
        /// </summary>
        bool UseHeaders { get; }

        /// <summary>
        /// Gets whether or not a cell can be edited.
        /// </summary>
        /// <param name="rowIndex">The index of the row to retrieve the data from.</param>
        /// <param name="columnIndex">The index of the column to retrieve the data from.</param>
        /// <returns>true if the cell can be edited; otherwise false.</returns>
        bool CanEdit(int rowIndex, int columnIndex);

        /// <summary>
        /// Gets the name of a column.
        /// </summary>
        /// <param name="columnIndex">The index of the column to retrieve.</param>
        /// <returns>Returns the column name.</returns>
        string GetColumnName(int columnIndex);

        /// <summary>
        /// Gets the column width.
        /// </summary>
        /// <param name="columnIndex">The index of the column to retrieve.</param>
        /// <returns>Returns the width of the column.</returns>
        int GetColumnWidth(int columnIndex);

        /// <summary>
        /// Gets the value for a table cell.
        /// </summary>
        /// <param name="rowIndex">The index of the row to retrieve the data from.</param>
        /// <param name="columnIndex">The index of the column to retrieve the data from.</param>
        /// <returns>Returns the value of the cell.</returns>
        object GetValue(int rowIndex, int columnIndex);
    
        /// <summary>
        /// Sets the value for a table cell.
        /// </summary>
        /// <param name="rowIndex">The index of the row to set the data.</param>
        /// <param name="columnIndex">The index of the column to set the data.</param>
        /// <param name="value">The value to assign to the cell.</param>
        void SetValue(int rowIndex, int columnIndex, object value);
    }
}
