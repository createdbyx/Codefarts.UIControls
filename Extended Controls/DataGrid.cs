namespace Codefarts.UIControls.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class DataGrid<T> : Grid where T : class
    {
        private BindingManager bindingManager;

        public bool AutoGenerateColumns { get; set; }

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
                    this.BuildGrid(this.DataContext as IEnumerable<T>);
                    break;
            }
        }

        private void BuildGrid(IEnumerable<T> context)
        {
            if (context == null)
            {
                this.Columns = 1;
                this.Rows = 1;
                this.ColumnDefinitions[0].Name = null;
                return;
            }

            // get public properties                                 
            var type = typeof(T);
            var properties = type.GetProperties();
            var members = properties.Where(this.ValidatePropertyType).ToArray();

            // setup columns
            this.Columns = members.Length;
            var dic = new Dictionary<string, int>();
            for (var i = 0; i < members.Length; i++)
            {
                var member = members[i];
                var column = this.ColumnDefinitions[i];
                column.Name = member.Name;
                this.SetCell(i, 0, new TextBlock(member.Name));
                dic[column.Name] = i;
            }

            // populate rows
            foreach (var item in context)
            {
                this.Rows++;
                foreach (var pair in dic)
                {
                    var prop = item.GetType().GetProperty(pair.Key);

                    if (prop.CanWrite)
                    {
                        this.SetCell(pair.Value, this.Rows - 1, new TextBlock("SetNI"));
                    }
                    else
                    {
                        var propertyValue = prop.GetGetMethod().Invoke(item, null).ToString();
                        this.SetCell(pair.Value, this.Rows - 1, new TextBlock(propertyValue));
                    }
                }
            }
        }

        private bool ValidatePropertyType(PropertyInfo x)
        {
            return x.PropertyType.IsPublic && x.CanRead && (x.PropertyType.IsValueType || x.PropertyType == typeof(string));
        }

        #region Overrides of Control

        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        public override object DataContext
        {
            get
            {
                return base.DataContext;
            }

            set
            {
                if (!(value is IEnumerable<T>))
                {
                    throw new InvalidCastException("Expected IEnumerable type of " + typeof(T).FullName);
                }

                base.DataContext = value;
            }
        }

        #endregion
    }
}
