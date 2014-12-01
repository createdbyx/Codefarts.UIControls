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
    using System;

    using UnityEngine;

    /// <summary>
    /// The button.
    /// </summary>
    public class Button : Control
    {                   
        #region Public Events

        public event EventHandler Click;

        #endregion

        #region Public Properties

        public string Text { get; set; }

        /// <summary>
        /// Gets or sets Texture.
        /// </summary>
        public image Texture Texture { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on click.
        /// </summary>
        public void OnClick()
        {
            var handler = this.Click;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}