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

    /// <summary>
    /// Provides a grid control that arranges child controls in a grid based layout.
    /// </summary>
    public class Grid :  Control
    {
        /// <summary>
        /// The rows varible used by the <see cref="Rows"/> property.
        /// </summary>
        protected int rows = 1;

        /// <summary>
        /// The columns varible used by the <see cref="Columns"/> property.
        /// </summary>
        protected int columns = 1;

        /// <summary>
        /// The row definitions varible used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        private RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions varible used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        private ColumnDefinitionCollection columnDefinitions;


        /// <summary>
        /// Gets the column definitions.
        /// </summary>
        public virtual ColumnDefinitionCollection ColumnDefinitions
        {
            get
            {
                return this.columnDefinitions;
            }

            protected set
            {
                this.columnDefinitions = value;
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

            protected set
            {
                this.rowDefinitions = value;
            }
        }

        /// <summary>
        /// Gets or sets the grid rows.
        /// </summary>    
        /// <exception cref="System.ArgumentOutOfRangeException">value;Rows must be greater then 0.</exception>
        public virtual int Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                if (this.rows == value)
                {
                    return;
                }

                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "Rows must be greater then 0.");
                }

                this.rows = value;
                this.OnPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// Gets or sets the grid columns.
        /// </summary>     
        /// <exception cref="System.ArgumentOutOfRangeException">value;Columns must be greater then 0.</exception>
        public virtual int Columns
        {
            get
            {
                return this.columns;
            }
            set
            {
                if (this.columns == value)
                {
                    return;
                }

                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value", "Columns must be greater then 0.");
                }

                this.columns = value;
                this.OnPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="rows">The number of grid rows.</param>
        /// <param name="columns">The number of grid columns.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// value;Columns must be greater then 0.
        /// or
        /// value;Rows must be greater then 0.
        /// </exception>
        public Grid(int rows, int columns)
        {
            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException("columns", "Columns must be greater then 0.");
            }

            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException("rows", "Rows must be greater then 0.");
            }

            this.rows = rows;
            this.columns = columns;
            this.columnDefinitions = new ColumnDefinitionCollection();
            this.rowDefinitions = new RowDefinitionCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
            : this(1, 1)
        {
        }
    }
}