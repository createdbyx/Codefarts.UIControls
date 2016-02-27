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
    /// Used to group collections of controls.
    /// </summary>
    public class Panel : ScrollViewer
    {
        #region Public Properties

        /// <summary>
        /// Indicates the automatic sizing behavior of the control.
        /// </summary>
        /// <returns>
        /// One of the <see cref="AutoSizeMode" /> values.
        /// </returns>
        public virtual AutoSizeMode AutoSizeMode { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Panel"/> class.
        /// </summary>
        public Panel()
        {
            this.horizontialScrollBarVisibility = ScrollBarVisibility.Hidden;
            this.verticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        }
    }
}