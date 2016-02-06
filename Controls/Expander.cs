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
        /// Gets or sets the direction in which the <see cref="Expander" /> content opens.  
        /// </summary>
        /// <returns>
        /// One of the <see cref="ExpandDirection" /> values that defines which direction the content opens. The default is <see cref="ExpandDirection.Down" />. 
        /// </returns>
        public ExpandDirection ExpandDirection
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
    }
}