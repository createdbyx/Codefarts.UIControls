namespace Codefarts.UIControls
{
    using System.ComponentModel;

    using Codefarts.UIControls.Interfaces;
    using Codefarts.UIControls.Models;

    /// <summary>
    /// Defines the functionality required to support a shared-size group that is used by the <see cref="ColumnDefinitionCollection" /> and
    /// <see cref="RowDefinitionCollection" /> classes. This is an abstract class. </summary>
    public abstract class DefinitionBase : INotifyPropertyChanged, IMarkup
    {
        /// <summary>
        /// The is tool tip value for the related property.
        /// </summary>
        protected string toolTip;

        /// <summary>
        /// The is tag value for the related property.
        /// </summary>
        protected object tag;

        /// <summary>
        /// The is name value for the related property.
        /// </summary>
        protected string name;

        /// <summary>
        /// The is enabled value for the related property.
        /// </summary>
        protected bool isEnabled = true;

        /// <summary>
        /// The is visible value for the related property.
        /// </summary>
        protected bool isVisible = true;


        /// <summary>
        /// Gets or sets a value that indicates whether this element is visible in the user interface (UI).
        /// </summary>
        /// <returns>
        /// true if the element is visible; otherwise, false. The default value is true.
        /// </returns>
        public virtual bool IsVisible
        {
            get
            {
                return this.isVisible;
            }

            set
            {
                var changed = this.isVisible == value;
                this.isVisible = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this element is enabled in the user interface (UI).
        /// </summary>
        /// <returns>
        /// true if the element is enabled; otherwise, false. The default value is true.
        /// </returns>
        public virtual bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                var changed = this.isEnabled == value;
                this.isEnabled = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the identifying name of the element. The name provides an instance reference so that programmatic code-behind, such as
        /// event handler code, can refer to an element once it is constructed during parsing of XAML.
        /// </summary>
        /// <returns>
        /// The name of the element.
        /// </returns>
        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                var changed = this.name == value;
                this.name = value;
                if (changed)
                {
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// Gets or sets an arbitrary object value that can be used to store custom information about this element.
        /// </summary>
        public virtual object Tag
        {
            get
            {
                return this.tag;
            }

            set
            {
                var changed = this.tag == value;
                this.tag = value;
                if (changed)
                {
                    this.OnPropertyChanged("Tag");
                }
            }
        }

        /// <summary>
        /// Gets or sets the tool-tip object that is displayed for this element in the user interface (UI).
        /// </summary>
        /// <returns>
        /// The tooltip object. See Remarks below for details on why this parameter is not strongly typed.
        /// </returns>
        public virtual string ToolTip
        {
            get
            {
                return this.toolTip;
            }

            set
            {
                var changed = this.toolTip == value;
                this.toolTip = value;
                if (changed)
                {
                    this.OnPropertyChanged("ToolTip");
                }
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Call to raise the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Builds a <see cref="Markup" /> object that represent the state of the implementor.
        /// </summary>
        /// <returns>
        /// A <see cref="Markup" /> object containing the relavent information.
        /// </returns>
        /// <remarks>
        ///   <p>The returned <see cref="Markup" /> object contains the relavnet data stored by the implementor.</p>
        /// </remarks>
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup["ToolTip"] = this.ToolTip;
            markup["Tag"] = this.Tag;
            markup["Name"] = this.Name;
            markup["IsEnabled"] = this.IsEnabled;
            return markup;
        }
    }
}