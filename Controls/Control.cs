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
    using System.Linq;

    using Codefarts.UIControls.Models;

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
        /// The previous size of the control.
        /// </summary>
        private Size previousSize;

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
        /// Holds the value for the <see cref="Properties"/> property.
        /// </summary>
        private IDictionary<string, object> properties;

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
        private HorizontalAlignment horizontalContentAlignment = HorizontalAlignment.Left;

        /// <summary>
        /// The name property value.
        /// </summary>
        protected string name;

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
        private VerticalAlignment verticalContentAlignment = VerticalAlignment.Top;

        /// <summary>
        /// The value for the <see cref="Opacity"/> property.
        /// </summary>
        private float opacity = 1;

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

        /// <summary>
        /// The value for the <see cref="AutoSize"/> property.
        /// </summary>
        private bool autoSize;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control" /> class.
        /// </summary>
        public Control()
        {
            this.Controls = new ControlsCollection(this);
            this.clipToBounds = true;
            this.properties = new Dictionary<string, object>();
            this.PropertyChanged += this.PropertyChangedHandler;
            this.size = this.DefaultSize;
        }

        /// <summary>
        /// Internal properties changed handler for internal use.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.</param>
        private void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Size":
                    this.PerformLayout();
                    break;
            }
        }

        /// <summary>
        /// Forces the control to apply layout logic to all its child controls.
        /// </summary>
        public virtual void PerformLayout()
        {
            var center = new Point(this.Width / 2, this.Height / 2);
            var controls = this.Controls;
            if (controls == null)
            {
                return;
            }

            foreach (var control in controls)
            {
                var pos = control.Location;
                var controlSize = control.Size;
                switch (control.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        break;

                    case HorizontalAlignment.Center:
                        pos.X = center.X - (control.Width / 2);
                        break;

                    case HorizontalAlignment.Right:
                        pos.X += this.Width - this.previousSize.Width;
                        break;

                    case HorizontalAlignment.Stretch:
                        controlSize.Width += this.Width - this.previousSize.Width;
                        break;
                }

                switch (control.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        break;

                    case VerticalAlignment.Center:
                        pos.Y = center.Y - (control.Height / 2);
                        break;

                    case VerticalAlignment.Bottom:
                        pos.Y += this.Height - this.previousSize.Height;
                        break;

                    case VerticalAlignment.Stretch:
                        controlSize.Height += this.Height - this.previousSize.Height;
                        break;
                }

                control.SetBounds(pos.X, pos.Y, controlSize.Width, controlSize.Height);
            }
        }

        /// <summary>Sets the bounds of the control to the specified location and size.</summary>
        /// <param name="x">The new <see cref="Control.Left" /> property value of the control. </param>
        /// <param name="y">The new <see cref="Control.Top" /> property value of the control. </param>
        /// <param name="width">The new <see cref="Control.Width" /> property value of the control. </param>
        /// <param name="height">The new <see cref="Control.Height" /> property value of the control. </param>
        public void SetBounds(float x, float y, float width, float height)
        {
            var newLocation = new Point(x, y);
            var newSize = new Size(width, height);
            this.Location = newLocation;
            this.Size = newSize;
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
        public virtual bool AutoSize
        {
            get
            {
                return this.autoSize;
            }
            set
            {
                var changed = this.autoSize != value;
                this.autoSize = value;
                if (changed)
                {
                    this.OnPropertyChanged("AutoSize");
                }
            }
        }

        /// <summary>
        /// Gets the control collection containing the child controls.
        /// </summary>
        public virtual ControlsCollection Controls { get; private set; }

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

                // this.SetSizeInternal(this.Size);
            }
        }

        /// <summary>
        /// Gets or sets the opacity factor applied to the entire <see cref="Control" /> when it is rendered in the user interface (UI).  This is a dependency property.
        /// </summary>
        /// <returns>
        /// The opacity factor. Default opacity is 1.0. Expected values are between 0.0 and 1.0.
        /// </returns>
        public virtual float Opacity
        {
            get
            {
                return this.opacity;
            }
            set
            {
                value = Math.Min(1, value);
                value = Math.Max(0, value);
                var changed = Math.Abs(this.opacity - value) > float.Epsilon;
                this.opacity = value;
                if (changed)
                {
                    this.OnPropertyChanged("Opacity");
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

        /// <summary>Gets the default size of the control.</summary>
        /// <returns>The default <see cref="Size" /> of the control.</returns>
        protected virtual Size DefaultSize
        {
            get
            {
                return Size.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the extended properties collection.
        /// </summary>
        public virtual IDictionary<string, object> Properties
        {
            get
            {
                return this.properties;
            }

            set
            {
                var changed = this.properties != value;
                this.properties = value;
                if (changed)
                {
                    this.OnPropertyChanged("Properties");
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
                this.Size = new Size(this.size.Width, value);
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
        /// Gets a value indicating whether this control is visible in the user interface (UI).
        /// </summary>
        /// <returns>
        /// true if the element is visible; otherwise, false.
        /// </returns>
        /// <remarks>This property ensures that the control is potentially visible on screen by walking up the parent hierarchy and ensuring
        /// that all parents are visible as well.</remarks>
        public virtual bool IsVisible
        {
            get
            {
                if (this.Parent == null)
                {
                    return this.Visibility == Visibility.Visible;
                }

                var control = this.Parent;
                while (control != null)
                {
                    if (control.Visibility != Visibility.Visible)
                    {
                        return false;
                    }

                    control = control.Parent;
                }

                return true;
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
                var props = this.Properties;
                if (props != null && props.ContainsKey(IsMouseOverKey))
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
                this.Location = new Point(value, this.location.Y);
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
                return this.location.Y;
            }

            set
            {
                this.Location = new Point(this.location.X, value);
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
                    this.Size = this.size;
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
                    this.Size = this.size;
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
                if (value.Width < this.minSize.Width && Math.Abs(this.minSize.Width) > float.Epsilon)
                {
                    value.Width = this.minSize.Width;
                }
                else if (value.Width > this.maxSize.Width && Math.Abs(this.maxSize.Width) > float.Epsilon)
                {
                    value.Width = this.maxSize.Width;
                }

                if (value.Height < this.minSize.Height && Math.Abs(this.minSize.Height) > float.Epsilon)
                {
                    value.Height = this.minSize.Height;
                }
                else if (value.Height > this.maxSize.Height && Math.Abs(this.maxSize.Height) > float.Epsilon)
                {
                    value.Height = this.maxSize.Height;
                }

                if (this.size == value)
                {
                    return;
                }

                this.SetSizeInternal(value);
            }
        }

        private void SetSizeInternal(Size value)
        {
            var widthChanged = Math.Abs(this.size.Width - value.Width) > float.Epsilon;
            var heightChanged = Math.Abs(this.size.Height - value.Height) > float.Epsilon;

            this.previousSize = this.size;
            this.size = value;
            this.OnPropertyChanged("Size");
            if (widthChanged)
            {
                this.OnPropertyChanged("Width");
                switch (this.horizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                    case HorizontalAlignment.Center:
                        this.OnPropertyChanged("Right");
                        break;

                    case HorizontalAlignment.Right:
                        this.Left -= this.size.Width - this.previousSize.Width;
                        break;

                    case HorizontalAlignment.Stretch:
                        if (Math.Abs(this.Width) < float.Epsilon && this.Parent != null)
                        {
                            this.Width = this.Parent.Width;
                        }
                        break;
                }
            }

            if (heightChanged)
            {
                this.OnPropertyChanged("Height");
                switch (this.verticalAlignment)
                {
                    case VerticalAlignment.Top:
                    case VerticalAlignment.Center:
                        this.OnPropertyChanged("Bottom");
                        break;

                    case VerticalAlignment.Bottom:
                        this.Top -= this.size.Height - this.previousSize.Height;
                        break;

                    case VerticalAlignment.Stretch:
                        if (Math.Abs(this.Height) < float.Epsilon && this.Parent != null)
                        {
                            this.Height = this.Parent.Height;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Retrieves the child control that is located at the specified coordinates, specifying whether to ignore child controls of a certain type.
        /// </summary>
        /// <returns>
        /// The child <see cref="Control" /> at the specified coordinates.
        /// </returns>
        /// <param name="point">A <see cref="Point" /> that contains the coordinates where you want to look for a control. 
        /// Coordinates are expressed relative to the upper-left corner of the control's client area.</param>
        /// <param name="skipValue">One of the values of <see cref="GetChildAtPointSkip" />, determining whether to ignore child controls of a certain type.</param>
        public virtual Control GetChildAtPoint(Point point, GetChildAtPointSkip skipValue)
        {
            lock (this.Controls)
            {
                for (var index = this.Controls.Count - 1; index >= 0; index--)
                {
                    var control = this.Controls[index];
                    if (point.X >= control.Left && point.X <= control.Left + control.Width &&
                        point.Y >= control.Top && point.Y <= control.Top + control.Height)
                    {
                        return control;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Retrieves the child control that is located at the specified coordinates.
        /// </summary>
        /// <returns>
        /// A <see cref="Control" /> that represents the control that is located at the specified point.
        /// </returns>
        /// <param name="point">A <see cref="Point" /> that contains the coordinates where you want to look for a control. 
        /// Coordinates are expressed relative to the upper-left corner of the control's client area. </param>
        public virtual Control GetChildAtPoint(Point point)
        {
            return this.GetChildAtPoint(point, GetChildAtPointSkip.None);
        }


        /// <summary>
        /// Computes the location of the specified screen point into client coordinates.
        /// </summary>
        /// <returns>
        /// A <see cref="Point" /> that represents the converted <see cref="Point" />, <paramref name="screenPoint" />, in client coordinates.
        /// </returns>
        /// <param name="screenPoint">The screen coordinate <see cref="Point" /> to convert. </param>
        public virtual Point PointToClient(Point screenPoint)
        {
            var controlPositionOnScreen = this.PointToScreen(Point.Empty);  
            return screenPoint - controlPositionOnScreen;
        }

        /// <summary>
        /// Computes the location of the specified client point into screen coordinates.
        /// </summary>
        /// <returns>
        /// A <see cref="Point" /> that represents the converted <see cref="Point" />, <paramref name="clientPoint" />, in screen coordinates.
        /// </returns>
        /// <param name="clientPoint">The client coordinate <see cref="Point" /> to convert. </param>
        public virtual Point PointToScreen(Point clientPoint)
        {
            var parentControl = this.Parent;
            clientPoint += this.Location;
            while (parentControl != null)
            {
                clientPoint += parentControl.Location;
                parentControl = parentControl.Parent;
            }

            return clientPoint;
        }

        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
        /// </summary>
        /// <returns>
        /// The <see cref="Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.
        /// </returns>
        public Point Location
        {
            get
            {
                return this.location;
            }
            set
            {
                var leftChanged = Math.Abs(this.location.X - value.X) > float.Epsilon;
                var topChanged = Math.Abs(this.location.Y - value.Y) > float.Epsilon;

                if (leftChanged && this.horizontalAlignment == HorizontalAlignment.Center)
                {
                    value.X = this.location.X;
                }

                if (topChanged && this.verticalAlignment == VerticalAlignment.Center)
                {
                    value.Y = this.location.Y;
                }

                if (this.location == value)
                {
                    return;
                }

                leftChanged = Math.Abs(this.location.X - value.X) > float.Epsilon;
                topChanged = Math.Abs(this.location.Y - value.Y) > float.Epsilon;

                this.location = value;
                this.OnPropertyChanged("Location");
                if (leftChanged)
                {
                    this.OnPropertyChanged("Left");
                    switch (this.horizontalAlignment)
                    {

                        case HorizontalAlignment.Center:
                            // ignore
                            break;
                        case HorizontalAlignment.Left:
                        case HorizontalAlignment.Right:
                        case HorizontalAlignment.Stretch:
                            this.OnPropertyChanged("Right");
                            break;
                    }
                }

                if (topChanged)
                {
                    this.OnPropertyChanged("Top");
                    switch (this.verticalAlignment)
                    {
                        case VerticalAlignment.Center:
                            // ignore
                            break;
                        case VerticalAlignment.Top:
                        case VerticalAlignment.Bottom:
                        case VerticalAlignment.Stretch:
                            this.OnPropertyChanged("Bottom");
                            break;
                    }
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
                this.Size = new Size(value, this.size.Height);
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

        #region Overrides of Object

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", string.IsNullOrEmpty(this.name) ? string.Empty : this.name + " ", this.GetType().Name);
        }

        #endregion

        /// <summary>
        /// Builds a <see cref="Markup"/> object that represent the state of the control.
        /// </summary>
        /// <returns>A <see cref="Markup"/> object containing the relavent control information.</returns>
        /// <remarks>
        /// <p>The returned <see cref="Markup"/> object represents </p>
        /// </remarks>
        public virtual Markup ToMarkup()
        {
            var markup = new Markup();
            markup.Name = this.GetType().FullName;
            markup.SetProperty("Name", this.Name != null, this.Name);
            markup.SetProperty("Location", this.Location != Point.Empty, this.Location);
            markup.SetProperty("Size", this.Size != this.DefaultSize, this.Size);
            markup.SetProperty("MinimumSize", this.MinimumSize != Size.Empty, this.MinimumSize);
            markup.SetProperty("MaximumSize", this.MaximumSize != Size.Empty, this.MaximumSize);
            markup.SetProperty("Opacity", Math.Abs(this.Opacity - 1) > float.Epsilon, this.Opacity);
            markup.SetProperty("Foreground", this.Foreground != null, this.Foreground);
            markup.SetProperty("Background", this.Background != null, this.Background);
            markup.SetProperty("Font", this.Font != null, this.Font);
            markup.SetProperty("AutoSize", this.AutoSize, this.AutoSize);
            markup.SetProperty("ClipToBounds", !this.ClipToBounds, this.ClipToBounds);
            markup.SetProperty("IsEnabled", !this.IsEnabled, this.IsEnabled);
            markup.SetProperty("Visibility", this.Visibility != Visibility.Visible, this.Visibility);
            markup.SetProperty("HorizontalAlignment", this.HorizontalAlignment != HorizontalAlignment.Left, this.HorizontalAlignment);
            markup.SetProperty("HorizontalContentAlignment", this.HorizontalContentAlignment != HorizontalAlignment.Left, this.HorizontalContentAlignment);
            markup.SetProperty("VerticalAlignment", this.VerticalAlignment != VerticalAlignment.Top, this.VerticalAlignment);
            markup.SetProperty("VerticalContentAlignment", this.VerticalContentAlignment != VerticalAlignment.Top, this.VerticalContentAlignment);
            markup.SetProperty("ToolTip", this.ToolTip != null, this.ToolTip);
            markup.SetProperty("Tag", this.Tag != null, this.Tag);
            markup.SetProperty("DataContext", this.DataContext != null, this.DataContext);
            var props = this.Properties;
            markup.SetProperty("Properties", props != null && props.Count > 0, new Dictionary<string, object>(this.properties));
            markup.Children = this.Controls.Select(x => x.ToMarkup()).ToList();
            return markup;
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