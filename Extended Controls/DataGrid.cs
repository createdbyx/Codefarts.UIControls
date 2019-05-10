namespace Codefarts.UIControls.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class DataGrid : Grid
    {
        private BindingManager bindingManager;

        /// <summary>
        /// Holds the value for the <see cref="ColumnWidth"/> property.
        /// </summary>
        private float columnWidth = float.NaN;

        /// <summary>
        /// Holds the value for the <see cref="RowHeight"/> property.
        /// </summary>
        private float rowHeight = float.NaN;

        /// <summary>
        /// Holds the value for the <see cref="AutoGenerateColumns"/> property.
        /// </summary>
        private bool autoGenerateColumns;

        /// <summary>
        /// Holds the value for the <see cref="CanUserResizeColumns"/> property.
        /// </summary>
        private bool canUserResizeColumns;

        /// <summary>
        /// Holds the value for the <see cref="CanUserResizeRows"/> property.
        /// </summary>
        private bool canUserResizeRows;

        /// <summary>
        /// Holds the value for the <see cref="CanUserReorderColumns"/> property.
        /// </summary>
        private bool canUserReorderColumns;

        /// <summary>
        /// Holds the value for the <see cref="CanUserSortColumns"/> property.
        /// </summary>
        private bool canUserSortColumns;

        /// <summary>
        /// Gets or sets a value that indicates whether the columns are created automatically.
        /// </summary>
        public bool AutoGenerateColumns
        {
            get
            {
                return this.autoGenerateColumns;
            }

            set
            {
                var changed = this.autoGenerateColumns != value;
                this.autoGenerateColumns = value;
                if (changed)
                {
                    this.OnPropertyChanged("AutoGenerateColumns");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user can adjust the width of columns by using an input device such as a mouse.
        /// </summary>
        /// <returns>
        /// true if the user can resize the column; otherwise, false. The registered default is false.
        /// </returns>
        /// <remarks>
        /// <para>This property does not affect whether column widths can be changed programmatically, such as by changing a column Width property.</para>
        /// <para>You can set this resize behavior for individual columns by setting the ColumnDefinition.CanUserResize property.
        /// If the ColumnDefinition.CanUserResize property and the DataGrid.CanUserResizeColumns property are both set, a
        /// value of false takes precedence over a value of true.</para>
        /// </remarks>
        public virtual bool CanUserResizeColumns
        {
            get
            {
                return this.canUserResizeColumns;
            }

            set
            {
                var changed = this.canUserResizeColumns != value;
                this.canUserResizeColumns = value;
                if (changed)
                {
                    this.OnPropertyChanged("CanUserResizeColumns");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user can adjust the height of rows by using an input device such as a mouse.
        /// </summary>
        /// <returns>
        /// true if the user can resize the row; otherwise, false. The registered default is false.
        /// </returns>
        /// <remarks>
        /// <para>This property does not affect whether row heights can be changed programmatically, such as by changing a <see cref="RowDefinition.Height"/> property.</para>
        /// <para>You can set this resize behavior for individual columns by setting the <see cref="RowDefinition.CanUserResize"/> property.
        /// If the ColumnDefinition.CanUserResize property and the DataGrid.CanUserResizeRows property are both set, a
        /// value of false takes precedence over a value of true.</para>
        /// </remarks>
        public virtual bool CanUserResizeRows
        {
            get
            {
                return this.canUserResizeColumns;
            }

            set
            {
                var changed = this.canUserResizeRows != value;
                this.canUserResizeRows = value;
                if (changed)
                {
                    this.OnPropertyChanged("CanUserResizeRows");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user can sort columns by clicking the column header.
        /// </summary>
        /// <returns>
        /// true if the user can sort the columns; otherwise, false. The registered default is false.
        /// </returns>
        /// <remarks>
        /// You can set this sorting behavior for individual columns by setting the DataGridColumn.CanUserSort property.
        /// If the DataGridColumn.CanUserSort property and the DataGrid.CanUserSortColumns property are both set, a value
        /// of false takes precedence over a value of true.
        /// </remarks>
        public virtual bool CanUserSortColumns
        {
            get
            {
                return this.canUserSortColumns;
            }

            set
            {
                var changed = this.canUserSortColumns != value;
                this.canUserSortColumns = value;
                if (changed)
                {
                    this.OnPropertyChanged("CanUserSortColumns");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the user can change the column display order by dragging column headers with a input device such as a mouse.
        /// </summary>
        /// <returns>
        /// true if the user can reorder columns; otherwise, false. The registered default is false.
        /// </returns>
        /// <remarks>
        /// <para>You can set this reorder behavior for individual columns by setting the <see cref="ColumnDefinition.CanUserReorder"/> property.
        /// If the ColumnDefinition.CanUserReorder property and the DataGrid.CanUserReorderColumns property are both set, a value of
        /// false will take precedence over a value of true.</para>
        /// </remarks>
        public virtual bool CanUserReorderColumns
        {
            get
            {
                return this.canUserReorderColumns;
            }

            set
            {
                var changed = this.canUserReorderColumns != value;
                this.canUserReorderColumns = value;
                if (changed)
                {
                    this.OnPropertyChanged("CanUserReorderColumns");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataGrid"/> class.
        /// </summary>
        public DataGrid()
        {
            this.bindingManager = new BindingManager();
            this.PropertyChanged += this.DataGridPropertyChanged;
        }

        private void DataGridPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.bindingManager.UnbindAll();
            switch (e.PropertyName)
            {
                case "DataContext":
                    this.BuildGrid(this.DataContext as IEnumerable);
                    break;
            }
        }

        private void BuildGrid(IEnumerable context)
        {
            if (context == null)
            {
                this.Columns = 1;
                this.Rows = 1;
                this.ColumnDefinitions[0].Name = null;
                return;
            }

            // get public properties
            var enumerator = context.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return;
            }

            var firstItem = enumerator.Current;
            if (firstItem == null)
            {
                return;
            }

            var type = firstItem.GetType();
            var typeProperties = type.GetProperties().Where(x => x.GetGetMethod().IsPublic);
            var members = typeProperties.Where(this.ValidatePropertyType).ToArray();

            // setup columns
            this.Columns = members.Length;
            var memberDictionary = new Dictionary<string, int>();
            for (var i = 0; i < members.Length; i++)
            {
                var member = members[i];
                var column = this.ColumnDefinitions[i];
                column.Name = member.Name;
                this.SetCell(i, 0, new TextBlock(member.Name));
                memberDictionary[column.Name] = i;
            }

            // populate rows
            foreach (var item in context)
            {
                this.Rows++;
                foreach (var pair in memberDictionary)
                {
                    var prop = members[memberDictionary[pair.Key]]; // item.GetType().GetProperties().Where(x => x.GetGetMethod().IsPublic).FirstOrDefault(x => x.Name == pair.Key);

                    if (prop.GetSetMethod().IsPublic && prop.CanWrite)
                    {
                        this.SetCell(pair.Value, this.Rows - 1, this.CreateDataBoundControl(prop, item));
                        continue;
                    }

                    var propertyValue = prop.GetGetMethod().Invoke(item, null).ToString();
                    this.SetCell(pair.Value, this.Rows - 1, new TextBlock(propertyValue));
                }
            }
        }

        private Control CreateDataBoundControl(PropertyInfo info, object item)
        {
            if (info.PropertyType == typeof(bool))
            {
                var checkBox = new CheckBox()
                {

                };
                var boolValue = (bool)info.GetGetMethod().Invoke(item, null);
                checkBox.IsChecked = boolValue;
                return checkBox;
            }

            if (info.PropertyType == typeof(string))
            {
                var textBox = new TextBox()
                {
                    AcceptsReturn = false,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden
                };
                var textValue = (string)info.GetGetMethod().Invoke(item, null);
                textBox.Text = textValue;
                return textBox;
            }

            var propertyValue = info.GetGetMethod().Invoke(item, null).ToString();
            return new TextBlock(propertyValue);
        }

#if WINDOWS_UWP
        private bool ValidatePropertyType(PropertyInfo x)
        {
            return x.GetMethod.IsPublic && x.CanRead && (x.PropertyType.GetTypeInfo().IsValueType || x.PropertyType == typeof(string));
        }
#else
        private bool ValidatePropertyType(PropertyInfo x)
        {
            return x.PropertyType.IsPublic && x.CanRead && (x.PropertyType.IsValueType || x.PropertyType == typeof(string));
        }
#endif

        #region Overrides of Control

        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        /// <remarks>When assigning a data context property all item in the <see cref="IEnumerable"/> are assumed to be of the same type when generating rows.</remarks>
        public override object DataContext
        {
            get
            {
                return base.DataContext;
            }

            set
            {
                if (!(value is IEnumerable))
                {
                    throw new InvalidCastException("Expected IEnumerable type.");
                }

                base.DataContext = value;
            }
        }

        /// <summary>
        /// Gets or sets the suggested width for all columns.
        /// </summary>
        public virtual float ColumnWidth
        {
            get
            {
                return this.columnWidth;
            }

            set
            {
                var changed = Math.Abs(this.columnWidth - value) > float.Epsilon;
                this.columnWidth = Math.Abs(value);
                if (changed)
                {
                    this.OnPropertyChanged("ColumnWidth");
                }
            }
        }

        /// <summary>
        /// Gets or sets the suggested height for all rows.
        /// </summary>
        public virtual float RowHeight
        {
            get
            {
                return this.rowHeight;
            }

            set
            {
                var changed = Math.Abs(this.rowHeight - value) > float.Epsilon;
                this.rowHeight = Math.Abs(value);
                if (changed)
                {
                    this.OnPropertyChanged("RowHeight");
                }
            }
        }

        #endregion
    }
}
