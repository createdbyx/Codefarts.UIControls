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
    /// The base control implementation.
    /// </summary>
    public class Control : INotifyPropertyChanged
    {
        public const string ControlStyle = "ControlStyle_009FE297-D820-45B6-8AAC-DD2FC42FDE0A";
        public const string IsMouseOverKey = "IsMouseOver_F87213B4-B311-4B40-B7BB-39B6083309D0";
        public const string PreviousMousePositionKey = "PreviousMousePosition_422715F7-53E9-4177-845A-627F388F4608";

        #region Fields

        /// <summary>
        /// The horizontal alignment.
        /// </summary>
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Left;

        /// <summary>
        /// Holds weather the control is enabled.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// The vertical alignment.
        /// </summary>
        private VerticalAlignment verticalAlignment = VerticalAlignment.Top;

        /// <summary>
        /// The visibility of the control.
        /// </summary>
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        /// The size of the control.
        /// </summary>
        private Size size;

        /// <summary>
        /// The location of the control.
        /// </summary>
        private Point location;

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
        /// The font property value.
        /// </summary>
        private Font font;

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

        /// <summary>
        /// The value for the <see cref="Parent"/> property.
        /// </summary>
        private Control parent;

        /// <summary>
        /// The minimum size for the control.
        /// </summary>
        private Size minSize;

        /// <summary>
        /// The maximum size for the control.
        /// </summary>
        private Size maxSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control" /> class.
        /// </summary>
        public Control()
        {
            this.Controls = new ControlsCollection(this);
            this.clipToBounds = true;
            this.extendedProperties = new PropertyCollection();
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when the control has focus and a key is pressed.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyDown;

        /// <summary>
        /// Occurs when the control has focus and a key is released.
        /// </summary>
        public event EventHandler<KeyEventArgs> KeyUp;

        /// <summary>
        /// Occurs when the mouse pointer enters the bounds of this element.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseEnter;

        /// <summary>
        /// Occurs when the mouse pointer leaves the bounds of this element.
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseLeave;

        /// <summary>
        /// Occurs when the mouse pointer moves while over this element. 
        /// </summary>
        public event EventHandler<MouseEventArgs> MouseMove;

        #endregion

        #region Public Properties

        /// <summary>
        /// This property is not relevant for this class.
        /// </summary>
        /// <returns>
        /// true if enabled; otherwise, false.
        /// </returns>
        public virtual bool AutoSize { get; set; }

        /// <summary>
        /// Gets the control collection containing the child controls.
        /// </summary>
        public ControlsCollection Controls { get; private set; }

        /// <summary>
        /// Brings the control to the front of the z-order.
        /// </summary>
        public void BringToFront()
        {
            if (this.parent != null)
            {
                this.parent.Controls.SetChildIndex(this, 0);
            }
        }

        /// <summary>
        /// Sends the control to the back of the z-order.
        /// </summary>
        public void SendToBack()
        {
            if (this.parent != null)
            {
                this.parent.Controls.SetChildIndex(this, -1);
            }
        }

        /// <summary>
        /// Gets or sets the parent control.
        /// </summary>
        public Control Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                if (this.parent != value)
                {
                    if (value != null)
                    {
                        value.Controls.Add(this);
                        return;
                    }

                    this.parent.Controls.Remove(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets a brush that describes the background of a control.
        /// </summary>   
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
        ///  Gets or sets the font of the text displayed by the control. 
        /// </summary>   
        public virtual Font Font
        {
            get
            {
                return this.font;
            }

            set
            {
                var changed = this.font != value;
                this.font = value;
                if (changed)
                {
                    this.OnPropertyChanged("Font");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether child controls are clipped to the bounds of this control.
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
        /// Gets or sets the data context.
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
        /// Gets or sets the extended properties collection.
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
        /// Gets or sets a brush that describes the foreground color.
        /// </summary>
        /// <returns>
        /// The brush that paints the foreground of the control.
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
        /// Gets or sets the height of the control.
        /// </summary>
        public virtual float Height
        {
            get
            {
                return this.size.Height;
            }

            set
            {
                if (value < this.minSize.Height && !this.minSize.IsEmpty)
                {
                    this.size.Height = this.minSize.Height;
                    this.OnPropertyChanged("Height");
                }
                else if (value > this.maxSize.Height && !this.maxSize.IsEmpty)
                {
                    this.size.Height = this.maxSize.Height;
                    this.OnPropertyChanged("Height");
                }
                else
                {
                    var changed = Math.Abs(value - this.size.Height) > float.Epsilon;
                    this.size.Height = value;
                    if (changed)
                    {
                        this.OnPropertyChanged("Height");
                        this.OnPropertyChanged("Bottom");
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the horizontal alignment characteristics applied to this element when it is composed within a parent
        /// control.
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
        /// Gets or sets the horizontal alignment of the control's content.
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
        /// Gets or sets a value indicating whether the control is enabled.
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
        }

        /// <summary>Gets or sets the distance, between the bottom edge of the control and the bottom edge of its container's client area.</summary>
        /// <returns>An <see cref="float" /> representing the distance, between the bottom edge of the control and the bottom edge of its container's client area.</returns>
        public virtual float Bottom
        {
            get
            {
                var p = this.parent;
                return p == null ? this.location.Y + this.size.Height : p.Height - (this.location.Y + this.size.Height);
            }

            set
            {
                var p = this.parent;
                this.Height = (p == null ? value - this.location.Y : p.Height - value - this.location.Y);
            }
        }

        /// <summary>Gets or sets the distance, between the left edge of the control and the left edge of its container's client area.</summary>
        /// <returns>An <see cref="float" /> representing the distance, between the left edge of the control and the left edge of its container's client area.</returns>
        public virtual float Left
        {
            get
            {
                return this.location.X;
            }

            set
            {
                var changed = Math.Abs(this.location.X - value) > float.Epsilon;
                this.location.X = value;
                if (changed)
                {
                    this.OnPropertyChanged("Left");
                }
            }
        }

        /// <summary>Gets or sets the distance, between the right edge of the control and the right edge of its container's client area.</summary>
        /// <returns>An <see cref="float" /> representing the distance, between the right edge of the control and the right edge of its container's client area.</returns>
        public virtual float Right
        {
            get
            {
                var p = this.parent;
                return p == null ? this.location.X + this.size.Width : p.Width - (this.location.X + this.size.Width);
            }

            set
            {
                var p = this.parent;
                this.Width = (p == null ? value - this.location.X : p.Width - value - this.location.X);
            }
        }

        /// <summary>Gets or sets the distance, between the top edge of the control and the top edge of its container's client area.</summary>
        /// <returns>An <see cref="float" /> representing the distance, between the bottom edge of the control and the top edge of its container's client area.</returns>
        public virtual float Top
        {
            get
            {
                return this.location.X;
            }

            set
            {
                var changed = Math.Abs(this.location.X - value) > float.Epsilon;
                this.location.X = value;
                if (changed)
                {
                    this.OnPropertyChanged("Top");
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum size of the control.
        /// </summary>
        public virtual Size MaximumSize
        {
            get
            {
                return this.maxSize;
            }

            set
            {
                var changed = this.maxSize != value;
                this.maxSize = value;
                if (changed)
                {
                    this.OnPropertyChanged("MaximumSize");
                }
            }
        }

        /// <summary>
        /// Gets or sets the minimum size of the control.
        /// </summary>
        public virtual Size MinimumSize
        {
            get
            {
                return this.minSize;
            }

            set
            {
                var changed = this.minSize != value;
                this.minSize = value;
                if (changed)
                {
                    this.OnPropertyChanged("MinimumSize");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the control.
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
        /// Gets or sets the tag.
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
        /// Gets or sets the tool tip for the control.
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
        /// Gets or sets the vertical alignment characteristics applied to this element when it is composed within a parent
        /// control.
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
        /// Gets or sets the vertical alignment of the control's content.
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
        /// Gets or sets the controls visibility.
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

        /// <summary>Gets or sets the height and width of the control.</summary>
        /// <returns>The <see cref="Size" /> that represents the height and width of the control.</returns>
        public Size Size
        {
            get
            {
                return this.size;
            }

            set
            {
                var changed = this.size != value;
                this.size = value;
                if (changed)
                {
                    this.OnPropertyChanged("Size");
                }
            }
        }

        /// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
        /// <returns>The <see cref="Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
        public Point Location
        {
            get
            {
                return this.location;
            }
            set
            {
                var changed = this.location != value;
                this.location = value;
                if (changed)
                {
                    this.OnPropertyChanged("Location");
                }
            }
        }

        /// <summary>
        /// Gets or sets the width of the control.
        /// </summary>
        public virtual float Width
        {
            get
            {
                return this.size.Width;
            }

            set
            {
                if (value < this.minSize.Width && !this.minSize.IsEmpty)
                {
                    this.size.Width = this.minSize.Width;
                    this.OnPropertyChanged("Width");
                }
                else if (value > this.maxSize.Width && !this.maxSize.IsEmpty)
                {
                    this.size.Width = this.maxSize.Width;
                    this.OnPropertyChanged("Width");
                }
                else
                {
                    var changed = Math.Abs(value - this.size.Width) > float.Epsilon;
                    this.size.Width = value;
                    if (changed)
                    {
                        this.OnPropertyChanged("Width");
                        this.OnPropertyChanged("Right");
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

        /// <summary>
        /// Assigns the parent to the internal <see cref="parent"/> field.
        /// </summary>
        /// <param name="owner">The control that will be the owner/parent of this control.</param>
        internal virtual void AssignParent(Control owner)
        {
            if (this.parent == owner)
            {
                return;
            }

            this.parent = owner;
            this.OnPropertyChanged("Parent");
        }
    }
}