namespace Codefarts.UIControls
{
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Paints an area with an image using a grid system to control what parts of the image appear where.
    /// </summary>
    public class GridImageBrush : ImageBrush
    {
        /// <summary>
        /// The row definitions variable used by the <see cref="RowDefinitions"/> property.
        /// </summary>
        private RowDefinitionCollection rowDefinitions;

        /// <summary>
        /// The column definitions variable used by the <see cref="ColumnDefinitions"/> property.
        /// </summary>
        private ColumnDefinitionCollection columnDefinitions;

        /// <summary>Initializes a new instance of the <see cref="GridImageBrush" /> class that paints an area with the specified image. </summary>
        /// <param name="source">The image to display.</param>
        public GridImageBrush(ImageSource source)
            : base(source)
        {
            this.rowDefinitions = new RowDefinitionCollection();
            this.columnDefinitions = new ColumnDefinitionCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GridImageBrush" /> class with no content.
        /// </summary>
        public GridImageBrush()
        {
            this.rowDefinitions = new RowDefinitionCollection();
            this.columnDefinitions = new ColumnDefinitionCollection();
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
                this.Columns = value != null ? this.columnDefinitions.Count : 0;
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
                this.Rows = value != null ? value.Count : 0;
                if (changed)
                {
                    this.OnPropertyChanged("RowDefinitions");
                }
            }
        }

        /// <summary>
        /// Gets or sets the grid rows.
        /// </summary>
        /// <remarks>
        /// <p>Setting a lower value then the current value will remove rows from the end of the <seealso cref="RowDefinitions"/> collection.
        /// Conversely setting a higher value then the current value will add rows to the <seealso cref="RowDefinitions"/> collection.</p>
        /// <p>If <seealso cref="RowDefinitions"/> is null this property will return 0. Setting a row count when <seealso cref="RowDefinitions"/> is null
        /// will instantiate a new <seealso cref="RowDefinitionCollection"/> reference.</p>
        /// </remarks>
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
                value = value < 0 ? 0 : value;
                var definitions = this.rowDefinitions;
                var setRows = false;
                if (definitions == null)
                {
                    definitions = new RowDefinitionCollection();
                    setRows = true;
                }

                var changed = definitions.Count != value;
                if (changed)
                {
                    while (value > definitions.Count)
                    {
                        definitions.Add(new RowDefinition());
                    }

                    while (value < definitions.Count)
                    {
                        definitions.RemoveAt(definitions.Count - 1);
                    }

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
        /// <remarks>
        /// <p>Setting a lower value then the current value will remove columns from the end of the <seealso cref="ColumnDefinitions"/> collection.
        /// Conversely setting a higher value then the current value will add columns to the <seealso cref="ColumnDefinitions"/> collection.</p>
        /// <p>If <seealso cref="ColumnDefinitions"/> is null this property will return 0. Setting a row count when <seealso cref="ColumnDefinitions"/> is null
        /// will instantiate a new <seealso cref="ColumnDefinitionCollection"/> reference.</p>
        /// </remarks>
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
                value = value < 0 ? 0 : value;
                var definitions = this.columnDefinitions;
                var setRows = false;
                if (definitions == null)
                {
                    definitions = new ColumnDefinitionCollection();
                    setRows = true;
                }

                var changed = definitions.Count != value;
                if (changed)
                {
                    while (value > definitions.Count)
                    {
                        definitions.Add(new ColumnDefinition());
                    }

                    while (value < definitions.Count)
                    {
                        definitions.RemoveAt(definitions.Count - 1);
                    }

                    this.OnPropertyChanged("Columns");
                }

                if (setRows)
                {
                    this.ColumnDefinitions = definitions;
                }
            }
        }

        /// <summary>
        /// Converts to markup.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relevant information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relevant data stored by the implementor.</p>
        /// </remarks>
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
    }
}