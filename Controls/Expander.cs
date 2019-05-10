/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.UIControls
{
    /// <summary>
    /// Represents the control that displays a header that has a collapsible content.
    /// </summary>
    public class Expander : Control
    {
        /// <summary>
        /// The backing field for the <see cref="Content"/> property.
        /// </summary>
        protected Control content;

        /// <summary>
        /// The backing field for the <see cref="Text"/> property.
        /// </summary>
        protected string text = string.Empty;

        /// <summary>
        /// The backing field for the <see cref="IsExpanded"/> property.
        /// </summary>
        protected bool isExpanded;

        /// <summary>
        /// The backing field for the <see cref="ExpandDirection"/> property.
        /// </summary>
        protected ExpandDirection expandDirection;


        /// <summary>
        /// Initializes a new instance of the <see cref="Expander"/> class.
        /// </summary>
        public Expander()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expander"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        public Expander(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expander"/> class.
        /// </summary>
        /// <param name="name">The name of the control.</param>
        /// <param name="text">The text displayed in the expander header.</param>
        public Expander(string name, string text) : base(name)
        {
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the direction in which the <see cref="Expander" /> content opens.
        /// </summary>
        /// <returns>
        /// One of the <see cref="ExpandDirection" /> values that defines which direction the content opens. The default is <see cref="UIControls.ExpandDirection.Down" />.
        /// </returns>
        public virtual ExpandDirection ExpandDirection
        {
            get
            {
                return this.expandDirection;
            }

            set
            {
                var changed = this.expandDirection != value;
                this.expandDirection = value;
                if (changed)
                {
                    this.OnPropertyChanged("ExpandDirection");
                }
            }
        }

        /// <summary>
        /// Gets or sets the content control that houses the expander content.
        /// </summary>
        public virtual Control Content
        {
            get
            {
                return this.content;
            }

            set
            {
                var changed = this.content != value;
                this.content = value;
                if (changed)
                {
                    this.OnPropertyChanged("Content");
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the <see cref="Expander" /> content is visible.
        /// </summary>
        /// <returns>
        /// true if the content is expanded; otherwise, false. The default is false.
        /// </returns>
        public virtual bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }

            set
            {
                var changed = this.isExpanded != value;
                this.isExpanded = value;
                if (changed)
                {
                    this.OnPropertyChanged("IsExpanded");
                }
            }
        }

        /// <summary>
        /// Gets or sets the header text.
        /// </summary>
        public virtual string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                var changed = this.text != value;
                this.text = value;
                if (changed)
                {
                    this.OnPropertyChanged("Text");
                }
            }
        }

        #region Overrides of Control

        /// <summary>Gets the default size of the control.</summary>
        /// <returns>The default <see cref="Control.Size" /> of the control.</returns>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(75, 20);
            }
        }

        #endregion
    }
}