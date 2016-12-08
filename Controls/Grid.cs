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
        /// The rows variable used by the <see cref="Rows"/> property.
        /// </summary>
        protected int rows = 1;

        /// <summary>
        /// The columns varible used by the <see cref="Columns"/> property.
        /// </summary>
        protected int columns = 1;

        /// <summary>
        /// The row definitions variable used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        protected RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions variable used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        protected ColumnDefinitionCollection columnDefinitions;


        /// <summary>
        /// The handle column change event.
        /// </summary>
        private bool handleColumnChangeEvent = true;

        /// <summary>
        /// The handle row change event.
        /// </summary>
        private bool handleRowChangeEvent = true;

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
            this.cells = new List<Control>[this.columns * this.rows];
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
        public Grid(string name, int columns, int rows) : this(columns, rows)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">
        /// The name of the control.
        /// </param>
        public Grid(string name) : this()
        {
            this.name = name;
        }

        /// <summary>
        /// Gets the column definitions.
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
        /// Gets the row definitions.
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
                return this.rowDefinitions.Count;
            }

            set
            {
                value = value < 1 ? 1 : value;
                var changed = this.rows != value;
                var oldRows = this.rows;
                this.rows = value;
                if (changed)
                {
                    this.handleRowChangeEvent = false;
                    while (value > this.rowDefinitions.Count)
                    {
                        this.rowDefinitions.Add(new RowDefinition());
                    }

                    while (value < this.rowDefinitions.Count)
                    {
                        this.rowDefinitions.RemoveAt(this.rowDefinitions.Count - 1);
                    }

                    this.handleRowChangeEvent = true;

                    this.UpdateCellArray(this.columns, oldRows);
                    this.OnPropertyChanged("Rows");
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
                return this.columnDefinitions.Count;
            }

            set
            {
                value = value < 1 ? 1 : value;
                var changed = this.columns != value;
                var oldColumns = this.columns;
                this.columns = value;
                if (changed)
                {
                    this.handleColumnChangeEvent = false;
                    while (value > this.columnDefinitions.Count)
                    {
                        this.columnDefinitions.Add(new ColumnDefinition());
                    }

                    while (value < this.columnDefinitions.Count)
                    {
                        this.columnDefinitions.RemoveAt(this.columnDefinitions.Count - 1);
                    }

                    this.handleColumnChangeEvent = true;

                    this.UpdateCellArray(oldColumns, this.rows);
                    this.OnPropertyChanged("Columns");
                }
            }
        }

        /// <summary>
        /// The update cell array.
        /// </summary>
        /// <param name="oldColumns">
        /// The old columns.
        /// </param>
        /// <param name="oldRows">
        /// The old rows.
        /// </param>
        private void UpdateCellArray(int oldColumns, int oldRows)
        {
            this.cells = this.ResizeArray(this.cells, oldColumns, oldRows);
        }

        /// <summary>
        /// The cells.
        /// </summary>
        private List<Control>[] cells;

        /// <summary>
        /// The resize array.
        /// </summary>
        /// <param name="original">
        /// The original.
        /// </param>
        /// <param name="oldColumns">
        /// The old columns.
        /// </param>
        /// <param name="oldRows">
        /// The old rows.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T[]"/>.
        /// </returns>
        protected T[] ResizeArray<T>(T[] original, int oldColumns, int oldRows)
        {
            var newArray = new T[this.columns * this.rows];

            var columnCount = Math.Min(this.columns, oldColumns);
            var rowCount = Math.Min(this.rows, oldRows);

            for (var i = 0; i < rowCount; i++)
            {
                Array.Copy(original, i * oldColumns, newArray, i * this.columns, columnCount);
            }

            return newArray;
        }

        /// <summary>
        /// The default size.
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
            if (!this.handleColumnChangeEvent || this.columns == this.columnDefinitions.Count)
            {
                return;
            }

            this.columns = this.columnDefinitions.Count;
            this.cells = this.ResizeArray(this.cells, this.columns, this.rows);
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
            if (!this.handleRowChangeEvent || this.rows == this.rowDefinitions.Count)
            {
                return;
            }

            this.rows = this.rowDefinitions.Count;
            this.cells = this.ResizeArray(this.cells, this.columns, this.rows);
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
            var cellIndex = ((int)pos.Y * this.columns) + (int)pos.X;
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
            var cellIndex = ((int)pos.Y * this.columns) + (int)pos.X;
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
            markup.SetProperty("Rows", this.Rows != 1, this.Rows);
            markup.SetProperty("Columns", this.Columns != 1, this.Columns);
            markup.SetProperty("RowDefinitions", this.RowDefinitions.Count != 0, this.RowDefinitions.ToMarkup());
            markup.SetProperty("ColumnDefinitions", this.ColumnDefinitions.Count != 0, this.ColumnDefinitions.ToMarkup());
            return markup;
        }

        #endregion
    }
}