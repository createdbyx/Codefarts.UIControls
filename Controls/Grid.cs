// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Grid.cs">
//   Copyright(c) 2012 Codefarts
//    All rights reserved.
//    contact@codefarts.com
//    http://www.codefarts.com
// </copyright>
// <summary>
//   Provides a grid control that arranges child controls in a grid based layout.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using Models;

    /// <summary>
    /// Provides a grid control that arranges child controls in a grid based layout.
    /// </summary>
    public class Grid : Control
    {
        /// <summary>
        /// The control style key used when accessing a property from the <see cref="Properties"/> property.
        /// </summary>
        public const string Row = "Grid.Row_9353515E-66FB-4D22-BB72-6D360DE101FE";

        /// <summary>
        /// The control style key used when accessing a property from the <see cref="Properties"/> property.
        /// </summary>
        public const string Column = "Grid.Column_B19DA9A1-4149-4C9B-8D62-E88CED091985";

        /// <summary>
        /// The row definitions variable used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        private RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions variable used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        private ColumnDefinitionCollection columnDefinitions;

        /// <summary>
        /// The handle column change event.
        /// </summary>
        private bool handleColumnChangeEvent = true;

        /// <summary>
        /// The handle row change event.
        /// </summary>
        private bool handleRowChangeEvent = true;

        /// <summary>
        /// The cells.
        /// </summary>
        private List<Control>[] cells;

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
            : this(1, 1)
        {
            this.canFocus = false;
            this.isTabStop = false;
            this.Controls.CollectionChanged += this.ControlsCollectionChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="columns">
        /// The number of grid columns.
        /// </param>
        /// <param name="rows">
        /// The number of grid rows.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// value;Columns must be greater then 0.
        /// or
        /// value;Rows must be greater then 0.
        /// </exception>
        public Grid(int columns, int rows)
        {
            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException("columns", "Columns must be greater then 0.");
            }

            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException("rows", "Rows must be greater then 0.");
            }

            this.columnDefinitions = new ColumnDefinitionCollection();
            this.rowDefinitions = new RowDefinitionCollection();

            for (var i = 0; i < columns; i++)
            {
                this.columnDefinitions.Add(new ColumnDefinition());
            }

            for (var i = 0; i < rows; i++)
            {
                this.rowDefinitions.Add(new RowDefinition());
            }

            this.rowDefinitions.CollectionChanged += this.RowDefinitionsCollectionChanged;
            this.columnDefinitions.CollectionChanged += this.ColumnDefinitionsCollectionChanged;
            this.cells = new List<Control>[columns * rows];
            this.Rows = rows;
            this.Columns = columns;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the control.
        /// </param>
        /// <param name="columns">
        /// The number of grid columns.
        /// </param>
        /// <param name="rows">
        /// The number of grid rows.
        /// </param>
        public Grid(string name, int columns, int rows)
            : this(columns, rows)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the control.
        /// </param>
        public Grid(string name)
            : this()
        {
            this.name = name;
        }

        /// <summary>
        /// Gets or sets the column definitions.
        /// </summary>
        public virtual ColumnDefinitionCollection ColumnDefinitions
        {
            get
            {
                return this.columnDefinitions;
            }

            set
            {
                var changed = this.columnDefinitions != value;
                this.columnDefinitions = value;
                this.Columns = this.columnDefinitions.Count;
                if (changed)
                {
                    this.OnPropertyChanged("ColumnDefinitions");
                }
            }
        }

        /// <summary>
        /// Gets or sets the row definitions.
        /// </summary>
        public virtual RowDefinitionCollection RowDefinitions
        {
            get
            {
                return this.rowDefinitions;
            }

            set
            {
                var changed = this.rowDefinitions != value;
                this.rowDefinitions = value;
                this.Columns = this.rowDefinitions.Count;
                if (changed)
                {
                    this.OnPropertyChanged("RowDefinitions");
                }
            }
        }

        /// <summary>
        /// Gets or sets the grid rows.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">value;Rows must be greater then 0.</exception>
        public virtual int Rows
        {
            get
            {
                var definitions = this.rowDefinitions;
                if (definitions != null)
                {
                    return definitions.Count;
                }

                return 0;
            }

            set
            {
                value = value < 1 ? 1 : value;
                var definitions = this.rowDefinitions;
                var setRows = false;
                if (definitions == null)
                {
                    definitions = new RowDefinitionCollection();
                    setRows = true;
                }

                var oldRows = definitions.Count;
                var changed = definitions.Count != value;
                if (changed)
                {
                    this.handleRowChangeEvent = false;
                    while (value > definitions.Count)
                    {
                        definitions.Add(new RowDefinition());
                    }

                    while (value < definitions.Count)
                    {
                        definitions.RemoveAt(definitions.Count - 1);
                    }

                    this.handleRowChangeEvent = true;
                    this.UpdateCellArray(this.Columns, oldRows);
                    this.OnPropertyChanged("Rows");
                }

                if (setRows)
                {
                    this.RowDefinitions = definitions;
                }
            }
        }

        /// <summary>
        /// Gets or sets the grid columns.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">value;Columns must be greater then 0.</exception>
        public virtual int Columns
        {
            get
            {
                var definitions = this.columnDefinitions;
                if (definitions != null)
                {
                    return definitions.Count;
                }

                return 0;
            }

            set
            {
                value = value < 1 ? 1 : value;
                var definitions = this.columnDefinitions;
                var setRows = false;
                if (definitions == null)
                {
                    definitions = new ColumnDefinitionCollection();
                    setRows = true;
                }

                var oldColumns = definitions.Count;
                var changed = definitions.Count != value;
                if (changed)
                {
                    this.handleColumnChangeEvent = false;
                    while (value > definitions.Count)
                    {
                        definitions.Add(new ColumnDefinition());
                    }

                    while (value < definitions.Count)
                    {
                        definitions.RemoveAt(definitions.Count - 1);
                    }

                    this.handleColumnChangeEvent = true;
                    this.UpdateCellArray(oldColumns, this.Rows);
                    this.OnPropertyChanged("Columns");
                }

                if (setRows)
                {
                    this.ColumnDefinitions = definitions;
                }
            }
        }

        /// <summary>
        /// Gets the default size.
        /// </summary>
        /// <returns>
        /// The default <see cref="Size"/> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        /// <summary>
        /// Update the internal cell array that holds the controls.
        /// </summary>
        /// <param name="oldColumns">
        /// The old column count.
        /// </param>
        /// <param name="oldRows">
        /// The old row count.
        /// </param>
        private void UpdateCellArray(int oldColumns, int oldRows)
        {
            this.cells = this.ResizeArray(this.cells, oldColumns, oldRows, this.Columns, this.Rows);
        }

        /// <summary>
        /// Resizes an array and keeps the old values.
        /// </summary>
        /// <param name="original">
        /// The original array.
        /// </param>
        /// <param name="oldColumns">
        /// The old column count.
        /// </param>
        /// <param name="oldRows">
        /// The old row count.
        /// </param>
        /// <param name="columns">
        /// The new column count.
        /// </param>
        /// <param name="rows">
        /// The new row count.
        /// </param>
        /// <returns>
        /// Returns a new resized array containing the old values.
        /// </returns>
        private T[] ResizeArray<T>(T[] original, int oldColumns, int oldRows, int columns, int rows)
        {
            var newArray = new T[columns * rows];

            var columnCount = Math.Min(columns, oldColumns);
            var rowCount = Math.Min(rows, oldRows);

            for (var i = 0; i < rowCount; i++)
            {
                Array.Copy(original, i * oldColumns, newArray, i * columns, columnCount);
            }

            return newArray;
        }

        /// <summary>
        /// Columns the definitions collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ColumnDefinitionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!this.handleColumnChangeEvent || this.Columns == this.columnDefinitions.Count)
            {
                return;
            }

            this.Columns = this.columnDefinitions.Count;
            this.cells = this.ResizeArray(this.cells, this.Columns, this.Rows, this.Columns, this.Rows);
        }

        /// <summary>
        /// Rows the definitions collection changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void RowDefinitionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!this.handleRowChangeEvent || this.Rows == this.rowDefinitions.Count)
            {
                return;
            }

            this.Rows = this.rowDefinitions.Count;
            this.cells = this.ResizeArray(this.cells, this.Columns, this.Rows, this.Columns, this.Rows);
        }

        /// <summary>
        /// handles changes to the <see cref="Control.Controls"/> collection.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.
        /// </param>
        private void ControlsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        var child = newItem as Control;
                        this.DoAddNewChild(child);
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var newItem in e.OldItems)
                    {
                        var child = newItem as Control;
                        this.DoRemoveChild(child);
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
#if !PORTABLE && !UNITY_5
                case NotifyCollectionChangedAction.Move:
#endif
                    foreach (var newItem in e.OldItems)
                    {
                        var child = newItem as Control;
                        this.DoRemoveChild(child);
                    }

                    foreach (var newItem in e.NewItems)
                    {
                        var child = newItem as Control;
                        this.DoAddNewChild(child);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;
            }
        }

        private void DoRemoveChild(Control child)
        {
            if (child == null)
            {
                return;
            }

            var pos = child.GetGridPosition();
            var cellIndex = ((int)pos.Y * this.Columns) + (int)pos.X;
            if (cellIndex < 0 || cellIndex > this.cells.Length - 1)
            {
                return;
            }

            var list = this.cells[cellIndex];
            if (list != null)
            {
                list.Remove(child);
            }
        }

        private void DoAddNewChild(Control child)
        {
            if (child == null)
            {
                return;
            }

            var pos = child.GetGridPosition();
            var cellIndex = ((int)pos.Y * this.Columns) + (int)pos.X;
            if (cellIndex < 0 || cellIndex > this.cells.Length - 1)
            {
                return;
            }

            var list = this.cells[cellIndex];
            if (list == null)
            {
                list = new List<Control>();
                this.cells[cellIndex] = list;
            }

            list.Add(child);
        }

        #region Overrides of Control

        /// <summary>
        /// The to markup.
        /// </summary>
        /// <returns>
        /// The <see cref="Markup"/>.
        /// </returns>
        public override Markup ToMarkup()
        {
            var markup = base.ToMarkup();
            markup.Name = this.GetType().FullName;
            var rows = this.rowDefinitions;
            markup["RowDefinitions"] = rows != null ? rows.ToMarkup() : null;
            var columns = this.columnDefinitions;
            markup["ColumnDefinitions"] = columns != null ? columns.ToMarkup() : null;
            return markup;
        }

        #endregion
    }
}