/*
<copyright>
  Copyright(c) 2012 Codefarts
 All rights reserved.
 contact@codefarts.com
 http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    using System;                         

    using Codefarts.UIControls.Models;

    /// <summary>
    /// Provides a grid control that arranges child controls in a grid based layout.
    /// </summary>
    public class Grid : Control
    {
        /// <summary>
        /// The rows varible used by the <see cref="Rows"/> property.
        /// </summary>
        protected int rows = 1;

        /// <summary>
        /// The columns varible used by the <see cref="Columns"/> property.
        /// </summary>
        protected int columns = 1;

        private bool handleColumnChangeEvent = true;
        private bool handleRowChangeEvent = true;

        /// <summary>
        /// The row definitions varible used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        protected RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions varible used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        protected ColumnDefinitionCollection columnDefinitions;


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

                    this.UpdateCellArray();
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

                    this.UpdateCellArray();
                    this.OnPropertyChanged("Columns");
                }
            }
        }

        private void UpdateCellArray()
        {
            this.cells = this.ResizeArray(this.cells, this.columns, this.rows);
        }

        private object[,] cells;

        public object GetCell(int column, int row)
        {
            return this.cells[column, row];
        }

        public void SetCell(int column, int row, object value)
        {
            this.cells[column, row] = value;
        }

        protected T[,] ResizeArray<T>(T[,] original, int x, int y)
        {
            T[,] newArray = new T[x, y];
            var minX = Math.Min(original.GetLength(0), newArray.GetLength(0));
            var minY = Math.Min(original.GetLength(1), newArray.GetLength(1));

            for (var i = 0; i < minY; ++i)
            {
                Array.Copy(original, i * original.GetLength(0), newArray, i * newArray.GetLength(0), minX);
            }

            return newArray;
        }

        /// <returns>
        /// The default <see cref="Size" /> of the control.
        /// </returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        #region Overrides of Control

        /// <summary>
        /// Gets the control collection containing the child controls.
        /// </summary>
        public override ControlsCollection Controls
        {
            get
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="rows">The number of grid rows.</param>
        /// <param name="columns">The number of grid columns.</param>
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
            this.cells = new object[this.columns, this.rows];
            this.Rows = rows;
            this.Columns = columns;
        }

        /// <summary>
        /// Columns the definitions collection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        private void ColumnDefinitionsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs" /> instance containing the event data.</param>
        private void RowDefinitionsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!this.handleRowChangeEvent || this.rows == this.rowDefinitions.Count)
            {
                return;
            }

            this.rows = this.rowDefinitions.Count;
            this.cells = this.ResizeArray(this.cells, this.columns, this.rows);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
            : this(1, 1)
        {
            this.canFocus= false;
            this.isTabStop = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        /// <param name="rows">The number of grid rows.</param>
        /// <param name="columns">The number of grid columns.</param>
        public Grid(string name, int columns, int rows) : this(columns, rows)
        {
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public Grid(string name) : this()
        {
            this.name = name;
        }

        #region Overrides of Control

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