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

    /// <summary>
    ///     The control.
    /// </summary>
    public class Control
    {
        public const string IsMouseOverKey = "F87213B4-B311-4B40-B7BB-39B6083309D0";
        public const string PreviousMousePositionKey = "422715F7-53E9-4177-845A-627F388F4608";

        #region Fields

        /// <summary>
        ///     The height of the control.
        /// </summary>
        private float height;

        /// <summary>
        ///     The horizontal alignment.
        /// </summary>
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Stretch;

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
        private VerticalAlignment verticalAlignment = VerticalAlignment.Stretch;

        /// <summary>
        ///     The visibility of the control.
        /// </summary>
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        ///     The width of the control.
        /// </summary>
        private float width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Control" /> class.
        /// </summary>
        public Control()
        {
            this.ClipToBounds = false;
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
        public virtual Brush Background { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether child controls are clipped to the bounds of this control.
        /// </summary>
        public bool ClipToBounds { get; set; }

        /// <summary>
        ///     Gets or sets the data context.
        /// </summary>
        public virtual object DataContext { get; set; }

        /// <summary>
        ///     Gets or sets the extended properties collection.
        /// </summary>
        public virtual PropertyCollection ExtendedProperties { get; set; }

        /// <summary>
        ///     Gets or sets a brush that describes the foreground color.
        /// </summary>
        /// <returns>
        ///     The brush that paints the foreground of the control.
        /// </returns>
        public virtual Brush Foreground { get; set; }

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
                }
                else if (value > this.MaxHeight)
                {
                    this.height = this.MaxHeight;
                }
                else
                {
                    this.height = value;
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
                this.horizontalAlignment = value;
            }
        }

        /// <summary>
        ///     Gets or sets the horizontal alignment of the control's content.
        /// </summary>
        public virtual HorizontalAlignment HorizontalContentAlignment { get; set; }

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
                this.isEnabled = value;
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
        public virtual float MarginBottom { get; set; }

        /// <summary>
        ///     Gets or sets the length of the margins left side.
        /// </summary>
        public virtual float MarginLeft { get; set; }

        /// <summary>
        ///     Gets or sets the length of the margins right side.
        /// </summary>
        public virtual float MarginRight { get; set; }

        /// <summary>
        ///     Gets or sets the length of the margins top side.
        /// </summary>
        public virtual float MarginTop { get; set; }

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
                this.maxHeight = value;
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
                this.maxWidth = value;
            }
        }

        /// <summary>
        ///     Gets or sets the min height of the control.
        /// </summary>
        public virtual float MinHeight { get; set; }

        /// <summary>
        ///     Gets or sets the min width of the control.
        /// </summary>
        public virtual float MinWidth { get; set; }

        /// <summary>
        ///     Gets or sets the name of the control.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Gets or sets the tag.
        /// </summary>
        public virtual object Tag { get; set; }

        /// <summary>
        ///     Gets or sets the tool tip for the control.
        /// </summary>
        public virtual string ToolTip { get; set; }

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
                this.verticalAlignment = value;
            }
        }

        /// <summary>
        ///     Gets or sets the vertical alignment of the control's content.
        /// </summary>
        public virtual VerticalAlignment VerticalContentAlignment { get; set; }

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
                this.visibility = value;
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
                }
                else if (value > this.MaxWidth)
                {
                    this.width = this.MaxWidth;
                }
                else
                {
                    this.width = value;
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
            EventHandler<KeyEventArgs> handler = this.KeyDown;
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
            EventHandler<KeyEventArgs> handler = this.KeyUp;
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
    }
}