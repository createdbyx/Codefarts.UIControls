// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Control.cs">
//   Copyright (c) 2012 Codefarts
//     All rights reserved.
//     contact@codefarts.com
//     http://www.codefarts.com
// </copyright>     
// --------------------------------------------------------------------------------------------------------------------
namespace Codefarts.UIControls
{
    /// <summary>
    /// The control.
    /// </summary>
    public class Control
    {
        #region Fields

        /// <summary>
        /// The clip to bounds value.
        /// </summary>
        private bool clipToBounds = false;

        /// <summary>
        /// The height of the control.
        /// </summary>
        private float height;

        /// <summary>
        /// The width of the control.
        /// </summary>
        private float width;

        /// <summary>
        /// Holds weather the control is enabled.
        /// </summary>
        private bool isEnabled = true;

        /// <summary>
        /// The horizontal alignment.
        /// </summary>
        private HorizontalAlignment horizontalAlignment = HorizontalAlignment.Stretch;

        /// <summary>
        /// The vertical alignment.
        /// </summary>
        private VerticalAlignment verticalAlignment = VerticalAlignment.Stretch;

        /// <summary>
        /// The visibility of the control.
        /// </summary>
        private Visibility visibility = Visibility.Visible;

        /// <summary>
        /// The minimum width.
        /// </summary>
        private float minWidth;

        /// <summary>
        /// The minimum height.
        /// </summary>
        private float minHeight;

        /// <summary>
        /// The maximum width.
        /// </summary>
        private float maxWidth = float.MaxValue;

        /// <summary>
        /// The maximum height.
        /// </summary>
        private float maxHeight = float.MaxValue;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the extended properties collection.
        /// </summary>       
        public virtual PropertyCollection ExtendedProperties { get; set; }

        /// <summary>
        ///     Gets or sets a brush that describes the background of a control. 
        /// </summary>
        /// <returns>
        ///     The brush that is used to fill the background of the control.   
        /// </returns>
        public virtual Brush Background { get; set; }

        /// <summary>
        ///     Gets or sets a brush that describes the foreground color.   
        /// </summary>
        /// <returns>
        ///     The brush that paints the foreground of the control. 
        /// </returns>
        public virtual Brush Foreground { get; set; }

        /// <summary>
        /// Gets or sets the length of the margins left side.
        /// </summary>
        public virtual float MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the length of the margins top side.
        /// </summary>
        public virtual float MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the length of the margins right side.
        /// </summary>
        public virtual float MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the length of the margins bottom side.
        /// </summary>
        public virtual float MarginBottom { get; set; }

        /// <summary>
        /// Gets or sets the horizontal alignment characteristics applied to this element when it is composed within a parent control.
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
        /// Gets or sets the vertical alignment characteristics applied to this element when it is composed within a parent control.
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
        /// Gets or sets the horizontal alignment of the control's content.
        /// </summary>
        public virtual HorizontalAlignment HorizontalContentAlignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment of the control's content.
        /// </summary>
        public virtual VerticalAlignment VerticalContentAlignment { get; set; }

        /// <summary>
        /// Gets or sets the tool tip for the control.
        /// </summary>
        public virtual string ToolTip { get; set; }

        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        public virtual object DataContext { get; set; }

        /// <summary>
        /// Gets or sets the height of the control.
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
        /// Gets or sets a value indicating whether child controls are clipped to the bounds of this control.
        /// </summary>    
        public bool ClipToBounds
        {
            get
            {
                return this.clipToBounds;
            }
            set
            {
                this.clipToBounds = value;
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
                this.isEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the max height of the control.
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
        /// Gets or sets the max width of the control.
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
        /// Gets or sets the min height of the control.
        /// </summary>
        public virtual float MinHeight
        {
            get
            {
                return this.minHeight;
            }

            set
            {
                this.minHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the min width of the control.
        /// </summary>
        public virtual float MinWidth
        {
            get
            {
                return this.minWidth;
            }

            set
            {
                this.minWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public virtual object Tag { get; set; }

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
                this.visibility = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the control.
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
    }
}