// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Codefarts" file="Control.cs">
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Codefarts.UIControls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    ///     The control.
    /// </summary>
    public class Control : INotifyPropertyChanged
    {
        public const string IsMouseOverKey = "IsMouseOver_F87213B4-B311-4B40-B7BB-39B6083309D0";
        public const string PreviousMousePositionKey = "PreviousMousePosition_422715F7-53E9-4177-845A-627F388F4608";

        #region Fields

        /// <summary>
        ///     The height of the control.
        /// </summary>
        private float height;

        /// <summary>
        ///     The horizontal alignment.
        /// </summary>
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;

        /// <summary>
        ///     Holds weather the control is enabled.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        ///     The maximum height.
        /// </summary>
        private float maxHeight = float.MaxValue;

        /// <summary>
        ///     The maximum width.
        /// </summary>
        private float maxWidth = float.MaxValue;

        /// <summary>
        ///     The vertical alignment.
        /// </summary>
        private VerticalAlignment verticalAlignment = VerticalAlignment.Top;

        /// <summary>
        ///     The visibility of the control.
        /// </summary>
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        ///     The width of the control.
        /// </summary>
        private float width;

        /// <summary>
        /// The cached property arguments.
        /// </summary>
        private IDictionary<string, PropertyChangedEventArgs> propertyArgs = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// Holds the value for the <see cref="ClipToBounds"/> property.
        /// </summary>
        private bool clipToBounds;

        /// <summary>
        /// Holds the value for the <see cref="ExtendedProperties"/> property.
        /// </summary>
        private PropertyCollection extendedProperties;

        /// <summary>
        /// The background brush property value.
        /// </summary>
        private Brush background;

        /// <summary>
        /// The data context property value.
        /// </summary>
        private object dataContext;

        /// <summary>
        /// The foreground property value.
        /// </summary>
        private Brush foreground;

        /// <summary>
        /// The horizontal content alignment property value.
        /// </summary>
        private HorizontalAlignment horizontalContentAlignment;

        /// <summary>
        /// The margin bottom property value.
        /// </summary>
        private float marginBottom;

        /// <summary>
        /// The margin left property value.
        /// </summary>
        private float marginLeft;

        /// <summary>
        /// The margin right property value.
        /// </summary>
        private float marginRight;

        /// <summary>
        /// The margin top property value.
        /// </summary>
        private float marginTop;

        /// <summary>
        /// The minimum height property value.
        /// </summary>
        private float minHeight;

        /// <summary>
        /// The minimum width property value.
        /// </summary>
        private float minWidth;

        /// <summary>
        /// The name property value.
        /// </summary>
        private string name;

        /// <summary>
        /// The tag property value.
        /// </summary>
        private object tag;

        /// <summary>
        /// The tool tip property value.
        /// </summary>
        private string toolTip;

        /// <summary>
        /// The vertical content alignment property value.
        /// </summary>
        private VerticalAlignment verticalContentAlignment;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Control" /> class.
        /// </summary>
        public Control()
        {
            this.clipToBounds = true;
            this.extendedProperties = new PropertyCollection();
        }

        #endregion

        #region Public Events

        /// <summary>
        ///     Occurs when the control has focus and a key is pressed.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        ///     Occurs when the control has focus and a key is released.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        ///     Occurs when the mouse pointer enters the bounds of this element.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;

        /// <summary>
        ///     Occurs when the mouse pointer leaves the bounds of this element.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;

        /// <summary>
        ///     Occurs when the mouse pointer moves while over this element. 
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets a brush that describes the background of a control.
        /// </summary>
        /// <returns>
        ///     The brush that is used to fill the background of the control.
        /// </returns>
        public virtual Brush Background
        {
            get
            {
                return this.background;
            }

            set
            {
                var changed = this.background != value;
                this.background = value;
                if (changed)
                {
                    this.OnPropertyChanged("Background");
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether child controls are clipped to the bounds of this control.
        /// </summary>
        public virtual bool ClipToBounds
        {
            get
            {
                return this.clipToBounds;
            }

            set
            {
                var changed = this.clipToBounds != value;
                this.clipToBounds = value;
                if (changed)
                {
                    this.OnPropertyChanged("ClipToBounds");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the data context.
        /// </summary>
        public virtual object DataContext
        {
            get
            {
                return this.dataContext;
            }

            set
            {
                var changed = this.dataContext != value;
                this.dataContext = value;
                if (changed)
                {
                    this.OnPropertyChanged("DataContext");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the extended properties collection.
        /// </summary>
        public virtual PropertyCollection ExtendedProperties
        {
            get
            {
                return this.extendedProperties;
            }

            set
            {
                var changed = this.extendedProperties != value;
                this.extendedProperties = value;
                if (changed)
                {
                    this.OnPropertyChanged("ExtendedProperties");
                }
            }
        }

        /// <summary>
        ///     Gets or sets a brush that describes the foreground color.
        /// </summary>
        /// <returns>
        ///     The brush that paints the foreground of the control.
        /// </returns>
        public virtual Brush Foreground
        {
            get
            {
                return this.foreground;
            }

            set
            {
                var changed = this.foreground != value;
                this.foreground = value;
                if (changed)
                {
                    this.OnPropertyChanged("Foreground");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the height of the control.
        /// </summary>
        public virtual float Height
        {
            get
            {
                return this.height;
            }

            set
            {
                if (value < this.MinHeight)
                {
                    this.height = this.MinHeight;
                    this.OnPropertyChanged("Height");
                }
                else if (value > this.MaxHeight)
                {
                    this.height = this.MaxHeight;
                    this.OnPropertyChanged("Height");
                }
                else
                {
                    var changed = value != this.height;
                    this.height = value;
                    if (changed)
                    {
                        this.OnPropertyChanged("Height");
                    }
                }
            }
        }

        /// <summary>
        ///     Gets or sets the horizontal alignment characteristics applied to this element when it is composed within a parent
        ///     control.
        /// </summary>
        public virtual HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this.horizontalAlignment;
            }

            set
            {
                var changed = this.horizontalAlignment != value;
                this.horizontalAlignment = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontalAlignment");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the horizontal alignment of the control's content.
        /// </summary>
        public virtual HorizontalAlignment HorizontalContentAlignment
        {
            get
            {
                return this.horizontalContentAlignment;
            }

            set
            {
                var changed = this.horizontalContentAlignment != value;
                this.horizontalContentAlignment = value;
                if (changed)
                {
                    this.OnPropertyChanged("HorizontalContentAlignment");
                }
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        public virtual bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }

            set
            {
                var changed = this.isEnabled != value;
                this.isEnabled = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsEnabled");
                }
            }
        }

        /// <summary>Gets a value indicating whether the mouse pointer is located over this element (including child elements in the visual tree).  This is a dependency property.</summary>
        /// <returns>true if mouse pointer is over the element or its child elements; otherwise, false. The default is false.</returns>
        public bool IsMouseOver
        {
            get
            {
                var props = this.ExtendedProperties;
                if (props != null && props.ContainsName(IsMouseOverKey))
                {
                    var mouseOver = (bool)props[IsMouseOverKey];
                    return mouseOver;
                }

                return false;
            }

            private set { }
        }

        /// <summary>
        ///     Gets or sets the length of the margins bottom side.
        /// </summary>
        public virtual float MarginBottom
        {
            get
            {
                return this.marginBottom;
            }

            set
            {
                var changed = this.marginBottom != value;
                this.marginBottom = value;
                if (changed)
                {
                    this.OnPropertyChanged("MarginBottom");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the length of the margins left side.
        /// </summary>
        public virtual float MarginLeft
        {
            get
            {
                return this.marginLeft;
            }

            set
            {
                var changed = this.marginLeft != value;
                this.marginLeft = value;
                if (changed)
                {
                    this.OnPropertyChanged("MarginLeft");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the length of the margins right side.
        /// </summary>
        public virtual float MarginRight
        {
            get
            {
                return this.marginRight;
            }

            set
            {
                var changed = this.marginRight != value;
                this.marginRight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MarginRight");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the length of the margins top side.
        /// </summary>
        public virtual float MarginTop
        {
            get
            {
                return this.marginTop;
            }

            set
            {
                var changed = this.marginTop != value;
                this.marginTop = value;
                if (changed)
                {
                    this.OnPropertyChanged("MarginTop");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the max height of the control.
        /// </summary>
        public virtual float MaxHeight
        {
            get
            {
                return this.maxHeight;
            }

            set
            {
                var changed = this.maxHeight != value;
                this.maxHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxHeight");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the max width of the control.
        /// </summary>
        public virtual float MaxWidth
        {
            get
            {
                return this.maxWidth;
            }

            set
            {
                var changed = this.maxWidth != value;
                this.maxWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaxWidth");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the min height of the control.
        /// </summary>
        public virtual float MinHeight
        {
            get
            {
                return this.minHeight;
            }

            set
            {
                var changed = this.minHeight != value;
                this.minHeight = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinHeight");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the min width of the control.
        /// </summary>
        public virtual float MinWidth
        {
            get
            {
                return this.minWidth;
            }

            set
            {
                var changed = this.minWidth != value;
                this.minWidth = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinWidth");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the name of the control.
        /// </summary>
        public virtual string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                var changed = this.name != value;
                this.name = value;
                if (changed)
                {
                    this.OnPropertyChanged("Name");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        public virtual object Tag
        {
            get
            {
                return this.tag;
            }

            set
            {
                var changed = this.tag != value;
                this.tag = value;
                if (changed)
                {
                    this.OnPropertyChanged("Tag");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the tool tip for the control.
        /// </summary>
        public virtual string ToolTip
        {
            get
            {
                return this.toolTip;
            }

            set
            {
                var changed = this.toolTip != value;
                this.toolTip = value;
                if (changed)
                {
                    this.OnPropertyChanged("ToolTip");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the vertical alignment characteristics applied to this element when it is composed within a parent
        ///     control.
        /// </summary>
        public virtual VerticalAlignment VerticalAlignment
        {
            get
            {
                return this.verticalAlignment;
            }

            set
            {
                var changed = this.verticalAlignment != value;
                this.verticalAlignment = value;
                if (changed)
                {
                    this.OnPropertyChanged("VerticalAlignment");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the vertical alignment of the control's content.
        /// </summary>
        public virtual VerticalAlignment VerticalContentAlignment
        {
            get
            {
                return this.verticalContentAlignment;
            }

            set
            {
                var changed = this.verticalContentAlignment != value;
                this.verticalContentAlignment = value;
                if (changed)
                {
                    this.OnPropertyChanged("VerticalContentAlignment");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the controls visibility.
        /// </summary>
        public virtual Visibility Visibility
        {
            get
            {
                return this.visibility;
            }

            set
            {
                var changed = this.visibility != value;
                this.visibility = value;
                if (changed)
                {
                    this.OnPropertyChanged("Visibility");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the width of the control.
        /// </summary>
        public virtual float Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value < this.MinWidth)
                {
                    this.width = this.MinWidth;
                    this.OnPropertyChanged("Width");
                }
                else if (value > this.MaxWidth)
                {
                    this.width = this.MaxWidth;
                    this.OnPropertyChanged("Width");
                }
                else
                {
                    var changed = value != this.width;
                    this.width = value;
                    if (changed)
                    {
                        this.OnPropertyChanged("Width");
                    }
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Raises the <see cref="E:KeyDown"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="KeyEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnKeyDown(KeyEventArgs e)
        {
            var handler = this.KeyDown;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:KeyUp"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="KeyEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnKeyUp(KeyEventArgs e)
        {
            var handler = this.KeyUp;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises the <see cref="E:MouseEnter"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MouseEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnMouseEnter(MouseEventArgs e)
        {
            var handler = this.MouseEnter;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:MouseLeave"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MouseEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnMouseLeave(MouseEventArgs e)
        {
            var handler = this.MouseLeave;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// Raises the <see cref="E:MouseMove"/> event.
        /// </summary>
        /// <param name="e">
        /// The <see cref="MouseEventArgs"/> instance containing the event data.
        /// </param>
        public virtual void OnMouseMove(MouseEventArgs e)
        {
            var handler = this.MouseMove;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        /// <summary>
        /// Provides a <see cref="INotifyPropertyChanged.PropertyChanged"/> implementation.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Used to invoke the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                if (!this.propertyArgs.ContainsKey(propertyName))
                {
                    this.propertyArgs[propertyName] = new PropertyChangedEventArgs(propertyName);
                }

                handler(this, this.propertyArgs[propertyName]);
            }
        }
    }
}