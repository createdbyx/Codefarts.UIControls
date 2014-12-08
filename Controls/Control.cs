// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="Control.cs">
//   Copyright (c) 2012 Codefarts
//     All rights reserved.
//     contact@codefarts.com
//     http://www.codefarts.com
// </copyright>
// <summary>
//   The control.
// </summary>
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
        /// The height of the control.
        /// </summary>
        private float height;

        /// <summary>
        /// The width of the control.
        /// </summary>
        private float width;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        public Control()
        {
            this.Visibility = Visibility.Visible;
            this.MinWidth = 0;
            this.MinHeight = 0;
            this.MaxWidth = float.MaxValue;
            this.MaxHeight = float.MaxValue;
            this.IsEnabled = true;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        public object DataContext { get; set; }

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
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the left position of the control.
        /// </summary>
        public virtual float Left { get; set; }

        /// <summary>
        /// Gets or sets the max height of the control.
        /// </summary>
        public virtual float MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the max width of the control.
        /// </summary>
        public virtual float MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the min height of the control.
        /// </summary>
        public virtual float MinHeight { get; set; }

        /// <summary>
        /// Gets or sets the min width of the control.
        /// </summary>
        public virtual float MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public virtual object Tag { get; set; }

        /// <summary>
        /// Gets or sets the top of the control.
        /// </summary>
        public virtual float Top { get; set; }

        /// <summary>
        /// Gets or sets the controls visibility.
        /// </summary>
        public virtual Visibility Visibility { get; set; }

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