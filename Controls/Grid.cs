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

    public class Grid : ItemsControl
    {
        //protected class CellData
        //{
        //    public Control[] cellArray;

        //    public int Rows;
        //    public int Columns;
        //}

        //private CellData cellData;
        private int rows = 1;
        private int columns = 1;

        public virtual ColumnDefinitionCollection ColumnDefinitions { get; protected set; }
        public virtual RowDefinitionCollection RowDefinitions { get; protected set; }

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
                //  this.UpdateCells();
            }
        }

        //protected void UpdateCells()
        //{
        //    lock (this.cellData)
        //    {
        //        var newCells = new Control[this.columns * this.rows];
        //        for (var y = 0; y < this.cellData.Rows; y++)
        //        {
        //            for (var x = 0; x < this.cellData.Columns; x++)
        //            {
        //                if (y > this.rows - 1 || x > this.columns - 1)
        //                {
        //                    continue;
        //                }

        //                var sourceIndex = (y * this.cellData.Columns) + x;
        //                var destIndex = (y * this.columns) + x;
        //                newCells[destIndex] = this.cellData.cellArray[sourceIndex];
        //            }
        //        }

        //        this.cellData.cellArray = newCells;
        //        this.cellData.Rows = this.rows;
        //        this.cellData.Columns = this.columns;
        //    }
        //}

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
                //   this.UpdateCells();
            }
        }

        public Grid(int rows, int columns)
        {
            if (columns < 1)
            {
                throw new ArgumentOutOfRangeException("value", "Columns must be greater then 0.");
            }

            if (rows < 1)
            {
                throw new ArgumentOutOfRangeException("value", "Rows must be greater then 0.");
            }

            this.rows = rows;
            this.columns = columns;
            this.ColumnDefinitions = new ColumnDefinitionCollection();
            this.RowDefinitions = new RowDefinitionCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Grid"/> class.
        /// </summary>
        public Grid()
            : this(1, 1)
        {
            //  this.cellData = new CellData() { Rows = 1, Columns = 1, cellArray = new Control[1] };
        }
    }
}