// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Panel.cs" company="Codefarts">
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>
// <summary>
//   The panel.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Codefarts.UIControls
{
    /// <summary>
    /// The panel.
    /// </summary>
    public class Panel : Control
    {
        #region Fields

        /// <summary>
        /// Holds the child controls.
        /// </summary>
        protected ControlCollection children;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel()
        {
            this.children = new ControlCollection();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the child control list.
        /// </summary> 
        public virtual ControlCollection Children
        {
            get
            {
                return this.children;
            }

            set
            {
                this.children = value;
            }
        }

        #endregion
    }
}