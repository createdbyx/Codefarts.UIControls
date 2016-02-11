namespace Codefarts.UIControls
{
    /// <summary>
    /// Paints an area with an image. 
    /// </summary>
    public class GridImageBrush : ImageBrush
    {
        /// <summary>
        /// The row definitions varible used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        protected RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions varible used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        protected ColumnDefinitionCollection columnDefinitions;

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
                    this.OnPropertyChanged("Rows");
                }
            }
        }

        /// <summary>
        /// Gets or sets the grid columns.
        /// </summary>     
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
                    this.OnPropertyChanged("Columns");
                }
            }
        }

        /// <summary>Initializes a new instance of the <see cref="GridImageBrush" /> class that paints an area with the specified image. </summary>
        /// <param name="source">The image to display.</param>
        public GridImageBrush(ImageSource source) : base(source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridImageBrush" /> class with no content.
        /// </summary>
        public GridImageBrush()
        {
        }
    }
}